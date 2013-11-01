﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

///// <summary>
///// 介面Type定義
///// </summary>
//public enum UIFormType
//{
//    None = 0,  // 0. 無
//    Start = 1, // 1. 開始介面
//}


/// <summary>
/// 遊戲中的UI管理器，先認定都是2D介面
/// By Feles
/// </summary>
public class GUIStation
{
    public const int MANUAL_SCREEN_WIDTH    = 1920; //鏡頭指定解析度 W, 與螢幕解析度無關
    public const int MANUAL_SCREEN_HEIGHT   = 1080;   //鏡頭指定解析度 H, 與螢幕解析度無關

    private GameControl _gameControl = null;
    private UIRoot _uiRoot = null;

    // 正準備隱藏/顯示的UI，如果UI/顯示/隱藏後的delegate有需要知道該UI，由此處取得
    public static GUIFormBase currentHideUI;
    public static GUIFormBase currentShowUI;

    #region UI攝影機相關
    private Camera _camera = null;  // 顯示UI用的Camera
    private UICamera _uiCamera = null; // 為了事件處理使用

    public static int ScreenWidth { get; set; }    //螢幕解析度 W
    public static int ScreenHeight { get; set; }   //螢幕解析度 H

    /// <summary>
    /// 顯示UI用的攝影機
    /// </summary>
    public Camera GUICamera
    {
        get { return _camera; }
    }

    /// <summary>
    /// UICamera的深度
    /// </summary>
    public float UICameraDepth
    {
        set { if (_camera != null) { _camera.depth = value; } }
    }

    public LayerMask UICameraEventReceiverMask
    {
        set { if (_uiCamera != null) { _uiCamera.eventReceiverMask = value; } }
    }
    
    #endregion

    private Dictionary<Type, GUIFormBase> _guiReference; // GUIForm參照儲存
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="control"></param>
    public GUIStation(GameControl control)
    {
        _gameControl = control;
        GUIStation.ScreenHeight = Screen.height;
        GUIStation.ScreenWidth = Screen.width;

        #region 建立必要元件
        // 如果場景上有兩個以上的UIRoot，會管理不到，請將多餘的刪除
        _uiRoot = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
        if (_uiRoot == null)
        {
            // 沒有的話建立Simple2D的UI Root
            GameObject rootObj = new GameObject("UI Root (2D)");
            GameObject.DontDestroyOnLoad(rootObj);
            _uiRoot = rootObj.AddComponent<UIRoot>();
            _uiRoot.scalingStyle = UIRoot.Scaling.FixedSizeOnMobiles;
            _uiRoot.manualHeight = MANUAL_SCREEN_HEIGHT; // TODO:先設死，之後測試用 Screen.height; //sh131020 marked, remove shortly
        }

        _uiRoot.gameObject.layer = GLOBALCONST.LAYER_UI_BASE;
        // 如果場景上有兩個以上的UICamera會管理不到，請將多餘的刪除
        _uiCamera = GameObject.FindObjectOfType(typeof(UICamera)) as UICamera;
        // 取得UICamera attach上的物件的camera
        if (_uiCamera == null)
        {   // 沒有UICamera attach的物件，則建立一個新物件並且attach 一個Camera 
            _camera = NGUITools.AddChild<Camera>(_uiRoot.gameObject);
            _uiCamera = _camera.gameObject.AddComponent<UICamera>();
        }
        else
        {
            // 將UICamera移到UIRoot底下
            _uiCamera.transform.parent = _uiRoot.transform; 
            // 有UICamera attach的物件，取得該物件上的Camera，若無，attach一個
            _camera = _uiCamera.gameObject.GetComponent<Camera>();
            if (_camera == null) { _camera = _uiCamera.gameObject.AddComponent<Camera>(); }
        }
        // 調整camera參數
        _camera.depth = 0;
        _camera.backgroundColor = Color.grey;
        _camera.cullingMask = 1 << GLOBALCONST.LAYER_UI_BASE;

        _camera.orthographicSize = 1f;
        _camera.orthographic = true;
        _camera.nearClipPlane = -2f;
        _camera.farClipPlane = 2f;
        _camera.clearFlags = CameraClearFlags.Depth;

        _uiCamera.eventReceiverMask = 1 << GLOBALCONST.LAYER_UI_BASE; // 只接收uiBase layer的事件
        #endregion

        _guiReference = new Dictionary<Type, GUIFormBase>();

    }
    /// <summary>
    /// 解構式
    /// </summary>
    ~GUIStation()
    {
        NGUITools.Destroy(_uiCamera);
        _uiCamera = null;
        NGUITools.Destroy(_camera);
        _camera = null;
        NGUITools.Destroy(_uiRoot);
        _uiRoot = null;

        _guiReference = null;
        _gameControl = null;
    }

