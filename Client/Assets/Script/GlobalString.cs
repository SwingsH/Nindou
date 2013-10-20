using UnityEngine;
using System.Collections;

/// <summary>
/// 全域 & 非全域 "顯示用" string 變數皆須放置於此, 減少魔術字串
/// </summary>
public static class GLOBAL_STRING
{	
	public const string GAME_TITLE = "忍豆豆豆風雲";
    public const string UI_BUTTON_1 = "快速登入";
    public const string UI_BUTTON_2 = "重試";
    public const string UI_BUTTON_3 = "確認";

    public const string DIALOG_NETWORK_FAILED = "無法連接網路，\n請先開啟無線上網\n或行動網路";
    public const string DIALOG_SERVER_FAILED = "無法連線到伺服器，\n或資料出現錯誤";
    public const string DIALOG_NETWORK_UNKNOW = "資料錯誤";
    public const string UI_START_HINT_1 = "連線中...";
    public const string UI_START_HINT_2 = "點選畫面進行更新";
    public const string UI_START_HINT_3 = "檔案更新中";
    public const string UI_START_HINT_4 = "點選畫面進入遊戲";

    public const string UI_INPUT_HINT_1 = "點選此處輸入名稱";

    public const string UI_LABEL_PLAYER_NAME = "玩家名稱 : {0}";

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
    #region UI_Main_StageSelect (顯示關卡選擇的主介面）
    public const string STAGE_OPEN_HINT_TEXT = "點擊觀看開啟條件";
    public const string STAGE_NOT_OPEN_TEXT = "未開啟";
    public const string STAGE_NAME_TEXT = "{0}\n消耗體力：{1} PT";
    public const string STAGE_EXPLORE_PROGRESS_TEXT = "探索度：{0}/{1}";
    #endregion
}