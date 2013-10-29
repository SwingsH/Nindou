﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 主介面 - 世界地圖
/// </summary>
public class UI_Main_WorldMap : GUIFormBase
{
    const int POINT_BG_WIDTH = 418; // 點數底圖寬
    const int POINT_BG_HEIGHT = 84; // 點數底圖高
    const int POINT_PIC_WIDTH = 50; // 點數指示圖寬
    const int POINT_PIC_HEIGHT = 50; // 點數指示圖高
    const int MENU_BG_WIDTH = 100; // 「選單」背景圖寬
    const int MENU_BG_HEIGHT = 100; // 「選單」背景圖高
    const int STAGE_BG_WIDTH = 300; // 進入關卡的背景圖寬
    const int STAGE_BG_HEIGHT = 375; // 進入關卡的背景圖高

    public BtnClick stageClick;
    #region 每個主介面都會有的部分
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    #endregion
    #region 世界地圖部分
    private UISlider _stamina; // 「體力」的slider
    private UILabel _staminaText; // 「體力」的slider內的說明文字
    private UILabel _playerNameText; // 「玩家名稱」
    private UIButton _headPictureBtn; // 「人物頭像」按鈕
    private UILabel _pointText; // 「點數」說明文字
    private UIButton _menuBtn; // 「選單」按鈕
    private UILabel _warningText; // 「強敵發現！！」文字
    private UIButton _stageBtn; // 「關卡」按鈕
    #endregion

    private int _staPoint = 5; // 體力(應該要從帳號取得
    private int _staMaxPoint = 10; // 體力最大值(應該要從帳號取得
    private int _gamePoint = 0; // 遊戲點數(應該要從帳號取得

    #region 繼承自GUIFormBase的method
    protected override void  CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        #region 每個主介面都有的部分
        // 背景圖
        UISprite backgroundPic = GUIComponents.WorldMapBackground(panel.gameObject, 0);
        // 下方物件的parent & 對應的出現/隱藏Tween
        TweenPosition tweenPos = GUIComponents.AddShowMoveEffect(backgroundPic.gameObject, new Vector3(0, -206, 0), Vector3.zero);
        tweenPos.name = "bottomObjects";

        // 建立基本的四個按鈕：「人物」、「背包」、「商店」、「好友」
        GUIComponents.MainMenuButtons(tweenPos.gameObject, out _characterBtn, out _shopBtn, out _friendBtn, out _bagBtn);
        // 設定對應的EventDelegate
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        #endregion

        #region 世界地圖部分
        // 世界地圖的背景圖
        UISprite worldMap = GUIComponents.StageFrame(backgroundPic.gameObject);

        // 體力條
        _stamina = GUIStation.CreateUIProgressBar(worldMap.gameObject, "Stamina", new Vector3(-698, 329, 0), 8,
            SpriteName.HP_FG, SpriteName.ROLE_HP_BG, 438, 82);
        // 調整FG位置&slider全滿時大小
        _stamina.foreground.localPosition = new Vector3(29, 5, 0);
        _stamina.fullSize = new Vector2(270, 38);
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        // 體力條文字
        _staminaText = GUIStation.CreateUILabel(_stamina.gameObject, "StaminaText", UIWidget.Pivot.Left, new Vector3(32, 0, 0), 10,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, string.Format("目前體力：{0}/{1}", _staPoint, _staMaxPoint));
        // 玩家名稱
        _playerNameText = GUIStation.CreateUILabel(worldMap.gameObject, "PlayerNameText", UIWidget.Pivot.Left, new Vector3(-691, 422, 0), 10,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.black, string.Format(GLOBAL_STRING.UI_LABEL_PLAYER_NAME, _guistation.Account.PlayerName));

