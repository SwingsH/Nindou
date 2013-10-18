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
    private class StageSubUI : System.IDisposable
    {
        private GameObject _stageSubUIObj; // 最上層物件
        private UIButton _stageBtn; // 關卡按鈕
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
        /// <param name="eventDelegate">按下按鈕的反應函式</param>
        /// <param name="stageName">要顯示的關卡名稱</param>
        public StageSubUI(GameObject parent, string stageUIName, Vector3 relativePos, int depth, EventDelegate clickEventDelegate)

        {
            // 最上層物件
            _stageSubUIObj = NGUITools.AddChild(parent);
            _stageSubUIObj.name = stageUIName;
            _stageSubUIObj.transform.localPosition = relativePos;
            // 底圖＆按鈕
            _stageBtn = GUIStation.CreateUIButton(_stageSubUIObj, "StageBtn", Vector3.zero, depth,
                ResourceStation.GetUIAtlas("TestAtlas"),
                "button_back", 1478, 200, null, Color.red, string.Empty);
            _stageBtn.SetColor(Color.white, Color.white, new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f), new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f));
            _stageBtn.onClick.Add(clickEventDelegate);
            
            // 關卡名稱＆消耗體力說明
            _stageNameText = GUIStation.CreateUILabel(_stageSubUIObj, "StageName", UIWidget.Pivot.Left, new Vector3(-653, 0, 0), depth + 1,
                ResourceStation.GetUIFont("MSJH_30"),
                Color.red, string.Empty);
            // 「點擊觀看開啟條件」的提示文字
            _hintText = GUIStation.CreateUILabel(_stageSubUIObj, "HintText", UIWidget.Pivot.Left, new Vector3(-120, 0, 0), depth + 1,
                ResourceStation.GetUIFont("MSJH_30"),
                Color.red, GLOBAL_STRING.STAGE_OPEN_HINT_TEXT);
            // 「未開啟」的提示文字
            _nonOpenText = GUIStation.CreateUILabel(_stageSubUIObj, "NonOpenText", UIWidget.Pivot.Center, new Vector3(430, 0, 0), depth + 1,
                ResourceStation.GetUIFont("MSJH_30"),
                Color.red, GLOBAL_STRING.STAGE_NOT_OPEN_TEXT);
            // 探索度的背景圖
            _exploreProgressBackground = GUIStation.CreateUISprite(_stageSubUIObj, "ExploreProgress", UISprite.Type.Simple, depth + 2,
                ResourceStation.GetUIAtlas("TestAtlas"),
                "button_back", UIWidget.Pivot.Center, 399, 67);
            _exploreProgressBackground.transform.localPosition = new Vector3(430, 0, 0);
            // 探索度的文字
            _exploreProgressText = GUIStation.CreateUILabel(_exploreProgressBackground.gameObject, "ExploreProgressText", UIWidget.Pivot.Left,
                new Vector3(-164, 0, 0), depth + 3,
                ResourceStation.GetUIFont("MSJH_30"),
                Color.red, string.Format(GLOBAL_STRING.STAGE_EXPLORE_PROGRESS_TEXT, 0, 1));
        }
        #endregion
        #region Dispose -- 資源釋放
        /* 直接將物件設為null，使其呼叫解構式來釋放資源會有「CompareBaseObjectsInternal can only be called from the main thread.」問題
         * 為了解決「CompareBaseObjectsInternal can only be called from the main thread.」問題，必須在main thread執行釋放資源，
         * 所以需要繼承IDisposable並實作Disposec函式。
         * http://msdn.microsoft.com/zh-tw/library/fs2xkftw(v=vs.90).aspx 有提供撰寫範例。
         * 但是呼叫端在刪除此物件時，需記得呼叫 Dispose()函式。
         */
        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);

            System.GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_exploreProgressText != null) { NGUITools.Destroy(_exploreProgressText.gameObject); }
                    if (_exploreProgressBackground != null) { NGUITools.Destroy(_exploreProgressBackground.gameObject); }
                    if (_nonOpenText != null) { NGUITools.Destroy(_nonOpenText.gameObject); }
                    if (_hintText != null) { NGUITools.Destroy(_hintText.gameObject); }
                    if (_stageNameText != null) { NGUITools.Destroy(_stageNameText.gameObject); }
                    if (_stageBtn != null) { NGUITools.Destroy(_stageBtn.gameObject); }
                    if (_stageSubUIObj != null) { NGUITools.Destroy(_stageSubUIObj); }
                }
                _exploreProgressText = null;
                _exploreProgressBackground = null;
                _nonOpenText = null;
                _hintText = null;
                _stageNameText = null;
                _stageBtn = null;
                _stageSubUIObj = null;
                _disposed = true;
            }
        }

        #endregion

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
                if (_stageOpen) { _stageBtn.SetColor(Color.white, Color.white, new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f), new Color(184.0f / 255.0f, 184.0f / 255.0f, 184.0f / 255.0f, 1.0f));}
                else { _stageBtn.SetColor(Color.gray, Color.gray, Color.gray, Color.gray); }

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

 
    #region 每個主介面都會有的部分
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    #endregion
    #region 關卡選擇部分
    private UISprite _stageSelectBackground; // 關卡選擇背景圖
    private UILabel _stageNameText;
    private UIButton _returnPreviousUIBtn; // 回到上一層的按鈕
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
        UISprite backgroundPic = GUIStation.CreateUISprite(panel.gameObject, "Background", UISprite.Type.Simple, 0,
            ResourceStation.GetUIAtlas("TestAtlas"),
           "pachuri", UIWidget.Pivot.Center, 1920, 1080);
        // 「人物」按鈕
        _characterBtn = GUIStation.CreateUIButton(backgroundPic.gameObject, "Character", new Vector3(-701, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        _characterBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        // 「背包」按鈕
        _bagBtn = GUIStation.CreateUIButton(backgroundPic.gameObject, "Bag", new Vector3(-274.2f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.BAG_BTN_TEXT);
        _bagBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        // 「商店」按鈕
        _shopBtn = GUIStation.CreateUIButton(backgroundPic.gameObject, "Shop", new Vector3(191.78f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.SHOP_BTN_TEXT);
        _shopBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        // 「好友」按鈕
        _friendBtn = GUIStation.CreateUIButton(backgroundPic.gameObject, "Friend", new Vector3(653.42f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.FRIEND_BTN_TEXT);
        _friendBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        #endregion
        // 關卡選擇背景圖
        _stageSelectBackground = GUIStation.CreateUISprite(backgroundPic.gameObject, "StageSelectBackGround", UISprite.Type.Simple, 1,
            ResourceStation.GetUIAtlas("TestAtlas2"),
            "chiruno", UIWidget.Pivot.Center, 1760, 838);
        _stageSelectBackground.transform.localPosition = new Vector3(10, 35, 0);
        // 場景名稱
        UISprite stageName = GUIStation.CreateUISprite(_stageSelectBackground.gameObject, "StageName", UISprite.Type.Simple, 2,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", UIWidget.Pivot.Center, 1044, 94);
        stageName.transform.localPosition = new Vector3(-249, 342, 0);
        _stageNameText = GUIStation.CreateUILabel(stageName.gameObject, "StageNameText", UIWidget.Pivot.Center, Vector3.zero, 3,
            ResourceStation.GetUIFont("MSJH_30"), Color.red, "場景名稱：靈山 探索度：80%");
        // 回到上一層的按鈕
        _returnPreviousUIBtn = GUIStation.CreateUIButton(_stageSelectBackground.gameObject, "X", new Vector3(783, 321, 0), 4,
            ResourceStation.GetUIAtlas("SciFi Atlas"),
            "X", 100, 100, null, Color.white, string.Empty);
        _returnPreviousUIBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _returnPreviousUIBtn.onClick.Add(new EventDelegate(this, "ReturnPreviousUI"));
        /// 先建三個 for Test
        for (int i = 0; i < 3; ++i)
        {
            AddStageInfo(i != 2, string.Format("靈山 - 山腳下 - {0}", i + 1), i + 1, i * 3 + 2, 25);
        }

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
        if (_returnPreviousUIBtn != null) { NGUITools.Destroy(_returnPreviousUIBtn.gameObject); }
        _returnPreviousUIBtn = null;
        ClearAllStageInfo();
        _allSubStage = null;
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
            BattleState.BattleID = (uint)(stageIndex + 1);
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
        _allSubStage.Add(new StageSubUI(_stageSelectBackground.gameObject, string.Format("Stage_{0}", curStageCount), 
              new Vector3(-76, 150 - curStageCount * 205, 0), 5,
                new EventDelegate(this, "SelectStage")));
        
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
