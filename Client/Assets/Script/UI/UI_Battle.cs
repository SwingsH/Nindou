using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Battle : GUIFormBase
{
    const int BOSS_ICON_WIDTH = 138;
    const int BOSS_ICON_HEIGHT = 123;
    const int ROLE_ICON_PLATE_BG_WIDTH = GUIStation.MANUAL_SCREEN_WIDTH;
    const int ROLE_ICON_PLATE_BG_HEIGHT = 248;
    const int ROLE_ICON_WIDTH = 408;
    const int ROLE_ICON_HEIGHT = 331;

    UISprite _bossPic; // Boss示意圖
    UILabel _bossNameText; // Boss 名稱
    UISlider _bossHPBar; // Boss HP 血條
    UIButton _fastForwardBtn; // 加速鈕
    UIButton _pauseBtn; // 暫停鈕
    UISprite _iconBackground; // 角色icon所在的背景圖
    List<UIButton> _iconBtns = new List<UIButton>(); // 玩家角色圖像
    List<UISlider> _hpBars = new List<UISlider>(); // 玩家角色血條
    List<UILabel> _roleNames = new List<UILabel>(); // 玩家角色名字

     #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);
        // BOSS 圖片
        _bossPic = UIImageManager.CreateUISprite(panel.gameObject, SpriteName.BOSS_PIC);
        //// TODO:sprite設定，之後統整出去---
        _bossPic.name = "Boss Pic";
        _bossPic.transform.localPosition = new Vector3(-400, 440, 0);
        _bossPic.depth = 3;
        _bossPic.width = BOSS_ICON_WIDTH;
        _bossPic.height = BOSS_ICON_HEIGHT;
        ////---------------------------------
        // BOSS 名稱
        _bossNameText = GUIStation.CreateUILabel(_bossPic.gameObject, "Boss Name", UIWidget.Pivot.Left, new Vector3(73, -8, 0), 4,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_BOSS_NAME, FontStyle.Bold),
            Color.white, "Boss名稱哈哈琳");
        // Boss HP 血條
        _bossHPBar = GUIStation.CreateUIProgressBar(_bossPic.gameObject, "Boss HP Bar", new Vector3(0, -25, 0), 1,
            SpriteName.HP_FG, SpriteName.BOSS_HP_BG, 999, 72);
        // 調整FG位置&slider全滿時大小
        _bossHPBar.foreground.localPosition = new Vector3(32, 1, 0);
        _bossHPBar.fullSize = new Vector2(826, 28);
        // 加速鈕
        _fastForwardBtn = GUIStation.CreateUIButton(panel.gameObject, "FastForward", new Vector3(714, 428, 0), 6,
            SpriteName.FAST_FORWARD_BTN, 150, 150, null, Color.white, string.Empty);
        _fastForwardBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _fastForwardBtn.onClick.Add(new EventDelegate(this, "FastForwardBtnClick"));
        // 暫停鈕
        _pauseBtn = GUIStation.CreateUIButton(panel.gameObject, "Pause", new Vector3(884, 428, 0), 5,
            SpriteName.PAUSE, 150, 150, null, Color.white, string.Empty);
        _pauseBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _pauseBtn.onClick.Add(new EventDelegate(this, "PauseBtnClick"));
        // 放置角色Icon的背景圖版
        _iconBackground = UIImageManager.CreateUISprite(panel.gameObject, SpriteName.ROLE_ICON_PLATE);
        // TODO:sprite設定，之後統整出去---
        _iconBackground.name = "IconBackground";
        _iconBackground.transform.localPosition = new Vector3(0, -398, 0);
        _iconBackground.depth = 0;
        _iconBackground.width = ROLE_ICON_PLATE_BG_WIDTH;
        _iconBackground.height = ROLE_ICON_PLATE_BG_HEIGHT;
        // --------------------------------
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
        foreach (UILabel roleName in _roleNames)
        {
            if (roleName != null) { NGUITools.Destroy(roleName.gameObject); }
        }
        _roleNames = null;
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
        if (_bossNameText != null) { NGUITools.Destroy(_bossNameText.gameObject); }
        _bossNameText = null;
        if (_bossPic != null) { NGUITools.Destroy(_bossPic.gameObject); }
        _bossPic = null;
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
        NGUITools.SetActive(_bossPic.gameObject, isVisible);
    }

    // BossName
    public string BossName
    {
        set { if (value != null) { _bossNameText.text = value; } }
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
        // 角色按鈕兼底圖
        UIButton tempIconBtn = GUIStation.CreateUIButton(parentObj, string.Format("Icon_{0}", playerIndex), new Vector3(-656 + 432 * playerIndex, 12, 0), 3,
            SpriteName.ROLE_ICON,  ROLE_ICON_WIDTH, ROLE_ICON_HEIGHT, null, Color.white, string.Empty);
        tempIconBtn.tweenTarget.name = "Role BG";
        tempIconBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        tempIconBtn.onClick.Add(new EventDelegate(this, "IconBtnClick"));
        _iconBtns.Add(tempIconBtn);
        // 角色血條
        UISlider tempHPBar = GUIStation.CreateUIProgressBar(tempIconBtn.gameObject, "Role HP Bar", Vector3.zero, 2, 
            SpriteName.HP_FG, SpriteName.NONE, 4, 4);
        // 調整位置& Slider全滿時大小
        tempHPBar.foreground.localPosition = new Vector3(-106, -104, 0);
        tempHPBar.fullSize = new Vector2(205, 28);
        _hpBars.Add(tempHPBar);
        // 角色名字
        UILabel tempRoleName = GUIStation.CreateUILabel(tempIconBtn.gameObject, "Role Name", UIWidget.Pivot.Center, new Vector3(74, -63, 0), 4,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_ROLE_NAME, FontStyle.Bold),
            Color.red, "玩家一二三四");
        _roleNames.Add(tempRoleName);
    }
    #endregion
}
