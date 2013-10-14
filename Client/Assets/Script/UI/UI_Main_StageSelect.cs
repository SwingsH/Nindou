using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 主介面 - 關卡選擇
/// </summary>
public class UI_Main_StageSelect: GUIFormBase // : MonoBehaviour 
{

    #region 每個主介面都會有的部分
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    #endregion
    #region 關卡選擇部分
    private UILabel _stageNameText;
    private UIButton _returnPreviousUIBtn; // 回到上一層的按鈕
    private List<UIButton> _allSubStageSelectUIBtn; // 所有子關卡選擇按鈕
    private List<UILabel> _allSubStageName; // 所有子關卡名稱
    #endregion
    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        #region 每個主介面都有的部分
        // 背景圖
        UISprite backgroundPic = CommonFunction.CreateUISprite(gameObject, "Background", UISprite.Type.Simple, 0,
            ResourceStation.GetUIAtlas("TestAtlas"),
           "pachuri", UIWidget.Pivot.Center, 1920, 1080);
        // 「人物」按鈕
        _characterBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Character", new Vector3(-701, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        _characterBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        // 「背包」按鈕
        _bagBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Bag", new Vector3(-274.2f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.BAG_BTN_TEXT);
        _bagBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        // 「商店」按鈕
        _shopBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Shop", new Vector3(191.78f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.SHOP_BTN_TEXT);
        _shopBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        // 「好友」按鈕
        _friendBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Friend", new Vector3(653.42f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.FRIEND_BTN_TEXT);
        _friendBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        #endregion
        // 關卡選擇背景圖
        UISprite stageSelectBackground = CommonFunction.CreateUISprite(backgroundPic.gameObject, "StageSelectBackGround", UISprite.Type.Simple, 1,
            ResourceStation.GetUIAtlas("TestAtlas2"),
            "chiruno", UIWidget.Pivot.Center, 1760, 838);
        stageSelectBackground.transform.localPosition = new Vector3(10, 35, 0);
        // 場景名稱
        UISprite stageName = CommonFunction.CreateUISprite(stageSelectBackground.gameObject, "StageName", UISprite.Type.Simple, 2,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", UIWidget.Pivot.Center, 1044, 94);
        stageName.transform.localPosition = new Vector3(-249, 342, 0);
        _stageNameText = CommonFunction.CreateUILabel(stageName.gameObject, "StageNameText", UIWidget.Pivot.Center, Vector3.zero, 3,
            ResourceStation.GetUIFont("MSJH_30"), Color.red, "場景名稱：靈山 探索度：80%");
        // 回到上一層的按鈕
        _returnPreviousUIBtn = CommonFunction.CreateUIButton(stageSelectBackground.gameObject, "X", new Vector3(783, 321, 0), 4,
            ResourceStation.GetUIAtlas("SciFi Atlas"),
            "X", 100, 100, null, Color.white, string.Empty);
        _returnPreviousUIBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _returnPreviousUIBtn.onClick.Add(new EventDelegate(this, "ReturnPreviousUI"));

    }
    #endregion
    #region 固定函式
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override void OnDestroy()
    {
        NGUITools.Destroy(_characterBtn.gameObject);
        _characterBtn = null;
        NGUITools.Destroy(_bagBtn.gameObject);
        _bagBtn = null;
        NGUITools.Destroy(_shopBtn.gameObject);
        _shopBtn = null;
        NGUITools.Destroy(_friendBtn.gameObject);
        _friendBtn = null;
        NGUITools.Destroy(_stageNameText.gameObject);
        _stageNameText = null;
        base.OnDestroy();
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
        CommonFunction.DebugMsgFormat("現在按下的按鈕為：{0}", UIButton.current.name);
    }
}
