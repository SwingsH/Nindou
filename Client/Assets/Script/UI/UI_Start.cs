using UnityEngine;
using System.Collections;

/// <summary>
/// 開始畫面
/// </summary>
public class UI_Start : GUIFormBase 
{    
    public BtnClick InheritBtnClick;
    public BtnClick LoginBtnClick;
    
    UISlider _progress;
    UIButton _inheritBtn;
    UIButton _loginBtn;
    UILabel _loginHint; // 登入說明文字
    UILabel _progressShow;

    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        // 登入的全畫面圖按鈕
        _loginBtn = GUIStation.CreateUIButton(panel.gameObject, "Login BG", Vector3.zero, 0,
            ResourceStation.GetUIAtlas("Atlas_Backgrounds"),
            "temp_nindou_bg", GUIStation.ScreenWidth, GUIStation.ScreenHeight, null, Color.white, string.Empty);
        _loginBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        // 登入文字
        _loginHint = GUIStation.CreateUILabel(panel.gameObject, "LoginHint", UIWidget.Pivot.Center, new Vector3(0, -106, 0), 7,
            ResourceStation.GetUIFont("MSJH_30"),
            new Color(1.0f, 0.2f, 0.3f), GLOBAL_STRING.UI_START_HINT_1);
        // 加上event
        _loginBtn.onClick.Add(new EventDelegate(this, "LoginClick"));
        // 繼承按鈕
        _inheritBtn = GUIStation.CreateUIButton(panel.gameObject, "Inhert Button", new Vector3(294, -159, 0), 1, 
            ResourceStation.GetUIAtlas("TestAtlas"),
            "fb_300_main", 40, 40, null, Color.white, string.Empty);
        _inheritBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        // 加上event
        _inheritBtn.onClick.Add(new EventDelegate(this, "InheritClick"));

        // 進度條
        _progress = GUIStation.CreateUIProgressBar(panel.gameObject, "Progress Bar_Build", new Vector3(-346, -167, 0), 2,
            ResourceStation.GetUIAtlas("SciFi Atlas"),
            "Dark", "Light", 690, 30);
        NGUITools.SetActive(_progress.gameObject, false);
        // 進度數值
        _progressShow = GUIStation.CreateUILabel(_progress.gameObject, "Progress", UIWidget.Pivot.Right, new Vector3(652.67f, -4.0f, 0), 6,
            ResourceStation.GetUIFont("MSJH_30"),
            new Color(1.0f, 0.2f, 0.3f), "0.00");
        _progressShow.overflowMethod = UILabel.Overflow.ResizeFreely; // fs: 讓文字佔的空間自由地重新配置
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
        NGUITools.Destroy(_loginBtn.gameObject);
        _loginBtn = null;
        NGUITools.Destroy(_loginHint.gameObject);
        _loginHint = null;
        NGUITools.Destroy(_progressShow.gameObject);
        _progressShow = null;
        NGUITools.Destroy(_progress.gameObject);
        _progress = null;
        NGUITools.Destroy(_inheritBtn.gameObject);
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
        get { return NGUITools.GetActive(_progress.gameObject); }
    }

    /// <summary>
    /// 設定讀取條的進度百分比
    /// </summary>
    public float progressPercent
    {
        set
        {
            _progress.value = value / 100.0f;
            _progressShow.text = string.Format("{0:0.00}%", value);
        }
        get
        {
            return _progress.value * 100.0f;
        }
    }

    /// <summary>
    /// 設定顯示進度與否
    /// 繼承按鈕在顯示進度條時會隱藏，隱藏進度條時會顯示
    /// 背景按鈕在顯示進度條時會無作用，隱藏進度條時才有作用
    /// </summary>
    public void SetProgressVisible(bool isVisible)
    {
        NGUITools.SetActive(_inheritBtn.gameObject, !isVisible);
        NGUITools.SetActive(_progress.gameObject, isVisible);
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
        //if (progress_test != null)
        //{
        //    NGUITools.Destroy(progress_test.gameObject);
        //    //Destroy(progress_test.gameObject);
        //    progress_test = null;
        //}
        //else
        //{
        //    progress_test = CommonFunction.CreateProgressBar(gameObject, "Progress Bar", new Vector3(-346, -167, 0), 2,
        //    (Resources.Load("TestUI/SciFi Atlas", typeof(UIAtlas)) as UIAtlas),
        //    "Dark", "Light", 690, 30);
        //}
    }

    public void ShowNeedUpdateMode()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_2;
    }

    public void ShowUpdatingMode()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_3;
        if (!IsShowLoading && progressPercent <= 0)
        {
            CommonFunction.DebugMsg("test----");
            progressPercent = 0.0f;
            SetProgressVisible(true);
        }
    }

    public void ShowReadyEnterGame()
    {
        _loginHint.text = GLOBAL_STRING.UI_START_HINT_4;
    }
}
