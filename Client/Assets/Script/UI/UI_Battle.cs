using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Battle : GUIFormBase
{
    const int BOSS_ICON_WIDTH = 138;
    const int BOSS_ICON_HEIGHT = 123;
    const int ROLE_ICON_PLATE_BG_WIDTH = GUIStation.MANUAL_SCREEN_WIDTH;
    const int ROLE_ICON_PLATE_BG_HEIGHT = 248;

    readonly Color START_TEXT_COLOR = CommonFunction.Color256Bit(255, 226, 0, 255);

    UILabel _readyText; // 準備文字
    UILabel _goText; // 開始文字

    UISprite _bossPic; // Boss示意圖
    UILabel _bossNameText; // Boss 名稱
    UISlider _bossHPBar; // Boss HP 血條
    UIButton _fastForwardBtn; // 加速鈕
    UIButton _pauseBtn; // 暫停鈕
    UISprite _iconBackground; // 角色icon所在的背景圖

    List<SubUI_RoleIcon> _playerRoleIcons = new List<SubUI_RoleIcon>(); // 玩家角色Icon

    Dictionary<string, SubUI_HPBar> _enemyInfos = new Dictionary<string, SubUI_HPBar>();

    bool isPause = false;

     #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);
        // "Ready..." 文字
        _readyText = GUIStation.CreateUILabel(new GORelativeInfo(panel.gameObject, "ReadyText"),
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_START, FontStyle.Bold),
            START_TEXT_COLOR, GLOBAL_STRING.READY_TEXT,
            0, UIWidget.Pivot.Center);
        _readyText.effectDistance = new Vector2(5, 5);
        _readyText.transform.localScale = Vector3.forward; // 先隱藏
        // "Ready..." 一開始的放大效果
        TweenScale readyScale = _readyText.gameObject.AddComponent<TweenScale>();
        readyScale.from = Vector3.forward;
        readyScale.to = Vector3.one;
        readyScale.duration = 0.5f;
        readyScale.tweenGroup = GLOBALCONST.UI_Battle_Start_TweenGroup;
        // "Ready..." 接著的移動效果
        TweenPosition readyPos = _readyText.gameObject.AddComponent<TweenPosition>();
        readyPos.from = Vector3.zero;
        readyPos.to = new Vector3(-1186, 0, 0);
        readyPos.duration = 0.5f;
        readyPos.delay = 0.6f;
        readyPos.tweenGroup = GLOBALCONST.UI_Battle_Start_TweenGroup;
        // "GO!!!!" 文字
        _goText = GUIStation.CreateUILabel(new GORelativeInfo(panel.gameObject, "GoText"),
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_START, FontStyle.Bold),
            START_TEXT_COLOR, GLOBAL_STRING.GO_TEXT,
            0, UIWidget.Pivot.Center);
        _goText.effectDistance = new Vector2(5, 5);
        _goText.transform.localScale = Vector3.forward; // 先隱藏
        // "GO!!!!" 一開始的放大效果
        TweenScale goScale = _goText.gameObject.AddComponent<TweenScale>();
        goScale.from = Vector3.forward;
        goScale.to = Vector3.one;
        goScale.duration = 0.5f;
        goScale.delay = 0.7f;
        goScale.tweenGroup = GLOBALCONST.UI_Battle_Start_TweenGroup;
        // "GO!!!!" 接著的移動效果
        TweenPosition goPos = _goText.gameObject.AddComponent<TweenPosition>();
        goPos.from = Vector3.zero;
        goPos.to = new Vector3(-1186, 0, 0);
        goPos.duration = 0.5f;
        goPos.delay = 1.3f;
        goPos.tweenGroup = GLOBALCONST.UI_Battle_Start_TweenGroup;

        // BOSS 圖片
        _bossPic = UIImageManager.CreateUISprite(new GORelativeInfo(panel.gameObject, new Vector3(-400, 440, 0), "Boss Pic"),
            new UISpriteInfo(NGUISpriteData.BOSS_PIC, BOSS_ICON_WIDTH, BOSS_ICON_HEIGHT, 3));
        // BOSS 名稱
        _bossNameText = GUIStation.CreateUILabel(_bossPic.gameObject, "Boss Name", UIWidget.Pivot.Left, new Vector3(73, -8, 0), 4,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_BOSS_NAME, FontStyle.Bold),
            Color.white, "Boss名稱哈哈琳");
        // Boss HP 血條
        _bossHPBar = GUIStation.CreateUIProgressBar(_bossPic.gameObject, "Boss HP Bar", new Vector3(0, -25, 0), 1,
            NGUISpriteData.HP_FG, NGUISpriteData.BOSS_HP_BG, 999, 72);
        // 調整FG位置&slider全滿時大小
        _bossHPBar.foreground.localPosition = new Vector3(32, 1, 0);
        _bossHPBar.fullSize = new Vector2(826, 28);
        // 加速鈕
        _fastForwardBtn = GUIStation.CreateUIButton(panel.gameObject, "FastForward", new Vector3(714, 428, 0), 6,
            NGUISpriteData.BTN_FAST_FORWARD, 150, 150, null, Color.white, string.Empty);
        _fastForwardBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _fastForwardBtn.onClick.Add(new EventDelegate(this, "FastForwardBtnClick"));
        // 暫停鈕
        _pauseBtn = GUIStation.CreateUIButton(panel.gameObject, "Pause", new Vector3(884, 428, 0), 5,
            NGUISpriteData.ICON_PAUSE, 150, 150, null, Color.white, string.Empty);
        _pauseBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _pauseBtn.onClick.Add(new EventDelegate(this, "PauseBtnClick"));
        // 放置角色Icon的背景圖版
        _iconBackground = UIImageManager.CreateUISprite(new GORelativeInfo(panel.gameObject, new Vector3(0, -398, 0), "IconBackground"),
            new UISpriteInfo(NGUISpriteData.ROLE_ICON_PLATE, ROLE_ICON_PLATE_BG_WIDTH, ROLE_ICON_PLATE_BG_HEIGHT, 0));
        // 玩家角色圖像 & 血條
        for (int i = 0; i < GLOBALCONST.MAX_BATTLE_ROLE_COUNT; ++i)
        {
            AddPlayerIcon(_iconBackground.gameObject);
        }

        AddShowOrHideFinishedDelegate(true, new EventDelegate(this, "ShowStart"), oneShot:false);
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
        NGUITools.Destroy(_readyText);
        _readyText = null;
        NGUITools.Destroy(_goText);
        _goText = null;
        foreach (SubUI_RoleIcon roleIcon in _playerRoleIcons)
        {
            roleIcon.Dispose();
        }
        _playerRoleIcons = null;
        foreach (SubUI_HPBar enemyInfo in _enemyInfos.Values)
        {
            enemyInfo.Dispose();
        }
        _enemyInfos = null;
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

    void ShowStart()
    {
        _uiPlayTween.tweenGroup = GLOBALCONST.UI_Battle_Start_TweenGroup;
        _uiPlayTween.resetIfDisabled = true; // disable時要重設tween
        _uiPlayTween.Play(true);
    }

    #region 按下按鈕的反應函式
    /// <summary>
    /// 按下加速鈕的反應函式
    /// </summary>
    void FastForwardBtnClick()
    {
        CommonFunction.DebugMsg("按下「加速鈕」");
        if (!isPause) { TimeMachine.FastForward(); }
    }

    /// <summary>
    /// 按下暫停鈕的反應函式
    /// </summary>
    void PauseBtnClick()
    {
        CommonFunction.DebugMsg("按下「暫停鈕」");
        isPause = !isPause;

        UISprite playAndPauseSprite = _pauseBtn.GetComponentInChildren<UISprite>();
        // 暫停時，顯示播放的圖，播放時，顯示暫停的圖
        playAndPauseSprite.spriteName = (isPause) ? NGUISpriteData.ICON_PLAY.GetSpriteName() : NGUISpriteData.ICON_PAUSE.GetSpriteName();

        if (isPause) { TimeMachine.Pause(); }
        else { TimeMachine.Resume(); }
        
    }
    /// <summary>
    /// 按下角色Icon的反應函式
    /// </summary>
    void IconBtnClick()
    {
        CommonFunction.DebugMsgFormat("按下 {0}", UIButton.current.name);
        int iconSelect = -1;
        string curClickRoleIconName = UIButton.current.transform.parent.name;
        string indexStr = curClickRoleIconName.Substring(curClickRoleIconName.LastIndexOf('_')+1);
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
        _playerRoleIcons[playerIndex].SetVisible(isVisible);
        if (isVisible)
        {
            _playerRoleIcons[playerIndex].SetHP((int)life, (int)maxLife);
        }
    }

    /// <summary>
    /// 加一個playerIcon
    /// </summary>
    /// <param name="parentObj">父物件</param>
    void AddPlayerIcon(GameObject parentObj)
    {
        int xBase = -656;
        int leftPadding = 432;
        int playerIndex = _playerRoleIcons.Count;

        _playerRoleIcons.Add(new SubUI_RoleIcon(new GORelativeInfo(parentObj, new Vector3(xBase + leftPadding * playerIndex, 12, 0), string.Format("PlayerRoleIcon_{0}", playerIndex)),
            ResourceStation.GetAtlas(InformalDataBase.Instance.playerInfo[playerIndex].spriteNames[(int)GLOBALCONST.eModelPartName.HEAD]),
            new EventDelegate(this, "IconBtnClick"), "玩家一二三四"));
    }
    #endregion

    #region 敵方場景上血條資訊
    const float enemyBarScale = 0.65f;

    // TODO: 此處只是測試使用==========
    static int count = 0;
    GameAttribute getTestGA()
    {
        return (GameAttribute)(count++ % System.Enum.GetValues(typeof(GameAttribute)).Length);
    }
    //=================================

    public void AddEnemyInfoUI(string enemyName, Vector3 relativePos, int curHP, int maxHP, GameAttribute ga = GameAttribute.NONE)
    {
        SubUI_HPBar tempHPBar = new SubUI_HPBar(_bossPic.transform.parent.gameObject, string.Format("Enemy_{0}_HP", enemyName), Vector3.zero,
            1, (int)(219 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI * enemyBarScale), (int)(41 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI * enemyBarScale),
            ga, 
            SubUI_HPBar.ShowMode.WITH_AVATAR);
        tempHPBar.FullSize = new Vector2(205 * enemyBarScale, 28 * enemyBarScale);
        tempHPBar.ForegroundPos = new Vector3(14, 2, 0);
        tempHPBar.ShiftPos = new Vector2(-71, -12);
        tempHPBar.SetHP(curHP, maxHP);
        _enemyInfos.Add(enemyName, tempHPBar);
        _enemyInfos[enemyName].UpdatePos(relativePos);
    }

    public void SetAllEnemyInfoUI(IList<Unit> allEnemy)
    {
        foreach (AnimUnit enemyUnit in allEnemy)
        {
            if (!_enemyInfos.ContainsKey(enemyUnit.Name))
            {
                AddEnemyInfoUI(enemyUnit.Name, BattleManager.UnitCamera.WorldToScreenPoint(enemyUnit.WorldUpperCenter), (int)enemyUnit.Life, (int)enemyUnit.MaxLife,
                    getTestGA()); // TODO: 改由enemyUnit內資訊取得
            }
            else
            {
                _enemyInfos[enemyUnit.Name].SetHP((int)enemyUnit.Life);
                _enemyInfos[enemyUnit.Name].UpdatePos(BattleManager.UnitCamera.WorldToScreenPoint(enemyUnit.WorldUpperCenter));
            }
        }
    }

    public void DeleteEnemyInfoUI(string enemyName)
    {
        if (_enemyInfos.ContainsKey(enemyName))
        {
            _enemyInfos[enemyName].Dispose();
            _enemyInfos.Remove(enemyName);
        }
    }

    public void ClearEnemyInfoUI()
    {
        foreach (SubUI_HPBar enemyInfo in _enemyInfos.Values)
        {
            enemyInfo.Dispose();
        }
        _enemyInfos.Clear();
    }
    #endregion
}
