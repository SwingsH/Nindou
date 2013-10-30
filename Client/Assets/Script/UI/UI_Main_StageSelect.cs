using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 主介面 - 關卡選擇
/// </summary>
public class UI_Main_StageSelect: GUIFormBase // : MonoBehaviour 
{
    /// <summary>
    /// 一個關卡的子介面
    /// </summary>
    private class StageSubUI : GUISubFormBase
    {
        const int EXPLORE_PROGRESS_BG_WIDTH = 522;
        const int EXPLORE_PROGRESS_BG_HEIGHT = 148;

        private UIButton _stageBtn; // 關卡按鈕
        private UISprite _stageBtnBG; // 關卡按鈕的背景圖
        private UILabel _stageNameText; // 關卡名稱(&消耗體力說明)
        private UILabel _hintText; // 「點擊觀看開啟條件」的提示文字
        private UILabel _nonOpenText; // 「未開啟」的提示文字
        private UISprite _exploreProgressBackground; // 探索度的背景圖
        private UILabel _exploreProgressText; // 探索度的文字

        #region 物件建立
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="parent">此子UI的parent物件</param>
        /// <param name="stageUIName">關卡UI名稱</param>
        /// <param name="relativePos">和parent的相對位置</param>
        /// <param name="depth">深度</param>
        /// <param name="stageName">要顯示的關卡名稱</param>
        /// <param name="clickEventDelegate">按下按鈕的反應函式</param>
        /// <param name="parentDraggablePanel">上層的可拖曳panel</param>
        public StageSubUI(GameObject parent, string stageUIName, Vector3 relativePos, int depth, EventDelegate clickEventDelegate, UIDraggablePanel parentDraggablePanel)
            : base(parent, stageUIName, relativePos)
        {
            // 最上層物件已經在base class中處理，此處處理拖拉的Panel
            if (parentDraggablePanel != null)
            {
                UIDragPanelContents tempDPC = _subUIRoot.gameObject.AddComponent<UIDragPanelContents>();
                tempDPC.draggablePanel = parentDraggablePanel;
            }
            // 底圖＆按鈕
            _stageBtn = GUIComponents.StageWideButton(_subUIRoot, depth);
			_stageBtnBG = _stageBtn.tweenTarget.GetComponent<UISprite>();
            _stageBtn.onClick.Add(clickEventDelegate);

            if (parentDraggablePanel != null)
            {
                UIDragPanelContents tempDPC = _stageBtn.gameObject.AddComponent<UIDragPanelContents>();
                tempDPC.draggablePanel = parentDraggablePanel;
            }
            // 關卡名稱＆消耗體力說明
            _stageNameText = GUIStation.CreateUILabel(_subUIRoot, "StageName", UIWidget.Pivot.Left, new Vector3(-653, 0, 0), depth + 1,
                GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                Color.white, string.Empty);
            // 「點擊觀看開啟條件」的提示文字
            _hintText = GUIStation.CreateUILabel(_subUIRoot, "HintText", UIWidget.Pivot.Left, new Vector3(-120, 0, 0), depth + 1,
                GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                Color.white, GLOBAL_STRING.STAGE_OPEN_HINT_TEXT);
            // 「未開啟」的提示文字
            _nonOpenText = GUIStation.CreateUILabel(_subUIRoot, "NonOpenText", UIWidget.Pivot.Center, new Vector3(430, 0, 0), depth + 1,
                GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                Color.red, GLOBAL_STRING.STAGE_NOT_OPEN_TEXT);
            // 探索度的背景圖
            _exploreProgressBackground = UIImageManager.CreateUISprite(_subUIRoot, SpriteName.EXPLORE_PROGRESS_BG);
            _exploreProgressBackground.Init(UISprite.Type.Simple, depth + 2, _exploreProgressBackground.pivot, EXPLORE_PROGRESS_BG_WIDTH, EXPLORE_PROGRESS_BG_HEIGHT);
            _exploreProgressBackground.name = "ExploreProgress";
            _exploreProgressBackground.transform.localPosition = new Vector3(430, 0, 0);

            // 探索度的文字
            _exploreProgressText = GUIStation.CreateUILabel(_exploreProgressBackground.gameObject, "ExploreProgressText", UIWidget.Pivot.Left,
                new Vector3(-104, -10, 0), depth + 3,
                GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, fontStyle:FontStyle.Bold),
                Color.white, string.Format(GLOBAL_STRING.STAGE_EXPLORE_PROGRESS_TEXT, 0, 1));

