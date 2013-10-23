using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Battle : GUIFormBase
{
    UILabel _boasNameText; // Boss 名稱
    UISlider _bossHPBar; // Boss HP 血條
    UIButton _fastForwardBtn; // 加速鈕
    UIButton _pauseBtn; // 暫停鈕
    UISprite _iconBackground; // 角色icon所在的背景圖
    List<UIButton> _iconBtns = new List<UIButton>(); // 玩家角色圖像
    List<UISlider> _hpBars = new List<UISlider>(); // 玩家角色血條
    

     #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);
        // boss 圖片


        // BOSS 名稱
        _boasNameText = GUIStation.CreateUILabel(panel.gameObject, "BossName", UIWidget.Pivot.Left, new Vector3(-858, 460, 0), 4,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, fontStyle:FontStyle.Bold),
            Color.red, "Boss Name");
        // Boss HP 血條
        _bossHPBar = GUIStation.CreateUIProgressBar(panel.gameObject, "Boss HP Bar", new Vector3(-400, 440, 0), 1,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST),
            //"button_back", "button_back", 
            GLOBALCONST.SPRITE_TEST_BUTTON_BACK, GLOBALCONST.SPRITE_TEST_BUTTON_BACK,
            835, 122);
        // 此處暫時作法，一般來說前景和背景圖會是不同的，且不需特別變色才是
        UISprite[] tempSprites = _bossHPBar.gameObject.GetComponentsInChildren<UISprite>();
        foreach (UISprite oneSprite in tempSprites)
        {
            if (oneSprite.name.Equals("Foreground")) { oneSprite.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 39.0f / 255.0f); }
            if (oneSprite.name.Equals("Background")) { oneSprite.color = new Color(255.0f / 255.0f, 4.0f / 255.0f, 4.0f / 255.0f); }
        }
        // 加速鈕
        _fastForwardBtn = GUIStation.CreateUIButton(panel.gameObject, "FastForward", new Vector3(714, 428, 0), 6,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST),
            //"Fast-forward", 
            GLOBALCONST.SPRITE_TEST_FAST_FORWARD,
            150, 150, null, Color.white, string.Empty);
        _fastForwardBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _fastForwardBtn.onClick.Add(new EventDelegate(this, "FastForwardBtnClick"));
        // 暫停鈕
        _pauseBtn = GUIStation.CreateUIButton(panel.gameObject, "Pause", new Vector3(884, 428, 0), 5,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST),
            //"pause", 
            GLOBALCONST.SPRITE_TEST_PAUSE,
            150, 150, null, Color.white, string.Empty);
        _pauseBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _pauseBtn.onClick.Add(new EventDelegate(this, "PauseBtnClick"));
        _iconBackground = GUIStation.CreateUISprite(panel.gameObject, "IconBackground", UISprite.Type.Simple, 0,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST),
            //"pachuri", 
            GLOBALCONST.SPRITE_TEST_BATTLE_ICON_BG,
            UIWidget.Pivot.Center, GUIStation.MANUAL_SCREEN_WIDTH, 248);
        _iconBackground.transform.localPosition = new Vector3(0, -416, 0);
        // 玩家角色圖像 & 血條
        for (int i = 0; i < GLOBALCONST.UI_BATTLE_ROLE_ICON_COUNT; ++i)
        {
            AddPlayerIcon(_iconBackground.gameObject);
        }
    }
     #endregion
    #region 固定函式
    // Use this for initialization
	void Start () 
    {
        //CreateAllComponent();
	}

    // Update is called once per frame
    void Update() 
    {

    }

    protected override void OnDestroy()
    {
        foreach (UIButton iconBtn in _iconBtns)
        {
            if (iconBtn != null) { NGUITools.Destroy(iconBtn.gameObject); }
        }
        _iconBtns = null;
        foreach (UISlider hpBar in _hpBars)
        {
            if (hpBar != null) { NGUITools.Destroy(hpBar.gameObject); }
        }
        _hpBars = null;
        if (_pauseBtn != null) { NGUITools.Destroy(_pauseBtn.gameObject); }
        _pauseBtn = null;
        if (_fastForwardBtn != null) { NGUITools.Destroy(_fastForwardBtn.gameObject); }
        _fastForwardBtn = null;
        if (_bossHPBar != null) { NGUITools.Destroy(_bossHPBar.gameObject); }
        _bossHPBar = null;
        if (_boasNameText != null) { NGUITools.Destroy(_boasNameText.gameObject); }
        _boasNameText = null;
        base.OnDestroy();
    }
    #endregion

    #region 按下按鈕的反應函式
    /// <summary>
    /// 按下加速鈕的反應函式
    /// </summary>
    void FastForwardBtnClick()
    {
        CommonFunction.DebugMsg("按下「加速鈕」");
        // TODO : 看是否真是這樣
        TimeMachine.SetTimeScale(Time.timeScale == 1 ? 5 : (Time.timeScale == 5 ? 10 : 1));
    }

    /// <summary>
    /// 按下暫停鈕的反應函式
    /// </summary>
    void PauseBtnClick()
    {
        CommonFunction.DebugMsg("按下「暫停鈕」");
        // TODO：
        TimeMachine.SetTimeScale(0.0f);
    }
    /// <summary>
    /// 按下角色Icon的反應函式
    /// </summary>
    void IconBtnClick()
    {
        CommonFunction.DebugMsgFormat("按下 {0}", UIButton.current.name);
        int iconSelect = -1;
        string indexStr = UIButton.current.name.Substring(5);
        CommonFunction.DebugMsgFormat("按下的indexStr = =={0}==", indexStr);
        if (!int.TryParse(indexStr, out iconSelect))
        {
            CommonFunction.DebugMsgFormat("現在選擇的icon為：{0}，無法解析index，請重新設定", UIButton.current.name);
            return;
        }
        if (BattleManager.Instance.Players == null)
        {
            CommonFunction.DebugMsg("戰鬥資料沒有玩家！！");
            return;
        }
        if (iconSelect < 0 || iconSelect >= BattleManager.Instance.Players.Length)
        {
            CommonFunction.DebugMsgFormat("現在按下的icon為：{0}，index有誤，請檢查", UIButton.current.name);
        }
        // 施展技能
        ActionUnit currentRole = BattleManager.Instance.Players[iconSelect] as ActionUnit;
        if (currentRole != null) { currentRole.CastExtrimSkill(); }
    }
    #endregion
    #region Boss資訊相關
    /// <summary>
    /// 設定Boss訊息是否顯示
    /// </summary>
    /// <param name="isVisible">是否顯示</param>
    public void SetBossMessageVisible(bool isVisible)
    {
        NGUITools.SetActive(_boasNameText.gameObject, isVisible);
        NGUITools.SetActive(_bossHPBar.gameObject, isVisible);
    }

    // BossName
    public string BossName
    {
        set { if (value != null) { _boasNameText.text = value; } }
    }

    /// <summary>
    /// 設定Boss血條
    /// </summary>
    /// <param name="newBossCurHP">Boss現在HP</param>
    /// <param name="newBossMaxHP">Boss最大HP</param>
    public void SetBossHP(int newBossCurHP, int newBossMaxHP)
    {
        if (newBossMaxHP < 0) { return; }
        int curHP = Mathf.Clamp(newBossCurHP, 0, newBossMaxHP);
        _bossHPBar.value = (float)curHP / (float)newBossMaxHP;
    }
    #endregion
    #region 玩家圖像相關

    /// <summary>
    /// 設定PlayerIcon資料
    /// </summary>
    /// <param name="playerIndex">Player Index</param>
    /// <param name="isVisible">是否看的到</param>
    public void SetPlayerIcon(int playerIndex, bool isVisible, float life = 0, uint maxLife = 1)
    {
        // 設定該格資料
        NGUITools.SetActive(_iconBtns[playerIndex].gameObject, isVisible);
        if (isVisible) { _hpBars[playerIndex].value = (float)life / (float)maxLife; }
    }

    /// <summary>
    /// 加一個playerIcon
    /// </summary>
    /// <param name="parentObj">父物件</param>
    void AddPlayerIcon(GameObject parentObj)
    {
        int playerIndex = _iconBtns.Count;

        UIButton tempIconBtn = GUIStation.CreateUIButton(parentObj.gameObject, string.Format("Icon_{0}", playerIndex), new Vector3(-656 + 432 * playerIndex, 12, 0), 3,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST2),
            //"chiruno", 
            GLOBALCONST.SPRITE_TEST_PLAYER_ICON,
            180, 180, null, Color.white, string.Empty);
        tempIconBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        tempIconBtn.onClick.Add(new EventDelegate(this, "IconBtnClick"));
        _iconBtns.Add(tempIconBtn);
        UISlider tempHPBar = GUIStation.CreateUIProgressBar(tempIconBtn.gameObject, "HP Bar", new Vector3(-178, -64, 0), 1,
            ResourceStation.GetUIAtlas(GLOBALCONST.ATLAS_TEST),
            //"button_back", "button_back", 
            GLOBALCONST.SPRITE_TEST_BUTTON_BACK, GLOBALCONST.SPRITE_TEST_BUTTON_BACK,
            350, 122);
        // TODO: 此處暫時作法，一般來說前景和背景圖會是不同的，且不需特別變色才是
        UISprite[] tempHPBarSprites = tempHPBar.gameObject.GetComponentsInChildren<UISprite>();
        foreach (UISprite oneSprite in tempHPBarSprites)
        {
            if (oneSprite.name.Equals("Foreground")) { oneSprite.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 39.0f / 255.0f); }
            if (oneSprite.name.Equals("Background")) { oneSprite.color = new Color(255.0f / 255.0f, 4.0f / 255.0f, 4.0f / 255.0f); }
        }
        _hpBars.Add(tempHPBar);
    }
    #endregion
}