        // 「人物頭像」按鈕
        _headPictureBtn = GUIStation.CreateUIButton(worldMap.gameObject, "HeadPicture", new Vector3(0, 318, 0), 5,
            SpriteName.ROLE_ICON, 
            220, 219, null, Color.white, string.Empty);
        _headPictureBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _headPictureBtn.onClick.Add(new EventDelegate(this, "HeadPictureBtnClick"));
        // 「點數」
        UISprite pointBasePic = UIImageManager.CreateUISprite(worldMap.gameObject, SpriteName.BTN_GENERIC_BG);
        pointBasePic.Init(pointBasePic.type, 6, pointBasePic.pivot, POINT_BG_WIDTH, POINT_BG_HEIGHT);
        pointBasePic.name = "Point";
        pointBasePic.transform.localPosition = new Vector3(347, 328, 0);
        UISprite pointGraphPic = UIImageManager.CreateUISprite(pointBasePic.gameObject, SpriteName.POINT_PIC);
        pointGraphPic.Init(pointGraphPic.type, 7, pointGraphPic.pivot, POINT_PIC_WIDTH, POINT_PIC_HEIGHT);
        pointGraphPic.name = "PointGraph";
        pointGraphPic.transform.localPosition = new Vector3(-155, 0, 0);
        _pointText = GUIStation.CreateUILabel(pointBasePic.gameObject, "PointText", UIWidget.Pivot.Left, new Vector3(-107, -6, 0), 10,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.red, string.Format("目前點數：{0}", _gamePoint));

