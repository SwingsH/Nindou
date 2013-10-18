using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


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
    private GameControl _gameControl = null;
    private UIRoot _uiRoot = null;

    #region UI攝影機相關
    private Camera _camera = null;  // 顯示UI用的Camera
    private UICamera _uiCamera = null; // 為了事件處理使用

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
    #endregion

    private Dictionary<Type, GUIFormBase> _guiReference; // GUIForm參照儲存
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="control"></param>
    public GUIStation(GameControl control)
    {
        _gameControl = control;
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
            _uiRoot.manualHeight = 1080; // TODO:先設死，之後測試用 Screen.height;
        }

        _uiRoot.gameObject.layer = GLOBALCONST.LAYER_UI_BASE;
        // 如果場景上有兩個以上的UICamera會管理不到，請將多餘的刪除
        _uiCamera = GameObject.FindObjectOfType(typeof(UICamera)) as UICamera;
        // 取得UICamera attach上的物件的camera
        //Camera cam;
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
        // 隱藏所有UI，若確定一次只開一個，就只需關前一個
        foreach (GUIFormBase ui in _guiReference.Values)
        {
            if (ui.Visible) { ui.Hide(); }
        }

        GUIFormBase showUI;
        if (!_guiReference.TryGetValue(uiType, out showUI))
        {
            showUI = InstantiateContainer(uiType);
            showUI.CreateUI(this);
        }
        showUI.Show();
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
        retSprite.name = spriteObjName;
        retSprite.type = spriteType;
        retSprite.depth = depth;
        retSprite.atlas = atlas;
        retSprite.spriteName = spriteName;
        retSprite.pivot = pivot;
        retSprite.transform.localPosition = Vector3.zero;
        retSprite.MakePixelPerfect(); // 如果type = simple or filled 會改回近原大小，故在呼叫此函式和才修改寬高      
        retSprite.width = width;
        retSprite.height = height;

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
            Texture uiImage = Resources.Load("NGUI/" + textureName, typeof(Texture)) as Texture;

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
    /// <param name="atlas">使用的Atlas</param>
    /// <param name="spriteName">使用的Sprite名稱</param>
    /// <param name="width">按鈕寬度</param>
    /// <param name="height">按鈕高度</param>
    /// <param name="btnLabelText">按鈕標籤文字(沒有傳入字體時無作用)</param>
    /// <param name="btnLabelColor">按鈕標籤顏色(沒有傳入字體時無作用)</param>
    /// <returns>建出的UIButton</returns>
    public static UIButton CreateUIButton(GameObject parentObj, string btnName, Vector3 relativePos, int depth, UIAtlas atlas, string spriteName,
        int width, int height, UIFont font, Color btnLabelColor, string btnLabelText)
    {
        GameObject retButtonObj = NGUITools.AddChild(parentObj);
        retButtonObj.name = btnName;
        retButtonObj.transform.localPosition = relativePos;
        // 設定按鈕背景圖
        UISprite bg = CreateUISprite(retButtonObj, "Background", UISprite.Type.Sliced, depth, atlas, spriteName, UIWidget.Pivot.Center, width, height);
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
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="progressBarName">Progress Bar名稱</param>
    /// <param name="relativePos">Progress Bar位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="atlas">使用的Atlas</param>
    /// <param name="backgroundName">background使用的Sprite名稱</param>
    /// <param name="foregroundName">foreground使用的Sprite名稱</param>
    /// <param name="width">Progress Bar寬度</param>
    /// <param name="height">Progress Bar高度</param>
    /// <returns>建出的 ProgressBar</returns>
    public static UISlider CreateUIProgressBar(GameObject parentObj, string progressBarName, Vector3 relativePos, int depth,
        UIAtlas atlas, string backgroundName, string foregroundName, int width, int height)
    {
        GameObject progressBarObject = NGUITools.AddChild(parentObj);
        progressBarObject.name = progressBarName;
        progressBarObject.transform.localPosition = relativePos;
        // Background sprite
        UISpriteData bgs = atlas.GetSprite(backgroundName);

        UISprite back = CreateUISprite(progressBarObject, "Background", (bgs.hasBorder ? UISprite.Type.Sliced : UISprite.Type.Simple), depth, atlas, backgroundName,
            UIWidget.Pivot.Left, width, height);
        // Foreground sprite
        UISpriteData fgs = atlas.GetSprite(foregroundName);

        UISprite front = CreateUISprite(progressBarObject, "Foreground", (fgs.hasBorder ? UISprite.Type.Sliced : UISprite.Type.Simple), depth + 1, atlas, foregroundName,
            UIWidget.Pivot.Left, width, height);
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
        UIFont font, Color labelColor, string labelText)
    {
        UILabel retLabel = NGUITools.AddWidget<UILabel>(parentObj);
        retLabel.pivot = pivot;
        retLabel.transform.localPosition = relativePos;
        retLabel.name = labelName;
        retLabel.depth = depth;
        retLabel.font = font;
        retLabel.text = labelText;
        retLabel.color = labelColor;
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
        /*
         * int depth = NGUITools.CalculateNextDepth(go);
			go = NGUITools.AddChild(go);
			go.name = "Scroll Bar";

			UISprite bg = NGUITools.AddWidget<UISprite>(go);
			bg.type = UISprite.Type.Sliced;
			bg.name = "Background";
			bg.depth = depth;
			bg.atlas = NGUISettings.atlas;
			bg.spriteName = mScrollBG;

			Vector4 border = bg.border;
			bg.width = Mathf.RoundToInt(400f + border.x + border.z);
			bg.height = Mathf.RoundToInt(14f + border.y + border.w);
			bg.MakePixelPerfect();

			UISprite fg = NGUITools.AddWidget<UISprite>(go);
			fg.type = UISprite.Type.Sliced;
			fg.name = "Foreground";
			fg.atlas = NGUISettings.atlas;
			fg.spriteName = mScrollFG;

			UIScrollBar sb = go.AddComponent<UIScrollBar>();
			sb.background = bg;
			sb.foreground = fg;
			sb.direction = mScrollDir;
			sb.barSize = 0.3f;
			sb.value = 0.3f;
			sb.ForceUpdate();

			if (mScrollCL)
			{
				NGUITools.AddWidgetCollider(bg.gameObject);
				NGUITools.AddWidgetCollider(fg.gameObject);
			}
        */
        return retSC;
    }


    #endregion
}

public static class GUIFunction
{
    /// <summary>
    /// 設定UIButton的各種情況顏色
    /// </summary>
    /// <param name="btn">要設定的UIButton</param>
    /// <param name="normalColor">沒任何事件時的顏色</param>
    /// <param name="disableColor">Disable的顏色</param>
    /// <param name="pressedColor">按下時的顏色</param>
    /// <param name="hoverColor">滑鼠經過時的顏色</param>
    public static void SetColor(this UIButton btn, Color normalColor, Color disableColor, Color pressedColor, Color hoverColor)
    {
        btn.defaultColor = normalColor;
        btn.UpdateColor(true, true);
        btn.disabledColor = disableColor;
        btn.pressed = pressedColor;
        btn.hover = hoverColor;
    }
}
