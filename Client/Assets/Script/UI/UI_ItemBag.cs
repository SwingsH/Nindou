using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum BagOperateState
{
    Normal,
    ReadyBlend, //選卡了, 準備選合成素材
    ChoosedBlend, //已經選合成素材
    Blending      //合成中
}

/// <summary>
/// 主介面（除了戰鬥&剛登入之外，幾乎會持續存在的部分)
/// </summary>
public class UI_ItemBag : GUIFormBase
{
    private const int MAX_COLUMN_NUMS = 5;        //  一列顯示的物品數量
    private const int MAX_ROW_NUM = 2;          //  顯示幾列的物品
    
    private uint _currentClickID  = 0;
    private UIButton _btnPrevious;       //    「返回」按鈕
    private UIButton _btnConfirm;       //    「確認」按鈕
    private UISprite _bgBagFrame;       //    背包區塊底圖
    private UISprite _titleBagFrame;    //    背包標題區塊底圖
    private UILabel _titleText;         // 標題
    private UIPanel _panel;
 
    private UIButton[,] _itemButtons;    //  「物品」底框
    private UISprite[,] _itemIcons;     //  「物品」Icon 圖
    private UILabel[,] _itemLabels;     //  「物品」文字
    //private uint[,] _itemIDs;           //  「物品」ID   , todo : UIButton 擴充 flag 來儲存資訊

    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    private BagOperateState _currentState = BagOperateState.Normal;

    private Dictionary<BagOperateState, List<UIButton>> _clickID_EachState; //每個狀態下 click 的 item button 群

    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;
        _panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        // 背景圖
        UISprite backgroundPic = GUIComponents.MainBackground(_panel.gameObject, 0);

        // 下方物件的parent
        TweenPosition bottomObjectsTween = GUIComponents.AddShowMoveEffect(backgroundPic.gameObject, new Vector3(0, -206, 0), Vector3.zero);
        bottomObjectsTween.name = "BottomObjects";

        // 建立基本的四個按鈕：「人物」、「背包」、「商店」、「好友」
        GUIComponents.MainMenuButtons(bottomObjectsTween.gameObject, out _characterBtn, out _shopBtn, out _friendBtn, out _bagBtn);

        // 背景圖
        _bgBagFrame = UIImageManager.CreateUISprite(_panel.gameObject, NGUISpriteData.SLICE_BAG_FRAME_BG);
        _bgBagFrame.SetEffectSizeParameter(1450, 490, UISprite.Type.Sliced, UIWidget.Pivot.Center);
        _bgBagFrame.depth = 0;
        _bgBagFrame.name = "BagFrame";
        _bgBagFrame.transform.localPosition = new Vector3(0, -75, 0);

        // 標題圖
        _titleBagFrame = UIImageManager.CreateUISprite(_panel.gameObject, NGUISpriteData.SLICE_BAG_TITLE);
        _titleBagFrame.SetEffectSizeParameter(1450, 100, UISprite.Type.Tiled, UIWidget.Pivot.Center);
        _titleBagFrame.depth = 0;
        _titleBagFrame.name = "BagFrameTitle";
        _titleBagFrame.transform.localPosition = new Vector3(0, 205, 0);