            // 取得關卡開啟/關閉時底圖SpriteName，方便之後動態切換
            EnumUISpriteConfig tempUISpriteConfig;
            if (CommonFunction.GetAttribute<EnumUISpriteConfig>(SpriteName.STAGE_BG_OPEN, out tempUISpriteConfig)) { _stageOpenSpriteName = tempUISpriteConfig.SpriteName; }
            else { _stageOpenSpriteName = string.Empty; }
            if (CommonFunction.GetAttribute<EnumUISpriteConfig>(SpriteName.STAGE_BG_CLOSE, out tempUISpriteConfig)) { _stageCloseSpriteName = tempUISpriteConfig.SpriteName; }
            else { _stageCloseSpriteName = string.Empty; }
        }
        #endregion
        #region Dispose -- 資源釋放
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                NGUITools.Destroy(_exploreProgressText);
                NGUITools.Destroy(_exploreProgressBackground);
                NGUITools.Destroy(_nonOpenText);
                NGUITools.Destroy(_hintText);
                NGUITools.Destroy(_stageNameText);
                NGUITools.Destroy(_stageBtnBG);
                NGUITools.Destroy(_stageBtn);
            }
            _exploreProgressText = null;
            _exploreProgressBackground = null;
            _nonOpenText = null;
            _hintText = null;
            _stageNameText = null;
            _stageBtnBG = null;
            _stageBtn = null;

            base.Dispose(disposing);
        }

        #endregion

        Color pressedAndHoverColor = CommonFunction.Color256Bit(212, 212, 212, 255);
        string _stageOpenSpriteName = null;
        string _stageCloseSpriteName = null;
        /// <summary>
        /// 關卡是否開啟
        /// </summary>
        private bool _stageOpen;
        public bool StageOpen
        {
            get { return _stageOpen; }
            set
            {
                _stageOpen = value;

                if (_stageOpen) { _stageBtn.SetColor(Color.white, Color.white, pressedAndHoverColor, pressedAndHoverColor); }
                else { _stageBtn.SetColor(Color.gray, Color.gray, Color.gray, Color.gray); }

                
                if (_stageBtnBG != null)
                {
                    _stageBtnBG.spriteName = _stageOpen ? _stageOpenSpriteName : _stageCloseSpriteName;
                }
                NGUITools.SetActive(_hintText.gameObject, !_stageOpen);
                NGUITools.SetActive(_nonOpenText.gameObject, !_stageOpen);
                NGUITools.SetActive(_exploreProgressBackground.gameObject, _stageOpen);
            }
        }

        /// <summary>
        /// 關卡名稱
        /// </summary>
        private string _stageName;
        public string StageName
        {
            get { return _stageName; }
        }
        /// <summary>
        /// 體力消耗
        /// </summary>
        private int _cost;
        /// <summary>
        /// 設定關卡名稱＆體力消耗，傳入值如果為null，表示不做變動
        /// </summary>
        /// <param name="stageName">關卡名稱</param>
        /// <param name="cost">體力消耗</param>
        public void SetStageNameAndCost(string stageName, int? cost)
        {
            if (stageName != null) { _stageName = stageName; }
            if (cost.HasValue) { _cost = cost.Value; }
            if (_stageNameText != null) { _stageNameText.text = string.Format(GLOBAL_STRING.STAGE_NAME_TEXT, _stageName, _cost); }
        }

        
        /// <summary>
        /// 現在的探索度
        /// </summary>
        private int _currentExploreProgress;
        /// <summary>
        /// 最大探索度
        /// </summary>
        private int _maxExploreProgress;
        /// <summary>
        /// 設定探索度，傳入值如果是null，表示不做變動
        /// </summary>
        /// <param name="currenExploreProgress">新的「現在探索度」</param>
        /// <param name="maxExploreProgress">新的「最大探索度」</param>
        public void SetExploreProgress(int? currenExploreProgress, int? maxExploreProgress)
        {
            if (currenExploreProgress.HasValue) { _currentExploreProgress = currenExploreProgress.Value; }
            if (maxExploreProgress.HasValue) { _maxExploreProgress = maxExploreProgress.Value; }
            if (_exploreProgressText != null) { _exploreProgressText.text = string.Format(GLOBAL_STRING.STAGE_EXPLORE_PROGRESS_TEXT, _currentExploreProgress, _maxExploreProgress); }
        }

    }


    const int STAGE_TITLE_BG_WIDTH = 1044;
    const int STAGE_TITLE_BG_HEIGHT = 94;
 
    #region 每個主介面都會有的部分
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    #endregion
    #region 關卡選擇部分
    private UISprite _stageSelectBackground; // 關卡選擇背景圖
    private UILabel _stageNameText; // 場景名稱
    private UILabel _stageProgress; // 場景進度
    private UIButton _returnPreviousUIBtn; // 回到上一層的按鈕
    private UIDraggablePanel _stageSelectDraggablePanel; // 關卡選擇所在的DraggablePanel
    private UIGrid _stageSelectGrid; // 關卡選擇所在的Grid
    private List<StageSubUI> _allSubStage = new List<StageSubUI>();
    #endregion
    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        #region 每個主介面都有的部分
        // 背景圖
        UISprite backgroundPic = GUIComponents.MainBackground(panel.gameObject, 0);

        TweenPosition bottomObjectsTween = GUIComponents.AddShowMoveEffect(backgroundPic.gameObject, new Vector3(0, -206, 0), Vector3.zero);
        bottomObjectsTween.name = "BottomObjects";

        // 建立基本的四個按鈕：「人物」、「背包」、「商店」、「好友」
        //GUIComponents.MainMenuButtons(backgroundPic.gameObject, out _characterBtn, out _shopBtn, out _friendBtn, out _bagBtn);
        GUIComponents.MainMenuButtons(bottomObjectsTween.gameObject, out _characterBtn, out _shopBtn, out _friendBtn, out _bagBtn);
        // 設定對應的EventDelegate
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        #endregion

        TweenPosition stageSelectObjectsTween = GUIComponents.AddShowMoveEffect(backgroundPic.gameObject, new Vector3(0, 1061, 0), Vector3.zero);
        stageSelectObjectsTween.name = "StageSelectObjects";
        // 關卡選擇背景圖
        //_stageSelectBackground = GUIComponents.StageFrame(backgroundPic.gameObject);
        _stageSelectBackground = GUIComponents.StageFrame(stageSelectObjectsTween.gameObject);

        // 場景名稱、進度的背景圖
        UISprite stageName = UIImageManager.CreateUISprite(_stageSelectBackground.gameObject, SpriteName.STAGE_TITLE_BG);
        stageName.Init(stageName.type, 2, stageName.pivot, STAGE_TITLE_BG_WIDTH, STAGE_TITLE_BG_HEIGHT);
        stageName.name = "StageTitle";
        stageName.transform.localPosition = new Vector3(-249, 342, 0);
        // 場景名稱
        _stageNameText = GUIStation.CreateUILabel(stageName.gameObject, "StageNameText", UIWidget.Pivot.Left, new Vector3(-258, -26, 0), 3,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, "場景名稱：靈山");
        // 場景進度
        _stageProgress = GUIStation.CreateUILabel(stageName.gameObject, "StageProgress", UIWidget.Pivot.Center, new Vector3(233, -26, 0), 3,
            GUIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, "探索度：80%");
        // 回到上一層的按鈕
        _returnPreviousUIBtn = GUIStation.CreateUIButton(_stageSelectBackground.gameObject, "X", new Vector3(783, 321, 0), 4,
            SpriteName.BTN_CLOSE,
            100, 100, null, Color.white, string.Empty);
        _returnPreviousUIBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _returnPreviousUIBtn.onClick.Add(new EventDelegate(this, "ReturnPreviousUI"));

        UIPanel stageSelectPanel = NGUITools.AddChild<UIPanel>(_stageSelectBackground.gameObject);
        stageSelectPanel.name = "StageSelect";
        stageSelectPanel.clipping = UIDrawCall.Clipping.SoftClip;
        stageSelectPanel.clipRange = new Vector4(35.3f, -66, 1623, 675);
        stageSelectPanel.clipSoftness = new Vector2(10, 10);
        
        _stageSelectDraggablePanel = stageSelectPanel.gameObject.AddComponent<UIDraggablePanel>();
        _stageSelectDraggablePanel.scale = new Vector3(0, 1, 0); // 限制只有垂直方向可拖曳

        _stageSelectGrid = NGUITools.AddChild<UIGrid>(stageSelectPanel.gameObject);
        _stageSelectGrid.arrangement = UIGrid.Arrangement.Vertical;
        _stageSelectGrid.cellHeight = 210;
        _stageSelectGrid.sorted = true;
        _stageSelectGrid.hideInactive = true;
        _stageSelectGrid.transform.localPosition = new Vector3(0, 200, 0);

        UICenterOnChild tempCOC = _stageSelectGrid.gameObject.AddComponent<UICenterOnChild>();

        /// for Test
        for (int i = 0; i < 4; ++i)
        {
            AddStageInfo(i != 2, string.Format("靈山 - 山腳下 - {0}", i + 1), i + 1, i * 3 + 2, 25);
        }
        // 強制將第一個置中
        tempCOC.Recenter();
    }
    #endregion
    #region 固定函式
    // Use this for initialization
    void Start()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
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
        if (_stageNameText != null) { NGUITools.Destroy(_stageNameText.gameObject); }
        _stageNameText = null;
        if (_stageProgress != null) { NGUITools.Destroy(_stageProgress.gameObject); }
        _stageProgress = null;
        if (_returnPreviousUIBtn != null) { NGUITools.Destroy(_returnPreviousUIBtn.gameObject); }
        _returnPreviousUIBtn = null;
        ClearAllStageInfo();
        _allSubStage = null;
        if (_stageSelectGrid != null) { NGUITools.Destroy(_stageSelectGrid.gameObject); }
        _stageSelectGrid = null;
        if (_stageSelectDraggablePanel != null) { NGUITools.Destroy(_stageSelectDraggablePanel.gameObject); }
        _stageSelectDraggablePanel = null;
        base.OnDestroy();
    }
    #endregion

    #region 每個主介面都有的部分
    /// <summary>
    /// 按下「人物」按鈕的反應函式
    /// </summary>
    void CharacterBtnClick()
    {
        CommonFunction.DebugMsg("按下 「人物」按鈕");
        // test :
        _allSubStage[0].StageOpen = false;
        CommonFunction.DebugMsg("關閉第一關");
    }
    /// <summary>
    /// 按下「背包」按鈕的反應函式
    /// </summary>
    void BagBtnClick()
    {
        CommonFunction.DebugMsg("按下「背包」按鈕");
        // test :
        _allSubStage[2].StageOpen = true;
        CommonFunction.DebugMsg("開啟第三關");
    }
    /// <summary>
    /// 按下「商店」按鈕的反應函式
    /// </summary>
    void ShopBtnClick()
    {
        CommonFunction.DebugMsg("按下「商店」按鈕");
        // test :
        _allSubStage[1].SetExploreProgress(8, 30);
        CommonFunction.DebugMsg("修改第二關探索度=> 8/30");
    }
    /// <summary>
    /// 按下「好友」按鈕的反應函式
    /// </summary>
    void FriendBtnClick()
    {
        CommonFunction.DebugMsg("按下「好友」按鈕");
        // test : 
        _allSubStage[0].SetStageNameAndCost("靈山山腰", null);
        CommonFunction.DebugMsg("修改第一關名字=> 「靈山山腰」");
    }
    #endregion

    /// <summary>
    /// 按下回到上一層（X）按鈕的反應函式
    /// </summary>
    void ReturnPreviousUI()
    {
        CommonFunction.DebugMsg("按下返回(X)按鈕");
        // TODO: 如果確定只能從主介面點選關卡地圖進入此介面，則直接回到進入遊戲狀態即可
        //       否則會需要處理「回到上一個開啟介面」的功能
        GameControl.Instance.ChangeGameState(GameEntered.Instance);
    }
    /// <summary>
    /// 按下選擇關卡的按鈕
    /// </summary>
    void SelectStage()
    {
        int stageIndex = -1;
        if (!int.TryParse(UIButton.current.transform.parent.name.Substring(6), out stageIndex))
        {
            CommonFunction.DebugMsgFormat("現在按下的關卡為：{0}，無法解析index，請重新設定", UIButton.current.transform.parent.name);
            return;
        }
        if (stageIndex < 0 || stageIndex >= _allSubStage.Count)
        {
            CommonFunction.DebugMsgFormat("現在按下的關卡為：{0}，index有誤，請檢查", UIButton.current.transform.parent.name);
            return;
        }
        // 關卡是開啟的，顯示探索介面
        // TODO : 先直接進入戰鬥，之後再改
        if (_allSubStage[stageIndex].StageOpen)
        {
            CommonFunction.DebugMsgFormat("{0}   關卡已經開啟，進入探索", _allSubStage[stageIndex].StageName);
            // for test : 進入戰鬥
            BattleState.BattleID = (uint)(stageIndex % 3 + 1);
            GameControl.Instance.ChangeGameState(BattleState.instance);
        }
        else // 關卡尚未開啟，顯示開啟條件
        {
            CommonFunction.DebugMsgFormat("{0}   關卡尚未開啟", _allSubStage[stageIndex].StageName);
        }
    }

    #region 關卡子UI相關
    /// <summary>
    /// 清除所有關卡資訊
    /// </summary>
    public void ClearAllStageInfo()
    {
        foreach (StageSubUI stage in _allSubStage)
        {
            stage.Dispose();
        }
        _allSubStage.Clear();
    }

    /// <summary>
    /// 加入一筆關卡資訊（同時會新增UI），回傳該關卡的index
    /// </summary>
    /// <param name="isOpen">是否已經開啟</param>
    /// <param name="stageName">關卡名稱</param>
    /// <param name="cost">體力消耗</param>
    /// <param name="currentExploreProgress">現在探索度</param>
    /// <param name="maxExplorerProgress">最大探索度</param>
    /// <returns>該關卡的index</returns>
    public int AddStageInfo(bool isOpen, string stageName, int cost, int currentExploreProgress, int maxExploreProgress)
    {
        int curStageCount = _allSubStage.Count;
        _allSubStage.Add(new StageSubUI(_stageSelectGrid.gameObject, string.Format("Stage_{0}", curStageCount), 
              new Vector3(-76, 150 - curStageCount * 25, 0), 5,
                new EventDelegate(this, "SelectStage"), _stageSelectDraggablePanel));
        
        SetStageInfo(curStageCount, isOpen, stageName, cost, currentExploreProgress, maxExploreProgress);
        return curStageCount;
    }

    /// <summary>
    /// 設定關卡資訊(index超過不做事情)，後四項參數可為null，若為null，表示不做修改
    /// </summary>
    /// <param name="stageIndex">關卡的index</param>
    /// <param name="isOpen">是否已經開啟</param>
    /// <param name="stageName">關卡名稱</param>
    /// <param name="cost">體力消耗</param>
    /// <param name="currentExploreProgress">現在探索度</param>
    /// <param name="maxExploreProgress">最大探索度</param>
    /// <returns>是否修改成功</returns>
    public bool SetStageInfo(int stageIndex, bool isOpen, string stageName, int? cost, int? currentExploreProgress, int? maxExploreProgress)
    {
        if (stageIndex >= _allSubStage.Count) { return false; }
        _allSubStage[stageIndex].StageOpen = isOpen;
        _allSubStage[stageIndex].SetStageNameAndCost(stageName, cost);
        _allSubStage[stageIndex].SetExploreProgress(currentExploreProgress, maxExploreProgress);
        return true;
    }
    #endregion
}
