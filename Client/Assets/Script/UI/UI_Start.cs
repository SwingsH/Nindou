using UnityEngine;
using System.Collections;

/// <summary>
/// 開始畫面
/// </summary>
public class UI_Start : GUIFormBase 
{    
    public BtnClick InheritBtnClick;
    public BtnClick LoginBtnClick;
    
    UIButton _inheritBtn;
    UIButton _loginBtn;
    UILabel _loginHint; // 登入說明文字

    SubUI_LoadingBar _loadingBar; // 讀取進度條


    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        // 登入的全畫面圖按鈕
        _loginBtn = GUIStation.CreateUIButton(panel.gameObject, "Login BG", Vector3.zero, 0,
            NGUISpriteData.MAIN_BG,
            GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT, null, Color.white, string.Empty);
        _loginBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        // 登入文字
        _loginHint = GUIStation.CreateUILabel(panel.gameObject, "LoginHint", UIWidget.Pivot.Center, new Vector3(0, -60, 0), 7,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, fontSizeEnum: UIFontSize.VERY_LARGE, fontStyle: FontStyle.Bold),
            Color.white, GLOBAL_STRING.UI_START_HINT_1);
        // 加上event
        _loginBtn.onClick.Add(new EventDelegate(this, "LoginClick"));
        // 繼承按鈕
        _inheritBtn = GUIStation.CreateUIButton(panel.gameObject, "Inhert Button", new Vector3(294, -159, 0), 1,
            NGUISpriteData.BTN_INHERIT,
            40, 40, null, Color.white, string.Empty);
        _inheritBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        // 加上event
        _inheritBtn.onClick.Add(new EventDelegate(this, "InheritClick"));

        // 進度條
        _loadingBar = new SubUI_LoadingBar(panel.gameObject, "LoadingBar", new Vector3(-685, -167, 0), 3);
        _loadingBar.SetVisible(false);
    }
    #endregion

    #region 固定函式
    //void Start()
    //{
    //}

	// Update is called once per frame
    //void Update () 
    //{
    //}

    protected override void OnDestroy()
    {
        if (_loginBtn != null) { NGUITools.Destroy(_loginBtn.gameObject); }
        _loginBtn = null;
        if (_loginHint != null) { NGUITools.Destroy(_loginHint.gameObject); }
        _loginHint = null;

        if (_loadingBar != null) { _loadingBar.Dispose(); }
        _loadingBar = null;

        if (_inheritBtn != null) { NGUITools.Destroy(_inheritBtn.gameObject); }
        _inheritBtn = null;

        InheritBtnClick = null;
        LoginBtnClick = null;
        base.OnDestroy();
    }
    #endregion
    /// <summary>
    /// 是否正在顯示讀取條中
    /// </summary>
    public bool IsShowLoading
    {
        get { return _loadingBar.Visible; }
    }

    /// <summary>
    /// 設定讀取條的進度百分比
    /// </summary>
    public float ProgressPercent
    {
        set { _loadingBar.ProgressPercent = value; }
        get { return _loadingBar.ProgressPercent; }
    }

    /// <summary>
    /// 設定顯示進度與否
    /// 繼承按鈕在顯示進度條時會隱藏，隱藏進度條時會顯示
    /// 背景按鈕在顯示進度條時會無作用，隱藏進度條時才有作用
    /// </summary>
    public void SetProgressVisible(bool isVisible)
    {
        NGUITools.SetActive(_inheritBtn.gameObject, !isVisible);
        _loadingBar.SetVisible(isVisible);
        _loginBtn.isEnabled = !isVisible;
    }
    /// <summary>
    /// 按下登入按鈕（背景圖）的反應函式
    /// </summary>
    private void LoginClick()
    {
        if (LoginBtnClick != null) { LoginBtnClick(); }
    }
    /// <summary>
    /// 按下繼承按鈕的反應函式
    /// </summary>
    private void InheritClick()
    {
        CommonFunction.DebugMsg("Click Inherit Button");
        if (InheritBtnClick != null) { InheritBtnClick(); }
    }

    public void ShowNeedUpdateMode()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_2;
    }

    public void ShowUpdatingMode()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_3;
        if (!IsShowLoading && ProgressPercent <= 0)
        {
            ProgressPercent = 0.0f;
            SetProgressVisible(true);
        }
    }

    public void ShowReadyEnterGame()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_4;
    }
}