        _menuBtn = GUIStation.CreateUIButton(worldMap.gameObject, "Menu", new Vector3(669, 330, 0), 2,
            SpriteName.BTN_GENERIC_BG,
            MENU_BG_WIDTH, MENU_BG_HEIGHT,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.red, GLOBAL_STRING.MENU_BTN_TEXT);
        _menuBtn.SetColor(Color.white, Color.white, Color.grey, Color.grey);
        _menuBtn.onClick.Add(new EventDelegate(this, "MenuBtnClick"));
        // 「強敵發現！！」文字
        _warningText = GUIStation.CreateUILabel(worldMap.gameObject, "Warnging", UIWidget.Pivot.Center, new Vector3(-621, 195, 0), 11,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, fontStyle: FontStyle.Bold),
            Color.red, GLOBAL_STRING.WARNING_LABEL_TEXT);
        // 「關卡」按鈕
        _stageBtn = GUIStation.CreateUIButton(worldMap.gameObject, "Stage", new Vector3(-9, -97, 0), 4,
            SpriteName.MAIN_STAGE_BG,
            STAGE_BG_WIDTH, STAGE_BG_HEIGHT,
            null, Color.white, string.Empty);
        _stageBtn.SetColor(Color.white, Color.black, Color.grey, Color.grey);
        _stageBtn.onClick.Add(new EventDelegate(this, "StageBtnClick"));
        #endregion
    }
    #endregion
    #region 固定函式
    // Use this for initialization
    //void Start()
    //{
    //    //CreateAllComponent();
    //    foreach (UIButton btn in GetComponentsInChildren<UIButton>())
    //    {
    //        if (btn.name.Equals("Character")) { _characterBtn = btn; }
    //        if (btn.name.Equals("Bag")) { _bagBtn = btn; }
    //        if (btn.name.Equals("Shop")) { _shopBtn = btn; }
    //        if (btn.name.Equals("Friend")) { _friendBtn = btn; }
    //        if (btn.name.Equals("HeadPicture")) { _headPictureBtn = btn; }
    //        if (btn.name.Equals("Menu")) { _menuBtn = btn; }
    //        if (btn.name.Equals("Stage")) { _stageBtn = btn; }
    //    }

    //    foreach (UILabel label in GetComponentsInChildren<UILabel>())
    //    {
    //        if (label.name.Equals("StaminaText")) { _staminaText = label; }
    //        if (label.name.Equals("PointText")) { _pointText = label; }
    //        if (label.name.Equals("Warning")) { _warningText = label; }
    //    }
    //    foreach (UISlider slider in GetComponentsInChildren<UISlider>())
    //    {
    //        if (slider.name.Equals("Stamina")) { _stamina = slider; }
    //    }
    //    foreach (UIPlayTween playTween in GetComponentsInChildren<UIPlayTween>())
    //    {
    //        if (playTween.name.Equals("UI_Main_WorldMap")) { _uiPlayTween = playTween; }
    //    }
    //}
	
	// Update is called once per frame
	void Update () 
    {

    }

    protected override void OnDestroy()
    {
        if (_characterBtn != null) { NGUITools.Destroy(_characterBtn.gameObject); }
        _characterBtn = null;
        if (_bagBtn != null) { NGUITools.Destroy(_bagBtn.gameObject); }
        _bagBtn = null;
        if (_shopBtn != null) { NGUITools.Destroy(_shopBtn.gameObject); }
        _shopBtn = null;
        if (_friendBtn != null) { NGUITools.Destroy(_friendBtn.gameObject); }
        _friendBtn = null;
        if (_stamina != null) { NGUITools.Destroy(_stamina.gameObject); }
        _stamina = null;
        if (_staminaText != null) { NGUITools.Destroy(_staminaText.gameObject); }
        _staminaText = null;
        if (_headPictureBtn != null) { NGUITools.Destroy(_headPictureBtn.gameObject); }
        _headPictureBtn = null;
        if (_pointText != null) { NGUITools.Destroy(_pointText.gameObject); }
        _pointText = null;
        if (_menuBtn != null) { NGUITools.Destroy(_menuBtn.gameObject); }
        _menuBtn = null;
        if (_warningText != null) { NGUITools.Destroy(_warningText.gameObject); }
        _warningText = null;
        if (_stageBtn != null) { NGUITools.Destroy(_stageBtn.gameObject); }
        _stageBtn = null;
        base.OnDestroy();
    }
    #endregion

    /// <summary>
    /// 按下「人物」按鈕的反應函式
    /// </summary>
    void CharacterBtnClick()
    {
        CommonFunction.DebugMsg("按下 「人物」按鈕");
        // test : 隱藏警告提示
        NGUITools.SetActive(_warningText.gameObject, false);
        CommonFunction.DebugMsg("隱藏警告提示");
    }
    /// <summary>
    /// 按下「背包」按鈕的反應函式
    /// </summary>
    void BagBtnClick()
    {
        CommonFunction.DebugMsg("按下「背包」按鈕");
        // test : 顯示警告提示
        NGUITools.SetActive(_warningText.gameObject, true);
        CommonFunction.DebugMsg("顯示警告提示");
    }
    /// <summary>
    /// 按下「商店」按鈕的反應函式
    /// </summary>
    void ShopBtnClick()
    {
        CommonFunction.DebugMsg("按下「商店」按鈕");
        // test : 增加體力（超過最大值改成0）
        _staPoint = (_staPoint >= _staMaxPoint) ? 0 : _staPoint + 1;
        _staminaText.text = string.Format("目前體力：{0}/{1}", _staPoint, _staMaxPoint);
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        CommonFunction.DebugMsg("增加體力（超過最大值改成0）");
    }
    /// <summary>
    /// 按下「好友」按鈕的反應函式
    /// </summary>
    void FriendBtnClick()
    {
        CommonFunction.DebugMsg("按下「好友」按鈕");
        // test : 減少體力（低於0改成最大值）
        _staPoint = (_staPoint == 0) ? _staMaxPoint : _staPoint - 1;
        _staminaText.text = string.Format("目前體力：{0}/{1}", _staPoint,_staMaxPoint);
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        CommonFunction.DebugMsg("減少體力（低於0改成最大值）");
    }

    /// <summary>
    /// 按下「頭像」按鈕的反應函式
    /// </summary>
    void HeadPictureBtnClick()
    {
        CommonFunction.DebugMsg("按下「頭像」按鈕");
        // test : 減少遊戲點數
        --_gamePoint;
        _pointText.text = string.Format("目前點數：{0}", _gamePoint);
        CommonFunction.DebugMsg("減少遊戲點數");
    }

    /// <summary>
    /// 按下「選單」按鈕的反應函式
    /// </summary>
    void MenuBtnClick()
    {
        CommonFunction.DebugMsg("按下「選單」按鈕");
        // test : 增加遊戲點數
        //++_gamePoint;
        //_pointText.text = string.Format("目前點數：{0}", _gamePoint);
        //CommonFunction.DebugMsg("增加遊戲點數");
        _uiPlayTween.Play(false);
    }

    /// <summary>
    /// 按下「關卡」按鈕的反應函式
    /// </summary>
    void StageBtnClick()
    {
        CommonFunction.DebugMsg("按下「關卡」按鈕");
        //_guistation.ShowAndHideOther(typeof(UI_Main_StageSelect));
        GameControl.Instance.ChangeGameState(GameStageSelect.Instance);
    }
}
