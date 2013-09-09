//#define OPEN_DATA_PIECE
//#define _LANGUAGE_ENG //英文字串
#define _LANGUAGE_CHT //繁中字串

using UnityEngine;
using System.Collections;

/// <summary>
/// 常數字串專用類別
/// Nic@atlantis
/// </summary>

#if _LANGUAGE_CHT
public static class GlobalConstString
{	
	//路徑
	//public const string Characters_AssetbundlePath = "assetbundles/Characters/";
	//public const string CharacterMeshs_AssetbundlePath = "assetbundles/Characters/Meshs/";
	//public const string CharacterMaterials_AssetbundlePath = "assetbundles/Characters/Materials/";
	//public const string CharacterAnimations_AssetbundlePath = "assetbundles/Characters/Animations/";
	public const string NPC_AssetbundlePath = "assetbundles/NPC/";
	public const string CameraEffectPath = "EffectCamera/";
	//檔名
	public const string AssetBundleExtName = ".unity3d";//AssetBundle副檔名
	public const string MaterialExtName = ".mat";//材質副檔名

	public const string PREFIX_CameraEffect = "Effectcam_"; // 鏡頭特效Prefix

    public const string POSTFIX_CharacterMeshsDatabase = "charactermeshsdatabase" ;
    public const string POSTFIX_CharacterMaterialsDatabase = "charactermaterialsdatabase" ;
    public const string POSTFIX_CharacterMeshMaterialCountData = "charactermeshmaterialcountdata" ;
    public const string POSTFIX_CharacterBase = "_characterbase" ;
    public static readonly string[] POSTFIX_CharacterAnimation = {
		"_motion_01",
		"_motion_02",
		"_motion_03",
		"_motion_04",
		"_motion_05",
		"_motion_06",
		"_motion_07",
		"_motion_08",
		"_motion_09"
	};
	
    public const string POSTFIX_NPCAnimation = "_npcanimation" ;
    public const string POSTFIX_Notify = "_notify" ;

    public const string AssetBundleFileName_CharacterMeshsDatabase = POSTFIX_CharacterMeshsDatabase + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterMaterialsDatabase = POSTFIX_CharacterMaterialsDatabase + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterMeshMaterialCountData = POSTFIX_CharacterMeshMaterialCountData + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterBase = POSTFIX_CharacterBase + GLOBALCONST.EXT_ASSETBUNDLE;
	
    public static readonly string[] AssetBundleFileName_CharacterAnimation = {
		POSTFIX_CharacterAnimation[0] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[1] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[2] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[3] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[4] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[5] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[6] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[7] + GLOBALCONST.EXT_ASSETBUNDLE,
		POSTFIX_CharacterAnimation[8] + GLOBALCONST.EXT_ASSETBUNDLE
	};

	public const string AssetBundleFileName_NPCAnimation = POSTFIX_NPCAnimation + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_Notify = POSTFIX_Notify + GLOBALCONST.EXT_ASSETBUNDLE;

    public const string LABEL_FRIEND_LIST = "好友列表";
    public const string LABEL_DUNGEON_LIST = "副本列表";
    public const string LABEL_FRIEND_INVATE = "Invite";
	public const string BtnSure = "確定";
	public const string BtnNext = "下一句";
	public const string BtnNextStep = "下一步";
	public const string BtnPrevStep = "上一步";
	public const string BtnLeave ="離開";
	public const string BtnSkip = "略過";
	public const string BtnBack = "返回";
	public const string NO_SELECTION = "請選擇選項";
	public const string NO_SELECTION_NEXT = "請選擇選項，然後點選{0}按鈕";

	public static readonly string[] SexTypeName = new string []
	{
		"男",
		"女",
	};
	public static readonly string[] BBodyTypeName = new string []
	{
		"男(中)",
		"男(大)",
	};
	public static readonly string[] GBodyTypeName = new string []
	{
		"女(中)",
		"女(小)",
	};
	public static readonly string[] CBFaceName = new string []
	{
		"冷酷型",
		"陽光型",
		"威武型",
		"殺氣型",
		"俊秀型",
		"穩重型"
	};
	public static readonly string[] CGFaceName = new string []
	{
		"清純型",
		"嫵媚型",
		"憂鬱型",
		"伶俐型",
		"優雅型",
		"穩重型"
	};
	public static readonly string[] CBHairName = new string []
	{
		"刺蝟頭",
		"短波浪髮",
		"長波浪髮",
		"短髮",
		"長束髮",
		"中長髮",
		"怪頭",
		"尖劉海",
		"尖劉海抓頭髮",
		"光頭"
	};
	public static readonly string[] CGHairName = new string []
	{
		"短髮",
		"中長髮",
		"長髮",
		"長瀏海",
		"馬尾",
		"公主頭",
		"雙辮頭",
		"大波浪",
		"長直髮",
		"雙馬尾"
	};
	public static readonly string[] CBUnderWearName = new string []
	{
		"內衣1",
		"內衣2",
		"內衣3",
		"內衣4",
		"內衣5"
	};
	public static readonly string[] CGUnderWearName = new string []
	{
		"內衣1",
		"內衣2",
		"內衣3",
		"內衣4",
		"內衣5"
	};
	public static readonly string[] CSkinName = new string []
	{
		"一般",
		"白皙",
		"朱紅",
		"古銅",
		"鐵灰色",
		"紫色"
	};
	public static readonly string[] CPIName = new string []
	{
		"頭髮:",//頭髮
		"面具:",//面具
		"臉型:",//臉型
		"ube:",//預留
		"上衣:",//上衣
		"手套:",//手套
		"ube:",//預留
		"褲子:",//褲子
		"ube:",//預留
		"ube:",//預留
		"披風:",//披風
		"ube:"//預留
	};
	public static readonly string[] CCreateCharacterSelectName = new string []
	{
		"角色名稱:",
		"性別:",
		"體型:",
		"臉部:",
		"髮型:",
		"膚色:",
		"套裝:"
	};
	public static readonly string[] npcPIName = new string []
	{
		"套裝:",//套裝
		"皮膚:",//皮膚
		"材質:",//材質
		"頭部:",//頭髮
		"上衣:",//上衣
		"護腕:",//護腕
		"鞋子:",//鞋子
		"披風:",//披風
		"背甲:",//背甲
		"面具:"//面具

	};
	
	public static readonly string[] CCreateNPCSelectName = new string []
	{
		"性別:",
		"體型:",
		"臉型:",
		"鬍子:",
		"眼珠:",
		"NPC:",
		"ube:"//預留
	};
	
	//斷線訊息
	public const string AccountLockedMsg = "[[[size=14]]]此帳號已被鎖，請帳號持有者透過客服專線：(02)2652-2381或官方網站線上客服了解鎖帳原因。";
	public const string NETWORK_BROKENMSG_100 = "與伺服器連線中斷";
	public const string NETWORK_BROKENMSG_101 = "連線卻不登入";
	public const string NETWORK_BROKENMSG_102 = "檔案檢查失敗";
	public const string NETWORK_BROKENMSG_103 = "檔案檢查無回應";
	public const string NETWORK_BROKENMSG_104 = "線路檢查無回應";
	public static readonly string[] NETWORK_BROKENMSG_001 = new string[]
	{
		"",	//0
		"3次密碼錯誤","重複登入","版本錯誤","檔案錯誤","帳號被鎖","Save檔人數已滿","已登入其他伺服器","SaveIndex已滿","網路接收錯誤",	//1..9
		"傳送buf overflow","鎖IP","連線已滿","chksum錯誤","儲存packet錯誤","未先觸發事件","重複觸發事件","Hold中觸發事件","接收莫名事件協定",	//10..18
		"MapNpc流水號錯誤","DataServer斷線","命令太頻繁","檔案檢查不對","檔案檢查數量不對","線路檢查碼錯誤","玩家正常斷線","money重複登入",	//19..26
		"點數不夠","不合法帳號","Block流水號錯誤","角色暫時被鎖","角色不存在","觸發NPC,距離太遠","移動速度異常","不正常離線","觸發地塊超出範圍",	//27..35
		"更換分流","不能隨意登入組織戰分流","被GM斷線","被GM改名字斷線","移動數度過快","不合法移動","回伺服器選單","此帳號要OB才能使用",	//36..43
		"被自己的同一帳號強登斷線","可以安心的再重登一次","目前伺服器擁擠中,請稍後再登入","IP不合法","移民斷線,請重登新伺服器",	//44..48
		"玩家儲值保帳卡成功","與檢驗伺服器中斷連線，請稍後再試","玩家註冊防盜碟成功","離線掛網啟動成功","保帳卡檢查登入的慣用IP不符",	//49..53
		"查無此帳號","連線已滿(幣商)","電話已解鎖,100秒未進入斷線","","有登記電話鎖的帳號,有不同組IP同時登入","非慣用ip","IP不合法",	//54..60
		"時脈異常離開遊戲","一次命令數太多","Ap斷線","大陸版login result","圖形防外掛斷線",	"切換分流","按X正常離線","回角色大廳離線","系統維護離線",	//61..69
		"體驗時間已過，可從伺服器選單或官網，綁定社群帳號，即可繼續享受【亞特蘭提斯Online】","更換分流檢查碼錯誤","創角人數過多，請稍後重登",	//70..72
		"尚未取得封測資格，請密切注意官網消息" //73
	};
	
	public static readonly string[] NETWORK_BROKENMSG_002 = new string[]
	{
		NETWORK_BROKENMSG_100,NETWORK_BROKENMSG_101,NETWORK_BROKENMSG_102,NETWORK_BROKENMSG_103,
		NETWORK_BROKENMSG_104
	};

	//系統相關訊息
	public static readonly string[] StoredResult = new string[]
	{
		"儲卡成功",//0
		"卡片帳號或種類錯誤",
		"失敗，密碼錯誤",
		"失敗，已經用過",
		"已經儲過開卡點",
		"此種卡號只能儲一次",//5
		"保帳卡還有效用,不能再儲",
		"此伺服器非您的儲值限定伺服器",//7
		"會員資料不完整",//8
		"會員狀態不正常",//9
		"",			   //預留擴充儲卡訊息10~19
		"",	"",	"", "", "", "", "", "", "",//11~19
		"龍幣點數不足", //20
		"", //21
		"此帳號被鎖住", //22
		"天空龍無此帳號"//23
	};

	public static readonly string[] AttackFailReason = new string[]
	{
		"",
		"距離太遠",
		"找不到路",
		"追過頭",
		"玩家消失",
		"玩家狀態異常或npc消失",
		"玩家場景不同"
	};

	public static readonly string[] PKChoice = new string[]
	{
		"",
		"1.是否PK(1)[0.不pk  1.pk]",
		"2.pk對象迴避+幫會(1) +聯會(1) +們派(1) +勢利(1)[0.不迴避 1.迴避]",
		"3. 關閉pk+倒數分鐘數(1)"
	};

	public static readonly string[] PhoneLockMsg = new string[]
	{
		"此人沒有設定電話鎖,可以進入遊戲",
		"請使用您綁定的電話號碼進行解鎖",
		"已經解鎖,可以進入遊戲"
	};
	public static readonly string[] PkMark = new string[]
	{
		"PK",
		"和平"
	};
	public static readonly string[]	StoreStatus = new string[]
	{
		"不開放",
		"開放"
	};

	public static readonly string[] FlyRideLimit = new string[]
	{
		"注意！上方屬皇家禁航領域，無法再提升飛行高度！",
		"座騎飛行能力已達極限，無法再提升高度",
		"此場景無法使用飛行功能"
	};

	public const string CantMoveToThere = "無法移動至目的地";
	public const string PleaseUseKeyBoard = "請使用鍵盤【WASD鍵】及【空白鍵】進行移動";
	public const string CantMoveWhenUseSteamGear = "蒸汽齒輪啟動中，無法進行角色移動";

	public const string TotemNumLimit = "場景圖騰數量已滿";

    public const string SystemException_Argument_ElementReadonly = "This element is readonly.";
    public const string SystemException_Argument_NoMarshalAttribute = "The array field has no marshal attribute.";
    public const string SystemException_Argument_QueueIsFull = "The queue is full.";

    public const string GameObjectTag_Main = "Main";

    public const string StringTag_Protocol_Http = "http://";
    public const string StringTag_Protocol_File = "file:///";
    public const string StringTag_StringLength = "_LEN";
    public const string StringTag_DataKind_Unity3d = "unity3d";

    //	public const string WebServer_Domain_Root = StringTag_Protocol_Http + "127.0.0.1"; //"http://127.0.0.1"
    public const string WebServer_Domain_Root = StringTag_Protocol_Http + "192.168.33.136";
    public const string STANDALONE_Domain_Root = StringTag_Protocol_File;

    public const string WebServer_FolderName_Root = "UnityWebServer";
    public const string WebServer_FolderName_List = "List";

    public const string WebServer_Address_Root = WebServer_Domain_Root + "/" + WebServer_FolderName_Root; //"http://127.0.0.1/UnityWebServer"
    public const string WebServer_Address_List = WebServer_Address_Root + "/" + WebServer_FolderName_List; //"http://127.0.0.1/UnityWebServer/List"

    public const string WebServer_DataFileName_List = "List.txt";

    public const string WebServer_URL_List = WebServer_Address_List + "/" + WebServer_DataFileName_List; //"http://127.0.0.1/UnityWebServer/List/List.txt"

    public const string NET_CONNECT_FAILED = "連線失敗";
	public const string NET_CONNECTING = "連線中                    ";
	public const string NET_CONNECTING_WAIT = "連線中 (已等待 {0}秒)";
    public const string NET_DISCONNECT = "連線中斷";
	public const string NET_RECONNECT = "連線中斷，遊戲將關閉";
	public const string NET_CONNECT_TRY = "連線失敗，嘗試重新連線？";
	public const string NET_CONNECT_EXCEPTION = "目前無法登入";
#if UNITY_STANDALONE_WIN // pc版
	public const string NET_DOWNLOAD_ERROR = "看來是破壞神在搗亂，請關閉遊戲，重新執行遊戲主程式 (No.{0})"; // fs 12/11/17 下載失敗時的回應訊息
#else
	public const string NET_DOWNLOAD_ERROR = "看來是破壞神在搗亂，請前往遊戲官方網站，重新登入遊戲 (No.{0})"; // fs 12/11/17 下載失敗時的回應訊息
#endif
	public const string DOWNLOAD_REMAIN = "讀取中…剩餘檔案數 {0}";
	public const string DOWNLOAD_COUNTING = "          檔案處理中...      ";
	public const string DOWNLOAD_INIT = "            初始化中…            ";
	public const string FTP_ERROR_MSG = "遊戲無法直接啟動，\n請開啟更新程式ATUpdate.exe";
	public const string FTP_ERROR_TITLE = "警告訊息";
	public const string FTP_ERROR_CLOSE = "關閉";

	public const string GAME_APP_CLOSE = "是否離開遊戲？";
	public const string GAME_APP_CLOSE_BATTLE = "戰鬥中下線，\n角色保留十秒，\n確定要離開遊戲？";
	public const string GAME_LOW_FPS_WARNING = "繪圖效能不足，是否要自動調降繪圖效能需求？";
    
    public const string DataFileName_StoreSaveData = "Treasure.unity3d";
    public const string DataFileName_TestData1 = "TreasureTest1.unity3d";
    public const string DataFileName_TestData2 = "TreasureTest2.unity3d";
    public const string DataFileName_TestData3 = "TreasureTest3.unity3d";
    public const string DataFileName_TestData4 = "TreasureTest4AAA.unity3d";
    public const string DataFileName_TestData5 = "TreasureTest5.unity3d";
    public const string DataFileName_TestData6 = "TreasureTest6.unity3d";
    public const string DataFileName_TestData7 = "TreasureTest7.unity3d";
    public const string DataFileName_TestData8 = "TreasureTest8.unity3d";
    public const string DataFileName_TestData9 = "TreasureTest9.unity3d";
    public static readonly string[] DataFileName_Array = new string[] { DataFileName_StoreSaveData, //0
																		DataFileName_TestData1, //1
																		DataFileName_TestData2, //2
																		DataFileName_TestData3, //3
																		DataFileName_TestData4, //4
																		DataFileName_TestData5, //5
																		DataFileName_TestData6, //6
																		DataFileName_TestData7, //7
																		DataFileName_TestData8, //8
																		DataFileName_TestData9 }; //9
	
		
#region 中地圖提示
	public static string MidMapTip_01 = "銀行 郵局";
	public static string MidMapTip_02 = "技能導師";
	public static string MidMapTip_03 = "世界地圖";
	public static string MidMapTip_04 = "簡易地圖";//切成半透明地圖
	public static string MidMapTip_05 = "關閉";//關閉窗體
	public static string MidMapTip_06 = "一般地圖";
	public static string MidMapTip_07 = "移動介面";
#endregion
	
#region 世界地圖提示	
	public static string WorldMapTip_01 = "中地圖";
	public static string WorldMapTip_02 = "[[[align=center size=14]]]傳送至|[[[size=15]]]%g[{0}][[[color=255,0,0]]]LV{1}~LV{2}|[[[size=14]]]%w需要消耗銀幣 %y{3} %w元";
	public static string[] CantDeliverTip = new string[]
	{
		"[[[align=center]]]該場景不提供傳送",
		"[[[align=center]]][[[color=255,0,0]]]金額不足，無法傳送|%w(此次傳送需要{0}元)",//"[[[align=center]]]金額不足，無法傳送",
		"[[[align=center]]]尚未取得進入資格，|無法傳送",
	};
	public static string InBattleCantDeliver = "戰鬥中無法傳送";
	public static string InEventCantDeliver  = "事件中無法傳送";
#endregion
	
#region 事件獎勵相關
	public const string EventRewardExp = "獲得 {0} 經驗";
	public const string EventRewardHonor = "獲得榮耀值 {0}";
	public const string EventRewardSilver = "獲得 {0} 銀幣";
	public const string EventDecSilver = "扣除 {0} 銀幣";
#endregion
	
#region 頻道相關錯誤訊息
	public static string[] ChannelErrorMsgs = new string[]
	{
		"Mp不足，無法說話",
		"無組織，無法發組頻", 
		"好友人數錯誤", 
		"無隊伍，無法發隊頻", 
		"無此角色或該角色目前不在遊戲中", 
		"目前無GM在線上", 
		"你已被禁言",
	};
	public static string[] ChannelHint = new string[]
	{
		"未上線",//0
		"查無此人",//1
		"尚未加入隊伍",//2
		"尚未加入領域。16級之後，請清出背包空間，並使用道具「通訊儀」加入",//3
		"不能發送密語給自己",//4
		"字數過長",//5
		"發話過於頻繁",//6
		"世界頻道發話每句1000銀幣，是否花費？",//7
		"尚無好友",//8
		"冒險團功能近期開放，敬請期待",//9 尚未加入冒險團
		"單句特殊表情符號使用已達上限(3種)",//10
		"世界頻道發話每句扣除「磁歐擴音器」×1，是否使用？",//11
	};
#endregion
}

#region 表定字串
public class TableStr
{
  //物品的裝備位置
  public const string EquipPos_01="武器";
  public const string EquipPos_02="頭飾";
  public const string EquipPos_03="手套";
  public const string EquipPos_04="上衣";
	public const string EquipPos_05 = "下衣";
	public const string EquipPos_06 = "披風";
  public const string EquipPos_07="面具";
  public const string EquipPos_10="項鍊";
  public const string EquipPos_11="戒指";
  public const string EquipPos_12="護身符";
  public const string EquipPos_13="造型武器";
  public const string EquipPos_14="造型頭飾";
  public const string EquipPos_15="造型手套";
  public const string EquipPos_16="造型上衣";
	public const string EquipPos_17 = "造型下衣";
	public const string EquipPos_18 = "造型披風";
  public const string EquipPos_19="造型面具";

  //物品的裝備效果
  public const string EquipEff_001="力量";
  public const string EquipEff_002="敏捷";
  public const string EquipEff_003="體力";
  public const string EquipEff_004="智力";
  public const string EquipEff_005="精神";
  public const string EquipEff_006="幸運值";
  public const string EquipEff_007="全屬性";
  public const string EquipEff_008="生命";
  public const string EquipEff_009="生命上限";
  public const string EquipEff_010="生命回復";
  public const string EquipEff_011="癒合力";
  public const string EquipEff_012="魔力";
  public const string EquipEff_013="魔力上限";
  public const string EquipEff_014="魔力回復";
  public const string EquipEff_015="調和力";
  public const string EquipEff_016="攻擊速度";
  public const string EquipEff_017="詠唱速度";
  public const string EquipEff_018="移動速度";
  public const string EquipEff_020="閃避";
  public const string EquipEff_021="命中";
  public const string EquipEff_022="物理爆擊";
  public const string EquipEff_023="韌性";
  public const string EquipEff_024="格擋";
  public const string EquipEff_026="穿透";
  public const string EquipEff_029="法術爆擊";
  public const string EquipEff_030="招架";
  public const string EquipEff_032="破壞";
  public const string EquipEff_033="防禦力";
  public const string EquipEff_034="地抗性";
  public const string EquipEff_035="水抗性";
  public const string EquipEff_036="火抗性";
  public const string EquipEff_037="風抗性";
  public const string EquipEff_038="光抗性";
  public const string EquipEff_039="暗抗性";
  public const string EquipEff_040="全抗性";
  public const string EquipEff_041="攻擊強度";
  public const string EquipEff_043="攻擊強度與法術傷害";
  public const string EquipEff_044="法術傷害";
  public const string EquipEff_049="治療量";
  public const string EquipEff_051="普攻攻擊距離";
  public const string EquipEff_052="技能攻擊距離";
  public const string EquipEff_053="PK傷害減免";
  public const string EquipEff_054="經驗值";

  //水晶抗性效果
  public static string[] CrystalResistEff =
  {
    "全抗性",  //元素種類起始0
    "地抗性",
    "水抗性",
    "火抗性",
    "風抗性",
    "光抗性",
    "闇抗性",
    "地水抗性",
    "地火抗性",
    "地風抗性",
    "地光抗性",
    "地闇抗性",
    "水火抗性",
    "水風抗性",
    "水光抗性",
    "水闇抗性",
    "火風抗性",
    "火光抗性",
    "火闇抗性",
    "風光抗性",
    "風闇抗性",
    "光闇抗性",
  };
}
#endregion


//UI 常數字串
public static class Const
{
	public static readonly string[] Str_00006 = new string[7]
	{ "角色資訊", "裝備", "屬性", "職業：{0}", "角色等級：{0}","{0} 的資訊","裝備積分：{0}" };
  public static readonly string[,] Str_00007 = new string[6,2]
  {
    {"攻擊強度：", "{0}"},
    {"法術傷害：", "{0}"},
    {"物理爆擊：", "{0}%"},
    {"法術爆擊：", "{0}%"},
    {"攻擊速度：", "{0}%"},
    {"詠唱速度：", "{0}%"}
  };
  public static readonly string[] Str_00008 = new string[4]
  {
    "技能", "戰鬥", "支援", "亞特蘭提斯之力：{0}/{1}"
  };
  public static readonly string[] Str_00009 = new string[5]
  {
    "[[[color=<replace>]]]", "[[[color=<replace>]]]", "[[[color=<replace>]]]", "[[[color=<replace>]]]","[[[color=<replace>]]]"
  };

  public static readonly string[] Str_00013 = new string[4] { "重整", "提領銀幣", "銀幣上限擴充", "銀行上限擴充"};		
  public static readonly string[] STR_CONN_INFO = new string[7] { "", "密碼錯誤" , "目前無法登入" , "登入中，請稍待",  "目前伺服器人數已滿" , "重覆登入" ,  "帳號錯誤"};		
	public static readonly string[] STR_AC_PW_ERROR = new string[2] {"帳號長度過短，請重新輸入", "密碼長度過短，請重新輸入"};

	#region 主介面hint
	public static readonly string[] Str_InfoBarHint = new string[7]
	{
		"%v【能量】|%y文明之塔%w內進行%j科技研究|%w需消耗能量|每經過%v{0}%w分鐘會回復%v{1}%w點",
		"%o【權力點】|%y騎士團%w內進行%j派遣部下|%w與%j加速完成回報%w需消耗權力點|每經過%o{0}%w分鐘會回復%o{1}%w點|進入限制：%o等級{2}級",
		"%j【訓練時間】|%y訓練時間介面領取%w（最高上限%j3小時%w）|可獲得：|%f打怪經驗效益：{0:P0} Exp|打怪銀幣效益：{1:P0} Silver|%w訓練時間為0時，效益回復為%y100％",
		"【銀幣】|使用銀幣可進行：|%y星盤%w（技能學習）|%y裝備強化星儀%w（裝備強化）|%y裝備修復",
		"%y【金幣】|%w經由%f儲值%w可獲得%y金幣|%w透過%f活動%w可獲得%y獎勵金幣%w|皆可於遊戲內進行%j介面消費%w|購買%j購物中心%w或%j魔法衣櫥%w的商品",
		"[[[color=255,244,92]]]【名氣值】|%y拓建文明之塔%w與%y習得更高階層的技能|%w可提升名氣值，名氣值愈高表示你在|亞特蘭提斯更為有名||再提升%g{0}%w點名氣值就可獲得下階段獎勵喔！",
		"%l【獎勵能量】 |%w每天你有5點獎勵能量可以協助好友研究工具，協助研究時你可以獲得材料、銀幣"
	};
	/// <summary>
	/// 0"能量轉換",
	///	1"權力點轉換",
	///	2"訓練時間",
	///	3"銀幣轉換",
	///	4"儲值",
	///	5"金銀幣切換"
	///	6"迷你HP/MP顯示"
	///	7"關閉顯示"
	/// </summary>
	public static readonly string[] Str_SimpleHintStr = new string[8]
	{
		"能量轉換",
		"權力點轉換",
		"訓練時間",
		"銀幣轉換",
		"儲值",
		"金銀幣切換",
		"迷你HP/MP顯示",
		"關閉顯示"
	};
	public static readonly string[] Str_InfoIconHint = new string[8]{
		"提示功能櫃(F)",
		"角色（C）",
		"背包（B）",
		"星盤（K）",
		"社群（G）",
		"購物中心（X）",
		"系統（N）",
		"魔法衣櫥（U）",
	};
	public static readonly string[] Str_MinMapBtnHint = new string[3]{
		"全螢幕切換（F10）",
		"區域地圖（M）",
		"分區選擇",
	};
	public const string Str_BuffHint = "[[[align=center]]]{0}[[[.]]] {2} [[[.]]] 剩餘時間{1:0}秒";
	public static readonly string[] Str_ChatBtn = new string[5] { "視窗範圍", "視窗模式", "清空","頻道開關設定","頻道不隱藏" };
	public static readonly string[] Str_ChatFunctionBtn = new string[6] { "密語", "加入好友", "加入黑名單", "邀請組隊", "玩家資訊", "取消" };

	public const string Str_CastingInfo = "%r{0}　%w施展　%y{1}";
	
	public const string Str_AutoEvtHint = "評定重整：|搜尋是否有可接或可回報的評定";
	
	public const string Str_OKAutoEvtHint = "重整完成";
	public const string Str_CloseAutoEvent = "蒸氣齒輪開啟中，無法搜尋評定";
	public const string Str_AutoEventClickNPC = "請先關閉蒸氣齒輪後再進行";
	#endregion
	#region 狀態hint
	public static readonly string[] Str_BuffCancelButton = new string[3]{
														"取消","開始","暫停"};
	public static readonly string[] Str_BufferHint = new string[5]{
		"[[[align=center]]]%y{0}%w[[[.]]] {1}",
		"[[[.]]]目前次數：{0}",
		"[[[.]]]%y剩餘時間：%w{0:0天;;}{1:0時;;}{2:0分;;}{3:0秒;;}",
		"[[[.]]]右鍵{0}/取消",
		"[[[.]]]右鍵取消",
	};
	#endregion
	#region 商店商城介面
	/// <summary>
	/// 0"商城", 
	///	1"商店", 
	///	2"購買", 
	///	3"販賣", 
	///	4"總價:",	 	
	///	<para>
	///	5"請滑鼠雙擊欲販售的物品或直接拖曳至左方欄位", 
	///	6"前往官網", 
	///	7"＜優惠快訊＞", 
	///	8"活動快訊", 
	///	9"確定",
	///	</para>
	///	<para>
	///	10"取消",
	///	11"穿",
	///	12"已擁有",
	///	13"魔法衣櫥",
	///	14"已有此物品",
	///	</para>
	///	<para>
	///	15"是否賣回此物品|獲得{0}金幣",
	///	16"金幣",
	///	17"銀幣",
	///	18"{0}/{1}/{2} PM12 下架"
	/// 19"脫"
	///	</para>
	///	<para>
	///	20"是否要購買以下商品",
	///	21"換裝中",
	///	22"修理",
	///	23"儲值",
	///	24"購買預覽中商品",	
	///	</para>
	/// <para>
	/// 25"總金額：",
	/// 26"%j【{0}】"//詢問購買商品時的文字顏色
	/// 27"VIP等級未達購買權限，需VIP {0}星方可購買",
	/// 28"活動時間： "
	/// 29"確認購買",
	///	</para>
	/// <para>
	/// 30"購買數量",
	/// 31"此物品已下架",
	/// 32"是否要出售以下商品
	///	</para>
	/// </summary>
	public static readonly string[] Str_ShopMall_String = new string[]
	{
		"購物中心",
		"商店",
		"購買",
		"販賣",
		"總價:",		
		"1、左鍵雙擊可將物品移至販賣區\n2、右鍵單擊可將全數物品移至販賣區\n3、亦可直接拖曳至販賣區單格內",
		"前往官網",
		"＜優惠快訊＞",
		"活動快訊",
		"確定",
		"取消",
		"穿",
		"已擁有",
		"魔法衣櫥",
		"已有此物品",
		"是否賣回此物品|獲得{0}{1}",
		"金幣",
		"銀幣",
		"[[[size=14]]]{0}/{1}/{2} 下架",
		"脫",
		"是否要購買以下物品：",
		"換裝中",
		"商店修裝",
		"儲值",
		"購買預覽中商品",
		"總金額：",
		"%l【{0}】",//詢問購買商品時的文字顏色
		"VIP等級未達購買權限，需VIP {0}星方可購買",
		"%y活動時間： %w",
		"確認購買",
		"購買數量",
		"此物品已下架",
		"%r是否要出售以下貴重物品：",
	};
	public static readonly string[] Str_ShopMall_RepairString = new string[]
	{
		"[[[align=center]]]{0}需消耗{1}銀幣",
		"修理全部",
		"修理背包",
		"修理裝備",
		"取消",
		"目前沒有需要修復的物品",
	};
	public static readonly string[] Str_ShopMall_TabString = new string[]{
		"新品",
		"VIP",
		"試衣鏡",
		"祝福",
	};
	public static readonly string[] Str_ShopMall_EventTypeString = new string[]{
		"儲值優惠",
		"耗點活動",
		"VIP活動",
		"超值特惠商城活動",
	};
	public const string Str_Wish_Info = "[[[color=221,221,221]]]亞特的子民們~為了協助新亞特的復甦，設計女神‧奧蘿拉來到人間，貢獻己長，設計出與眾不同、注有神力的各式物件~擁有設計女神‧奧蘿拉每週精心設計的特殊物件，則每日可領取一次奧蘿拉的祝福哦~";
	public const string Str_Wish_TimeFormat = "[[[color=255,214,91]]]<領取時間>[[[color=255,145,193]]]{0}/{1}/{2}維護後~{3}/{4}/{5}維護前[[[color=255,214,91]]]；每日中午12：00可重新領取";
	public static readonly string[] Str_Wish_Text = new string[]
	{
		"領取祝福", //0
		"已領取",
		"重置",
		"虛寶",
		"本週奧羅拉物件",
		"★{0}物件",//5
		"[[[paraGap=2 color=243,151,0]]]頭、衣、手、褲、面飾、背甲",
		"[[[paraGap=2 color=243,151,0]]]武器、其他",
		"有機會獲得獎勵金幣或銀幣|%y(若銀幣空間不足，超出之獎勵將無法獲得)",
		"有機會獲得神秘禮物。",
		"有機會獲得{0}%~{1}%的經驗值加成效果(時間6小時)。", //10
		"有機會獲得全屬性提升{0}~{1}效果(時間6小時)。",
		"擁有一件本週奧蘿拉銀幣物件可領取。|%y(若銀幣空間不足，超出之獎勵將無法獲得)",
		"擁有{0}件本週奧蘿拉金幣物件可領取。",
		"狀態跨每日中午12:00，即無法重置需重新領取。",
	};
	#endregion
	#region 寶物交易
	public static readonly string[] Str_TradeSuccess =new string[2]{ "成功購買","成功購買:{0} 數量:{1}"};
	public static readonly string[] Str_TradeError = new string[13] {
		"金幣不足",
		"身上銀幣不足",
		"未販賣此物品",
		"此物品已售完",
		"此伺服器非您的消費限定伺服器，如需轉換，請洽客服專員",
		"限時商品,要在限時時間內才能購買",
		"購買數量超過上限",
		"超過物品堆疊上限",
		"暫停販售",
		"其他原因失敗",
		"背包空間不足",
		"不可交易金幣不足",
		"交易伺服器目前關閉中,請稍候",
	};
	public const string Str_SellSuccess = "成功販賣";
	public static readonly string[] Str_SellResult = new string[7] {
		"成功販賣:{0} 數量:{1}",
		"失敗",
		"身上物品數量不足",
		"商店空間不足",
		"身上金錢空間不足",
		"此物品無法賣給Npc",
		"販賣總類過多",
	};

	#endregion

	#region 施放結果
	//本地施放判斷結果
	public static readonly string[] Str_CastingCheck = new string[15]
	{
		"成功",
		"冷卻中無法施展",
		"生命不足",
		"魔力不足",
		"目標太遠",
		"目標太近",
		"目標太遠",
		"目標類型錯誤",
		"無法施法",
		"正在施法",
		"不可穿越障礙點",
		"不可施放被動",
		"場景限制",
		"沒有選擇地板位置",
		"騎乘座騎中，無法使用技能"
  };

	//server回傳施放結果
	public static readonly string[] Str_CastFailResult = new string[50]
	{
	    "",
	    "目標太遠",//1
	    "目標狀態異常",//2
	    "場景不同",//3
	    "生命不足",//4
	    "魔力不足",//5
	    "SP不足",//6
	    "等級不足",//7
	    "技能施展中",//8
	    "尚未習得此技能",//9
	    "連續使用同一技能",//10
	    "需指定目標",//11
	    "範圍內無施展目標",//12
	    "冷卻中無法施展",//13
	    "需騎乘中才可使用",//14
	    "無法對機關使用",//15
	    "戰鬥中無法使用",//16
	    "不可對玩家角色使用",//17
	    "此場景無法使用",//18
	    "此地點無法使用",//19
	    "對象未成年",//20
	    "對象未開pk",//21
	    "pk狀態不同",//22
	    "超出高度",//23
	    "對象場景無法到達",//24
	    "無潛行狀態無法使用",//25
	    "不在線上(26)",//26
	    "不足(27)",//27
	    "不同場景(28)",//28
	    "目標不正確(29)",//29
	    "今日已無法使用(30)",//30
	    "領域職業不符無法使用",//31
	    "目標目前無法獲得此狀態",//32
	    "地形阻礙施展失敗",//33
	    "目標條件不符",//34
	    "目標太近",//35
	    "對方生命需低於25%才能使用",//36
	    "生命需低於50%才能使用",//37
	    "不符(38)",//38
	    "裝備武器不符",//39
	    "階段不足(40)",//40
	    "封印(41)",//41
	    "星數不足",//42
	    "錯誤(43)",//43
	    "不足(44)",//44
	    "不足(45)",//45
	    "道具不足",//46
	    "金錢不足",//47
	    "寵物未出戰無法使用",//48
	    "寵物生命不足",//49
	};
	#endregion
	
#region 創角介面
  public static readonly string LittleCatName = "飛喵亞斯蘭";	
  public static readonly string ArtefactHintText = "<請點選神器觀看詳細資訊>";	
  public static readonly string[] ItemComfirmText = new string[2] {"ComfirmDialog","是否確定置換?"};
  public static readonly string[] NameCheckingStr = new string[4] { "名稱已使用", "名稱不合法","中文名稱不得超過7個字", "請輸入角色名稱"};	
  public static readonly string[] ServerResultStr = new string[3] { "伺服器遊戲人數已達上限", "此位置的角色已創過","此為特殊帳號,無法創第2角色"};	
	
  public static readonly string[] MainButString = new string[3] { "創角完成", "返回", "隨機創角" };
  public static readonly string[] GenerderTitleString = new string[3] { "性別", "男生", "女生" };	
  public static readonly string[] BodyTitleString = new string[4] { "體型", "體型選擇", "內衣", "膚色"};	
  public static readonly string[] FaceTitleString = new string[3] { "臉部", "臉型選擇", "瞳色"};
  public static readonly string[] HairTitleString = new string[3] { "頭部", "髮型選擇", "髮色相關"};	
  public static readonly string[] SalonTitleString = new string[2] { "染髮中...", "美體美容室"};
  public static readonly string[] EndingMessage = new string[3] 
	{ 
		"恭喜創角成功，取得封測資格",
		"期待11/21亞特蘭提斯裡再相會。\n記得前來領取超值的[先鋒寶袋]喔!",
		"完成活動，回到官網"
	};		
  
	public static readonly string[] LittleCatMotion = new string[6] {"idle01", "gather01", "rest01", "run01", "ready01", "walk01"};
	
    public static readonly string[] CreateCharacterDialogue1 = new string[2] 
	{ 
		"[[[align=center]]]唔？本喵怎麼醒來了？[[[.]]]難道是[[[color=255,255,0]]]破壞神降臨%w了嗎？", 
		"[[[align=center]]]這裡是誰的意識之海？[[[.]]][[[color=255,255,0]]]亞特蘭提斯的子民啊！[[[.]]]%w可以告訴本喵你是誰嗎？"
	};	
	
	public static readonly string[] CreateCharacterDialogue2 = new string[1] 
	{ 
		"[[[align=center]]]挑一件順眼的[[[color=255,255,0]]]神器！[[[.]]]%w本喵與它將伴隨你踏上[[[.]]]消滅破壞神的勇者之路！"
	};	
	
	public static readonly string[] CreateCharacterDialogue3 = new string[1] 
	{ 
		"[[[align=center]]]與本喵一同阻止破壞神毀滅世界吧！"
	};	
	
	public static readonly string AskNameInput = "勇者啊，請問你叫什麼名字";
	
	public static readonly string[] ArtefactNames = new string[6] 
	{ 
		"[[[align=center]]][[[color=255,255,0]]]《黑貓陶罐》", 
		"[[[align=center]]][[[color=255,255,0]]]《聖騎士的號角》", 
		"[[[align=center]]][[[color=255,255,0]]]《染血的巨劍》", 
		"[[[align=center]]][[[color=255,255,0]]]《蘊含時光之盒》", 
		"[[[align=center]]][[[color=255,255,0]]]《御匠之錘》",
		"[[[align=center]]][[[color=255,255,0]]]《飛行之羽》"
	};
	
	public static readonly string[] ArtefactInfo = new string[6] 
	{ 
		"[[[align=center]]]搖晃陶罐！裡頭住著神祕的黑貓！[[[.]]]黑貓可替你%l偷取敵人身上的財物，[[[.]]]%w同時你的%l閃避、格檔、招架將大幅提升！[[[.]]][[[.]]][[[color=77,255,0]]]闇抗性永久提升10點", 
		"[[[align=center]]]吹響勝利的號角！[[[.]]]你%l騎士團%w的騎士將%l恢復所有行動能力，%w[[[.]]]且派遣中的騎士將%l瞬間達成任務！[[[.]]][[[.]]][[[color=77,255,0]]]水抗性永久提升10點", 
		"[[[align=center]]]渴求敵人的血液嗎？[[[.]]]拔出巨劍！魔力充斥全身！[[[.]]]%l攻擊強度、法術傷害、物理爆擊、法術爆擊[[[.]]]全部大幅提高！[[[.]]][[[.]]][[[color=77,255,0]]]火抗性永久提升10點",
		"[[[align=center]]]開啟時光之盒，將使時光逆轉！[[[.]]]%l你和戰友們%w的%l生命與魔力瞬間恢復至100%，[[[.]]]%w還能額外獲得%l強力防護盾！[[[.]]][[[.]]][[[color=77,255,0]]]光抗性永久提升10點", 
		"[[[align=center]]]敲擊大師的傳奇之錘！[[[.]]]生產將%l100%取得材料%w，且%l產物將是兩倍，[[[.]]]%w還可得到%l一份珍貴的配方！[[[.]]][[[.]]][[[color=77,255,0]]]地抗性永久提升10點",
		"[[[align=center]]]展開天神的羽翼－神奇的飛行之羽！[[[.]]]你可以在天際間自由地%l翱翔飛行！[[[.]]]移動速度將大幅增加！[[[.]]][[[.]]][[[color=77,255,0]]]風抗性將永久提升10點"
	};
	
	public static readonly string[] ArtefactDescription = new string[6] 
	{ 
		"[[[align=center]]]如果你熱愛精彩多變的生活，[[[.]]]黑貓陶罐將讓你事事充滿驚奇喵！[[[.]]][[[color=255,255,0]]]你確定選擇黑貓陶罐嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)", 
		"[[[align=center]]]如果你自認是統御型的人才！[[[.]]]吹響號角，你的騎士團會更加強大！[[[.]]][[[color=255,255,0]]]你確定選擇《聖騎士的號角》嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)", 
		"[[[align=center]]]如果你渴望以力量震撼世界！[[[.]]]持有巨劍，你將擁有強悍的戰鬥威力！[[[.]]][[[color=255,255,0]]]你確定選擇染血的巨劍嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)", 
		"[[[align=center]]]如果你看重同伴的生命勝過一切！[[[.]]]時光之盒，將使你成為隊友的堅強後盾！[[[.]]][[[color=255,255,0]]]你確定選擇蘊含時光之盒嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)", 
		"[[[align=center]]]你喜歡創造勝過戰鬥嗎？[[[.]]]它將讓你在發明大師的道路上更進一步！[[[.]]][[[color=255,255,0]]]你確定選擇御匠之錘嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)",
		"[[[align=center]]]如果你喜歡自由自在地生活！[[[.]]]翱翔天際不再是鳥類的專利！[[[.]]][[[color=255,255,0]]]你確定選擇飛行之羽嗎？[[[.]]][[[.]]]%r(等級十四級時，可重新選擇一次)"
	};
	
#endregion
	
#region Crazy use
  public static readonly string CrzS_00001 = "提示功能櫃";
  public static readonly string[] CrzS_00002 = new string[2] { "動作指令", "功能指令" };
  public static readonly string CrzS_00003 = "強度等級：{0}";
  public static readonly string CrzS_00004 = "招募工具";
  public static readonly string CrzS_00005 = "招募人民所帶來的新工具，進行研究可獲得新的合成素材";
  public static readonly string[] CrzS_00006 = new string[UI_RecruitFM.MAX_TOOLINFO] { "名稱：", "位置：", "需求名氣：" };
  public static readonly string CrzS_00007 = "立即刷新";
  public static readonly string CrzS_00008 = "招募";
  public static readonly string CrzS_00009 = "只要";
  public static readonly string CrzS_00010 = "系統選單";
  public static readonly string[] CrzS_00011 = new string[UI_SystemFM.MAX_BUTTON_NUM] { "系統設定"/*, "返回伺服器選單"*/, "離開遊戲" };
  public static readonly string CrzS_00012 = "系統設定";
  public static readonly string[] CrzS_00013 = new string[3] { "畫面", "設定", "訊息" };
  public static readonly string[] CrzS_00014 = new string[UI_SysOptionFM.MAX_OPTION_PART_NUM] 
  { "Tab切換目標", "移動操作設定", "鏡頭設定", "音量設定" };
  public static readonly string[][] CrzS_00015 = 
  {
		new string[UI_SysOptionFM.MAX_RADIO1_NUM]{"全部選擇", "玩家優先", "只選擇玩家"},
		new string[UI_SysOptionFM.MAX_RADIO2_NUM]{"A模式", "B模式"}
  };
  public static readonly string[] CrzS_00016 = new string[4] { "鏡頭距離", "旋轉速度", "音效音量", "音樂音量" };
  public static readonly string[] CrzS_00017 = new string[UI_SysOptionFM.MAX_BUTTON_NUM] { "返回", "套用", "取消" };
	public static readonly string[][] CrzS_00018 = 
	{
		new string[UI_SysOptionFM.MAX_VISION_CHECK_NUM]
		{"全螢幕","戰鬥光影", "場景特效", "氛圍光源", "光影柔化", "戰鬥畫面震動", "描邊效果", "反鋸齒效果"},
		new string[UI_SysOptionFM.MAX_OPTION_CHECK_NUM]{"音效開關", "音樂開關"},
		new string[UI_SysOptionFM.MAX_MESSAGE_CHECK_NUM]
		{"玩家資訊", "血條數據", "NPC名稱", "個人得寶訊息", "組隊得寶訊息", "好友詢問", "組隊詢問", "星盤提示"}
	};
	public static readonly string CrzS_00019 = "低";
  public static readonly string[] CrzS_00020 = new string[UI_SysOptionFM.MAX_COMBO_NUM] 
  { "顯示配置", "視野範圍", "畫面效果", "影子效果", "水面效果", "解析度" };
  public static readonly string[] CrzS_00022 = new string[3] { "玩家顯示", "顯示設定", "訊息設定" };
  public static readonly string[] CrzS_00023 = new string[4] { "不顯示", "顯示隊友", "顯示好友", "顯示所有玩家" };
  public static readonly string[] CrzS_00024 = new string[7] { "玩家資訊", "血條數據", "NPC名稱", "個人得寶訊息", "組隊得寶訊息", "好友詢問", "組隊詢問" };
  public static readonly string CrzS_00025 = "萬用密碼說明";
  public static readonly string[] CrzS_00026 = new string[2] { "變更密碼", "取消" };
  public static readonly string[] CrzS_00027 = new string[3] 
  { "輸入密碼由鍵盤或是螢幕小鍵盤兩種方式，防止鍵盤側錄器監測，建議使用螢幕小鍵盤的方式輸入", 
    "螢幕小鍵盤會以亂數排列0~9的數字，每次開啟介面時將會自動變更", 
    "萬用密碼可使用於遊戲內的銀行、商店等" 
  };
  public static readonly string CrzS_00028 = "萬用密碼變更介面";
  public static readonly string[] CrzS_00029 = new string[3] { "請輸入萬用密碼", "設定萬用密碼", "再次確認密碼" };
  public static readonly string CrzS_00030 = "分流選單";
  public static readonly string CrzS_00031 = "分流{0:00}";
  public static readonly string CrzS_00032 = "材料櫃";
	public static readonly string[] CrzS_00033 = new string[2] 
	{
		"點擊出售按鈕可賣出多餘的工具與材料",
		"點擊使用按鈕放置工具到你的文明之塔"
	};
  public static readonly string[] CrzS_00034 = new string[2] { "原料", "工具" };
  public static readonly string CrzS_00035 = "頁面{0}/{1}";
  public static readonly string CrzS_00036 = "確定要切換到" + CrzS_00031 + "？";
	public static readonly string CrzS_00037 = "是否離開遊戲？";
  public static readonly string CrzS_00038 = "銀幣已達上限，請先消耗一些";
  public static readonly string CrzS_00039 = "信件";
  public static readonly string[] CrzS_00040 = new string[2] { CrzS_00039, "快遞" };
  public static readonly string[] CrzS_00041 = new string[3] { "新增信件", "全部刪除", "回報客服" };
  public static readonly string[] CrzS_00042 = new string[2] { CrzS_00039, "GM" };
  public static readonly string CrzS_00043 = "日期";
  public static readonly string CrzS_00044 = "標題";
  public static readonly string[,] CrzS_00045 = new string[2, 3]
  {
    {"寄件人",CrzS_00043, CrzS_00044},
    {"收件人",CrzS_00043, CrzS_00044}
  };
  public static readonly string[,] CrzS_00046 = new string[2, 2] {{"回覆", "刪除"}, {"寄出", "取消"}};
  public static readonly string CrzS_00047 = "以%y{0}%w銀幣賣出";
  public static readonly string CrzS_00048 = "報到人數";
  public static readonly string CrzS_00049 = "銀幣數量{0:0%}";
  public static readonly string CrzS_00050 = "活動";
  public static readonly string CrzS_00051 = "這一位是新朋友，獎勵能量將於中午12:00後補滿";
  public static readonly string CrzS_00052 = "獎勵能量已用完";
  public static readonly string CrzS_00053 = "名氣值不足{0}無法使用此工具";
  public static readonly string CrzS_00054 = "寄件成功";
  public static readonly string CrzS_00055 = "{0}不存在於世界上！";
  public static readonly string CrzS_00056 = "{0}的信箱已經滿了，請稍後再做嘗試！";
  public static readonly string CrzS_00057 = "購買銀幣";
  public static readonly string CrzS_00058 = "{0}已滿，請先賣掉一些不需要的東西";
  public static readonly string CrzS_00059 = "招募人民帶來的%g{0}%w需要給予%y{1}%w銀幣的賞金。|是否要招募該工具";
  public static readonly string CrzS_00060 = "購買金幣";
  public static readonly string CrzS_00061 = "親愛的閣下：重金之下必有勇夫，你是否要花費金幣{0}讓人民更快帶來新的工具？";
	public static readonly string[] CrzS_00062 = new string[2] { "出售", "使用" };
  public static readonly string CrzS_00063 = "需要";
  public static readonly string CrzS_00064 = "{0}的文明之塔";
  public static readonly string CrzS_00065 = "文明之塔大廳";
  public static readonly string CrzS_00066 = "放置";
  public static readonly string CrzS_00067 = "立即完成";
  public static readonly string[] CrzS_00068 = new string[2] { "研究", "卸下" };
  public static readonly string[] CrzS_00069 = new string[2] 
  { 
	"擴建文明之塔樓層，可以放置更多的工具", 
	"擴建需要以下條件：" 
  };
  public static readonly string[] CrzS_00070 = new string[4] { "生產系", "製造系", "加工系", "夢幻系" };
  public static readonly string CrzS_00071 = "擴建文明之塔的資金不足！多使用工具可以獲得更多的銀幣！";
  public static readonly string[] CrzS_00072 = new string[2] { CrzS_00057, GlobalConstString.MidMapTip_05 };
  public static readonly string CrzS_00073 = "位階到達{0}，可縮短建設時間10%";
	public static readonly string CrzS_00074 = "增建{0}%w樓層|消耗銀幣%y{1}";
  public static readonly string CrzS_00075 = "%g(因為你的位階，建設時間縮短10%)";
  public static readonly string[] CrzS_00076 = new string[2] 
  { 
	"距離建造完成", 
	"還有　天　小時　分" 
  };
  public static readonly string CrzS_00077 = "{0}F 施工中...";
  public static readonly string CrzS_00078 = "你的名氣值提升了{0}點，在王國裡更為有名了";
	public static readonly string[] CrzS_00079 = new string[2] 
	{ 
		"研究工具的過程中有機率獲得各種材料", 
		"[[[underline]]]可到招募工具招募人民帶來的新工具" 
	};
  public static readonly string[] CrzS_00080 = new string[2] { "工具研究", "解鎖新科技" };
	public static readonly string[] CrzS_00081 = new string[2] { "使用書頁", "解鎖" };
  public static readonly string CrzS_00082 = "獲得：{0}";
  public static readonly string CrzS_00083 = "這個工具要放置在適合場所才可以研究更高深的知識";
  public static readonly string CrzS_00084 = "解鎖新科技需要以下材料";
  public static readonly string CrzS_00085 = "詢問好友";
  public static readonly string CrzS_00086 = "能量已使用完畢，請等候能量恢復！";
  public static readonly string CrzS_00087 = "文明之塔第{0}樓已經建設完畢|這次工人們為你蓋出了{1}";
  public static readonly string CrzS_00088 = "恭喜！你的文明之塔已經到達最高境界！";
  public static readonly string CrzS_00089 = "已達上限";
  public static readonly string CrzS_00090 = "文明之塔已在拓建中";
  public static readonly string CrzS_00091 = "{0}的熟練度已修滿，可以解鎖下一階的技能囉！";
  public static readonly string CrzS_00092 = "點擊可取消修練";
  public static readonly string[] CrzS_00093 = new string[7] 
	{ UnStr_1401, CrzS_00004, "磁歐動力混合機", CrzS_00032, "科技寶典", Str2056_0030, Str_ShopMall_String[0]};
	public static readonly string CrzS_00094 = "裝修樓層";
	public static readonly string CrzS_00095 = "重新裝修這個樓層|需要花費%j{0}%w金";
	public static readonly string CrzS_00096 = "需先將所有工具卸下";
	public static readonly string CrzS_00097 = "協助";
	public static readonly string CrzS_00098 = "Bonus {0}";
	public static readonly string[] CrzS_00099 = new string[2] { "好友資料異常", "拜訪好友太頻繁，請稍候"};
	public static readonly string CrzS_00100 = "已經無法協助更多好友";
	public static readonly string CrzS_00101 = "中等";
	public static readonly string CrzS_00102 = "高";
	public static readonly string[][] CrzS_000103 = new string[UI_SysOptionFM.MAX_COMBO_NUM][]
	{
		new string[4]{"系統預設", "畫面優先", "效能優先", "自行設定"},
		new string[5]{"最近", "近", "適中", "遠", "最遠"},
		new string[5]{"最低", CrzS_00019, CrzS_00101, CrzS_00102, "最高"},
		new string[3]{CrzS_00019, CrzS_00101, "即時"},
		new string[3]{CrzS_00019, "中", CrzS_00102},
		new string[]{""}
	};
	public static readonly string CrzS_00104 = "離開文明之塔";
	public static readonly string CrzS_00105 = "回到自己的文明之塔";
	public static readonly string CrzS_00106 = "重新裝修樓層";
	public static readonly string CrzS_00107 = "<1 Min";
	public static readonly string CrzS_00108 = "當你的騎士團位階到達%l{0}%w時，新樓層的建設速度增加%y10%";
	public static readonly string CrzS_00109 = "%y【{0}】";
	public static readonly string CrzS_00110 = "%w因放置在適合場所獲得效果|%l研究速度提升300%|研究獲得銀幣數量提升100%|";
	public static readonly string CrzS_00111 = "%w當此工具放置在[[[color={0}]]]{1}%w裡你可以獲得%l提升研究速度的效果%w以及%l可學習更高階的技能|";
	public static readonly string[] CrzS_00112 = new string[2] { "切換全螢幕", "離開全螢幕" };
	public static readonly string CrzS_00113 = "立即完成樓層擴建|需要花費%j{0}%w金|是否要立即完成？";
	public static readonly string[] CrzS_00114 = new string[2] 
	{ 
		"鍵盤WSAD操作移動方向，QE操作鏡頭轉動", 
		"鍵盤WSQE操作移動方向，AD操作鏡頭轉動" 
	};
	public static readonly string CrzS_00115 = "亞特蘭提斯王國的貨幣，在文明之塔中從事研究行為時可以獲得";
	public static readonly string CrzS_00116 = "%w左鍵:  研究    /     右鍵:  卸下";
	public static readonly string CrzS_00117 = "[[[color={0}]]]{1}%w|下次刷新";
	public static readonly string CrzS_00118 = "機率刷新各種工具";
	public static readonly string CrzS_00119 = "帶著新工具的人民將在此時間後到達";
	public static readonly string CrzS_00120 = "%r|點擊快捷列上的文明之塔|開始遊玩大師系統";
	public static readonly string CrzS_00121 = "－傳奇的起點－";
	public static readonly string[] CrzS_00122 = new string[3] 
	{ 
		"以驚人的戰鬥實力|取下破壞神首級|傳說中的英雄現身",
		"以絕世的奇巧智慧|研發出高度科技|神話級的大師風範",
		"命中注定你將經歷這兩段旅程|[[[color=255,128,128]]]請選擇%w你傳奇的第一步"
	};
	public static readonly string[] CrzS_00123 = new string[2] { "以冒險為起點", "以經營為起點"};
	public static readonly string[] CrzS_00124 = new string[2] { "熱血戰鬥的角色扮演", "模擬經營的文明建設"};
	public static readonly string[] CrzS_00125 = new string[2] 
	{ 
		"您選擇先成為一名%y戰士%w，進行%y熱血戰鬥的角色扮演之路。",
		"您選擇先成為一名%y大師%w，進行%y模擬經營的文明建設之路。"
	};
	public static readonly string CrzS_00126 = "%w可得材料：%o{0}";
	public static readonly string CrzS_00127 = "%w適合場所：{0}";
	public static readonly string CrzS_00128 = "研究速度{0}%|{1}|最高技能{2}階|";
	public static readonly string CrzS_00129 = "%b{0}[[[color=248,210,191]]]金幣";
	public static readonly string[,] CrzS_00130 = new string[2,2]
	{
		{"開啟音樂", "關閉音樂"},
		{"開啟音效", "關閉音效"}
	};
	public static readonly string CrzS_00131 = "戰鬥中無法離開遊戲";
	public static readonly string CrzS_00132 = "第{0}階段";
	public static readonly string CrzS_00133 = "[[[color=255,240,197]]]統計情形";
	public static readonly string[] CrzS_00134 = new string[2] { "達成階段", "報到好友"};
	public static readonly string[] CrzS_00135 = new string[3] { "詳細規則&教學", "領福袋x{0}", "領獎" };
	public static readonly string CrzS_00136 = "獎勵";
	public static readonly string CrzS_00137 = "快點邀請您的朋友們上線，進行報到就有機會獲得好禮！";
	public static readonly string[] CrzS_00138 = new string[]
	{
		"「活動規則」",
		"1.%y活動時間%w：每天的PM20:00~PM22:00之間皆可報到，系統每30分鐘判斷一次人數。",
		"2.%y活動方法%w：成功報到玩家在%r20:30、21:00、21:30、22:00%w會獲得「報到福袋*1」，%r且系統會判斷目前已報到人數，達到設定條件則在線報到玩家皆可以再獲得「達成階段獎勵」%w。",
		"3.%y活動條件%w：",
		"%g《與民同歡》%w第一階段標準是%r80%w人，每達成一個階段，下一個階段標準則會提高，作為下一個階段標準，最高有%r四%w個階段。",
		"%g《好友挺你》%w每個時間點判斷報到人數時，若已報到玩家同時間擁有12個已報到好友在線上，另外會獲得「銀幣驚喜袋*1」。(一天限領一次)",
		"4.%y領獎資格%w：獎勵保留至隔天中午11:59分，超過則系統回收，不予補發。"
	};
	public static readonly string CrzS_00139 = "報到成功|獲得5000銀幣";
	public static readonly string CrzS_00140 = "等級不足{0}級";
	public static readonly string CrzS_00141 = "達{0}人：{1}  +{2}";
	public static readonly string CrzS_00142 = "非活動時間";
	public static readonly string CrzS_00143 = "選擇你要的夢幻系樓層|(功能性樓層已建造{0}/{1})";
	public static readonly string CrzS_00144 = "(已擁有)";
	public static readonly string CrzS_00145 = "(可建造)";
	public static readonly string CrzS_00146 = "(未達條件)";
	public static readonly string CrzS_00147 = "同一種夢幻系場所只能同時擁有一所";
	public static readonly string CrzS_00148 = "尚未符合建造條件，將游標移動到樓層名稱可查看建造條件";
	public static readonly string CrzS_00149 = "(可蓋出未擁有樓層)";
	public static readonly string CrzS_00150 = "樓層高度不足以擁有更多夢幻系場所";
	public static readonly string CrzS_00151 = "是否使用|{0}|%w銀幣消耗減少|%g{1}";
#endregion

#region Gene use
  public const string GStr_00001 = "銀行";
  public const string GStr_00002 = "輸入錯誤";
  public const string GStr_00003 = "(需{0}金幣)";	
  public const string GStr_00004 = "是否確定要丟棄{0}{1}個";		
  public const string GStr_00005 = "郵局";
  public const string GStr_00006 = "不存在於世界上！";
  public const string GStr_00007 = "信箱已滿！！！";	
  public const string GStr_00008 = "是否確定要刪除信件?"; 
  public const string GStr_00009 = "確定要花費{0}銀幣寄出此信件嗎?";
  public const string GStr_00010 = "確定要取消編輯此信件嗎?";
  public const string GStr_00011 = "郵件";
  public const string GStr_00012 = "無";
  public const string GStr_00013 = "確定要回覆此信件嗎?";
  public const string GStr_00014 = "確定要寄出此信件嗎?";	
  public const string GStr_00015 = "是否願意發佈於FB?";		
  public const string GStr_00016 = "立即領取";
  public const string GStr_00017 = "系統信件";	
  public const string GStr_00018 = "此功能目前關閉中...";
  public const string GStr_00019 = "確定要刪除全部信件嗎?";
	
  public const string GStr_GM = "GM";
  public static string[] GStr_10001 = new string[4] {"ExpandForm","1","2","3"};	
  public static string[] GStr_10002 = new string[3] {"MailConfirm","1","2"};		
  //public static string[] GStr_10003 = new string[3] {"CountConfirm","",""};	
  public static string[] GStr_10004 = new string[4] {"PackageConfirm","領取快遞","itemname","cost"};	
  public static string[] GStr_10005 = new string[4] {"TradeBuyConfirm","購買","itemname","cost"};
	
  public static readonly string[] GStr_20001 = new string[4] { "重整", "提領銀幣", "銀行空間擴充", "銀幣上限擴充"};
  public static readonly string[] GStr_20002 = new string[5] { "Withdraw","提領銀幣","請輸入銀幣數量","提領","存入"};	
  public static readonly string[] GStr_20003 = new string[2] { "確定", "取消"};	
  public static readonly string[] GStr_20004 = new string[29] { //錯誤訊息-server
		"","目前狀態無法使用銀行!",  "銀幣不足，無法存入","銀幣不足，無法領取 ","物品不足，無法存入",  //1-4
		"物品不足，無法領取","銀幣已達上限，無法再存入","物品已達上限，無法再存入","玩家身上銀幣已達上限，無法領取",//5-8
		"無此物品，無法存入","無此物品，無法領取","身上物品已達上限，無法領取",	"銀行密碼錯誤","無效的銀行物品索引",//9-13
		"不同的物品無法堆疊","該格已有物品","角色負重不足","無效的物品索引","租用已到期，此格無法使用","銀行租用已滿",//14~19
		"重整成功","重整失敗","角色銀行重整成功","角色銀行重整失敗","天晶不足，無法存入",//20 ~24
		"天晶不足，無法領取","天晶已達上限，無法再存入","玩家身上天晶已達上限，無法領取",//25~27
		"刪除銀行物品失敗"};
  public static readonly string[] GStr_20005 = new string[8] { //訊息-client
		"存入","領出","刪除","元","個","目前場景無法使用銀行!!","金幣不足,無法擴充","銀行空間已達上限"};//0~7
  public static readonly string[] GStr_20006 = new string[2] {
		"成功","失敗"};		
  public static readonly string[] GStr_20007 = new string[2] { 
		"InputForm","請輸入數量"};	
  public static readonly string[] GStr_20008 = new string[2] {
		"是否擴充銀幣上限?","擴充{0}"};	
  public static readonly string[] GStr_20009 = new string[2] {
		"是否擴充銀行格數?","擴充{0}格",};	
  public static readonly string[] GStr_20010 = new string[2] {
		"信箱","快遞"};	
  public static readonly string[] GStr_20011 = new string[4] {
		"新增信件","全部刪除","回報客服","新增快遞"};		
  public static readonly string[] GStr_20012 = new string[5] {
		"收件者","寄件者","收件時間","標題","寄件時間"};	
  public static readonly string[] GStr_20013 = new string[4] {
		"寄出","取消","回覆","刪除"};
  public static readonly string[] GStr_20014 = new string[5] {	//server訊息-信件
		"","寄件成功","Server信件已達上限，失敗","","GM目前不在線上!!!"};		
  public static readonly string[] GStr_20015 = new string[8] {	//client訊息
		"金額不足","您尚未輸入收件人喔！","您尚未輸入標題/內文喔！","請先關掉快遞!!","銀幣上限:{0}", //0~4
		"郵票數：{0} / {1}。|可至購物中心（X）購買郵票|※ 需點擊使用郵票來進行補充",//5
		"請透過「回報客服」來進行回報","需VIP 3方可使用新增快遞功能"};//6~7
  public static readonly string[] GStr_20016 = new string[4] {
		"收費規則","物品","郵票","單格物品須花費  {0}  郵票"};
  public static readonly string[] GStr_20017 = new string[2] { 
		"確定要寄出快遞嗎?","確定要取消編輯快遞嗎?"};
  public static readonly string[] GStr_20018 = new string[3] {  
		"此物品不可存銀行!","此物品不可存郵局!","寄件人:"};
  public static readonly string[] GStr_20019 = new string[17] { //server訊息-鏢局
		"","身上金錢不足","身上物品不足","對方郵局空間不足","無此玩家","物品數量超過可堆疊上限","郵局物品數量不足，無法領取",//1-6
		"身上物品空間不足，無法領取","對方郵局忙碌中,請稍候","超過可使用貨到付款數量","寄快遞成功","交易失敗","對方金錢超過上限",//7-12
		"超過身上金錢上限，無法領取","無此現金袋","寄送失敗","身上郵票不足"};//13-16
  public static readonly string[] GStr_20020 = new string[10] {  //client訊息
		"目前狀態下無法使用郵局或郵局已開啟!","背包空間不足!","不能寄快遞給自己!!","%a共%b個","%a寄%b給%c","領取快遞{0}",//0-5
		"寄快遞到你的郵局","已超過郵票數上限","獲得郵票{0}(郵票總數可於信件快遞介面查詢)","尚未置入物品"};//6-9
  public static readonly string[] GStr_20021 = new string[2] {
		"確定要花費{0}銀幣領取物品嗎?","您可以花費金幣立即領取，或自行回主城找郵務士花費銀幣領取"};
  public static readonly string[] GStr_20022 = new string[18] {//拍賣場文字標籤		
		"金幣區","銀幣區","販賣","等級","顏色","關鍵字","購買","下架","商品出售區",//0-8
		"物品名稱","數量","價格","上架","交易中心","商品上架區","出售","個人上架物品","只顯示自己職業適用"};	//9-17
  public static readonly string[] GStr_20023 = new string[30] {	//server訊息-交易中心
		"","目前交易中心交易人數過多,請稍候","系統忙碌中請稍候","上拍數量已達上限","找不到你要下架的物品","身上無此物品","不能購買自己上架商品",//0~6
		"此物品已下架","同帳號角色之間不能互相買賣","此場景不可開啟交易中心","競標價必須高於底價","競標商品出價被其他玩家超過","銀幣不足支付上架費",//7~12
		"貨幣中心未清空","競標價不合法","金幣不足","此物品出售中","非消費伺服器","購買失敗","購買銀幣不足","上架失敗","交易中心關閉中","上架成功",//13~22
		"交易中心上架數量已滿","下架成功","下架失敗","出售成功","出售失敗","出售失敗，超過玩家銀幣上限","此物品回收數量已達上限"}; //23~29
  public static readonly string[] GStr_20024 = new string[15] {	//client訊息-交易中心
		"資料傳輸中...請稍候","此物品不可交易","確定出售此項物品","{0}已經成功跟你購買{1}","購買{0}成功",//0..4
		"此物品不可賣金幣","此物品不可賣銀幣","低於交易金幣下限({0})","超過交易金幣上限({0})","手續費不足,需{0}銀幣",//5..9
		"身上物品空間不足，無法購買","搜尋失敗","搜尋結束","確定上架此項物品","須完整物品才可上架" };//10..14
  public static readonly string[] GStr_20025 = new string[2] {
		"共計金幣:","共計銀幣:"};
  public static readonly string[] GStr_20026 = new string[3] {//for trade hint
		"請輸入欲上架的金幣價格|須收上架費:{0}銀幣","我們願意用以下價格({0}~{1}銀幣)|向你回收此二手物品","{0}/{1}"};
  public static readonly string[] GStr_20027 = new string[4] {//拍賣場大標籤
		"銀幣區","金幣區","銀幣出售","金幣上架"};	//0-3	
  public static readonly string[] GStr_20028 = new string[5] {//GM回報
		"儲值問題回報","BUG問題回報","任務問題回報","投訴玩家回報","其他問題回報"};
	
  public static readonly string[] GTimeType = new string[2]{
		 "{0}/{1}/{2}","{0}年{1}月{2}日"};	
  public static readonly string[] GColor = new string[5]{
		 "無","白","藍","紫","金"};	
  public static readonly string[] GLevel = new string[6]{
		 "無","0~40","40~80","80~120","120~160","160~200"};
  //all SpriteVectorText name
  public const string Common_TitleName = "Title";	
  public const string Common_ButtonQ = "ButtonQ";
  public static readonly string[] Common_ContextName = new string[] {"Context1","Context2","Context3"};	
  public static readonly string[] Common_ButtonName = new string []{"Btn_First","Btn_Second"};	
  public static readonly string[] Common_RadioName = new string []{"Radio1","Radio2","Radio3"};		
#endregion
	
#region RplaceStr
	public static readonly string[][] ReplaceStr = new string[6][]
	{
		new string[]{"先生",   "小姐"},		//%B
		new string[]{"大人",   "女士"},		//%D
		new string[]{"王子",   "公主"},		//%E
		new string[]{"爵士",   "女爵士"},		//%F
		new string[]{"紳士",   "淑女"},		//%G
		new string[]{"大哥哥", "大姊姊"}		//%H
	};
#endregion
	
#region Base
	public static readonly string[] Base0001 = new string[]
	{
		"{0}%y EXP",
		"{0}%y 銀幣",
		"{0}%y 金幣",
		"{0}%y 王國點",
		"{0}%y 權力點",
		"{0}%y 能量",
		"{0}%y 訓練時間",
		"{0}%y 神器經驗值",
		"{0}%y 榮耀值",
	};
#endregion
	
#region Assessment
	public static readonly string[] Assessment0001 = new string[]
	{
		"",
		"空間不足",
		"銀幣上限不足"
	};
	
	public static readonly string[] Assessment0002 = new string[]
	{
		"",
		"Council_y",
		"Council_g",
		"Council_r",
		"Council_p",
		"Council_b"
	};
	
	public static readonly string[] Assessment0003 = new string[]
	{
		"",
		"_001",
		"_002",
		"_003",
		"_004",
		"_005",
		"_006",
		"_007",
		"_008",
		"_009",
		"_010"
	};
	
	public static readonly string[] Assessment0004 = new string[]
	{
		"",
		"風行之地",
		"土息之地",
		"知識之地",
		"火炙之地",
		"水湧之地"
	};
	
	public static readonly string[][] Assessment0005 = new string[7][]
	{
		new string[]{""},
		new string[]{"","11133","11134","11135","11136","11137","11138","11139","11140","11141","11142","11143","11144","11145","11146","11147"},
		new string[]{"","11148","11149","11150","11151","11152","11153","11154","11155","11156","11157","11158","11159","11160","11161","11162"},
		new string[]{"","11163","11164","11165","11166","11167","11168","11169","11170","11171","11172","11173","11174","11175","11176","11177"},
		new string[]{"","11178","11179","11180","11181","11182","11183","11184","11185","11186","11187","11188","11189","11190","11191","11192"},
		new string[]{"","11193","11194","11195","11196","11197","11198","11199","11200","11201","11202","11203","11204","11205","11206","11207"},
		new string[]{"","11208","11209","11210","11211","11212"}
	};
	
	public static readonly string[] Assessment0006 = new string[]
	{
		"同    意","返    回"
	};
	
	public static readonly string[] Assessment0007 = new string[]
	{
		"部下資訊","部下招募"
	};
	
	public static string[] Assessment0008 = new string[4] {"AssessmentConfirm","招募提示","立即招募好友成為您的部下吧！","(初次招募可獲得免費金幣喔！)"};	
	
	public const string Assessment0009="花費：";
	
	public const string Assessment0010="尚有{0}位部將未指派工作";
	
	public static readonly string[] Assessment0011 = new string[]
	{
		"","招募","替換"
	};
	
	public const string Assessment0012="指派部下({0}/{1}):";
	
	public static readonly string[] Assessment0013 = new string[]
	{
		"力　量　：","敏　捷　：","體　力　：","智　力　：","精　神　：",
		"攻擊強度：","法術傷害：","防 禦 力 ："
	};
	
	public static readonly string[] Assessment0014 = new string[]
	{
		"確　　定","替換部下"
	};
	
	public static readonly string[] Assessment0015 = new string[]
	{
		"部下屬性","榮譽勳章","部下性格"
	};
	
	public static string[] Assessment0016 = new string[4] {"ReplaceConfirm","提示","替換將消除已獲得的個性與屬性","但仍保留得到的勳章(技能)"};	
	
	public static readonly string[] Assessment0017 = new string[]
	{
		"確　　定","領取獎賞"
	};
	
	public const string Assessment0018="贈送";
	public static readonly string[] Assessment0019 = new string[]
	{
		"名次","職業","名稱","位階"
	};
	
	public const string Assessment0020="權力點不足";
	
	public const string Assessment0021="此部下無可回報任務。";
	
	public const string Assessment0022="位階{0}-";
	
	public static readonly string[] Assessment0023 = new string[]
	{
		"王國點 +{0}",
		"權力點 +{0}"
	};
	
	public static readonly string[] Assessment0024 = new string[]
	{
		"部下資訊","位階排行榜","訊息","招募","社群","背包"
	};
	
	public static readonly string[] Assessment0025 = new string[]
	{
		"確　　定","指派工作"
	};
	
	public const string Assessment0026="等級不足{0}級無法進入騎士團";
	
	public static readonly string[] Assessment0027 = new string[]
	{
		"",
		"工作中",
		"已完成",
		"待替換",
		"待招募",
		""
	};
	
	public const string Assessment0028="無可指派部下。";
	
	public const string Assessment0029="部下已達上限。";
	
	public const string Assessment0030="部下不存在";
	
	public const string Assessment0031="恭喜您的騎士位階提升至{0}階";
	
	public static readonly string[] Assessment0032 = new string[]
	{
		"%y風行之地||%w主要取得屬性：%j敏捷|%w可能取得屬性：%j智力",
		"%y土息之地||%w主要取得屬性：%j體力|%w可能取得：%j榮譽勳章(技能)",
		"%y知識之地||%w主要取得屬性：%j智力|%w可能取得屬性：%j體力",
		"%y火炙之地||%w主要取得屬性：%j力量|%w可能取得屬性：%j未知",
		"%y水湧之地||%w主要取得屬性：%j精神|%w可能取得：%j性格"
	};
	
	public const string Assessment0033="部下: {0}/{1}";
	
	public static readonly string[] Assessment0034 = new string[]
	{
		"",
		"已選取",
		"待替換"
	};
	
	public const string Assessment0035="消耗權力點立即完成任務";
	
	public static readonly string[] Assessment0036 = new string[]
	{
		"%o力量：%w|可提高攻擊強度與格檔",
		"%o敏捷：%w|可提高攻擊",
		"%o體力：%w|可提高生命最大值、招架",
		"%o智力：%w|可提高法術傷害、治療量、法術爆擊、魔力最大值",
		"%o精神：%w|可提高生命回復、魔力回復、韌性"
	};
	
	public static string[] Assessment0037 = new string[4] {"Assessment0037","提示","進行此操作將會花費您{0}枚金幣","並消除此勳章，確定要繼續嗎？"};
	
	public const string Assessment0038="此欄不可清除!!";
	
	public static string[] Assessment0039 = new string[4] {"Assessment0039","提示","進入騎士團將會離開隊伍，","是否繼續？"};
	
	public const string Assessment0040="(提升為{0}階騎兵還需總屬性點數:{1}點)";
	
	public const string Assessment0041="能力:";
	
	public static string[] Assessment0042 = new string[4] {"Assessment0042","提示","進行此操作將會花費您{0}枚金幣","並消除此性格，確定要繼續嗎？"};
	
	public const string Assessment0043="{0}";
	
	public const string Assessment0044="恭喜你的部下";
	
	public const string Assessment0045="提升至";
	
	public const string Assessment0046="下階段目標";
	
	public const string Assessment0047="騎士位階達{0}階後即可解鎖此任務";
	
	public const string Assessment0048="%o力量：%w|可提高攻擊強度與格檔|%o敏捷：%w|可提高攻擊、閃避、物理爆擊|%o體力：%w|可提高生命最大值、招架|%o智力：%w|可提高法術傷害、治療量、法術爆擊、魔力最大值|%o精神：%w|可提高生命回復、魔力回復、韌性|%r屬性數值為紅色時，則不可再從各項任務中|獲得該屬性的提升，必須透過玩家的升級來|提高部下的屬性數值上限";
	
	public const string Assessment0049 = "銀行開啟中無法進入騎士團";
	
	public const string Assessment0050 = "部下工作中，無法替換";
	
	public const string Assessment0051="目前一次最多可指派{0}位，提升VIP等級至{1}，可增加指派數量至{2}位";
	
	public const string Assessment0052="[[[lineGap=2]]]%j替換部下將會消除已獲得的屬性與性格，但您可花費{0}銀幣或{1}金幣同時保留屬性與性格。||請選擇替換部下的方式：|花費：|          {0}|                 {1}";
	
	public static readonly string[] Assessment0053 = new string[]
	{
		"保留勳章與性格","保留勳章與性格","直接替換不保留"
	};
#endregion
	
#region Advanture
	
	public static string Advanture0001 = "提示";
	
	public static string Advanture0002 = "%w是否成立冒險團？%y須滿30級、需冒險團之證或20萬銀幣";
	
	public static string Advanture0003 = "身上沒有冒險團之證而且銀幣不足{0}，無法創立！";
	
	public static string Advanture0004 = "等級未滿{0}級，無法創立！";
	
	public static string Advanture0005 = "已加入冒險團，無法另行成立新冒險團！";
	
	public static string[] Advanture0006 = new string[2]{"ConfirmSendAdvName", "請輸入冒險團名稱"};
	
	public static string Advanture0007 = "招收";
#endregion
	
#region EnergyTran
	public static readonly string[] EnergyTran0001=new string[]
	{
		"[[[size=15]]]%y能量說明：|%w每%v3%w分鐘恢復%v1%w點。自然恢復時能量現有值不可超過能量上限，可使用%y金幣%w購買能量",
		"[[[size=15]]]%y權力點說明：|%w每%o1%w分鐘恢復%o1%w點。自然恢復時權力點現有值不可超過權力點上限，可使用%y金幣%w購買權力點",
		"[[[size=15]]]%y銀幣說明：|%w遊戲中進行%f裝備修復%w、%f星盤學習%w、%f裝備強化%w…等動作會消耗銀幣，可使用%y金幣%w購買銀幣"
	};
	
	public static readonly string[] EnergyTran0002=new string[]
	{
		"[[[size=15]]]轉換%y{0}%w金幣為%y{1}%w點能量",
		"[[[size=15]]]轉換%y{0}%w金幣為%y{1}%w點權力點",
		"[[[size=15]]]轉換%y{0}%w金幣為%y{1}%w銀幣"
	};
	
	public static readonly string[] EnergyTran0003=new string[]
	{
		"挑選欲轉換的能量：",
		"挑選欲轉換的權力點：",
		"挑選欲轉換的銀幣："
	};
	
	public static readonly string[] EnergyTran0004=new string[]
	{
		"能量轉換",
		"權力點轉換",
		"銀幣轉換"
	};
	
	public const string EnergyTran0005="訓練時間";
	
	public const string EnergyTran0006="剩餘時間：";
	
	public const string EnergyTran0007="[[[size=15]]]%y訓練時間說明：|%w每日%o中午12點%w可領取最多%o3小時%w，擁有訓練時間時，打倒怪物獲得之%fEXP%w、%f銀幣效益%w為%o500%%w。進入%b戰鬥狀態%w後會自動消耗，在%b副本%w中消耗更快。";
	
	public static readonly string[] EnergyTran0008=new string[]
	{
		"[[[size=15]]]%y【免費】領取訓練時間：|%w可%l【免費】%w領取訓練時間，|%f請注意：%w訓練時間不可累積超過%b3%w小時！",
		"[[[size=15]]]%y【銀幣】購買訓練時間：|%w可使用%l【銀幣】%w購買訓練時間，|%f請注意：%w訓練時間不可累積超過%b3%w小時！",
		"[[[size=15]]]%y【金幣】購買訓練時間：|%w可使用%l【金幣】%w購買訓練時間，|%f請注意：%w訓練時間不可累積超過%b3%w小時！"
	};
	
	public static readonly string[] EnergyTran0009=new string[]
	{
		"能量不可超過上限",
		"權力點不可超過上限",
		"銀幣不可超過上限"
	};
	
	public const string EnergyTran0010="已領取";
	
	public static string[] EnergyTran0011 = new string[3] {"EnergyTranConfirm","提示","訓練時間無法超過3小時，是否仍要領取訓練時間？"};
	
	public static readonly string[] EnergyTran0012=new string[]
	{
		"[[[size=15]]]|，並額外獲得1個%f大師回饋驚奇袋|【有機率從中獲得%b御匠造型背甲%f】",
		"[[[size=15]]]|，並額外獲得1個%f騎士回饋驚奇袋|【有機率從中獲得%b體驗座騎－斯特克騎士%f】",
		""
	};
	
	public const string EnergyTran0013="背包放不下贈品囉";
	
	public static readonly string[] EnergyTran0014=new string[]
	{
		"購買{0}能量成功",
		"購買{0}權力點成功",
		"購買{0}銀幣成功"
	};
#endregion
	
#region Str2056

  //物品Hint
  public static readonly string[] Str2056_0001 = new string[]
  {
    "[[[size=22 color={0}]]]{1}",//0:物品名稱
    "[[[size=18 color=240,80,80]]]靈魂綁定",      //1
    "[[[size=18 color=240,80,80]]]租用中",        //2
    "[[[size=14 color=240,80,80]]]等級需求：{0}",    //3
    "[[[size=14 color=255,249,170]]]等級需求：{0}",  //4
    "[[[size=14 color=255,249,170]]]類型：{0}",      //5
    "[[[size=14 color=255,249,170]]]強度等級：{0}",  //6
    "[[[size=14 color=240,80,80]]]耐久：{0}/{1}",    //7
    "[[[size=14 color=255,249,170]]]耐久：{0}/{1}",  //8
    "[[[size=14 color=170,225,55]]]冷卻時間：{0} 秒",  //9
    "[[[size=14 color=255,255,180]]]重量：{0}",      //10
    "[[[size=14 color=49,255,160]]]攻擊強度+{0}{1:(+#);;}",    //11
    "[[[. color=49,255,160]]]法術傷害+{0}{1:(+#);;}", //12
    "[[[size=14 color=49,255,160]]]基本防禦+{0}{1:(+#);;}",    //13
    "[[[. color=49,255,160]]]法術防禦+{0}{1:(+#);;}", //14
    "[[[size=14 color=255,255,180]]]{0}",   //15:裝備效果素質
    "[[[. color=255,255,180]]]{0}",         //16:裝備效果素質
    "[[[size=14 color=170,225,55]]]恢復生命 {0}",                //17
    "[[[size=14 color=170,225,55]]]恢復魔力 {0}",                //18
    "[[[size=14 color=170,225,55]]]增加經驗值 {0}",              //19
    "[[[size=14 color=170,225,55]]]沒有效果",                    //20
    "[[[size=14 color=170,225,55]]]藥水容量：{0}/{1}[[[.]]]一次回復量：{2}",   //21
    "[[[size=14 color=255,255,180]]]恢復玩家氣血",                 //22
    "[[[size=14 color=255,255,180]]]恢復玩家真氣",                 //23
    "[[[size=14 color=255,255,180]]]恢復靈獸氣血",                 //24
    "[[[size=14 color=255,255,180]]]恢復靈獸真氣",                 //25
    "[[[size=14 color=255,249,170]]]{0}",                        //26:物品說明
    "[[[size=14 color=130,230,255]]]剩餘使用天數：{0}天/{1}天",      //27
    "[[[size=14 color=130,230,255]]]本日剩餘使用次數：{0}次",      //28
    "[[[size=14 color=130,230,255]]]※您已經擁有此造型",           //29
    "[[[size=14 color=255,255,180 align=right]]]{0}",           //30:銀幣賣價
    "[[[size=14 color=170,225,55]]]依玩家等級給予經驗值",              //31:
    "[[[size=14 color=170,225,55]]]{0}",                        //32:狀態Hint說明
    "[[[size=18 color=170,225,55]]]星儀攻防加成+{0}%",            //33
    "[[[size=18 color=170,225,55]]]可隨機獲得一個品項：",          //34:福袋小標題
    "[[[size=18 color=170,225,55]]]可一次獲得所有品項：",          //35:福袋小標題
    "[[[size=18 color=170,225,55]]]可自行兌換一個品項：",          //36:兌換卡小標題
    "[[[size=14 color=255,249,170]]]{0}",                        //37:福袋兌換卡內容物, 套裝武器系列名稱
    "[[[size=15 align=center color=255,204,0]]]<ALT+點擊物品顯示詳細內容>",     //38
    "[[[size=18 color=245,160,35]]]星石爆發效果",                  //39
    "[[[size=14 color=240,225,100]]]爆發1星：{0}+{1}{2}",         //40
    "[[[size=14 color=240,225,100]]]爆發2星：{0}+{1}{2}",         //41
    "[[[size=14 color=240,225,100]]]爆發3星：{0}+{1}{2}",         //42
    "[[[size=14 color=240,225,100]]]爆發4星：{0}+{1}{2}",         //43
    "[[[size=14 color=240,225,100]]]爆發5星：{0}+{1}{2}",         //44
    "[[[size=14 color=135,135,135]]]爆發1星：(尚未開啟)",          //45
    "[[[size=14 color=135,135,135]]]爆發2星：(尚未開啟)",          //46
    "[[[size=14 color=135,135,135]]]爆發3星：(尚未開啟)",          //47
    "[[[size=14 color=135,135,135]]]爆發4星：(尚未開啟)",          //48
    "[[[size=14 color=135,135,135]]]爆發5星：(尚未開啟)",          //49
    "[[[size=14 color=49,255,160]]]",                        //50.福袋內容物
    "[[[size=14 color=170,225,55]]]移動速度：{0}",               //51:座騎速度
    "|[[[size=14 color=170,225,55]]]最高飛行高度：{0}",            //52:座騎飛行高度
    "|[[[size=14 color=170,225,55]]]最高飛行高度：無限制",         //53:座騎飛行高度
    "[[[size=14 color=170,225,55]]]恢復權力點 {0}",               //54.藥品效果
    "[[[size=14 color=170,225,55]]]恢復能量 {0}",                 //55.藥品效果
    "[[[size=14 color=130,230,255]]]剩餘使用次數：{0}次",          //56
    "[[[size=18 color=240,80,80]]]{0} ({1}/{2})",                 //57.套裝名稱和件數
    "[[[size=14 color=145,135,130]]]{0}",                         //58.套裝武器系列名稱
    "[[[size=14 color=255,249,170]]]{0}",                         //59.套裝有搭配裝備, 套裝效果(已達件數)
    "[[[size=14 color=145,135,130]]]{0}",                         //60.套裝無搭配裝備, 套裝效果(未達件數)
    "[[[size=14 color=130,230,255]]]穿著{0}件效果",                //61.
    "[[[size=14 color=255,249,170]]]{0}+{1}",                     //62.套裝效果(已達件數)
    "[[[size=14 color=145,135,130]]]{0}+{1}",                     //63.套裝效果(未達件數)
    "[[[size=14 color=255,249,170]]]神器等級：{0}|最高等級：{1}",   //64
    "[[[size=14 color=255,249,170]]]累積經驗：{0}",                //65
    "[[[size=14 color=255,249,170]]]升級經驗：{0}",                //66
    "[[[size=14 color=170,225,55]]]冷卻時間：{0} 分",              //67
    "[[[size=14 color=170,225,55]]]冷卻時間：{0}小時{1}分",        //68
    "", //69
    "[[[size=14 color=170,225,55]]]{0}",                             //70.神器效果提示文字
    "[[[size=14 color=170,255,55]]]恢復權力點 {0}",                  //71
    "[[[size=14 color=170,225,55]]]移動速度 {0}[[[.]]]飛行高度 {1}",  //72
    "[[[size=15 align=center color=225,70,54]]]<點擊右鍵自動使用>",   //73
    "[[[size=14 color=130,230,255 .]]]......等品項",                 //74:福袋兌換卡內容物超過顯示數量
    "[[[size=14 color=130,230,255]]]剩餘擴充次數：{0}次",             //75.
    "[[[size=14 color=91,215,255]]]主星石：提高星儀攻防加成至{0}%",    //76.
    "[[[size=14 color=135,135,135]]]主星石：提高星儀攻防加成至{0}%",    //77.
    "[[[size=14 color=240,80,80]]]類型：{0}",                      //78
    "[[[size=14 color={0}]]](目前裝備)",                             //79
    "[[[size=14 color=170,225,55]]]累積靈魂值：{0}|覺醒靈魂值：{1}",  //80
    "[[[size=18 color=245,160,35]]]水晶效果",                       //81
    "[[[size=14 color=255,255,0]]]{0}+{1} (Max)",     //82:水晶效果(靈魂值已滿,MAX)
    "[[[size=14 color=145,135,130]]]{0}+{1} (Max)",   //83:水晶效果(靈魂值未滿,Max)
    "[[[size=14 color=240,225,100]]]{0}+{1}",         //84:水晶效果(靈魂值已滿)
    "[[[size=14 color=145,135,130]]]{0}+{1}",         //85:水晶效果(靈魂值未滿)
    "[[[size=18 color=0,0,0,0]]].......[[[size=14 color=245,160,35]]]未鑲嵌",  //86
    "[[[size=18 color=0,0,0,0]]].......[[[size=14 color=245,160,35]]]{0}",    //87
    "[[[size=14 color=255,255,180]]]{0}+{1}",                                 //88:裝備鑲嵌效果
    "[[[size=18 paraGap=2 color=0,0,0,0]]]  `[[[size=14 color=255,255,255]]]    回收價：{0:#,##0}",            //89
    "[[[size=14 color=49,255,160]]]",              //90:福袋內容物(廣播=1)
    "[[[size=14 color=222,158,255]]]",             //91:福袋內容物(廣播=2)
    "[[[size=14 color=170,225,55]]]冷卻倒數：{0} 分",        //92
    "[[[size=14 color=170,225,55]]]冷卻倒數：{0} 秒",        //93
    "[[[size=14 color=170,225,55]]]加倍狀態持續 {0} 秒",     //94.神器槌子
    "", //95
    "恢復權力點 {0}",        //96
    "加倍狀態持續 {0} 秒",   //97
    "移動速度 {0}[[[.]]]飛行高度 {1}",  //98
    "",   //99
    "|%r攻擊強度加成%w：{0}%",  //100:技能說明
    "|%b法術傷害加成%w：{0}%",  //101:技能說明
    "|%g治療量加成%w：{0}%",    //102:技能說明
    "冷卻倒數：{0:0} 分", //103
    "冷卻倒數：{0:0.00} 秒", //104
    "[[[size=18 color=240,80,80]]]適用魔化裝備[[[size=14 color=255,249,170]]]", //105
    "|{0}", //106
    "[[[size=14 color=141,180,227]]]星石能量：{0}+{1}",//107
    "[[[size=18 color=170,225,55]]]技能說明",   //108
    "[[[size=14]]]%w距離：{0:0}{1}{2}{3}|%w{4}",   //109:技能說明
    "", //110
    "[[[size=14 color=130,230,255]]]※您已經加冕過此頭銜",  //111
    "|[[[size=14 color=170,225,55]]]騎乘人數：{0}", //112
    "已達最高", //113
    " (MAX)", //114
    "[[[size=14 color=212,255,39]]]儲存靈魂值：{0}",   //115
    "[[[size=14 color=130,230,255]]]※您已經喚醒過此星！", //116
    "[[[size=14 color=255,249,170]]]", //117
    "[[[size=14 color=130,230,255]]]", //118
    " (已擁有)", //119
    "[[[size=14 color=255,249,170]]]", //120
    "|{0}(等級需求：{1})", //121
    " (已有{0}件)", //122
  };

  public const string Str2056_0002="此物品不正確";
  public const string Str2056_0003="請先裝備一般【{0}】";
  public const string Str2056_0004="無此物品";
  public const string Str2056_0005="物品使用中，無法裝備";
  public const string Str2056_0006="物品無法裝備在該位置";
  public const string Str2056_0007="裝備使用中無法脫下";
  public const string Str2056_0008="租用物品已到期，無法裝備";
  public const string Str2056_0009="收到未定義的錯誤";
  public const string Str2056_0010="裝備物品異常";
  public const string Str2056_0011="背包物品異常";
  public const string Str2056_0012="此物品無法卸下";
  public const string Str2056_0013="職業不符，無法進行裝備！";
  public const string Str2056_0014="等級不符，無法進行裝備！";
  public const string Str2056_0015="物品已損毀，無法裝備";
  public static string[] Str2056_0016=
  {
    "背包空間不足！",  //0
    "背包空間不足，無法卸下裝備",  //1
    "背包空間不足，請清出空間後再進行拆裝", //2
    "背包空間不足，無法使用功能",  //3
    "背包空間不足，無法卸下水晶",  //4
    "背包空間不足，最少需有一格空間",  //5
    "背包空間不足，無法使用！", //6
    "背包空間不足，無法挖掘水晶",  //7
  };
  public const string Str2056_0017="裝備成功";
  public const string Str2056_0018="裝備已卸下";
//public const string Str2056_0019="失去{0}";
//public const string Str2056_0020="失去{0} X {1}";
  public const string Str2056_0021="獲得[[[color={0}]]]【{1}】";
  public const string Str2056_0022="獲得[[[color={0}]]]【{1}】X {2}";
  public const string Str2056_0023="物品使用中，無法搬移";
  public const string Str2056_0024="物品數量不足";
  public const string Str2056_0025="搬移目的格已有物品";
  public const string Str2056_0026="負重不足";
  public const string Str2056_0027="超過堆疊上限";
  public const string Str2056_0028="背包租賃到期，無法放入";
  public static readonly string[] Str2056_0029={"拆裝","重整","交易中心","隨身銀行","背包擴充"};
  public const string Str2056_0030="背包";
  public const string Str2056_0031="購買隨身銀行";
  public const string Str2056_0032="隨身銀行";
  public const string Str2056_0033="確定";
  public const string Str2056_0034="取消";
  public const string Str2056_0035="購買";
  public const string Str2056_0036="購買{0}次(需{1}金幣)";
  public const string Str2056_0037="[[[align=center]]]使用隨身銀行[[[.]]]剩餘使用次數{0}次";
  public const string Str2056_0038="[[[align=center]]]購買隨身銀行[[[.]]]購買{0}次(需{1}金幣)";
  public const string Str2056_0039="購買成功，增加{0}次隨身銀行使用次數";
  public const string Str2056_0040="超過堆疊上限";
  public const string Str2056_0041="請輸入數量";
  public const string Str2056_0042="物品刪除成功";
  public const string Str2056_0043="物品刪除失敗";
  public const string Str2056_0044="物品數量不足";
  public const string Str2056_0045="物品使用中，無法刪除";
  public const string Str2056_0046="無此物品";
  public const string Str2056_0047="[[[align=center]]]{0}╳{1}個[[[.]]]是否刪除此物品？";
  public const string Str2056_0048="使用{0}";
  public const string Str2056_0049="重整物品成功";
  public const string Str2056_0050="重整物品失敗";
  public const string Str2056_0051="[[[align=center]]]是否永久擴充背包{0}格[[[.]]]擴充{0}格(需{1}金幣)";
  public const string Str2056_0052="隨身銀行使用次數已達上限";
  public const string Str2056_0053="物品冷卻中，無法使用";
  public const string Str2056_0054="物品已過期，無法使用";
  public const string Str2056_0055="血量已滿";
  public const string Str2056_0056="魔力已滿";
  public const string Str2056_0057="本日可使用次數已用完，無法使用";
  public const string Str2056_0058="物品無效果，無法使用";
  public const string Str2056_0059="一般物品無法使用";
  public static readonly string[] Str2056_0060={"陸行座騎","飛行座騎","陸空座騎"};
  public const string Str2056_0061="騎乘座騎準備中......";
  public const string Str2056_0062="此地無法降落";
  public const string Str2056_0063="兌換卡";
  public const string Str2056_0064="內容物資訊";
  public const string Str2056_0065="數量：{0}";
  public const string Str2056_0066="請選擇要兌換的物品";
  public const string Str2056_0067="恭喜【{0}】從【{1}】中抽中【{2}】╳ {3}";  //"恭喜【玩家ID】從【福袋名稱】中抽中【物品名稱】X【數量】"
  public const string Str2056_0068="銀幣上限：{0}";
  public const string Str2056_0069="請選擇要兌換的物品";
  public const string Str2056_0070="今天無法再承接此任務";
  public const string Str2056_0071="已承接此任務";
  public const string Str2056_0072="已超過任務上限";
  public const string Str2056_0073="已接任務：『{0}』";      //"已接任務：『任務名稱』"
  public const string Str2056_0074="此物品無法使用";
  public const string Str2056_0075="是否承接『{0}』任務？";  //"是否承接『任務名稱』任務？"
  public const string Str2056_0076="{0} ╳ {1}";              //"物品名稱X物品數量"
//public const string Str2056_0077="";
  public const string Str_DelMissOK="已成功刪除任務";
  public const string Str_EventClosed="事件已關閉";
	
#if OPEN_DATA_PIECE
  public static string[] Str2056_0078=
  {
    "職業不符，必須為「聖劍士」才能進行裝備！",   //0
    "職業不符，必須為「福音祭司」才能進行裝備！", //1
    "職業不符，必須為「幽影法師」才能進行裝備！",  //2
    "職業不符，必須為「深淵刺客」才能進行裝備！",  //3
    "職業不符，必須為「齒輪大師」才能進行裝備！",  //4
    "職業不符，必須為「煉金術士」才能進行裝備！",  //5
    "職業不符，必須為「聖弓手」才能進行裝備！", //6
    "職業不符，必須為「黑武士」才能進行裝備！", //7
    "職業不符，必須為「蒸氣神兵」才能進行裝備！",  //8
    "職業不符，堅甲必須為「聖劍士、齒輪大師、深淵刺客」才能進行裝備！", //9
    "職業不符，輕甲必須為「福音祭司、幽影法師、煉金術士」才能進行裝備！",  //10
    "職業不符，必須為「聖劍士、福音祭司、聖弓手」才能進行裝備！", //11
    "職業不符，必須為「幽影法師、深淵刺客、黑武士」才能進行裝備！", //12
    "職業不符，必須為「齒輪大師、煉金術士、蒸氣神兵」才能進行裝備！",  //13
  };
#else
  public static string[] Str2056_0078=
  {
    "職業不符，必須為「聖劍士」才能進行裝備！",   //0
    "職業不符，必須為「福音祭司」才能進行裝備！", //1
    "職業不符，必須為「幽影法師」才能進行裝備！",  //2
    "職業不符，必須為「深淵刺客」才能進行裝備！",  //3
    "職業不符，必須為「齒輪大師」才能進行裝備！",  //4
    "職業不符，必須為「煉金術士」才能進行裝備！",  //5
    "職業不符，必須為「聖弓手」才能進行裝備！", //6
    "職業不符，必須為「黑武士」才能進行裝備！", //7
    "職業不符，必須為「蒸氣神兵」才能進行裝備！",  //8
    "職業不符，堅甲必須為「聖劍士、齒輪大師、深淵刺客」才能進行裝備！", //9
    "職業不符，輕甲必須為「福音祭司、幽影法師、煉金術士」才能進行裝備！",  //10
    "職業不符，必須為「聖劍士、福音祭司」才能進行裝備！", //11
    "職業不符，必須為「幽影法師、深淵刺客」才能進行裝備！", //12
    "職業不符，必須為「齒輪大師、煉金術士」才能進行裝備！",  //13
  };
#endif
  public static string[][] Str2056_0079=
  {
    new string[2] {"吾乃亞特蘭提斯之盾！", "守護王之榮耀！"},
    new string[2] {"為了亞特蘭提斯的子民！", "旋律能讓靈魂得到救贖！"},
    new string[2] {"渾沌之力是最後的解放！", "破壞乃重生之起源！"},
    new string[2] {"在死亡的陰影下出擊！", "來自深淵，征服一切！"},
    new string[2] {"為了偉大的科學！", "齒輪！扳手！機器人！"},
    new string[2] {"下一個實驗令我感到無比興奮！", "為了探究永恆真理而生！"},
  };
//public string Str2056_0080="";
//public string Str2056_0081="";
//public string Str2056_0082="";
//public string Str2056_0083="";
//public string Str2056_0084="";
//public string Str2056_0085="";
//public string Str2056_0086="";
//public string Str2056_0087="";
//public string Str2056_0088="";
  public const string Str2056_0089="物品使用中";
  public const string Str2056_0090="物品使用失敗";
  public const string Str2056_0091="無法使用此物品";
  public const string Str2056_0092="延遲時間不足，請稍後再試";
  public const string Str2056_0093="背包操作中，無法再進行其他操作";
  public const string Str2056_0094="關閉介面以結束拆解裝備";
  public static string[] Str2056_0095={"[[[align=center]]][[[paraGap=8]]]%o『{0}』|%w對你使用{1}，是否接受？|{2}|%o", "[[[align=reight]]]秒"};
  public const string StillPunish = "%f接受友軍復活仍有輕微懲罰";
  public const string Str2056_0096="騎乘中，無法操作";
  public const string Str2056_0097="拆解失敗";
  public const string Str2056_0098="此物品無法進行拆解";
  public const string Str2056_0099="[[[size=14]]]%w拆解後無法還原，您確認要拆解這件裝備嗎？|[[[color=255,70,70]]](請確認身上銀幣空間是否足夠，建議剩餘空間至少大於五萬，超過上限系統將回收不予以補發)";
//public const string Str2056_0100="";
  public const string Str2056_0101="忙碌中，無法操作";
  public const string Str2056_0102="已拆解裝備，獲得 {0} 銀幣";               //"已拆解裝備，獲得【數量】銀幣";
  public const string Str2056_0103="已拆解裝備，獲得 {0} 銀幣以及{1} ╳ {2}";  //"已拆解裝備，獲得【數量】銀幣以及【道具】x【數量】"
  public const string Str2056_0104="連續開啟福袋：";
  public const string Str2056_0105="物品忙碌中，無法操作";
  public const string Str2056_0106="物品不可刪除";
  public const string Str2056_0107="物品不可移動";
  public const string Str2056_0108="背包無法重整";
  public const string Str2056_0109="背包物品重整中，無法操作";
  public const string Str2056_0110="可使用次數已用完，無法使用";
  public const string Str2056_0111="已放置到材料櫃中";
  public const string Str2056_0112="金幣不足";
  public const string Str2056_0113="請先選擇要進行偷竊的目標";
  public const string Str2056_0114="使用神器成功";
  public const string Str2056_0115="使用神器失敗";
  public const string Str2056_0116="目前狀態良好無法被偷取";
  public const string Str2056_0117="已經沒有東西可以被偷了";
  public const string Str2056_0118="黑貓靈巧的竊取銀幣";
//public const string Str2056_0119="";
  public const string Str2056_0120="黑貓靈巧的竊走了{0}";       //"黑貓靈巧的竊走了【物品名稱】
  public const string Str2056_0121="黑貓靈巧的竊走了{0}銀幣";   //"黑貓靈巧的竊走了【銀幣】"
  public const string Str2056_0122="銀幣或背包空間不足";
  public const string Str2056_0123="此場景無法記憶";
  public const string Str2056_0124="記憶成功";
  public const string Str2056_0125="記憶失敗";
  public const string Str2056_0126="傳送成功";
  public const string Str2056_0127="傳送失敗";
  public const string Str2056_0128="所在場景無法使用";
  public const string Str2056_0129="等級不足，無法傳送";
  public const string Str2056_0130="目標所在場景不允許傳送";
  public const string Str2056_0131="目標玩家飛行狀態無法傳送";
  public const string Str2056_0132="目前玩家狀態不允許被呼叫";
  public const string Str2056_0133="您沒有足夠的道具無法進行傳送";
  public const string Str2056_0134="此場景無法進行傳送";
  public const string Str2056_0135="您或目標所在的場景不允許進行傳送";
  public const string Str2056_0136="您尚未組隊，請使用個人傳送功能";
  public const string Str2056_0137="此位置已有物品";
  public const string Str2056_0138="此物品未開放";
  public const string Str2056_0139="等級不符，無法使用";
  public const string Str2056_0140="目前場景無法使用此物品";
  public const string Str2056_0141="已修復此物品耐久度";
  public const string Str2056_0142="已增加此物品耐久度";
  public const string Str2056_0143="此物品無法進行操作";
  public const string Str2056_0144="此物品不存在";
  public const string Str2056_0145="物品操作失敗";
  public const string Str2056_0146="獲得金幣{0}";
  public const string Str2056_0147="會員狀態不符";
  public const string Str2056_0148="已在重生點場景";
  public const string Str2056_0149="空間彈跳器";
  public const string Str2056_0150="記憶石";
  public const string Str2056_0151="時空之鑰";
  public const string Str2056_0152="請選擇定位，或者要取代的定位點：";
  public const string Str2056_0153="{0}.";
  public const string Str2056_0154="尚未儲存記憶點";
  public const string Str2056_0155="記憶";
  public const string Str2056_0156="編輯註解";
  public const string Str2056_0157="傳送";
  public const string Str2056_0158="請輸入註解";
  public const string Str2056_0159="傳送至{0}，一次需要1個{1}，確定傳送？";
  public const string Str2056_0160="前往人數";
  public const string Str2056_0161="單獨前往";
  public const string Str2056_0162="組隊前往";
  public const string Str2056_0163="目標對象";
  public const string Str2056_0164="玩家";
  public const string Str2056_0165="好友";
  public const string Str2056_0166="請輸入玩家名稱";
  public const string Str2056_0167="[[[align=center color=196,189,151]]]組隊傳送需使用{0}個空間彈跳器，是否傳送?[[[. . color=192,80,77]]]※隊友可能拒絕傳送，或無法進行傳送場景。";
  public const string Str2056_0168="[[[align=cneter]]]{0}邀請您進行傳送，是否進行傳送？";
  public const string Str2056_0169="{0}({1},{2})";  //地圖名稱(X座標,Y座標)
  public const string Str2056_0170="獲得獎勵金幣{0}";
  public const string Str2056_0171="戰鬥中，無法記憶";
  public const string Str2056_0172="設定註解成功";
  public const string Str2056_0173="設定註解失敗";
  public const string Str2056_0174="此編號尚未記憶";
  public const string Str2056_0175="請先脫下造型武器";
  public static readonly string[] Str2056_0176={"主城","各等級地圖","副本入口","首領戰入口"};
  public const string Str2056_0177="請選擇欲傳送的區域";
  public const string Str2056_0178="戰鬥中，無法傳送";
  public const string Str2056_0179="物品數量不足，無法傳送";
  public const string Str2056_0180="等級不符，無法傳送";
  public const string Str2056_0181="角色不存在或離線狀態";
  public const string Str2056_0182="對方拒絕你傳送至身旁";
  public const string Str2056_0183="[[[align=center]]]{0}欲傳送至你身旁，是否接受傳送？";
  public const string Str2056_0184="目標對象不能是自己！";
  public const string Str2056_0185="副本中，無法使用此功能";
  public const string Str2056_0186="戰鬥中，無法使用此物品";
  public const string Str2056_0187="已達上限，無法再進行擴充";
  public const string Str2056_0188="記憶位置已達上限，無法再進行擴充";
  public const string Str2056_0189="空間已增加";
  public const string Str2056_0190="記憶石已增加5個可記憶位置";
  public const string Str2056_0191="物品條件不符";
  public const string Str2056_0192="已擁有此造型";
  public const string Str2056_0193="連續上線獎勵";
  public const string Str2056_0194="今日上線獎勵已領取";
  public const string Str2056_0195="領取上線獎勵";
  public const string Str2056_0196="金幣遊樂場";
  public const string Str2056_0197="此物品無法兌換造型";
  public const string Str2056_0198="[[[size=14 color=255,255,255]]]已連續上線：第[[[color=255,204,0]]]{0}[[[color=255,255,255]]]天";
  public const string Str2056_0199="[[[size=14 color=255,255,255]]]獲得銀幣：[[[color=255,204,0]]]{0}";
  public const string Str2056_0200="[[[size=14 color=255,204,0]]]恭喜你，可進入金幣遊樂場";
  public const string Str2056_0201="副本中，請先結束副本，請至提示櫃開啟此介面進入金幣遊樂場";
  public const string Str2056_0202="物品拍賣中";
  public const string Str2056_0203="目前場景無法騎乘";
  public const string Str2056_0204="造型無變更";
//public const string Str2056_0205="座騎施打縮小劑後變小了！";
  public const string Str2056_0206="此座騎無法再縮得更小了！";
//public const string Str2056_0207="座騎施打放大劑後變大了！";
  public const string Str2056_0208="此座騎無法再變得更大了！";
  public const string Str2056_0209="{0}不足"; //【物品】不足
  public const string Str2056_0210="藥劑施打失效";
  public const string Str2056_0211="煉金釜";
  public const string Str2056_0212="請置入要煉金的物品以及材料";
  public const string Str2056_0213="煉金";
  public const string Str2056_0214="材料不足";
  public const string Str2056_0215="煉金成功！";
  public const string Str2056_0216="煉金失敗！";
  public const string Str2056_0217="計時狀態中，請稍候";
  public const string Str2056_0218="資料傳輸中，請稍候";
  public const string Str2056_0219="回城傳送準備中";
  public const string Str2056_0220="傳送中斷";
  public const string Str2056_0221="目前角色狀態無法傳送";
  public const string Str2056_0222="權力點已滿";
  public const string Str2056_0223="能量已滿";
  public const string Str2056_0224="目前背包狀態無法進行其他操作";
  public const string Str2056_0225="已向對方發出傳送詢問";
  public const string Str2056_0226="開福袋中，請稍候";
  public const string Str2056_0227="背包物品已重整";
  public const string Str2056_0228="職業選擇";
  public const string Str2056_0229="選擇你的職業星盤";
  public static readonly string[] Str2056_0230 =
  {
    "聖劍士", "福音祭司", "幽影法師", "深淵刺客", "齒輪大師", "煉金術士", "聖弓手", "黑武士", "蒸氣神兵"
  };
  public static readonly string[] Str2056_0231 =
  {
    "~守護亞特國度的不屈盾牌~", "~撫慰人心的光之歌頌者~", "~操弄強大力量的魔法智者~", "~隱蔽於暗影的渾沌殺手~", "~喜愛機械的爆破藝術家~", "~生命煉金術的狂熱研究者~"
  };
  public static readonly string[] Str2056_0232 =
  {
    "%y光明殿堂%w所培育出來的精英戰士們，大多選擇踏上%m聖劍士%w這條神聖的道路。自古以來，他們肩負守衛亞特蘭提斯的重責大任，在破壞神甦醒、魔物肆虐的今日，更顯其重要性。在戰鬥中，聖劍士所持的盾牌有如堅不可摧的高牆，總是站在最前線%o守護隊伍%w中的每一個成員。",
    "%y光明殿堂%w內繼承光行者意志的人們，被授予祭司的神聖職責，隱藏在殿堂深處與世俗隔離；然而在經過千年的洗禮之後，擅長吟唱聖歌的%m福音祭司%w逐漸成為人們心靈的砥柱。在戰場上他們柔聲輕唱，將光明的力量化為動人的音樂，不但能%o治癒友軍%w的傷口，更能刺穿邪惡的耳膜。",
    "%l渾沌議會%w中極為渴求力量的人們，能將許多古老典籍中的知識秘法，化為一個個致命的法術，而神祕多變的%m幽影法師%w便是其名號。遇上戰鬥時，各種不同元素的%o魔法屬性攻擊%w從幽影法師手底釋放而出，可發揮驚人的威力以及各種不可思議的效果，使敵人在近身之前就倒地不起。",
    "%l渾沌議會%w裡難以察覺，卻又不得不提防其存在的勢力，也就是令人聞風喪膽的%m深淵刺客%w們，其一生追求的是自身極致的渾沌力量。進入戰鬥之時，刺客將過人的速度體現在%o隱匿暗殺%w之上，敵人常常在一個眨眼之間就丟失了刺客的蹤跡，或者遭到從背後而來的匕首刺穿。",
    "%g科學基地%w中有著許多對機械狂熱的極端份子，他們總是自稱為%m齒輪大師%w；這些人常常%o召喚機械公仔%w彼此炫耀較勁。進入戰鬥時，齒輪大師喜歡運用各種科技產品來消滅敵人，更能夠活用機械扳手來進行修復及輔助的工作，他與公仔間的搭配就彷彿是一個小團隊般，無懈可擊。",
    "%g科學基地%w內眾人熟知的%m煉金術士%w，身兼生命體以及煉金術研究兩大專長；這群手持雙槍的學者們為了捕抓動植物進行研究，培養出了一副媲美專業獵人的好身手。至於戰鬥，煉金術士可以憑藉的豐富藥學知識來進行各種%o狀態輔助%w，不論是對友軍治療，抑或是對敵人下毒。",
  };
  public static readonly string[] Str2056_0233 =
  {
    "堅甲職業", "輕甲職業", "輕甲職業", "堅甲職業", "堅甲職業", "輕甲職業"
  };
  public static readonly string[] Str2056_0234 =
  {
    "對敵進行強烈的懲戒打擊", "以美妙歌聲療癒傷口", "各式魔法元素攻擊", "防不勝防的隱匿暗殺", "召喚機器公仔助陣", "遠距襲擊的槍彈攻勢"
  };
  public static readonly string[] Str2056_0235 =
  {
    "以盾牌格擋抵禦傷害", "施展光明之盾守護隊友", "變幻法術進行牽制或輔助", "疾速的移動與殘影分身", "各式爆破科技運用", "奇特的煉金法術與藥劑"
  };
  public const string Str2056_0236="目前職業已為{0}";
  public const string Str2056_0237="請選擇職業";
  public const string Str2056_0238="成功轉職為{0}";
  public const string Str2056_0239="欲煉金物品無法轉換";
  public const string Str2056_0240="找不到符合條件的煉金物品";
  public const string Str2056_0241="%w角色星盤上所有%l星石與技能效果%w將於轉換為新職業時%r全部重置歸零%w，是否確定要轉換職業？";
  public const string Str2056_0242="水晶重置儀式";
  public const string Str2056_0243="請放置欲重置的水晶";
  public const string Str2056_0244="※請雙擊或直接拖曳欲重置的水晶或鑲嵌有水晶的裝備至左方欄位";
  public const string Str2056_0245="啟動";
  public const string Str2056_0246="再次啟動";
  public const string Str2056_0247="保留此效果";
  public const string Str2056_0248="原效果";
  public const string Str2056_0249="新效果";
  public const string Str2056_0250="是否花費銀幣{0}來進行水晶效果重置？";
  public const string Str2056_0251="銀幣不足";
//public const string Str2056_0252="";
  public const string Str2056_0253="水晶靈魂已滿，可進行鑲嵌";
  public const string Str2056_0254="水晶鑲嵌成功";
  public const string Str2056_0255="水晶鑲嵌失敗";
  public const string Str2056_0256="水晶尚未覺醒";
  public const string Str2056_0257="水晶裝備成功";
  public const string Str2056_0258="水晶已卸下";
  public const string Str2056_0259="請先卸下水晶";
  public const string Str2056_0260="卸除水晶";
  public static string[] Str2056_0261=
  {
    "[[[size=15 color=240,80,80]]]不可轉移[[[size=18 color=00FFFFFF]]] ",               //0:Limit 2
    "[[[size=15 color=240,80,80]]]不可拍賣/寄送[[[size=18 color=00FFFFFF]]] ",          //1:Limit 3
    "[[[size=15 color=240,80,80]]]不可刪除/轉移[[[size=18 color=00FFFFFF]]] ",          //2:Limit 4
    "[[[size=15 color=240,80,80]]]不可賣商店/寄送[[[size=18 color=00FFFFFF]]] ",         //3:Limit 5
    "[[[size=15 color=240,80,80]]]不可賣商店[[[size=18 color=00FFFFFF]]] ",              //4:Limit 6
    "[[[size=15 color=240,80,80]]]體驗中[[[size=18 color=00FFFFFF]]] ",                 //5:Limit 7
    "[[[size=15 color=240,80,80]]]體驗中/不可賣商店/拍賣/寄送[[[size=18 color=00FFFFFF]]] ",//6:Limit 8
    "[[[size=15 color=240,80,80]]]不可賣商店/拍賣[[[size=18 color=00FFFFFF]]] ",        //7:Limit 9
    "[[[size=15 color=240,80,80]]]不可拍賣[[[size=18 color=00FFFFFF]]] ",               //8:Limit 10
    "[[[size=15 color=240,80,80]]]不可賣商店[[[size=18 color=00FFFFFF]]] ",              //9:Limit 11
    "[[[size=15 color=240,80,80]]]不可賣商店/拍賣/寄送[[[size=18 color=00FFFFFF]]] ",   //10:Limit 12
    "[[[size=15 color=240,80,80]]]不可拍賣/寄送[[[size=18 color=00FFFFFF]]] ",          //11:Limit 13
    "[[[size=15 color=240,80,80]]]體驗中/不可轉移[[[size=18 color=00FFFFFF]]] ",         //12:Limit 14
  };
  public static string[] Str2056_0262=
  {
    "[[[size=15 align=center color=255,204,0]]]{0}",
    "<雙擊左鍵使用>", //1
    "", //2
    "<雙擊左鍵使用>", //3
    "<單擊右鍵自動使用>", //4
    "<雙擊左鍵合成>", //5
    "<單擊左鍵預覽>", //6
    "<雙擊左鍵置入材料櫃>",  //7
  };
  public const string Str2056_0264="水晶挖掘成功";
  public const string Str2056_0265="水晶挖掘失敗";
  public const string Str2056_0266="[[[color=213,87,74 align=center]]]獲得物品："; // fs 12/08/15 獲得物品的圖示訊息文字
  public const string Str2056_0267="水晶效果已重置";
  public const string Str2056_0268="水晶效果已變更";
  public const string Str2056_0269="找不到可使用的物品";
  public const string Str2056_0270="此物品無法置入煉金釜";
  public const string Str2056_0271="此物品無法置入";
  public const string Str2056_0272="獲得物品數量錯誤";
  public const string Str2056_0273="職業不符，無法獲得物品";
  public const string Str2056_0274="角色尚未選擇職業";
  public const string Str2056_0275="黑爵卡";
  public const string Str2056_0276="使用";
  public static string[] Str2056_0277=
  {
    "黃金之眼",
    "時空之鑰",
    "召喚之門",
    "友情之鏈",
  };
  public static string[] Str2056_0278=
  {
    "[[[size=15 color=200,185,165]]]使用黃金之眼後，在購物中心及魔法衣櫥內購買商品皆享九折優惠",
    "[[[size=15 color=200,185,165]]]使用後可（無限次數）傳送至主城、及各地圖",
    "[[[size=15 color=200,185,165]]]使用後可進入秘境探險，可獲得特殊的寶物（每日一次）",
    "[[[size=15 color=200,185,165]]]使用後可呼叫複數好友到身邊，最多10名好友（每日三次）",
  };
  public const string Str2056_0279="說明";
  public const string Str2056_0280="此功能無法使用";
  public const string Str2056_0281="友情鏈鎖";
  public const string Str2056_0282="剩餘招喚次數 {0}/{1}";
  public const string Str2056_0283="今日使用次數已滿";
  public const string Str2056_0284="[[[align=center paraGap=8 color=B2EC0A]]]『{0}』|%w[[[size=15]]]對您進行招喚，是否接受？|[[[color=FF0000]]]";
  public const string Str2056_0285="秒%w後自動拒絕";
  public const string Str2056_0286="黑爵卡購物折扣已結束";
  public const string Str2056_0287="目前場景無法使用此功能";
  public const string Str2056_0288="目前狀況無法使用此功能";
  public const string Str2056_0289="殘頁存放區擴充成功";
  public const string Str2056_0290="您沒有邀請好友";
  public const string Str2056_0291="超過邀請人數限制";
  public const string Str2056_0292="您沒有招喚好友";
  public const string Str2056_0293="已送出邀請";
  public const string Str2056_0294="黃金之眼已使用";
  public const string Str2056_0295="黃金之眼已停止使用";
  public const string Str2056_0296="[[[size=15 lineGap=2 color=245,160,35]]]使用說明：|%w可以置入物品及材料，煉出其它物品。|[[[color=245,160,35]]]材料：|%w獨角獸之角的煉成率為100%。|元素怪粉塵有機率煉出原始物品。";
  public const string Str2056_0297="殘頁存放區";
  public static string[] Str2056_0298={"重整", "魔法故事書", "殘頁存放區擴充", "魔法故事書(H)"};
  public static string[] Str2056_0299={"故事書抄寫", "開始抄寫", "請放入欲抄寫的魔法故事書"};
  public const string Str2056_0300="您沒有放入魔法故事書";
  public const string Str2056_0301="您沒有手抄紙";
  public const string Str2056_0302="此魔法故事書無法抄寫";
  public const string Str2056_0303="此手抄紙無法使用";
  public const string Str2056_0304="物品已更動或不存在，請重新再試";
  public const string Str2056_0305="[[[align=center color=FFFFFF]]]是否確認抄寫【{0}】？|[[[color=FFFF00]]](需扣除手抄紙*1)";
  public const string Str2056_0306="抄寫完成！獲得【{0}】X {1}";
  public const string Str2056_0307="抄寫爆發！恭喜獲得【{0}】X {1}";
  public const string Str2056_0308="背包空間不足，最少需有一格空間";
  public static string[] Str2056_0309=
  {
    "單載陸行-{0}",              //0
    "單載陸行-{0}({1}天)",       //1
    "單載飛行-{0}",              //2
    "單載飛行-{0}({1}天)",       //3
    "單載陸空-{0}",              //4
    "單載陸空-{0}({1}天)",       //5
    "雙載陸行-{0}",              //6
    "雙載陸行-{0}({1}天)",       //7
    "雙載飛行-{0}",              //8
    "雙載飛行-{0}({1}天)",       //9
    "雙載陸空-{0}",              //10
    "雙載陸空-{0}({1}天)",       //11
    "多載陸行-{0}",              //12
    "多載陸行-{0}({1}天)",       //13
    "多載飛行-{0}",              //14
    "多載飛行-{0}({1}天)",       //15
    "多載陸空-{0}",              //16
    "多載陸空-{0}({1}天)",       //17
    "大師工具-{0}",              //18
    "大師材料-{0}",              //19
    "怪物故事書-{0}",            //20
    "裝備故事書-{0}",            //21
    "技能故事書-{0}",            //22
    "{0}(藍)",                  //23.裝備名稱(顏色)
    "{0}(紫)",                  //24.裝備名稱(顏色)
    "{0}(金)",                  //25.裝備名稱(顏色)
    "體驗座騎-{0}({1}天)",       //26
  };
  public const string Str2056_0310="目前背包狀態無法使用神器";
  public const string Str2056_0311="物品已鎖定，無法使用神器";
  public const string Str2056_0312="請先進入騎士團再使用！";
  public const string Str2056_0313="請先進入文明之塔再使用！";
  public const string Str2056_0314="擦拭神器後神器變得亮晶晶，取得神器經驗值！";
  public const string Str2056_0315="[[[color=245,160,35]]]單擊一下%w擦拭布，進行擦拭神器。|擦拭神器可以[[[color=245,160,35]]]提升神器的經驗值%w，|並且有機率獲得其他驚喜！";
  public static string[] Str2056_0316=
  {
    "[[[size=22 color=255,245,83]]]{0}|[[[size=16]]]%w{1}|{2}|{3}|{4}|[[[color=170,225,55]]]{5}{6}{7}",   //0
    "神器等級：{0}", //1
    "最高等級：{0}", //2
    "累積經驗：{0}|升級經驗：{1}", //3
    "累積經驗：{0}|升級經驗：已達最高等級",  //4
    "", //5
    "冷卻倒數：{0:0}分", //6
    "冷卻倒數：{0:0.00}秒", //7
    "||[[[color=145,135,130]]]提升至{0}級後效果|{1}",  //8
    "||[[[color=255,45,45]]]＊擦拭神器可取得[[[color=255,240,0]]]神器經驗值", //9
    "|[[[color=255,45,45]]]＊神器冷卻時間為{0}分，每次玩家等級提升，冷卻時間會重置1次", //10
    "|[[[color=255,45,45]]]＊神器冷卻時間為{0}分，每次玩家建造樓層，冷卻時間會重置1次", //11.槌子
    "|[[[color=255,45,45]]]＊[[[color=255,240,0]]]目前等級[[[color=255,45,45]]]達上限後，可透過冒險地圖的古老神喻之石，提升神器[[[color=255,240,0]]]最高等級[[[color=255,45,45]]](擦拭神器可繼續升級)", //12
  };
  public const string Str2056_0317="目前背包狀態無法開啟殘頁存放區";
  public const string Str2056_0318="此物品無法發動技能";
  public const string Str2056_0319="您尚未選擇職業，無法使用此物品";
  public const string Str2056_0320="確認魔化「{0}」？";
  public const string Str2056_0321="您沒有指定「裝備魔法故事書」";
  public const string Str2056_0322="您指定的「裝備魔法故事書」無法使用";
  public const string Str2056_0323="您選擇的物品無法進行魔化";
  public const string Str2056_0324="恭喜！裝備魔化成功";
  public const string Str2056_0325="魔化失敗";
  public const string Str2056_0326="此物品無法放入殘頁存放區";
  public const string Str2056_0327="指定目標位置已有物品";
  public const string Str2056_0328="殘頁存放空間已滿";
  public const string Str2056_0329="此物品無法操作";
  public const string Str2056_0330="殘頁已重整";
  public const string Str2056_0331="超過重整等待時間，停止重整";
  public const string Str2056_0332="殘頁重整中，請稍候";
  public const string Str2056_0333="物品資料不正確";
  public const string Str2056_0334="已擁有此表情符號";
  public const string Str2056_0335="獲得「{0}」表情符號";
  public const string Str2056_0336="條件不符，無法擴充";
  public const string Str2056_0337="[[[align=center lineGap=4 color=255,255,255]]]是否永久擴充殘頁存放區{0}格？|[[[color=255,255,0]]](需扣除{1}金幣)";
  public const string Str2056_0338="進場景準備中，目前角色狀態無法進行騎乘";
  public static string[] Str2056_0339=
  {
    "怪物篇", "裝備篇", "職業篇"
  };
  public static string[] Str2056_0340=
  {
    "創世之章", //1
    "黎明平原之章", //2
    "聖靈之山之章", //3
    "繁星沙漠之章", //4
    "星月山谷之章", //5
    "銳風之峽之章", //6
    "微光森林之章", //7
    "鹽湖之章",     //8
    "石灰洞穴之章", //9
    "舊王城之章", //10
    "無底沼澤之章", //11
    "失落之島之章", //12
    "流沙洞穴之章", //13
    "巨人遺跡之章", //14
    "(噩)黎明平原之章",  //15
    "(噩)聖靈之山之章",  //16
    "(噩)繁星沙漠之章",  //17
    "(噩)星月山谷之章",  //18
    "(噩)銳風之峽之章",  //19
    "(噩)微光森林之章",  //20
    "(噩)鹽湖之章",      //21
    "(噩)石灰洞穴之章",  //22
    "(噩)舊王城之章",   //23
    "(噩)失落之島之章",  //24
    "(獄)黎明平原之章",  //25
    "(獄)聖靈之山之章",  //26
    "(獄)繁星沙漠之章",  //27
    "(獄)星月山谷之章",  //28
    "(獄)銳風之峽之章",  //29
    "(獄)微光森林之章",  //30
    "(獄)鹽湖之章",    //31
    "(獄)石灰洞穴之章",  //32
    "(獄)舊王城之章 ",    //33
    "(獄)失落之島之章",  //34
  };
  public static string[] Str2056_0341=
  {
    "一般探索進度", "噩夢探索進度", "地獄探索進度"
  };
  public static string[] Str2056_0342=
  {
    "故事書功能", //0
    "此故事書已使用次數", //1
    "故事書適用裝備", //2
    "技能說明", //3
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]{0}",  //4
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]{0}",  //5
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]冷卻：{0:0.00}秒|距離：{1:0}|{2}", //6
    "[[[size=20 color=0,0,0 outlineColor=00000000]]]~謎樣的章節~|[[[size=15]]]   尚未解開魔法文字枷鎖|",  //7
    "[[[size=15 align=right paraGap=8 lineGap=2 color=0,0,255 outlineColor=00000000 underline=0,0,255]]]初次裝訂此故事書可解開魔法文字枷鎖|[[[underline=0,0,255]]]累積解鎖數量可獲得探索效果",  //8
    "集滿殘頁可點擊裝訂成故事書", //9
    "裝訂", //10
    "對應殘頁不足！", //11
    "尚未解開章節地圖", //12
    "故事探索進度", //13
    "現有星星數：{0}", //14
    "現有探索效果", //15
    "條件", //16
    "效果", //17
    "星星數：{0:#0}", //18
    "恭喜開啟「{0}」故事", //19
    "獲得【{0}】", //20
    "通過前張地圖副本／首領戰關卡即可解鎖", //21
    "無",  //22
    "[[[align=center color=FFFFFFFF]]]是否花費銀幣提前開放「[[[color=FFFFFF00]]]{0}[[[color=FFFFFFFF]]]」探索章節？|[[[color=FFFFFF00]]](需扣除{1:#,#}銀幣)",  //23
    "此章節已解鎖", //24
    "成功解開故事章節", //25
    "尚未開放探索", //26
    "此故事書已使用次數({0})", //27
    "此故事書已使用次數({0}/{1})", //28
  };
  public const string Str2056_0343="未達使用次數，無法使用";
  public const string Str2056_0344="使用次數已滿";
  public const string Str2056_0345="目前狀況無法進行物品刪除";
  public const string Str2056_0346="目前狀況無法進行物品搬移";
  public const string Str2056_0347="跳躍狀態無法進行騎乘";
  public const string Str2056_0348="強化";
  public const string Str2056_0349="物品已鎖住";
  public const string Str2056_0350="此物品狀態無法進行操作";
  public const string Str2056_0351="座騎騎乘中，無法使用";
  public const string Str2056_0352="美體美容進行中，無法進行騎乘";
  public const string Str2056_0353="[[[color=245,160,35]]]可以置入材料：|%w獨角獸之角|元素怪粉塵";
  public const string Str2056_0354="染髮進行中，無法進行騎乘";
  public const string Str2056_0355="玩家飛行狀態無法傳送";
  public const string Str2056_0356="飛行狀態中，無法使用";
  public const string Str2056_0357="尚未突破地圖關卡，無法前往此區域";
  public const string Str2056_0358="補充天數超過目前所需，超過部分將無法保留，確定要使用嗎？";
  public const string Str2056_0359="%w是否花費{0}金幣來進行[[[color=255,255,0]]]抗性數值重置？";
  public const string Str2056_0360="%w是否花費{0}金幣來進行[[[color=255,255,0]]]隨機效果重置？";
  public const string Str2056_0361="成功延長座騎剩餘使用時間";
  public const string Str2056_0362="使用永生藥水失敗！";
  public const string Str2056_0363="任務已滿，請刪除或解完任務後再進行";
//public const string Str2056_0364="";
  public const string Str2056_0365="堆疊數量不足，無法合成！";
  public const string Str2056_0366="鑰匙不足，無法開啟";
  public const string Str2056_0367="背包物品異常（位置={0}），無法重整";
  public const string Str2056_0368="點擊可連結查看現有探索效果";
  public const string Str2056_0369="神器不存在！";
  public const string Str2056_0370="無法再取得更多神器經驗";
  public const string Str2056_0371="已擁有此標記";
  public const string Str2056_0372="飛行狀態無法更換騎乘";
  public const string Str2056_0373="落地中，無法使用物品";
  public const string Str2056_0374="兌換銀幣中，請稍候";
  public const string Str2056_0375="物品使用失敗，停止兌換銀幣";
  public const string Str2056_0376="物品使用失敗，停止開啟福袋";
  public const string Str2056_0377="銀幣已達上限，無法使用";
  public const string Str2056_0378="獎勵金幣已達上限";
  public const string Str2056_0379="角色需達VIP2星，方可開啟此功能";
  public const string Str2056_0380="已達需求數量";
  public const string Str2056_0381="購買";
  public const string Str2056_0382="獲得神器經驗 {0}";
  public const string Str2056_0383="使用黑貓陶罐召喚出黑貓偷竊財物";
  public const string Str2056_0384="購買成功！存入殘頁存放區！";
  public const string Str2056_0385="您已經加冕過此頭銜！";
  public const string Str2056_0386="稱號資料不正確";
  public const string Str2056_0387="超出購買上限";
  public const string Str2056_0388="騎乘中，無法使用此物品";
  public const string Str2056_0389="此場景無法使用傳送";
  public const string Str2056_0390="欲兌換的銀幣已超過銀幣上限";
  public const string Str2056_0391="此場景無法使用隨身銀行";
  public const string Str2056_0392="請先離開障礙點，再進入金幣遊樂場";
  public const string Str2056_0393="戰鬥狀態無法進行騎乘";
  public const string Str2056_0394="轉換成功！";
  public const string Str2056_0395="請先卸除水晶才可進行拆解";
  public const string Str2056_0396="您已經死了，無法開啟背包";
  public const string Str2056_0397="落地中，無法使用隨身銀行";
  public const string Str2056_0398="權力點已滿，無法使用";
  public const string Str2056_0399="身上銀幣已達上限";
  public const string Str2056_0400="目標玩家所在場景不允許傳送";
  public const string Str2056_0401="【{0}-{1}級】{2}";
  public const string Str2056_0402="進場景中，無法進行騎乘";
  public const string Str2056_0403="與原有髮色相同，請重新設定";
  public const string Str2056_0404="與原有造型相同，請重新設定";
  public const string Str2056_0405="空水晶無法進行吸取";
  public const string Str2056_0406="[[[size=14]]]%w是否要對「[[[color={0}]]]{1}」%w進行靈魂吸取？||[[[color=255,70,70]]]備註：水晶將被銷毀！";
  public const string Str2056_0407="需先裝備水晶";
  public const string Str2056_0408="容器沒有靈魂值";
  public const string Str2056_0409="水晶靈魂已滿";
  public const string Str2056_0410="水晶已成功吸取{0}靈魂值";
  public const string Str2056_0411="目前狀況無法進行騎乘";
  public const string Str2056_0412="價格異常";
  public const string Str2056_0413="職業不符，無法使用物品";
  public const string Str2056_0414="角色需達{0}級方可開啟此功能";
  public const string Str2056_0415="找不到技能星石資料";
  public const string Str2056_0416="此物品無法喚醒星盤的星石";
  public const string Str2056_0417="職業不符，無法喚醒星盤的星石";
  public const string Str2056_0418="您已經喚醒過此星！";
  public const string Str2056_0419="[[[size=14]]]補充靈魂值超過目前所需，超過部份將無法保留，確定要使用嗎？";
  public const string Str2056_0420="目前場景無法使用神器";
  public const string Str2056_0421="點擊此按鈕更換";
  public const string Str2056_0422="事件進行中，無法使用傳送功能";
  public const string Str2056_0423="蒸汽齒輪啟動中，無法使用此功能";
  public const string Str2056_0424="充能膠囊已設定，請先取消再進行設定";
  public const string Str2056_0425="增幅膠囊已設定，請先取消再進行設定";
  public const string Str2056_0426="成功設定充能膠囊";
  public const string Str2056_0427="成功設定增幅膠囊";
  public const string Str2056_0428="取消使用充能膠囊";
  public const string Str2056_0429="取消使用增幅膠囊";
  public const string Str2056_0430="扣除失敗，取消使用充能膠囊";
  public const string Str2056_0431="扣除失敗，取消使用增幅膠囊";
  public const string Str2056_0432="強度等級";
  public const string Str2056_0433="擴充次數已達上限";
  public const string Str2056_0434="角色需達VIP{0}星，方可進行擴充";
  public const string Str2056_0435="角色需達VIP{0}星，方可使用隨身銀行";
  public const string Str2056_0436="兌換物品中，請稍候";
  public const string Str2056_0437="請輸入兌換數量：";
  public const string Str2056_0438="物品使用失敗，停止兌換";
  public const string Str2056_0439="兌換失敗";
  public const string Str2056_0440="指定兌換物異常，無法兌換";
  public const string Str2056_0441="找不到符合條件物品";
  public const string Str2056_0442="兌換超過等待時間，停止兌換";
  public const string Str2056_0443="兌換完成";
  public const string Str2056_0444="拜訪的民眾已達上限，無法使用邀請函";
  public const string Str2056_0445="已超過命運籌碼上限";
  public static string[] Str2056_0446=
  {
    "神奇輪轉石",  //0
    "轉換",   //1
    "目前神奇輪轉石數量：", //2
    "{0} 個", //3
    "欲轉換的裝備：", //4
    "← 請置入裝備", // 5
    "請選擇欲轉換的屬性：", //6
    "請選擇欲轉換的屬性", //7
    "無法轉換多個屬性", //8
    "您沒有指定「神奇輪轉石」", //9
    "神奇輪轉石不足", //10
    "欲轉換物品無法轉換", //11
    "使用神奇輪轉石成功轉換星石效果！", //12
  };
#endregion Str2056

#region ITEM_MSG
  public static string[] ITEM_MSG001=
  {
    "使用物品錯誤代碼：{0}", //
    "無此物品", //1
    "物品數量不足", //2
    "物品使用中", //3
    "物品使用失敗", //4
    "無法使用此物品", //5
    "冷卻時間不足，請稍後再試", //6
    "無此角色或角色不在線上", //7
    "身上物品空間不足,無法使用",  //8
    "已超出身上最大技能點數上限", //9
    "注意!你開寶箱獲得的寶物太貴重,而你負重不足,所以東西掉在地上", //10
    "未開啟銀行無法使用", //11
    "注意!你開福袋獲得的寶物太貴重,而你負重不足,所以東西掉在地上", //12
    "已達累計上限", //13
    "金錢不足", //14
    "金錢上限不足", //15
    "身上負重不足", //16
    "寵物數量已滿", //17
    "身上租賃背包數量已滿", //18
    "銀行格數已達上限", //19
    "金幣不足", //20
    "達英雄帖上限", //21
    "沒有幫會", //22
    "身上已有效果", //23
    "須先設置重生點才可使用", //24
    "隊友不同場景不能使用", //25
    "物品狀態不符", //26
    "物品類型不符", //27
    "物品條件不足", //28
    "效果累計時間已滿", //29
    "鑰匙錯誤，開啟寶箱失敗", //30
    "", //31
    "", //32
    "沒有伴侶，不能使用", //33
    "伴侶不在線上", //34
    "伴侶在特殊場景內，無法傳送", //35
    "今日使用副本重置丹已達上限", //36
    "戰意已滿", //37
    "狀態已滿無法增加", //38
    "狀態數量", //39
    "線上時間4小時才能再用", //40
    "物品條件不符", //41    //虛寶擴充(73)
    "造型無變更", //42    //虛寶的體型置換(75)
    "奇緣題庫已擴充", //43
    "場景招喚數量已達上限", //44
    "伺服器寵物滿載,請稍後再試", //45
    "經驗值已達上限，無法使用物品", //46
    "銀幣已達上限,無法使用", //47
    "職業不符,無法使用", //48
    "已擁有此造型", //49    //虛寶造型兌換卡(77)
    "超過不可交易金幣上限", //50
  };
#endregion ITEM_MSG

#region Unlink use
  public const string UnStr_0002 = "社群";
  public static readonly string[] UnStr_0301 = new string[] {"無此角色或角色不在線上", "好友名單數量已達上限",  ".對方好友名單數量已達上限",
		"對方拒絕加入好友",  "對方被詢問成為好友中",  "邀請你成為好友的玩家已下線",  "對方未送出好友邀請",   "無此好友，無法刪除",  "設定群組名稱失敗",  "設定好友群組失敗",  "gm無法成為好友", "目標玩家所在場景不允許追蹤傳送",
		"目標玩家不在線上", "金葉值不足", "目前玩家所在場景不允許被呼叫", "目前玩家狀態不允許被呼叫", "忙碌中，無法加好友", "設定臨時好友開關成功",  "對方拒絕你傳送至身旁", "對方忙碌中無法傳送詢問", "等級不足,無法傳送", "接收的訊息錯誤"};
  public static string[] UnStr_0302 = new string[3]{"[[[align=center]]][[[paraGap=8]]] [[[color=B2EC0A]]]『", "』|%w邀請你成為他的%o好友|%w於%o", "秒%w後自動拒絕"};
  public const string UnStr_0303 = "對 {0} 送出成為好友的要求";
  public static string[] UnStr_0304 = new string[2]{"加入好友","成功"};
  public static string[] UnStr_0305 = new string[2]{"刪除好友","成功"};
  public const string UnStr_0306 = "加到待邀請區";
  public const string UnStr_0307 = "待邀請區為空的，請選擇要邀請的角色";
  public static string[] UnStr_0308 = new string[3]{"對 ", " 等人...", " 送出好友邀請。"};
  public static string[] UnStr_0309 = new string[5]{"無相近等級玩家 ", "無相近生活技能玩家", "好友未上線或不在同分流", "沒有Facebook朋友", "該區玩家已全數邀請"};
  public const string UnStr_0310 = "好友 {0} 上線";
  public const string UnStr_0311 = "上線 {0} 人\n離線 {1} 人";
  public static string[] UnStr_0312 = new string[3]{"亞特蘭提斯", "我在亞特蘭提斯的{0}伺服器，希望你前來與我一起遊玩", "RoleID={0},RoleName={1},ServerName={2}"};
  public static string[] UnStr_1001 = new string[2]{"好友列表","副本列表"};
  public static string[] UnStr_1002 = new string[4]{"等級","名氣值","FB","全部選取/取消"};
  public static string[] UnStr_1003 = new string[3]{"送出邀請","返回", "招喚"};
  public static string[] UnStr_1004 = new string[2]{"好友","/200"};
  public const string UnStr_RecommandFilterType = "依條件搜尋：";
  public const string UnStr_AddFreindByNameDescription = "輸入欲新增好友ID...";
  public static string[] UnStr_0101 = new string[2]{"ConfirmSendInvitation", "玩家角色名稱"};
  public static string[] UnStr_0102 = new string[2]{"ComfirmDeleteFriend", "[[[align=center]]][[[paraGap=8]]] 確定要結束與|[[[color=B2EC0A]]]『{0}』|%w的好友關係"}; 
  public static string[] UnStr_0103 = new string[4]{"所在地區無法進入", "騎乘坐騎時無法進入", "須先完成薩亞城的「文明寶典」任務", "戰鬥中無法進入"};
  public static string[] UnStr_0001 = new string[2]{"新增好友","搜尋好友"};
  public const string UnStr_RequireLevel = "《需達等級{0}級》";
  public static string[] UnStr_0401 = new string[7]{"Lv", "上線", "離線", "在線", "未記錄", "第{0}分流", "組隊次數: {0}"};
  public static string[] UnStr_0402 = new string[3]{"秒", "分鐘", "小時"};
  public const string UnStr_0403 = "系統推薦朋友\n請點選『+』加為好友";
  public static string[] UnStr_0404 = new string[6]{"推薦和自己等級相近的玩家", "推薦和自己生活技能相近的玩家", "自己Facebook的好友", "將全部推薦好友都加到待邀請區", "對邀請區玩家送出邀請", "返回好友清單"};
  public static string[] UnStr_0405 = new string[7]{"文明之塔", "騎士團", "飛天搶金", "聖光鬥技", "攻堅行動", "節慶活動", "即刻報到"};
  public static string[] UnStr_GroupFunctionHints = new string[7]{"重建帝國的大師之路", "維護亞特的世界和平", "你今天飛過了嗎？", "拿起你的武器，準備決鬥！", "拯救王子大作戰", "", "快來報到，王子給獎！"};
  public static string[] UnStr_GroupFunctionDescs = new string[7]{"", "", "", "點選此按鈕報名參加「聖光鬥技」： |%b<規則>%w |1.報名後需等待系統自動配對 |2.進場後直接進行對戰，時間內打倒對方者獲勝 |3.時間到，勝負依血量判斷 <獎勵> 依狀況獲得：%y經驗值%w、%y鬥技表揚證明%w、%y鬥技福袋%w、%y鬥技驚喜袋%w(內含%p「神織設計圖」%w或%p「紫色披風」%w) |4.%r每天晚上7點到晚上12點開放報名進場%w，系統將%r每十分鐘%w配對一次。|※詳細規則詳見官網", "" ,"" ,""};
  public const string UnStr_ActivityInactive = "非活動開放期間";
  public static string[] UnStr_0501 = new string[3]{"不可以加自己為好友", "不能邀請已經為朋友的玩家", "發送邀請太頻繁，請於{0}秒後再發送"};
  public static string[] UnStr_0601 = new string[4]{"密語", "邀請組隊", "好友資訊", "刪除好友"};
  public const string UnStr_0701 = "正在讀取外觀中";
  public const string UnStr_0702 = "觀看玩家資訊";
  public const string UnStr_0703 = "請{0}秒後再要求觀看個人資訊";
  public static string[] UnStr_0801 = new string[2]{"[[[align=center]]][[[paraGap=8]]][[[color=B2EC0A]]]『{0}』|%w邀請您共乘坐騎|%r", "%w秒後自動取消"};
  public static string[] UnStr_0802 = new string[14]{"已有座騎無法搭載", "距離過遠邀請失敗", "對象忙碌中", "對象狀態異常", "雙載對象不在相同場景", "對象拒絕搭載", "目前狀態其他詢問中", "對象忙碌中", "搭載對象位置已滿", "被請下馬", "乘客下馬", "成功送出邀請", "對象狀態無法邀請", "對象下載中無法邀請"};
  public static string[] UnStr_0803 = new string[3] {"自行離開座騎", "乘客 {0} 離開座騎", "被騎乘者驅逐"};
  public const string UnStr_0901 = "機會";
  public const string UnStr_0902 = "命運";
  public const string UnStr_0903 = "機會 V.S. 命運";
  public static string[] UnStr_0904 = new string[3] {"金幣", "經驗值", "銀幣"};
  public const string UnStr_0905 = "選擇機會或命運，幸運隨之降臨";
  public const string UnStr_0906 = "背包已滿，請留空位後在繼續";
  public const string UnStr_0907 = "金幣已達上限，請保留空間後後在繼續";
  public const string UnStr_0908 = "獲得獎勵";
  public const string UnStr_0909 = "你選擇{0}，獲得{1}，已經加到你身上囉";
  public const string UnStr_0910 = "離開";
  public static string[] UnStr_1101 = new string[2]{"恭喜你製造出{0}，已經放到你的背包中", "合成失敗，救回一個材料{0}"};
  public const string UnStr_1102 = "磁歐動力混合機";
  public static string[] UnStr_1103 = new string[10]{"傳說之章", "武器之章", "防具之章", "飾品之章", "一般食品", "魔法食品", "特產之章", "元素之章", "機械之章", "服飾之章"};
  public const string UnStr_1104 = "材料區";
  public static string[] UnStr_1105 = new string[4]{"需放上三樣材料才能開始混合", "能量不足，不能使用混合機", "要用來混合的材料不足", "材料櫃或背包空間不足，無法使用混合機"};
  public const string UnStr_1106 = "請拉動混合機的拉桿開始混合";
  public const string UnStr_1107 = "Atlantis Meta-technology Research";
  public static string[] UnStr_1108 = new string[2]{"在舊大陸沉沒之後，我嘗試利用煉金混合機將從文明之塔獲得的材料，重新組合，希望可以重現各種曾經為亞特蘭提斯王國帶來繁榮與便利生活的，各種器具與物品。", "[[[paraGap=8]]][[[color=B2EC0A]]][放入物品]: 點擊材料區物品|[混合方式]: 點擊混合機的操作桿"};
  public const string UnStr_1109 = "已獲得配方";
  public const string UnStr_1110 = "？？？？？？？？？？";
  public const string UnStr_1111 = "返回分類列表";
  public const string UnStr_1112 = "返回章節分類";
  public static string[] UnStr_1113 = new string[4]{"類型：{0}", "強度等級：{0}", "回收價: {0}", "合成機率: {0}%"};
  public const string UnStr_1114 = "請再試一次或重新開啟介面";
  public static string[] UnStr_1115 = new string[2]{"製作方法", "原料來源"};
  public const string UnStr_1116 = "配方表";
  public const string UnStr_1117 = "可用來合成下列物品";
  public const string UnStr_1118 = "%j獲得新配方|%l{0}%j！！！！";
  public const string UnStr_CraftLimit = "合成限制：";
  public const string UnStr_Comma = "、";
  public static string[] UnStr_CraftFails = new string[2]{"這樣的組合似乎可以做出甚麼東西，可能是我的運氣不好", "這樣的組合似乎可以做出甚麼東西，可能是我的技巧還不夠({0})"};
  public static string[] UnStr_CraftLimits = new string[3]{"名氣值不足{0}", "工具階段不足", "需先習得配方"};
  public const string UnStr_1119 = "確認";
  public const string UnStr_1120 = "材料不足";
  public const string UnStr_1121 = "只顯示已習得配方";
  public const string UnStr_SearchRecipe = "關鍵字搜尋配方";
  public const string UnStr_AutoCraft = "自動合成";
  public const string UnStr_AutoCraftHint = "點擊自動放上材料合成該項物品";
  public const string UnStr_MultiCraft = "連續合成";
  public const string UnStr_MultiCraftHint = "";
  public const string UnStr_HasVisionProduction = "此為已習得配方";
  public const string UnStr_NoVisionProduction = "這樣的組合可以做出什麼呢？";
  public const string UnStr_MultiCraftInfoText = "合成%y{0}|%w請輸入數量";
  public const string UnStr_MultiCraftMsg = "合成 {0} {1} 次";
  public const string UnStr_MultiCraftLimitHint = "想使用快速的連續合成嗎？快成為尊榮的VIP等級{0}神選者吧！";
  public static string[] UnStr_1201 = new string[7] {"物品無法使用", "2.非儲物櫃物品", "3.儲物櫃位置不對", "4.加儲物櫃物品失敗", "5.數量超過上限", "6.不同物品無法堆疊", "7.沒有適合的擺放空間"};
  public static string[] UnStr_1301 = new string[2] {"[[[size=18]]][[[underline=B5F6FE]]][[[OutlineColor=163640]]][[[color=B5F6FE]]]提示更多...", "[[[size=18]]][[[underline=9EFFC7]]][[[OutlineColor=175B51]]][[[color=9EFFC7]]]提示更多..."};
  public const string UnStr_1302 = "達成";
  public const string UnStr_1303 = "待完成";
  public const string UnStr_1304 = "稍後完成";
  public const string UnStr_1305 = "OK！";
  public const string UnStr_1306 = "飛喵在大師、騎士團或特殊場景中，無法做更多提示";
  public const string UnStr_1307 = "提示更多";
  public static string[] UnStr_1308 = new string[2] {"[[[size=14]]][[[underline=B5F6FE]]][[[OutlineColor=163640]]][[[color=B5F6FE]]]稍後完成", "[[[size=14]]][[[underline=9EFFC7]]][[[OutlineColor=175B51]]][[[color=9EFFC7]]]稍後完成"};
	#region 收件夾
	public const string UnStr_1401 = "訊息";
	public const string UnStr_1402 = "按下同意後，你就可以免費送給好友需要的材料唷!";
	public const string UnStr_1403 = "同意全部";
	public static string[] UnStr_1404 = new string[3]{"接 受", "略 過", "確 認"};
	public const string UnStr_1405 = "儲物箱空間已滿，已無法接受好友的禮物，請清除不需要的工具或材料";
	public static string[] UnStr_1406 = new string[5]{"成功", "系統的收件匣訊息數量已滿", "找不到收件玩家", "今天已經有送同一訊息給此玩家", "收件匣功能暫停中"};
	public const string UnStr_1407 = "{0:0000}年{1:00}月{2:00}日{3:00}時{4:00}分";
	public static string[] UnStr_1408 = new string[3]{"0.成功", "1.跟你不同組伺服器,所以無法成為好友", "2.好友名單數量已達上限"};
	public static string[] UnStr_InboxErrorMsgs = new string[9]{"加儲物櫃物品失敗", "沒有適合的儲物櫃擺放空間", "身上物品已滿", "數量超過上限", "負重不足", "加身上物品失敗", "超過不可交易金幣上限", "超過身上銀幣上限", "其他失敗"};
	public const string UnStr_InboxAcceptAllLimit = "升級VIP就可以使用全部同意的便利功能囉!";
	public const string UnStr_InboxRoyalClubBtn = "皇家俱樂部";
	#endregion
	#region 材料邀請
	public static string[] UnStr_1501 = new string[3]{"已經對 ", " 等人...", " 送出材料邀請。"};
	public const string UnStr_1502 = "詢問好友";
	public const string UnStr_1503 = "請朋友送你";
	public static string[] UnStr_1504 = new string[2]{"送出邀請","返回"};
	public static string[] UnStr_1505 = new string[3]{"將全部推薦好友都加到待邀請區", "對邀請區玩家送出邀請", "返回解鎖介面"};
	public const string UnStr_1506 = "次數 {0}/{1}";
	public const string UnStr_1507 = "可邀請次數已達到上限";
	#endregion
	#region 大師系統
	public static string[] UnStr_1601 = new string[2]{"拜訪", "邀請"};
	public static string[] UnStr_1602 = new string[3]{"好久不見，歡迎你來我家坐坐！", "我的文明之塔需要你的幫忙，請來我家逛逛吧！", "誠摯地邀請你來拜訪我的文明之塔！"};
	public const string UnStr_1603 = "邀請 {0} 來我的文明之塔";
	public static string[] UnStr_1604 = new string[5]{"恭喜", "你成功研發新科技", "文明之塔完成擴建|本次獲得", "你的名氣值到達", "下階段名氣值目標"};
	public static string[] UnStr_1605 = new string[2]{"可獲得銀幣", "可獲得能量"};
	public const string UnStr_1606 = "第{0}層樓";
	public const string UnStr_AssistRewardMsg = "[[[size=22]]]感謝你協助|[[[size=16]]]請收下我的禮物";
	public const string UnStr_TOKVisitLeft = "今天尚可拜訪與協助{0}位，沒有進行過協助的朋友";
	public const string UnStr_CantFriendAssit = "恭喜你已經完成今日好友的協助，每天中午會恢復協助次數，記得要來幫忙你的好友們唷！";
	#endregion
	#region 人民拜訪
	public static string[] UnStr_1701 = new string[6]{"學習{0}的人民", "熱心的人民", "科技大師", "皇家建設隊", "亞特蘭提斯貴族", "亞特蘭提斯貴族"};
	public static string[] UnStr_1702 = new string[4]{"人民拜訪任務成功獲得銀幣", "人民拜訪任務成功獲得熟練度", "人民拜訪任務成功獲得材料", "人民拜訪任務成功獲得金幣"};
	public static string[][] UnStr_1703 = new string[6][]{new string[2]{"[[[align=center]]]%v學習{0}的人民", "%w請將他送到放有「%y{0}%w」的樓層"}, 
	new string[2]{"[[[align=center]]]%v熱心的人民", "%w送他到任何有放工具的樓層他會協助你%y研究其中一件工具一次"}, new string[2]{"[[[align=center]]]%v科技大師", "%w送他到任何有放工具的樓層他會將%y其中一件工具的熟練度提升到滿"}, 
	new string[2]{"[[[align=center]]]%v皇家建設隊", "%w送他到建造中的樓層，可%y縮短建造時間3小時"}, new string[2]{"[[[align=center]]]%v亞特蘭提斯貴族", "%w送他到任何有放工具的樓層他會協助你將該樓層內%y所有工具進行5次研究"}, new string[2]{"[[[align=center]]]%v亞特蘭提斯貴族", "%w送他到任何有放工具的樓層他會協助你將該樓層內%y所有工具進行5次研究"}};
	public static string[]	UnStr_1704 = new string[2]{"Silver + {0}", "Gold + {0}"};
	public const string UnStr_HasVisitor = "有人民到訪";
	#endregion
	#region 榮耀加冕
	public const string UnStr_RoyalTitle = "榮耀加冕";
    public const string UnStr_ChangeTitle = "頭銜更換";
    public const string UnStr_TitleColor = "尊榮色";
	public const string UnStr_OwnSpecialTitle = "已擁有";
	public const string UnStr_FrontTitle = "前銜名";
	public const string UnStr_BackTitle = "後銜名";
	public const string UnStr_SpecialtTitle = "特殊榮耀";
	public const string UnStr_ApplyTitleHint = "點選配戴此頭銜";
	public const string UnStr_DisableTitleHint = "點選卸下此頭銜";
	public const string UnStr_ApplyTitleMsg = "成功更換頭銜！";
	public const string UnStr_DisableTitleMsg = "成功卸下頭銜！";
	public const string UnStr_LiteralTitleEmpty = "即將開放";
	public const string UnStr_SpecialTitleEmpty = "目前尚未擁有特殊榮耀";
	public const string UnStr_OwnTitle = "已加冕";
	public const string UnStr_OwnTitleHint = "已加冕！可點選[頭銜更換]的頁籤前往配戴";
	public const string UnStr_OwnTitleNum = "已加冕的頭銜數量";
	public const string UnStr_EffectHint = "集滿左方5個頭銜，將永久獲得此榮耀效果";
	public const string UnStr_Crown = "加冕";
	public const string UnStr_LearnTitleSuccessful = "加冕成功：{0}";
	public const string UnStr_LearnTitleEffect = "獲得永久榮耀效果：{0}";
	public const string UnStr_HonorInsufficient = "您的榮耀值不足";
	public const string UnStr_DontUse = "不使用";
	public const string UnStr_HonorHelpHint = "榮耀值可由特定的評定任務、活動中獲得";
	public const string UnStr_TitleLevelLimitMsg = "LV32以上的亞特子民將有「榮耀加冕」的資格";
	public const string UnStr_CanLearnTitleMsg = "[[[OutlineColor=EBDBC2]]][[[color=5B200E]]]你達成了[[[color=FF0000]]]{0}[[[color=5B200E]]]的加冕條件。";
	public const string UnStr_OpenRoyalUI = "開啟榮耀加冕";
	public const string UnStr_Finish = "(已完成)";
	public const string UnStr_TitleLimit = "您尚未達成此頭銜的加冕條件";
	public const string UnStr_DontHaveTitle = "尚未擁有";
	public const string UnStr_HadNumTitleMsg = "取得方式：擁有{0}個以上的頭銜";
	public const string UnStr_TitleColorSelectorTitle = "更換尊榮色";
	#endregion
	public static string UnStr_PopupConstructFinish = "{0}%w樓層已經蓋好囉！|%w這次獲得了{1}！";
	public static string UnStr_PopupNewInboxMsg = "你收到了一封新訊息，|快打開訊息介面看看！";
	public const string UnStr_RacingScoreTitle = "目前分數";
	public static string UnStr_PopupNewskill = "%j獲得新勳章||%w{0}%j！！！";
	public static string UnStr_PopupNewchar = "%j獲得新個性||%w{0}%j！！！";
#endregion

#region teamSettingUI
	public const string GreedOrNot_Full = "背包已滿，若贏得此物品，物品將被系統回收";
  public static readonly string[] TmSet_title = new string[3]
  {
		"隊伍設定",
		"顯  示",
		"隊長設定"
  };  
	
  public static readonly string[] TmSet_ItemN = new string[4]
  {
		"．狀態",
		"．經驗值、金錢",
		"．物品",
		"．物品等級"
  };	
	
  public static readonly string[] YesOrNo = new string[2]	
  {
		"確 認", "取 消",
  };
	
  public static readonly string[] Team_GreedOrNot = new string[3]
  {
		"爭取-想要此物品", "備取-沒人要再給我", "秒"
  };
	
  public static readonly string[] TmSet_wtMode = new string[3]
  {
		"待命", "防禦", "攻擊"
  };

  public static readonly string[] GreedChoice = new string[3]
  {
		"放棄", "爭取", "備取"
  };

//  public static readonly string settingSuggestion = "---請選擇---";
  public static readonly string[][] settingItemStr = new string[4][]
  {
    new string[]{"全顯示",   "全顯示",  "不顯示",  "好狀態",   "壞狀態"},//顯示
    new string[]{"獨自獲得", "獨自獲得", "平均分配"},				//獲得金錢＆經驗模式
    new string[]{"獨自獲得", "獨自獲得", "輪流獲得", "需求分配"},   //獲得物品模式
    new string[]{"藍色以上", "藍色以上", "紫色以上"},	//
  };
	
//  public static readonly byte[][] settingItemNum = new byte[4][]
//  {
//	new byte[]{0, 0,1,2,3},//{"全顯示",   "全顯示",  "不顯示",   "好狀態",   "壞狀態"},		
//	new byte[]{0, 0,1},	   //{"獨自獲得", "獨自獲得", "平均分配"},
//	new byte[]{0, 0,1,2},  //{"獨自獲得", "獨自獲得", "輪流獲得", "需求分配"}, 
//	new byte[]{0, 0,1},    //{"藍色以上", "藍色以上", "紫色以上"}
//  };	
#endregion teamSettingUI
	
#region ATMission
	
	public static readonly string[] Str_Mission0001 = new string[]
	{
		"王國評定",
		"任務標題任務標題任務標題任務標題任務標題",
		"任務大綱",
		"任務內文1任務內文2任務內文3任務內文4任務內文5任務內文6任務內文7任務內文8任務內文9任務內文0任務內文1任務內文2任務內文3",
		"　 {0}",
		"確  定",
		"刪除任務",
		"回報任務",
		"拒絕任務",
		"{0}/{1}",
		"可解次數:({0}/{1})"
	};
	
	public static readonly string[] Str_Mission0002 = new string[]
	{
		"OK_{0}",
		"complete",
		"delete",
		"accept",
		"request"
	};
	
	public static readonly string[] Str_Mission0003 = new string[]
	{
		"王國評定",
		"隨機評定",
		"勇者評定",
		"文明評定",
		"世界評定",
		"機會命運",
		"副本評定",
		"實力評定",
		"騎士評定",
		"地城評定",
		"文明回報",
		"騎士團回報",
		"隨機回報",
		"實力回報",
		"秘密評定",
		"秘密評定",
		"秘密評定"
	};
	
	public static string[] Str_Mission0004 = new string[3] {"ATMissionConfirmDelete","刪除提示","確認是否刪除？"};
	
	public static string[] Str_Mission0005 = new string[3] {"ATMissionConfirmGoldUse","提示","是否使用金幣消費？"};
	
	public static readonly string Str_Mission0006 = " 適合等級：";
	
	public static readonly string Str_Mission0007 = "({0},{1})";
	
	public static readonly string Str_Mission0008 = "『{0}』";
	
	public static readonly string Str_Mission0009 = "任務已達上限";
	
	public static readonly string Str_Mission0010 = "完成條件：{0}/{1}";
	
	public static readonly string Str_Mission0011 = "隨機獲得金幣/虛寶/銀幣 三者其一";
	
	public static readonly string[] Str_Mission0012 = new string[]
	{
		"依表現獲得經驗值",
		"依表現獲得銀幣",
		"依表現獲得金幣"
	};
#endregion
	
#region CharacterFM
  public static readonly bool Str_isChinese = true;
#region Attr2Type
  public static readonly string[] Str_Attr2Type = new string[4]
  {
    "攻擊屬性",
    "防禦屬性",
    "法術抗性",
    "其它屬性",
  };
  #endregion
#region Attr2Title
  public static readonly string[][] Str_Attr2Title= new string[][]
  { //攻擊屬性
    new string[]{
      "攻擊強度",
      "法術傷害", 
      "治療量", 
      "物理爆擊",
      "法術爆擊",
      "攻擊速度",
      "詠唱速度", 
    },
    //防禦屬性
    new string[]{
      "防禦力",
      "韌性",
      "閃避",
      "命中",
      "格檔",
      "穿透",
      "招架",
      "破壞",
    },
    //法術抗性
    new string[]{
      "地抗性",
      "水抗性",
      "火抗性",
      "風抗性",
      "光抗性",
      "闇抗性",
    },
    //其他屬性
    new string[]{
      "生命回復",
      "魔力回復",
      "癒合力",
      "調和力",
      "移動速度",
      "幸運", 
    },
  };
  public const string Str_Attr2HintFormat = "%j{0}|{1}";
  public static readonly string[][] Str_Attr2Hint = new string[][]
  { //攻擊屬性
    new string[]{
		"%w影響%o普通攻擊、技能攻擊%w造成的傷害",
		"%w影響%j技能攻擊%w造成的傷害",
		"%w影響%o治療類型技能%w造成的治療效果",
		"%w影響%o普通攻擊、物理傷害型技能攻擊%w發動%y爆擊%w的機率，會受目標的%y韌性%w影響，%y爆擊%w發動會使傷害加倍",
		"%w影響%o魔法傷害型技能%w攻擊發動%y爆擊%w的機率，會受目標的%y韌性%w屬性影響，%y爆擊%w發動會使傷害加倍",
		"%w影響%o普通攻擊%w的攻擊速度",
		"%w影響%o技能攻擊%w詠唱需要的時間",
    },
    //防禦屬性
    new string[]{
		"%w可減少受到%o普通攻擊、物理類型技能%w的傷害",
		"%w可減少受到%o普通攻擊、技能攻擊%w時被發動爆擊的機率",
		"%w可增加受到%o普通攻擊、技能攻擊%w時發動%y閃避(Dodge)%w的機率，%y閃避%w發動可完全不受到該次傷害",
		"%w可減少%o普通攻擊、技能攻擊%w時被目標發動%y閃避(Dodge)%w的機率，受目標的%y閃避%w屬性影響",
		"%w可增加受到%o普通攻擊、物理類型技能%w時發動%y格擋(Block)%w的機率，%y格擋%w發動可完全不受到該次傷害，並對目標造成一次普攻傷害",
		"%w可減少%o普通攻擊、物理類型技能%w時被目標發動%y格擋(Block)%w的機率，受目標的%y格擋%w屬性影響",
		"%w可增加受到%o普通攻擊、物理類型技能%w時發動%y招架(Parry)%w的機率，%y招架%w發動時可讓該次傷害減半",
		"%w可減少%o普通攻擊、物理類型技能%w時被目標發動%y招架(Parry)%w的機率，受目標的%y招架%w屬性影響",
    },
    //法術抗性
    new string[]{
		"%w可減少受到%o地屬性技能%w的傷害",
		"%w可減少受到%o水屬性技能%w的傷害",
		"%w可減少受到%o火屬性技能%w的傷害",
		"%w可減少受到%o風屬性技能%w的傷害",
		"%w可減少受到%o光屬性技能%w的傷害",
		"%w可減少受到%o闇屬性技能%w的傷害",
    },
    //其他屬性
    new string[]{
		"%w非戰鬥中每12秒可回復的生命",
		"%w非戰鬥中每12秒可回復的魔力",
		"%w使用一次性恢復生命的藥水時，會受此屬性影響而使恢復量加成",
		"%w使用一次性恢復魔力的藥水時，會受此屬性影響而使恢復量加成",
		"%w影響在地圖上移動之速度",
		"%w影響擊倒怪物時，獲得寶物的機率",
    },
  };
#endregion
  public static readonly string Str_DressHint = "|[[[color=240,225,10]]]點擊可開啟造型商店";
  public static readonly string[] Str_AttrClass = new string[]
  {
    "初心者",
    "聖劍士",
    "福音祭司",
    "幽影法師",
    "深淵刺客",
    "齒輪大師",
    "煉金術士",
    "聖弓手",
    "黑武士",
    "蒸汽神兵",
  };
  public static readonly string Str_newbie = "見習";

  public static readonly string[] Str_AttrInfo = new string[]
  {
	"%j力量|%w可提高%o攻擊強度%w、%o格擋%w",
	"%j敏捷|%w可提高%o物理暴擊%w、%o閃避%w",
	"%j體力|%w可提高%o生命最大值%w、%o招架%w",
	"%j智力|%w可提高%o法術傷害%w、%o治療量%w、%o法術爆擊%w、%o魔力最大值%w",
	"%j精神|%w可提高%o生命回復%w、%o魔力回復%w、%o韌性%w",
	"%j敏捷|%w可提高%o攻擊強度%w、%o物理暴擊%w、%o閃避%w",
  };
  public static readonly string[] Str_CharacterButtonHint = new string[]
  {
	  "頭部外觀顯示：開",//0
	  "頭部外觀顯示：關",//1
	  "顯示角色屬性",//2
	  "隱藏角色屬性",//3
	  "命運之輪",//4
	  "自身強化光影：開",//5
	  "自身強化光影：關",//6
  };
  public static readonly string[] Str_CharacterInofText = new string[]
  {
	"魔化效果",
	"(身上尚無魔化裝備)",
	"詳細裝備魔化方式可開啟魔法故事書（H）→裝備篇 進行了解",
  };
#endregion

#region SkillStarGemFM
	/// <summary>
	/// 0 "找到自我天命的亞特英雄，將領悟星盤之力"
	/// 1 "未通過成年禮前，無法喚醒星石"
	/// 2 "目前經驗值：{0}",
	/// 3 "%y喚醒此星石需求之等級",
	///	4 "%y喚醒此星石需消耗之EXP",
	///	5 "%y喚醒此星石需消耗之銀幣",
	///	6 "%y星塵碎片%w：|有機率在%r打倒怪物%w後取得，或者完成%r勇者評定%w後取得。",
	///	7 "[[[aligen = center]]]可喚醒星石：|{0}",
    ///	8 "%y※最多可安裝6招主星技能",
    /// 9 "死亡中無法學習技能",
	/// </summary>
	public static readonly string[] Str_Skill_Hint = new string[]
	{
		"找到自我天命的亞特英雄，將領悟星盤之力",	//0
		"未通過成年禮前，無法喚醒星石",				//1
		"目前經驗值：{0}",
		"%y喚醒此星石需求之等級",
		"%y喚醒此星石需消耗之EXP",
		"%y喚醒此星石需消耗之銀幣",
		"%y星塵碎片%w：|有機率在%r打倒怪物%w後取得，或者完成%r勇者評定%w後取得。",
		"[[[align=center]]]可喚醒星石：|{0}",
		"※最多可安裝6招主星技能",
		"死亡中無法學習技能",
	};
  public static readonly string[] Str_SkillAwakeResult = new string[]
  {
    "喚醒成功", //0
    "星石資訊不正確",
    "已開啟過星石",
    "玩家習得空間上限不足",
    "銀幣不足",
    "等級不足",//5
    "經驗值不足",
    "物品不足",
    "前置技能不足",
    "喚醒失敗",
    "強化資訊不正確",
  };

  public static readonly string[] SkillFM_Title = new string[2] { "星盤", "技能" };
  public static readonly string[] SkillFM_SubTitle = new string[4] { "主星", "行星", "衛星","星鎖"};
  public static readonly string[] SkillFM_Str = new string[19]{
      "星盤",     //0
      "星石",     //1
      "主星",     //2
      "行星",     //3
      "衛星",     //4
      "軌道",     //5
      "星鎖",     //6
      "星塵碎片", //7
      "喚醒",     //8
      "強化技能", //9
      "仙王座",   //10
      "仙后座",   //11
      "飛馬座",   //12
      "仙女座",   //13
      "蠍虎座",   //14
      "御夫座",   //15
      "三角座",   //16
      "英仙座",   //17
      "鯨魚座",   //18
  };
  public static readonly string[] SkillFM_SkillType = new string[3]{
    "主星能力","行星能力","衛星能力"
  };
  public static readonly string[] SkillFM_SkillState = new string[2]{
    "已啟動","未啟動",
  };
  public static readonly string[] SkillFM_Satellites = new string[6]{
    "攻擊強度","法術傷害","治療量","防禦力","生命","魔力",
  };
#endregion
#region SkillHint

	public static readonly string[]  Str_SkillValueFormat = new string[10]
	{
		"消耗：{0:###0}MP",
		"消耗：{0:###0}%MP",
		"詠唱：{0:#0.00}秒",
		"冷卻：{0:#0.00}秒",
		"距離：{0:####0}",
		"[[[size=14]]]%r攻擊強度加成%w：{0}%",
		"[[[size=14]]]%b法術傷害加成%w：{0}%",
		"[[[size=14]]]%g治療量加成%w：{0}%",
		"被動技能",
		"星雲演化說明：",
	};
#endregion

	#region 強化介面
	public static readonly string Str_Strengthen_Title = "裝備強化星儀";

	public static readonly string[] Str_Strengthen_BtnLabel = new string[] { "星石覺醒", "連續覺醒", "星石爆發", "主星爆發", "能量回收","連續爆發" };
	public static readonly string[] Str_Strengthen_Hint =new string[] {
		"[[[size=15 color=245,160,35]]]使用說明：|%w        放入裝備後，點擊[[[color=130,230,255]]]覺醒%w按鈕來獲得星石能量，累積的能量越多裝備的效果也越強。|[[[color=170,225,55]]]每次累積5能量，都會增加星石能量效果，集滿25能量後，裝備上的基本攻擊力與基本防禦力都會增加，裝備的強度等級也會隨著點亮的大星石而增加。",
		"您身上沒有爆發之心，請問是否要消耗金幣進行星石爆發？",//1
		"我了解，不再顯示此訊息",//2
		"確認",//3
		"取消",//4
		"星石能量：{0} + {1}",	//5
		"[[[size=16 color=216,237,144 OutlineColor=16,64,19]]]請放置欲強化的裝備於星儀上",//6
		"尚未開啟",//7
		"使用銀幣每次消耗：",//8
		"使用金幣每次消耗：",//9
		"(能量有機會爆擊)",//10
		"尚餘 {0}/{1} 次",//11
		"請輸入覺醒最大次數",//12
		"銀幣不足！是否要使用金幣來完成接下來的次數？",//13
		"中止",//14
		"[[[size=16 color=238,230,161]]]基本攻防 + {0}%",//15;
		"[[[size=16 color=255,248,139 OutlineColor=178,50,20]]]基本攻防 + {0}%",//16;
		"花費銀幣進行一次覺醒星石的動作。",//17
		"每次消耗1個爆發之心，來累積能量進行爆發，可以取得特殊效果",//18
		"可以自行輸入次數，進行多次的覺醒動作。",//19
		"可以回收5格能量，換取銀幣或爆發之心，如果回收能量不足，會降星等。",//20
		"因為能量不足，所以此次回收能量會造成降星，請確認是否要進行回收。",//21
		"爆發之心不足！是否要使用金幣來完成接下來的次數？",//22
		"使用爆發之心：",//23
		"請輸入爆發最大次數",//24
		"可以自行輸入次數，進行多次的爆發動作。",//25
		"裝備出現新的隨機效果！",//26
		"哇！裝備上出現神秘的主星！",//27
		"主星爆發！此裝備已到達強化的巔峰！",//28
	};
	public static readonly string[] Str_Strengthen_AwakeHint = new string[]{
		"[[[align=center]]]請確認是否要星石覺醒？",
		"需要消耗：|[[[align=center]]]",
		"[[[align=center]]]請輸入想要連續星石覺醒的次數",
		"覺醒消耗：|[[[align=center]]]",
		"[[[color=225,70,54]]]※每次星石覺醒會自動扣所需消耗。",
		"%o覺醒消耗：%w",
	};
	public static readonly string[] Str_Strengthen_PrayHint = new string[]{
		"[[[align=center]]]請確認是否要使用爆發之心來進行星石爆發？",
		"※進行星石爆發失敗時，會導致爆發之心毀壞。",
		"[[[align=center]]]您身上沒有爆發之心，是否要使用金幣來進行爆發？",
		"[[[align=center]]]%y(需xxx金幣)",
	};
	public static readonly string[] Str_Strengthen_ErrorInfo = new string[]{
		"次數錯誤",
		"您身上沒有爆發之心",
	};
	public static readonly string[] Str_Strengthen_ResultSucessMessage = new string[]{
		"太好了！成功覺醒了一個星石，裝備變得更強了！","太好了！星石爆發成功，裝備獲得特殊能力！"
	};

	public static readonly string Str_StrengthenInfo_Title = "星石總覽";
	public static readonly string[] Str_StrengthenInfo_Text = new string[]{
		"頭飾、上衣、褲鞋",
		"面具、護腕、披風",
		"武器、戒指、項鍊",	
		"[[[size=15]]]　　當裝備上的星石皆覺醒時，就到了爆發階段。此階段會出現帶有[[[color=83,221,235]]]隨機效果%w的特殊星石，可利用[[[color=83,221,235]]]【星石爆發】%w來開啟。而各種隨機效果又分為[[[color=83,221,235]]]普通、超強(MAX)%w兩種強度將視運氣出現。|%o目前置放在強化星儀上的裝備有機會得到的隨機效果一覽：",
		"增加",
	};

	#endregion
	#region 物品強化相關訊息
	public static readonly string[] Str_StrengthenItemResult = new string[7] {
		"",
		"星石只微微閃耀了一下，你得再嘗試看看。",
		"星石爆發失敗", 
		"銀幣不足！",
		"", 
		"物品使用中" ,
		"金幣不足，無法進行星石爆發"
	};


	#endregion

	#region 技能強化
	public static readonly string[] Str_Evolution_Nebula_Name = new string[]{
		"玫瑰星雲",
		"沙漏星雲",
		"螺旋星雲",
		"鷹星雲",
		"貓眼星雲",
		"螞蟻星雲",
		"蜘蛛星雲",
	};
	public static readonly string[] Str_Evolution_Nebula_Content = new string[]{
		"汲取玫瑰星雲能量增加主星技能之治療效果",
		"汲取沙漏星雲能量減少主星技能之冷卻時間",
		"汲取螺旋星雲能量增加主星技能附加之狀態持續時間",
		"汲取鷹星雲能量增加主星技能之傷害",
		"汲取貓眼星雲能量增加主星技能附加狀態之效果",
		"汲取螞蟻星雲能量減少主星技能之耗魔",
		"汲取蜘蛛星雲能量使主星技能造成傷害時有機率附加狀態",
	};
	public static readonly string[] Str_Evolution_Content = new string[]{
		"演化",
		"選擇一個主星技能",
		"還擇一種星雲能量",
		"主星技能：{0}演化為{1}",
		"死亡中無法進行演化",
	};
	#endregion

#region VIP介面
  public static string VIPUI_Title = "VIP";
  public static string GoToStore = "前往儲值";
  public static string VIPExplainArea = "VIP功能說明區";

  public static string[] ChineseDigital = new string[11] {"○", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十"};
  public static string VipStr = "VIP {0} 星";
  public static string RemainingGoldRequired = "再儲值{0}升級";
	
  //領取詢問
  public static string[] Confirm_GetReward = new string[3] {"GetReward" ,"領取提示", "是否要領取物品?"};
#endregion VIP

#region Vip_ProMsg_101-6-21
public static string[] Trade_ProMsg_6_21 = new string[4]
{
	"VIP兌換獎勵成功",
	"VIP獎勵已兌換過",
	"VIP兌換獎勵結果: VIP等級不足",
	"VIP兌換獎勵結果: 身上空間數量不足"
};
#endregion

#region 死亡復活相關訊息
	
  public static readonly string[] Below30_RebornRuleExplan = new string[4]
  {
		"",
		"[[[align=center]]][[[size=15]]][[[color=192,88,88]]][角色等級30級以前]|%w無任何懲罰，復活傳回重生點",
		"",
		"",
  };
	
  public static string[] Upper30_RebornRuleExplan = new string[4]
  {
		"",
		"[[[align=left,size=15]]]1.身上穿著所有裝備耐久值%c下降25%|%w2.獲得虛弱狀態%c180秒%w，狀態效果：攻擊強度、法術傷害、抗性、防禦力%c下降50%",
		"",
		"",
  };

  public static string CostSilver_Reborn ="[[[align=left]]][[[size=14]]]1.身上穿著所有裝備耐久值%c下降5%|%w2.獲得虛弱狀態%c60秒%w，狀態效果：攻擊強度、法術傷害、抗性、防禦力%c下降50%%w。此次復活需花費銀幣:%c{0}%w元(隨死亡次數，增加費用，每日中午重置)";
  public static string CostGold_Reborn   ="[[[align=left]]][[[size=14]]]1.原地復活|2.獲得保護狀態%c5秒%w，狀態效果：無法作任何事情，但也不會受傷或中壞狀態。此次復活需花費金幣:%c{0}%w元";

  public static string[] RebornMethodName = new string[4]
  {
		"","",
		"等級31級以上方可使用",
		"等級31級以上，且VIP3星以上，方可使用",
  };

  public static readonly string DeathUI_Title = "死亡復活";
  public static readonly string Reborn_Hint = "[[[align=center]]][[[size=16]]][[[127,191,107]]] |請選擇復活方式";

  public static readonly string[] CostIsNotEnough = new string[4]
  {
		"", "",
		"所需銀幣不足",
		"所需金幣不足",
  };
#endregion RebornUI
	
#region 區域相關訊息
  public static readonly string[] Area_ProMsg_9 = new string[36]
  {
	"",
//	"玩家一般殺死",
//	"玩家武功殺死",
//	"玩家內功殺死",
//	"npc一般殺死",
//	"npc特殊攻擊殺死",
//	"玩家十層功力槍法波及",
//	"玩家棍法反彈",
//	"玩家大範圍打玩家波及",
//	"玩家大範圍打npc波及",
//	"npc大範圍打玩家波及",
//	"玩家內傷攻擊",
//	"npc內傷",
	"1-1玩家普攻",
	"1-2玩家外功",
	"1-3玩家內功",
	"1-4npc普攻",
	"1-5npc特攻",
	"1-6玩家擴散波及",
	"",
	"1-8被玩家內功打玩家波及",
	"1-9被玩家內攻打npc波及",
	"1-10被npc打玩家內功波及",
	"1-11中玩家內傷傷害",
	"1-12中npc內傷傷害",
	"1-13事件狀態扣血",
	"",
	"1-15玩家內傷反噬傷害",
	"1-16玩家反噬彈傷害",
	"1-17玩家靈魂鏈結(壞)",
	"1-18對寵物靈魂鏈結(好)寵物用)",
	"1-19玩家近戰內功受傷",
	"1-20玩家近戰內功波及",
	"","","","",
	"1-25箭塔機關攻擊",//
	"1-26玩家絕招",
	"1-27玩家圖騰攻擊",
	"1-28.寵物內功",
	"1-29.寵物普攻",
	"1-30寵物內功打NPC受波及",
	"1-31.寵物內功打玩家受波及",
	"1-32.寵物內功打寵物受波及",
	"1-33.中寵物內功傷害",
	"1-34.NPC狀態反彈傷害",
	"1-35.寵物反彈",
  };
	
  public static readonly string[] Area_ProMsg_10 = new string[3]
  {
		"自己爬起來",
		"自發武功",
		"他人武功",
  };
	
  public static readonly string KilledDyingMsg  = "[[[align=left]]][[[size=15]]]你已經被【[[[color=192,88,88]]]{0}%w】殺死了，|請選擇下列方式復活吧！";
  public static readonly string DefaultDyingMsg = "[[[align=center]]][[[size=15]]]你已經死了，請選擇下列方式復活吧！";
  public static string DyingInfor = string.Format(DefaultDyingMsg, "");// 死亡介面未載下來暫存訊息用
	
#endregion
	
#region 組隊協定相關訊息 Form Team
  public static readonly string BeenOffline = "{0} 已離線";

  public static readonly string[] ft_ErrorCode = new string[51]
  {
	"",
	"無此角色或角色不在線上",//1. 
	"距離太遠",//2.
	"對方已有隊伍",//	3.
	"對方拒絕與你組隊",//	4.
	"非隊長無法使用此功能",//5.
	"已有隊伍無法使用此功能",//6.
	"對方被邀請組隊中",//7.
	"無人邀你組隊或對方已下線",//8.
	"對方未邀請你組隊",//9.
	"隊伍數量已滿，無法成立新隊伍",//10.
	"隊伍人數已滿,無法新增隊員",//11.
	"對方沒有隊伍",//12.
	"對方不是隊長,無法邀請你入隊",//13.
	"非隊長無法邀請入隊",//14.
	"無隊伍無法使用此功能",//15.
	"不同隊伍無法使用此功能",//16.
	"忙碌中，無法組隊",//17.
	"找不到隊長人選，卸任隊長失敗",//18.
	"對方忙碌中，無法組隊",//19.
	"所在場景無法使用隊伍功能",//20.
	"對方場景無法組隊",//21.
	"不同陣營無法組隊",//22.
	"此場景無缺額隊伍",//23.
	"私人隊伍不開放加入申請",//24.
	"場景無未組隊人員",//25.
	"隊伍密碼錯誤",//26.
	"組隊資訊不合法",//27.
	"對方已有團隊",//28.
	"已有團隊無法使用此功能",//29.
	"非團長或副團長無法使用此功能",//30.
	"正在副本配對無法使用此功能",//31.
	"對方正在副本配對",//32.
		"","","","","","","","",//33-40
		"","","","","","","","","","對方正在參加聖光鬥技活動無法組隊"//41-50
  };

  public static readonly string ft_DialogTile = "組隊";
  public static readonly string[] ft_FunctionName = new string[4]
  {
		"離開隊伍",
		"解散隊伍",
		"指定隊長",
		"驅逐隊員",
  };
	
  public static readonly string AskedTeam = "已送出詢問組隊要求";

  public static readonly string[] ft_ProMsg_3 = new string[2]{"[[[align=center]]][[[paraGap=8]]] %o『{0}』%w|邀請您組隊，您是否答應？||於%o", "秒%w後自動拒絕"};
	
  public static readonly string TeamFormed = "隊伍成立";
  public static readonly string Invitation = "已送出邀請入隊要求";

  public static readonly string[] ft_ProMsg_6 = new string[4]
  {
	"已加入隊伍", "隊伍目前獎勵分配：經驗值、金錢採「{0}」，物品採「{1}」",
	"[[[align=center]]][[[paraGap=8]]] %o『{0}』%w|邀請您入隊，您是否答應？||於%o", "秒%w後自動拒絕"
  };
	
  public static readonly string NewMemberJoin = "新隊員:{0} 加入";
	
  public static readonly string LeaveTeam =	"{0} 離開隊伍";
	
  public static readonly string BecomeLeader = "{0} 成為隊長";
	
  public static readonly string[] ft_ProMsg_10 = new string[4]
  {
	"", "隊伍解散", "自己離開隊伍", "隊伍人數不足，隊伍已解散。"
  };	
	
  public static readonly string[,] ft_ProMsg_12 = new string[4, 4]
  {
	{"", "", "", ""},
	{"", "{0}已被驅逐出隊伍", "", ""}, 
	{"", "不是隊長，無法使用此功能", "對方不是隊員", "刪除隊員失敗"},
	{"", "你已被驅逐出隊伍", "", ""}
  };	
	
  public static readonly string[,] ft_ProMsg_13 = new string[3, 4]
  {
	{"", "", "", ""},
	{"", "{0}已被指定為隊長", "", ""}, 
	{"", "不是隊長，無法使用此功能", "對方不是隊員", "對方已下線"}
  };
	
  public static readonly string CongratulateLevelUp = "恭喜{0}等級提升！";
  public static readonly string	OtherObtainGoods = "{0}獲得物品 {1}";
  public static readonly string SelfObtainGoods = "獲得 {0}";
	
  public static readonly string[,] ft_ProMsg_21 = new string[4, 4]
  {
    {"全顯示",   "不顯示",  "好狀態",   "壞狀態"},		
	{"獨自獲得", "平均分配", "", ""},
	{"獨自獲得", "輪流獲得", "需求分配", ""}, 
	{"藍色以上", "紫色以上", "金色以上", ""}
  };

  public static readonly string MemberInfoHint = "職業: {0}|"+
												 "場景: {1}|"+
												 "HP :[[[color=255,122,122]]] {2} / {3}|%w" + 
  												 "MP :[[[color=112,112,255]]] {4} / {5}";

	public static readonly string HideTeamUI = "隱藏組隊介面";
	public static readonly string OpenTeamSetting = "開啟組隊設定";
	public static readonly string ExpandTeamUI = "展開組隊介面";
	
	public static readonly string WaitForReborn = "等待重生中";
	public static readonly string CDForReborn = "重生倒數：{0}秒";
	
	public static readonly string MemberSelectGreedOrNot = "{0}選擇 {1}：{2}";
	
	public static readonly string ThrowPoints = "{0}擲出點數{1}";
	public static readonly string PointsThrow = "{0} - {1}由{2}擲出點數{3}";
	public static readonly string WinPrizes = "{0} 贏得了：{1}";
	public static readonly string DiceResults = "{0} 擲骰結果：{1}";
	
	public static readonly string InPKCantFTeam = "聖光鬥技排序中，請離開排序後再進行組隊";
#endregion 組隊協定相關訊息 Form Team	

#region 頭圖功能 字串
  public static readonly string[] Str_TargetHeadFormF1 = new string[6] { "人物裝備欄", "邀請組隊", "密語", "加入好友", "邀請共乘", "" };
  public static readonly string[] Str_LeaveMount = new string[3]{"邀請乘坐","離開座位", "驅逐乘坐者"};
  public static readonly string[] Str_TargetHeadFormF2 = new string[5] { "怪物紀事",	"掉落物",	"預設空位",	"預設空位",	"預設空位",};
  public static string[] Str_Element =new string[7]
  {
    "全屬性",  //元素種類起始0
    "地屬性",
    "水屬性",
    "火屬性",
    "風屬性",
    "光屬性",
    "闇屬性"
  };
#endregion

#region 場景地圖提示
	public static string DangerScene = "[[[align=center]]][[[size=18]]]%d{0}";
	public static string PeaceScene  = "[[[align=center]]][[[size=18]]]%a{0}";
#endregion

#region 儲值系統介面
	public static string StoredValueTitle = "儲值";
	public static string CurrentlyGold = "[[[align=right]]][[[size=16]]][[[color=255,227,95]]]目前金幣 : {0}";
	public static string NoticeContent = "[[[align=left]]][[[size=16]]][[[color=168,241,129]]]公告事項";
	public static string NoticeContext = "[[[align=left]]][[[size=15]]][[[color=247,243,229]]]1、點數儲值至遊戲內稱為『金幣』|2、商城物品皆以『金幣』販售|3、社群帳號購點時，請先輸入天空龍平台之『會|	  員帳號』";
	public static string StoredCardEnterAcc = "請輸入儲值卡號 : ";
	public static string StoredCardEnterPas = "請輸入卡號密碼 : ";
	public static string StoredConfirm = "確 定";
	public static string OnlinePurchasePoint = "線上購點";
	public static string CurrencyConversion  = "龍幣轉換";

	//龍幣轉換
	public static string DCConversionTitle = "龍幣轉換";
	public static string CurrentDragonCoin = "[[[align=center]]][[[size=14]]][[[color=255,227,95]]]目前龍幣 : {0}";
	public static string ConversionContext = "[[[align=center]]][[[size=14]]]%w在下方欄位輸入點數，可以把天空|龍會員裡的點數快速轉為金幣";
	public static string WantDC = "要換多少龍幣 : ";
	//public static string CanCNC = "[[[align=left]]][[[size=14]]]%j可換多少金幣 : {0}";
	public static string CanCNC = "可換多少金幣 : {0}";
	public static string Change = "轉 換";
	public static string DragonIsNotEnough = "龍幣不足";
	public static string EnterIncorrect = "輸入不正確";

	//線上購點
	public static string OnlinePurchaseTitle = "線上購點";
	public static string Buy = "購 買";
	
	//線上購卡
	public static string ATMStore  = "線上ATM儲值";
	public static string BuyMyCard = "MyCard線上購卡";
	public static string Placard   = "線上購點超優惠，400點特價370，\n1000點特價860，1500點特價只要\n1200 !";
	public static string YourLoginAcc = "您的遊戲帳號：\n";
	public static string CopyAccount = "複製帳號";
	public static string CopyAccSuccess = "複製帳號成功";
#endregion

#region 副本資訊介面
	public static string InstanceUITitle = "副 本";
	public static string InstamceName  = "[[[size=15]]]副本名稱 : {0}";
	public static string Difficulty    = "[[[size=15]]]難度 :%s {0}";
	public static string TeamMember    = "隊伍成員";
	public static string[] DifficultyStr = new string[4]
	{
		"",
		"一般", "惡夢", "地獄"
	};
	public static string nRequiredLevel	 = "[[[size=15]]]適合等級 :%e {0}";//不適合
	public static string RequiredLevel	 = "[[[size=15]]]適合等級 :%s {0}";
	public static string nSugIntensity	 = "[[[size=15]]]建議裝備強度 :%e {0}";
	public static string SugIntensity	 = "[[[size=15]]]建議裝備強度 :%s {0}";
	public static string nRequiredMemNum = "[[[size=15 align==left]]]需求人數 :%e {0}";//人數不足
	public static string RequiredMemNum  = "[[[size=15 align==left]]]需求人數 :%s {0}";
	//public static string InstInformation = "[[[size=15]]]副本名稱 : {0}難度 : {1}	適合等級 : {2}	建議裝備強度 : {3}";
	public static string Difficulty_Cost = "[[[align=center color=255,231,174]]]難度 / 花費銀幣";
	public static string Normal    = "[[[size=16]]]一般";
	public static string Nightmare = "[[[size=16]]]惡夢";
	public static string Hell 	   = "[[[size=16]]]地獄";
	public static string CostTime    = "[[[size=16]]]%s{0}";
	public static string Assign    = "指派";
	public static string EnterCC   = "進入副本";
	public static string Cancle    = "取消";
	public static string CCExplain = "副本說明";
	public static string OffLined = "(已離線)";
	//指派介面
	public static string AssignSub = "指派部下";
	public static string AllowedAssign = "可指派";
	public static string NoSubordinate = "目前並無可指派的部下，您可透過騎士團﹝快捷鍵O﹞\n來新增好友成為部下，陪伴您一同勇闖副本！";
	public static string Return    = "返回";
	public static string WarTeam   = "戰隊";
	public static string ODStr = "攻擊強度 : {0}|法術傷害 : {1}|防禦力 : {2}";
	public static string TeamIsFull = "隊伍已滿";
	public static string TeamIsEmpty= "隊伍中已無部下";
	//進入副本確認
	public static string CCInformation = "副本資訊";
	public static string CancleEnter = "取消進入";
	public static string CDRemind = "[[[size=14 align==left]]]%u請按下按鈕進入副本,倒數%e{0}%u秒";
	public static string CCInfor  = "[[[size=14 align==left]]]副本難度 　　: {0}|經驗值、金錢 :%s {1}|%w掉落物品　　 :%s {2}|[[[246,150,37]]](副本中隊長可更改獎勵設定)";
	public static string WaitForCResult = "等待確認中...";

	//協定訊息[設定多人戰隊副本結果]
	public static string[] SetCCResult =
	{
		"設定成功",//成功
		"銀幣不足",//1.訓練時間不夠
		"已離線",//2
		"等級不符",//3
		"設定太頻繁",//4
		"組隊伍失敗",//5
		"尚未領取獎勵，請先領取獎勵後再進行",//6.隊伍中{0}尚未領取獎勵，請先領取獎勵後再進行
		"尚未通過關卡，無法挑戰此戰記",//7.隊XXX尚未通過關卡，無法挑戰此戰記
		"忙碌中無法進入",//8
		"所在場景無法出去",//9
		"動標已滿",//10
		"死亡中",//11
	};
	public static string DeathToCancel = "因隊伍成員{0}死亡，取消進入副本";
	
	public static string CancelToEnterCC = "{0}取消進入副本";
	public static string SubLevelIsInsufficient = "部下等級不足，無法勝任此副本";
	public static string MemberNotEnough = "人數不足，無法進入副本";
	public static string IsNotCaptain = "請由隊長選擇進入副本";
	public static string LevelNotMatch = SetCCResult[3] + "，無法進入副本";
	
	public static string ExpReward = "{0} / {1}";
	public static string DailyWarteamReward = "每日戰隊獎勵";
	public static string WarTeamExperience  = "戰隊經驗：";
	public static string Accept  = "確 認";
#endregion

#region 事件訊息字串
  public const string EventMSG001="您的銀幣已達上限，請清出銀幣空間後，再至提示櫃開啟此介面領取獎勵";
  public const string EventMSG002="恭喜您，獲得今天的上線獎勵";
  public const string EventMSG003="目前金幣遊樂場已達上限，請稍等再進入，請至提示櫃開啟此介面進入金幣遊樂場";
  public const string EventMSG004="恭喜您，獲得今天的上線獎勵+{0}銀幣";
#endregion
	
	#region MapGuidePathFinding
	public static string MapGuideNoFound = "無法提供尋路功能！";
	public static string MapGuideCloseTo = "您已達目標處！";
	public static string MapGuideCostomTarget = "自定目標";
    public static string MapCantOpen = "此場景無法使用中地圖";
	#endregion
    #region 伺服器選單
    public const string ServerList_GSName = "分流{0:00}";
    public static readonly string[] ServerLightText = new string[5]
	{
		"[[[size=14]]][[[color=166,166,166]]]未知",
        "[[[size=14]]][[[color=0,204,0]]]推薦",
        "[[[size=14]]][[[color=0,204,0]]]推薦",
        "[[[size=14]]][[[color=255,30,30]]]熱鬧",
        "[[[size=14]]][[[color=204,0,255]]]滿載",
	};
	#endregion

    #region 登入訊息
    public const string WebLoginFail = "閒置過久，請重新登入";
    #endregion

	#region 場景UI用
	public const string Str_FollowTime = "剩餘時間：{0}分";
	#endregion

	#region 昇級資訊用
	public const string Str_LVUPFM_Title = "升級資訊";
	public static readonly string[] Str_LVUPFM_SubTitle = new string[]{
		"獲得之能力",
		"可喚醒之星石",
		"升級贈禮",
	};
	#endregion

	public const string Totem_Name_Format = "{0}的{1}";

	public static readonly string[] Str_BuffSet_Result = new string[3]
	{
		"中止成功",
		"重啟成功",
		"戰鬥狀態下無法更改設定"
	};
	#region 分區介面
	public static readonly string[] Str_Zone_Btn_Text = new string[2]
	{
		"確定",
		"取消"
	};
	public const string Str_Zone_Same = "請選擇其他分區";
	public const string Str_Zone_Title = "分區選擇";
	public const string Str_Zone_Select_Text = "分區{0}";
	public const string Str_Zone_Info = "[[[size={0} color=206,236,114 lineGap={1}]]]1、切換分區可避免擁擠，確保玩家權益，對遊戲進行沒有任何影響。|2、當您在同場景卻沒有看到朋友時，請切換分區到朋友的所在分區。";
	#endregion
	
	#region 介面消費提示訊息
	public const string Cancel = "取 消";
	public static readonly string[][] TipMsg = new string[][]
	{
		new string[] {"能量不夠",  "[[[align=center]]]哦哦~能量不夠了|是否前往轉換?"},
		new string[] {"權力點不夠","[[[align=center]]]哦哦~權力點不夠了|是否前往轉換?"},
		new string[] {"金幣不夠",  "[[[align=center]]]哦哦~金幣不夠了|是否前往購買?"},
		new string[] {"訓練時間不足",  "[[[align=center]]]哦哦~訓練時間不夠了|是否前往領取?"},
	};
	#endregion

    public const string LoginPreSelect="最近登入伺服器";
    public const string LoginAllSelect = "所有伺服器";
	
	//武器強化光影名稱
	public static readonly string[][] Particle_Weapon = new string[10][]{
		new string[]{"",  "0"},
		new string[]{"e0055", "0.65"},
		new string[]{"e0056", "0.65"},
		new string[]{"e0057", "0.65"},
		new string[]{"e0058", "0.65"},
		new string[]{"e0059", "0.65"},
		new string[]{"e0060", "0.65"},
		new string[]{"e0061", "0.65"},
		new string[]{"e0062", "0.65"},
		new string[]{"e0063", "0.65"}
	};
	
	//防具強化光影名稱
	public static readonly string[] Particle_Armor = new string[10] {
		"",
		"e0064",
		"e0065",
		"e0066",
		"e0067",
		"e0068",
		"e0069",
		"e0070",
		"e0071",
		"e0072"
	};
	
	//撿取掉落物結果
	public static readonly string[] TreasureErrMsg = new string[6]{
		"撿取成功",  
		"無此地上物",
		"非地上物主人，無法撿拾",
		"與地上物距離太遠", 
		"撿拾地上物失敗",
		"身上物品已滿，無法撿拾"
	};
	
	#region 遠古成長秘術
	public const string AncientGrowthMystic = "遠古成長秘術";
	public const string GrowthExplain = "亞特子民代代傳承的成長秘術，在離線休息時開始凝聚\n成長元素，上線後提供完整補充。";
	public const string CumulativeTime = "目前累積可兌換時間：{0}小時{1}分鐘";
	public const string ExchangeObtExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：{0}({1}%)";
//  public const string ReachedMaxiExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：[[[color=255,0,0]]]已達上限(請先消秏經驗值再領取)";
	public const string ReachedMaxiExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：[[[color=255,0,0]]]已達本階段兌換上限";
	public const string GoThroughAfter = "(通過{0}副本後將可繼續兌換)";
	public const string ConversionTime = "兌換時間：";
	public const string Exchange = "兌 換";
	public const string ExchangeExplain = "[[[size=14]]]以離線時間換算成經驗值，單次離線每達15分鐘計算1次。|若逾15分鐘未達30分鐘，則以15分鐘計算。|單日最多8小時(每日中午12點重計)，最高累計8小時。|單次兌換時間以小時為單位。";

	public const string FreeExchange   = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] 免費";
	public const string GoldConvertion = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] {0}金幣";
	public const string VIPsConvertion = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] {0}金幣[[[color=255,0,0]]](VIP{1}星可兌換)";
	public static readonly string[] ConvertTime = new string[]{"全部兌換", "1小時", "2小時", "4小時", "12小時", "24小時"};
	
	public const string NONVIP = "[[[align=center]]][[[size=15]]]尚未達兌換條件。|前往了解VIP相關資訊？";
	public const string GoldExChangeConfirm = "[[[align=center]]][[[size=16]]]是否確認進行兌換？|[[[color=255,227,95]]](需扣除{0}金幣)";
	public const string FreeExChangeConfirm = "[[[align=center]]][[[size=16]]]是否確認進行兌換？";
	public const string ExChangeFailed = "兌換失敗！最低兌換時數(1小時)";
	
	public const string ExchangeSuccessful = "兌換成功";
	
	public static readonly string LessThanLowestGROWLVMSG = string.Format("角色需達{0}級且通過人馬禁地副本後方可開啟此功能", GLOBALCONST.LOWEST_ALLOWGROWLV);
	public static readonly string LessThanLowestGEARLVMSG = string.Format("角色需達{0}級可開啟此功能", GLOBALCONST.LOWEST_ALLOWAUTOGEAR);
	public static readonly string[] Grow_ProMsg = new string[2]
	{
		"",
		"超出經驗上限，購買失敗！"//1."兌換經驗值超越上限"
	};
	#endregion

	#region GUIStation
	public static readonly string[] OpenUIError = new string[]
	{
		"銀行使用中，無法開啟其他介面",
	};
	#endregion

    #region HolyLight
    public static readonly string LightTitle = "聖光鬥技";
    public static readonly string[] LightAsk = new string[] { "MatchAsk", "聖光鬥技", "是否要報名「聖光鬥技」？","﹝活動期間無法轉換職業﹞" };
    public static readonly string[] LightCancel = new string[] { "MatchCancel", "聖光鬥技", "退出「聖光鬥技」戰鬥排序？" };
    public static readonly string LightgoFightPretext = "%w找到對手，是否進入鬥技場？|%y背包已滿則無法獲得獎勵|%r當天棄權6次將無法參加活動|%o離進場尚餘";
    public static readonly string LightgoFightPostext = "秒";
    public static readonly string LightRunAway = "%y對手沒有接受挑戰！|已退出排序，請重新報名";
    public static readonly string LightNoMatch = "%y目前沒有適合的對手";
    //public static readonly string LightWait = "%y聖光鬥技%w";
    public static readonly string LightFull = "報名人數已滿";
	public static readonly string LightWrongTime = "非報名時間";
	public static readonly string LightNotOpen = "無法加入聖光鬥技";
    public static readonly string LightLevelNoReach = "%r等級不足26級";
    public static readonly string LightScoreNoEnough = "%r積分不足2分";
    public static readonly string LightAbortTooMany = "%r棄權次數過多";
    public static readonly string LightTeamCant = "%r請離開隊伍後再報名";
    public static readonly string LightSceneLimit = "%r此場景無法報名聖光鬥技";
    public static readonly string LightDead = "等待復活中，無法參加活動。";
    public static readonly string LightTimeLimit = "《活動時間為晚上7點至晚上12點》";

    public const string LightCountDown = "[[[align=center]]]%y聖光鬥技%w|排序中 約%r{0}分%w後進場";
    public const string LightCountDownAlmost = "[[[align=center]]]%y聖光鬥技%w|排序中 %r即將進場";

    public static readonly string LightResultNameLv = "[[[size=20]]]%b{0}|[[[size=16]]]%wLevel： %r{1}";
    public static readonly string LightResultWin = "%j恭喜您獲得最後勝利！";
    public static readonly string LightResultLose = "%b不幸落敗，再接再厲！";
    public static readonly string LightResultScore = "[[[size=16]]]%y積分：{0}";
    public static readonly string LightResultWinLose = "[[[align=center]]]%w勝場： %r{0}　%w敗場：%l{1}";
    public static readonly string LightResultWinStreak = "　%w連勝場次：{0}";
    #endregion

    #region FateWheel 命運之輪
    public const string FateTitle = "命運之輪";
    public const string FateAttrTitle = "命運詩篇";
    public const string FateAttrPoint = "屬性投點：{0}";
    public const string FateChargeMsg = "%v{1}|售價：{0}金幣|是否購買？";
    public const string FateSpandPoint = "投一點{0}+{1}";
    public const string DiceRoll = "命運啟動";
    public const string DiceMainInfo = "[[[color=72,42,7]]][[[OutlineColor=255,218,195]]]規則說明|※   命運啟動：花費10點命運籌碼|※   屬性投數：可開啟命運詩篇|自行分配屬性點數";
    public const string FateChanceKind1 = "[[[align=center]]] %v來自惡魔的契約|%y獲得虛弱狀態15分鐘|換取屬性點數+5";
    public const string FateChanceKind2 = "[[[align=center]]] %v{2}|%w原價：{1}金幣|%y超級優惠！特價{0}金幣";
    public const string FateChipHint = "命運籌碼：{0}/60000|可至購物中心（X）購買籌碼或活動取得";

    public const string FateFateKindTitle = "FateFate";

    public const string FateFateKind1 = "[[[align=center]]] %v命運女神的祝福|%y獲得{0}點命運籌碼";
    public const string FateFateKind2 = "[[[align=center]]] %v來自家鄉的歌謠|%y立即返回起點|休息後再出發";
    
    public const string FateChipFull = "欲購買之籌碼將超過上限";
    public const string FateChipNotEnough = "命運籌碼不足";
    public const string FateAttrNotEnough = "啟動命運以獲得屬性點";

    public const string FateGainGold = "獲得{0}金幣";
    public const string FateGainSilver = "獲得{0}銀幣";
    public const string FateChipGain = "獲得{0}命運籌碼";
    public const string FateGainSkill = "獲得{0}屬性點數";
    public const string FateStartPoint = "回到起點獲得2000銀幣獎勵！！！";

    public const string FateIconKindStart = "回到起點。|休息後再出發|（獲得2000銀幣獎勵！）";
    public const string FateIconKindChance = "抽取機會牌一次。|會出現怎樣的機會選項呢？";
    public const string FateIconKindFate = "抽取命運牌一次。|會出現怎樣的命運轉變呢？";
    public const string FateIconKindSilver = "獲得{0}~{1}銀幣";
    public const string FateIconKindGold = "獲得{0}~{1}金幣（不可交易）";
    public const string FateIconKindItem = "獲得一個神祕禮物";
    public const string FateIconKindSkill = "獲得{0}~{1}點屬性點數";
    #endregion

    #region 一次性特惠介面相關
    public static readonly string ImmediatelySnappedUp = "[[[align=center]]][[[[color=255,70,0]]]%r限時優惠!! {0:00} : {1:00}|[[[color=255,204,0]]][[[underline]]]【立即搶購】";
	public static readonly string CoundDown_MS = "倒數計時 {0:00}:{1:00}";
	public static readonly string WantToBuy = "我要買";
	public static readonly string ConfirmToBuy = "[[[align=center]]]是否花費{0}金幣購買|{1}?";
	public static readonly string GiveUpDisposSpecial = "[[[algin=center]]]僅此一次機會～|你確定要%r放棄%w嗎？";
	public static readonly string BoughtAbandon= "您已購買 / 放棄過此優惠";
#endregion
	#region 皇家俱樂部
	public const string RankInfoTitleStr = "排行資訊";
	public const string RankInfoText = "[[[color=243,229,232 OutlineColor=44,21,30 size=14]]]{0}";

	public const string RankRewardTitleStr = "排行獎勵";
	public const string FirstRankRewardStr = "【第1名獎勵】";

	public static readonly string [] RealRewardTypeStr = new string[]
	{
		"當日最高名次",
		"結算日名次"
	};

	public static readonly string [,] RealRewardStr = new string[,]
	{
		{
			"{0}：第1名",   // 符合第一名
			"{0}：第{1}名", // 符合其他獎勵區間
			"名次到達{0}名以內",     // 不符合任何獎勵區間
		},
		{
			"",                  // 符合第一名
			"【第{0}名獎勵】",    // 符合其他獎勵區間
			"可領取神秘獎勵喔！"   // 不符合任何獎勵區間
		}
	};
	public static readonly string [] GetRewardBtnStr = new string[] // 按鈕文字
	{
		"領取獎勵",
		"已領取",
		"資格不符"
	};
	public const string LeaderBoardName = "{0}入圍者";
	public const string LeaderBoardHint = "{0}：{1}|[[[color=235,97,0]]]{2}：{3}|[[[color=255,236,147]]]{4}：{5}|[[[color=178,236,10]]]{6}：{7}||[[[color=255,255,255]]]左鍵點擊，可查看玩家資訊";
	public static readonly string[] RankType = new string[]
	{
		"全部排名",
		"好友排名"
	};
	public const string RankDataInfo = "排名";
	public static readonly string [] RankCongratulationTypeText = new string[] // 祝賀文字在兩種type的變化
	{
		"你目前名次",
		"在好友排名"
	};
	public static readonly string [] RankCongratulationText = new string[] // 祝賀文字
	{
		"恭喜{0}為第1名，榮耀的滋味不是人人嚐得起！", // 第1名
		"恭喜{0}為第2名，榮耀之光就在你的一尺前方！", // 第2名
		"恭喜{0}為第3名，因為你！前2名心裡開始緊張了！", // 第3名
		"恭喜{0}為第4名，無需畏懼，你已證明自己的實力！", // 第4名
		"恭喜{0}為第5名，國家代表隊歡迎你的加入！", // 第5名
		"恭喜{0}為第6名，還差一名，就能讓所有人都看到你！", // 第6名
		"恭喜{0}為第7名，LuckySeven還能帶你衝到第幾名呢？", // 第7名
		"恭喜{0}為第8名，先鋒們，請注意！你們即將被他超越！", // 第8名
		"恭喜{0}為第9名，你又往前一小步，後面朋友一大哭！", // 第9名
		"恭喜{0}為第10名，有堅毅實力，才能對抗前9強的挑戰！", // 第10名
		"恭喜{0}為第{1}名，請繼續往前追求榮耀！", // 第11~1000名
		"可惜！你目前尚未進榜。請繼續努力挑戰！", // 未進榜
	};

	public static readonly string [] RankMessageText = new string []
	{
		"",
		"排行榜正在排行中,請稍候...", // Server傳來訊息代號1
	};
	#endregion
	
	#region 蒸汽齒輪相關
	public static readonly string QuestionCombatGear = "點擊START啟動蒸汽齒輪|啟動後會自動搜尋怪物進行攻擊|並依序自動施展安裝在快捷鍵1~6的技能|點擊STOP後關閉蒸汽齒輪";
	public static readonly string SceneNotAllowSteamGear = "此場景不可使用蒸汽齒輪！";
	public static readonly string SceneNotComplete = "場景尚未準備好！";
	public static readonly string YouaAreDead = "你已經死了！";
	public static readonly string RiddingToCantSG = "騎乘中不可使用蒸汽齒輪！";
	public static readonly string InEventToCant = "事件中無法開啟此功能";
	public static readonly string WaterIsNotEnough = "你身上的紅水或藍水不足，確定要啟動蒸汽齒輪？";
	#endregion
	
#region 港版專用(？)
  public static readonly string HKS_00001 = "%g《好友挺你》%w每個時間點判斷報到人數時，若已報到玩家%r在同一分流擁有12個已報到好友%w，另外會獲得「銀幣驚喜袋*1」。(一天限領一次) ";
#endregion	
}
#endif

#if _LANGUAGE_ENG
public static class GlobalConstString
{	
	//路徑
	//public const string Characters_AssetbundlePath = "assetbundles/Characters/";
	//public const string CharacterMeshs_AssetbundlePath = "assetbundles/Characters/Meshs/";
	//public const string CharacterMaterials_AssetbundlePath = "assetbundles/Characters/Materials/";
	//public const string CharacterAnimations_AssetbundlePath = "assetbundles/Characters/Animations/";
	public const string NPC_AssetbundlePath = "assetbundles/NPC/";
	public const string CameraEffectPath = "EffectCamera/";
	//檔名
	public const string AssetBundleExtName = ".unity3d";//AssetBundle副檔名
	public const string MaterialExtName = ".mat";//材質副檔名

	public const string PREFIX_CameraEffect = "Effectcam_"; // 鏡頭特效Prefix	

    public const string POSTFIX_CharacterMeshsDatabase = "charactermeshsdatabase" ;
    public const string POSTFIX_CharacterMaterialsDatabase = "charactermaterialsdatabase" ;
    public const string POSTFIX_CharacterMeshMaterialCountData = "charactermeshmaterialcountdata" ;
    public const string POSTFIX_CharacterBase = "_characterbase" ;
    public const string POSTFIX_CharacterAnimation = "_characteranimation" ;
    public const string POSTFIX_NPCAnimation = "_npcanimation" ;
    public const string POSTFIX_Notify = "_notify" ;

    public const string AssetBundleFileName_CharacterMeshsDatabase = POSTFIX_CharacterMeshsDatabase + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterMaterialsDatabase = POSTFIX_CharacterMaterialsDatabase + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterMeshMaterialCountData = POSTFIX_CharacterMeshMaterialCountData + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterBase = POSTFIX_CharacterBase + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_CharacterAnimation = POSTFIX_CharacterAnimation + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_NPCAnimation = POSTFIX_NPCAnimation + GLOBALCONST.EXT_ASSETBUNDLE;
    public const string AssetBundleFileName_Notify = POSTFIX_Notify + GLOBALCONST.EXT_ASSETBUNDLE;

    public const string LABEL_FRIEND_LIST = "Friends";
    public const string LABEL_DUNGEON_LIST = "Instance List";
    public const string LABEL_FRIEND_INVATE = "Invite";
	public const string BtnSure = "OK";
	public const string BtnNext = "Next";
	public const string BtnNextStep = "Next Step";
	public const string BtnPrevStep = "Prev Step";
	public const string BtnLeave ="Exit";
	public const string BtnSkip = "Skip";
	public const string NO_SELECTION = "No Selection";
	public const string NO_SELECTION_NEXT = "請選擇選項，然後點選{0}按鈕";

	public static readonly string[] SexTypeName = new string []
	{
		"Male",
		"Female",
	};
	public static readonly string[] BBodyTypeName = new string []
	{
		"Male(Strong)",
		"Male(Fit)",
	};
	public static readonly string[] GBodyTypeName = new string []
	{
		"Female(Fit)",
		"Female(S)",
	};
	public static readonly string[] CBFaceName = new string []
	{
		"Cool",
		"Sporty",
		"Mighty",
		"Intimidating",
		"Charming",
		"Steady"
	};
	public static readonly string[] CGFaceName = new string []
	{
		"Innocent",
		"Alluring",
		"Melancholy",
		"Clever",
		"Elegant",
		"Steady"
	};
	public static readonly string[] CBHairName = new string []
	{
		"Spiky",
		"Short Curve",
		"Long Curve",
		"Short",
		"Long Tied",
		"Mid Long",
		"Freaky",
		"Sharp Bangs",
		"Sharp Bangs & Spiky",
		"Bald",
	};
	public static readonly string[] CGHairName = new string []
	{
		"Short",
		"Mid Long",
		"Long",
		"Long Bang",
		"Ponytail",
		"Half Up",
		"Twin Braid",
		"Big Wave",
		"Long Straight",
		"Double Ponytail"
	};
	public static readonly string[] CBUnderWearName = new string []
	{
		"Underwear1",
		"Underwear2",
		"Underwear3",
		"Underwear4",
		"Underwear5"
	};
	public static readonly string[] CGUnderWearName = new string []
	{
		"Underwear1",
		"Underwear2",
		"Underwear3",
		"Underwear4",
		"Underwear5"
	};
	public static readonly string[] CSkinName = new string []
	{
		"Common",
		"White",
		"Red",
		"Tanned",
		"Iron Grey",
		"Purple"
	};
	public static readonly string[] CPIName = new string []
	{
		"Hairstyle:",//頭髮
		"Mask:",//面具
		"Face Shape:",//臉型
		"ube:",//預留
		"Chest:",//上衣
		"Bracer:",//手套
		"ube:",//預留
		"Pants:",//褲子
		"ube:",//預留
		"ube:",//預留
		"Cloak:",//披風
		"ube:"//預留
	};
	public static readonly string[] CCreateCharacterSelectName = new string []
	{
		"Name:",
		"Gender:",
		"Body Shape:",
		"Face:",
		"Hairstyle:",
		"Skin Color:",
		"Suit:"
	};
	public static readonly string[] npcPIName = new string []
	{
		"Suit:",//套裝
		"Skin:",//皮膚
		"Texture:",//材質
		"Helmet:",//頭髮
		"Chest:",//上衣
		"Bracer:",//護腕
		"Pants:",//鞋子
		"Cloak:",//披風
		"Back Armor:",//背甲
		"Mask:"//面具

	};
	
	public static readonly string[] CCreateNPCSelectName = new string []
	{
		"Gender:",
		"Body Shape:",
		"Face Shape:",
		"Beard:",
		"Eyeballs:",
		"NPC:",
		"ube:"//預留
	};
	
	//斷線訊息
	public const string AccountLockedMsg = "[[[size=14]]]此帳號已被鎖，請帳號持有者透過客服專線：(02)2652-2381或官方網站線上客服了解鎖帳原因。";
	public const string NETWORK_BROKENMSG_100 = "GameServer disconnect";
	public const string NETWORK_BROKENMSG_101 = "Link but not login";
	public const string NETWORK_BROKENMSG_102 = "didn't send the first files check";
	public const string NETWORK_BROKENMSG_103 = "didn't respond to files check";
	public const string NETWORK_BROKENMSG_104 = "didn't respond to connection check";
	public static readonly string[] NETWORK_BROKENMSG_001 = new string[]  //0..73
	{
		"",
		"Password error for 3 times","Duplicate login","Version error","File error","Account locked",
		"Save is full","Had logined other server","SaveIndex is full","Receive network error","Send buf overflow",
		"IP locked","Connection is full","Chksum erroe","Save packet error","Touch event before login",
		"Duplicately touch events","Touch event while holding","Receive unknown event","MapNpc serial number error","DataServer disconnected",
		"Commands sended too often","Files check error","Files check sum error","Line checking code error","Player log out normally",
		"Money duplicate login","Cash insufficient","Illegal","Block serial number error","Role locked temporarily",
		"Role doesn't exist","Touching NPC beyond distence","Move speed abnormal","Disconnect abnormally","Touching NPC beyond distence",
		"Change channel","Can't dirrectly login War Server","Disconnected by GM","Changed name by GM so that disconnected","Move Speed too fast",
		"Illegal move","Back to Server List","This account is OB only","The same account login forcibly so disconnected ","Can relogin again",
		"Server is crowded now, please login later","Illegal IP","Disconnect because of merge, please relogin","Player recharged Account Saving Card successfully, ","Disconnected with examining server, please try later",
		"Player registered Account Saving Disk successfullt","Started Offline Ideling Successfully","Account Saving Card checking common used IP, but didn't fit","Account doesn't exist","Link sum is full(coin dealer)",
		"Disconnected because Phone Lock unlocked,but didn't login in 100 secs","","Account with Phone Lock, and there are multi IP login at the same time","Not common used IP","Illegal IP",
		"Clock rate is abnormal so that disconnected","Too many commands at one time","Ap disconnected","Mainlaind version login result","Graphical Anti-bot disconnect",
		"Change Channel","Press X to log out normally","Disconeected because of back to Role List","Disconnected because of Maintenance","Experiecing Time is over. To continue playing Atlantis Online, please register your social networking account from the links at Server List and offical website",
		"The code of changing channel checked error","創角人數過多，請稍後重登",
		"尚未取得封測資格，請密切注意官網消息" //73
	};
	
	public static readonly string[] NETWORK_BROKENMSG_002 = new string[]
	{
		NETWORK_BROKENMSG_100,NETWORK_BROKENMSG_101,NETWORK_BROKENMSG_102,NETWORK_BROKENMSG_103,
		NETWORK_BROKENMSG_104
	};

	//系統相關訊息
	public static readonly string[] StoredResult = new string[]
	{
		"Top-up succeed",
		"Card number otr type error",
		"Failed, wrong password",
		"Failed, card is used",
		"已經儲過開卡點",
		"This type of cards can only top-up once",
		"保帳卡還有效用,不能再儲",
		"Please check there's enough weight and space and try again",
		"The item had been sent to bank",
		"The item had been sent to mailbox",
		"insufficient Gold",
		"Exchange to Gold succeed",
		"",
		"You can onlt top-up in the server you bond to"
	};

	public static readonly string[] AttackFailReason = new string[]
	{
		"",
		"Too far",
		"Can't go to there",
		"Out of range",
		"Player disappear",
		"Player state incorrect or NPC disappear",
		"Player isn't in this scene"
	};

	public static readonly string[] PKChoice = new string[]
	{
		"",
		"1.PK or not(1)[0.No  1.Yes]",
		"2.pk對象迴避+幫會(1) +聯會(1) +們派(1) +勢利(1)[0.不迴避 1.迴避]",
		"3. 關閉pk+倒數分鐘數(1)"
	};

	public static readonly string[] PhoneLockMsg = new string[]
	{
		"There is no Phone Lock on this accounnt, you can enter the game now",
		"Please unlock to Phone Lock with the phones you set up",
		"Unlocked successful, you can enter the game now"
	};
	public static readonly string[] PkMark = new string[]
	{
		"PK",
		"PVE"
	};
	public static readonly string[]	StoreStatus = new string[]
	{
		"Closed",
		"Open"
	};

	public static readonly string[] FlyRideLimit = new string[]
	{
		"Can't raise up because of thin air",
		"Can't raise up because of mount's limitation",
		"Can't fly in this scene"
	};

	public const string CantMoveToThere = "Can't move to there";
	public const string PleaseUseKeyBoard = "Please use key【WASD】and【Space】to move";
	public const string CantMoveWhenUseSteamGear = "You can't move when running steam gear";

	public const string TotemNumLimit = "This scene is full of totems";

    public const string SystemException_Argument_ElementReadonly = "This element is readonly.";
    public const string SystemException_Argument_NoMarshalAttribute = "The array field has no marshal attribute.";
    public const string SystemException_Argument_QueueIsFull = "The queue is full.";

    public const string GameObjectTag_Main = "Main";

    public const string StringTag_Protocol_Http = "http://";
    public const string StringTag_Protocol_File = "file:///";
    public const string StringTag_StringLength = "_LEN";
    public const string StringTag_DataKind_Unity3d = "unity3d";

    //	public const string WebServer_Domain_Root = StringTag_Protocol_Http + "127.0.0.1"; //"http://127.0.0.1"
    public const string WebServer_Domain_Root = StringTag_Protocol_Http + "192.168.33.136";
    public const string STANDALONE_Domain_Root = StringTag_Protocol_File;

    public const string WebServer_FolderName_Root = "UnityWebServer";
    public const string WebServer_FolderName_List = "List";

    public const string WebServer_Address_Root = WebServer_Domain_Root + "/" + WebServer_FolderName_Root; //"http://127.0.0.1/UnityWebServer"
    public const string WebServer_Address_List = WebServer_Address_Root + "/" + WebServer_FolderName_List; //"http://127.0.0.1/UnityWebServer/List"

    public const string WebServer_DataFileName_List = "List.txt";

    public const string WebServer_URL_List = WebServer_Address_List + "/" + WebServer_DataFileName_List; //"http://127.0.0.1/UnityWebServer/List/List.txt"
    
    public const string NET_CONNECT_FAILED = "Connect failed.";
    public const string NET_CONNECTING = "Connecting";
	public const string NET_CONNECTING_WAIT = "連線中 (已等待 {0}秒)";
    public const string NET_DISCONNECT = "Disconnected";
    public const string NET_RECONNECT = "連線中斷，遊戲將關閉";
    public const string NET_CONNECT_TRY = "連線失敗，嘗試重新連線？";
	public const string NET_CONNECT_EXCEPTION = "目前無法登入";
	public const string NET_DOWNLOAD_ERROR = "網路忙碌中，請重新整理頁面"; // fs 12/11/17 下載失敗時的回應訊息
    
    public const string DOWNLOAD_REMAIN = "Downloading…Left Files {0}";
    public const string DOWNLOAD_COUNTING = "Processing…";	
	public const string DOWNLOAD_INIT = "Initializing…";
	public const string FTP_ERROR_MSG = "遊戲無法直接啟動，\n請開啟更新程式ATUpdate.exe";
	public const string FTP_ERROR_TITLE = "警告訊息";
	public const string FTP_ERROR_CLOSE = "關閉";
	
	public const string GAME_APP_CLOSE = "是否離開遊戲？";
	public const string GAME_APP_CLOSE_BATTLE = "戰鬥中下線，\n角色保留十秒，\n確定要離開遊戲？";
	public const string GAME_LOW_FPS_WARNING = "繪圖效能不足，是否要自動調降繪圖效能需求？";

    public const string DataFileName_StoreSaveData = "Treasure.unity3d";
    public const string DataFileName_TestData1 = "TreasureTest1.unity3d";
    public const string DataFileName_TestData2 = "TreasureTest2.unity3d";
    public const string DataFileName_TestData3 = "TreasureTest3.unity3d";
    public const string DataFileName_TestData4 = "TreasureTest4AAA.unity3d";
    public const string DataFileName_TestData5 = "TreasureTest5.unity3d";
    public const string DataFileName_TestData6 = "TreasureTest6.unity3d";
    public const string DataFileName_TestData7 = "TreasureTest7.unity3d";
    public const string DataFileName_TestData8 = "TreasureTest8.unity3d";
    public const string DataFileName_TestData9 = "TreasureTest9.unity3d";
    public static readonly string[] DataFileName_Array = new string[] { DataFileName_StoreSaveData, //0
																		DataFileName_TestData1, //1
																		DataFileName_TestData2, //2
																		DataFileName_TestData3, //3
																		DataFileName_TestData4, //4
																		DataFileName_TestData5, //5
																		DataFileName_TestData6, //6
																		DataFileName_TestData7, //7
																		DataFileName_TestData8, //8
																		DataFileName_TestData9 }; //9
#region 中地圖提示
	public static string MidMapTip_01 = "Bank Mailbox";
	public static string MidMapTip_02 = "Skill teacher";
	public static string MidMapTip_03 = "World map";
	public static string MidMapTip_04 = "Simple map";//切成半透明地圖
	public static string MidMapTip_05 = "Close";//關閉窗體
	public static string MidMapTip_06 = "Map";
	public static string MidMapTip_07 = "Move Interface";
#endregion
	
#region 世界地圖提示	
	public static string WorldMapTip_01 = "Medium Map";
	public static string WorldMapTip_02 = "[[[align=center size=14]]]Transportting to |[[[size=15]]]%g[{0}][[[color=255,0,0]]]LV{1}~LV{2}|[[[size=14]]]%w takes %y{3}Silver %w";
	public static string[] CantDeliverTip = new string[]
	{
		"[[[align=center]]]The scene is not open to transport.",
		"[[[align=center]]][[[color=255,0,0]]]Insufficient Silver, unable to transport.|%w(此次傳送需要{0}元)",
		"[[[align=center]]]Unqualified for entering this sence, unablt to transport.",
	};
	public static string InBattleCantDeliver = "戰鬥中無法傳送";
	public static string InEventCantDeliver  = "事件中無法傳送";
#endregion
	
#region 事件獎勵相關
	public const string EventRewardExp = "Got {0} EXP";
	public const string EventRewardSilver = "Got {0} Silver";
	public const string EventDecSilver = "Spent {0} Silver";
#endregion

#region 頻道相關錯誤訊息
	public static string[] ChannelErrorMsgs = new string[]
	{
		"Mp不足，無法說話",
		"無組織，無法發組頻", 
		"好友人數錯誤", 
		"無隊伍，無法發隊頻", 
		"無此角色或該角色目前不在遊戲中", 
		"目前無GM在線上", 
		"你已被禁言",
	};
	public static string[] ChannelHint = new string[]
	{
		" is offline",
		" is not exist",
		"尚未加入隊伍",//2
		"尚未加入領域。16級之後，請清出背包空間，並使用道具「通訊儀」加入",//3
		"不能發送密語給自己",//4
		"字數過長",//5
		"發話過於頻繁",//6
		"世界頻道發話每句1000銀幣，是否花費？",//7
		"尚無好友",//8
		"冒險團功能近期開放，敬請期待",//9 尚未加入冒險團
		"單句特殊表情符號使用已達上限(3種)",
		"世界頻道發話每句扣除「磁歐擴音器」×1，是否使用？",//11
	};
#endregion
}


#region 表定字串
public class TableStr
{
  //物品的裝備位置
  public const string EquipPos_01="Weapon";
  public const string EquipPos_02="Helmet";
  public const string EquipPos_03="Bracer";
  public const string EquipPos_04="Chest";
  public const string EquipPos_05="Pants";
  public const string EquipPos_06="Cloak";
  public const string EquipPos_07="Mask";
  public const string EquipPos_10="Necklace";
  public const string EquipPos_11="Ring";
  public const string EquipPos_12="Amulet";
  public const string EquipPos_13="Styling Weapon";
  public const string EquipPos_14="Styling Helmet";
  public const string EquipPos_15="Styling Bracer";
  public const string EquipPos_16="Styling Chest";
  public const string EquipPos_17="Styling Pants";
  public const string EquipPos_18="Styling Cloak";
  public const string EquipPos_19="Styling Mask";

  //物品的裝備效果
  public const string EquipEff_001="Strength";
  public const string EquipEff_002="Agility";
  public const string EquipEff_003="Vitality";
  public const string EquipEff_004="Intelligence";
  public const string EquipEff_005="Spirit";
  public const string EquipEff_006="Luck";
  public const string EquipEff_007="All Attributes";
  public const string EquipEff_008="HP";
  public const string EquipEff_009="Max HP";
  public const string EquipEff_010="HP Regen";
  public const string EquipEff_011="癒合力";
  public const string EquipEff_012="MP";
  public const string EquipEff_013="Max MP";
  public const string EquipEff_014="MP Regen";
  public const string EquipEff_015="調和力";
  public const string EquipEff_016="Attack Speed";
  public const string EquipEff_017="Spell Speed";
  public const string EquipEff_018="Move Speed";
  public const string EquipEff_020="Dodge";
  public const string EquipEff_021="Hit";
  public const string EquipEff_022="Critical Hit";
  public const string EquipEff_023="Tenacity";
  public const string EquipEff_024="Block";
  public const string EquipEff_026="Penetration";
  public const string EquipEff_029="Magic Critical Hit";
  public const string EquipEff_030="Parry";
  public const string EquipEff_032="Destroy";
  public const string EquipEff_033="Defense";
  public const string EquipEff_034="Earth Resistance";
  public const string EquipEff_035="Water Resistance";
  public const string EquipEff_036="Fire Resistance";
  public const string EquipEff_037="Wind Resistance";
  public const string EquipEff_038="Light Resistance";
  public const string EquipEff_039="Dark Resistance";
  public const string EquipEff_040="All Resistance";
  public const string EquipEff_041="Attack Power";
  public const string EquipEff_043="攻擊強度與法術傷害";
  public const string EquipEff_044="Magic Damage";
  public const string EquipEff_049="Heal Amount";
  public const string EquipEff_051="普攻攻擊距離";
  public const string EquipEff_052="技能攻擊距離";
  public const string EquipEff_053="Damage Reduction in PK";
  public const string EquipEff_054="EXP";

  //水晶抗性效果
  public static string[] CrystalResistEff =
  {
    "All Resistance",  //元素種類起始0
    "Earth Resistance",
    "Water Resistance",
    "Fire Resistance",
    "Wind Resistance",
    "Light Resistance",
    "Dark Resistance",
    "Earth & Water Resistance",
    "Earth & Fire Resistance",
    "Earth & Wind Resistance",
    "Earth & Light Resistance",
    "Earth & Dark Resistance",
    "Water & Fire Resistance",
    "Water & Wind Resistance",
    "Water & Light Resistance",
    "Water & Dark Resistance",
    "Fire & Wind Resistance",
    "Fire & Light Resistance",
    "Fire & Dark Resistance",
    "Wind & Light Resistance",
    "Wind & Dark Resistance",
    "Light & Dark Resistance",
  };
}
#endregion


//UI 常數字串
public static class Const
{
	public static readonly string[] Str_00006 = new string[7]
	{ "Role Info", "Equip", "Attribute", "Class：{0}", "Role LV：{0}","{0} Informations","裝備積分：{0}" };
  public static readonly string[,] Str_00007 = new string[6,2]
  {
    {"Attack Power：", "{0}"},
    {"Magic Damage：", "{0}"},
    {"Critical Hit：", "{0}%"},
    {"Magic Critical Hit", "{0}%"},
    {"Attack Speed：", "{0}%"},
    {"Spell Speed：", "{0}%"}
  };
  public static readonly string[] Str_00008 = new string[4]
  {
    "Skill", "Battle", "Support", "Atlantis Power：{0}/{1}"
  };
  public static readonly string[] Str_00009 = new string[5]
  {
    "[[[color=<replace>]]]", "[[[color=<replace>]]]", "[[[color=<replace>]]]", "[[[color=<replace>]]]","[[[color=<replace>]]]"
  };

  public static readonly string[] Str_00013 = new string[4] { "Rearrange", "Withdraw Silver", "Expand the upper limit of silver", "Expand Bank space"};
  public static readonly string[] STR_CONN_INFO = new string[7] { "", "Password Incorrect" , "Unable to login now" , "Logining, please wait",  "This Server is full now" , "重覆登入" ,  "帳號錯誤"};		
	public static readonly string[] STR_AC_PW_ERROR = new string[2] {"帳號長度過短，請重新輸入", "密碼長度過短，請重新輸入"};
#region 主介面hint
	public static readonly string[] Str_InfoBarHint = new string[7]
	{
		"%v【Energy】|%yIn Tower of Civilization, %wdoing %jtechnology research|%wtakes Energy|Every %v{0}%wminute, it regens for%v{1}%wpoint.",
		"%o【Power】|%yIn Chivalry, %jsending vassals|%w and %jspeed up completing works %wtakes Power|Every%o{0}%wminute, it regens for%o{1}%wpoint|Entry condition:%oLV {2}",
		"%j【Training Time】|%yTake Training Time %w（Max time: %j3 hours%w）|can get：|%fBattle EXP Rate：{0:P0} Exp|Battle Silver Rate：{1:P0} Silver|%wWhen there is no Training Time, all rates resume to %y100％",
		"【Silver】|It can be used to do：|%yAstrolabe%w（Learning skills）|%ySoul Core Astrolabe%w（Upgrate equipments）|%yRepair equipments",
		"%y【Gold】|%wFrom %fTop up %wto get %y Gold%w, and from %f activities%w to get %y Bonus Gold%w|It can use for %j Gold functions%w, buying items in %j Mall%w or %j Magic wardrobe%w",
		"[[[color=255,244,92]]]【Fame】|%yConstructing Tower of Civilization %wand %ylearning higher skills|%w can increase Fame. The more Fame you get, the more famous you are in Atlantis||To get %g{0}%w more Fame to get the reward for next LV!",
		"%l【Bonus Energy】 |%wYou will get 5 Bonus Energy everyday to help friends researching tool, and you will get materials and Silver through helping"
	};
	public static readonly string[] Str_SimpleHintStr = new string[8]
	{
		"Energy Exchange",
		"Power Exchange",
		"Training Time",
		"銀幣轉換",
		"Top up",
		"Gold/Silver Switch",
		"迷你HP/MP顯示",
		"關閉顯示"
	};
	public static readonly string[] Str_InfoIconHint = new string[8]{
		"Function Box(F)",
		"Character（C）",
		"Bag（B）",
		"Astrolabe（K）",
		"Groups（G）",
		"Mall（X）",
		"System（N）",
		"魔法衣櫥（U）",
	};
	public static readonly string[] Str_MinMapBtnHint = new string[3]{
		"Full Screen",
		"Map（M）",
		"Area Choose",
	};
	public const string Str_BuffHint = "[[[align=center]]]{0}[[[.]]] {2} [[[.]]] Left Time {1:0} Seconds";
    public static readonly string[] Str_ChatBtn = new string[5] { "Window Size", "Window Mode", "Clear" ,"頻道開關設定","頻道不隱藏" };
	public static readonly string[] Str_ChatFunctionBtn = new string[6] { "密語", "加入好友", "加入黑名單", "邀請組隊", "玩家資訊", "取消" };

	public const string Str_CastingInfo = "%r{0}　%wUse　%y{1}";

	public const string Str_AutoEvtHint = "評定重整：|搜尋是否有可接或可回報的評定";
	
	public const string Str_OKAutoEvtHint = "重整完成";
	public const string Str_CloseAutoEvent = "蒸氣齒輪開啟中，無法搜尋評定";
	public const string Str_AutoEventClickNPC = "請先關閉蒸氣齒輪後再進行";
#endregion
#region 狀態hint
	public static readonly string[] Str_BuffCancelButton = new string[3]{
														"取消","開始","暫停"};
	public static readonly string[] Str_BufferHint = new string[4]{
		"[[[align=center]]]%y{0}%w[[[.]]] {1} [[[.]]]%y剩餘時間：%w{2:0天;;}{3:0時;;}{4:0分;;}{5:0秒;;}",
		"[[[align=center]]]%y{0}%w[[[.]]] {1}",
		"[[[align=center]]]%y{0}%w[[[.]]] {1} [[[.]]]%y剩餘時間：%w{2:0天;;}{3:0時;;}{4:0分;;}{5:0秒;;}[[[.]]]右鍵取消",
		"[[[align=center]]]%y{0}%w[[[.]]] {1} [[[.]]]%y剩餘時間：%w{2:0天;;}{3:0時;;}{4:0分;;}{5:0秒;;}[[[.]]]右鍵{6}/取消",
	};
#endregion
#region 商店商城介面
	public static readonly string[] Str_ShopMall_String = new string[]
	{
		"Mall",
		"Shop",
		"Buy",
		"Sell",
		"Sum:",		
		"1、Double clicks to put items into sell area\n2、Right click to put all items into sell area\n3、You can also drag the item into a grid in sell area",
		"Go to the official website",
		"＜Sale News＞",
		"Sale News",
		"Apply",
		"Cancel",
		"Put on",
		"Owned",
		"Styling Shop",
		"You already have this item",
		"Sure to sell this item?|Will get {0}{1}",
		"Gold",
		"Silver",
		"[[[size=16]]]till {0}/{1}/{2}",
		"Take off",
		"Are you sure to but items below?",
		"Changin cloths","",
		"Repair equipments in shop",
		"Top up",
		"Buy the items shown in preview",
		"Total：",
		"%l【{0}】",//詢問購買商品時的文字顏色
		"VIP LV unquilified, it can only be bought with VIP {0} stars",
		"%ySale Time： %w",
		"Buy",
		"Amount",
		"此物品已下架",
		"%r是否要出售以下貴重物品：",

	};
	public static readonly string[] Str_ShopMall_RepairString = new string[]
	{
		"[[[align=center]]]{0}takes {1} Silver",
		"Repair all",
		"Repair items in Bag",
		"Repair equipments",
		"Cancel",
		"There is nothing to repair",
	};
	public static readonly string[] Str_ShopMall_TabString = new string[]{
		"New",
		"VIP",
		"試衣鏡",
		"Wish",
	};
	public static readonly string[] Str_ShopMall_EventTypeString = new string[]{
		"Payment Gift",
		"Bonus",
		"VIP Only",
		"Special Sale",
	};
	public const string Str_Wish_Info = "[[[color=221,221,221]]]亞特的子民們~為了協助新亞特的復甦，設計女神‧奧蘿拉來到人間，貢獻己長，設計出與眾不同、注有神力的各式物件~擁有設計女神‧奧蘿拉每週精心設計的特殊物件，則每日可領取一次奧蘿拉的祝福哦~";
	public const string Str_Wish_TimeFormat = "[[[color=255,214,91]]]<領取時間>[[[color=255,145,193]]]{0}/{1}/{2}維護後~{3}/{4}/{5}維護前[[[color=255,214,91]]]；每日中午12：00可重新領取";
	public static readonly string[] Str_Wish_Text = new string[]
	{
		"領取祝福", //0
		"已領取",
		"重置",
		"虛寶",
		"本週奧羅拉物件",
		"★{0}物件",//5
		"[[[paraGap=2 color=243,151,0]]]頭、衣、手、褲、面飾、背甲",
		"[[[paraGap=2 color=243,151,0]]]武器、其他",
		"有機會獲得獎勵金幣或銀幣|%y(若銀幣空間不足，超出之獎勵將無法獲得)",
		"有機會獲得神秘禮物。",
		"有機會獲得{0}%~{1}%的經驗值加成效果(時間6小時)。", //10
		"有機會獲得全屬性提升{0}~{1}效果(時間6小時)。",
		"擁有一件本週奧蘿拉銀幣物件可領取。|%y(若銀幣空間不足，超出之獎勵將無法獲得)",
		"擁有{0}件本週奧蘿拉金幣物件可領取。",
		"狀態跨每日中午12:00，即無法重置需重新領取。",
	};
#endregion

#region 寶物交易
		public static readonly string[] Str_TradeSuccess =new string[2]{ "Bought successfully","Bought:{0} Amount:{1}"};
	public static readonly string[] Str_TradeError = new string[13] {
			"insufficient Gold",
			"insufficient Sliver",
			"The item is not on shelf",
			"The item is sold out",
			"The server is not the one you bond to consume. If you need to change, please contact our customer service",
			"Time limited item, can only baught in specific time",
			"The amount you want to buy exceeded the upper limit",
			"Exceeded stack-up",
			"Stop sell",
			"Fail because of other reason",
			"Insufficient Bag space",
			"不可交易金幣不足",
			"交易伺服器目前關閉中,請稍候",
		};
		public const string Str_SellSuccess = "Sold successfully";
		public static readonly string[] Str_SellResult = new string[7] {
			"Sold successfully:{0} Amount:{1}",
			"Failed",
			"Insufficient amount",
			"Insufficient space in shop",
			"Exceed upper limit of Silver",
			"The item can't be sold to NPC",
			"Sell too many kinds of items",
	};

#endregion

#region 施放結果
  //本地施放判斷結果
  public static readonly string[] Str_CastingCheck = new string[14] {
   	"Success",
    "Still in Cool Down",
    "Not Enough HP",
    "Not Enough MP",
    "Target is too far",
    "Target is too close",
    "Target is too far",
    "Target is incorrect",
    "Can't Cast",
    "Casting",
    "Can't go through obstacle",
    "Can't use passive skill",
    "Can't use in this scene",
    "Please select position",
  };
  //server回傳施放結果
  public static readonly string[] Str_CastFailResult = new string[50]{
    "",
    "Too far away",//1
    "Status error",//2
    "Different scenes",//3
    "Insufficient HP",//4
    "Insufficient MP",//5
    "Insufficient SP",//6
    "LV unqualified",//7
    "Using skill",//8
    "Skill is not waked",//9
    "Spell the same skill",//10
    "Target needs",//11
    "No target in visible area",//12
    "Can't use due to the time",//13
    "Can't use without getting on a mount",//14
    "Can't use on a DeviceNPC",//15
    "Can't use in battle mode",//16
    "Target is alive",//17
    "Can't use in the scene",//18
    "Can't use in current position",//19
    "Target is under age",//20
    "Target is not in PK mode",//21
    "Different PK status",//22
    "The height differs",//23
    "Can't reach where the taget is",//24
    "Can't use without invisible status",//25
    "Apprentice is offline",//26
    "insufficient Respect point",//27
    "Apprentice is not in the scene",//28
    "The target is not your apprentice",//29
    "Apprentice can't use the skill today",//30
    "Different sect, can't use",//31
    "Target can't get the buff/debuff now",//32
    "Spell failed due to the landforms",//33
    "Target unqualified",//34
    "Too close",//35
    "Can only use when the target's HP is lower than 25%",//36
    "Can only use when the target's HP is lower than 25%",//37
    "Guard Star unqualified",//38
    "Equipped weapon unqualified",//39
    "Lover level unqualified",//40
    "Guard Star is sealed",//41
    "Star level unqualified",//42
    "Mode error",//43
    "Insufficient SP",//44
    "Insufficient SP",//45
    "Insufficient item",//46
    "Insufficient money",//47
    "Can't use without pet gaurding",//48
    "Insufficient pet HP",//49
  };
#endregion
	
#region 創角介面
  public static readonly string LittleCatName = "Aslan";
  public static readonly string ArtefactHintText = "<Please Select one Weapon!>";	
  public static readonly string[] ItemComfirmText = new string[2] {"ComfirmDialog","Are you sure?"};	
  public static readonly string[] NameCheckingStr = new string[4] { "This Name has been used", "This name is illegal", "Chinese name can not contain more than 7 characters, Please enter character's name"};	
  public static readonly string[] ServerResultStr = new string[3] { "This server is full", "This character has been created before","This account can not have two characters"};
  public static readonly string[] MainButString = new string[3] { "Complete", "Back", "Random" };	
  public static readonly string[] GenerderTitleString = new string[3] { "Genender", "Male", "Female" };	
  public static readonly string[] BodyTitleString = new string[4] { "Body Shape", "Body Type", "Underwear", "Skin Color"};	
  public static readonly string[] FaceTitleString = new string[3] { "Face", "Face", "Eye Color"};		
  public static readonly string[] HairTitleString = new string[3] { "Hair Style", "Hair", "Hair Color"};
  public static readonly string[] SalonTitleString = new string[2] { "Dying Hair...", "Salon"};	
   public static readonly string[] EndingMessage = new string[3] 
	{ 
		"Congradulation!!",
		"You have created a new role!",
		"Back to website"
	};		
	
  public static readonly string[] LittleCatMotion = new string[6] {"idle01", "gather01", "rest01", "run01", "ready01", "walk01"};

  public static readonly string[] CreateCharacterDialogue1 = new string[2] 
	{ 
		"Oh no！ How come I wake up now? I should be sleeping in the xxx sea.", 
		"You are so lucky to see me。" 
	};	
	
	public static readonly string[] CreateCharacterDialogue2 = new string[1] 
	{ 
		"You can use this holy artefact to rebuild Atlantis and defeat Diablo。"
	};
	
	public static readonly string[] CreateCharacterDialogue3 = new string[1] 
	{ 
		"My friend, it's nice to meet you! Let's start our journey now!"
	};
	
	public static readonly string AskNameInput = "What is your name？";
	
	public static readonly string[] ArtefactNames = new string[6] 
	{ 
		"A~", 
		"B~",
		"C~", 
		"D~",
		"E~", 
		"F~"
	};
	
   public static readonly string[] ArtefactInfo = new string[6] 
	{ 
		"AAA~", 
		"BBB~",
		"CCC~", 
		"DDD~",
		"EEE~", 
		"FFF~"
	};	

   public static readonly string[] ArtefactDescription = new string[6] 
	{ 
		" AAA~~~~", 
		" BBB~~~~", 
		" CCC~~~~", 
		" DDD~~~~", 
		" EEE~~~~",
		" FFF~~~~"
	};
	
#endregion	
	
#region Crazy use
  public static readonly string CrzS_00001 = "Function Box";
  public static readonly string[] CrzS_00002 = new string[2] { "Actions", "Functions" };
  public static readonly string CrzS_00003 = "strength LV{0}";
  public static readonly string CrzS_00004 = "Raising Tool";
	public static readonly string CrzS_00005 = "Civilian bring now tool, doing research on it to get new craft materials";
	public static readonly string[] CrzS_00006 = new string[UI_RecruitFM.MAX_TOOLINFO] { "Name：", "Place：", "需求名氣：" };
  public static readonly string CrzS_00007 = "Refresh";
  public static readonly string CrzS_00008 = "Raise";
	public static readonly string CrzS_00009 = " Only ";
  public static readonly string CrzS_00010 = "System Menu";
  public static readonly string[] CrzS_00011 = new string[UI_SystemFM.MAX_BUTTON_NUM] { "Game Options"/*, "Server List"*/, "Exit Game" };
  public static readonly string CrzS_00012 = "Game Options";
  public static readonly string[] CrzS_00013 = new string[3] { "Graphic", "Settings", "Message" };
  public static readonly string[] CrzS_00014 = new string[UI_SysOptionFM.MAX_OPTION_PART_NUM] 
  { "Tab Order", "Opeartion", "Camera", "Audio"};
  public static readonly string[][] CrzS_00015 = 
  {
		new string[UI_SysOptionFM.MAX_RADIO1_NUM]{"All Players", "Players First", "Players Only" },
		new string[UI_SysOptionFM.MAX_RADIO2_NUM]{"A Mode", "B Mode"}
  };
  public static readonly string[] CrzS_00016 = new string[4] { "Camera Distance", "Camera Speed", "Sound Volume", "Music Volume" };
	public static readonly string[] CrzS_00017 = new string[UI_SysOptionFM.MAX_BUTTON_NUM] { "Beck", "Apply", "Cancel" };
	public static readonly string[][] CrzS_00018 = 
	{
		new string[UI_SysOptionFM.MAX_VISION_CHECK_NUM]
		{"Fullscreen", "Battle Particles", "Scene Effects", "Aura Light", "Particle Soften", "Battle Shaking", "描邊效果", "反鋸齒效果"},
		new string[UI_SysOptionFM.MAX_OPTION_CHECK_NUM]{"Sound", "Music" },
		new string[UI_SysOptionFM.MAX_MESSAGE_CHECK_NUM]
		{"Player Info", "HP Info", "NPC Name", "Treasure Gaining", "Party Treasure Gaining", "Friend Inquiry", "Party Inquiry", "星盤提示"}
	};
	public static readonly string CrzS_00019 = "低";
	public static readonly string[] CrzS_00020 = new string[UI_SysOptionFM.MAX_COMBO_NUM] 
  { "Mode", "Visible Range", "Vision Effect", "Shadow Effect", "Water Effect", "解析度" };
  public static readonly string[] CrzS_00022 = new string[3] { "Player Display", "Info Display", "Message Settings" };
  public static readonly string[] CrzS_00023 = new string[4] { "None", "Party Members Only", "Friends Only", "All" };
  public static readonly string[] CrzS_00024 = new string[7] { "Player Info", "HP Info", "NPC Name", "Treasure Gaining", "Party Treasure Gaining", "Friend Inquiry", "Party Inquiry" };
  public static readonly string CrzS_00025 = "About In-game Password";
  public static readonly string[] CrzS_00026 = new string[2] { "Change", "Cancel" };
  public static readonly string[] CrzS_00027 = new string[3] 
  { "1.There are two ways to input the password, use keyboard or virtual keyboard shown on the screen. To prevent from being recorded by  keyloggers, input password with the virtual keyboard is advised.", 
    "2.The number 0~9 will be shown in random order on the virtual keyboard every time.", 
    "3.Correct In-game Password is needed before using in-game function such as warehouse and virtual shop etc.." 
  };
  public static readonly string CrzS_00028 = "Change password";
  public static readonly string[] CrzS_00029 = new string[3] { "Please input the In-game Password", "Set up In-game Password", "Retype In-game Password" };
  public static readonly string CrzS_00030 = "Channel List";
  public static readonly string CrzS_00031 = "Channel{0:00}";
  public static readonly string CrzS_00032 = "Material Box";
	public static readonly string[] CrzS_00033 = new string[2] 
	{
		"Click Sell button to sell unneeded tools and materials",
		"Click Use button to put the tool into your Tower of Civilization"
	};
  public static readonly string[] CrzS_00034 = new string[2] { "Materials" , "Tools"};
  public static readonly string CrzS_00035 = "Page{0}/{1}";
  public static readonly string CrzS_00036 = "Sure to change to " + CrzS_00031 + "？";
	public static readonly string CrzS_00037 = "Sure to end game？";
	public static readonly string CrzS_00038 = "銀幣已達上限，請先消耗一些";
  public static readonly string CrzS_00039 = "Mail";
  public static readonly string[] CrzS_00040 = new string[2] { CrzS_00039, "Package" };
  public static readonly string[] CrzS_00041 = new string[3] { "Write a Mail", "Delete All", "Contact Us" };
  public static readonly string[] CrzS_00042 = new string[2] { CrzS_00039, "GM" };
  public static readonly string CrzS_00043 = "Date";
  public static readonly string CrzS_00044 = "Title";
  public static readonly string[,] CrzS_00045 = new string[2, 3]
  {
    {"From",CrzS_00043, CrzS_00044},
    {"To",CrzS_00043, CrzS_00044}
  };
  public static readonly string[,] CrzS_00046 = new string[2, 2] {{"Reply", "Delete"}, {"Send", "Cancels"}};
	public static readonly string CrzS_00047 = "以%y{0}%w銀幣賣出";
	public static readonly string CrzS_00048 = "報到人數";
	public static readonly string CrzS_00049 = "銀幣數量{0:0%}";
	public static readonly string CrzS_00050 = "活動";
	public static readonly string CrzS_00051 = "這一位是新朋友，獎勵能量將於中午12:00後補滿";
  public static readonly string CrzS_00052 = "獎勵能量已用完";
	public static readonly string CrzS_00053 = "名氣值不足{0}無法使用此工具";
  public static readonly string CrzS_00054 = "Send out successfully";
  public static readonly string CrzS_00055 = "{0}doesn't exit!";
  public static readonly string CrzS_00056 = "{0}'s mailbox is full, please try again later!";
  public static readonly string CrzS_00057 = "Purchase Silver";
  public static readonly string CrzS_00058 = "The {0} is full, please sell some unneeded things fisrt";
  public static readonly string CrzS_00059 = "Reise the tool %g{0}%w that brought from civilian Need to give a bounty of %y%AT%w Silver|Sure to raise the tool?";
  public static readonly string CrzS_00060 = "Purchase Gold";
  public static readonly string CrzS_00061 = "Your Excellency: Great bounty appeals braves. Are you willing to spend {0} Gold to make civilians to bring new tools even sooner?";
	public static readonly string[] CrzS_00062 = new string[2] { "Sell", "Use" };
	public static readonly string CrzS_00063 = " Need ";
  public static readonly string CrzS_00064 = "{0}'s Tower of Civilization";
  public static readonly string CrzS_00065 = "Lobby of Tower of Civilization";
  public static readonly string CrzS_00066 = "Place";
  public static readonly string CrzS_00067 = "Complete now";
  public static readonly string[] CrzS_00068 = new string[2] { "Research", "Remove" };
  public static readonly string[] CrzS_00069 = new string[2] 
  {
	"Build more floors then you can place more tools", 
	"Required to build：" 
  };
  public static readonly string[] CrzS_00070 = new string[4] { "Production", "Manufacture", "Process", "Dream" };
  public static readonly string CrzS_00071 = "Insufficient Silver to build a floor! Earn more Silver by using tools more!";
  public static readonly string[] CrzS_00072 = new string[2] { CrzS_00057, GlobalConstString.MidMapTip_05 };
  public static readonly string CrzS_00073 = "Rank reached {0}, makes building time faster for 10%";
	public static readonly string CrzS_00074 = "增建{0}%w樓層|消耗銀幣%y{1}";
  public static readonly string CrzS_00075 = "%g(Because of your Rank, building time is faster for 10%)";
  public static readonly string[] CrzS_00076 = new string[2] 
  { 
	"Build completed",
	"after　day　hour　minute" 
  };
  public static readonly string CrzS_00077 = "{0}F Constructing...";
  public static readonly string CrzS_00078 = "You fame inceases for {0} point. you are more and more famous in the kingdom";
	public static readonly string[] CrzS_00079 = new string[2] 
	{ 
		"There are chances to get various materials when researching tools.", 
		"[[[underline]]]Go to Recruit Tools to recruit new tools brought by civilians." 
	};
  public static readonly string[] CrzS_00080 = new string[2] { "Tool Research", "Unlock new technology" };
	public static readonly string[] CrzS_00081 = new string[2] { "使用書頁", "Unlock" };
  public static readonly string CrzS_00082 = "Got：{0}";
  public static readonly string CrzS_00083 = "You need to put this tool in suitable place to research more";
  public static readonly string CrzS_00084 = "Required materials to unlock new technology";
  public static readonly string CrzS_00085 = "Ask Friends";
  public static readonly string CrzS_00086 = "There is no energy, please wait for regaining";
  public static readonly string CrzS_00087 = "The {0} floor of Tower of Civilization is completed|The workers constructed {1} for you";
  public static readonly string CrzS_00088 = "Congradulations! You have reached the highest level in Tower of Civilization!";
  public static readonly string CrzS_00089 = "Alreadt reached the highest";
  public static readonly string CrzS_00090 = "Tower of Civilization is under constructing";
  public static readonly string CrzS_00091 = "You have practiced max on {0}, and ready to unlock the next skill!";
  public static readonly string CrzS_00092 = "Click to cancel practicing";
	public static readonly string[] CrzS_00093 = new string[7] 
	{ UnStr_1401, CrzS_00004, "Zio Power Mixer", CrzS_00032, "Techpedia", Str2056_0030, Str_ShopMall_String[0]};
  public static readonly string CrzS_00094 = "Reconstruct Floor";
	public static readonly string CrzS_00095 = "重新裝修這個樓層|需要花費%j{0}%w金";
	public static readonly string CrzS_00096 = "You need to remove all the tools first";
	public static readonly string CrzS_00097 = "Help";
	public static readonly string CrzS_00098 = "Bonus {0}";
	public static readonly string[] CrzS_00099 = new string[2] { "Info of friend error", "Visit friend too frequently, please try again later"};
	public static readonly string CrzS_00100 = "已經無法協助更多好友";
	public static readonly string CrzS_00101 = "Medium";
	public static readonly string CrzS_00102 = "High";
	public static readonly string[][] CrzS_000103 = new string[UI_SysOptionFM.MAX_COMBO_NUM][]
	{
		new string[4]{"Default", "Visual Quality First", "Performance First", "Manual" },
		new string[5]{"Closest", "Close", "Normal", "Far", "Farest"},
		new string[5]{"Lowest", CrzS_00019, CrzS_00101, CrzS_00102, "Highest"},
		new string[3]{CrzS_00019, CrzS_00101, "Real"},
		new string[3]{CrzS_00019, "Medium", CrzS_00102},
		new string[]{""}
	};
	public static readonly string CrzS_00104 = "Leave Tower of Civilization";
	public static readonly string CrzS_00105 = "Back to my Tower of Civilization";
	public static readonly string CrzS_00106 = "Reconstruct Floor";
	public static readonly string CrzS_00107 = "<1 Min";
	public static readonly string CrzS_00108 = "When your Chivalry rank reaches%l{0}%w, the speed of constructing new floor will increase %y10%";
	public static readonly string CrzS_00109 = "%y【{0}】";
	public static readonly string CrzS_00110 = "%lThe research speed increases 300%|The gaining Silver from researchg increase 100%";
	public static readonly string CrzS_00111 = "%wWhen this tool placed in %g{0}%w, you will get bonus effects, %lincreasing research speed%w and %lable to learn higher skills";
	public static readonly string[] CrzS_00112 = new string[2] { "Fullscreen", "Exit Fullscreen" };
	public static readonly string CrzS_00113 = "立即完成樓層擴建|需要花費%j{0}%w金|是否要立即完成？";
	public static readonly string[] CrzS_00114 = new string[2] 
	{ 
		"Keyboard WSAD for operating moving, and QE for camara", 
		"Keyboard WSQE for operating moving, and AD for camara", 
	};
	public static readonly string CrzS_00115 = "The currency of Atalntis, you can get it when research in Tower of Civilization";
	public static readonly string CrzS_00116 = "%wleft Click:  Research    /     Right Click:  Remove";
	public static readonly string CrzS_00117 = "[[[color={0}]]]{1}%w|Next refresh";
	public static readonly string CrzS_00118 = "機率刷新各種工具";
	public static readonly string CrzS_00119 = "Civilian with new tools with arrive after the time";
	public static readonly string CrzS_00120 = "%r|Click on the icon of Tower of Civilization on the Quick Launch Bar|to start playing Master System";
	public static readonly string CrzS_00121 = "－The Start of Legend－";
	public static readonly string[] CrzS_00122 = new string[3] 
	{ 
		"以驚人的戰鬥實力|取下破壞神首級|傳說中的英雄現身",
		"以絕世的奇巧智慧|研發出高度科技|神話級的大師風範",
		"命中注定你將經歷這兩段旅程|[[[color=255,128,128]]]請選擇%w你傳奇的第一步"
	};
	public static readonly string[] CrzS_00123 = new string[2] { "以冒險為起點", "以經營為起點"};
	public static readonly string[] CrzS_00124 = new string[2] { "熱血戰鬥的角色扮演", "模擬經營的文明建設"};
	public static readonly string[] CrzS_00125 = new string[2] 
	{ 
		"您選擇先成為一名%y戰士%w，進行%y熱血戰鬥的角色扮演之路。",
		"您選擇先成為一名%y大師%w，進行%y模擬經營的文明建設之路。"
	};
	public static readonly string CrzS_00126 = "%w可得材料：%o{0}";
	public static readonly string CrzS_00127 = "%w適合場所：{0}";
	public static readonly string CrzS_00128 = "研究速度{0}%|{1}|最高技能{2}階|";
	public static readonly string CrzS_00129 = "%b{0}[[[color=248,210,191]]] Gold";
	public static readonly string[,] CrzS_00130 = new string[2,2]
	{
		{"Music ON", "Music Off"},
		{"Sound On", "Sound Off"}
	};
	public static readonly string CrzS_00131 = "戰鬥中無法離開遊戲";
	public static readonly string CrzS_00132 = "第{0}階段";
	public static readonly string CrzS_00133 = "[[[color=255,240,197]]]統計情形";
	public static readonly string[] CrzS_00134 = new string[2] { "達成階段", "報到好友"};
	public static readonly string[] CrzS_00135 = new string[3] { "詳細規則&教學", "領福袋x{0}", "領獎" };
	public static readonly string CrzS_00136 = "獎勵";
	public static readonly string CrzS_00137 = "快點邀請您的朋友們上線，進行報到就有機會獲得好禮！";
	public static readonly string[] CrzS_00138 = new string[]
	{
		"「活動規則」",
		"1.%y活動時間%w：每天的PM20:00~PM22:00之間皆可報到，系統每30分鐘判斷一次人數。",
		"2.%y活動方法%w：成功報到玩家在%r20:30、21:00、21:30、22:00%w會獲得「報到福袋*1」，%r且系統會判斷目前已報到人數，達到設定條件則在線報到玩家皆可以再獲得「達成階段獎勵」%w。",
		"3.%y活動條件%w：",
		"%g《與民同歡》%w第一階段標準是%r80%w人，每達成一個階段，下一個階段標準則會提高，作為下一個階段標準，最高有%r四%w個階段。",
		"%g《好友挺你》%w每個時間點判斷報到人數時，若已報到玩家同時間擁有12個已報到好友在線上，另外會獲得「銀幣驚喜袋*1」。(一天限領一次)",
		"4.%y領獎資格%w：獎勵保留至隔天中午11:59分，超過則系統回收，不予補發。"
	};
	public static readonly string CrzS_00139 = "報到成功|獲得5000銀幣";
	public static readonly string CrzS_00140 = "等級不足{0}級";
	public static readonly string CrzS_00141 = "達{0}人：{1}  +{2}";
	public static readonly string CrzS_00142 = "非活動時間";
	public static readonly string CrzS_00143 = "選擇你要的夢幻系樓層|(功能性樓層已建造{0}/{1})";
	public static readonly string CrzS_00144 = "(已擁有)";
	public static readonly string CrzS_00145 = "(可建造)";
	public static readonly string CrzS_00146 = "(未達條件)";
	public static readonly string CrzS_00147 = "同一種夢幻系場所只能同時擁有一所";
	public static readonly string CrzS_00148 = "尚未符合建造條件，將游標移動到樓層名稱可查看建造條件";
	public static readonly string CrzS_00149 = "(可蓋出未擁有樓層)";
	public static readonly string CrzS_00150 = "樓層高度不足以擁有更多夢幻系場所";
	public static readonly string CrzS_00151 = "是否使用|{0}|%w銀幣消耗減少|%g{1}";
#endregion

#region Gene use
  public const string GStr_00001 = "Bank";
  public const string GStr_00002 = "Input error";
  public const string GStr_00003 = "({0}Gold needed)";	
  public const string GStr_00004 = "Sure to discard{0}{1}";
  public const string GStr_00005 = "Mail Box";
  public const string GStr_00006 = "The player doesn't exist";
  public const string GStr_00007 = "The Mail Box is full";
  public const string GStr_00008 = "Sure to delete the mail?";
  public const string GStr_00009 = "Sure to spend {0} Silver to send this mail?";
  public const string GStr_00010 = "Sure to quit editing this mail?";
  public const string GStr_00011 = "Mail";
  public const string GStr_00012 = "None";
  public const string GStr_00013 = "Sure to reply this mail?";
  public const string GStr_00014 = "Sure to send out this mail?";
  public const string GStr_00015 = "Post on FB?";
  public const string GStr_00016 = "Take it now";
  public const string GStr_00017 = "System message";	
  public const string GStr_00018 = "此功能目前關閉中...";
  public const string GStr_00019 = "確定要刪除全部信件嗎?";

  public const string GStr_GM = "GM";
  public static string[] GStr_10001 = new string[4] {"ExpandForm","1","2","3"};
  public static string[] GStr_10002 = new string[3] {"MailConfirm","1","2"};
  //public static string[] GStr_10003 = new string[3] { "CountConfirm", "", "" };
  public static string[] GStr_10004 = new string[4] { "PackageConfirm", "Take Package", "itemname", "cost" };	
  public static string[] GStr_10005 = new string[4] {"TradeBuyConfirm","購買","itemname","cost"};	

  public static readonly string[] GStr_20001 = new string[4] { "Rearrange", "Withdraw", "Expand bank space", "Expand the upper limit of silver"};
  public static readonly string[] GStr_20002 = new string[5] { "Withdraw","Withdraw","Enter the number of silver","Withdraw","Deposit"};	
  public static readonly string[] GStr_20003 = new string[2] { "Accept", "Cancel"};	
  public static readonly string[] GStr_20004 = new string[29] { //錯誤訊息-server
		"Can't use Bank undter the status",  "Silver insufficient, unable to deposit","Silver insufficient，unable to withdraw ","Item insufficient, unable to deposit",  //1-5
		"Item insufficient, unable to withdraw","Reached the upper limit of money, unable to deposit","Reached the upper limit of items, unable to deposit","Money on the character reached the upper limit, unable to withdraw",//6-9
		"No such item, can't deposit","No such item", "can't withdraw","Items in the Bag reached the upper limit, unable to withdraw",	"In-game Password error","Invalid Bank item index",//10-13
		"Can't accumulate different items","This grid is occupied","Insufficient character weight carriage","Invalid item index","Rental expired, usage of this grid is forbidden","Bank rental is full",//14~18
		"Rearrangement completed","Rearrangement failed","Role Bank rearrangement completed","Role Bank rearrangement failed","Sky Crystal insufficient, unable to deposit",//19~24
		"Sky Crystal insufficient, unable to withdraw","Reached the upper limit of Sky Crystal, unable to deposit","Skt Crystal on the character reached the upper limit, unable to withdraw",//25~27
		"Deleted Bank item failed"};
  public static readonly string[] GStr_20005 = new string[8] { //訊息-client
		"Deposit","Withdraw","Delete"," "," ","This scene can not use the bank!","金幣不足,無法擴充","銀行空間已達上限"};//0~7		
  public static readonly string[] GStr_20006 = new string[2] {
		"Completed","Failed"};		
  public static readonly string[] GStr_20007 = new string[2] { 
		"InputForm","Please input the amount"};	
  public static readonly string[] GStr_20008 = new string[2] {
		"Sure to expand the upper limit of Silver?","Expand{0}"};	
  public static readonly string[] GStr_20009 = new string[2] {
		"Sure to enlarge the bank space?","Enlarge for{0}grid",};
  public static readonly string[] GStr_20010 = new string[2] {
		"Mail Box","Package"};
  public static readonly string[] GStr_20011 = new string[4] {
		"Write mail","Delete all","Contact us","New package"};
  public static readonly string[] GStr_20012 = new string[5] {
		"To","From","Received time","Title","寄件時間"};
  public static readonly string[] GStr_20013 = new string[4] {
		"Send","Cancel","Reply","Delete"};
  public static readonly string[] GStr_20014 = new string[5] {	
		"","Succeeded","Failed, mail amount in the server reaches maximum","","GM is currently offline"};
  public static readonly string[] GStr_20015 = new string[8] {	//client訊息
		"Insuffient Silver","You haven't input the receiver!","You haven't input the title/text!","Pleace close the package first!!","銀幣上限:{0}",
		"郵票數：{0} / {1}。|可至購物中心（X）購買郵票|※ 需點擊使用郵票來進行補充",//5
		"請透過「回報客服」來進行回報","需VIP 3方可使用新增快遞功能"};//6~7
  public static readonly string[] GStr_20016 = new string[4] {
		"Charging Policy","Item","Stamp","Each item takes {0} Stamp"};
  public static readonly string[] GStr_20017 = new string[2] { 
		"Sure to send out this package?","Sure to quit editing this package?"};
  public static readonly string[] GStr_20018 = new string[3] {  
		"This item can't be stored in bank!","This item can't be stored in mail box!","From:"};
  public static readonly string[] GStr_20019 = new string[17] { //server訊息-鏢局
		"","Insufficient Silver","Insufficient item amount","No enough space in the receiver's Mail Box","No such player","Exceeded stack-up limit","Unable to take, no enough item amount to take form the Mail Box",//1-6
		"Unable to take, lack of Bag space","Receiver's Mail Box is accupied by others, please wait","Exceeded item amoubt limit","Sending package successfully","Trade failed","Exceeded receiver's upper limit of Silver",//7-12
		"Unable to take, exceeded upper limit of Silver","No such cash envelope","send failure","not enough stamp(s)"};//13-16
  public static readonly string[] GStr_20020 = new string[10] {  //client訊息
		"Unable to use Mail Box in current player status or it's already open!","Lack of Bag space!","Can't send package to yourself!!!","%b of %a","%a sent %b to %c","Take package{0}",
		"There's package in your Mail Box","over the maximun stamps","receive {0} stamps","尚未置入物品"};//6-9
  public static readonly string[] GStr_20021 = new string[2] {
		"Sure to spend {0} Silver to take out the item?","The item can only be taken from the mail box in the capital"};
  public static readonly string[] GStr_20022 = new string[18] {//拍賣場文字標籤		
		"金幣區","銀幣區","販賣","等級","顏色","關鍵字","購買","下架","商品出售區",//0-8
		"物品名稱","數量","價格","上架","交易中心","商品上架區","出售","個人上架物品","只顯示自己職業適用"};	//9-17
  public static readonly string[] GStr_20023 = new string[30] {	//server訊息-交易中心
		"","目前交易中心交易人數過多,請稍候","系統忙碌中請稍候","上拍數量已達上限","找不到你要下架的物品","身上無此物品","不能購買自己上架商品",//0~6
		"此物品已下架","同帳號角色之間不能互相買賣","此場景不可開啟交易中心","競標價必須高於底價","競標商品出價被其他玩家超過","銀幣不足支付上架費",//7~12
		"貨幣中心未清空","競標價不合法","金幣不足","此物品出售中","非消費伺服器","購買失敗","購買銀幣不足","上架失敗","交易中心關閉中","上架成功",//13~22
		"交易中心上架數量已滿","下架成功","下架失敗","出售成功","出售失敗","出售失敗，超過玩家銀幣上限","此物品回收數量已達上限"}; //23~29
  public static readonly string[] GStr_20024 = new string[15] {	//client訊息-交易中心
		"資料傳輸中...請稍候","此物品不可交易","確定出售此項物品","{0}已經成功跟你購買{1}","購買{0}成功",//0..4
		"此物品不可賣金幣","此物品不可賣銀幣","低於交易金幣下限({0})","超過交易金幣上限({0})","手續費不足,需{0}銀幣",//5..9
		"身上物品空間不足，無法購買","搜尋失敗","搜尋結束","確定上架此項物品","須完整物品才可上架" };//10..14
  public static readonly string[] GStr_20025 = new string[2] {
		"共計金幣:","共計銀幣:"};
  public static readonly string[] GStr_20026 = new string[3] {//for trade hint
		"請輸入欲上架的金幣價格|須收上架費:{0}銀幣","我們願意用以下價格({0}~{1}銀幣)|向你回收此二手物品","{0}/{1}"};	
  public static readonly string[] GStr_20027 = new string[4] {//拍賣場大標籤
		"銀幣區","金幣區","銀幣出售","金幣上架"};	//0-3	
  public static readonly string[] GStr_20028 = new string[5] {//GM回報
		"儲值問題回報","BUG問題回報","任務問題回報","投訴玩家回報","其他問題回報"};	
	
  public static readonly string[] GTimeType = new string[2]{
		 "{0}/{1}/{2}","{0}/{1}/{2}"};
  public static readonly string[] GColor = new string[5]{
		 "無","白","藍","紫","金"};	
  public static readonly string[] GLevel = new string[6]{
		 "無","0~40","40~80","80~120","120~160","160~200"};
  //all SpriteVectorText name
  public const string Common_TitleName = "Title";	
  public const string Common_ButtonQ = "ButtonQ";
  public static readonly string[] Common_ContextName = new string[] {"Context1","Context2","Context3"};	
  public static readonly string[] Common_ButtonName = new string []{"Btn_First","Btn_Second"};	
  public static readonly string[] Common_RadioName = new string []{"Radio1","Radio2","Radio3"};		
#endregion		
	
#region RplaceStr
	public static readonly string[][] ReplaceStr = new string[6][]
	{
		new string[]{"Mister",   "Miss"},		//%B
		new string[]{"Sir",   "Madam"},		//%D
		new string[]{"Prince",   "Princess"},		//%E
		new string[]{"Duke",   "Duchess"},	//%F
		new string[]{"Gentleman",   "Lady"},		//%G
		new string[]{"大哥哥", "大姊姊"}		//%H
	};
#endregion

#region Base
	public static readonly string[] Base0001 = new string[]
	{
		"{0}%y EXP",
		"{0}%y Silver",
		"{0}%y 金幣",
		"{0}%y 王國點",
		"{0}%y 權力點",
		"{0}%y 能量",
		"{0}%y 訓練時間",
		"{0}%y 神器經驗值",
		"{0}%y 榮耀值",
	};
#endregion
	
#region Assessment
	public static readonly string[] Assessment0001 = new string[]
	{
		"",
		"Lack of space",
		"Exceeded the upper limit of Silver"
	};
	
	public static readonly string[] Assessment0002 = new string[]
	{
		"",
		"Council_y",
		"Council_g",
		"Council_r",
		"Council_p",
		"Council_b"
	};
	
	public static readonly string[] Assessment0003 = new string[]
	{
		"",
		"_001",
		"_002",
		"_003",
		"_004",
		"_005",
		"_006",
		"_007",
		"_008",
		"_009",
		"_010"
	};
	
	public static readonly string[] Assessment0004 = new string[]
	{
		"",
		"風行之地",
		"土息之地",
		"知識之地",
		"火炙之地",
		"水湧之地"
	};
	
	public static readonly string[][] Assessment0005 = new string[7][]
	{
		new string[]{""},
		new string[]{"","11133","11134","11135","11136","11137","11138","11139","11140","11141","11142","11143","11144","11145","11146","11147"},
		new string[]{"","11148","11149","11150","11151","11152","11153","11154","11155","11156","11157","11158","11159","11160","11161","11162"},
		new string[]{"","11163","11164","11165","11166","11167","11168","11169","11170","11171","11172","11173","11174","11175","11176","11177"},
		new string[]{"","11178","11179","11180","11181","11182","11183","11184","11185","11186","11187","11188","11189","11190","11191","11192"},
		new string[]{"","11193","11194","11195","11196","11197","11198","11199","11200","11201","11202","11203","11204","11205","11206","11207"},
		new string[]{"","11208","11209","11210","11211","11212"}
	};
	
	public static readonly string[] Assessment0006 = new string[]
	{
		"Accept","Return"
	};
	
	public static readonly string[] Assessment0007 = new string[]
	{
		"Information","Recruitment"
	};
	
	public static string[] Assessment0008 = new string[4] {"AssessmentConfirm","Note: Recruit","You can sill recruit friends as your vassals","Recruit now?"};	
	
	public const string Assessment0009="COST：";
	
	public const string Assessment0010="There is/are {0} vassal not assigned yet";
	
	public static readonly string[] Assessment0011 = new string[]
	{
		"","Recruitment","Replace"
	};
	
	public const string Assessment0012="Appoints the subordinate({0}/{1}):";
	
	public static readonly string[] Assessment0013 = new string[]
	{
		"Strength：","Agility：","Vitality：","Intelligence：","Spirit：",
		"攻擊強度：","法術傷害：","防禦力："
	};
	
	public static readonly string[] Assessment0014 = new string[]
	{
		"Sure","Replace vassal"
	};
	
	public static readonly string[] Assessment0015 = new string[]
	{
		"Attributes","Medal","Personality"
	};
	
	public static string[] Assessment0016 = new string[4] {"Replace Confirm","Notice","The attributes and personality will be reset after replace vassal","but the Medels(skills) will be kept"};	
	
	public static readonly string[] Assessment0017 = new string[]
	{
		"確　　定","領取獎賞"
	};
	
	public const string Assessment0018="贈送";
	public static readonly string[] Assessment0019 = new string[]
	{
		"名次","職業","名稱","分數"
	};
	
	public const string Assessment0020="權力點不足";
		
	public const string Assessment0021="此部下無可回報任務。";
		
	public const string Assessment0022="位階-";
	
	public static readonly string[] Assessment0023 = new string[]
	{
		"王國點 +{0}",
		"權力點 +{0}"
	};
	
	public static readonly string[] Assessment0024 = new string[]
	{
		"部下資訊","位階排行榜","收件夾","招募","社群","物品"
	};

	public static readonly string[] Assessment0025 = new string[]
	{
		"確　　定","指派工作"
	};
	
	public const string Assessment0026="等級不足{0}級無法進入騎士團";
	
	public static readonly string[] Assessment0027 = new string[]
	{
		"",
		"工作中",
		"已完成",
		"待替換",
		"待招募",
		""
	};
	
	public const string Assessment0028="無可指派部下。";	
	
	public const string Assessment0029="部下已達上限。";
	
	public const string Assessment0030="部下不存在";
	
	public const string Assessment0031="恭喜您的騎士位階提升至{0}階";
	
	public static readonly string[] Assessment0032 = new string[]
	{
		"%y風行之地||%w主要取得屬性：%j敏捷|%w可能取得屬性：%j智力",
		"%y土息之地||%w主要取得屬性：%j體力|%w可能取得：%j榮譽勳章(技能)",
		"%y知識之地||%w主要取得屬性：%j智力|%w可能取得屬性：%j體力",
		"%y火炙之地||%w主要取得屬性：%j力量|%w可能取得屬性：%j未知",
		"%y水湧之地||%w主要取得屬性：%j精神|%w可能取得：%j性格"
	};
	
	public const string Assessment0033="部下: {0}/{1}";
	
	public static readonly string[] Assessment0034 = new string[]
	{
		"",
		"已選取",
		"待替換"
	};
	
	public const string Assessment0035="消耗權力點立即完成任務";
	
	public static readonly string[] Assessment0036 = new string[]
	{
		"%o力量：%w|可提高攻擊強度與格檔",
		"%o敏捷：%w|可提高攻擊",
		"%o體力：%w|可提高生命最大值、招架",
		"%o智力：%w|可提高法術傷害、治療量、法術爆擊、魔力最大值",
		"%o精神：%w|可提高生命回復、魔力回復、韌性"
	};
	
	public static string[] Assessment0037 = new string[4] {"Assessment0037","提示","進行此操作將會花費您{0}枚金幣","並消除此勳章，確定要繼續嗎？"};
	
	public const string Assessment0038="此欄不可清除!!";
	
	public static string[] Assessment0039 = new string[4] {"Assessment0039","提示","進入騎士團將會離開隊伍，","是否繼續？"};
	
	public const string Assessment0040="(提升為{0}階騎兵還需總屬性點數:{1}點)";
	
	public const string Assessment0041="能力:";
	
	public static string[] Assessment0042 = new string[4] {"Assessment0042","提示","進行此操作將會花費您{0}枚金幣","並消除此性格，確定要繼續嗎？"};
	
	public const string Assessment0043="{0}階-{1}";
	
	public const string Assessment0044="恭喜你的部下";
	
	public const string Assessment0045="提升至";
	
	public const string Assessment0046="下階段目標";
	
	public const string Assessment0047="騎士位階達{0}階後即可解鎖此任務";
	
	public const string Assessment0048="%o力量：%w|可提高攻擊強度與格檔|%o敏捷：%w|可提高攻擊、閃避、物理爆擊|%o體力：%w|可提高生命最大值、招架|%o智力：%w|可提高法術傷害、治療量、法術爆擊、魔力最大值|%o精神：%w|可提高生命回復、魔力回復、韌性|%r屬性數值為紅色時，則不可再從各項任務中|獲得該屬性的提升，必須透過玩家升級的來|提高部下的屬性數值上限";
	
	public const string Assessment0049 = "銀行開啟中無法進入騎士團";
	
	public const string Assessment0050 = "部下工作中，無法替換";
	
	public const string Assessment0051="目前一次最多可指派{0}位，提升VIP等級至{1}，可增加指派數量至{2}位";
	
	public const string Assessment0052="[[[lineGap=2]]]%j替換部下將會消除已獲得的屬性與性格，但您可花費{0}銀幣或{1}金幣同時保留屬性與性格。||請選擇替換部下的方式：|花費：|          {0}|                 {1}";
	
	public static readonly string[] Assessment0053 = new string[]
	{
		"保留勳章與性格","保留勳章與性格","直接替換不保留"
	};
#endregion
	
#region Advanture
	
	public static string Advanture0001 = "提示";
	
	public static string Advanture0002 = "%w是否成立冒險團？%y須滿30級、需冒險團之證或20萬銀幣";
	
	public static string Advanture0003 = "身上沒有冒險團之證而且銀幣不足{0}，無法創立！";
	
	public static string Advanture0004 = "等級未滿{0}級，無法創立！";
	
	public static string Advanture0005 = "已加入冒險團，無法另行成立新冒險團！";
	
	public static string[] Advanture0006 = new string[2]{"ConfirmSendAdvName", "請輸入冒險團名稱"};
	
	public static string Advanture0007 = "招收";
#endregion
	
#region EnergyTran
	public static readonly string[] EnergyTran0001=new string[]
	{
		"能量說明：|每2分鐘恢復1點能量，自然恢復時能量不會超過能量上限，可使用金幣購買能量。",
		"權力點說明：|每3分鐘恢復1點，權力點現有值不可超過權力點最大值，可消費購買權力點。",
		"銀幣說明：|遊戲中進行裝備修復、星盤、裝備強化......等動作時會消耗銀幣，可消費購買銀幣。"
	};
	
	public static readonly string[] EnergyTran0002=new string[]
	{
		"轉換%y{0}%w金幣為%y{1}%w能量。",
		"轉換{0}金幣為{1}權力點。",
		"轉換{0}金幣為{1}銀幣。"
	};
	
	public static readonly string[] EnergyTran0003=new string[]
	{
		"挑選欲轉換的能量：",
		"挑選欲轉換的權力點：",
		"挑選欲轉換的銀幣："
	};
	
	public static readonly string[] EnergyTran0004=new string[]
	{
		"能量轉換",
		"權力點轉換",
		"銀幣轉換"
	};
	
	public const string EnergyTran0005="訓練時間";
	
	public const string EnergyTran0006="剩餘時間：";
	
	public const string EnergyTran0007="[[[size=15]]]%y訓練時間說明：|%w每日%o中午12點%w可領取最多%o3小時%w，擁有訓練時間時，打倒怪物獲得之%fEXP%w、%f銀幣效益%w為%o500%%w。進入%b戰鬥狀態%w後會自動消耗，在%b副本%w中消耗更快。";
	
	public static readonly string[] EnergyTran0008=new string[]
	{
		"[[[size=15]]]%y【免費】領取訓練時間：|%w可%l【免費】%w領取訓練時間，│%f請注意：%w訓練時間不可累積超過%b3%w小時！",
		"[[[size=15]]]%y【銀幣】購買訓練時間：|%w可使用%l【銀幣】%w購買訓練時間，│%f請注意：%w訓練時間不可累積超過%b3%w小時！",
		"[[[size=15]]]%y【金幣】購買訓練時間：|%w可使用%l【金幣】%w購買訓練時間，│%f請注意：%w訓練時間不可累積超過%b3%w小時！"
	};
	
	public static readonly string[] EnergyTran0009=new string[]
	{
		"能量不可超過上限",
		"權力點不可超過上限",
		"銀幣不可超過上限"
	};
	
	public const string EnergyTran0010="已領取";
	
	public static string[] EnergyTran0011 = new string[3] {"EnergyTranConfirm","提示","訓練時間無法超過3小時，是否仍要領取訓練時間？"};
	
	public static readonly string[] EnergyTran0012=new string[]
	{
		"|並額外獲得1個%g知識匯集能量寶盒%w|有機率從中獲得%b造型背甲─大師的證明",
		"|並可獲得一個%o騎士回饋驚奇袋",
		""
	};
	
	public const string EnergyTran0013="背包放不下贈品囉";
	
	public static readonly string[] EnergyTran0014=new string[]
	{
		"購買{0}能量成功",
		"購買{0}權力點成功",
		"購買{0}銀幣成功"
	};
#endregion

#region Str2056

  //物品Hint
  public static readonly string[] Str2056_0001 = new string[]
  {
    "[[[size=22 color={0}]]]{1}",                //0:物品名稱
    "[[[size=18 color=240,80,80]]]Soul Bound",             //1
    "[[[size=18 color=240,80,80]]]Renting",                //2
    "[[[size=14 color=240,80,80]]]LV Requirement：{0}",    //3
    "[[[size=14 color=255,249,170]]]LV Requirement：{0}",  //4
    "[[[size=14 color=255,249,170]]]Type：{0}",            //5
    "[[[size=14 color=255,249,170]]]Strength Lv：{0}",     //6
    "[[[size=14 color=240,80,80]]]Durability：{0}/{1}",      //7
    "[[[size=14 color=255,249,170]]]Durability：{0}/{1}",  //8
    "[[[size=14 color=170,225,55]]]CD：{0}",              //9
    "[[[size=14 color=255,255,180]]]Weight：{0}",          //10
    "[[[size=14 color=49,255,160]]]Basic Atk+{0}",         //11
    "[[[. color=49,255,160]]]Magic Atk+{0}",               //12
    "[[[size=14 color=49,255,160]]]Basic Def+{0}",         //13
    "[[[. color=49,255,160]]]Magic Def+{0}", //14
    "[[[size=14 color=255,255,180]]]{0}",   //15:裝備效果素質
    "[[[. color=255,255,180]]]{0}",         //16:裝備效果素質
    "[[[size=14 color=170,225,55]]]HP Recover {0}",         //17
    "[[[size=14 color=170,225,55]]]MP Recover {0}",         //18
    "[[[size=14 color=170,225,55]]]EXP add-on {0}",         //19
    "[[[size=14 color=170,225,55]]]Unknown Effect {0}",                //20
    "[[[size=14 color=170,225,55]]]Pition Capacity：{0}/{1}[[[.]]]Per Cure Amount：{2}",   //21
    "[[[size=14 color=255,255,180]]]Player HP heal",                 //22
    "[[[size=14 color=255,255,180]]]Player MP heal",                 //23
    "[[[size=14 color=255,255,180]]]Pet HP heal",                     //24
    "[[[size=14 color=255,255,180]]]Pet MP heal",                     //25
    "[[[size=14 color=255,249,170]]]{0}",                             //26:物品說明
    "[[[size=14 color=130,230,255]]]Rent term: {0} days/{1} days",//27
    "[[[size=14 color=130,230,255]]]Left daily amount of charge{0}times", //28
    "[[[size=14 color=130,230,255]]]※您已經擁有此造型",           //29
    "[[[size=14 color=255,255,180 align=right]]]{0}",                 //30:銀幣賣價
    "[[[size=14 color=170,225,55]]]依玩家等級給予經驗值",               //31:
    "[[[size=14 color=170,225,55]]]{0}",                        //32:狀態Hint說明
    "[[[size=18 color=170,225,55]]]Astrolabe Awake+{0}%",            //33
    "[[[size=18 color=170,225,55]]]Get one item randomly：",           //34:福袋小標題
    "[[[size=18 color=170,225,55]]]Get all the contained items：",           //35:福袋小標題
    "[[[size=18 color=170,225,55]]]Choose one item and get it：",           //36:兌換卡小標題
    "[[[size=14 color=255,249,170]]]{0}",                        //37:福袋兌換卡內容物, 套裝武器系列名稱
    "[[[size=15 align=center color=255,204,0]]]<ALT+click on the item to see details>",     //38
    "[[[size=18 color=245,160,35]]]Star Gem Prayer Effect",                  //39
    "[[[size=14 color=240,225,100]]]Star 1：{0}+{1}{2}",        //40
    "[[[size=14 color=240,225,100]]]Star 2：{0}+{1}{2}",        //41
    "[[[size=14 color=240,225,100]]]Star 3：{0}+{1}{2}",        //42
    "[[[size=14 color=240,225,100]]]Star 4：{0}+{1}{2}",        //43
    "[[[size=14 color=240,225,100]]]Star 5：{0}+{1}{2}",        //44
    "[[[size=14 color=135,135,135]]]Star 1：(Sealed)",          //45
    "[[[size=14 color=135,135,135]]]Star 2：(Sealed)",          //46
    "[[[size=14 color=135,135,135]]]Star 3：(Sealed)",          //47
    "[[[size=14 color=135,135,135]]]Star 4：(Sealed)",          //48
    "[[[size=14 color=135,135,135]]]Star 5：(Sealed)",          //49
    "[[[size=14 color=49,255,160]]]",                        //50.福袋內容物
    "[[[size=14 color=170,225,55]]]move speed：{0}",            //51:座騎速度
    "|[[[size=14 color=170,225,55]]]The highest flying height：{0}",        //52:座騎飛行高度
    "|[[[size=14 color=170,225,55]]]The highest flying height：Unlimited",  //53:座騎飛行高度
    "[[[size=14 color=170,225,55]]]Regain Power {0}",                      //54.藥品效果
    "[[[size=14 color=170,225,55]]]Regain Energy {0}",                     //55.藥品效果
    "[[[size=14 color=130,230,255]]]Left amount of charges：{0}",                   //56
    "[[[size=18 color=240,80,80]]]{0} ({1}/{2})",                 //57.套裝名稱和件數
    "[[[size=14 color=145,135,130]]]{0}",                         //58.套裝武器系列名稱
    "[[[size=14 color=255,249,170]]]{0}",                         //59.套裝有搭配裝備, 套裝效果(已達件數)
    "[[[size=14 color=145,135,130]]]{0}",                         //60.套裝無搭配裝備, 套裝效果(未達件數)
    "[[[size=14 color=130,230,255]]]Set effect,{0} in the set",                //61.
    "[[[size=14 color=255,249,170]]]{0}+{1}",                     //62.套裝效果(已達件數)
    "[[[size=14 color=145,135,130]]]{0}+{1}",                     //63.套裝效果(未達件數)
    "[[[size=14 color=255,249,170]]]Lv：{0}|Max Lv：{1}",               //64
    "[[[size=14 color=255,249,170]]]Exp accumulate：{0}",          //65
    "[[[size=14 color=255,249,170]]]Needed Exp：{0}",              //66
    "[[[size=14 color=170,225,55]]]CD：{0} min(s)",              //67
    "[[[size=14 color=170,225,55]]]CD：{0} hour(s) {1} min(s)",     //68
    "", //69
    "[[[size=14 color=170,225,55]]]{0}",                             //70.神器效果提示文字
    "[[[size=14 color=170,255,55]]]Regain Power {0}",                  //71
    "[[[size=14 color=170,225,55]]]Move speed {0}[[[.]]]Fly height {1}",  //72
    "[[[size=15 align=center color=225,70,54]]]<Right Click to enable auto use>",                //73
    "[[[size=14 color=130,230,255 .]]]......等品項",                 //74:福袋兌換卡內容物超過顯示數量
    "[[[size=14 color=130,230,255]]]剩餘擴充次數：{0}次",             //75.
    "[[[size=14 color=91,215,255]]]主星石：提高星儀攻防加成至{0}%",    //76.
    "[[[size=14 color=135,135,135]]]主星石：提高星儀攻防加成至{0}%",    //77.
    "[[[size=14 color=240,80,80]]]類型：{0}",                      //78
    "[[[size=14 color={0}]]](目前裝備)",                             //79
    "[[[size=14 color=170,225,55]]]累積靈魂值：{0}|覺醒靈魂值：{1}",  //80
    "[[[size=18 color=245,160,35]]]水晶效果",                       //81
    "[[[size=14 color=255,255,0]]]{0}+{1} (Max)",     //82:水晶效果(靈魂值已滿,MAX)
    "[[[size=14 color=145,135,130]]]{0}+{1} (Max)",   //83:水晶效果(靈魂值未滿,Max)
    "[[[size=14 color=240,225,100]]]{0}+{1}",         //84:水晶效果(靈魂值已滿)
    "[[[size=14 color=145,135,130]]]{0}+{1}",         //85:水晶效果(靈魂值未滿)
    "[[[size=18 color=0,0,0,0]]].......[[[size=14 color=245,160,35]]]未鑲嵌",  //86
    "[[[size=18 color=0,0,0,0]]].......[[[size=14 color=245,160,35]]]{0}",    //87
    "[[[size=14 color=255,255,180]]]{0}+{1}",                                 //88:裝備鑲嵌效果
    "[[[size=18 paraGap=2 color=0,0,0,0]]]  `[[[size=14 color=255,255,255]]]    回收價：{0:#,##0}",            //89
    "[[[size=14 color=49,255,160]]]",              //90:福袋內容物(廣播=1)
    "[[[size=14 color=222,158,255]]]",             //91:福袋內容物(廣播=2)
    "[[[size=14 color=170,225,55]]]冷卻倒數：{0} 分",        //92
    "[[[size=14 color=170,225,55]]]冷卻倒數：{0} 秒",        //93
    "[[[size=14 color=170,225,55]]]加倍狀態持續 {0} 秒",     //94.神器槌子
    "", //95
    "恢復權力點 {0}",        //96
    "加倍狀態持續 {0} 秒",   //97
    "移動速度 {0}[[[.]]]飛行高度 {1}",  //98
    "",   //99
    "|%r攻擊強度加成%w：{0}%",  //100:技能說明
    "|%b法術傷害加成%w：{0}%",  //101:技能說明
    "|%g治療量加成%w：{0}%",    //102:技能說明
    "冷卻倒數：{0:0} 分", //103
    "冷卻倒數：{0:0.00} 秒", //104
    "[[[size=18 color=240,80,80]]]適用魔化裝備[[[size=14 color=255,249,170]]]", //105
    "|{0}", //106
    "[[[size=14 color=141,180,227]]]星石能量：{0}+{1}",//107
    "[[[size=18 color=170,225,55]]]技能說明",   //108
    "[[[size=14]]]%w距離：{0:0}{1}{2}{3}|%w{4}",   //109:技能說明
    "", //110
    "[[[size=14 color=130,230,255]]]※您已經加冕過此頭銜",  //111
    "|[[[size=14 color=170,225,55]]]騎乘人數：{0}", //112
    "已達最高", //113
    " (MAX)", //114
    "[[[size=14 color=212,255,39]]]儲存靈魂值：{0}",   //115
    "[[[size=14 color=130,230,255]]]※您已經喚醒過此星！", //116
    "[[[size=14 color=255,249,170]]]", //117
    "[[[size=14 color=130,230,255]]]", //118
    " (已擁有)", //119
    "[[[size=14 color=255,249,170]]]", //120
    "|{0}(等級需求：{1})", //121
    " (已有{0}件)", //122
  };

  public const string Str2056_0002="Item error";
  public const string Str2056_0003="Place equip common equipment first【{0}】";
  public const string Str2056_0004="No such item";
  public const string Str2056_0005="Item in use, unable to equip";
  public const string Str2056_0006="Item cannot be equipped at this position";
  public const string Str2056_0007="Equipment currently in use, unable to take off";
  public const string Str2056_0008="Item rental expired, unable to equip";
  public const string Str2056_0009="Receive undefined error";
  public const string Str2056_0010="Equipment item abnormal";
  public const string Str2056_0011="Bag item abnormal";
  public const string Str2056_0012="This item can't be taken off";
  public const string Str2056_0013="Career unmatched, unable to equip!";
  public const string Str2056_0014="LV unqualified, unable to equip!";
  public const string Str2056_0015="Item spoilt, unable to equip";
  public static string[] Str2056_0016=
  {
    "Lack of Bag space!", //0
    "Lack of Bag space, unable to take off equipment",  //1
    "Lack of Bag space, please to do decomposing after there's enough space", //2
    "Lack of Bag space, unable to use this function", //3
    "背包空間不足，無法卸下水晶",  //4
    "背包空間不足，最少需有一格空間",  //5
    "背包空間不足，無法使用！", //6
    "背包空間不足，無法挖掘水晶",  //7
  };
  public const string Str2056_0017="Equipped completed";
  public const string Str2056_0018="Equipment taken off";
//public const string Str2056_0019="Lost{0}";
//public const string Str2056_0020="Lost{0} X {1}";
  public const string Str2056_0021="Got[[[color={0}]]][{1}]";
  public const string Str2056_0022="Got[[[color={0}]]][{1}] X {2}";
  public const string Str2056_0023="Item in use, unable to move";
  public const string Str2056_0024="Item amount insufficient";
  public const string Str2056_0025="That grid has been occupied";
  public const string Str2056_0026="Weight Carriage insufficient";
  public const string Str2056_0027="Exceeded stack-up limit";
  public const string Str2056_0028="Backpack rental expired, unable to put in";
  public static readonly string[] Str2056_0029={"Decompose", "Rearrange","Auction","Mobile Bank","Enlarge Bag"};
  public const string Str2056_0030="Bag";
  public const string Str2056_0031="Purchase Mobile Bank";
  public const string Str2056_0032="Mobile Bank";
  public const string Str2056_0033="Accept";
  public const string Str2056_0034="Cancel";
  public const string Str2056_0035="Purchase";
  public const string Str2056_0036="{0} times for {1} Gold";
  public const string Str2056_0037="[[[align=center]]]Use Mobile Bank[[[.]]]Left amount of charges:{0}times";
  public const string Str2056_0038="[[[align=center]]]Purchase Mobile Bank[[[.]]]Sure to purchase/n{0} times for {1} Gold?";
  public const string Str2056_0039="Purchase succeeded, amount of charges of Mobile Bank increased {0}times ";
  public const string Str2056_0040="Exceeded stack-up limit";
  public const string Str2056_0041="Please enter the amount";
  public const string Str2056_0042="Succeeded in discarding item";
  public const string Str2056_0043="Failed to discard item";
  public const string Str2056_0044="Insufficient amount for the item";
  public const string Str2056_0045="Item in use, unable to discard";
  public const string Str2056_0046="No such item";
  public const string Str2056_0047="[[[align=center]]]{0} X {1}[[[.]]]Discard the item?";
  public const string Str2056_0048="Use{0}";
  public const string Str2056_0049="Rearrange completed";
  public const string Str2056_0050="Rearrange failed";
  public const string Str2056_0051="Enlarge Bag for {0} grids for {1} Gold)";
  public const string Str2056_0052="The amount of charges of Mobile Bank has reached the limit";
  public const string Str2056_0053="The item in under CD, unable to use";
  public const string Str2056_0054="The item expired, unable to use";
  public const string Str2056_0055="HP is full";
  public const string Str2056_0056="MP is full";
  public const string Str2056_0057="Out of daily amount of charge, unable to use";
  public const string Str2056_0058="There is no effect on this item, unable to use";
  public const string Str2056_0059="Common items can't be used";
  public static readonly string[] Str2056_0060={"Ground mount","Air mount","Multi mount"};
  public const string Str2056_0061="Prepareing getting on mount......";
  public const string Str2056_0062="Can't land at current position";
  public const string Str2056_0063="Exchange Card";
  public const string Str2056_0064="Choices Info";
  public const string Str2056_0065="Amount：{0}";
  public const string Str2056_0066="Please choose the item you want to exchange";
  public const string Str2056_0067="Congratulations!【{0}】 got 【{2}】X {3} from 【{1}】";  //"恭喜【玩家ID】從【福袋名稱】中抽中【物品名稱】X【數量】"
  public const string Str2056_0068="Upper limit：{0}";
  public const string Str2056_0069="Please choose the item you want to exchange";
  public const string Str2056_0070="Can't take this mission again today";
  public const string Str2056_0071="You already got this mission";
  public const string Str2056_0072="Exceeded upper limit of mission";
  public const string Str2056_0073="Mission taken：『{0}』";      //"已接任務：『任務名稱』"
  public const string Str2056_0074="The item can't be used";
  public const string Str2056_0075="Sure to take the mission \"{0}\"?";  //"是否承接『任務名稱』任務？"
  public const string Str2056_0076="{0} ╳ {1}";              //"物品名稱X物品數量"
//public const string Str2056_0077="";
  public const string Str_DelMissOK="Delete Mission Success";
  public const string Str_EventClosed="Event has Closed";
#if OPEN_DATA_PIECE
  public static string[] Str2056_0078=
  {
    "Class unqualified, the equipment is for Holy Swordsman!",  //0
    "Class unqualified, the equipment is for Evangel Priest!",  //1
    "Class unqualified, the equipment is for Shadow Mage!", //2
    "Class unqualified, the equipment is for Abyss Assassin!",  //3
    "Class unqualified, the equipment is for Gear Master!", //4
    "Class unqualified, the equipment is for Alchemist!", //5
    "Class unqualified, the equipment is for Holy Archer!", //6
    "Class unqualified, the equipment is for Black Knight!",  //7
    "Class unqualified, the equipment is for Steam Master!",  //8
    "Class unqualified, Heavy Armor is for Holy Swordsman, Gear Master, and Abyss Assassin!", //9
    "Class unqualified, Heavy Armor is for Evangel Priest, Shadow Mage, and Alchemist!",  //10
    "Class unqualified, the equipment is for Holy Swordsman, Evangel Priest, and Holy Archer!", //11
    "Class unqualified, the equipment is for Shadow Mage, Abyss Assassin, and Black Knight!", //12
    "Class unqualified, the equipment is for Gear Master, Alchemist, and Steam Master!",  //13
  };
#else
  public static string[] Str2056_0078=
  {
    "Class unqualified, the equipment is for Holy Swordsman!",  //0
    "Class unqualified, the equipment is for Evangel Priest!",  //1
    "Class unqualified, the equipment is for Shadow Mage!", //2
    "Class unqualified, the equipment is for Abyss Assassin!",  //3
    "Class unqualified, the equipment is for Gear Master!", //4
    "Class unqualified, the equipment is for Alchemist!", //5
    "Class unqualified, the equipment is for Holy Archer!", //6
    "Class unqualified, the equipment is for Black Knight!",  //7
    "Class unqualified, the equipment is for Steam Master!",  //8
    "Class unqualified, Heavy Armor is for Holy Swordsman, Gear Master, and Abyss Assassin!", //9
    "Class unqualified, Heavy Armor is for Evangel Priest, Shadow Mage, and Alchemist!",  //10
    "Class unqualified, the equipment is for Holy Swordsman and Evangel Priest!", //11
    "Class unqualified, the equipment is for Shadow Mage and Abyss Assassin!", //12
    "Class unqualified, the equipment is for Gear Master and Alchemist!",  //13
  };
#endif
  public static string[][] Str2056_0079=
  {
    new string[2] {"吾乃亞特蘭提斯之盾！", "守護王之榮耀！"},
    new string[2] {"為了亞特蘭提斯的子民！", "旋律能讓靈魂得到救贖！"},
    new string[2] {"渾沌之力是最後的解放！", "破壞乃重生之起源！"},
    new string[2] {"在死亡的陰影下出擊！", "來自深淵，征服一切！"},
    new string[2] {"為了偉大的科學！", "齒輪！扳手！機器人！"},
    new string[2] {"下一個實驗令我感到無比興奮！", "為了探究永恆真理而生！"},
  };
//public const string Str2056_0080="",
//public const string Str2056_0081="",
//public const string Str2056_0082="",
//public const string Str2056_0083="",
//public const string Str2056_0084="",
//public const string Str2056_0085="",
//public const string Str2056_0086="",
//public const string Str2056_0087="",
//public const string Str2056_0088="",
  public const string Str2056_0089="Item is currently in use";
  public const string Str2056_0090="Failed to use";
  public const string Str2056_0091="Unable to use this item";
  public const string Str2056_0092="Item is in Cool Down, please try again later";
  public const string Str2056_0093="Using Bag. Unable to do other things";
  public const string Str2056_0094="Close the interface to end decomposing";
  public static string[] Str2056_0095={"[[[align=center]]][[[paraGap=8]]]{0} uses {1} on you, accept?|{2}|%o", "[[[align=right]]][[[paraGap=8]]]seconds"};
  public const string StillPunish = "%f接受友軍復活仍有輕微懲罰";
  public const string Str2056_0096="Riding, unable to do it";
  public const string Str2056_0097="Decompose failed";
  public const string Str2056_0098="The item can't be decomposed";
  public const string Str2056_0099="[[[size=14]]]%w拆解後無法還原，您確認要拆解這件裝備嗎？|[[[color=255,70,70]]](請確認身上銀幣空間是否足夠，建議剩餘空間至少大於五萬，超過上限系統將回收不予以補發)";
//public const string Str2056_0100="";
  public const string Str2056_0101="You are busy, unable to do it";
  public const string Str2056_0102="Equipment decomposed, got {0} Silver";               //"已拆解裝備，獲得【數量】銀幣";
  public const string Str2056_0103="Equipment decomposed, got {0} Silver and {1} ╳ {2}";  //"已拆解裝備，獲得【數量】銀幣以及【道具】x【數量】"
  public const string Str2056_0104="Continual open Fortune Bag：";
  public const string Str2056_0105="Item is occupied, unable to do it";
  public const string Str2056_0106="The item can't be discard";
  public const string Str2056_0107="The item can't be moved";
  public const string Str2056_0108="Unable to rearrange items in Bag";
  public const string Str2056_0109="Rearranging items in Bag, unable to do it";
  public const string Str2056_0110="There's no left amount of charges, unable to use";
  public const string Str2056_0111="Already put in material shelf";
  public const string Str2056_0112="Insufficient Gold";
  public const string Str2056_0113="Plaese choose the stealing target";
  public const string Str2056_0114="Use Divine Artificial successfully";
  public const string Str2056_0115="Use Divine Artificial failed";
  public const string Str2056_0116="Can't steal under good status";
  public const string Str2056_0117="There is nothing to steal";
  public const string Str2056_0118="黑貓靈巧的竊取銀幣";
//public const string Str2056_0119="";
  public const string Str2056_0120="The black cat stole back {0} dexterously";       //"黑貓靈巧的竊走了【物品名稱】
  public const string Str2056_0121="The black cat stole back {0} Silver dexterously";   //"黑貓靈巧的竊走了【銀幣】"
  public const string Str2056_0122="Insufficient Silver or Bag space";
  public const string Str2056_0123="Unable to memorize in current scene";
  public const string Str2056_0124="Memorize successfully";
  public const string Str2056_0125="Memorize failed";
  public const string Str2056_0126="Transfer successfully";
  public const string Str2056_0127="Transfer failed";
  public const string Str2056_0128="Unable to use in current scene";
  public const string Str2056_0129="LV unquilified, unable to transfer";
  public const string Str2056_0130="Tansfers is forbidden at where the target currently is";
  public const string Str2056_0131="Target player can not be transferred in flight";
  public const string Str2056_0132="The plaayer is under certain status that not allowed to be called";
  public const string Str2056_0133="Insufficient items to do transfering";
  public const string Str2056_0134="Unable to transfer in current scene";
  public const string Str2056_0135="The scene that you or the target at is not allowed to transfer";
  public const string Str2056_0136="You are not in a party, please use single transfer";
  public const string Str2056_0137="The place is occupied by other item";
  public const string Str2056_0138="The item is not open";
  public const string Str2056_0139="LV unquilified, unable to use";
  public const string Str2056_0140="Unable to use this item in current scene";
  public const string Str2056_0141="The item is repaired, and durability resumes to max";
  public const string Str2056_0142="The durability of this item increased";
  public const string Str2056_0143="The item can't be used";
  public const string Str2056_0144="The item doesn't exist";
  public const string Str2056_0145="Use item failed";
  public const string Str2056_0146="Got Gold {0}";
  public const string Str2056_0147="Membership unquilified";
  public const string Str2056_0148="You are at spawn point";
  public const string Str2056_0149="Space Jumper";
  public const string Str2056_0150="Memory Stone";
  public const string Str2056_0151="Space Key";
  public const string Str2056_0152="Please choose a memory or the one you want to replace：";
  public const string Str2056_0153="{0}.";
  public const string Str2056_0154="You have not memorized";
  public const string Str2056_0155="Memorize";
  public const string Str2056_0156="Edit Note";
  public const string Str2056_0157="Transfer";
  public const string Str2056_0158="Please input the note";
  public const string Str2056_0159="Transfer to {0}, and need one {1}, are you sure to transfer?";
  public const string Str2056_0160="Jump Mode";
  public const string Str2056_0161="Single";
  public const string Str2056_0162="Party";
  public const string Str2056_0163="Target Player";
  public const string Str2056_0164="Player";
  public const string Str2056_0165="Friend";
  public const string Str2056_0166="Please input the player's name";
  public const string Str2056_0167="Party transfer need to use {0} Space Jumpers, are sure to transfer?[[[. .]]]※The party member might refuse to transfer or unable to transfer to the scene";
  public const string Str2056_0168="[[[align=cneter]]]{0} invite you to transfer, do you agree?";
  public const string Str2056_0169="{0}({1},{2})";  //地圖名稱(X座標,Y座標)
  public const string Str2056_0170="Got reward Gold {0}";
  public const string Str2056_0171="Unable to memorize under battle";
  public const string Str2056_0172="Set note successfully";
  public const string Str2056_0173="Set note failed";
  public const string Str2056_0174="The No. is not memorized";
  public const string Str2056_0175="請先脫下造型武器";
  public static readonly string[] Str2056_0176={"Main City","Level of Map","Instance Enterance","Boss Raid Enterance"};
  public const string Str2056_0177="Please choose the area you want to transfer";
  public const string Str2056_0178="Unable to transfer under battle";
  public const string Str2056_0179="Insufficient item amount, unable to transfer";
  public const string Str2056_0180="LV unquilified, unablt to transfer";
  public const string Str2056_0181="The player doesn't exist or is currentlt offline";
  public const string Str2056_0182="The player refused you to transfer to him/her";
  public const string Str2056_0183="[[[align=center]]]{0} want to transfer to you, do you accept?";
  public const string Str2056_0184="The target can't be yourself!";
  public const string Str2056_0185="副本中，無法使用此功能";
  public const string Str2056_0186="戰鬥中，無法使用此物品";
  public const string Str2056_0187="已達上限，無法再進行擴充";
  public const string Str2056_0188="記憶位置已達上限，無法再進行擴充";
  public const string Str2056_0189="空間已增加";
  public const string Str2056_0190="記憶石已增加5個可記憶位置";
  public const string Str2056_0191="物品條件不符";
  public const string Str2056_0192="已擁有此造型";
  public const string Str2056_0193="連續上線獎勵";
  public const string Str2056_0194="今日上線獎勵已領取";
  public const string Str2056_0195="領取上線獎勵";
  public const string Str2056_0196="金幣遊樂場";
  public const string Str2056_0197="此物品無法兌換造型";
  public const string Str2056_0198="[[[size=14 color=255,255,255]]]已連續上線：第[[[color=255,204,0]]]{0}[[[color=255,255,255]]]天";
  public const string Str2056_0199="[[[size=14 color=255,255,255]]]獲得銀幣：[[[color=255,204,0]]]{0}";
  public const string Str2056_0200="[[[size=14 color=255,204,0]]]恭喜你，可進入金幣遊樂場";
  public const string Str2056_0201="副本中，請先結束副本，請至提示櫃開啟此介面進入金幣遊樂場";
  public const string Str2056_0202="物品拍賣中";
  public const string Str2056_0203="目前場景無法騎乘";
  public const string Str2056_0204="造型無變更";
//public const string Str2056_0205="座騎施打縮小劑後變小了！";
  public const string Str2056_0206="此座騎無法再縮得更小了！";
//public const string Str2056_0207="座騎施打放大劑後變大了！";
  public const string Str2056_0208="此座騎無法再變得更大了！";
  public const string Str2056_0209="{0}不足"; //【物品】不足
  public const string Str2056_0210="藥劑施打失效";
  public const string Str2056_0211="煉金釜";
  public const string Str2056_0212="請置入要煉金的物品以及材料";
  public const string Str2056_0213="煉金";
  public const string Str2056_0214="材料不足";
  public const string Str2056_0215="煉金成功";
  public const string Str2056_0216="煉金失敗";
  public const string Str2056_0217="計時狀態中，請稍候";
  public const string Str2056_0218="資料傳輸中，請稍候";
  public const string Str2056_0219="回城傳送準備中";
  public const string Str2056_0220="傳送中斷";
  public const string Str2056_0221="目前角色狀態無法傳送";
  public const string Str2056_0222="權力點已滿";
  public const string Str2056_0223="能量已滿";
  public const string Str2056_0224="目前背包狀態無法進行其他操作";
  public const string Str2056_0225="已向對方發出傳送詢問";
  public const string Str2056_0226="開福袋中，請稍候";
  public const string Str2056_0227="背包物品已重整";
  public const string Str2056_0228="職業選擇";
  public const string Str2056_0229="選擇你的職業星盤";
  public static readonly string[] Str2056_0230 =
  {
    "聖劍士", "福音祭司", "幽影法師", "深淵刺客", "齒輪大師", "煉金術士", "聖弓手", "黑武士", "蒸氣神兵"
  };
  public static readonly string[] Str2056_0231 =
  {
    "~守護亞特國度的不屈盾牌~", "~撫慰人心的光之歌頌者~", "~操弄強大力量的魔法智者~", "~隱蔽於暗影的渾沌殺手~", "~喜愛機械的爆破藝術家~", "~生命煉金術的狂熱研究者~"
  };
  public static readonly string[] Str2056_0232 =
  {
    "%y光明殿堂%w所培育出來的精英戰士們，大多選擇踏上%m聖劍士%w這條神聖的道路。自古以來，他們肩負守衛亞特蘭提斯的重責大任，在破壞神甦醒、魔物肆虐的今日，更顯其重要性。在戰鬥中，聖劍士所持的盾牌有如堅不可摧的高牆，總是站在最前線%o守護隊伍%w中的每一個成員。",
    "%y光明殿堂%w內繼承光行者意志的人們，被授予祭司的神聖職責，隱藏在殿堂深處與世俗隔離；然而在經過千年的洗禮之後，擅長吟唱聖歌的%m福音祭司%w逐漸成為人們心靈的砥柱。在戰場上他們柔聲輕唱，將光明的力量化為動人的音樂，不但能%o治癒友軍%w的傷口，更能刺穿邪惡的耳膜。",
    "%l渾沌議會%w中極為渴求力量的人們，能將許多古老典籍中的知識秘法，化為一個個致命的法術，而神祕多變的%m幽影法師%w便是其名號。遇上戰鬥時，各種不同元素的%o魔法屬性攻擊%w從幽影法師手底釋放而出，可發揮驚人的威力以及各種不可思議的效果，使敵人在近身之前就倒地不起。",
    "%l渾沌議會%w裡難以察覺，卻又不得不提防其存在的勢力，也就是令人聞風喪膽的%m深淵刺客%w們，其一生追求的是自身極致的渾沌力量。進入戰鬥之時，刺客將過人的速度體現在%o隱匿暗殺%w之上，敵人常常在一個眨眼之間就丟失了刺客的蹤跡，或者遭到從背後而來的匕首刺穿。",
    "%g科學基地%w中有著許多對機械狂熱的極端份子，他們總是自稱為%m齒輪大師%w；這些人常常%o召喚機械公仔%w彼此炫耀較勁。進入戰鬥時，齒輪大師喜歡運用各種科技產品來消滅敵人，更能夠活用機械扳手來進行修復及輔助的工作，他與公仔間的搭配就彷彿是一個小團隊般，無懈可擊。",
    "%g科學基地%w內眾人熟知的%m煉金術士%w，身兼生命體以及煉金術研究兩大專長；這群手持雙槍的學者們為了捕抓動植物進行研究，培養出了一副媲美專業獵人的好身手。至於戰鬥，煉金術士可以憑藉的豐富藥學知識來進行各種%o狀態輔助%w，不論是對友軍治療，抑或是對敵人下毒。",
  };
  public static readonly string[] Str2056_0233 =
  {
    "堅甲職業", "輕甲職業", "輕甲職業", "堅甲職業", "堅甲職業", "輕甲職業"
  };
  public static readonly string[] Str2056_0234 =
  {
    "對敵進行強烈的懲戒打擊", "以美妙歌聲療癒傷口", "各式魔法元素攻擊", "防不勝防的隱匿暗殺", "召喚機器公仔助陣", "遠距襲擊的槍彈攻勢"
  };
  public static readonly string[] Str2056_0235 =
  {
    "以盾牌格擋抵禦傷害", "施展光明之盾守護隊友", "變幻法術進行牽制或輔助", "疾速的移動與殘影分身", "各式爆破科技運用", "奇特的煉金法術與藥劑"
  };
  public const string Str2056_0236="目前職業已為{0}";
  public const string Str2056_0237="請選擇職業";
  public const string Str2056_0238="成功轉職為{0}";
  public const string Str2056_0239="欲煉金物品無法轉換";
  public const string Str2056_0240="找不到符合條件的煉金物品";
  public const string Str2056_0241="%w角色星盤上所有%l星石與技能效果%w將於轉換為新職業時%r全部重置歸零%w，是否確定要轉換職業？";
  public const string Str2056_0242="水晶重置儀式";
  public const string Str2056_0243="請放置欲重置的水晶";
  public const string Str2056_0244="※請雙擊或直接拖曳欲重置的水晶或鑲嵌有水晶的裝備至左方欄位";
  public const string Str2056_0245="啟動";
  public const string Str2056_0246="再次啟動";
  public const string Str2056_0247="保留此效果";
  public const string Str2056_0248="原效果";
  public const string Str2056_0249="新效果";
  public const string Str2056_0250="是否花費銀幣{0}來進行水晶效果重置？";
  public const string Str2056_0251="銀幣不足";
//public const string Str2056_0252="";
  public const string Str2056_0253="水晶靈魂已滿，可進行鑲嵌";
  public const string Str2056_0254="水晶鑲嵌成功";
  public const string Str2056_0255="水晶鑲嵌失敗";
  public const string Str2056_0256="水晶尚未覺醒";
  public const string Str2056_0257="水晶裝備成功";
  public const string Str2056_0258="水晶已卸下";
  public const string Str2056_0259="請先卸下水晶";
  public const string Str2056_0260="卸除水晶";
  public static string[] Str2056_0261=
  {
    "[[[size=15 color=240,80,80]]]不可轉移[[[size=18 color=00FFFFFF]]] ",               //0:Limit 2
    "[[[size=15 color=240,80,80]]]不可拍賣/寄送[[[size=18 color=00FFFFFF]]] ",          //1:Limit 3
    "[[[size=15 color=240,80,80]]]不可刪除/轉移[[[size=18 color=00FFFFFF]]] ",          //2:Limit 4
    "[[[size=15 color=240,80,80]]]不可賣商店/寄送[[[size=18 color=00FFFFFF]]] ",         //3:Limit 5
    "[[[size=15 color=240,80,80]]]不可賣商店[[[size=18 color=00FFFFFF]]] ",              //4:Limit 6
    "[[[size=15 color=240,80,80]]]體驗中[[[size=18 color=00FFFFFF]]] ",                 //5:Limit 7
    "[[[size=15 color=240,80,80]]]體驗中/不可賣商店/拍賣/寄送[[[size=18 color=00FFFFFF]]] ",//6:Limit 8
    "[[[size=15 color=240,80,80]]]不可賣商店/拍賣[[[size=18 color=00FFFFFF]]] ",        //7:Limit 9
    "[[[size=15 color=240,80,80]]]不可拍賣[[[size=18 color=00FFFFFF]]] ",               //8:Limit 10
    "[[[size=15 color=240,80,80]]]不可賣商店[[[size=18 color=00FFFFFF]]] ",              //9:Limit 11
    "[[[size=15 color=240,80,80]]]不可賣商店/拍賣/寄送[[[size=18 color=00FFFFFF]]] ",   //10:Limit 12
    "[[[size=15 color=240,80,80]]]不可拍賣/寄送[[[size=18 color=00FFFFFF]]] ",          //11:Limit 13
    "[[[size=15 color=240,80,80]]]體驗中/不可轉移[[[size=18 color=00FFFFFF]]] ",         //12:Limit 14
  };
  public static string[] Str2056_0262=
  {
    "[[[size=15 align=center color=255,204,0]]]{0}",
    "<雙擊左鍵使用>", //1
    "", //2
    "<雙擊左鍵使用>", //3
    "<單擊右鍵自動使用>", //4
    "<雙擊左鍵合成>", //5
    "<單擊左鍵預覽>", //6
    "<雙擊左鍵置入材料櫃>",  //7
  };
  public const string Str2056_0264="水晶挖掘成功";
  public const string Str2056_0265="水晶挖掘失敗";
  public const string Str2056_0266="[[[color=213,87,74 align=center]]]獲得物品："; // fs 12/08/15 獲得物品的圖示訊息文字
  public const string Str2056_0267="水晶效果已重置";
  public const string Str2056_0268="水晶效果已變更";
  public const string Str2056_0269="找不到可使用的物品";
  public const string Str2056_0270="此物品無法置入煉金釜";
  public const string Str2056_0271="此物品無法置入";
  public const string Str2056_0272="獲得物品數量錯誤";
  public const string Str2056_0273="職業不符，無法獲得物品";
  public const string Str2056_0274="角色尚未選擇職業";
  public const string Str2056_0275="黑爵卡";
  public const string Str2056_0276="使用";
  public static string[] Str2056_0277=
  {
    "黃金之眼",
    "時空之鑰",
    "召喚之門",
    "友情之鏈",
  };
  public static string[] Str2056_0278=
  {
    "[[[size=15 color=200,185,165]]]使用黃金之眼後，在購物中心及魔法衣櫥內購買商品皆享九折優惠",
    "[[[size=15 color=200,185,165]]]使用後可（無限次數）傳送至主城、及各地圖",
    "[[[size=15 color=200,185,165]]]使用後可進入秘境探險，可獲得特殊的寶物（每日一次）",
    "[[[size=15 color=200,185,165]]]使用後可呼叫複數好友到身邊，最多10名好友（每日三次）",
  };
  public const string Str2056_0279="說明";
  public const string Str2056_0280="此功能無法使用";
  public const string Str2056_0281="友情鏈鎖";
  public const string Str2056_0282="剩餘招喚次數 {0}/{1}";
  public const string Str2056_0283="今日使用次數已滿";
  public const string Str2056_0284="[[[align=center paraGap=8 color=B2EC0A]]]『{0}』|%w[[[size=15]]]對您進行招喚，是否接受？|[[[color=FF0000]]]";
  public const string Str2056_0285="秒%w後自動拒絕";
  public const string Str2056_0286="黑爵卡購物折扣已結束";
  public const string Str2056_0287="目前場景無法使用此功能";
  public const string Str2056_0288="目前狀況無法使用此功能";
  public const string Str2056_0289="殘頁存放區擴充成功";
  public const string Str2056_0290="您沒有邀請好友";
  public const string Str2056_0291="超過邀請人數限制";
  public const string Str2056_0292="您沒有招喚好友";
  public const string Str2056_0293="已送出邀請";
  public const string Str2056_0294="黃金之眼已使用";
  public const string Str2056_0295="黃金之眼已停止使用";
  public const string Str2056_0296="[[[size=15 lineGap=2 color=245,160,35]]]使用說明：|%w可以置入物品及材料，煉出其它物品。|[[[color=245,160,35]]]材料：|%w獨角獸之角的煉成率為100%。|元素怪粉塵有機率煉出原始物品。";
  public const string Str2056_0297="殘頁存放區";
  public static string[] Str2056_0298={"重整", "魔法故事書", "殘頁存放區擴充", "魔法故事書(H)"};
  public static string[] Str2056_0299={"故事書抄寫", "開始抄寫", "請放入欲抄寫的魔法故事書"};
  public const string Str2056_0300="您沒有放入魔法故事書";
  public const string Str2056_0301="您沒有手抄紙";
  public const string Str2056_0302="此魔法故事書無法抄寫";
  public const string Str2056_0303="此手抄紙無法使用";
  public const string Str2056_0304="物品已更動或不存在，請重新再試";
  public const string Str2056_0305="[[[align=center color=FFFFFF]]]是否確認抄寫【{0}】？|[[[color=FFFF00]]](需扣除手抄紙*1)";
  public const string Str2056_0306="抄寫完成！獲得【{0}】X {1}";
  public const string Str2056_0307="抄寫爆發！恭喜獲得【{0}】X {1}";
//public const string Str2056_0308="";
  public static string[] Str2056_0309=
  {
    "單載陸行-{0}",              //0
    "單載陸行-{0}({1}天)",       //1
    "單載飛行-{0}",              //2
    "單載飛行-{0}({1}天)",       //3
    "單載陸空-{0}",              //4
    "單載陸空-{0}({1}天)",       //5
    "雙載陸行-{0}",              //6
    "雙載陸行-{0}({1}天)",       //7
    "雙載飛行-{0}",              //8
    "雙載飛行-{0}({1}天)",       //9
    "雙載陸空-{0}",              //10
    "雙載陸空-{0}({1}天)",       //11
    "多載陸行-{0}",              //12
    "多載陸行-{0}({1}天)",       //13
    "多載飛行-{0}",              //14
    "多載飛行-{0}({1}天)",       //15
    "多載陸空-{0}",              //16
    "多載陸空-{0}({1}天)",       //17
    "大師工具-{0}",              //18
    "大師材料-{0}",              //19
    "怪物故事書-{0}",            //20
    "裝備故事書-{0}",            //21
    "技能故事書-{0}",            //22
    "{0}(藍)",                  //23.裝備名稱(顏色)
    "{0}(紫)",                  //24.裝備名稱(顏色)
    "{0}(金)",                  //25.裝備名稱(顏色)
    "體驗座騎-{0}({1}天)",       //26
  };
  public const string Str2056_0310="目前背包狀態無法使用神器";
  public const string Str2056_0311="物品已鎖定，無法使用神器";
  public const string Str2056_0312="請先進入騎士團再使用！";
  public const string Str2056_0313="請先進入文明之塔再使用！";
  public const string Str2056_0314="擦拭神器後神器變得亮晶晶，取得神器經驗值！";
  public const string Str2056_0315="[[[color=245,160,35]]]單擊一下%w擦拭布，進行擦拭神器。|擦拭神器可以[[[color=245,160,35]]]提升神器的經驗值%w，|並且有機率獲得其他驚喜！";
  public static string[] Str2056_0316=
  {
    "[[[size=22 color=255,245,83]]]{0}|[[[size=16]]]%w{1}|{2}|{3}|{4}|[[[color=170,225,55]]]{5}{6}{7}",   //0
    "神器等級：{0}", //1
    "最高等級：{0}", //2
    "累積經驗：{0}|升級經驗：{1}", //3
    "累積經驗：{0}|升級經驗：已達最高等級",  //4
    "", //5
    "冷卻倒數：{0:0}分", //6
    "冷卻倒數：{0:0.00}秒", //7
    "||[[[color=145,135,130]]]提升至{0}級後效果|{1}",  //8
    "||[[[color=255,45,45]]]＊擦拭神器可取得[[[color=255,240,0]]]神器經驗值", //9
    "|[[[color=255,45,45]]]＊神器冷卻時間為{0}分，每次玩家等級提升，冷卻時間會重置1次", //10
    "|[[[color=255,45,45]]]＊神器冷卻時間為{0}分，每次玩家建造樓層，冷卻時間會重置1次", //11.槌子
    "|[[[color=255,45,45]]]＊[[[color=255,240,0]]]目前等級[[[color=255,45,45]]]達上限後，可透過冒險地圖的古老神喻之石，提升神器[[[color=255,240,0]]]最高等級[[[color=255,45,45]]](擦拭神器可繼續升級)", //12
  };
  public const string Str2056_0317="目前背包狀態無法開啟殘頁存放區";
  public const string Str2056_0318="此物品無法發動技能";
  public const string Str2056_0319="您尚未選擇職業，無法使用此物品";
  public const string Str2056_0320="確認魔化「{0}」？";
  public const string Str2056_0321="您沒有指定「裝備魔法故事書」";
  public const string Str2056_0322="您指定的「裝備魔法故事書」無法使用";
  public const string Str2056_0323="您選擇的物品無法進行魔化";
  public const string Str2056_0324="恭喜！裝備魔化成功";
  public const string Str2056_0325="魔化失敗";
  public const string Str2056_0326="此物品無法放入殘頁存放區";
  public const string Str2056_0327="指定目標位置已有物品";
  public const string Str2056_0328="殘頁存放空間已滿";
  public const string Str2056_0329="此物品無法操作";
  public const string Str2056_0330="殘頁已重整";
  public const string Str2056_0331="超過重整等待時間，停止重整";
  public const string Str2056_0332="殘頁重整中，請稍候";
  public const string Str2056_0333="物品資料不正確";
  public const string Str2056_0334="已擁有此表情符號";
  public const string Str2056_0335="獲得「{0}」表情符號";
  public const string Str2056_0336="條件不符，無法擴充";
  public const string Str2056_0337="[[[align=center lineGap=4 color=255,255,255]]]是否永久擴充殘頁存放區{0}格？|[[[color=255,255,0]]](需扣除{1}金幣)";
  public const string Str2056_0338="進場景準備中，目前角色狀態無法進行騎乘";
  public static string[] Str2056_0339=
  {
    "怪物篇", "裝備篇", "職業篇"
  };
  public static string[] Str2056_0340=
  {
    "創世之章", //1
    "黎明平原之章", //2
    "聖靈之山之章", //3
    "繁星沙漠之章", //4
    "星月山谷之章", //5
    "銳風之峽之章", //6
    "微光森林之章", //7
    "鹽湖之章",     //8
    "石灰洞穴之章", //9
    "舊王城之章", //10
    "無底沼澤之章", //11
    "失落之島之章", //12
    "流沙洞穴之章", //13
    "巨人遺跡之章", //14
    "(噩)黎明平原之章",  //15
    "(噩)聖靈之山之章",  //16
    "(噩)繁星沙漠之章",  //17
    "(噩)星月山谷之章",  //18
    "(噩)銳風之峽之章",  //19
    "(噩)微光森林之章",  //20
    "(噩)鹽湖之章",      //21
    "(噩)石灰洞穴之章",  //22
    "(噩)舊王城之章",   //23
    "(噩)失落之島之章",  //24
    "(獄)黎明平原之章",  //25
    "(獄)聖靈之山之章",  //26
    "(獄)繁星沙漠之章",  //27
    "(獄)星月山谷之章",  //28
    "(獄)銳風之峽之章",  //29
    "(獄)微光森林之章",  //30
    "(獄)鹽湖之章",    //31
    "(獄)石灰洞穴之章",  //32
    "(獄)舊王城之章 ",    //33
    "(獄)失落之島之章",  //34
  };
  public static string[] Str2056_0341=
  {
    "一般探索進度", "噩夢探索進度", "地獄探索進度"
  };
  public static string[] Str2056_0342=
  {
    "故事書功能", //0
    "此故事書已使用次數", //1
    "故事書適用裝備", //2
    "技能說明", //3
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]{0}",  //4
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]{0}",  //5
    "[[[size=15 color=35,27,16 outlineColor=00000000]]]冷卻：{0:0.00}秒|距離：{1:0}|{2}", //6
    "[[[size=20 color=0,0,0 outlineColor=00000000]]]~謎樣的章節~|[[[size=15]]]   尚未解開魔法文字枷鎖|",  //7
    "[[[size=15 align=right paraGap=8 lineGap=2 color=0,0,255 outlineColor=00000000 underline=0,0,255]]]初次裝訂此故事書可解開魔法文字枷鎖|[[[underline=0,0,255]]]累積解鎖數量可獲得探索效果",  //8
    "集滿殘頁可點擊裝訂成故事書", //9
    "裝訂", //10
    "對應殘頁不足！", //11
    "尚未解開章節地圖", //12
    "故事探索進度", //13
    "現有星星數：{0}", //14
    "現有探索效果", //15
    "條件", //16
    "效果", //17
    "星星數：{0:#0}", //18
    "恭喜開啟「{0}」故事", //19
    "獲得【{0}】", //20
    "通過前張地圖副本／首領戰關卡即可解鎖", //21
    "無",  //22
    "[[[align=center color=FFFFFFFF]]]是否花費銀幣提前開放「[[[color=FFFFFF00]]]{0}[[[color=FFFFFFFF]]]」探索章節？|[[[color=FFFFFF00]]](需扣除{1:#,#}銀幣)",  //23
    "此章節已解鎖", //24
    "成功解開故事章節", //25
    "尚未開放探索", //26
    "此故事書已使用次數({0})", //27
    "此故事書已使用次數({0}/{1})", //28
  };
  public const string Str2056_0343="未達使用次數，無法使用";
  public const string Str2056_0344="使用次數已滿";
  public const string Str2056_0345="目前狀況無法進行物品刪除";
  public const string Str2056_0346="目前狀況無法進行物品搬移";
  public const string Str2056_0347="跳躍狀態無法進行騎乘";
  public const string Str2056_0348="強化";
  public const string Str2056_0349="物品已鎖住";
  public const string Str2056_0350="此物品狀態無法進行操作";
  public const string Str2056_0351="座騎騎乘中，無法使用";
  public const string Str2056_0352="美體美容進行中，無法進行騎乘";
  public const string Str2056_0353="[[[color=245,160,35]]]可以置入材料：|%w獨角獸之角|元素怪粉塵";
  public const string Str2056_0354="染髮進行中，無法進行騎乘";
  public const string Str2056_0355="玩家飛行狀態無法傳送";
  public const string Str2056_0356="飛行狀態中，無法使用";
  public const string Str2056_0357="尚未突破地圖關卡，無法前往此區域";
  public const string Str2056_0358="補充天數超過目前所需，超過部分將無法保留，確定要使用嗎？";
  public const string Str2056_0359="%w是否花費{0}金幣來進行[[[color=255,255,0]]]抗性數值重置？";
  public const string Str2056_0360="%w是否花費{0}金幣來進行[[[color=255,255,0]]]隨機效果重置？";
  public const string Str2056_0361="成功延長座騎剩餘使用時間";
  public const string Str2056_0362="使用永生藥水失敗！";
  public const string Str2056_0363="任務已滿，請刪除或解完任務後再進行";
//public const string Str2056_0364="";
  public const string Str2056_0365="堆疊數量不足，無法合成！";
  public const string Str2056_0366="鑰匙不足，無法開啟";
  public const string Str2056_0367="背包物品異常（位置={0}），無法重整";
  public const string Str2056_0368="點擊可連結查看現有探索效果";
  public const string Str2056_0369="神器不存在！";
  public const string Str2056_0370="無法再取得更多神器經驗";
  public const string Str2056_0371="已擁有此標記";
  public const string Str2056_0372="飛行狀態無法更換騎乘";
  public const string Str2056_0373="落地中，無法使用物品";
  public const string Str2056_0374="兌換銀幣中，請稍候";
  public const string Str2056_0375="物品使用失敗，停止兌換銀幣";
  public const string Str2056_0376="物品使用失敗，停止開啟福袋";
  public const string Str2056_0377="銀幣已達上限，無法使用";
  public const string Str2056_0378="獎勵金幣已達上限";
  public const string Str2056_0379="角色需達VIP2星，方可開啟此功能";
  public const string Str2056_0380="已達需求數量";
  public const string Str2056_0381="購買";
  public const string Str2056_0382="獲得神器經驗 {0}";
  public const string Str2056_0383="使用黑貓陶罐召喚出黑貓偷竊財物";
  public const string Str2056_0384="購買成功！存入殘頁存放區！";
  public const string Str2056_0385="您已經加冕過此頭銜！";
  public const string Str2056_0386="稱號資料不正確";
  public const string Str2056_0387="超出購買上限";
  public const string Str2056_0388="騎乘中，無法使用此物品";
  public const string Str2056_0389="此場景無法使用傳送";
  public const string Str2056_0390="欲兌換的銀幣已超過銀幣上限";
  public const string Str2056_0391="此場景無法使用隨身銀行";
  public const string Str2056_0392="請先離開障礙點，再進入金幣遊樂場";
  public const string Str2056_0393="戰鬥狀態無法進行騎乘";
  public const string Str2056_0394="轉換成功！";
  public const string Str2056_0395="請先卸除水晶才可進行拆解";
  public const string Str2056_0396="您已經死了，無法開啟背包";
  public const string Str2056_0397="落地中，無法使用隨身銀行";
  public const string Str2056_0398="權力點已滿，無法使用";
  public const string Str2056_0399="身上銀幣已達上限";
  public const string Str2056_0400="目標玩家所在場景不允許傳送";
  public const string Str2056_0401="【{0}-{1}級】{2}";
  public const string Str2056_0402="進場景中，無法進行騎乘";
  public const string Str2056_0403="與原有髮色相同，請重新設定";
  public const string Str2056_0404="與原有造型相同，請重新設定";
  public const string Str2056_0405="空水晶無法進行吸取";
  public const string Str2056_0406="[[[size=14]]]%w是否要對「[[[color={0}]]]{1}」%w進行靈魂吸取？||[[[color=255,70,70]]]備註：水晶將被銷毀！";
  public const string Str2056_0407="需先裝備水晶";
  public const string Str2056_0408="容器沒有靈魂值";
  public const string Str2056_0409="水晶靈魂已滿";
  public const string Str2056_0410="水晶已成功吸取{0}靈魂值";
  public const string Str2056_0411="目前狀況無法進行騎乘";
  public const string Str2056_0412="價格異常";
  public const string Str2056_0413="職業不符，無法使用物品";
  public const string Str2056_0414="角色需達{0}級方可開啟此功能";
  public const string Str2056_0415="找不到技能星石資料";
  public const string Str2056_0416="此物品無法喚醒星盤的星石";
  public const string Str2056_0417="職業不符，無法喚醒星盤的星石";
  public const string Str2056_0418="您已經喚醒過此星！";
  public const string Str2056_0419="[[[size=14]]]補充靈魂值超過目前所需，超過部份將無法保留，確定要使用嗎？";
  public const string Str2056_0420="目前場景無法使用神器";
  public const string Str2056_0421="點擊此按鈕更換";
  public const string Str2056_0422="事件進行中，無法使用傳送功能";
  public const string Str2056_0423="蒸汽齒輪啟動中，無法使用此功能";
  public const string Str2056_0424="充能膠囊已設定，請先取消再進行設定";
  public const string Str2056_0425="增幅膠囊已設定，請先取消再進行設定";
  public const string Str2056_0426="成功設定充能膠囊";
  public const string Str2056_0427="成功設定增幅膠囊";
  public const string Str2056_0428="取消使用充能膠囊";
  public const string Str2056_0429="取消使用增幅膠囊";
  public const string Str2056_0430="扣除失敗，取消使用充能膠囊";
  public const string Str2056_0431="扣除失敗，取消使用增幅膠囊";
  public const string Str2056_0432="強度等級";
  public const string Str2056_0433="擴充次數已達上限";
  public const string Str2056_0434="角色需達VIP{0}星，方可進行擴充";
  public const string Str2056_0435="角色需達VIP{0}星，方可使用隨身銀行";
  public const string Str2056_0436="兌換物品中，請稍候";
  public const string Str2056_0437="請輸入兌換數量：";
  public const string Str2056_0438="物品使用失敗，停止兌換";
  public const string Str2056_0439="兌換失敗";
  public const string Str2056_0440="指定兌換物異常，無法兌換";
  public const string Str2056_0441="找不到符合條件物品";
  public const string Str2056_0442="兌換超過等待時間，停止兌換";
  public const string Str2056_0443="兌換完成";
  public const string Str2056_0444="拜訪的民眾已達上限，無法使用邀請函";
  public const string Str2056_0445="已超過命運籌碼上限";
  public static string[] Str2056_0446=
  {
    "神奇輪轉石",  //0
    "轉換",   //1
    "目前神奇輪轉石數量：", //2
    "{0} 個", //3
    "欲轉換的裝備：", //4
    "← 請置入裝備", // 5
    "請選擇欲轉換的屬性：", //6
    "請選擇欲轉換的屬性", //7
    "無法轉換多個屬性", //8
    "您沒有指定「神奇輪轉石」", //9
    "神奇輪轉石不足", //10
    "欲轉換物品無法轉換", //11
    "使用神奇輪轉石成功轉換星石效果！", //12
  };
#endregion Str2056

#region ITEM_MSG
  public static string[] ITEM_MSG001=
  {
    "使用物品錯誤代碼：{0}", //
    "無此物品", //1
    "物品數量不足", //2
    "物品使用中", //3
    "物品使用失敗", //4
    "無法使用此物品", //5
    "冷卻時間不足，請稍後再試", //6
    "無此角色或角色不在線上", //7
    "身上物品空間不足,無法使用",  //8
    "已超出身上最大技能點數上限", //9
    "注意!你開寶箱獲得的寶物太貴重,而你負重不足,所以東西掉在地上", //10
    "未開啟銀行無法使用", //11
    "注意!你開福袋獲得的寶物太貴重,而你負重不足,所以東西掉在地上", //12
    "已達累計上限", //13
    "金錢不足", //14
    "金錢上限不足", //15
    "身上負重不足", //16
    "寵物數量已滿", //17
    "身上租賃背包數量已滿", //18
    "銀行格數已達上限", //19
    "金幣不足", //20
    "達英雄帖上限", //21
    "沒有幫會", //22
    "身上已有效果", //23
    "須先設置重生點才可使用", //24
    "隊友不同場景不能使用", //25
    "物品狀態不符", //26
    "物品類型不符", //27
    "物品條件不足", //28
    "效果累計時間已滿", //29
    "鑰匙錯誤，開啟寶箱失敗", //30
    "", //31
    "", //32
    "沒有伴侶，不能使用", //33
    "伴侶不在線上", //34
    "伴侶在特殊場景內，無法傳送", //35
    "今日使用副本重置丹已達上限", //36
    "戰意已滿", //37
    "狀態已滿無法增加", //38
    "狀態數量", //39
    "線上時間4小時才能再用", //40
    "物品條件不符", //41    //虛寶擴充(73)
    "造型無變更", //42    //虛寶的體型置換(75)
    "奇緣題庫已擴充", //43
    "場景招喚數量已達上限", //44
    "伺服器寵物滿載,請稍後再試", //45
    "經驗值已達上限，無法使用物品", //46
    "銀幣已達上限,無法使用", //47
    "職業不符,無法使用", //48
    "已擁有此造型", //49    //虛寶造型兌換卡(77)
    "超過不可交易金幣上限", //50
  };
#endregion ITEM_MSG

#region Unlink use
  public const string UnStr_0002 = "GROUPS";
  public static readonly string[] UnStr_0301 = new string[] {"This role do not exist or is offline", "The friend list is out of bound",  "The friend list of invited role is out of bound",
		"Invited role disagree the invitation",  "Invited role is busy for asking",  "The role invited you is offline",  "Respond role did not invite you",   "This role is not you friend, can't remove",  "Fail to set group name",  "Fail to set friend group",  "GM can't be your friend", "Scene of target player is not allow trace portal",
		"Target player is offline", "Not enough golden leaves", "Calling is not allowing in this scene", "Calling is not allowing for the current state of player", "Busy, can't add friend", "Temparary friend functionalty switching successful",  "Your friend refused your request for portaling to his position", "Your friend si busy, can't send portal request", "Level is not enough to portal", "Fail to recieve message"};
  public static string[] UnStr_0302 = new string[3]{"[[[align=center]]][[[paraGap=4]]] [[[color=B2EC0A]]]『", "』%w|Ask for being friend|%o", "s %wto close dialog"};
  public const string UnStr_0303 = "Send friend invitation to {0}";
  public static string[] UnStr_0304 = new string[2]{"Successfull to add ","as a friend"};
  public static string[] UnStr_0305 = new string[2]{"Successfull to remove",""};
  public const string UnStr_0306 = "Add to waiting for invite section";
  public const string UnStr_0307 = "Invite section is empty, please add some invitations.";
  public static string[] UnStr_0308 = new string[3]{"Had send friend invitations to", "... ", ""};
  public static string[] UnStr_0309 = new string[5]{"No similar level player", "No similar living profession player", "Friends are not on the line or not on the same shunt", "No facebook friends", "Had invited all suggested players"};
  public const string UnStr_0310 = "Friend {0} online";
  public const string UnStr_0311 = "{0} friends is online\n{1} friends is offline";
  public static string[] UnStr_0312 = new string[3]{"亞特蘭提斯", "我在亞特蘭提斯的{0}伺服器，希望你前來與我一起遊玩", "RoleID={0},RoleName={1},ServerName={2}"};
  public static string[] UnStr_1001 = new string[2]{"Friends","Instance of the list"};
  public static string[] UnStr_1002 = new string[4]{"LV","Fame","FB","全部選取/取消"};
  public static string[] UnStr_1003 = new string[3]{"Send invitation","Return", "Call"};
  public static string[] UnStr_1004 = new string[2]{"Friends ", "/200"};
  public const string UnStr_RecommandFilterType = "依條件搜尋：";
  public const string UnStr_AddFreindByNameDescription = "輸入欲新增好友ID...";
  public const string UnStr_1005 = "Instance list is diabled";
  public static string[] UnStr_0101 = new string[2]{"ConfirmSendInvitation", "Input the name of role"};
  public static string[] UnStr_0102 = new string[2]{"ComfirmDeleteFriend", "[[[align=center]]][[[paraGap=8]]] Confirm remove|[[[color=B2EC0A]]]『{0}』"}; 
  public static string[] UnStr_0103 = new string[4]{"所在地區無法進入", "騎乘坐騎時無法進入", "須先完成薩亞城的「文明寶典」任務", "戰鬥中無法進入"};
  public static string[] UnStr_0001 = new string[2]{"Add friend","Search friends"};
  public const string UnStr_RequireLevel = "《需達等級{0}級》";
  public static string[] UnStr_0401 = new string[7]{"Lv", "Online", "Offline", "Online", "None record", "Channal {0}", "組隊次數: {0}"};
  public static string[] UnStr_0402 = new string[3]{"s", "m", "hr"};
  public const string UnStr_0403 = "Suggested player\nClick "+" to add friend"; 
  public static string[] UnStr_0404 = new string[6]{"Suggest playrs with similar level", "Suggest playrs with similar living profession", "Facebook friends", "Add all suggest players to invite section", "Send invitations to chosen players", "Return to friend list"};
  public static string[] UnStr_0405 = new string[7]{"文明之塔", "騎士團", "飛天搶金", "聖光鬥技", "攻堅行動", "節慶活動", "即刻報到"};
  public static string[] UnStr_GroupFunctionHints = new string[7]{"重建帝國的大師之路", "維護亞特的世界和平", "你今天飛過了嗎？", "拿起你的武器，準備決鬥！", "拯救王子大作戰", "", "快來報到，王子給獎！"};
  public static string[] UnStr_GroupFunctionDescs = new string[7]{"", "", "", "點選此按鈕報名參加「聖光鬥技」： |%b<規則>%w |1.報名後需等待系統自動配對 |2.進場後直接進行對戰，時間內打倒對方者獲勝 |3.時間到，勝負依血量判斷 <獎勵> 依狀況獲得：%y經驗值%w、%y鬥技表揚證明%w、%y鬥技福袋%w、%y鬥技驚喜袋%w(內含%p「神織設計圖」%w或%p「紫色披風」%w) |4.%r每天晚上7點到晚上12點開放報名進場%w，系統將%r每十分鐘%w配對一次。|※詳細規則詳見官網", "" ,"" ,""};
  public const string UnStr_ActivityInactive = "非活動開放期間";
  public static string[] UnStr_0501 = new string[3]{"Can't add yourself", "Can't invite your friend", "發送邀請太頻繁，請於{0}秒後再發送"};
  public static string[] UnStr_0601 = new string[4]{"whisper", "recruitment", "information", "delete"};
  public const string UnStr_0701 = "Loading Apearance";
  public const string UnStr_0702 = "Friend Info";
  public const string UnStr_0703 = "Please send watch player information afer {0} seconds ";
  public static string[] UnStr_0801 = new string[2]{"[[[align=center]]][[[paraGap=4]]][[[color=B2EC0A]]]『{0}』|%wInvite you ride together|auto cancel after%r", "%wseconds"};
  public static string[] UnStr_0802 = new string[14]{"Had ride", "Ride failt: Too far", "Ride failt: Busy", "Ride failt: Had anomalies", "Ride failt: Different scenes", "Ride failt: Refuse", "Ride failt: Other asking", "Ride failt: Busy", "Ride failt: No sit", "Ride failt: Reject ride", "Passenger leave", "Ride invite successful", "Can't invite the player riding with you", "Loading mount, can't accept invitation"};
  public static string[] UnStr_0803 = new string[3] {"Leaving mount", "Passenger {0} leave mount", "Drive out"}; 
  public const string UnStr_0901 = "Chance";
  public const string UnStr_0902 = "Destiny";
  public const string UnStr_0903 = "CHANCE V.S. DESTINY";
  public static string[] UnStr_0904 = new string[3] {"Gold ", "Exp.", "銀幣"};
  public const string UnStr_0905 = "Choose Chance or Destiny, and luck will be there for you";
  public const string UnStr_0906 = "Bag is full, please remain a space";
  public const string UnStr_0907 = "Gold reached upper limit, please try again after there is space";
  public const string UnStr_0908 = "Got reward";
  public const string UnStr_0909 = "你選擇{0}，獲得{1}，已經加到你身上囉";
  public const string UnStr_0910 = "離開"; // 離開機會命運介面按鈕
  public static string[] UnStr_1101 = new string[2]{"Get ", "Crafting failt，救回一個材料{0}"};
  public const string UnStr_1102 = "Craft";
  public static string[] UnStr_1103 = new string[10]{"Legends", "Weapons", "Armors", "Accessories", "Foods", "Potions", "Specialities", "Elements", "Machines", "Clothes"};
  public const string UnStr_1104 = "Materials";
  public static string[] UnStr_1105 = new string[4]{"You need to put at least three materials to mix", "Insufficient energy, unable to use Mix Machine", "Materials for mixing are not enough", "No enough space in material box or bag, unable to use Mix Machine"};
  public const string UnStr_1106 = "Please pull the rod on the Mix Machine to start mixing";
  public const string UnStr_1107 = "Atlantis Meta-technology Research";
  public static string[] UnStr_1108 = new string[2]{"After the old Stuart sink, people can make use of the materials obtained from the Tower of Civilization and Zio Power Mixer.", "[[[paraGap=8]]][[[color=B2EC0A]]][放入物品]: 點擊材料區物品|[混合方式]: 點擊混合機的操作桿"};
  public const string UnStr_1109 = "Rate of acquisition";
  public const string UnStr_1110 = "? ? ? ? ? ? ? ? ?";
  public const string UnStr_1111 = "Return to category page";
  public const string UnStr_1112 = "Return to chapter page";
  public static string[] UnStr_1113 = new string[4]{"Type: ", "Item Lv: ", "回收價: {0}", "合成機率: {0}%"};
  public const string UnStr_1114 = "請再試一次或重新開啟介面";
  public static string[] UnStr_1115 = new string[2]{"Production methods", "Resource type"};
  public const string UnStr_1116 = "Recipe table";
  public const string UnStr_1117 = "Can be used to craft the following items";
  public const string UnStr_1118 = "%j獲得新配方|%l{0}%j！！！！";
  public const string UnStr_CraftLimit = "合成限制：";
  public const string UnStr_Comma = ",";
  public static string[] UnStr_CraftFails = new string[2]{"這樣的組合似乎可以做出甚麼東西，可能是我的運氣不好", "這樣的組合似乎可以做出甚麼東西，可能是我的技巧還不夠({0})"};
  public static string[] UnStr_CraftLimits = new string[3]{"名氣值不足{0}", "工具階段不足", "需先習得配方"};
  public const string UnStr_1119 = "OK";
  public const string UnStr_1120 = "材料不足";
  public const string UnStr_1121 = "只顯示已習得配方";
  public const string UnStr_SearchRecipe = "關鍵字搜尋配方";
  public const string UnStr_AutoCraft = "自動合成";
  public const string UnStr_AutoCraftHint = "點擊自動放上材料合成該項物品";
  public const string UnStr_MultiCraft = "連續合成";
  public const string UnStr_MultiCraftHint = "";
  public const string UnStr_HasVisionProduction = "此為已習得配方";
  public const string UnStr_NoVisionProduction = "這樣的組合可以做出什麼呢？";
  public const string UnStr_MultiCraftInfoText = "合成{0}|請輸入數量";
  public const string UnStr_MultiCraftMsg = "合成 {0} {1} 次";
  public const string UnStr_MultiCraftLimitHint = "想使用快速的連續合成嗎？快成為尊榮的VIP等級{0}神選者吧！";
  public static string[] UnStr_1201 = new string[7] {"The item can't be used", "2.Not a item for Material Shelf", "3.Material Shelf place error", "4.Add item to Material Shelf failed", "5.Amount exceed the upper limit", "6.Unable to stack different items", "7.No suitablt place to put"};
  public static string[] UnStr_1301 = new string[2] {"[[[size=18]]][[[underline=B5F6FE]]][[[OutlineColor=163640]]][[[color=B5F6FE]]]More Tips...", "[[[size=18]]][[[underline=9EFFC7]]][[[OutlineColor=175B51]]][[[color=9EFFC7]]]More Tips..."};
  public const string UnStr_1302 = "Finish: ";
  public const string UnStr_1303 = "待完成";
  public const string UnStr_1304 = "稍後完成";
  public const string UnStr_1305 = "OK！";
  public const string UnStr_1306 = "飛喵在大師、騎士團或特殊場景中，無法做更多提示";
  public const string UnStr_1307 = "提示更多";
  public static string[] UnStr_1308 = new string[2] {"[[[size=14]]][[[underline=B5F6FE]]][[[OutlineColor=163640]]][[[color=B5F6FE]]]稍後完成", "[[[size=14]]][[[underline=9EFFC7]]][[[OutlineColor=175B51]]][[[color=9EFFC7]]]稍後完成"};
#region 收件夾
	public const string UnStr_1401 = "Messages";
	public const string UnStr_1402 = "You can send friends the materials in demand by accepting their ask.";
	public const string UnStr_1403 = "Accept All";
	public static string[] UnStr_1404 = new string[3]{"Accept", "Ignore", "Confirm"};
	public const string UnStr_1405 = "儲物箱空間已滿，已無法接受好友的禮物，請清除不需要的工具或材料";
	public static string[] UnStr_1406 = new string[5]{"成功", "系統的收件匣訊息數量已滿", "找不到收件玩家", "今天已經有送同一訊息給此玩家", , "收件匣功能暫停中"};
	public const string UnStr_1407 = "{0:0000}年{1:00}月{2:00}日{3:00}時{4:00}分";
	public static string[] UnStr_1408 = new string[3]{"0.成功", "1.跟你不同組伺服器,所以無法成為好友", "2.好友名單數量已達上限"};
	public static string[] UnStr_InboxErrorMsgs = new string[9]{"加儲物櫃物品失敗", "沒有適合的儲物櫃擺放空間", "身上物品已滿", "數量超過上限", "負重不足", "加身上物品失敗", "超過不可交易金幣上限", "超過身上銀幣上限", "其他失敗"};
	public const string UnStr_InboxAcceptAllLimit = "升級VIP就可以使用全部同意的便利功能囉!";
	public const string UnStr_InboxRoyalClubBtn = "皇家俱樂部";
#endregion
#region 材料邀請
	public static string[] UnStr_1501 = new string[3]{"已經對 ", " 等人...", " 送出材料邀請。"};
	public const string UnStr_1502 = "詢問好友";
	public const string UnStr_1503 = "請朋友送你";
	public static string[] UnStr_1504 = new string[2]{"送出邀請","返回"};
	public static string[] UnStr_1505 = new string[3]{"將全部推薦好友都加到待邀請區", "對邀請區玩家送出邀請", "返回解鎖介面"};
	public const string UnStr_1506 = "次數 {0}/{1}";
	public const string UnStr_1507 = "可邀請次數已達到上限";
#endregion
#region 大師系統
	public static string[] UnStr_1601 = new string[2]{"Visit", "Invite"};
	public static string[] UnStr_1602 = new string[3]{"好久不見，歡迎你來我家坐坐！", "我的文明之塔需要你的幫忙，請來我家逛逛吧！", "誠摯地邀請你來拜訪我的文明之塔！"};
	public const string UnStr_1603 = "邀請 {0} 來我的文明之塔";
	public static string[] UnStr_1604 = new string[5]{"恭喜", "你成功研發新科技", "文明之塔完成擴建|本次獲得", "你的名氣值到達", "下階段名氣值目標"};
	public static string[] UnStr_1605 = new string[2]{"可獲得銀幣", "可獲得能量"};
	public const string UnStr_1606 = "第{0}層樓";
	public const string UnStr_AssistRewardMsg = "[[[size=22]]]感謝你協助|[[[size=16]]]請收下我的禮物";
	public const string UnStr_TOKVisitLeft = "今天尚可拜訪與協助{0}位，沒有進行過協助的朋友";
	public const string UnStr_CantFriendAssit = "恭喜你已經完成今日好友的協助，每天中午會恢復協助次數，記得要來幫忙你的好友們唷！";
#endregion
#region 人民拜訪
	public static string[] UnStr_1701 = new string[6]{"學習{0}的人民", "熱心的人民", "科技大師", "皇家建設隊", "亞特蘭提斯貴族", "亞特蘭提斯貴族"};
	public static string[] UnStr_1702 = new string[4]{"人民拜訪任務成功獲得銀幣", "人民拜訪任務成功獲得熟練度", "人民拜訪任務成功獲得材料", "人民拜訪任務成功獲得金幣"};
	public static string[][] UnStr_1703 = new string[6][]{new string[2]{"[[[align=center]]]%v學習{0}的人民", "%w請將他送到放有「%y{0}%w」的樓層"}, 
	new string[2]{"[[[align=center]]]%v熱心的人民", "%w送他到任何有放工具的樓層他會協助你%y研究其中一件工具一次"}, new string[2]{"[[[align=center]]]%v科技大師", "%w送他到任何有放工具的樓層他會將%y其中一件工具的熟練度提升到滿"}, 
	new string[2]{"[[[align=center]]]%v皇家建設隊", "%w送他到建造中的樓層，可%y縮短建造時間3小時"}, new string[2]{"[[[align=center]]]%v亞特蘭提斯貴族", "%w送他到任何有放工具的樓層他會協助你將該樓層內%y所有工具進行5次研究"}, new string[2]{"[[[align=center]]]%v亞特蘭提斯貴族", "%w送他到任何有放工具的樓層他會協助你將該樓層內%y所有工具進行5次研究"}};
	public static string[]	UnStr_1704 = new string[2]{"Silver + {0}", "Gold + {0}"};
	public const string UnStr_HasVisitor = "有人民到訪";
#endregion
#region 榮耀加冕
	public const string UnStr_RoyalTitle = "榮耀加冕";
    public const string UnStr_ChangeTitle = "頭銜更換";
    public const string UnStr_TitleColor = "尊榮色";
	public const string UnStr_OwnSpecialTitle = "已擁有";
	public const string UnStr_FrontTitle = "前銜名";
	public const string UnStr_BackTitle = "後銜名";
	public const string UnStr_SpecialtTitle = "特殊榮耀";
	public const string UnStr_ApplyTitleHint = "點選配戴此頭銜";
	public const string UnStr_DisableTitleHint = "點選卸下此頭銜";
	public const string UnStr_ApplyTitleMsg = "成功更換頭銜！";
	public const string UnStr_DisableTitleMsg = "成功卸下頭銜！";
	public const string UnStr_LiteralTitleEmpty = "即將開放";
	public const string UnStr_SpecialTitleEmpty = "目前尚未擁有特殊榮耀";
	public const string UnStr_OwnTitle = "已加冕";
	public const string UnStr_OwnTitleHint = "已加冕！可點選[頭銜更換]的頁籤前往配戴";
	public const string UnStr_OwnTitleNum = "已加冕的頭銜數量";
	public const string UnStr_EffectHint = "集滿左方5個頭銜，將永久獲得此榮耀效果";
	public const string UnStr_Crown = "加冕";
	public const string UnStr_LearnTitleSuccessful = "加冕成功：{0}";
	public const string UnStr_LearnTitleEffect = "獲得永久榮耀效果：{0}";
	public const string UnStr_HonorInsufficient = "您的榮耀值不足";
	public const string UnStr_DontUse = "不使用";
	public const string UnStr_HonorHelpHint = "榮耀值可由特定的評定任務、活動中獲得";
	public const string UnStr_TitleLevelLimitMsg = "LV32以上的亞特子民將有「榮耀加冕」的資格";
	public const string UnStr_CanLearnTitleMsg = "[[[OutlineColor=EBDBC2]]][[[color=5B200E]]]你達成了[[[color=FF0000]]]{0}[[[color=5B200E]]]的加冕條件。";
	public const string UnStr_OpenRoyalUI = "開啟榮耀加冕";
	public const string UnStr_Finish = "(已完成)";
	public const string UnStr_TitleLimit = "您尚未達成此頭銜的加冕條件";
	public const string UnStr_DontHaveTitle = "尚未擁有";
	public const string UnStr_HadNumTitleMsg = "取得方式：擁有{0}個以上的頭銜";
	public const string UnStr_TitleColorSelectorTitle = "更換尊榮色";
#endregion
	public static string UnStr_PopupConstructFinish = "{0}%w樓層已經蓋好囉！|%w這次獲得了{1}！";
	public static string UnStr_PopupNewInboxMsg = "你收到了一封新訊息，|快打開訊息介面看看！";
	public const string UnStr_RacingScoreTitle = "目前分數";
	public static string UnStr_PopupNewskill = "%j獲得新勳章||%w{0}%j！！！";
	public static string UnStr_PopupNewchar = "%j獲得新個性||%w{0}%j！！！";
#endregion

#region teamSettingUI
  public static readonly string[] TmSet_title = new string[3]
  {
		"Party Settings",
		"Display",
		"Leader Settings"
  };  
	
  public static readonly string[] TmSet_ItemN = new string[4]
  {
		"．Status",
		"．Exp、Silver",
		"．Item",
		"．Item LV"
  };	
	
  public static readonly string[] YesOrNo = new string[2]	
  {
		"Accept", "Cancel",
  };
	
  public static readonly string[] Team_GreedOrNot = new string[3]
  {
		"Need- I need this one", "Greed-  if no one need  this", "秒"
  };
	
  public const string GreedOrNot_Full = "背包已滿，若贏得此物品，物品將被系統回收";
	
  public static readonly string[] TmSet_wtMode = new string[3]
  {
		"Standby", "Defend", "Attack"
  };

  public static readonly string[] GreedChoice = new string[3]
  {
		"Give up", "Need", "Greed"
  };

//  public static readonly string settingSuggestion = "---Plaese choose---";
  public static readonly string[][] settingItemStr = new string[4][]
  {
    new string[]{"Display none",   "Display all",   "Display none",  "Buff",   "Debuff"},//顯示
    new string[]{"Solely obtain",  "Solely obtain", "Equally distribute"},				//獲得金錢＆經驗模式
    new string[]{"Solely obtain",  "Solely obtain", "Obtain in turns", "Distribute with need"},   //獲得物品模式
    new string[]{"Blue or better", "Blue or better", "Purple or better"},	//
  };
	
//  public static readonly byte[][] settingItemNum = new byte[4][]
//  {
//	new byte[]{1, 0,1,2,3},//{"全顯示",   "全顯示",  "不顯示",   "好狀態",   "壞狀態"},		
//	new byte[]{0, 0,1},	   //{"獨自獲得", "獨自獲得", "平均分配"},
//	new byte[]{0, 0,1,2},  //{"獨自獲得", "獨自獲得", "輪流獲得", "需求分配"}, 
//	new byte[]{0, 0,1},    //{"藍色以上", "藍色以上", "紫色以上"}
//  };		
#endregion teamSettingUI
	
#region ATMission
	
	public static readonly string[] Str_Mission0001 = new string[]
	{
		"介面標題",
		"任務標題任務標題任務標題任務標題任務標題",
		"任務大綱",
		"任務內文1任務內文2任務內文3任務內文4任務內文5任務內文6任務內文7任務內文8任務內文9任務內文0任務內文1任務內文2任務內文3",
		"   {0}",
		"確  定",
		"刪除任務",
		"接受任務",
		"拒絕任務",
		"接受任務",
		"可解次數:({0}/{1})"
	};
	
	public static readonly string[] Str_Mission0002 = new string[]
	{
		"OK_{0}",
		"complete",
		"delete",
		"accept",
		"request"
	};
	
	public static readonly string[] Str_Mission0003 = new string[]
	{
		"王國評定",
		"隨機評定",
		"勇者評定",
		"文明評定",
		"世界評定",
		"機會命運",
		"副本評定",
		"實力評定",
		"騎士評定",
		"地城評定",
		"文明回報",
		"騎士團回報",
		"隨機回報",
		"實力回報",
		"秘密評定",
		"秘密評定",
		"秘密評定"
	};
	
	public static string[] Str_Mission0004 = new string[3] {"ATMissionConfirmDelete","刪除提示","確認是否刪除？"};
	
	public static string[] Str_Mission0005 = new string[3] {"ATMissionConfirmGoldUse","提示","是否使用金幣消費？"};
	
	public static readonly string Str_Mission0006 = " 適合等級：";
	
	public static readonly string Str_Mission0007 = "({0},{1})";
	
	public static readonly string Str_Mission0008 = "{0}";
	
	public static readonly string Str_Mission0009 = "任務已達上限";
	
	public static readonly string Str_Mission0010 = "完成條件：{0}/{1}";
	
	public static readonly string Str_Mission0011 = "隨機獲得金幣/虛寶/銀幣 三者其一";
	
	public static readonly string[] Str_Mission0012 = new string[]
	{
		"依表現獲得經驗值",
		"依表現獲得銀幣",
		"依表現獲得金幣"
	};
#endregion
	
#region CharacterFM
  public static readonly bool Str_isChinese = false;

#region Attr2Type
  public static readonly string[] Str_Attr2Type = new string[4]
  {
    "Attack",
    "Defense",
    "Magic",
    "Others",
  };
#endregion
#region Attr2Title
  //[][][0] 中文 [][][1]英文
  public static readonly string[][] Str_Attr2Title= new string[][]
  { //攻擊屬性
    new string[]{
      "Attack Power",   
      "Magic Damage",   
      "Heal Amount",     
      "Critical Hit", 
      "Magic Critical Hit", 
      "Attack Speed",   
      "Spell Speed",
    },
    //防禦屬性
    new string[]{
      "Defense", 
      "Tenacity",   
      "Dodge",   
      "Hit",   
      "Block",   
      "Penetration",   
      "Parry",   
      "Destroy",
    },
    //法術抗性
    new string[]{
      "Earth Resistance",
      "Water Resistance",
      "Fire Resistance", 
      "Wind Resistance", 
      "Light Resistance",
      "Dark Resistance"
    },
    //其他屬性
    new string[]{
      "HP Regen", 
      "MP Regen", 
      "癒合力",
      "調和力",
      "Move Speed", 
      "Luck",
    },
  };
  public const string Str_Attr2HintFormat = "%j{0}{1}|{2}";
  public static readonly string[][] Str_Attr2Hint = new string[][]
  { //攻擊屬性
    new string[]{
		"%w影響%o普通攻擊、技能攻擊%w造成的傷害",
		"%w影響%j技能攻擊%w造成的傷害",
		"%w影響%o治療類型技能%w造成的治療效果",
		"%w影響%o普通攻擊、物理傷害型技能攻擊%w發動%y爆擊%w的機率，會受目標的%y韌性%w影響，%y爆擊%w發動會使傷害加倍",
		"%w影響%o魔法傷害型技能%w攻擊發動%y爆擊%w的機率，會受目標的%y韌性%w屬性影響，%y爆擊%w發動會使傷害加倍",
		"%w影響%o普通攻擊%w的攻擊速度",
		"%w影響%o技能攻擊%w詠唱需要的時間",
    },
    //防禦屬性
    new string[]{
		"%w可減少受到%o普通攻擊、物理類型技能%w的傷害",
		"%w可減少受到%o普通攻擊、技能攻擊%w時被發動爆擊的機率",
		"%w可增加受到%o普通攻擊、技能攻擊%w時發動%y閃避(Dodge)%w的機率，%y閃避%w發動可完全不受到該次傷害",
		"%w可減少%o普通攻擊、技能攻擊%w時被目標發動%y閃避(Dodge)%w的機率，受目標的%y閃避%w屬性影響",
		"%w可增加受到%o普通攻擊、物理類型技能%w時發動%y格擋(Block)%w的機率，%y格擋%w發動可完全不受到該次傷害，並對目標造成一次普攻傷害",
		"%w可減少%o普通攻擊、物理類型技能%w時被目標發動%y格擋(Block)%w的機率，受目標的%y格擋%w屬性影響",
		"%w可增加受到%o普通攻擊、物理類型技能%w時發動%y招架(Parry)%w的機率，%y招架%w發動時可讓該次傷害減半",
		"%w可減少%o普通攻擊、物理類型技能%w時被目標發動%y招架(Parry)%w的機率，受目標的%y招架%w屬性影響",
    },
    //法術抗性
    new string[]{
		"%w可減少受到%o地屬性技能%w的傷害",
		"%w可減少受到%o水屬性技能%w的傷害",
		"%w可減少受到%o火屬性技能%w的傷害",
		"%w可減少受到%o風屬性技能%w的傷害",
		"%w可減少受到%o光屬性技能%w的傷害",
		"%w可減少受到%o闇屬性技能%w的傷害",
    },
    //其他屬性
    new string[]{
		"%w非戰鬥中每12秒可回復的生命",
		"%w非戰鬥中每12秒可回復的魔力",
		"%w使用一次性恢復生命的藥水時，會受此屬性影響而使恢復量加成",
		"%w使用一次性恢復魔力的藥水時，會受此屬性影響而使恢復量加成",
		"%w影響在地圖上移動之速度",
		"%w影響擊倒怪物時，獲得寶物的機率",
    },
  };
#endregion
  public static readonly string Str_DressHint = "|[[[color=240,225,10]]]點擊可開啟造型商店";
  public static readonly string[] Str_AttrClass = new string[]
  {
    "Novice",
    "Holy Swordsman",
    "Evangel Priest",
    "Shadow Mage",
    "Abyss Assassin",
    "Gear Master",
    "Alchemist",
    "Holy Archer",
    "Black Knight",
    "Steam Weapon",
  };
  public static readonly string Str_newbie ="Apprentice ";
  public static readonly string[] Str_AttrInfo = new string[]
  {
    "%jStrength|%wIncreases your %oAttack Power%w and %oParry Power%w",
    "%jAgility|%wIncreases your %oCritical Hit rate%w and %oDodge rate%w",
    "%jVitality|%wIncreases your %omaximum of HP%w and %oParry Rate%w",
    "%jIntelligence|%wIncreases your %oMagic Damage%w, %oHealing Amount%w, %oMagic Critical Hit rate%w, and %omaximum of MP%w ",
    "%jSpirit|%wIncreases your %oHP Regen%w, %oMP Regen%w, and %oTenacity%w",
    "%jAgility|%wIncreases your %oAttack Power%w, %oCritical Hit rate%w and %oDodge rate%w",
  };
  public static readonly string[] Str_CharacterButtonHint = new string[]
  {
	  "頭部外觀顯示：開",//0
	  "頭部外觀顯示：關",//1
	  "顯示角色屬性",//2
	  "隱藏角色屬性",//3
	  "命運之輪",//4
	  "自身強化光影：開",//5
	  "自身強化光影：關",//6
  };
  public static readonly string[] Str_CharacterInofText = new string[]
  {
	"魔化效果",
	"(身上尚無魔化裝備)",
	"詳細裝備魔化方式可開啟魔法故事書（H）→裝備篇 進行了解",
  };
#endregion


#region 強化介面
	public static readonly string Str_Strengthen_Title = "Soul Core Torquetum ";
	public static readonly string[] Str_Strengthen_BtnLabel =new string[]{"Wake Star Gem","Continual Wake","Holy Pray","主星覺醒","能量回收","連續爆發" };
	public static readonly string[] Str_Strengthen_Hint = new string[] {
		"[[[size=15 color=245,160,35]]]使用說明：|%w        放入裝備後，點擊[[[color=130,230,255]]]覺醒%w按鈕來獲得星石能量，累積的能量越多裝備的效果也越強。|[[[color=170,225,55]]]每次累積5能量，都會增加星石能量效果，集滿25能量後，裝備上的基本攻擊力與基本防禦力都會增加，裝備的強度等級也會隨著點亮的大星石而增加。",
		"您身上沒有爆發之心，請問是否要消耗金幣進行星石爆發？",//1
		"我了解，不再顯示此訊息",//2
		"確認",//3
		"取消",//4
		"[[[size=14 color=141,180,227]]]星石能量：{0} {1}",	//5
		"[[[size=16 color=216,237,144 OutlineColor=16,64,19]]]請放置欲強化的裝備於星儀上",//6
		"尚未開啟",//7
		"使用銀幣每次消耗：",//8
		"使用金幣每次消耗：",//9
		"(能量有機會爆擊)",//10
		"尚餘 {0}/{1} 次",//11
		"請輸入覺醒最大次數",//12
		"銀幣不足！是否要使用金幣來完成接下來的次數？",//13
		"中止",//14
		"[[[size=16 color=238,230,161]]]基本攻防 + {0}%",//15;
		"[[[size=16 color=255,248,139 OutlineColor=178,50,20]]]基本攻防 + {0}%",//16;
		"花費銀幣進行一次覺醒星石的動作。",//17
		"每次消耗1個爆發之心，來累積能量進行爆發，可以取得特殊效果",//18
		"可以自行輸入次數，進行多次的覺醒動作。",//19
		"可以回收5格能量，換取銀幣，如果能量不足會降星等。",//20
		"因為能量不足，所以此次回收能量會造成降星，請確認是否要進行回收。",//21
		"爆發之心不足！是否要使用金幣來完成接下來的次數？",//22
		"使用爆發之心：",//23
		"請輸入爆發最大次數",//24
		"可以自行輸入次數，進行多次的爆發動作。",//25
		"裝備出現新的隨機效果！",//26
		"哇！裝備上出現神秘的主星！",//27
		"主星爆發！此裝備已到達強化的巔峰！",//28
	};
	public static readonly string[] Str_Strengthen_AwakeHint = new string[]{
		"[[[align=center]]]Sure to wake the Star Gem?",
		"Costs: |[[[align=center]]]",
		"[[[align=center]]]Input waking times",
		"Costs per time:|[[[align=center]]]",
		"[[[color=225,70,54]]]※It will take the needed silver every single time.",
		"%oCosts per time:%w",
	};
	public static readonly string[] Str_Strengthen_PrayHint = new string[]{
		"[[[align=center]]]Sure to use Pray Item to do the Holy Pray?",
		"※If it fails, the Pray Item will scrap.",
		"[[[align=center]]]You don't have Pray Item now, do you want to do the Holy Pray with Gold?",
		"[[[align=center]]]%y(need xxx Gold)",
	};
	public static readonly string[] Str_Strengthen_ErrorInfo = new string[]{
		"Times error",
	};
	public static readonly string[] Str_Strengthen_ResultSucessMessage = new string[]{
		"Congratulations! The Star Gem is wokenm the equipment is more powerful now!","Congratulations! Holy Pray succeeded, there is special effect enchanted on the equipment!"
	};

	public static readonly string Str_StrengthenInfo_Title = "Star Gems";
	public static readonly string[] Str_StrengthenInfo_Text = new string[]{
		"頭飾、上衣、褲鞋",
		"面具、護腕、披風",
		"武器、戒指、項鍊",	
		"[[[size=15]]]　　當裝備上的星石皆覺醒時，就到了爆發階段。此階段會出現帶有[[[color=83,221,235]]]隨機效果%w的特殊星石，可利用[[[color=83,221,235]]]【星石爆發】%w來開啟。而各種隨機效果又分為[[[color=83,221,235]]]普通、超強(MAX)%w兩種強度將視運氣出現。|%o目前置放在強化星儀上的裝備有機會得到的隨機效果一覽：",
		"增加",
	};
#endregion

#region 物品強化相關訊息
	public static readonly string[] Str_StrengthenItemResult = new string[7] {
		"",
		"The Star Gem just shined a little, you gotta keep trying.",
		"Pray failed", 
		"insufficient Silver！",
		"", 
		"Item being used" ,
		"insufficient Gold！"
	};


#endregion
#region 技能強化
	public static readonly string[] Str_Evolution_Nebula_Name = new string[]{
		"Rosette Nebula",
		"Hourglass Nebula",
		"Helix Nebula",
		"Eagle Nebula",
		"Cat's Eye Nebula",
		"Ant Nebula",
		"Tarantula Nebula",
	};
	public static readonly string[] Str_Evolution_Nebula_Content = new string[]{
		"Acquire power from Rosette Nebula to increase the healing amount of Main Star skills",
		"Acquire power from Hourglass Nebula to decrease the CD of Main Star skills",
		"Acquire power from Helix Nebula to increase the buff duration of Main Star skills",
		"Acquire power from Eagle nebula to increase the damage of Main Star skills",
		"Acquire power from Cat's Eye Nebula so that there are chances to buff/debuff when using Mai Star skills",
		"Acquire power from Ant Nebula to decrease MP cost of Main Star skills",
		"汲取蜘蛛星雲能量使主星技能造成傷害時有機率附加狀態",
	};
	public static readonly string[] Str_Evolution_Content = new string[]{
		"Evolution",
		"選擇一個主星技能",
		"還擇一種星雲能量",
		"主星技能：{0}演化為{1}",
		"死亡中無法進行演化",
	};
#endregion

#region VIP介面
 public static string VIPUI_Title = "VIP";
  public static string GoToStore = "Top Up";
  public static string VIPExplainArea = "VIP功能說明區";

  public static string[] ChineseDigital = new string[11] {"", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十"};
  public static string VipStr = "VIP {0} Star";
  public static string RemainingGoldRequired = "Upgrade after top up {0} more";
	
  //領取詢問
  public static string[] Confirm_GetReward = new string[3] {"GetReward" ,"領取提示", "是否要領取物品?"};
#endregion VIP

#region Vip_ProMsg_101-6-21
public static string[] Trade_ProMsg_6_21 = new string[4]
{
	"Exchange VIP reward:{0} successfully",
	"Exchange VIP reward: Already got this",
	"Exchange VIP reward: VIP LV unquilified",
	"Exchange VIP reward: In sufficient space in Bag"
};
#endregion

#region 死亡復活相關訊息
	
  public static readonly string[] Below30_RebornRuleExplan = new string[4]
  {
		"",
		"[[[align=center]]][[[size=14]]][[[color=192,88,88]]][角色等級30級以前]|%wResurrect at the spawn point|without any punishment.",
		"",
		"",
  };

  public static readonly string[] Upper30_RebornRuleExplan = new string[4]
  {
		"",
		"[[[align=left]]][[[size=14]]]1.Durability of equipped items %cdecreases by 25%|%w2.Under weakened soul debuff for %c180 seconds%w,|the debuff effect: attack power, magic demage,|resistance and defense all %cdecrease by 50%",
		"",
		""
  };

  public static string CostSilver_Reborn ="[[[align=left]]][[[size=12]]]1.Durability of equipped items %cdecreases by 5%|%w2.Under weakened soul debuff for %c180 seconds,|the debuff effect: attack power, magic demage,|resistance and defense all %cdecrease by 50%.%w It takes Silver: %c{0}%w(隨死亡次數，增加費用，每日中午重置)";
  public static string CostGold_Reborn   ="[[[align=left]]][[[size=12]]]1.Resurrect at current location|2.Under protection buf for 5 seconds,|the buff effect：Can't do anything, but also won't be hurt or get any debuff. It takes Gold: %c{0}%w";

  public static string[] RebornMethodName = new string[4]
  {
		"","",
		"等級31級以上方可使用",//"Resurrect with Silver",
		"等級31級以上，且VIP3星以上，方可使用"//"Resurrect with Gold",
  };

  public static readonly string DeathUI_Title = "Resurrection";
  public static readonly string Reborn_Hint = "[[[align=center]]][[[size=15]]][[[127,191,107]]] |Pleace a way to resurrect";

  public static readonly string[] CostIsNotEnough = new string[4]
  {
		"", "",
		"Insufficient Silver",
		"Insufficient Gold",
  };
	
#endregion 
	
#region 區域相關訊息
  public static readonly string[] Area_ProMsg_9 = new string[13]
  {
	"",
	"玩家一般殺死",
	"玩家武功殺死",
	"玩家內功殺死",
	"npc一般殺死",
	"npc特殊攻擊殺死",
	"玩家十層功力槍法波及",
	"玩家棍法反彈",
	"玩家大範圍打玩家波及",
	"玩家大範圍打npc波及",
	"npc大範圍打玩家波及",
	"玩家內傷攻擊",
	"npc內傷",
  };
	
  public static readonly string[] Area_ProMsg_10 = new string[3]
  {
		"自己爬起來",
		"自發武功",
		"他人武功",
  };
	
  public static readonly string KilledDyingMsg  = "[[[align=left]]][[[size=14]]]You were killed by【[[[192,88,88]]]{0}%w】,|choose a way to resurrect!";
  public static readonly string DefaultDyingMsg = "[[[align=center]]][[[size=14]]]You were killed, choose a way to resurrect!";
  public static string DyingInfor = string.Format(DefaultDyingMsg, "");// 死亡介面未載下來暫存訊息用
	
#endregion
	
#region 組隊協定相關訊息 Form Team
  public static readonly string BeenOffline = "{0} 已離線";

  public static readonly string[] ft_ErrorCode = new string[33]
  {
	"",
	"No such character or the character currently offline",//1. 
	"Too far away",//2.
	"This player has joined another team",//	3.
	"The invitation is rejected",//	4.
	"The function can only be used by leader",//5.
	"You have team already, unable to use this function",//6.
	"The player has other invitation to reply",//7.
	"There is no invitation or the inviter is offline",//8.
	"The player didn't send invitation to you",//9.
	"Parties overloaded, unable to form new one",//10.
	"There is no vacancy in the party",//11.
	"The player doesn't belong to any party",//12.
	"The player isn't party leader, unable send invitation to you",//13.
	"Only the leader can send invitation",//14.
	"You aren't in any party yet, unable to use this function",//15.
	"You aren't in the same party, can use function",//16.
	"You are occupied by other things, unable to form/join a party",//17.
	"There is no player can be the leader, pass the leadership failed",//18.
	"The player is occupied by other things, unable to join the party",//19.
	"Party functions is forbidden in current scene",//20.
	"The player is in a scene which forbids party functions",//21.
	"You aren't in the same sect, can't be in the same party",//22.
	"There is no party needs members in current scene",//23.
	"Private party doesn't open for applying",//24.
	"There no player without party in this scene",//25.
	"The party password incorrect",//26.
	"Party information is illegal",//27.
	"The player is in other raid",//28.
	"You are in a raid, unable to use this function",//29.
	"The function can only be used by raid leader or vice raid leader",//30.
	"You are in instance matching, unable to use this function",//31.
	"The player is in instance matching",//32.
  };

  public static readonly string ft_DialogTile = "Form Party";
  public static readonly string[] ft_FunctionName = new string[4]
  {
		"Leave",
		"Dismiss",
		"Assign leader",
		"Expel member",
  };
	
  public static readonly string AskedTeam = "已送出詢問組隊要求";

	public static readonly string[] ft_ProMsg_3 = new string[2]{"[[[align=center]]][[[paraGap=8]]] %o『{0}』%w invite you to join the party, accept?||Reject after %o ", " seconds%w"};

  public static readonly string TeamFormed = "隊伍成立";
  public static readonly string Invitation = "已送出邀請入隊要求";
	
  public static readonly string[] ft_ProMsg_6 = new string[4]
  {
	"Got into the party", "Loot: exp and Silver with \"{0} mode\", items with \"{1} mode\"",
	"[[[align=center]]][[[paraGap=8]]] %o『{0}』%w invite you to join the party, accept?|Reject after %o ", " seconds%w"
  };

  public static readonly string NewMemberJoin = "新隊員:{0} 加入";
	
  public static readonly string LeaveTeam =	"{0} 離開隊伍";
	
  public static readonly string BecomeLeader = "{0} 成為隊長";
	
  public static readonly string[] ft_ProMsg_10 = new string[3]
  {
	"Leader dismiss", "Player leave the party", "Insufficient party members"
  };	
	
  public static readonly string[,] ft_ProMsg_12 = new string[3, 3]
  {
	{"{0}is expelled from the party", "", ""}, 
	{"The function can only be used by leader", "The player is not party member", "Expelling party member failed"},
	{"You has been expelled from the party", "", ""}
  };	
	
  public static readonly string[,] ft_ProMsg_13 = new string[3, 4]
  {
	{"", "", "", ""},
	{"", "{0}is assigned as party leader", "", ""}, 
	{"", "The function can only be used by leader", "The player is not party member", "The player is offline"}
  };
	
  public static readonly string CongratulateLevelUp = "恭喜{0}等級提升！";
  public static readonly string	OtherObtainGoods = "{0}獲得物品 {1}";
  public static readonly string SelfObtainGoods = "獲得 {0}";
	
  public static readonly string[,] ft_ProMsg_21 = new string[4, 4]
  {
	{"", "", "", ""},
	{"Solely obtain", "Equally distribute", "", ""},
	{"Solely obtain", "Obtain in turns", "Distribute with need", ""}, 
	{"Blue or better", "Purple or better", "Golden or better", ""}

  };	
  
  public static readonly string MemberInfoHint = "職業: {0}\n場景: {1}\nHP : {2} / {3}\nMP : {4} / {5}";

	public static readonly string HideTeamUI = "隱藏組隊介面";
	public static readonly string OpenTeamSetting = "開啟組隊設定";
	public static readonly string ExpandTeamUI = "展開組隊介面";
	
  public static readonly string WaitForReborn = "等待重生中";
  public static readonly string CDForReborn = "重生倒數：{0}秒";
	
  public static readonly string MemberSelectGreedOrNot = "{0}選擇 {1}：{2}";
	
  public static readonly string ThrowPoints = "{0}擲出點數{1}";
  public static readonly string PointsThrow = "{0} - {1}由{2}擲出點數{3}";
  public static readonly string WinPrizes = "{0} 贏得了：{1}";
  public static readonly string DiceResults = "{0} 擲骰結果：{1}";
	
  public static readonly string InPKCantFTeam = "聖光鬥技排序中，請離開排序後再進行組隊";
#endregion 回傳組隊錯誤代碼		

#region SkillStarGemFM
/// <summary>
	/// 0 "找到自我天命的亞特英雄，將領悟星盤之力"
	/// 1 "未通過成年禮前，無法喚醒星石"
	/// </summary>
	public static readonly string[] Str_Skill_Hint = new string[]
	{
		"找到自我天命的亞特英雄，將領悟星盤之力",	//0
		"未通過成年禮前，無法喚醒星石",				//1
		"目前經驗值：{0}",
		"找到自我天命的亞特英雄，將領悟星盤之力",	//0
		"未通過成年禮前，無法喚醒星石",				//1
		"目前經驗值：{0}",
		"%y喚醒此星石需求之等級",
		"%y喚醒此星石需消耗之EXP",
		"%y喚醒此星石需消耗之銀幣",
		"%y星塵碎片%w：|有機率在%r打倒怪物%w後取得，或者完成%r勇者評定%w後取得。",
		"[[[align=center]]]可喚醒星石：|{0}",
		"※最多可安裝6招主星技能",
		"死亡中無法學習技能",
	};
  public static readonly string[] Str_SkillAwakeResult = new string[]
  {
    "Wake Complete",
    "Star Gem info error",
    "The Star Gem had been woken",
    "No vacancy to learn it",
    "Insufficient silver",
    "等級不足",
    "Insufficient EXP",
    "Insufficient Item",
    "Lack of prerequisite skill",
    "Wake failed",
    "Upgrade info incorrect",
  };

  public static readonly string[] SkillFM_Title = new string[2] { "Astrolabe", "Skill" };
  public static readonly string[] SkillFM_SubTitle = new string[4]{"Main Star","Planet","Satellite","Locked"};
  public static readonly string[] SkillFM_Str = new string[19]{
      "Astrolabe",
      "Star Gem",
      "Main Star",
      "Planet",
      "Satellite",
      "Track",
      "Lock",
      "Star Dust",
      "Wake",
      "Upgrade",
      "Cepheus",
      "Cassiopeia",
      "Pegasus",
      "Andromeda",
      "Lacerta",
      "Auriga",
      "Triangulum",
      "Perseus",
      "Cetus",
  };
  public static readonly string[] SkillFM_SkillType = new string[3]{
    "Main Star Skills","Planet Skills","Satellite Skills"
  };

  public static readonly string[] SkillFM_Satellites = new string[6]{
    "Attack Power","Magic Damage","Healing Amount","Defense","HP","MP",
  };
  public static readonly string[] SkillFM_SkillState = new string[2]{
    "Woken","Not Woken",
  };
#endregion
#region SkillHint
  public static readonly string[] Str_SkillValueFormat = new string[10]{
    "cost：{0:###0}MP",
    "cost：{0:###0}%MP",
    "cast time：{0:#0.00}sec",
    "cool down：{0:#0.00}sec",
    "range：{0:####0}",
	"%r攻擊強度加成%w：{0}%",
	"%b法術傷害加成%w：{0}%",
	"%g治療量加成%w：{0}%"
	"被動技能",
	"星雲演化說明：",
  };
#endregion
#region 頭圖功能 字串
  public static readonly string[] Str_TargetHeadFormF1 = new string[6] { "Equipments", "Invite to Party", "Personal chat", "Add as Friends", "Invite getting on mount", "", };
  public static readonly string[] Str_LeaveMount = new string[3]{"Invite mount", "Leave mount", "Remove passenger"};
  public static readonly string[] Str_TargetHeadFormF2 = new string[5] { "Monster info",	"Falling Items",	"",	"",	"",};
  public static string[] Str_Element =new string[7]
  {
    "全屬性",  //元素種類起始0
    "地屬性",
    "水屬性",
    "火屬性",
    "風屬性",
    "光屬性",
    "闇屬性"
  };
#endregion

#region
	public static string DangerScene = "[[[align=center]]][[[size=18]]]%d{0}";
	public static string PeaceScene  = "[[[align=center]]][[[size=18]]]%a{0}";
#endregion

#region　儲值介面
	public static string StoredValueTitle = "儲值";
	public static string CurrentlyGold = "[[[align=right]]][[[size=16]]][[[color=255,227,95]]]目前金幣 : {0}";
	public static string NoticeContent = "[[[align=left]]][[[size=16]]][[[color=168,241,129]]]公告事項";
	public static string NoticeContext = "[[[align=left]]][[[size=15]]][[[color=247,243,229]]]1、The point you purchased called \"Gold\" in Atlantis|2、Items in Mall can only be baught by \"Gold\"|3、社群帳號購點時，請先輸入天空龍平台之『會|	 員帳號』";

	public static string StoredCardEnterAcc = "Please input the Card Number: ";
	public static string StoredCardEnterPas = "Plaese  input the Card Password: ";
	public static string StoredConfirm = "Sure";
	public static string OnlinePurchasePoint = " Purchase Now";
	public static string CurrencyConversion  = "龍幣轉換";

	//龍幣轉換
	public static string DCConversionTitle = "龍幣轉換";
	public static string CurrentDragonCoin = "[[[align=center]]][[[size=14]]][[[color=255,227,95]]]目前龍幣 : {0}";
	public static string ConversionContext = "[[[align=center]]][[[size=14]]]%w在下方欄位輸入點數，可以把天空|龍會員裡的點數快速轉為金幣";
	public static string WantDC = "要換多少龍幣 : ";
	public static string CanCNC = "可換多少金幣 : {0}";
	public static string Change = "轉 換";
	public static string DragonIsNotEnough = "龍幣不足";
	public static string EnterIncorrect = "輸入不正確";

	//線上購點
	public static string OnlinePurchaseTitle = "線上購點";
	public static string Buy = "購 買";
	
	//線上購卡
	public static string ATMStore = "線上ATM儲值";
	public static string BuyMyCard = "MyCard線上購卡";
	public static string Placard = "線上購點超優惠，400點特價370，\n1000點特價860，1500點特價只要\n1200 !";
	public static string YourLoginAcc = "您的遊戲帳號：\n";
	public static string CopyAccount = "複製帳號";
	public static string CopyAccSuccess = "複製帳號成功";
#endregion

#region 副本資訊介面
	public static string InstanceUITitle = "副 本";
	public static string InstamceName  = "[[[size=15]]]副本名稱 : {0}";
	public static string Difficulty    = "[[[size=15]]]難度 :%s {0}";
	public static string TeamMember    = "隊伍成員";
	public static string[] DifficultyStr = new string[4]
	{
		"",
		"一般", "惡夢", "地獄"
	};
	public static string nRequiredLevel	 = "[[[size=15]]]適合等級 :%e {0}";//不適合
	public static string RequiredLevel	 = "[[[size=15]]]適合等級 :%s {0}";
	public static string nSugIntensity	 = "[[[size=15]]]建議裝備強度 :%e {0}";
	public static string SugIntensity	 = "[[[size=15]]]建議裝備強度 :%s {0}";
	public static string nRequiredMemNum = "[[[size=15 align==left]]]需求人數 :%e {0}";//人數不足
	public static string RequiredMemNum  = "[[[size=15 align==left]]]需求人數 :%s {0}";
	//public static string InstInformation = "[[[size=15]]]副本名稱 : {0}難度 : {1}	適合等級 : {2}	建議裝備強度 : {3}";
	public static string Difficulty_Cost = "[[[align=center color=255,231,174]]]難度 / 花費銀幣";
	public static string Normal    = "[[[size=16]]]一般";
	public static string Nightmare = "[[[size=16]]]惡夢";
	public static string Hell 	   = "[[[size=16]]]地獄";
	public static string CostTime  = "[[[size=16]]]%s{0}";
	public static string Assign    = "指派";
	public static string EnterCC   = "進入副本";
	public static string Cancle    = "取消";
	public static string CCExplain = "副本說明";
	public static string OffLined = "(已離線)";
	//指派介面
	public static string AssignSub = "指派部下";
	public static string AllowedAssign = "可指派";
	public static string NoSubordinate = "目前並無可指派的部下，您可透過騎士團﹝快捷鍵O﹞\n來新增好友成為部下，陪伴您一同勇闖副本！";
	public static string Return    = "返回";
	public static string WarTeam   = "戰隊";
	public static string ODStr = "攻擊強度 : {0}|法術傷害 : {1}|防禦力 : {2}";
	public static string TeamIsFull = "隊伍已滿";
	public static string TeamIsEmpty= "隊伍中已無部下";
	//進入副本確認
	public static string CCInformation = "副本資訊";
	public static string CancleEnter = "取消進入";
	public static string CDRemind = "[[[size=14 align==left]]]%u請按下按鈕進入副本,倒數%e{0}%u秒";
	public static string CCInfor  = "[[[size=14 align==left]]]副本難度 　　: {0}|經驗值、金錢 :%s {1}|%w掉落物品　　 :%s {2}|[[[246,150,37]]](副本中隊長可更改獎勵設定)";
	public static string WaitForCResult = "等待確認中...";

	//協定訊息[設定多人戰隊副本結果]
	public static string[] SetCCResult =
	{
		"設定成功",//成功
		"銀幣不足",//1
		"已離線",//2
		"等級不符",//3
		"設定太頻繁",//4
		"組隊伍失敗",//5
		"尚未領取獎勵，請先領取獎勵後再進行",//6.隊伍中{0}尚未領取獎勵，請先領取獎勵後再進行
		"尚未通過關卡，無法挑戰此戰記",//7.隊XXX尚未通過關卡，無法挑戰此戰記
		"忙碌中無法進入",//8
		"所在場景無法出去",//9
		"動標已滿",//10
		"死亡中",//11
	};
	public static string DeathToCancel = "因隊伍成員{0}死亡，取消進入副本";
	
	public static string CancelToEnterCC = "{0}取消進入副本";
	public static string SubLevelIsInsufficient = "部下等級不足，無法勝任此副本";
	public static string MemberNotEnough = "人數不足，無法進入副本";
	public static string IsNotCaptain = "請由隊長選擇進入副本";
	public static string LevelNotMatch = SetCCResult[3] + "，無法進入副本";
	
	public static string ExpReward = "{0} / {1}";
	public static string DailyWarteamReward = "每日戰隊獎勵";
	public static string WarTeamExperience  = "戰隊經驗：";
	public static string Accept  = "確 認";
#endregion

#region 事件訊息字串
  public const string EventMSG001="您的銀幣已達上限，請清出銀幣空間後，再至提示櫃開啟此介面領取獎勵";
  public const string EventMSG002="恭喜您，獲得今天的上線獎勵";
  public const string EventMSG003="目前金幣遊樂場已達上限，請稍等再進入，請至提示櫃開啟此介面進入金幣遊樂場";
  public const string EventMSG004="恭喜您，獲得今天的上線獎勵+{0}銀幣";
#endregion
#region MapGuidePathFinding
	public static string MapGuideNoFound = "無法提供尋路功能！";
	public static string MapGuideCloseTo = "您已達目標處！";
	public static string MapGuideCostomTarget = "自定目標";
    public static string MapCantOpen = "此場景無法使用中地圖";
#endregion
#region 伺服器選單
    public const string ServerList_GSName = "分流{0:00}";
#endregion
#region 場景UI用
	public const string Str_FollowTime = "剩餘時間：{0}分";
#endregion

#region 昇級資訊用
	public static readonly string[] Str_LVUPFM_SubTitle = new string[]{
		"獲得之能力",
		"可喚醒之星石",
		"升級贈禮",
	};
#endregion

	public const string Totem_Name_Format = "{0}的{1}";

	public static readonly string[] Str_BuffSet_Result = new string[3]
	{
		"中止成功",
		"重啟成功",
		"戰鬥狀態下無法更改設定"
	};

#region 分區介面
	public static readonly string[] Str_Zone_Btn_Text = new string[2]
	{
		"OK",
		"cancel"
	};
	public const string Str_Zone_Same = "Please select another Zone";
	public const string Str_Zone_Title = "Zone Select";
	public const string Str_Zone_Select_Text = "Zone {0}";
	public const string Str_Zone_Info = "[[[size={0} color=206,236,114 lineGap={1}]]]1. To avoid crowded scene, you can change Zones to find a Zone with fewer people. And there is no negtive effects.|2. When you can't see your friend in the same scene, please make sure you are in the same Zone.";
#endregion
	
#region 介面消費提示訊息
	public const string Cancel = "取 消";
	public static readonly string[][] TipMsg = new string[][]
	{
		new string[] {"能量不夠",  "[[[align=center]]]哦哦~能量不夠了|是否前往轉換?"},
		new string[] {"權力點不夠","[[[align=center]]]哦哦~權力點不夠了|是否前往轉換?"},
		new string[] {"金幣不夠",  "[[[align=center]]]哦哦~金幣不夠了|是否前往購買?"},
		new string[] {"訓練時間不足",  "[[[align=center]]]哦哦~訓練時間不夠了|是否前往領取?"},
	};
#endregion
	
#region 遠古成長秘術
	public const string AncientGrowthMystic = "遠古成長秘術";
	public const string GrowthExplain = "亞特子民代代傳承的成長秘術，在離線休息時開始凝聚\n成長元素，上線後提供完整補充。";
	public const string CumulativeTime = "目前累積可兌換時間：{0}小時{1}分鐘";
	public const string ExchangeObtExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：{0}({1}%)";
//  public const string ReachedMaxiExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：[[[color=255,0,0]]]已達上限(請先消秏經驗值再領取)";
	public const string ReachedMaxiExp = "[[[size=15]]][[[color=195,230,90]]]兌換可獲得經驗值：[[[color=255,0,0]]]已達本階段兌換上限";
	public const string GoThroughAfter = "(通過{0}副本後將可繼續兌換)";
	public const string ConversionTime = "兌換時間：";
	public const string Exchange = "兌 換";
	public const string ExchangeExplain = "[[[size=14]]]以離線時間換算成經驗值，單次離線每達15分鐘計算1次。|若逾15分鐘未達30分鐘，則以15分鐘計算。|單日最多8小時(每日中午12點重計)，最高累計8小時。|單次兌換時間以小時為單位。";

	public const string FreeExchange   = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] 免費";
	public const string GoldConvertion = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] {0}金幣";
	public const string VIPsConvertion = "[[[size=15]]]兌換消耗：[[[size=13]]][[[color=245,150,40]]] {0}金幣[[[color=255,0,0]]](VIP{1}星可兌換)";
	public static readonly string[] ConvertTime = new string[]{"全部兌換", "1小時", "2小時", "4小時", "12小時", "24小時"};
	
	public const string NONVIP = "[[[align=center]]][[[size=15]]]尚未達兌換條件。|前往了解VIP相關資訊？";
	public const string GoldExChangeConfirm = "[[[align=center]]][[[size=16]]]是否確認進行兌換？|[[[color=255,227,95]]](需扣除{0}金幣)";
	public const string FreeExChangeConfirm = "[[[align=center]]][[[size=16]]]是否確認進行兌換？";
	public const string ExChangeFailed = "兌換失敗！最低兌換時數(1小時)";
	
	public const string ExchangeSuccessful = "兌換成功";
	
	public static readonly string LessThanLowestGROWLVMSG = string.Format("角色需達{0}級且通過人馬禁地副本後方可開啟此功能", GLOBALCONST.LOWEST_ALLOWGROWLV);
	public static readonly string LessThanLowestGEARLVMSG = string.Format("角色需達{0}級可開啟此功能", GLOBALCONST.LOWEST_ALLOWAUTOGEAR);
	public static readonly string[] Grow_ProMsg = new string[2]
	{
		"",
		"超出經驗上限，購買失敗！"//1."兌換經驗值超越上限"
	};
#endregion
#region GUIStation
	public static readonly string[] OpenUIError = new string[]
	{
		"銀行使用中，無法開啟其他介面",
	};
#endregion
	
#region 一次性特惠介面相關
	public static readonly string ImmediatelySnappedUp = "[[[align=center]]][[[[color=255,70,0]]]%r限時優惠!! {0:00} : {1:00}|[[[color=255,204,0]]][[[underline]]]【立即搶購】";
	public static readonly string CoundDown_MS = "倒數計時 {0:00}:{1:00}";
	public static readonly string WantToBuy = "我要買";
	public static readonly string ConfirmToBuy = "[[[align=center]]]是否花費{0}金幣購買|{1}?";
	public static readonly string GiveUpDisposSpecial = "[[[algin=center]]]僅此一次機會～|你確定要%r放棄%w嗎？";
	public static readonly string BoughtAbandon= "您已購買 / 放棄過此優惠";
#endregion
#region 皇家俱樂部
	public const string RankInfoTitleStr = "排行資訊";
	public const string RankInfoText = "[[[color=243,229,232 OutlineColor=44,21,30 size=14]]]{0}";
	public const string RankRewardTitleStr = "排行獎勵";
	public const string FirstRankRewardStr = "【第1名獎勵】";
	public static readonly string [] RealRewardTypeStr = new string[]
	{
		"當日最高名次",
		"結算日名次"
	};

	public static readonly string [,] RealRewardStr = new string[,]
	{
		{
			"{0}：第1名",   // 符合第一名
			"{0}：第{1}名", // 符合其他獎勵區間
			"名次到達{0}名以內",     // 不符合任何獎勵區間
		},
		{
			"",                  // 符合第一名
			"【第{0}名獎勵】",    // 符合其他獎勵區間
			"可領取神秘獎勵喔！"   // 不符合任何獎勵區間
		}
	};
	public static readonly string [] GetRewardBtnStr = new string[] // 按鈕文字
	{
		"領取獎勵",
		"已領取",
		"資格不符"
	};
	public const string LeaderBoardName = "{0}入圍者";
	public const string LeaderBoardHint = "{0}：{1}|[[[color=235,97,0]]]{2}：{3}|[[[color=255,236,147]]]{4}：{5}|[[[color=178,236,10]]]{6}：{7}||[[[color=255,255,255]]]左鍵點擊，可查看玩家資訊";
	public static readonly string[] RankType = new string
	{
		"全部排名",
		"好友排名"
	};
	public const string RankDataInfo = "排名";
	public static readonly string [] RankCongratulationTypeText = new string[] // 祝賀文字在兩種type的變化
	{
		"你目前名次",
		"在好友排名"
	};
	public static readonly string [] RankCongratulationText = new string[] // 祝賀文字
	{
		"恭喜{0}為第1名，榮耀的滋味不是人人嚐得起！", // 第1名
		"恭喜{0}為第2名，榮耀之光就在你的一尺前方！", // 第2名
		"恭喜{0}為第3名，因為你！前2名心裡開始緊張了！", // 第3名
		"恭喜{0}為第4名，無需畏懼，你已證明自己的實力！", // 第4名
		"恭喜{0}為第5名，國家代表隊歡迎你的加入！", // 第5名
		"恭喜{0}為第6名，還差一名，就能讓所有人都看到你！", // 第6名
		"恭喜{0}為第7名，LuckySeven還能帶你衝到第幾名呢？", // 第7名
		"恭喜{0}為第8名，先鋒們，請注意！你們即將被他超越！", // 第8名
		"恭喜{0}為第9名，你又往前一小步，後面朋友一大哭！", // 第9名
		"恭喜{0}為第10名，有堅毅實力，才能對抗前9強的挑戰！", // 第10名
		"恭喜{0}為第{1}名，請繼續往前追求榮耀！", // 第11~1000名
		"可惜！你目前尚未進榜。請繼續努力挑戰！", // 未進榜
	};
	public static readonly string [] RankMessageText = new string []
	{
		"",
		"排行榜正在排行中,請稍候...", // Server傳來訊息代號1
	};
#endregion
	
#region 蒸汽齒輪相關
	public static readonly string QuestionCombatGear = "點擊START啟動蒸汽齒輪|啟動後會自動搜尋怪物進行攻擊|並依序自動施展安裝在快捷鍵1~6的技能|點擊STOP後關閉蒸汽齒輪";
	public static readonly string SceneNotAllowSteamGear = "此場景不可使用蒸汽齒輪！";
	public static readonly string SceneNotComplete = "場景尚未準備好！";
	public static readonly string YouaAreDead = "你已經死了！";
	public static readonly string RiddingToCantSG = "騎乘中不可使用蒸汽齒輪！";
	public static readonly string InEventToCant = "事件中無法開啟此功能";
	public static readonly string WaterIsNotEnough = "你身上的紅水或藍水不足，確定要啟動蒸汽齒輪？";
#endregion
	
#region 港版專用(？)佔位
  public static readonly string HKS_00001 = "";
#endregion
}
#endif