    //todo: protect
    public AccountData Account
    {
        get { return _gameControl.Account; }
    }

    /// <summary>
    /// 重新設定場景上所有攝影機，將其cullingMask避開NGUI使用的layer（目前是GLOBALCONST.LAYER_UI_BASE）
    /// 並且重設看UI用的Camera的深度，使UI在最上層
    /// </summary>
    public void ResetAllCamera()
    {
        float depth = -1f;
        int mask = 1 << GLOBALCONST.LAYER_UI_BASE;

        Camera[] allCamera = GameObject.FindObjectsOfType(typeof(Camera)) as Camera[];
        foreach (Camera c in allCamera)
        {
            depth = Mathf.Max(depth, c.depth);
            // 並非只是看UI的攝影機，去掉看ui那層layer
            if (c.cullingMask != mask) { c.cullingMask = c.cullingMask & ~mask; }
        }
        UICameraDepth = depth + 1;   
    }

    #region UI取得相關
    /// <summary>
    /// 傳入 UI Class 型別，取得實體介面
    /// </summary>
    /// <typeparam name="T">UI Class 型別</typeparam>
    /// <returns>實體化之介面</returns>
    public T Form<T>() where T : GUIFormBase
    {
        GUIFormBase retUI;

        if (_guiReference == null) { _guiReference = new Dictionary<Type, GUIFormBase>(); }

        if (!_guiReference.TryGetValue(typeof(T), out retUI))
        {
            retUI = InstantiateContainer(typeof(T));
            retUI.CreateUI(this);
        }
        return (retUI as T);
    }

    /// <summary>
    /// 實體化介面容器（該UI的Root，該UI所有建立的GameObject都放置在其下）
    /// </summary>
    /// <param name="uiType">ui的type</param>
    /// <returns>該UI</returns>
    private GUIFormBase InstantiateContainer(Type uiType)
    {
        GUIFormBase retUI = null;
        _guiReference.TryGetValue(uiType, out retUI);
        if (retUI != null)
        {
            CommonFunction.DebugMsgFormat(" InstantiateContainer failed. 已經實體化過介面: {0}", uiType.ToString());
            return null;
        }
        string componentName = uiType.ToString();
        GameObject uiObject = NGUITools.AddChild(_camera.gameObject);
        uiObject.name = componentName + "Object";
        retUI = uiObject.AddComponent(componentName) as GUIFormBase;
        if (retUI == null)
        {
            CommonFunction.DebugMsgFormat(" AddComponent failed. 介面: {0}", componentName);
            return retUI;
        }

        _guiReference.Add(uiType, retUI);
        return retUI;
    }
    #endregion


