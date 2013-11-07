using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Battle : GUIFormBase
{
    const int BOSS_ICON_WIDTH = 138;
    const int BOSS_ICON_HEIGHT = 123;
    const int ROLE_ICON_PLATE_BG_WIDTH = GUIStation.MANUAL_SCREEN_WIDTH;
    const int ROLE_ICON_PLATE_BG_HEIGHT = 248;

    UISprite _bossPic; // Boss示意圖
    UILabel _bossNameText; // Boss 名稱
    UISlider _bossHPBar; // Boss HP 血條
    UIButton _fastForwardBtn; // 加速鈕
    UIButton _pauseBtn; // 暫停鈕
    UISprite _iconBackground; // 角色icon所在的背景圖
    List<UIButton> _iconBtns = new List<UIButton>(); // 玩家角色圖像
    List<SubUI_HPBar> _hpBars = new List<SubUI_HPBar>(); 
    List<UILabel> _roleNames = new List<UILabel>(); // 玩家角色名字

    Dictionary<string, SubUI_HPBar> _enemyInfos = new Dictionary<string, SubUI_HPBar>();

    bool isPause = false;

     #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);
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
        foreach (SubUI_HPBar hpBar in _hpBars)
        {
            hpBar.Dispose();
        }
        _hpBars = null;
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
        if (isVisible)
        {
            _hpBars[playerIndex].SetHP((int)life, (int)maxLife);
        }
    }

    /// <summary>
    /// 加一個playerIcon
    /// </summary>
    /// <param name="parentObj">父物件</param>
    void AddPlayerIcon(GameObject parentObj)
    {
        float iconScale = 1.5f;
        int leftPadding = 432;
        int playerIndex = _iconBtns.Count;
        // 角色按鈕兼底圖
        UIButton tempIconBtn = GUIStation.CreateUIButton(parentObj, string.Format("Icon_{0}", playerIndex), new Vector3(-656 + leftPadding * playerIndex, 12, 0), 1,
            NGUISpriteData.ROLE_ICON,  (int)(220 * iconScale), (int)(219*iconScale), null, Color.white, string.Empty);
        tempIconBtn.tweenTarget.name = "Role BG";
        tempIconBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        // 按鈕效果
        UIButtonScale tempBtnScale = tempIconBtn.gameObject.AddComponent<UIButtonScale>();
        tempBtnScale.tweenTarget = tempIconBtn.transform;
        tempIconBtn.onClick.Add(new EventDelegate(this, "IconBtnClick"));
        _iconBtns.Add(tempIconBtn);
        // 角色圖示(暫定)
#region 角色圖示(暫定)
        SmoothMoves.TextureAtlas atlas = ResourceStation.GetAtlas(InformalDataBase.Instance.playerInfo[playerIndex].spriteNames[(int)GLOBALCONST.eModelPartName.HEAD]);
        // Root
        GameObject roleGraphGO = NGUITools.AddChild(tempIconBtn.gameObject);
        roleGraphGO.name = "Role Graph";
        roleGraphGO.transform.localPosition = new Vector3(12, -21, 0);
        // 頭
        UITexture testHead = NGUITools.AddChild<UITexture>(roleGraphGO);
        testHead.name = "Head";
        testHead.material = atlas.material;
        
        int headSmoothMoveAtlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.HEAD]));

        testHead.uvRect = atlas.uvs[headSmoothMoveAtlasIndex];

        Vector2 headSize = atlas.GetTextureSize(headSmoothMoveAtlasIndex);
        testHead.width = (int)headSize.x;
        testHead.height = (int)headSize.y;
        testHead.depth = 2;
        // 眼睛
        UITexture testEye = NGUITools.AddChild<UITexture>(roleGraphGO);
        testEye.name = "Eye";
        testEye.material = atlas.material;
        
        int eyeSmoothMoveAtlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.EYES]));

        testEye.uvRect = atlas.uvs[eyeSmoothMoveAtlasIndex];

        Vector2 eyeSize = atlas.GetTextureSize(eyeSmoothMoveAtlasIndex);
        testEye.width = (int)eyeSize.x;
        testEye.height = (int)eyeSize.y;
        testEye.depth = 3;
        // 頭髮
        UITexture testHair = NGUITools.AddChild<UITexture>(roleGraphGO);
        testHair.name = "Hair";
        testHair.material = atlas.material;

        int hairSmoothMoveAtlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.HAIR]));

        testHair.uvRect = atlas.uvs[hairSmoothMoveAtlasIndex];

        Vector2 hairSize = atlas.GetTextureSize(hairSmoothMoveAtlasIndex);
        testHair.width = (int)hairSize.x;
        testHair.height = (int)hairSize.y;
        testHair.depth = 4;
#endregion

        SubUI_HPBar tempHPBar = new SubUI_HPBar(tempIconBtn.gameObject, "Role_HP_Bar", new Vector3(-104, -93, 0), 5,
            (int)(219 * iconScale), (int)(41 * iconScale));
        tempHPBar.FullSize = new Vector2(205, 28);
        _hpBars.Add(tempHPBar);
        // 角色名字
        UILabel tempRoleName = GUIStation.CreateUILabel(tempIconBtn.gameObject, "Role Name", UIWidget.Pivot.Center, new Vector3(107, -57, 0), 5,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_ROLE_NAME, FontStyle.Bold),
            Color.red, "玩家一二三四");
        _roleNames.Add(tempRoleName);
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
