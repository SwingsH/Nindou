using UnityEngine;
using System.Collections;

/// <summary>
/// 全域 & 非全域 "顯示用" string 變數皆須放置於此, 減少魔術字串
/// </summary>
public static class GLOBAL_STRING
{	
	public const string GAME_TITLE = "忍豆豆豆風雲";
    public const string UI_BUTTON_1 = "快速登入";

    #region UI_Main （主介面共通）
    public const string CHARACTER_BTN_TEXT = "人物";
    public const string BAG_BTN_TEXT = "背包";
    public const string SHOP_BTN_TEXT = "商店";
    public const string FRIEND_BTN_TEXT = "好友";
    #endregion
    #region UI_Main_WorldMap (顯示世界地圖的主介面)
    public const string MENU_BTN_TEXT = "選單";
    public const string WARNING_LABEL_TEXT = "強敵發現！！";
    #endregion
}