    /// <summary>
    /// 顯示uiType表示的ui，並且關閉其他UI
    /// </summary>
    /// <param name="uiType">要顯示的UI</param>
    public void ShowAndHideOther(Type uiType)
    {
        if (!typeof(GUIFormBase).IsAssignableFrom(uiType)) {return;} // 並非繼承自GUIFormBase的uiType，不做事

        if (_guiReference == null) { _guiReference = new Dictionary<Type, GUIFormBase>(); }

        List<GUIFormBase> allVisibleUI =  _guiReference.Values.Where(ui => ui.Visible).ToList();

        GUIFormBase showUI;
        if (!_guiReference.TryGetValue(uiType, out showUI))
        {
            showUI = InstantiateContainer(uiType);
            showUI.CreateUI(this);
        }

        GUIFormBase preUI = null;
        GUIFormBase curUI = null;

        if (allVisibleUI.Count > 0)
        {
            foreach (GUIFormBase ui in allVisibleUI)
            {
                preUI = curUI;
                curUI = ui;
                if (preUI != null && curUI != null)
                {
                    preUI.AddShowOrHideFinishedDelegate(false, new EventDelegate(curUI, "Hide"));
                }
            }
            curUI.AddShowOrHideFinishedDelegate(false, new EventDelegate(showUI, "Show"));
            allVisibleUI.First().Hide();
        }
        else
        {
            showUI.Show();
        }
    }

    ///// <summary>
    ///// 顯示前一個UI
    ///// </summary>
    //public void ShowPreviouUI()
    //{
    //    _previousShowUI.Show();
    //}

    public void TestDestroy<T>()
    {
         GUIFormBase deUI;

         if (_guiReference.TryGetValue(typeof(T), out deUI))
         {
             _guiReference.Remove(typeof(T));
             NGUITools.Destroy(deUI.gameObject);
         }
         deUI = null;
    }


    #region NGUI 基本元件相關
    public static UISprite CreateUISprite(GameObject parentObj, string spriteObjName, UISprite.Type spriteType, int depth, UIAtlas atlas, string spriteName,
        UISprite.Pivot pivot, int width, int height)
    {
        UISprite retSprite = NGUITools.AddWidget<UISprite>(parentObj);
        retSprite.atlas = atlas;
        retSprite.spriteName = spriteName;
        retSprite.Init(spriteType, depth, pivot, width, height);
        retSprite.name = spriteObjName;
        retSprite.transform.localPosition = Vector3.zero;

        return retSprite;
    }

