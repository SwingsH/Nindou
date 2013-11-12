using UnityEngine;
using System.Collections;

public class UI_ShowLogo : GUIFormBase 
{
    const float WAIT_PRESS_TIME = 5.0f;
    const float MUST_WAIT_TO_TIME = 3.0f;
    UIButton _bg;
    UILabel _email;
    float _mustWaitToTime;
    float _changeStateTime;
    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        _bg = GUIStation.CreateUIButton(panel.gameObject, "LOGO", Vector3.zero, 0,
            NGUISpriteData.LOGO, GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT, null, Color.white, string.Empty);
        _bg.SetColor(Color.white, Color.white, Color.white, Color.white);
        _bg.onClick.Add(new EventDelegate(this, "HideLogo"));

        _email = GUIStation.CreateUILabel(new GORelativeInfo(panel.gameObject, new Vector3(-46,0,0),  "Email"),
            new UILabelInfo(UIFontName.MSJH, UIFontSize.UI_SHOW_EMAIL, FontStyle.Bold, GLOBAL_STRING.EMAIL));

        TweenColor ta = _email.gameObject.AddComponent<TweenColor>();
        ta.from = Color.white;
        ta.to = Color.yellow;
        ta.duration = 1.5f;
        ta.style = UITweener.Style.PingPong;
        AddShowOrHideFinishedDelegate(true, new EventDelegate(this, "SetWaitPressTime"), oneShot: false);
    }
    #endregion
    #region 固定函式
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.realtimeSinceStartup > _changeStateTime)
        {
            HideLogo();
        }
	}

    protected override void OnDestroy()
    {
        NGUITools.Destroy(_bg);
        _bg = null;
        base.OnDestroy();
    }
    #endregion

    void SetWaitPressTime()
    {
        _changeStateTime = Time.realtimeSinceStartup + WAIT_PRESS_TIME;
        _mustWaitToTime = Time.realtimeSinceStartup + MUST_WAIT_TO_TIME;
    }

    void HideLogo()
    {
        if (Time.realtimeSinceStartup > _mustWaitToTime)
        {
            _guistation.Control.ChangeGameState(GameStageSelect.Instance);
        }
    }
}
