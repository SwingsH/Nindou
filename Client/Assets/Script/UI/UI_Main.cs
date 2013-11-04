using UnityEngine;
using System.Collections;

/// <summary>
/// 主介面（除了戰鬥&剛登入之外，幾乎會持續存在的部分)
/// </summary>
public class UI_Main : GUIFormBase
{
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; //       「背包」按鈕
    private UIButton _shopBtn; //      「商店」按鈕
    private UIButton _friendBtn; //    「好友」按鈕

    #region 繼承自GUIFormBase的method

    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        // 背景圖
        UISprite backgroundPic = UIImageManager.CreateUISprite(panel.gameObject, NGUISpriteData.WORLDMAP_BG);
        backgroundPic.SetEffectSizeParameter(UISprite.Type.Simple, UIWidget.Pivot.Center, GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT);
        backgroundPic.depth = 0;
        backgroundPic.name = "Background";
        // 下方物件的parent
        TweenPosition tweenPos = GUIComponents.AddShowMoveEffect(backgroundPic.gameObject, new Vector3(0, -206, 0), Vector3.zero);
        tweenPos.name = "BottomObjects";

        // 建立基本的四個按鈕：「人物」、「背包」、「商店」、「好友」
        MainMenuButtons(tweenPos.gameObject, out _characterBtn, out _shopBtn, out _friendBtn, out _bagBtn);
        // 設定對應的EventDelegate
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
    }
    #endregion

    #region 固定方法
    // Use this for initialization
	void Start () 
    {
        CreateAllComponent();
        _uiPlayTween = GUIComponents.AddPlayShowTweenComponent(gameObject);

        //foreach (UIButton btn in GetComponentsInChildren<UIButton>())
        //{
        //    if (btn.name.Equals("Character"))
        //    {
        //        _characterBtn = btn;
        //        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        //    }
        //    if (btn.name.Equals("Bag"))
        //    {
        //        _bagBtn = btn;
        //        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        //    }
        //    if (btn.name.Equals("Shop"))
        //    {
        //        _shopBtn = btn;
        //        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        //    }
        //    if (btn.name.Equals("Friend"))
        //    {
        //        _friendBtn = btn;
        //        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        //    }
        //}

        //foreach (UIPlayTween playTween in GetComponentsInChildren<UIPlayTween>())
        //{
        //    if (playTween.name.Equals("UI_Main")) { _uiPlayTween = playTween; }
        //}
	}
	
	// Update is called once per frame
	void Update () 
    {

    }
    #endregion
    public static void MainMenuButtons(GameObject parent, out UIButton character, out UIButton shop, out UIButton friend, out UIButton bag)
    {
        Transform pos = null;
        int margin = -576; //解析度 * 0.1 作為邊界
        int leftPadding = 360;
        int x = margin;
        int y = -435;
        float iconScale = 1.6f;

        character = GUIStation.CreateUIButton(parent, "Chatacter", new Vector3(x, y, 0), 10,
            NGUISpriteData.ICON_PERSON,
            (int)(136 * iconScale), (int)(115 * iconScale),
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        //調整文字位置
        pos = character.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 40, pos.localPosition.z);
        character.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        bag = GUIStation.CreateUIButton(parent, "Bag", new Vector3(x, y, 0), 10,
                                        NGUISpriteData.ICON_BAG,
                                        (int)(135 * iconScale), (int)(122 * iconScale),
                                        UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                        Color.white, GLOBAL_STRING.BAG_BTN_TEXT);
        //調整文字位置
        pos = bag.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 50, pos.localPosition.z);
        bag.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        shop = GUIStation.CreateUIButton(parent, "Shop", new Vector3(x, y, 0), 10,
                                        NGUISpriteData.ICON_STORE,
                                        (int)(133 * iconScale), (int)(115 * iconScale),
                                        UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                        Color.white, GLOBAL_STRING.SHOP_BTN_TEXT);
        //調整文字位置
        pos = shop.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 40, pos.localPosition.z);
        shop.SetColor(Color.white, Color.black, Color.grey, Color.grey);

        x = x + leftPadding;
        friend = GUIStation.CreateUIButton(parent, "Friend", new Vector3(x, y, 0), 10,
                                            NGUISpriteData.ICON_FRIEND,
                                            (int)(136 * iconScale), (int)(122 * iconScale),
                                            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
                                            Color.white, GLOBAL_STRING.FRIEND_BTN_TEXT);
        //調整文字位置
        pos = friend.gameObject.GetComponentInChildren<UILabel>().transform;
        pos.localPosition = new Vector3(pos.localPosition.x + 20, pos.localPosition.y - 50, pos.localPosition.z);
        friend.SetColor(Color.white, Color.black, Color.grey, Color.grey);
    }

    /// <summary>
    /// 按下「人物」按鈕的反應函式
    /// </summary>
    void CharacterBtnClick()
    {
        CommonFunction.DebugMsg("按下 「人物」按鈕");
    }
    /// <summary>
    /// 按下「背包」按鈕的反應函式
    /// </summary>
    void BagBtnClick()
    {
        CommonFunction.DebugMsg("按下「背包」按鈕");
    }
    /// <summary>
    /// 按下「商店」按鈕的反應函式
    /// </summary>
    void ShopBtnClick()
    {
        CommonFunction.DebugMsg("按下「商店」按鈕");
        _uiPlayTween.Play(true);
        CommonFunction.DebugMsg("play tween true");
    }
    /// <summary>
    /// 按下「好友」按鈕的反應函式
    /// </summary>
    void FriendBtnClick()
    {
        CommonFunction.DebugMsg("按下「好友」按鈕");
        _uiPlayTween.Play(false);
        CommonFunction.DebugMsg("play tween false");
    }

}