    /// <summary>
    /// 從 resouce 中讀取一個 texture 
    /// </summary>
    /// <returns></returns>
    public static Texture LoadTextureFromResource(string textureName)
    {
        try
        {
            Texture uiImage = Resources.Load(GLOBALCONST.DIR_RESOURCES_NGUI + textureName, typeof(Texture)) as Texture;

            CommonFunction.DebugMsg(" textureName : " + textureName);
            CommonFunction.DebugMsg(string.Format("   uiImage {0} : {1}", textureName, uiImage.width.ToString()));

            return uiImage;
        }
        catch (Exception e)
        {
            CommonFunction.DebugMsg("Exception : " + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 依據設定 建立一個UIButton
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="btnName">按鈕名稱</param>
    /// <param name="relativePos">按鈕位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="btnBG">按鈕使用的圖的資訊</param>
    /// <param name="width">按鈕寬度</param>
    /// <param name="height">按鈕高度</param>
    /// <param name="font">使用的字型</param>
    /// <param name="btnLabelText">按鈕標籤文字(沒有傳入字體時無作用)</param>
    /// <param name="btnLabelColor">按鈕標籤顏色(沒有傳入字體時無作用)</param>
    /// <returns>建出的UIButton</returns>
    public static UIButton CreateUIButton(GameObject parentObj, string btnName, Vector3 relativePos, int depth, SpriteName btnBG, int width, int height,
        UIFont font, Color btnLabelColor, string btnLabelText)
    {
        GameObject retButtonObj = NGUITools.AddChild(parentObj);
        retButtonObj.name = btnName;
        retButtonObj.transform.localPosition = relativePos;
        // 設定按鈕背景圖
        UISprite bg = UIImageManager.CreateUISprite(retButtonObj, btnBG);

        bg.Init(UISprite.Type.Sliced, depth, bg.pivot, width, height);
        bg.name = "Background";
        // font
        if (font != null)
        {
            UILabel lbl = NGUITools.AddWidget<UILabel>(retButtonObj);
            lbl.font = font;
            lbl.text = btnLabelText;
            lbl.color = btnLabelColor;
            lbl.MakePixelPerfect();
        }
        // Add a Collider
        NGUITools.AddWidgetCollider(retButtonObj);
        // Add the scripts
        retButtonObj.AddComponent<UIPlaySound>();
        UIButton retBtn = retButtonObj.AddComponent<UIButton>();
        retBtn.tweenTarget = bg.gameObject;

        return retBtn;
    }

    /// <summary>
    /// 依據設定 建立一個Progress Bar
    /// </summary>
    /// <param name="parentObj">parent的GameObject</param>
    /// <param name="progressBarName">Progress Bar名稱</param>
    /// <param name="relativePos">Progress Bar位置（相對於parentObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="foreground">前景圖資訊</param>
    /// <param name="background">背景圖資訊</param>
    /// <param name="width">Progress Bar（=背景圖）寬度 </param>
    /// <param name="height">Progress Bar（=背景圖）高度</param>
    /// <returns>建出的 ProgressBar</returns>
    public static UISlider CreateUIProgressBar(GameObject parentObj, string progressBarName, Vector3 relativePos, int depth,
        SpriteName foreground, SpriteName background, int width, int height)
    {
        GameObject progressBarObject = NGUITools.AddChild(parentObj);
        progressBarObject.name = progressBarName;
        progressBarObject.transform.localPosition = relativePos;

        // Background sprite (傳入空值，不產生sprite
        if (background != SpriteName.NONE)
        {
            UISprite back = UIImageManager.CreateUISprite(progressBarObject, background);
            back.Init(back.type, depth, UIWidget.Pivot.Left, width, height);
            back.name = "Background";
            back.transform.localPosition = Vector3.zero; // 因為調整pivot會影響localPosition，所以需要再次重設
        }
        // Foreground sprite
        UISprite front = UIImageManager.CreateUISprite(progressBarObject, foreground);
        front.Init(front.type, depth + 1, UIWidget.Pivot.Left, width, height);
        front.name = "Foreground";
        front.transform.localPosition = Vector3.zero; // 因為調整pivot會影響localPosition，所以需要再次重設
        
        // Add the slider script
        UISlider retSilder = progressBarObject.AddComponent<UISlider>();
        retSilder.foreground = front.transform;
        retSilder.value = 0.0f;

        return retSilder;
    }

    /// <summary>
    /// 依據設定 建立一個Label
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="labelName">label名稱</param>
    /// <param name="pivot">對位方式</param>
    /// <param name="relativePos">label位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="font">使用的Font</param>
    /// <param name="labelColor">文字顏色</param>
    /// <param name="labelText">文字內容</param>
    /// <returns>建出的Label</returns>
    public static UILabel CreateUILabel(GameObject parentObj, string labelName, UIWidget.Pivot pivot, Vector3 relativePos, int depth,
        UIFont font, Color labelColor, string labelText, bool outline = true)
    {
        UILabel retLabel = NGUITools.AddWidget<UILabel>(parentObj);
        retLabel.pivot = pivot;
        retLabel.transform.localPosition = relativePos;
        retLabel.name = labelName;
        retLabel.depth = depth;
        retLabel.font = font;
        retLabel.text = labelText;
        retLabel.color = labelColor;
        retLabel.effectStyle = (outline) ? UILabel.Effect.Outline : UILabel.Effect.None;
        retLabel.MakePixelPerfect();
        return retLabel;
    }
    /// <summary>
    /// 依據設定 建立一個Scroll Bar
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="scName">ScrollBar名字</param>
    /// <param name="relativePos">ScrollBar位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="atlas">使用的Atlas</param>
    /// <returns>建出的ScrollBar</returns>
    public static UIScrollBar CreateUIScrollBar(GameObject parentObj, string scName, Vector3 relativePos, int depth,
        UIAtlas atlas, string backgroundName, string foregroundName, int width, int height, UIScrollBar.Direction dir, bool canMoveThumb)
    {
        GameObject ScrollBarObject = NGUITools.AddChild(parentObj);
        ScrollBarObject.name = scName;
        ScrollBarObject.transform.localPosition = relativePos;

        UISprite bg = CreateUISprite(ScrollBarObject, "Background", UISprite.Type.Sliced, depth, atlas, backgroundName, UIWidget.Pivot.Center, width, height);
        UISprite fg = CreateUISprite(ScrollBarObject, "Foreground", UISprite.Type.Sliced, depth + 1, atlas, foregroundName, UIWidget.Pivot.Center, width, height);

        UIScrollBar retSC = ScrollBarObject.AddComponent<UIScrollBar>();
        retSC.background = bg;
        retSC.foreground = fg;
        retSC.direction = dir;
        retSC.barSize = 0.3f;
        retSC.value = 0.3f;
        retSC.ForceUpdate();
        if (canMoveThumb)
        {
            NGUITools.AddWidgetCollider(bg.gameObject);
            NGUITools.AddWidgetCollider(fg.gameObject);
        }

        return retSC;
    }

    /// <summary>
    /// 建立一個輸入用 input 元件
    /// todo: UIInput 只是"輸入框"相關元件*3 的其中一個, 回傳的 UIInput 提供使用者能做的操作 有限 & 不便, 有時間可建新input類別 + 封裝
    /// </summary>
    public static UIInput CreateUIInput(GameObject parentObject, string inputName, Vector3 relativePos, Color inputColor, string inputInitText,
        int depth, SpriteName background, UIFont font, int width, int height)
    {
        GameObject inputRootObject = NGUITools.AddChild(parentObject);
        inputRootObject.name = inputName;
        inputRootObject.transform.localPosition = relativePos;

        UISprite bg = UIImageManager.CreateUISprite(inputRootObject, background);
        bg.Init(UISprite.Type.Sliced, depth + 1, UIWidget.Pivot.Center, width, height);
        bg.name = "Background";

        UILabel targetLabel = CreateUILabel(inputRootObject, inputName + typeof(UIInput).ToString(), UIWidget.Pivot.Center,
                                            Vector3.zero, depth + 2, font, inputColor, inputInitText);
        NGUITools.AddWidgetCollider(inputRootObject);
        UIInput input = inputRootObject.AddComponent<UIInput>();
        input.label = targetLabel; // point to the label

        return input;
    }
    #endregion
}

/// <summary>
/// UI 組件產生用類別, 專案內 ui components 重複性高, 
/// 避免 ui 取得 image 與 default 值在各 UI class 實作重複性過高, 盡量集中此 class (ex: 圖名只出現於此處)
/// </summary>
public static class GUIComponents
{
    /// <summary>
    /// 在wantAttachedObj上附加一個負責播放顯示/消失的UIPlayTween
    /// p.s. 如果需要用此UIPlayTween播放其他Tween，請記得修改對應參數。
    /// </summary>
    /// <param name="wantAttachedObj">想要被附加的物件</param>
    /// <returns>對應的UIPlayTween</returns>
    public static UIPlayTween AddPlayShowTweenComponent(GameObject wantAttachedObj)
    {
        UIPlayTween retUIPlayTween = wantAttachedObj.AddComponent<UIPlayTween>();
        retUIPlayTween.tweenTarget = wantAttachedObj;
        retUIPlayTween.includeChildren = true;
        retUIPlayTween.SetForShowOrHideTween();
        return retUIPlayTween;
    }

    /// <summary>
    /// 在parentObj底下新增一顯示/隱藏用的移動效果
    /// </summary>
    /// <param name="parentObj">移動效果物件的父物件</param>
    /// <param name="moveFrom">移動起點</param>
    /// <param name="moveTo">移動終點</param>
    /// <returns>對應的TweenPosition</returns>
    public static TweenPosition AddShowMoveEffect(GameObject parentObj, Vector3 moveFrom, Vector3 moveTo)
    {
        TweenPosition tp = NGUITools.AddChild<TweenPosition>(parentObj);
        tp.from = moveFrom;
        tp.to = moveTo;
        tp.tweenGroup = GLOBALCONST.UI_ShowOrHide_TweenGroup;
        return tp;
    }

    /// <summary>
    /// 新增一顯示/隱藏用的縮放效果
    /// </summary>
    /// <param name="parentObj">縮放效果物件的父物件</param>
    /// <param name="scaleFrom">起始的縮放大小</param>
    /// <param name="scaleTo">最後的縮放大小</param>
    /// <returns>對應的TweenScale</returns>
    public static TweenScale AddShowZoomEffect(GameObject parentObj, Vector3 scaleFrom, Vector3 scaleTo)
    {
        TweenScale ts = NGUITools.AddChild<TweenScale>(parentObj);
        ts.from = scaleFrom;
        ts.to = scaleTo;
        ts.tweenGroup = GLOBALCONST.UI_ShowOrHide_TweenGroup;
        return ts;
    }


    /// <summary>
    /// 建立忍豆豆用之提示框
    /// </summary>
    public static UIButton DialogFrame(GameObject parent)
    {
        UIButton button = GUIStation.CreateUIButton(parent, "DialogFrame", Vector3.zero, 0, 
            SpriteName.DIALOG_FRAME,
            GUIStation.MANUAL_SCREEN_WIDTH / 2, GUIStation.MANUAL_SCREEN_HEIGHT / 2, null, Color.white, string.Empty);

        return button;
    }

    /// <summary>
    /// 關卡選擇外層框
    /// </summary>
    public static UISprite StageFrame(GameObject parent)
    {
        UISprite stageFrameSprite = UIImageManager.CreateUISprite(parent, SpriteName.STAGE_FRAME);
        stageFrameSprite.Init(UISprite.Type.Sliced, 1, UIWidget.Pivot.Center, (int)(GUIStation.MANUAL_SCREEN_WIDTH * 0.95f), (int)(GUIStation.MANUAL_SCREEN_HEIGHT * 0.95f));
        stageFrameSprite.name = "StageFrame";
        stageFrameSprite.transform.localPosition = Vector3.zero; // 因為調整pivot會影響localPosition，所以需要再次重設

        return stageFrameSprite;
    }

    /// <summary>
    /// 關卡選擇用寬按鈕
    /// </summary>
    public static UIButton StageWideButton(GameObject parent, int depth)
    {
        int height = CalculateStageWideButtonHeight();

        UIButton button = GUIStation.CreateUIButton(parent, "StageBtn", Vector3.zero, depth,
            SpriteName.STAGE_BG_OPEN,
            (int)(GUIStation.MANUAL_SCREEN_WIDTH * 0.9f), height, null, Color.white, string.Empty);

        button.SetColor(Color.white, Color.white, new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f), new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f));

        return button;
    }

    public static int CalculateStageWideButtonHeight()
    {
        int fixedWidth = (int)(GUIStation.MANUAL_SCREEN_HEIGHT * 0.2f); //todo: 因為按鈕內 layout fixed, height fixed 可能比較美觀, 此為暫用, 這裡待議
        return fixedWidth;
    }

    /// <summary>
    /// 建立忍豆豆用之文字訊息
    /// todo:  Label 一定建立在 panel or frame 上, parent 應該強訂為 component
    /// </summary>
    public static UILabel MessageLabel(GameObject parent, string message, Color color)
    {
        UILabel label = GUIStation.CreateUILabel(parent, "DialogMessage", UIWidget.Pivot.Center, new Vector3(0, 0, 0), 7,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                color, message);

        return label;
    }

    /// <summary>
    /// 建立忍豆豆用之通用按鈕 1.
    /// todo:  button 一定建立在 panel or frame 上, parent 應該強訂為 component
    /// </summary>
    public static UIButton DialogButton(GameObject parent, string showWord, int depth)
    {
        // todo: 不應該讓 UI 實作者處理 depth 這個參數
        UIButton button = GUIStation.CreateUIButton(parent, "DialogButton", Vector3.zero, depth,
            SpriteName.BTN_GENERIC_BG,
            116, 94,
            UIFontManager.GetUIDynamicFont(UIFontName.DragonWord),
            Color.white, showWord);

        return button;
    }

    /// <summary>
    /// 建立忍豆豆用之 通用 名稱 輸入框 1.
    /// todo:  button 一定建立在 panel or frame 上, parent 應該強訂為 component
    /// </summary>
    public static UIInput NameInputField(GameObject parent, string showWord, int depth)
    {
        // todo: 不應該讓 UI 實作者處理 depth 這個參數
        UIInput input = GUIStation.CreateUIInput(parent, "NormalInput", Vector3.zero, Color.white, showWord, depth,
            SpriteName.INPUT_NAME_BG,
            UIFontManager.GetUIDynamicFont(UIFontName.DragonWord),
            341, 75);

        return input;
    }

    public static void MainMenuButtons(GameObject parent, out UIButton character, out UIButton shop, out UIButton friend, out UIButton bag)
    {
        Transform pos = null;
        int margin = -576; //解析度 * 0.1 作為邊界
        int leftPadding = 360;
        int x = margin;
        int y = -435;
        float iconScale = 1.6f;

        character = GUIStation.CreateUIButton(parent, "Chatacter", new Vector3(x, y, 0), 10,
            SpriteName.ICON_PERSON,
            (int)(136 * iconScale), (int)(115 * iconScale),
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        //調整文字位置
        pos = character.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 40, pos.localPosition.z);
        character.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        bag = GUIStation.CreateUIButton(parent, "Bag", new Vector3(x, y, 0), 10, 
                                        SpriteName.ICON_BAG,
                                        (int)(135 * iconScale), (int)(122 * iconScale),
                                        UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                        Color.white, GLOBAL_STRING.BAG_BTN_TEXT);
        //調整文字位置
        pos = bag.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 50, pos.localPosition.z);
        bag.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        shop = GUIStation.CreateUIButton(parent, "Shop", new Vector3(x, y, 0), 10,
                                        SpriteName.ICON_STORE,
                                        (int)(133 * iconScale), (int)(115 * iconScale),
                                        UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                        Color.white, GLOBAL_STRING.SHOP_BTN_TEXT);
        //調整文字位置
        pos = shop.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 40, pos.localPosition.z);
        shop.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        friend = GUIStation.CreateUIButton(parent, "Friend", new Vector3(x, y, 0), 10,
                                            SpriteName.ICON_FRIEND,
                                            (int)(136 * iconScale), (int)(122 * iconScale),
                                            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                            Color.white, GLOBAL_STRING.FRIEND_BTN_TEXT);
        //調整文字位置
        pos = friend.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 50, pos.localPosition.z);
        friend.SetColor(Color.white, Color.black, Color.grey, Color.grey);
    }

    /// <summary>
    /// 遊戲主要底圖
    /// </summary>
    public static UISprite MainBackground(GameObject parent, int depth )
    {
        UISprite background = UIImageManager.CreateUISprite(parent, SpriteName.MAIN_BG);
        background.Init(UISprite.Type.Simple, depth, UIWidget.Pivot.Center, GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT);
        background.name = "Background";
        background.transform.localPosition = Vector3.zero; // 因為調整pivot會影響localPosition，所以需要再次重設
        return background;
    }

    /// <summary>
    /// 遊戲主要底圖 2
    /// </summary>
    public static UISprite WorldMapBackground(GameObject parent, int depth)
    {
        UISprite background = UIImageManager.CreateUISprite(parent, SpriteName.WORLDMAP_BG);
        background.Init(UISprite.Type.Simple, depth, UIWidget.Pivot.Center, GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT);
        background.name = "Background";
        background.transform.localPosition = Vector3.zero; // 因為調整pivot會影響localPosition，所以需要再次重設
        return background;
    }

    public static UISprite WorldMap(GameObject parent)
    {
        UISprite worldMap = UIImageManager.CreateUISprite(parent, SpriteName.WORLDMAP);
        worldMap.Init(UISprite.Type.Simple, 1, UIWidget.Pivot.Center, 1760, 838);
        worldMap.name = "WorldMap";
        worldMap.transform.localPosition = new Vector3(10, 35, 0); // 因為調整pivot會影響localPosition，所以需要再次重設

        return worldMap;
    }
}