        // 標題文字
        _titleText = GUIStation.CreateUILabel(_titleBagFrame.gameObject, "TitleLabel", UIWidget.Pivot.Center, new Vector3(0, 0, 0),  5,
                UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.LARGE, FontStyle.Bold), Color.white, GLOBAL_STRING.UI_LABEL_BAG_TITLE);

        //返回按鈕
        _btnPrevious = GUIStation.CreateUIButton(_panel.gameObject, "ButtonReturn", new Vector3(-842, 147, 0), 4,
            NGUISpriteData.BTN_PREVIOUS, 115, 153, null, Color.white, string.Empty);

        //確認按鈕
        _btnConfirm = GUIStation.CreateUIButton(_panel.gameObject, "ButtonConfirm", new Vector3(828, 190, 0), 4,
            NGUISpriteData.BTN_CONFIRM, 144, 132, null, Color.white, string.Empty);
        _btnConfirm.onClick.Add(new EventDelegate(this, "OnConfirmClick"));

        //建立 N 個 卡片 icon
        _itemButtons = new UIButton[MAX_ROW_NUM, MAX_COLUMN_NUMS];
        _itemIcons   = new UISprite[MAX_ROW_NUM, MAX_COLUMN_NUMS];
        _itemLabels = new UILabel[MAX_ROW_NUM, MAX_COLUMN_NUMS];
        UpdateItems();
    }

    public Dictionary<BagOperateState, List<UIButton>> ClickIDEachState
    {
        get { return _clickID_EachState;  }
    }

    public void UpdateItems()
    {
        uint cardID = 0;
        int cardCount = 0;
        ItemData itemData = null;
        for (int row = 0; row < MAX_ROW_NUM; row++)
        {
            for (int i = 0; i < MAX_COLUMN_NUMS; i++, cardCount++)
            {
                cardID = _guistation.Account.Cards[cardCount];

                Vector3 pos = new Vector3(-480 + i * 230, row * -200);
                _itemButtons[row, i] = GUIStation.CreateUIButton(_panel.gameObject, string.Format("Item_{0}_{1}_{2}", row, i, cardID), pos, 6,
                                        NGUISpriteData.ICON_EQUIP_BG, 140, 140, null, Color.white, string.Empty);
                _itemButtons[row, i].onClick.Add(new EventDelegate(this, "OnItemClick"));

                //CommonFunction.DebugMsg(" cardID : " + cardID.ToString());

                itemData = InformalDataBase.Instance.GetItemData(cardID);
                if (itemData == null)
                {
                    CommonFunction.DebugMsg(" itemData == null ");
                    if (_itemIcons[row, i] != null)
                        NGUITools.SetActive(_itemIcons[row, i].gameObject, false);
                    if (_itemLabels[row, i] != null)
                        NGUITools.SetActive(_itemLabels[row, i].gameObject, false);
                    continue;
                }
                else
                {
                    if (_itemIcons[row, i] != null)
                        NGUITools.SetActive(_itemIcons[row, i].gameObject, true);
                    if (_itemLabels[row, i] != null)
                        NGUITools.SetActive(_itemLabels[row, i].gameObject, true);
                }

                // 建立物品圖
                ItemKind itemKind = (ItemKind)itemData.Kind;
                if (_itemIcons[row, i] != null)
                    NGUITools.DestroyImmediate(_itemIcons[row, i].gameObject);
                switch (itemKind)
                {
                    // 技能類 icon 圖庫
                    case ItemKind.Skill:
                    case ItemKind.PassiveSkill:
                    case ItemKind.Weapon:
                        _itemIcons[row, i] = UIImageManager.CreateWeaponIconSprite(_itemButtons[row, i].gameObject, (int)itemData.IconNumber);
                        break;

                    // 素材 icon 圖庫
                    case ItemKind.Material:
                        _itemIcons[row, i] = UIImageManager.CreateMaterialIconSprite(_itemButtons[row, i].gameObject, (int)itemData.IconNumber);
                        break;
                    default:
                        break;
                }

                if (_itemIcons[row, i] == null)
                {
                    CommonFunction.DebugError(" item icons is null.  " + cardID.ToString());
                    continue;
                }
                _itemIcons[row, i].SetEffectSizeParameter(140, 140, UISprite.Type.Simple, UIWidget.Pivot.Center);

                // 建立物品 label
                if (_itemLabels[row, i] == null)
                {
                    _itemLabels[row, i] = GUIStation.CreateUILabel(_itemIcons[row, i].gameObject,
                                    string.Format("ItemLabel_{0}_{1}", row, i), UIWidget.Pivot.Center, new Vector3(0, -50, 0), 9,
                                    UIFontManager.GetUIDynamicFont(UIFontName.MSJH, fontStyle: FontStyle.Bold),
                                    Color.white, itemData.Name);
                }
                else
                {
                    CommonFunction.DebugMsg(" _itemLabels[row, i].text = itemData.Name; " + itemData.Name);
                    _itemLabels[row, i].text = itemData.Name;
                }
            }
        }
    }

    #region 固定方法
    // Use this for initialization
	void Start () 
    {
        _uiPlayTween = GUIComponents.AddPlayShowTweenComponent(gameObject);
        _clickID_EachState = new Dictionary<BagOperateState, List<UIButton>>();
        _clickID_EachState.Add(BagOperateState.Normal, new List<UIButton>());
        _clickID_EachState.Add(BagOperateState.ReadyBlend, new List<UIButton>());
        _clickID_EachState.Add(BagOperateState.ChoosedBlend, new List<UIButton>());
        _clickID_EachState.Add(BagOperateState.Blending, new List<UIButton>());
	}
	
	// Update is called once per frame
	void Update () 
    {

    }
    #endregion

    // todo: 讓 btn 可以儲存資訊, 至少包含 ID
    void OnItemClick()
    {
        ItemData itemData = null;
        if (_currentState == BagOperateState.ReadyBlend) // 選擇合成素材的狀態
        {
            itemData = InformalDataBase.Instance.GetItemData(_currentClickID);
            _currentClickID = GetItemIDFromButton(UIButton.current);
            if (_currentClickID == 0)
                return;
            if (itemData.UpgradeMaterialID != _currentClickID)
            {
                CommonFunction.DebugMsg("無法用這個素材合成");
                return;
            }
            CommonFunction.DebugMsg("準備 !!使用這個素材合成");

            _currentState = BagOperateState.ChoosedBlend;
            UIButton.current.disabledColor = Color.yellow;
            UIButton.current.defaultColor = Color.yellow;
            UIButton.current.UpdateColor(false, true);

            if (!_clickID_EachState[_currentState].Contains(UIButton.current))
                _clickID_EachState[_currentState].Add(UIButton.current);

            return;
        }

        _currentClickID = GetItemIDFromButton(UIButton.current);

        // 搜索該 卡片 合成 所需 material
        itemData = InformalDataBase.Instance.GetItemData(_currentClickID);
        if (itemData == null)
            return;

        if (_currentClickID == 0)
            return;
        uint materialID = itemData.UpgradeMaterialID;

        if (materialID == 0)
        {
            CommonFunction.DebugMsg("無法升級");
            return; // 無法升級
        }

        _currentState = BagOperateState.ReadyBlend;

        uint btnID = 0;
        for (int row = 0; row < MAX_ROW_NUM; row++)
        {
            for (int i = 0; i < MAX_COLUMN_NUMS; i++)
            {
                btnID = GetItemIDFromButton(_itemButtons[row, i]);

                if (btnID != materialID)
                {
                    DoDisableFlag(row, i);
                }
            }
        }

        if (!_clickID_EachState[_currentState].Contains(UIButton.current))
            _clickID_EachState[_currentState].Add(UIButton.current);
    }

    public void OnConfirmClick()
    {
        UIButton mainItem     = _clickID_EachState[BagOperateState.ReadyBlend][0];
        UIButton materialItem = _clickID_EachState[BagOperateState.ChoosedBlend][0];

        uint mainItemIndex      = GetItemIndexFromButton(mainItem);
        uint materialItemIndex  = GetItemIndexFromButton(materialItem);
        uint mainItemID         = GetItemIDFromButton(mainItem);
        uint materialItemID     = GetItemIDFromButton(materialItem);

        _guistation.Control.DoItemBlend(mainItemIndex, materialItemIndex, mainItemID, materialItemID);
        _currentState = BagOperateState.Normal;
    }

    /// <summary>
    /// 無法選取的效果
    /// </summary>
    private void DoDisableFlag(int row, int column)
    {
        if (_itemButtons[row, column] != null)
        {
            _itemButtons[row, column].disabledColor = Color.gray;
            _itemButtons[row, column].defaultColor = Color.gray;
            _itemButtons[row, column].UpdateColor(false, true);
        }
        if (_itemLabels[row, column] != null)
            _itemLabels[row, column].text = GLOBAL_STRING.UI_LABEL_DISABLE_SELECT;
        if (_itemIcons[row, column] != null)
            _itemIcons[row, column].color = Color.gray;
    }

    /// <summary>
    /// 取得 BUTTON 的 ITEM ID, todo : use button tags
    /// </summary>
    public uint GetItemIDFromButton(UIButton button)
    {
        string name = button.name;
        string[] strs = name.Split(new char[] { '_' });
        return Convert.ToUInt32(strs[strs.Length - 1]);
    }

    /// <summary>
    /// 取得 BUTTON 的 ITEM index, todo : use button tags
    /// </summary>
    public uint GetItemIndexFromButton(UIButton button)
    {
        string name = button.name;
        string[] strs = name.Split(new char[] { '_' });

        uint col = Convert.ToUInt32(strs[2]);
        uint row = Convert.ToUInt32(strs[1]);

        return row * MAX_COLUMN_NUMS + col + 1;
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
