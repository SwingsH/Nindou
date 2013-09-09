//#define _LANGUAGE_ENG //英文設定
#define _LANGUAGE_CHT_TW //繁中[台灣]設定
//#define _LANGUAGE_CHT_HK //繁中[香港]設定

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GLOBALCONST
{
	public const string GAME_MAIN_VERSION = "2";
	public const string GAME_SERIAL_NUM = "249";

	public const int USUAL_VERSION = 0; //通用版
	public const int TAIWAN_VERSION = 1; //台版
	public const int CHINA_VERSION = 2; //陸版
	
#if _LANGUAGE_CHT_TW
	public const string GAME_SUB_VERSION = "0";
	public const int IP_VERSION_NUM = 76; //Server 版號
	public const int CURRENT_VERSION = CHINA_VERSION; //區版
	public const ReleaseLanguage RELEASE = ReleaseLanguage.CHINESE_TRADITIONAL_TW;
#elif _LANGUAGE_ENG
	public const string GAME_SUB_VERSION = "1";
	public const int IP_VERSION_NUM = 76; //Server 版號
	public const int CURRENT_VERSION = CHINA_VERSION; //區版
	public const ReleaseLanguage RELEASE = ReleaseLanguage.ENGLISH;
#elif _LANGUAGE_CHT_HK
	public const string GAME_SUB_VERSION = "2";
	public const int IP_VERSION_NUM = 77; //Server 版號
	public const int CURRENT_VERSION = CHINA_VERSION; //區版
	public const ReleaseLanguage RELEASE = ReleaseLanguage.CHINESE_TRADITIONAL_HK;
#endif
	
	public const int IP_GAMEPORT        = 6160;

    public const string GAME_TITLE = "Atlantis";
	public const int GAME_DEMO 	= 1; //DEMO版
	public const int GAME_CB 	= 2; //CB版
	public const int GAME_FST 	= 3; //1版
	public const int GAME_VERSION = GAME_CB; //區版
    public const int GAME_RESOLUTION_WIDTH = 1024;
    public const int GAME_RESOLUTION_HEIGHT = 768;
	
    public const string CACHELICENCE_NAME = "AT"; //快取註冊名
    public const string CACHELICENCE_DOMAIN = "chinesegamer.net/"; //快取網域
    public const string CACHELICENCE_SIGNATURE = "371fd01363dddce5d1bc63c76ecc146039cd08d58c5e7adc5b65eccaf1df1710f7a7a6423b6cbb718734e51909e374aa5a78c36ebf3c81a9d229d755819d0b6d747c230e38d054b876a4f819bf303dcc9bf109de105c61234d6fedef32b568de330f2a0febcff1c3dfd12ddabfe46adb8f0dfa650264fb55dbc00a259b89f26f";
    public const long CACHELICENCE_SIZE = 6442450944;
	public const int GAME_TYPE = 10;
    public const int VERSION_EMPTY = -1;
	public const int VERSION_FILE_NOT_EXIST = -999;
    
    public const string PROJECT_COMPANY_NAME = "chinesegamer I.N.C";
	public const string PROJECT_PRODUCT_NAME = "AT Online";

	public const int CONTROLPLAYER_MAX = 1;  //同時間最多可操控人數

// 平台差異區	
#if (UNITY_EDITOR || UNITY_WEBPLAYER)
    public const string PATH_EDITOR_SHARES = "/192.168.33.89/EditorShares/";
#elif UNITY_STANDALONE_WIN	
    public const string PATH_EDITOR_SHARES = "";
#endif

	public const string DIR_ASSETS              = "Assets/";
    public const string DIR_EDITOR_OUTPUT       = GAME_TITLE + "Output/";
    public const string DIR_EDITOR_ASSETBUNDLE_OUTPUT = DIR_EDITOR_OUTPUT + DIR_ASSETBUNDLE;
	public const string DIR_ASSETBUNDLE = "assetbundles/";

#if _LANGUAGE_CHT_TW || _LANGUAGE_CHT_HK
	public const string BUNDLE_LANGUAGE_POSTFIX = ""; //中文版,特定檔案類型, bundle 檔名後綴			
#elif _LANGUAGE_ENG
    public const string DIR_LANGUAGE_ASSETBUNDLE         = "assetbundles_eng/";
    public const string BUNDLE_LANGUAGE_POSTFIX = "_eng"; //英文版,特定檔案類型, bundle 檔名後綴			
#endif

	public const string DIR_XML                             = "xml/";
	public const string DIR_JSON                            = "json/";
	public const string DIR_ASSETBUNDLE_SCENE               = DIR_ASSETBUNDLE + "Scene/";  //場景檔
	public const string DIR_ASSETBUNDLE_WEAPONS             = DIR_ASSETBUNDLE + "Weapons/";  //武器模型
	public const string DIR_ASSETBUNDLE_WEAPONS_MATERIAL    = DIR_ASSETBUNDLE_WEAPONS + "Materials/";  //武器材質
	public const string DIR_ASSETBUNDLE_NPC                 = DIR_ASSETBUNDLE + "NPC/";
	public const string DIR_ASSETBUNDLE_NPC_MESH            = DIR_ASSETBUNDLE_NPC + "Meshs/";  //NPC模型
	public const string DIR_ASSETBUNDLE_NPC_MATERIAL        = DIR_ASSETBUNDLE_NPC + "Materials/";  //NPC材質
	public const string DIR_ASSETBUNDLE_NPC_ANIMATION       = DIR_ASSETBUNDLE_NPC + "Animations/";  //NPC動作
	public const string DIR_ASSETBUNDLE_PLAYER              = DIR_ASSETBUNDLE + "Player/";
	public const string DIR_ASSETBUNDLE_ICON                = DIR_ASSETBUNDLE + "ICON/";
	public const string DIR_ASSETBUNDLE_PLAYER_MESH         = DIR_ASSETBUNDLE_PLAYER + "Meshs/";  //角色模型
	public const string DIR_ASSETBUNDLE_PLAYER_ANIMATION    = DIR_ASSETBUNDLE_PLAYER + "Animations/";  //角色動作
	public const string DIR_ASSETBUNDLE_PLAYER_MATERIAL     = DIR_ASSETBUNDLE_PLAYER + "Materials/";  //角色材質
    public const string DIR_ASSETBUNDLE_PARTICLE            = DIR_ASSETBUNDLE + "Particles/";  //光影檔
	public const string DIR_ASSETBUNDLE_NOTIFYDATA          = DIR_ASSETBUNDLE + "NotifyData/";
    public const string DIR_ASSETBUNDLE_DEVICENPC 			= DIR_ASSETBUNDLE + "NPC/";//備註 放在NPC下
    public const string DIR_ASSETBUNDLE_MAP                 = DIR_ASSETBUNDLE + "MAP/";  //小地圖
	public const string DIR_ASSETBUNDLE_TOK                 = DIR_ASSETBUNDLE + "TOK/";  //知識之塔
	public const string DIR_ASSETBUNDLE_SOUND = DIR_ASSETBUNDLE + "Sound/";  //音效
	public const string DIR_ASSETBUNDLE_ANIMATION			= DIR_ASSETBUNDLE + "Animations/";
	public const string DIR_ASSETBUNDLE_BASICANMS		= DIR_ASSETBUNDLE_ANIMATION + "BasicAnimations/";
	public const string DIR_ASSETBUNDLE_FONT						= DIR_ASSETBUNDLE + "Font/";    //字型的輸出資料夾
	public const string DIR_ASSETBUNDLE_MOVIE					= DIR_ASSETBUNDLE + "Movie/";    //影片(MovieTexture)

	/// 依照 Language 會有路徑差異的資料類型
#if _LANGUAGE_CHT_TW || _LANGUAGE_CHT_HK
	public const string DIR_ASSETBUNDLE_GUI						= DIR_ASSETBUNDLE + "gui/";
	public const string DIR_ASSETBUNDLE_XML_EVENTDATA			= DIR_ASSETBUNDLE + "XML/"; //事件資料(XML), 與 XML Data 用同一個 path, 之後要與 GameData 分不同資料夾
	public const string DIR_ASSETBUNDLE_JSON_EVENTDATA			= DIR_ASSETBUNDLE + "JSON/"; //事件資料(XML), 與 XML Data 用同一個 path, 之後要與 GameData 分不同資料夾
	public const string DIR_ASSETBUNDLE_BINARY_EVENTDATA		= DIR_ASSETBUNDLE + "BINARY/";//事件資料(BINARY), 與 XML Data 用同一個 path, 之後要與 GameData 分不同資料夾
	public const string DIR_ASSETBUNDLE_XML_GAMEDATA			= DIR_ASSETBUNDLE + "XML/";  //遊戲資料(XML), 
	public const string DIR_ASSETBUNDLE_JSON_GAMEDATA			= DIR_ASSETBUNDLE + "JSON/";  //遊戲資料(JSON),
	public const string DIR_ASSETBUNDLE_BINARY_GAMEDATA			= DIR_ASSETBUNDLE + "BINARY/";  //遊戲資料(BINARY),
	public const string DIR_ASSETBUNDLE_XML_COUNCIL				= DIR_ASSETBUNDLE + "XML/";  //議會(XML)
	public const string DIR_ASSETBUNDLE_JSON_COUNCIL			= DIR_ASSETBUNDLE + "JSON/";  //議會(JSON)
	public const string DIR_ASSETBUNDLE_BINARY_COUNCIL			= DIR_ASSETBUNDLE + "BINARY/";  //議會(BINARY)
#elif _LANGUAGE_ENG
	public const string DIR_ASSETBUNDLE_GUI						= DIR_LANGUAGE_ASSETBUNDLE + "gui/";
	public const string DIR_ASSETBUNDLE_XML_EVENTDATA			= DIR_LANGUAGE_ASSETBUNDLE + "XML/"; //事件資料(XML), 與 XML Data 用同一個 path, 之後要與 GameData 分不同資料夾
	public const string DIR_ASSETBUNDLE_JSON_EVENTDATA			= DIR_LANGUAGE_ASSETBUNDLE + "JSON/"; //事件資料(XML), 與 XML Data 用同一個 path, 之後要與 GameData 分不同資料夾
	public const string DIR_ASSETBUNDLE_XML_GAMEDATA			= DIR_LANGUAGE_ASSETBUNDLE + "XML/";  //遊戲資料(XML), 
	public const string DIR_ASSETBUNDLE_JSON_GAMEDATA			= DIR_LANGUAGE_ASSETBUNDLE + "JSON/";  //遊戲資料(JSON),
	public const string DIR_ASSETBUNDLE_XML_COUNCIL				= DIR_LANGUAGE_ASSETBUNDLE + "XML/";  //議會
	public const string DIR_ASSETBUNDLE_JSON_COUNCIL			= DIR_LANGUAGE_ASSETBUNDLE + "JSON/";  //議會
#endif

	public const string DIR_DATAPATH_ASSETBUNDLE_XML = GLOBALCONST.DIR_ASSETS + GLOBALCONST.DIR_ASSETBUNDLE_XML_GAMEDATA;
	public const string DIR_DATAPATH_ASSETBUNDLE_JSON = GLOBALCONST.DIR_ASSETS + GLOBALCONST.DIR_ASSETBUNDLE_JSON_GAMEDATA;
	public const string DIR_DATAPATH_ASSETBUNDLE_BINARY = GLOBALCONST.DIR_ASSETS + GLOBALCONST.DIR_ASSETBUNDLE_BINARY_GAMEDATA;

	public const string DIR_DATA                = "data/";  //遊戲資料
	public const string DIR_MAP_DATA            = DIR_DATA + "Maps/";
    public const string DIR_WEBPAGE             = "fbphpsdk/fbhost/";
	public const string DIR_SCENE               = "Maps/";
	public const string DIR_WEAPON              = "Weapons/";
	public const string DIR_NPC                 = "NPC/";
	public const string DIR_PARTICLE            = "Particle/";
	public const string DIR_PLAYER              = "Player/";
	public const string DIR_ANIMATION           = "Motion/";
	public const string DIR_NOTIFY           	= "NotifyData/";
    public const string DIR_DEVICENPC           = "Device/";
    public const string DIR_MIDMAP              = "Maps/MidMap/";
	public const string DIR_SOUND = "Sound/";
	
	public const string DIR_UI                  = "/__UI/";
	public const string DIR_FONT                = "/Resources/Font/";   //字型檔案的來源路徑
    public const string DIR_DEBUGLOG            = "DebugLog/"; //Editor下開發用  Log

    public const int DL_PRIORITY_FIRST = 1;
    public const int DL_PRIORITY_SECOND = 2;
    public const int DL_PRIORITY_THIRD = 3;
	public const int DL_PRIORITY_FOUR = 4;
	public const int DL_PRIORITY_FIVE = 5;

	public const string DATA_BASIC_ANIME        = "anime";

	public const string EXT_ASSETBUNDLE = ".unity3d";
	public const string EXT_SCENE       = ".unity";
	public const string EXT_FBX         = ".fbx";
	public const string EXT_PREFAB      = ".prefab";
	public const string EXT_UI          = ".prefab"; //目前方案ui會由art做成.prefab檔案
	public const string EXT_ASSET       = ".asset";
	public const string EXT_BYTES       = ".bytes";
	public const string EXT_XMLDATA     = ".xml";
	public const string EXT_JSONDATA     = ".json";
	public const string EXT_FONT        = ".ttf";   //字型檔案的副檔名
    public const string EXT_WEBPAGE     = ".php";   //網頁程式檔的副檔名

	public const string FONT_BUNDLE = UIConfig.DEFAULT_FONT;
	
	public const string XML_GAME_CONFIG		    = "config.xml";
    public const string XML_BUNDLE_RUNINFO      = "BundleInfoRuntime.xml";
	public const string XML_BUNDLE_EDITINFO     = "BundleInfoEdit.xml";
    public const string XML_PRE_DOWNLOAD        = "BundlePreDownload.xml"; //預先下載清單
	public const string XML_SERVERLIST          = "serverconfig.xml"; // 遊戲伺服器 game server 清單
    public const string XML_FILESERVERLIST      = "fileHostList.xml"; // 檔案伺服器 file server 清單
	public const string XML_CHARACTER_M         = "characterSettings_m.xml";
	public const string XML_CHARACTER_G         = "characterSettings_g.xml";
	public const string XML_CHARACTER_B         = "characterSettings_b.xml";
	public const string XML_CHARACTER_S         = "characterSettings_s.xml";
	public const string XML_NPCCHARACTER        = "npcCharacterSettings.xml";
	//public const string XML_FIXED_FB            = "fixed_fbresult.xml";
	
	public const string JSON_GAME_CONFIG		= "config.json";
	public const string JSON_BUNDLE_RUNINFO     = "BundleInfoRuntime.json";
	public const string JSON_BUNDLE_EDITINFO    = "BundleInfoEdit.json";
	public const string JSON_PRE_DOWNLOAD       = "BundlePreDownload.json";//預先下載清單
	public const string JSON_SERVERLIST         = "serverconfig.json"; // 遊戲伺服器 game server 清單
	public const string JSON_FILESERVERLIST     = "fileHostList.json"; // 檔案伺服器 file server 清單
	public const string JSON_CHARACTER_M        = "characterSettings_m.json";
	public const string JSON_CHARACTER_G        = "characterSettings_g.json";
	public const string JSON_CHARACTER_B        = "characterSettings_b.json";
	public const string JSON_CHARACTER_S        = "characterSettings_s.json";
	public const string JSON_NPCCHARACTER       = "npcCharacterSettings.json";
	public const string JSON_FIXED_FB           = "fixed_fbresult.json";
	
	public const string XML_PREFIX = "XML_";
	public const string JSON_PREFIX = "JSON_";
	public const string BINARY_PREFIX = "BINARY_";
	
#if UNITY_STANDALONE_WIN
	public const string MOV_ATLANTIS_OPEN = "PrefabAtlantisOP.unity3d"; // 開頭動畫 MovieTexture
#endif
	
	public const string DATA_NPC_SingleDat   	= "NPC_SingleDat.unity3d";			//單一NPC 
	public const string DATA_NPC_CrowdDat    	= "NPC_CrowdDat.unity3d";			//一群NPC
	public const string DATA_NPC_EventDat    	= "NPC_EventDat.unity3d";			//NPC事件資料
	public const string DATA_NPC_RoamDat     	= "NPC_RoamDat.unity3d";			//漫遊NPC
	public const string DATA_NPC_DeviceDatClient = "NPC_DeviceDatClient.unity3d";	//機關NPC
	public const string DATA_KRS_K9 				= "KRS_K9.unity3d";					//種植檔的索引檔
	public const string DATA_NPC_Talk			= "NPC_Talk.unity3d";				//NPC發話
	public const string DATA_Evt					= "Evt.unity3d";					//事件結構
	public const string DATA_Evt_Trig			= "Evt_Trig.unity3d";				//事件觸發條件
	public const string DATA_Evt_SEA				= "Evt_SEA.unity3d";				//事件推演資料
	public const string DATA_Plot_DBook			= "Plot_DBook.unity3d";				//個人動標(任務)
	public const string	DATA_Plot_DStep			= "Plot_DStep.unity3d";				//任務步驟
	public const string DATA_Plot_DNPC			= "Plot_DNPC.unity3d";				//任務NPC資料
	public const string DATA_Plot_DPrize			= "Plot_DPrize.unity3d";			//任務獎勵
	public const string DATA_Plot_DPart			= "Plot_DPart.unity3d";				//任務細節
	public const string DATA_EventMark			= "EventMark.unity3d";				//任務追蹤1
	public const string DATA_EventMark1			= "EventMark1.unity3d";				//任務追蹤1
	public const string DATA_EventMark2			= "EventMark2.unity3d";				//任務追蹤2
	public const string DATA_Plot_OrgDBook		= "Plot_OrgDBook.unity3d";			//幫會任務---目前無幫會
	public const string DATA_Plot_OrgDStep		= "Plot_OrgDStep.unity3d";
	public const string DATA_Plot_OrgDNPC		= "Plot_OrgDNPC.unity3d";
	public const string DATA_Plot_OrgDPrize		= "Plot_OrgDPrize.unity3d";
	public const string DATA_Plot_AllyDBook		= "Plot_AllyDBook.unity3d";			//聯會任務---目前無聯會
	public const string DATA_Plot_AllyDStep		= "Plot_AllyDStep.unity3d";
	public const string DATA_Plot_AllyDNPC		= "Plot_AllyDNPC.unity3d";
	public const string DATA_Plot_AllyDPrize		= "Plot_AllyDPrize.unity3d";
	public const string DATA_ShareEvent			= "ShareEvent.unity3d";				//分享任務
	public const string DATA_Block_Client		= "Block_Client.unity3d";			//地塊資訊
	public const string DATA_Talk1				= "Talk1.unity3d";					//對話索引1(該事件的所有對話)
	public const string DATA_Talk2				= "Talk2.unity3d";					//對話索引2(每一段對話)
	public const string DATA_Str1				= "Str1.unity3d";					//一組字
	public const string DATA_StrAll				= "StrAll.unity3d";					//所有字串
	public const string DATA_NPC_CLIENT			= "NPC_CLIENT.unity3d";				//CLIENT所有NPC資料
	public const string DATA_Item				= "Object.unity3d";					//CLIENT所有物品資料
	public const string DATA_ItemKindName		= "kind.unity3d";					//物品名稱
	public const string DATA_ItemInfo			= "ItemInfo.unity3d";				//物品說明
	public const string DATA_EquipDecompose      = "decompose.unity3d";				//裝備拆裝
	public const string DATA_EquipStarEff		= "star_effect.unity3d";			//裝備星石效果
	public const string DATA_EquipSuit			= "SetInfo.unity3d";				//裝備套裝
	public const string DATA_FuDai				= "fudai.unity3d";					//福袋物品
	public const string DATA_FuCard				= "ExchangeCard.unity3d";			//兌換卡物品
	public const string DATA_Alchemy				= "Cauldron_item.unity3d";			//煉金釜資料表
	public const string DATA_GodArtifact			= "goditem.unity3d";				//神器
	public const string DATA_NPC_Name			= "NPC_NAME.unity3d";				//NPC名稱
	public const string DATA_DeviceNPC_CLIENT    = "DeviceNPC.unity3d";				//機關資料
	public const string DATA_Auto_Event			= "autoevt.unity3d";				//主動任務
	public const string DATA_SKILL               = "skill.unity3d";  //技能表
	public const string DATA_WOGON2              = "wogon2.unity3d";  //技能2表
	public const string DATA_STATE               = "status.unity3d";  //狀態表
	public const string DATA_SKILLSTARGEM        = "stargem.unity3d";  //技能星石表
	public const string DATA_SKILLSATELLITES     = "satellites.unity3d";  //技能衛星能力表
	public const string DATA_NPC_SKILL_NAME		= "NPC_SKILLNAME.unity3d";//特殊npc施放技能名稱表
	public const string DATA_CHANCEDESTINY_ITEMS	= "chance.unity3d";  //機會命運獎勵表
	public const string DATA_BASIC_FACTOR		= "chain.unity3d";  //基數表
	public const string DATA_CRAFT_RECIPE		= "Craft.unity3d";  //物品合成表
	public const string DATA_SKILLEVOLUTION		= "skillevolution.unity3d";     //技能演化表
	public const string DATA_INBOX_DESCRIPTION	= "inbox.unity3d";	//收件夾說明內容表
	public const string DATA_RIDINGEFF = "ridingeff.unity3d";  //座騎掛載光影表
	public const string DATA_PLAYER_EXP          = "playerexp.unity3d";  //升級所需經驗表
	public const string DATA_STATEVALUE          = "statusvalue.unity3d";  //狀態數值表
	public const string DATA_NPCACTION           = "NPC_Action.unity3d";  //NPC動作表
	public const string DATA_NPCCHANGE           = "NPC_CHANGE.unity3d";  //NPC異變表
	public const string DATA_TOTEM = "totem.unity3d";  //圖騰表
	public const string DATA_SCENEATTR           = "SceneAttr.unity3d";  //場景限制表
    public const string DATA_MapRoute            = "MapRoute.unity3d";  //場景路徑表
	
	public const string DATA_Councils_task		= "Councils_task.unity3d";  		//議會
	public const string DATA_Councils_talk		= "Councils_talk.unity3d";  		//議會
	public const string DATA_Councils_Skill		= "councils_skill.unity3d";  		//議會
	public const string DATA_Councils_personality= "councils_personality.unity3d";	//議會

	public const string DATA_ATUI = "atui.unity3d";   //介面共存表    

	public const string DATA_RepairTable = "FixTable.unity3d";			//修理價目表
	public const string DATA_MallInfoTable = "Hang.unity3d";				//商城主頁顯示資料（夯表）
	public const string DATA_ShopItemTable = "Treasure.unity3d";		//商品表
	public const string DATA_ShopTabTable = "TreasureInfo.unity3d";//商店頁籤表
	public const string DATA_RareShopItemTable = "Rare.unity3d";		//限量商品表
	public const string DATA_TUTORIAL_EVENTS = "tips.unity3d";		//新手教學
	public const string DATA_LVUPBONUS = "atlvup.unity3d"; //升級獎勵
	public const string DATA_LVUPATTRIBUTE = "attribute.unity3d"; //升級能力
	public const string DATA_TWGLEAF = "TWGleaf.unity3d";
	public const string DATA_DELIVER = "deliver.unity3d";
	public const string DATA_BIDCAT = "BidCat.unity3d";
	public const string DATA_AUCTION = "Auction.unity3d";
	public const string DATA_RAISINGTOOL = "Raisingtool.unity3d";
	public const string DATA_DEPARTMENT = "Department.unity3d";
	public const string DATA_EMOTION = "emotion.unity3d";
	public const string DATA_TRADEINFO = "tradeinfo.unity3d";
	public const string DATA_CRYSTAL = "Crystal.unity3d";
	public const string DATA_EFFECT2 = "Effect_All2.unity3d";
	public const string DATA_STORY1 = "story1.unity3d";
	public const string DATA_STORY2 = "story2.unity3d";
	public const string DATA_ENCHANT2 = "story_enchant(e).unity3d";
	public const string DATA_SOUND = "evtsound.unity3d";
	public const string DATA_EMOTICONS = "emotional.unity3d";
	public const string DATA_TROOPLV = "councils_level.unity3d";
	public const string DATA_TITLE = "title.unity3d";
	public const string DATA_TITLE_EFFECT = "titleeffect.unity3d";
    public const string DATA_MailMsg = "MailMsg.unity3d";
	public const string DATA_RANK = "rank.unity3d"; // 排行榜
    public const string DATA_DICE_TABLE = "fate_info.unity3d";
    public const string DATA_DICE_Chapter = "fate_chapter.unity3d";
    public const string DATA_DICE_Effect = "fate_effect.unity3d";
	public const string DATA_VIP = "VIPClass.unity3d";
	public const string DATA_COLORFORCE = "Colorforce.unity3d";

	public static readonly string JAVASCRIPT_QUIT = "window.close()";
	public static readonly string JAVASCRIPT_NEW_TAB = "window.open('{0}', '_blank')";
	public static readonly string JAVASCRIPT_REDIRECT = "window.location='{0}' ";

	public static readonly string URL_AT_WEB = "http://at.chinesegamer.net/index.asp";
	public static readonly string URL_WEB_LOGIN = "http://www.sky-dragon.net/weblogin/at/open.aspx" ;

	public static readonly  string[] ResourceFileList =
  	{
		DATA_TWGLEAF,	//0 介面消費表 
		DATA_DELIVER,  //1 傳送表		
		DATA_BIDCAT,	//2
		DATA_AUCTION,	//3
		DATA_SKILLSTARGEM, //4 技能星石表
		DATA_INBOX_DESCRIPTION, //5 收件夾說明內容表
		DATA_RAISINGTOOL, //6 招募工具表
		DATA_BASIC_FACTOR,     //7 基數表
		DATA_EMOTION,	//8 情感動作表
		DATA_TRADEINFO, //9 資訊說明
		DATA_Alchemy,			//10 煉金釜資料表
		DATA_CRYSTAL,		//11 水晶資料表
		DATA_EFFECT2,	//12 水晶效果表
		DATA_RIDINGEFF,  //13 座騎掛載光影表
		DATA_TOTEM,  //14 圖騰表
		DATA_STORY1,    //15 魔法故事書資料表
		DATA_STORY2,    //16 魔法故事書「星星蒐集效果表」
		DATA_ENCHANT2, //17 裝備魔化效果
		DATA_SOUND,		//18 音效表
		DATA_EMOTICONS, //19表情符號表
		DATA_TROOPLV, //20部下等級能力表
		DATA_TITLE, 	//21頭銜列表
		DATA_TITLE_EFFECT, //22頭銜效果
		DATA_MailMsg, //23系統信件
		DATA_RANK, //24排行榜
		DATA_VIP,	//25 VIP
		DATA_DEPARTMENT,//26場所表
		DATA_COLORFORCE,//27尊榮色效果表 
  	};

	public static readonly  int[] ResourcePP =//裡面的優先下載
	{
		0,1,4,(int)ResourceTag.InboxDescription, (int)ResourceTag.RaisingTool, (int)ResourceTag.BasicFactor,(int)ResourceTag.Emotion,
		(int)ResourceTag.GeneInformation, (int)ResourceTag.Alchemy, (int)ResourceTag.Crystal, (int)ResourceTag.CrystalEffect, (int)ResourceTag.RidingEff,
		(int)ResourceTag.Totem, (int)ResourceTag.Story1, (int)ResourceTag.Story2,
		(int)ResourceTag.StoryEnchant,(int)ResourceTag.SoundEffect,(int)ResourceTag.Emoticons,(int)ResourceTag.CouncilTroopLv,
		(int)ResourceTag.Titles,(int)ResourceTag.TitleEffects, (int)ResourceTag.MailMsg, (int)ResourceTag.VIP, (int)ResourceTag.Department, (int)ResourceTag.TitleColorEffects,
	};
	
	public enum ResourceTag
	{
		GoldLeaf = 0, 
		Deliver = 1, 
		BidCat = 2, 
		Auction = 3, 
		SkillStarGem = 4, 
		InboxDescription = 5, 
		RaisingTool = 6, 
		BasicFactor = 7,
		Emotion = 8,
		GeneInformation = 9,
		Alchemy = 10,
		Crystal = 11,
		CrystalEffect = 12,
		RidingEff = 13,
		Totem = 14,
		Story1 = 15,
		Story2 = 16,
		StoryEnchant = 17,
		SoundEffect = 18,
		Emoticons = 19,
		CouncilTroopLv = 20,
		Titles = 21, 
		TitleEffects = 22,
		MailMsg = 23,
		Rank = 24, // 排行榜
		VIP = 25,
		Department=26, 
		TitleColorEffects = 27, //尊榮色效果
	}

	public enum ReleaseLanguage
	{
		CHINESE_TRADITIONAL_TW,
		CHINESE_TRADITIONAL_HK,
		CHINESE_SIMPLIFIED, 
		ENGLISH
	}
	
    public const string LOG_DOWNLOAD = "download.txt";

	public const string NAME_UIMANAGER = "UIManagerContainer";
	public const string NAME_ROLEUNIT_PREFIX = "RoleUnit";
	public const string NAME_NPCUNIT_PREFIX = "NPCUnit";
	public const string NAME_DEVICENPCUNIT_PREFIX = "DNPCUnit";
	public const string NAME_TOTEMUNIT_PREFIX = "TotemUnit";
    public const string NAME_UIBILLBOARD_PREFIX = "Billboard";
	public const string NAME_UNIT_COMPONENT_PREFAB = "ThirdPersonControllerPrefab"; //場景可移動單位的 component container 

	public const uint ROLEID_NULL = 0;
	public const uint TALKNPC_ID = 9999; // 對話NPC的ID

	public const byte NPC_DIE_FOREVER = 83;//永遠倒地
	public const byte BATTLE_NO_TRIGGER = 29;//戰鬥時不可觸發
	public const byte NPC_DIE_Animation_ID = 20;//倒地死亡動作ID
	public const float NPC_DIE_Speed = 1000.0f;//倒地死亡動作speed

	public const float CONN_WAIT_SECOND = 5.0f; //連線等待秒數
	
	//YS_20121004
//    public const int POLICY_WAIT_MILLISECOND = 8000; //Unity連線認證等待秒數 
	public const int POLICY_WAIT_MILLISECOND = (int)(CONN_WAIT_SECOND/2)*1000; //Unity連線認證等待秒數

    //靜態資源檔檔名 (Asset/Resources/)
    public const string RESOURSES_START_BG = "Background/LOADING_base_01";

	//2D 圖片的前置名稱
	public const string PIC_PREFIX_ITEM = "Item/";    //物品圖名, 後面接數字
	public const string PIC_PREFIX_HEAD = "Item/";    //頭像圖名, 後面接數字
	public const string PIC_PREFIX_TROOPHINT = "Item/";    	//部下工作HINT
	public const string PIC_PREFIX_OTHER = "Item/";    
	public const string PIC_PREFIX_TITLE = "Title/";  //特殊榮耀圖

	//Layer Name
	public const string LAYER_NAME_50M = "50M";
	public const string LAYER_NAME_100M = "100M";
	public const string LAYER_NAME_150M = "150M";
	public const string LAYER_NAME_200M = "200M";
	public const string LAYER_NAME_300M = "300M";
	public const string LAYER_NAME_2000M = "2000M";
	// fs 12/07/12 播放Drama時才看的到的物件的layer名
	public const string LAYER_NAME_DRAMA_OBJECT = "Drama Object";
	// fs 12/07/19 Drama字幕用layer名
	public const string LAYER_NAME_DRAMA_SUBTITLE = "Drama Subtitle";
	// fs 12/08/24 播放Drama時看不到的layer名
	public const string LAYER_NAME_DRAMA_INVISIBLE = "Drama Invisible";
	public const string LAYER_NAME_MINI_CAMERA = "mini camera";
	public const string LAYER_NAME_UI_Collider = "UI Collider";
	public const string LAYER_NAME_UI_SHOW = "UI show";
	public const string LAYER_NAME_UI_VISIBLE_TRUE = "UI visible=true";
	public const string LAYER_NAME_UI_VISIBLE_FALSE = "UI visible=false";
	public const string LAYER_NAME_EN = "En";
	public const string LAYER_NAME_GAMEUNIT = "GameUnit";
	public const string LAYER_NAME_TERRAIN = "Terrain";

	//Layer ID
	public const byte LAYER_ID_DEFAULT = 0;
	public const byte LAYER_ID_IGNORERAYCAST = 2;
	public const byte LAYER_ID_50M = 8;
	public const byte LAYER_ID_100M = 9;
	public const byte LAYER_ID_150M = 10;
	public const byte LAYER_ID_200M = 11;
	public const byte LAYER_ID_300M = 12;
	public const byte LAYER_ID_2000M = 13;
	// fs 12/07/12 播放Drama時才看的到的layer ID
	public const byte LAYER_ID_DRAMA_OBJECT = 14;
	// fs 12/07/19 Drama字幕用layer ID
	public const byte LAYER_ID_DRAMA_SUBTITLE = 15;
	// fs 12/08/24 播放Drama時看不到的layer ID
	public const byte LAYER_ID_DRAMA_INVISIBLE = 16;
	public const byte LAYER_ID_MINI_CAMERA = 20;
	public const byte LAYER_ID_NAME_LABEL = 21;
	public const byte LAYER_ID_TalkingView = 22;
	public const byte LAYER_ID_UI_Collider = 23;
	public const byte LAYER_ID_UI_SHOW = 24;
	public const byte LAYER_ID_UI_VISIBLE_TRUE = 25;
	public const byte LAYER_ID_UI_VISIBLE_FALSE = 26;
	public const byte LAYER_ID_NPCEN = 27;
	public const byte LAYER_ID_WALL = 28;
	public const byte LAYER_ID_EN = 29;
	public const byte LAYER_ID_GAMEUNIT = 30;
	public const byte LAYER_ID_TERRAIN = 31;
	
	//對話場景區塊 Speak RLTB (上下左右) 數值
	public static readonly short[] SPEAK_RL = {60, 57, 65, 57}; //體型 m,g,b,s
    public static readonly short[] SPEAK_TB = {165, 158, 180, 165};//體型 m,g,b,s

	public const int OFFSET_RIDE_BASEVALUE = 1000;  //座騎位移基本值

	//創角流程中使用到的特別layer
	public const int CreateCharacterLayer = 17;
	//設定角色所在的圖層, 用在迷你攝影機(MiniCamera)時遮罩問題
	public const int PlayerLayer = 20;
	//設定介面碰撞圖層, 偵測介面與介面是否相互碰撞以觸發事件
	public const int UIColliderLayer = 23;
	public const int GameUnitLayer = 30;
	//圓(取自獵人Hunter X Hunter)的layer, 用來偵測場景中在玩家圓的範圍內的其他NPC或玩家
	public const int EnLayer = 29;

	public static readonly float[] LAYER_DISTANCE =
	{
		0,  //LAYER_ID_DEFAULT
		0,  //1
		0,  //2
		0,  //3
		0,  //4
		0,  //5
		0,  //6
		0,  //7
		50,  //LAYER_ID_50M
		100,  //LAYER_ID_100M
		150,  //LAYER_ID_150M
		200,  //LAYER_ID_200M
		300,  //LAYER_ID_300M
		2000,  //LAYER_ID_2000M
		0,  //14
		0,  //15
		0,  //16
		0,  //17
		0,  //18
		0,  //19
		0,  //LAYER_ID_MINI_CAMERA
		50,  //LAYER_ID_NAME_LABEL
		0,  //22
		0,  //23
		0,  //LAYER_ID_UI_SHOW
		0,  //LAYER_ID_UI_VISIBLE_TRUE
		0,  //LAYER_ID_UI_VISIBLE_FALSE
		0,  //LAYER_ID_NPCEN
		0,  //28
		0,  //LAYER_ID_EN
		50,  //LAYER_ID_GAMEUNIT
		0  //LAYER_ID_TERRAIN
	};

	///固定投影所用的
	public const int SHADOW_IGNORE_LAYER =
		(1 << LAYER_ID_IGNORERAYCAST) | 
		(1 << LAYER_ID_DRAMA_OBJECT) | // fs 12/07/16 投影時忽略動畫相關物件
		(1 << LAYER_ID_DRAMA_SUBTITLE) | // fs 12/07/19 投影時忽略動畫相關物件
		(1 << LAYER_ID_MINI_CAMERA) | 
		(1 << LAYER_ID_NAME_LABEL) | 
		(1 << LAYER_ID_TalkingView) |
		(1 << LAYER_ID_UI_SHOW) | 
		(1 << LAYER_ID_UI_VISIBLE_TRUE) | 
		(1 << LAYER_ID_UI_VISIBLE_FALSE) | 
		(1 << LAYER_ID_GAMEUNIT);

	//玩家碰撞盒預設大小
	public const float COLLIDER_CENTER_X = 0;
	public const float COLLIDER_CENTER_Y = 1f;
	public const float COLLIDER_CENTER_Z = 0;
	public const float COLLIDER_HEIGHT = 2f;
	public const float COLLIDER_RADIUS = .5f;

	//NPC預設碰撞盒大小
	public const float NPC_COLLIDER_DEFAULT_RADIUS = 1.4f;
	public const float NPC_COLLIDER_DEFAULT_HEIGHT = 2f;

	//server預設玩家半徑
	public const int RADIUS_PLAYER = 20;  //單位：server

	//server預設攻擊高度
	public const int HEIGHT_ATTACK = 500;  //單位：server

	//技能距離誤差值
	public const int OFFSETDISTANCE_SKILL_TARGET = 50;  //單位：server

	//預設空手攻擊範圍
	public const int DISTANCE_UNARM_ATK = 30;  //單位：server
	//基本面對目標距離
	public const int DISTANCE_BASE_FIGHT = 15;


	//單位換算
	public const byte BITS_PER_BYTE             = 8;  //1 byte = 8 bits
	public const byte BITS_PER_USHORT           = 16;  //1 ushort = 16 bits
	public const byte BITS_PER_UINT             = 32;  //1 uint = 32 bits
	public const byte BITS_PER_UINT64           = 64;  //1 uint64 = 64 bits

	public const ushort MILLISECONDS_PER_SECOND = 1000;  //1 sec = 1000 milli-secs
	public const byte SECONDS_PER_MINUTE        = 60;  //1 min = 60 secs
	public const byte MINUTES_PER_HOUR          = 60;  //1 hour = 60 mins
	public const byte HOUR_PER_DAY              = 24;  //1 day = 24 hour
	public const byte DAYS_PER_MONTH            = 30;  //1 month = 30 days
	public const byte MONTHS_PER_SEASON		= 3;	//1 season = 3 months
	public const byte MONTHS_PER_YEAR		= 12;	//1 year = 12 months
	public const byte SEASONS_PER_YEAR		= 4;	//1 year = 4 seasons
	public const byte MINUTES_PER_QUARTER	= 15;	//1 quarter = 15 minute
	public const byte QUERTER_PER_HOUR		= 4;	//1 hour = 4 quarter

	public const byte CENTIMETER_PER_METER = 100;  //1 meter = 100 centi-meters
	public const float CENTIMETER_PER_INCH = 2.54f;  //1 inch = 2.54 centi-meters

	public const byte CENTIMETERS_PER_SERVER_UNIT = 4;  //1 server unit = 4 centi-meters
	public const byte OBUNIT_PER_GRID = 16;  //1 OB grid's length = 16 server units
	public const byte HEIGHTUNIT_PER_GRID = 32;  //1 Height grid's length = 32 server units

	public const byte PER_PERCENT = 100;  //百分比%
	public const byte DISCOUNT_PERCENT = 10;  //折扣率1折
	
	public const int SIZE_OF_BYTE              = 1;
	public const int SIZE_OF_BOOL              = 1;
	public const int SIZE_OF_SHORT             = 2;
	public const int SIZE_OF_USHORT            = 2;
	public const int SIZE_OF_INT               = 4;
	public const int SIZE_OF_UINT              = 4;
	public const int SIZE_OF_FLOAT             = 4;
	public const int SIZE_OF_UNICODE_CHAR      = 2;
	public const int SIZE_OF_CHAR              = 1;
	public const int SIZE_OF_DOUBLE            = 8;
    public const int SIZE_OF_VECTOR3           = 12;

	//地圖障礙點、高度表、動態障礙點相關
	public const byte MAX_DYNAMIC_OB_COUNT = byte.MaxValue;
	public const int OB_SIZE = CENTIMETERS_PER_SERVER_UNIT * OBUNIT_PER_GRID;  //公分
	public const int HEIGHT_SIZE = CENTIMETERS_PER_SERVER_UNIT * HEIGHTUNIT_PER_GRID;  //公分
	
	//副本相關
	public const int DUNGEON_SCENE = 9000;
	public const int DUNGEON_SCENE_NUM = 1000; // 副本場景ID區間需要和server同步！！！)

	//分區相關
	public const int ZONE_SCENE = 3001;      // 分區場景（非分區0）的編碼開頭數值
	public const int ZONE_SCENE_NUM = 162;    // 所有有分區的分區數量總和（扣除分區0）
	public const int ZONE_NUM_PER_SCENE = 9; // 每個有分區的場景的分區數量（扣除分區0）
	public const int ZONE_HAS_NOT_ZONE_SCENE_STR_NUM = 10421; // "此場景沒有分區"的字串編號
	public const int ZONE_CAN_NOT_CHANGE_ZONE_STR_NUM = 10468; // "此地無法換分區"的字串編號
	public const int ZONE_CAN_NOT_CHANGE_ZONE_IN_BATTLE = 10658; // "戰鬥中，不可切換分區"的字串編號
	public const int ZONE_CAN_NOT_CHANGE_ZONE_IN_RIDING = 10660; // "騎乘中，不可切換分區"的字串編號

	public const ushort SCENEID_LIFE = 1002;
	public const ushort SCENEID_COUNCIL = 1003;
	public const ushort SCENEID_CREATECHAR = 2001;

	// 程式特殊場景編號名字(場景編號區間：1900~1993)
	public const ushort SCENEID_SERVERLIST = 1900;
	public const ushort SCENEID_LOGIN = 1901;

	public const string SCENE_NAME_SERVERLIST = "1900_map";
	public const string SCENE_NAME_LOGIN = "1901_map";

	//玩家移動相關
	public const int MAXTIME_WAIT_FOR_RESPONSE = 5000;  //單位：毫秒

	public const int DELTATIME_SPEED_TO_DISTANCE = 500;  //玩家速度位移表單位，單位：毫秒
	public const int DELTATIME_SEND_WASD_MOVE_TO_SERVER = 300;  //送WASD移動的時間間隔，單位：毫秒
	public const int DELTATIME_SEND_FLY_MOVE_TO_SERVER = 300;  //送飛行移動的時間間隔，單位：毫秒
	public const int DELTATIME_SEND_ATK_TO_SERVER = 300;  //送server普攻間隔
	public const int DELTATIME_CHANGE_DIR = 300;  //送改變方向的時間間隔，單位：毫秒
	public const int DELTATIME_JUMP = 2000;  //跳躍的時間，單位：毫秒
    public const int DELTATIME_FLYUP = 1000;  //向上飛的時間，單位：毫秒
	public const int MAX_DISTANCE_PER_LINEMOVE = 5000;  //一次直線移動的最大距離，server是7000，但是server說最好不要真的送7000

	public const int MAX_SEND_SERVER_WAYPOINTNUM = 5;  //最多可送給server的轉折點數

	public const int DELTADEGREE_CHANGE_DIR = 2;  //判斷改變方向是否成立的依據，單位：角度
	
	public const int HEIGHT_PER_JUMP = 300;  //每次跳躍的高度，單位：公分
	public const int HEIGHT_PER_FLYUP = 240;  //每次向上飛行的高度，單位：公分
	public const int DEFAULT_MOVE_SPEED = 8;  //預設移動速度等級
	public const int DEFAULT_NPC_MOVE_SPEED = 1;  //預設NPC移動速度等級

	public const float FLY_LANDING_OFFSET = 1f;  //判斷距離地面多少位置可著地用
	public const float OFFSET_FOLLOW_DISTANCE = .2f;  //跟隨者與目標距離間隔

	//硬體輸入名稱
	public const string INPUT_AXIS_FRONT_BACK = "FrontBack";
	public const string INPUT_AXIS_LEFT_RIGHT_B = "LeftRight_B";
	public const string INPUT_AXIS_LEFT_RIGHT = "LeftRight";
	public const string INPUT_AXIS_ROTATE = "Rotate";
	public const string INPUT_AXIS_ROTATE_B = "Rotate_B";
	public const string INPUT_AXIS_MOUSE_X = "Mouse X";
	public const string INPUT_AXIS_MOUSE_Y = "Mouse Y";
	public const string INPUT_AXIS_JUMP_OR_FLYUP = "Jump";
	
	public const string KEY_SPACE = "Space";
	
	public const int MOUSE_BUTTON_LEFT = 0;  //滑鼠左鍵
	public const int MOUSE_BUTTON_RIGHT = 1;  //滑鼠右鍵
	public const int MOUSE_BUTTON_MIDDLE = 2;  //滑鼠中鍵

    //Web 存檔 PlayerPref 存檔名稱
    public const string PREF_FULLSCREEN = "IsFullScreen";

	//同場景可見玩家數量上限
	public const int MaxScenePlayerNum = 5;
	public const int RoleMaxNormalEquipNum = 16;           //  角色最大一般裝備總量
	public const int RoleMaxModelEquipNum = 6;             //  角色最大造型裝備數量
	public const int MaxObjAddNum = 4;                     //  物品格式附加屬性數量
	public const int MaxObjAttrNum = 1;                    //  物品格式隨機武器屬性數量-剩附魔

	//技能表欄位換算相關
	public const byte SKILL_BONUS_RATE = 10;  //技能數值加成，欄位「物理技能傷害加成」、「魔法技能傷害加成」、「治療技能加成」使用，填1000 = 100%

	//武功領域
	public const byte SKILLFIELD_MAGICSCROLL = 4;  //武功領域

	//技能表hint相關
	public static readonly string[] SKILL_HINT_DELIMETER = {"^"};  //技能hint分割符號
	public static readonly string[] SKILL_HINT_CONVERT_SIGNS = {"0", "1", "2", "3", "4", "5", "A", "B"};  //技能hint轉換標示字元

	//NPC受擊動作光影
	public static readonly string[] PARTICLE_NPC_HIT = {"h0001", "h0046", "h0049", "h0052", "h0055", "h0058", "h0061", "h0064", "h0067", "h0070", "h0073", "h0001"};  //NPC受擊動作光影
	public static readonly string[] PARTICLE_NON_FIGHTLIGHT_NAME = {"i", "e", "y", "p", "z"};  //非戰鬥光影名稱開頭文字

	//普攻受擊光影
	public const string PARTICLE_NORMALATK_HIT = "h0078";

	//水花光影
	public const string PARTICLE_SPRAY = "i0023";
	//變身光影
	public const string PARTICLE_CHANGEBODY = "i0076";
	//掛機光影
	public const string PARTICLE_GEAR = "i0146";

	public const string LEVELUP_PARTICLE = "i0062";//升級光影
	public const string LEVELUP_SOUND = "lvup";//升級音效
	public const string LEVELUP_ACT = "hail";//升級動作

	//音效
	public const string SOUND_CANTUSESKILL_M = "Voice_93a";
	public const string SOUND_CANTUSESKILL_B = "Voice_93b";
	public const string SOUND_CANTUSESKILL_S = "Voice_94a";
	public const string SOUND_CANTUSESKILL_G = "Voice_94b";

	//動作layer
	public const int ANIMATION_LAYER_MOVEBASE = 1;  //移動
	public const int ANIMATION_LAYER_EMOTION = 2;  //情感動作
	public const int ANIMATION_LAYER_TALKNPC = 3; // 對話NPC播的動作
	public const int ANIMATION_LAYER_DEVICENPC = 4; // 機關NPC播的動作
	public const int ANIMATION_LAYER_BATTLEBASE = 10;  //戰鬥動作基礎值
	public const int ANIMATION_LAYER_BATTLEIDLE = ANIMATION_LAYER_BATTLEBASE + 1;  //戰鬥idle
	public const int ANIMATION_LAYER_BATTLEHIT = ANIMATION_LAYER_BATTLEBASE + 2;  //受擊
	public const int ANIMATION_LAYER_BATTLEATTACK = ANIMATION_LAYER_BATTLEBASE + 3;  //普攻
	public const int ANIMATION_LAYER_BATTLESKILL = ANIMATION_LAYER_BATTLEBASE + 4;  //集氣、技能、衝鋒
	public const int ANIMATION_LAYER_BATTLEDRAIN = ANIMATION_LAYER_BATTLEBASE + 5;  //吸取
	public const int ANIMATION_LAYER_BATTLEDIZZY = ANIMATION_LAYER_BATTLEBASE + 6;  //暈眩
	public const int ANIMATION_LAYER_BATTLEDOWN = ANIMATION_LAYER_BATTLEBASE + 7;  //擊倒、擊倒受擊、起身
	public const int ANIMATION_LAYER_BATTLECHANGE = ANIMATION_LAYER_BATTLEBASE + 8;  //變身、異變
	public const int ANIMATION_LAYER_BATTLEDIE = ANIMATION_LAYER_BATTLEBASE + 9;  //死亡動作、死亡起身、死亡後動作
	public const int ANIMATION_LAYER_BATTLEMAX = ANIMATION_LAYER_BATTLEBASE + 10;  //目前戰鬥動作最大layer
	
	//玩家體型種類數量
	public const int BODYTYPE_NUM = 4;
	//玩家動作組別數量 (每個體型有9組動作)
	public const int PLAYER_ANIMATION_GROUP_NUM = 9;
	public const int PLAYER_ANIMATION_GROUP_1 = 0;
	public const int PLAYER_ANIMATION_GROUP_2 = 1;
	public const int PLAYER_ANIMATION_GROUP_3 = 2;
	public const int PLAYER_ANIMATION_GROUP_4 = 3;
	public const int PLAYER_ANIMATION_GROUP_5 = 4;
	public const int PLAYER_ANIMATION_GROUP_6 = 5;
	public const int PLAYER_ANIMATION_GROUP_7 = 6;
	public const int PLAYER_ANIMATION_GROUP_8 = 7;
	public const int PLAYER_ANIMATION_GROUP_9 = 8;
	
	//動作體型代號
	public const string BODYTYPE_BIGBOY = "b";  //大男
	public const string BODYTYPE_MEDIUMBOY = "m";  //中男
	public const string BODYTYPE_SMALLGIRL = "s";  //小女
	public const string BODYTYPE_GIRL = "g";  //中女

    //動作體型高度
    public const float BODYTYPE_BIGBOY_HEIGHT = 2.2f;  //大男
    public const float BODYTYPE_MEDIUMBOY_HEIGHT = 2.0f;  //中男
    public const float BODYTYPE_SMALLGIRL_HEIGHT = 1.5f;  //小女
    public const float BODYTYPE_GIRL_HEIGHT = 1.8f;  //中女

	//攝影機TargetHeight
	public const float BODYTYPE_BIGBOY_TARGETHEIGHT = 2.3f;  //大男
    public const float BODYTYPE_MEDIUMBOY_TARGETHEIGHT = 1.9f;  //中男
	public const float BODYTYPE_GIRL_TARGETHEIGHT = 1.8f;  //中女
    public const float BODYTYPE_SMALLGIRL_TARGETHEIGHT = 1.3f;  //小女

	//動作、體型區隔符號
	public const string ANIMATION_DELIMETER = "@";

	//戰鬥動作名稱相關
	public const string ANIMATION_BATTLE_END_STR = "_a";  //戰鬥動作結尾字元
	public const string ANIMATION_DEPART_END_STR = "_depart";  //上下分離結尾字元
	public const string ANIMATION_TOP_BONE = "Bip01 Head";
	public const string ANIMATION_MIDDLE_BONE = "Bip01 Spine";
	public const string ANIMATION_LOWER_BONE = "Bip01 Pelvis";
	public const string ANIMATION_MOVE_BACK_END_STR = "_k";  //向後移動的結尾字元
	public const string ANIMATION_MOVE_LEFT_END_STR = "_l";  //向左移動的結尾字元
	public const string ANIMATION_MOVE_RIGHT_END_STR = "_r";  //向右移動的結尾字元

	//動作武器代號
	public const string ANIMATION_WEAPON_UNARMED = "una";  //空手
	public const string ANIMATION_WEAPON_SWSH = "swsh";  //劍盾
	public const string ANIMATION_WEAPON_STAFF = "staff";  //法杖
	public const string ANIMATION_WEAPON_INST = "inst";  //法器
	public const string ANIMATION_WEAPON_DAGGER = "da";  //匕首
	public const string ANIMATION_WEAPON_WRENCH = "wrench";  //扳手
	public const string ANIMATION_WEAPON_PISTOL = "pistol";  //手槍
	public static readonly string[] ANIMATION_JOB_WEAPON = {ANIMATION_WEAPON_UNARMED, ANIMATION_WEAPON_SWSH, ANIMATION_WEAPON_STAFF,
		ANIMATION_WEAPON_INST, ANIMATION_WEAPON_DAGGER, ANIMATION_WEAPON_WRENCH, ANIMATION_WEAPON_PISTOL};  //目前職業只有1~6

	//武器Hit點延遲時間
	public const float DELAYTIME_WEAPON_UNARMED = .3f;  //空手
	public const float DELAYTIME_WEAPON_SWSH = .3f;  //劍盾
	public const float DELAYTIME_WEAPON_STAFF = .2f;  //法杖
	public const float DELAYTIME_WEAPON_INST = .2f;  //法器
	public const float DELAYTIME_WEAPON_DAGGER = .1f;  //匕首
	public const float DELAYTIME_WEAPON_WRENCH = .3f;  //扳手
	public const float DELAYTIME_WEAPON_PISTOL = .1f;  //手槍
	public const float DELAYTIME_WEAPON_DEFAULT = .3f;  //預設

	//武器Hit點延遲速度
	public const float DELAYSPEED_WEAPON_UNARMED = .5f;  //空手
	public const float DELAYSPEED_WEAPON_SWSH = .15f;  //劍盾
	public const float DELAYSPEED_WEAPON_STAFF = .5f;  //法杖
	public const float DELAYSPEED_WEAPON_INST = .4f;  //法器
	public const float DELAYSPEED_WEAPON_DAGGER = 2f;  //匕首
	public const float DELAYSPEED_WEAPON_WRENCH = .1f;  //扳手
	public const float DELAYSPEED_WEAPON_PISTOL = 2f;  //手槍
	public const float DELAYSPEED_WEAPON_DEFAULT = .2f;  //預設
	
	//職業代碼
	public const int NEWBIE = 0;
	public const int SOLDIER = 1;
	public const int PRIEST = 2;
	public const int ASSASSIN = 3;
	public const int MAGICIAN = 4;
	public const int MACHINE = 5;
	public const int ALCHEMIST = 6;

	//職業轉職語音
	public static string[][] CareerVoice = new string[][]
	{
		new string[2]
		{
			"Voice_95a",    //男 聖劍士座右銘      I'm the shield of Atlantis!
			"Voice_96a",    //女 聖劍士座右銘      Protect the glory of our Lord!
		},
		new string[2]
		{
			"Voice_97a",    //男 福音祭司座右銘     For people of Atlantis!
			"Voice_98a",    //女 福音祭司座右銘     Melody will rescue their souls!
		},
		new string[2]
		{
			"Voice_99a",    //男 幽影法師座右銘     The power of chaos is the last liberation…
			"Voice_100a",   //女 幽影法師座右銘     Destruction will be the origin of rebirth…
		},
		new string[2]
		{
			"Voice_101a",   //男 深淵刺客座右銘     Attack!! under the shadow of death!
			"Voice_102a",   //女 深淵刺客座右銘     conquered by abyss…
		},
		new string[2]
		{
			"Voice_103a",   //男 齒輪大師座右銘     For the mighty science!
			"Voice_104a",   //女 齒輪大師座右銘     Gear! Wrench! Robot!
		},
		new string[2]
		{
			"Voice_105a",   //男 煉金術士座右銘     The next experiment always excites me!
			"Voice_106a",   //女 煉金術士座右銘     I seek… for the eternal truth
		},
	};
	
	//陣營
	public const int BRIGHT = 0;
	public const int CHAOS = 1;
	public const int SCIENCE = 2;

	//動作
	public const string ANIMATION_BATTLEIDLE = "idle_001";  //戰鬥idle
	public const string ANIMATION_ATTACK = "ak";  //普攻01~02
	public const string ANIMATION_SKILL = "sk";  //技能01~06
	public const string ANIMATION_HIT = "hit";  //受擊
	public const string ANIMATION_DOWN = "down";  //擊倒
	public const string ANIMATION_LIEHIT = "liehit";  //倒地受擊
	public const string ANIMATION_UP = "up";  //起身
	public const string ANIMATION_PULL = "pull";  //被吸取
	public const string ANIMATION_DIZZY = "dizzy";  //暈眩
	public const string ANIMATION_CHARGE = "charge";  //衝鋒
	public const string ANIMATION_JUMPKILL = "jump_ing";  //躍殺
	public const string ANIMATION_DIE = "die";  //死亡
	public const string ANIMATION_DIE_IDLE = "die_idle";  //死亡後
	public const string ANIMATION_REVIVE = "die_revive";  //死亡起身
	public const string ANIMATION_CASTING = "gather";  //集氣01a、01b~07a、07b
	public const string ANIMATION_IDLE = "idle";  //idle
	public const string ANIMATION_RUN = "run";  //跑
	public const string ANIMATION_JUMP = "jump_ing";  //跳
	public const string ANIMATION_JUMPROLL = "jump_ing2";  //翻滾
	public const string ANIMATION_RELAX = "leisure";  //休閒
	public const string ANIMATION_RIDE = "una_ride";  //騎乘
	public const string ANIMATION_LAUGH = "laugh"; //大笑
    public const string ANIMATION_HAIL = "hail"; //歡呼
    public const string ANIMATION_KNEELING = "kneeling"; //給我跪下
    public const string ANIMATION_KNEEUP = "kneelup"; //跪姿起身
	public const string ANIMATION_FALLING = "una_jump_ing3";
    public static readonly string[] ANIMATION_CREATE_CHAR = { "create01", "create02", "create03", "create04", "create05", "create06", "create07" }; //創角動作
    public static readonly string[] AvailableCharacters = new string[4] {
		"m",
		"g",
		"b",
		"s"
	};

	//技能ID
	public const ushort SKILLID_BACKTOWN = 49;  //回城術

	//狀態
	public const byte STATEID_HITFLY = 82;  //柯加斯式擊飛
	public const byte STATEID_CHANGEBODY = 86;  //變身
	public const byte STATEID_MUTATION = 89;  //異變
	public const byte STATEID_FALLDOWN = 91;  //落下
	public const ushort STATEID_CHANGESCALE = 1035;  //玩家放大

	//議會
    public const ushort MAX_TROOP_NUM = 15;					//部下最大值
	public const ushort	MAX_TROOP_PROPERTY 	= 3000;			//部下屬性最大值
	public const ushort	PROPERTY_SCALE 		= 3;			//屬性比例
	public const byte	RECRUIT_TROOPS 		= 1;			//招募部下
	public const byte	CHANGE_TROOPS 		= 2;			//替換部下
	public const byte	DESIGNATE_TROOPS	= 3;			//指派任務
	public const byte	REPORT_TROOPS 		= 4;			//回報任務
	public const byte	QUICK_REPORT 		= 7;			//回報任務
	public const byte	ASK_TROOPS 			= 8;			//要求部下資料
	
	public const byte	COUNCIL_LV 			= 12;			//等級限制
	public const ushort	COUNCIL_MARK		= 3121;			//限制永標
	public const ushort	COUNCIL_LIMIT		= 11299;		//限制字串
	
	public const byte	COUNCIL_WORK6		= 6;
	
	public const int NoWork 	= 0;				// 無工作
	public const int OnWork 	= 1;				// 工作中
	public const int WorkDown 	= 2;				// 可回報
	public const int TroopNotExt= 3;				// 部下不存在
	public const int NoTroop 	= 4;				// 無部下
	public const int Special 	= 5;				// 特殊
	
	//動作基本速度
	public const float ANIMATION_DEFAULT_SPEED = 1f;
	public const int ANIMATION_DEFAULT_ATTACK_TIME = 1000;  //單位：毫秒

	//顏色
	public static readonly Color ColorWhite = Color.white;
	public readonly Color ColorOrange = new Color(1.0f,0.6235f,0.345f,1.0f);
	public readonly Color ColorYellow = new Color(1.0f,0.9411f,0.0f,1.0f);
	public readonly Color ColorLightGreen = new Color(0.8313f,1.0f,0.1529f,1.0f);
	public readonly Color ColorLightBlue = new Color(0.4039f,0.5882f,1.0f,1.0f);
	public readonly Color ColorLightPurple = new Color(0.7333f,0.5568f,1.0f,1.0f);
	public readonly Color ColorPink = new Color(1.0f,0.6588f,0.9882f,1.0f);
	public static readonly Color ColorRed = new Color(1.0f,0.1764f,0.1764f,1.0f);
	public static readonly Color ColorInsideRange = new Color(.1098f, 1f, .9216f, 1f);
	public static readonly Color ColorOutOfRange = Color.red;

	public const uint Color_Black =       0xFF000000;	
	public const uint Color_NearBlack =   0xFF28170D;//文字用(非背景)	
	public const uint Color_Bk059054011 = 0xFF3B360B;//黃黑
	public const uint Color_Br102041000 = 0xFF662900;//深咖啡色
	public const uint Color_Ye135114000 = 0xFF877200;//黃褐色
	public const uint Color_White =       0xFFFFF9E8;
	public const uint Color_GrayWhite =   0xFFE3E3E3;
	public const uint Color_Red =         0xFFFF0000;
	public const uint Color_Redlight =    0xFFF39999;
	public const uint Color_Green =       0xFF00FF00;
	public const uint Color_Blue =        0xFF0000FF;		
	public const uint Color_DarkGreen =   0xFF006400;//綠色系
	public const uint Color_ForestGreen = 0xFF228B22;
	public const uint Color_Gr000055044 = 0xFF00372C;
	public const uint Color_GreenOut	= 0xFF030D21;
	public const uint Color_Gr027067022 = 0xFF1B4316;
	public const uint Color_GreenIn		= 0xFFB3F181;
	public const uint Color_LightGreen  = 0xFFB2EC0A;
	public const uint Color_G0xFFC3E65A = 0xFFC3E65A;
	public const uint Color_GreenName	= 0xFF81FD87;//g1
	public const uint Color_GreenEnough = 0xFF95E591;
	public const uint Color_Gress  		= 0xFF6EC6A7;
	public const uint Color_Gr232255204 = 0xFFE8FFCC;
	public const uint Color_GooseYellow	= 0xFFEFFCA7;
	public const uint Color_Goldenrod 	= 0xFFDAA520;//紅色系
	public const uint Color_RedOut  	= 0xFF3D061C;
	public const uint Color_Rd126009000 = 0xFF7E0900;//暗紅
	public const uint Color_RedIn	  	= 0xFFFA8C8C;
	public const uint Color_Ye255243185 = 0xFFFFF3B9;//淺黃
	public const uint Color_Golden =      0xFFFFF799;
	public const uint Color_Ye255247154 = 0xFFFFF79A;
	public const uint Color_Gold =        0xFFFFD700;
	public const uint Color_Yellow =      0xFFFFFF00;//黃色系
	public const uint Color_Yw255255155 = 0xFFFFFF9B;
	public const uint Color_PaleYellow  = 0xFFF7FD9D;
	public const uint Color_EggYellow   = 0xFFFFCC00;//y2
	public const uint Color_Yellow_01   = 0xFFFFC05E;//255 192 94
	public const uint Color_Yw255214091	= 0xFFFFD65B;
	public const uint Color_Yw255220169 = 0xFFFFDCA9;
	public const uint Color_ThinYellow  = 0xFFFFE7AE;//y1
	public const uint Color_Yw255239191 = 0xFFFFEFBF;
	public const uint Color_O0xFFF59628 = 0xFFF59628;
	public const uint Color_Yw255242219 = 0xFFFFF2DB;
	public const uint Color_DarkOrange  = 0xFFF38F00;
	public const uint Color_OrangeOut  	= 0xFF470505;
	public const uint Color_OrangeIn 	= 0xFFE4BB6D;
	public const uint Color_CoinYellow  = 0xFFFFF89D;
	public const uint Color_GrayBlue    = 0xFF666666;//藍色系
	public const uint Color_BlueOut		= 0xFF011229;
	public const uint Color_BlueIn		= 0xFF6DB3E9;
	public const uint Color_Blue2       = 0xFF5EB0E5;//b2 
	public const uint Color_WarterBlue	= 0xFF98E2DF;
	public const uint Color_BlueName	= 0xFF6CE2FF;
	public const uint Color_Bl213255243 = 0xFFD5FFF3;
	public const uint Color_Saddlebrown = 0xFF8B4513;
	public const uint Color_Chocolate   = 0xFFD2691E;	
	public const uint Color_Gray	    = 0xFFA99B8E;
	public const uint Color_GrayOut	    = 0xFFB4B4B4;
	public const uint Color_PurpleOut   = 0xFF3B013C;
	public const uint Color_PurpleIn    = 0xFFD78FE9;
	public const uint Color_lightPurple = 0xFFEDE0FF;
	public const uint Color_lightBlue   = 0xFF39D9F5;

	//unit 頭上的名稱
	public const uint Color_Purple		= 0xFFB24EFF;
	public const uint Color_PurpleOutline = 0xFF42005A;
	public const uint Color_LightPurple	= 0xFFDC98DE;
	public const uint Color_NPCPink		= 0xFFFF6363;
	public const uint Color_LightBlue	= 0xFF6CE1CF;
	public const uint Color_NPCRed		= 0xFFFF4A4A;
	public const uint Color_NPCRedOutline = 0xFF490000;
	public const uint Color_NPCLightGreen = 0xFF66FF66;
	public const uint Color_NPCYellow	= 0xFFEC9A51;//企劃說這是黃色
	public const uint Color_RoleGreen	= 0xFFB0E973;// "R：176 G：233B：115"
	public const uint Color_RoleBlue	= 0xFF9CB4EB;//一般玩家藍
	public const uint Color_RoleYellow	= 0xFFFCD757;//自己黃

	//Council Working State Color
	public const uint Color_Orangein	= 0xFFFF900D;
	public const uint Color_Orang_Out	= 0xFF311204;
	public const uint Color_Forestin	= 0xFFBCF679;
	public const uint Color_ForestOut	= 0xFF1B6713;
	public const uint Color_OrangeHint	= 0xFFFF8000;
	public static readonly uint[] Working_State_in = 
	{
		Color_White,
		Color_Orangein,
		Color_Forestin,
		Color_White,
		Color_White,
		Color_White
	};
	
	public static readonly uint[] Working_State_Out = 
	{
		Color_White,
		Color_Orang_Out,
		Color_ForestOut,
		Color_Black,
		Color_Black,
		Color_Black
	};
	
	//時間相關
	public const float TIME_LEAVE_BATTLE = 6f;  //最多持續戰鬥idle後脫離的時間，單位：秒
	public const float TIME_IDLE_RELAX = 6f;  //最多持續idle後播放休閒動作的時間，單位：秒
	
	//地塊相關
	public const string BlockImage0 = "";  		//
	public const string BlockImage1 = "";  		//不顯示
	public const string BlockImage2 = "i0009";  //傳點藍 (觸發1次)
	public const string BlockImage3 = "i0009";  //傳送點 (重覆觸發)
	public const string BlockImage4 = "";  		//巨型傳送點
	public const string BlockImage5 = ""; 		//只顯示中地圖
	public const string BlockImage6 = "";  		//小型傳送點 (藍)
	public const string BlockImage7 = "";  		//基本型傳送點 (黃)
	public const string BlockImage8 = "";  		//副本傳點
	public const string BlockImage9 = "i0077";  //副本傳點 (紅)
	public const string BlockImage10 = "i0095"; //地空傳點 (觸發1次)
	public const string BlockImage11 = "i0095"; //地空傳點 (重覆觸發)
	
	public static readonly string[] BlockImage = 
	{
		BlockImage0,			//0
		BlockImage1,			//1 不顯示
		BlockImage2,			//2 傳點藍 (觸發1次)
		BlockImage3,			//3 傳送點 (重覆觸發)
		BlockImage4,			//4 巨型傳送點
		BlockImage5,			//5 只顯示中地圖
		BlockImage6,			//6 小型傳送點 (藍)
		BlockImage7,			//7 基本型傳送點 (黃)
		BlockImage8,			//8 副本傳點
		BlockImage9,			//9 副本傳點(紅)
		BlockImage10,			//10 副本傳點
		BlockImage11			//11 副本傳點(紅)
	};
	
	public static readonly string[] EventLight = 
	{
		"",			//0 無
		"i0118",
		"",
		"",
		"",
		"",
		"",
		""
	};
	
	//其他
	public const float GRAVITY = 9.8f;  //重力
	public const float FontCharacterSize = 8.0f;
	public const char STR_O_COLON = '_';
	public const char STR_P_COLON = '%';
	public const int OtherShortcutSaveID = 9000;   // 快捷列存取用起始ID-其他功能
	public const int AdvanceShortcutSaveID = 9900;   // 快捷列存取用起始ID-預留
	public const int ItemShortcutSaveID = 9999;   // 快捷列存取用起始ID-物品
	public const int RoleMaxEventIndex = 1250;   // 角色最大永標索引 對照 SERVER的 RoleMaxEventByteNum
	public const int RoleMaxDynaEventNum = 200;  // 角色最大動標索引
    public const int RankStartMsgID = 11015;	 // 位階字串起始編號
	public const int OFFSET_CARDID = 100000000;   //CardID換算位移值
	public const float DELTATIME_WAITDIE = 5f;  //最多等待進行死亡處理時間
	public const ushort GOD_KnightBugle = 60024;//神器-號角

	//TAG
	public const string TAG_STEAMGEAR = "SteamGear";
	public const string TAG_DONTCLEAR = "DontClear";
	
	public const ushort MSG_IN_BATTLE = 607;
	
	public static readonly int[] Societies = 
	{
		2796,		// 光明殿堂
		2797,		// 渾沌議會
		2798		// 科學基地
	};
	
	public static readonly  string[] LinkList =
	{
		"http://at.chinesegamer.net/event/121129/serial.asp?gameid={0}&SV={1}&MD={2}",
		"http://www.facebook.com/AtlantisEmpire",
		"http://at.chinesegamer.net/event/121207/"
	};
	//http://at.chinesegamer.net/event/121129/serial.asp?gameid=遊戲帳號&SV=伺服器&MD=雜湊
	
	//上限相關
	public const int MAX_MONEY = 4500000;      //銀幣上限
	public const int MAX_FAKE_GOLD = 10000;    //獎勵金幣上限
	public const int MAX_ROLELV= 60;//目前開放玩家角色最高等級

	//能量恢復相關數值
	public const int ENERGY_REGAIN_TIME = 3;	 // 能量恢復時間（分）
	public const int ENERGY_REGAIN_POINT = 1;	 // 每次恢復多少能量
	public const int ACTIONPOINT_REGAIN_TIME = 1;	 // 行動值恢復時間
	public const int ACTIONPOINT_REGAIN_POINT = 1;	 // 每次恢復多少行動值
	public const float TRANINGTIME_EXP_RATE = 5.0f;	 // 訓練時間經驗倍率
	public const float TRANINGTIME_SILVER_RATE = 5.0f;	 // 訓練時間銀幣倍率

	//任務相關
	public const byte MAX_MISSION_NUM = 35;  	// 身上最大任務數
	public const byte MissionOnPage = 3;  	// 任務介面單頁任務數
	public const byte PrizeOnPage = 9;   	// 任務介面單頁獎勵數
	public const ushort MAIN_DFLAG_START = 12601;   // 主動標起始
	public const ushort MAIN_DFLAG_END = 32600;   	// 主動標終結
	
	public const int QuestionKinds = 17;	// 角色最大任務類型
	public const int MissionMain = 1;		// 王國評定
	public const int MissionArea = 2;		// 地區評定
	public const int MissionFight = 3;		// 勇者評定
	public const int MissionProfession = 4;	// 職人評定
	public const int MissionAct = 5;		// 世界評定
	public const int MissionChance = 6;		// 機會命運
	public const int MissionDungeon = 7;	// 副本評定
	public const int MissionNewbie = 8;		// 新手評定
	public const int MissionCouncil = 9;	// 議會評定
	public const int MissionDungeon2 = 10;	// 地城評定
	public const int MissionCouncil2 = 11;	// 文明回報
	public const int MissionDungeon3 = 12;	// 騎士團回報
	public const int MissionArea2 = 13;		// 隨機回報
	public const int Missionback = 14;		// 實力回報
	public const int MissionSecret = 15;	// 秘密評定
	public const int MissionSecret2 = 16;	// 秘密評定
	public const int MissionSecret3 = 17;	// 秘密評定
	
	public static readonly uint[] MissionNums = 
	{
		1,		// 1.王國評定
		1,		// 2.地區評定
		1,		// 3.勇者評定
		1,		// 4.職人評定
		10,		// 5.世界評定
		1,		// 6.機會命運
		1,		// 7.副本評定
		3,		// 8.新手評定
		1,		// 9.議會評定
		3,		// 10.地城評定
		1,		// 11.文明回報
		1,		// 12.騎士團回報
		1,		// 13.隨機回報
		1,		// 14.實力回報
		2,		// 15.秘密評定
		1,		// 16.秘密評定
		1,		// 17.秘密評定
	};

	// 獎勵類型
	public const int PrizCHANCE   = 1;		// 機會命運
	public const int PrizItem     = 2;		// 物品
	public const int PrizExp      = 4;		// 經驗
	public const int PrizEnergy   = 5;		// 能量
	public const int PrizPower    = 6;		// 權力點
	public const int PrizTrain    = 7;		// 訓練時間
	public const int PrizSlive    = 8;		// 銀幣
	public const int PrizGold     = 9;		// 金幣
	public const int PrizGodExp   = 14;		// 神器經驗
	public const int PrizKingPT   = 15;		// 王國點
	public const int PrizLvExp    = 16;		// 依等級給經驗
	public const int PrizLvSlive  = 17;		// 依等級給銀幣
	public const int PrizLvKPT    = 18;		// 依等級給王國點
	public const int PrizLvGExp   = 19;		// 依等級給神器經驗
	public const int PrizGold2    = 20;		// 金幣(綁定)
	public const int PrizLvExp2   = 23;		// 依所填等級給經驗
	public const int PrizLvSlive2 = 24;		// 依所填等級給銀幣
	public const int PrizHONOR    = 27;		// 榮耀值
	public const int PrizBNEXP    = 28;		// 依表現獲得經驗值
	public const int PrizBNSLV    = 29;		// 依表現獲得銀幣
	public const int PrizBNGOLD   = 30;		// 依表現獲得金幣
	
	public static readonly int[] MissionPrize = 
	{
		PrizCHANCE, PrizItem, PrizExp, PrizEnergy, PrizPower, PrizTrain, PrizSlive, PrizGold, PrizGodExp, PrizKingPT, PrizLvExp,
		PrizLvSlive, PrizLvKPT, PrizLvGExp, PrizGold2, PrizLvExp2, PrizLvSlive2, PrizHONOR, PrizBNEXP, PrizBNSLV, PrizBNGOLD
	};
	
	//圖號
	public const ushort MISSION_CHANCE = 40288;	 //機會命運編號
	public const ushort MISSION_EXP    = 40249;	 //經驗值編號
	public const ushort MISSION_ENERGY = 40250;	 //能量編號
	public const ushort MISSION_POWER  = 40251;	 //權力點編號
	public const ushort MISSION_TRAIN  = 40252;	 //訓練時間編號
	public const ushort MISSION_SLIVE  = 40253;	 //銀幣編號
	public const ushort MISSION_GOLD   = 40254;	 //金幣編號
	public const ushort MISSION_GODEXP = 40255;	 //神器經驗編號
	public const ushort MISSION_KINGPT = 40256;	 //王國點編號
	public const ushort MISSION_LVEXP  = 40258;	 //依等級給予經驗值編號
	public const ushort MISSION_LVSLIV = 40257;	 //依等級給予銀幣編號
	public const ushort MISSION_LVKPT  = 40259;	 //依等級給予王國點編號
	public const ushort MISSION_LVGEXP = 40260;	 //依等級給予神器經驗值編號
	public const ushort QUESTION_ITEM  = 65535;	 //謎樣物品編號
	public const ushort MISSION_PWRPT  = 29001;	 //王國點編號
	public const ushort MISSION_HONOR  = 40295;	 //榮耀值
	public const ushort MISSION_BNEXP  = 40249;	 //依表現獲得經驗值
	public const ushort MISSION_BNSLV  = 40253;	 //依表現獲得銀幣
	public const ushort MISSION_BNGOLD = 40254;	 //依表現獲得金幣

	// 獎勵類型和圖號對應表
	public static readonly Dictionary<int, ushort> PrizeAndGraphNumMap = new Dictionary<int, ushort>()
	{
		{PrizCHANCE,   MISSION_CHANCE}, // 機會命運
		{PrizExp,      MISSION_EXP},    // 經驗值
		{PrizEnergy,   MISSION_ENERGY}, // 能量
		{PrizPower,    MISSION_POWER},  // 權力點
		{PrizTrain,    MISSION_TRAIN},  // 訓練時間
		{PrizSlive,    MISSION_SLIVE},  // 銀幣
		{PrizGold,     MISSION_GOLD},   // 金幣
		{PrizGodExp,   MISSION_GODEXP}, // 神器經驗值
		{PrizKingPT,   MISSION_KINGPT}, // 王國點
		{PrizLvExp,    MISSION_LVEXP},  // 依等級給予經驗值
		{PrizLvSlive,  MISSION_LVSLIV}, // 依等級給予銀幣
		{PrizLvKPT,    MISSION_LVKPT},  // 依等級給予王國點
		{PrizLvGExp,   MISSION_LVGEXP}, // 依等級給予神器經驗值
		{PrizGold2,    MISSION_GOLD},   // 金幣(綁定)
		{PrizLvExp2,   MISSION_LVEXP},  // 依所填等級給經驗
		{PrizLvSlive2, MISSION_LVSLIV}, // 依所填等級給銀幣
		{PrizHONOR,    MISSION_HONOR},  // 榮耀值
		{PrizBNEXP,    MISSION_BNEXP},  // 依表現獲得經驗值
		{PrizBNSLV,    MISSION_BNSLV},  // 依表現獲得銀幣
		{PrizBNGOLD,   MISSION_BNGOLD}  // 依表現獲得金幣
	};

	// 獎勵類型和提示字串對應表(無需參數)
	public static readonly Dictionary<int, string> PrizeAndHint = new Dictionary<int, string>()
	{
		{PrizCHANCE, Const.Str_Mission0011},    //  1 機會命運
		{PrizBNEXP,  Const.Str_Mission0012[0]}, // 28 依表現獲得經驗值
		{PrizBNSLV,  Const.Str_Mission0012[1]}, // 29 依表現獲得銀幣
		{PrizBNGOLD, Const.Str_Mission0012[2]}  // 30 依表現獲得金幣
	};

	// 獎勵類型和提示字串對應表(需一參數)
	public static readonly Dictionary<int, string> PrizeAndHintWithOneParameter = new Dictionary<int, string>()
	{
		{PrizExp,      Const.Base0001[0]}, //  4 經驗值
		{PrizEnergy,   Const.Base0001[5]}, //  5 能量
		{PrizPower,    Const.Base0001[4]}, //  6 權力點
		{PrizTrain,    Const.Base0001[6]}, //  7 訓練時間
		{PrizSlive,    Const.Base0001[1]}, //  8 銀幣
		{PrizGold,     Const.Base0001[2]}, //  9 金幣
		{PrizGodExp,   Const.Base0001[7]}, // 14 神器經驗值
		{PrizKingPT,   Const.Base0001[3]}, // 15 王國點
		{PrizLvExp,    Const.Base0001[0]}, // 16 依等級給予經驗值
		{PrizLvSlive,  Const.Base0001[1]}, // 17 依等級給予銀幣
		// 18 依等級給予王國點: 沒有指定，這部份有點奇怪，不過任務介面那邊就這樣了，企劃說照任務介面，所以維持不變orz
		{PrizLvGExp,   Const.Base0001[7]}, // 19 依等級給予神器經驗值
		{PrizGold2,    Const.Base0001[2]}, // 20 金幣(綁定)
		{PrizLvExp2,   Const.Base0001[0]}, // 23 依所填等級給予經驗值
		{PrizLvSlive2, Const.Base0001[1]}, // 24 依所填等級給予銀幣
		{PrizHONOR,    Const.Base0001[8]}  // 27 榮耀值
	};

	public const float MaxTriggerRange = 3;	 // 事件觸發距離
	
	//釘死的永標
	public const int TRAININGTIME3_MARKID = 353;   			//金幣領取訓練時間
	public const int MARKID_ONLINE_REWARD_RECEIVED = 371;   //每日線上獎勵已領取
	public const int SIGNIN_MARKID = 421;
	public const int FUTURE_ROAD_MARKID = 2578;
	public const int WEB_GUIDE_MARKID = 2755;
    public const int WEB_PC_LOGIN_MARKID = 2756;
	public const int GODATRIFACT_USE_MARKID = 2794;         //神器使用標記
	public const int FIRST_BUILD_MARKID = 3673;
	public const int CAN_BUILD_MARKID = 3684;
	public const int ORACLE_STONE_MARKID = 4632;            //神諭之石教學任務標記
	public const int EMOTION_START_MARK_ID = 4751;          //表情符號起始標記
	public const int EMOTION_END_MARK_ID = 5250;            //表情符號結束標記
	public const int OPEN_RECRUIT_MARKID = 7403;
	public const int ENERGY_LIMIT1_MARKID = 7502;
	public const int ENERGY_LIMIT2_MARKID = 7504;

	//釘死的動標
	public const ushort DYNAMARKID_BLACKCARD_SHOPPING_DISCOUNT = 10103;     //黑爵卡之購物折扣(黃金之眼)
	public const ushort DYNAMARKID_BLACKCARD_OPEN_DUPLICATE_SCENE = 10104;  //黑爵卡之開副本場景(召喚之門)
	public const ushort DYNAMARKID_BLACKCARD_CALL_FRIEND = 10105;           //黑爵卡之招喚好友(友情之鏈)
	public const ushort DYNAMARKID_ONLINE_DAY_NUM = 33235;  //連續上線天數
	public const ushort DYNAMARKID_GOD_ARTIFACT_MAXLV = 33871; //神器目前最高等級
	public const ushort DYNAMARKID_GOD_ARTIFACT_WIPE = 33872;  //神器擦拭

	//介面相關
	public const int Font10 = 10;   // 文字SIZE10
	public const int Font12 = 12;   // 文字SIZE12
	public const int Font13 = 13;   // 文字SIZE13
	public const int Font14 = 14;   // 文字SIZE14
	public const int Font15 = 15;   // 文字SIZE15
	public const int Font16 = 16;   // 文字SIZE16
	public const int Font17 = 17;   // 文字SIZE17
	public const int Font18 = 18;   // 文字SIZE18
	public const int Font23 = 23;   // 文字SIZE23
	public const int Font24 = 24;   // 文字SIZE24
	public const byte MAXCOUMESSAGE = 20;   // 最大議會訊息數
	public const float OnTopPos_Z = -17000.0f;
	public const float OnAlmostTopPos_Z = -16000.0f;
	
	//骨架相關
	public const string ANIMATION_DEPART_BONE = "Bip01 Spine1";  //上下分離動作的起始骨架
	public const string BONE_RIGHT_HAND = "Bip01 R Hand";  //右手骨架
	public const string BONE_LEFT_HAND = "Bip01 L Hand";  //左手骨架
	public const string BONE_RIGHT_WEAPON = "WR01";  //右武器骨架
	public const string BONE_LEFT_WEAPON = "WL01";  //左武器骨架
	public const string BONE_RIGHT_FOOT = "Bip01 R Foot";  //右腳骨架
	public const string BONE_LEFT_FOOT = "Bip01 L Foot";  //左腳骨架
	public const string BONE_WAIST = "Bip01";  //腰骨架
	public const string BONE_HEAD = "Bip01 Head";  //頭骨架
	public const string BONE_RIDE = "mountpoint";  //座騎骨架
	public const string BONE_BIP01 = "Bip01";
	public const string BONE_WEAPON_Origin = "wp01"; //武器骨架原點
	public const string BONE_WEAPON_End = "wp02";    //武器骨架終點

    public const string CachingLicense = "Cache=Atlantis;Domain=http://chinesegamer.net/;Size=2147483648;Signature=34012e49d3abe87b642425eaaccaaa5999912c61f229fd289c923d0dc00b8995baed18b2f9459588da400f45f253d8266c273f2a0b665df38fe5758ff6fe344cf27d2d3816230a6d9748cd34f1e7a19f9cb98eb7a0f5d340e5b8ae827387a8da627b7165f1395df6f8fdc3d363d120470b2d19ab7e722438676632aa5401056d;Expiration=1325462400";
	
	public readonly byte[] BITS_IN_BYTE =
	{
		0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2,

		3, 3, 4, 3, 4, 4, 5, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3,

		3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3,

		4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4,

		3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5,

		6, 6, 7, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4,

		4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5,

		6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 2, 3, 3, 4, 3, 4, 4, 5,

		3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 3,

		4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 4, 5, 5, 6, 5, 6, 6, 7, 5, 6,

		6, 7, 6, 7, 7, 8
	};

    public static readonly byte[] Maskbits = {1,2,4,8,16,32,64,128};
    public static readonly byte[] UnMaskbits = {254,253,251,247,239,223,191,127};		
	
	//斷線原因
	public const byte NETWORK_BROKE_CAUSE_RELOGIN = 2;
	public const byte NETWORK_BROKE_CAUSE_LOCKACCOUNT = 5;
	public const byte NETWORK_BROKE_CAUSE_NORMALEXIT = 25;
	public const byte NETWORK_BROKE_CAUSE_BACKSERVERLIST = 42;
	public const byte NETWORK_BROKE_CAUSE_X_CLOSE_ICON = 67;
	public const byte NETWORK_BROKE_CAUSE_CREATCHARACTEREXCEED = 72;
	public const byte NETWORK_BROKE_CAUSE_NOTCBQUALIFER = 73;

	public static readonly string[] DOWNLOAD_ERROR =
	{
		"Invalid Unity Web File (Decompression Failure)",      // 0
		"Not enough space in cache to write file",             // 1
		"Bad file length",                                     // 2
		"Invalid Unity Web File (Couldn't Decode LZMA Header", // 3
		"Failed writing body",                                 // 4
		"",                                                    // 5 為了缺該檔案而保留的
	};

	public const byte DOWNDLOAD_ERROR_FILE_NOT_EXIST = 5;

	//協定相關 (S to C)
	public const byte PROTOCOL_LOGIN = 1;
	public const byte PROTOCOL_SYSTEM = 2;
	public const byte PROTOCOL_PLAYER = 3;
	public const byte PROTOCOL_AREA = 5;
	public const byte PROTOCOL_SCENE = 6;
	public const byte PROTOCOL_TEAM = 7;
	public const byte PROTOCOL_COMBAT = 8;  //普攻戰鬥
	public const byte PROTOCOL_SKILLCOMBAT = 9;  //技能戰鬥
	public const byte PROTOCOL_MOVE = 10;  //移動
	public const byte PROTOCOL_NPC = 12;  //NPC資料
	public const byte PROTOCOL_STATUS = 13;  //狀態
	public const byte PROTOCOL_RIDE = 16;  //共乘
	public const byte PROTOCOL_TOTEM = 20;  //圖騰
	public const byte PROTOCOL_INBOX = 24;  //收件夾
	public const byte PROTOCOL_ADVANTURE = 60;  //冒險團
	public const byte PROTOCOL_CHAT = 61;  //聊天
    public const byte PROTOCOL_SOCIAL = 71;  //社群
	public const byte PROTOCOL_EVENT = 72;  //事件
	public const byte PROTOCOL_LEADERBOARD = 73; // 排行榜
	public const byte PROTOCOL_SUNDRY = 74;         //雜項 
	public const byte PROTOCOL_Market = 91; //拍賣場專用 		
	public const byte PROTOCOL_ITEM = 100;        //物品
	public const byte PROTOCOL_LIFE = 110;        //生活(技能)
	public const byte PROTOCOL_ACTIVITY = 111;  //活動
	public const byte PROTOCOL_GM = 200;  //GM(客服中心)
	public const byte PROTOCOL_TRADE = 101;        //貿易相關-to gs
	public const byte PROTOCOL_TRADE_RCV = 101;    //貿易相關-from gs

	//限制相關
	public const byte LIMIT_BATTLE_SKILL_MAX = 200;  //玩家最多擁有的技能
	public const byte LIMIT_BATTLE_STATUS_MAX = 30;  //玩家最多有的狀態

    public const string DEVICENPC_BASEMODEL = "";
    public const string DEVICENPC_3 = "d";
    public const string DEVICENPC_5 = "da";
    public const string DEVICENPC_6 = "db";
    public const string DEVICENPC_7 = "dc";
    public const string DEVICENPC_8 = "dd";
    public const string DEVICENPC_9 = "de";
    public const string DEVICENPC_10 = "df";
    public const string DEVICENPC_11 = "dg";
    public const string DEVICENPC_12 = "dh";
    public const string DEVICENPC_13 = "di";
    public const string DEVICENPC_14 = "dj";
    public const string DEVICENPC_15 = "dk";
    public const string DEVICENPC_16 = "dl";
    public const string DEVICENPC_17 = "dm";
    public const string DEVICENPC_18 = "dn";
    public const string DEVICENPC_19 = "do";
    public const string DEVICENPC_20 = "dp";
    public const string DEVICENPC_21 = "dq";
    public const string DEVICENPC_22 = "dr";
    public const string DEVICENPC_23 = "ds";
    public const string DEVICENPC_24 = "dt";

    public static readonly string[] DEVICENPC_MODELNAME = new string[]
    {
        DEVICENPC_BASEMODEL, "", "", DEVICENPC_3, "", DEVICENPC_5,  //0~5
        DEVICENPC_6 , DEVICENPC_7 , DEVICENPC_8 , DEVICENPC_9 , DEVICENPC_10,
        DEVICENPC_11, DEVICENPC_12, DEVICENPC_13, DEVICENPC_14, DEVICENPC_15,
        DEVICENPC_16, DEVICENPC_17, DEVICENPC_18, DEVICENPC_19, DEVICENPC_20,
        DEVICENPC_21, DEVICENPC_22, DEVICENPC_23, DEVICENPC_24,
    };

	public const string NAME_SIMPLEMODEL_MAN = "a0103";
	public const string NAME_SIMPLEMODEL_WOMAN = "a0104";

    public const string MIDMAP_BG = "01";
    public const string MIDMAP_PATH = "02";
	//地圖相關
	public const int Min_MapMarkID = 3121;
	public const int Max_MapMarkID = 3220;
	
	// 組隊相關
	public const int MAX_TeamMemberNum = 4;
	
	// 好友相關
	public const int MAX_RecommandFriends = 20;
	public const int MAX_NumFriends = 100;
	public const float WAIT_ADDFRIEND_TIME = 30.0f;
	
	// 活動相關
	public const uint RACING_ACTIVITY_EVENTID = 380001;
	public const uint NORMAL_ACTIVITY_EVENTID = 390029;
	public const ushort RACING_SCORE_DMARK = 10153;
	public const ushort RACING_SCENE_ID = 6024;
	
	// 字串編號
	public const ushort POST_CHAR_LIMIT_STRID = 11357;
	public const ushort NO_RECIPE_STRID = 16204;
	public const ushort CRAFT_FAIL_STRID = 16202;
	
	// 生活技能相關
	public const int TOOL_KIND = 82;
	public const int MATERIAL_KIND = 83;
	public const int FRIEND_ASSIST_DMARK = 10302;
	public const int FRIEND_ASSIST_LIMIT = 20;
	
	// 材料邀請相關
	public const int VALUE_BUY_MATERIAL = 15; // 材料購買價錢
	public const ushort MATERIAL_INVITATION_LIMIT = 50; // 邀請次數上限
	public const ushort MATERIAL_INVITATION_DMARK = 10001; // 紀錄邀請次數的動標
	
	// 頭銜相關
	public const int TITLE_LIMIT_LEVEL = 32;
	
	//Buff 相關
	public const int Max_ShowIconNum = 30;
	public static readonly float[] buffGridSize = new float[3] { 35f, 21f, 13f };
	public static readonly float[] buffIconSize = new float[3] { 32f, 20f, 12f };

	//死亡復活所需花費, 此承接S:5-9參數變化
	public static uint Reborn_CostSilver = 0;//銀幣復活花費
	public static uint Reborn_CostGold = 0;//金幣復活花費

	public const ushort NoviceProtLV = 30; //新手保護(最高)等級

	public const ushort WEAK_STATE_ID = 228;
	public const ushort WEAK_STATE_ID1 = 238;

	//強化裝備相關
	public const ushort STRENGTHEN_TOOL_ID = 38001;
	public const ushort STRENGTHEN_GOLDLEAF_PRAY_ID = 30;
	public const ushort STRENGTHEN_GOLDLEAF_ID_0 = 77;//	0~19級
	public const ushort STRENGTHEN_GOLDLEAF_ID_20 = 78;//	20~29級
	public const ushort STRENGTHEN_GOLDLEAF_ID_30 = 71;//	30~39級
	public const ushort STRENGTHEN_GOLDLEAF_ID_40 = 79;//	40~49級
	public const ushort STRENGTHEN_GOLDLEAF_ID_50 = 72;//	50~59級
	public const ushort STRENGTHEN_GOLDLEAF_ID_60 = 80;//	60級以上
	public const ushort STRENGTHEN_ERROR_MSG_ID_FULL = 25035;	//銀幣或道具滿了
	//演化相關
	public const ushort EVOLUTION_MARK_ID = 2924;

	//Hud Text相關
	public static Vector2 ExpHudPos = new Vector2(180,50+UICameraY_offset);
	public static Vector2 MoneyHudPos = new Vector2(180,7.5f+UICameraY_offset);
	public static Vector2 HonorHudPos = new Vector2(180,-40+UICameraY_offset);
	//ui camera位移的offset, 讓介面不和場景重疊
	public const int UICameraY_offset = -10000;
	
	//商城相關
	public static readonly byte[] ShopMallTabs = new byte[] { 1, 2, 3, 4, 5};//紀錄哪些頁籤種類是商城
	public static readonly byte[] ClothesTabs = new byte[] { 6, 7};//紀錄哪些頁籤種類是造型
	public const byte ShopMallNewTabID = 0;
	public const byte ShopMallVIPTabID = 254;
	public const byte MALL_REPAIR_VIPLV = 4; //vip要幾級才可以開啟商城修理
	public const byte MALL_SELL_VIPLV = 1; //vip要幾級才可以開啟商城賣道具
	public const int Lowest_VipLV = 1;  //最低VIP等級
	public const int HighestVipLV = 10; //最高VIP等級
	public static readonly ushort[] VIP_BUFF_ID = new ushort[] { 601, 602, 603 };
	public static readonly byte[] VIP_BUFF_LV = new byte[] { 4, 6, 8 };

	//技能相關
	public const int SKILL_MAXAMOUNT = 120;
	public const int LEARN_SKILL_LOCK_BOOKMARK = 2624; //要有這個永標才能學技能
	public const ushort SKILL_STAR_MATERIAL = 34001; // 星塵道具編號
	public static readonly Color[] UnitStateColors = new Color[5] {
		new Color(139f/255f,22f/255f,45f/255f,1.0f),  //紅色
		new Color(207f/255f,167f/255f,0f/255f,1.0f),  //金黃色
		new Color(97f/255f,139f/255f,188f/255f,1.0f),  //深藍色
		new Color(83f/255f,152f/255f,10f/255f,1.0f),  //綠色
		new Color(75f/255f,39f/255f,112f/255f,1.0f)	  //紫色
	};
	
	public static readonly Color[] ChangeSpecialColors = new Color[8] {
		new Color(255f/255f,0f/255f,0f/255f,1.0f),
		new Color(146f/255f,208f/255f,80f/255f,1.0f),
		new Color(0f/255f,176f/255f,240f/255f,1.0f),
		new Color(255f/255f,255f/255f,0f/255f,1.0f),
		new Color(112f/255f,48f/255f,160f/255f,1.0f),	
		new Color(64f/255f,64f/255f,64f/255f,1.0f),
		new Color(247f/255f,150f/255f,70f/255f,1.0f),
		new Color(255f/255f,153f/255f,255f/255f,1.0f)
	};
	
	public static readonly Color[] DefaultSkinColors = new Color[21] {
		new Color(196f/255f, 198f/255f, 213f/255f,  33f/255f),
		new Color(175f/255f, 175f/255f, 175f/255f,  33f/255f),
		new Color(150f/255f, 150f/255f, 150f/255f,  33f/255f),
		new Color(145f/255f, 147f/255f, 129f/255f,  33f/255f),
		new Color(166f/255f, 160f/255f, 143f/255f,  33f/255f),
		new Color(155f/255f, 158f/255f, 120f/255f,  33f/255f),
		new Color(152f/255f, 144f/255f, 115f/255f,  33f/255f),
		new Color(139f/255f, 125f/255f, 124f/255f,  33f/255f),
		new Color(161f/255f, 186f/255f, 185f/255f,  33f/255f),
		new Color(158f/255f, 174f/255f, 198f/255f,  33f/255f),
		new Color(175f/255f, 179f/255f, 226f/255f,  33f/255f),
		new Color(133f/255f, 118f/255f, 114f/255f,  33f/255f),
		new Color(120f/255f, 108f/255f, 81f/255f,  33f/255f),
		new Color(103f/255f, 77f/255f, 61f/255f,  33f/255f),
		new Color(99f/255f, 68f/255f, 68f/255f,  33f/255f),
		new Color(95f/255f, 90f/255f, 90f/255f,  33f/255f),
		new Color(93f/255f, 79f/255f, 79f/255f,  33f/255f),
		new Color(72f/255f, 68f/255f, 64f/255f,  33f/255f),
		new Color(80f/255f, 80f/255f, 80f/255f,  33f/255f),
		new Color(67f/255f, 67f/255f, 67f/255f,  33f/255f),
		new Color(57f/255f, 57f/255f, 57f/255f,  33f/255f)
	};
	
	public static readonly Color[] CombatDamageTextColor = new Color[8] {
		new Color(255f/255f, 255f/255f, 255f/255f,  255f/255f), //白色
		new Color(255f/255f, 29f/255f, 0f/255f,  255f/255f),    //紅色
		new Color(31f/255f, 219f/255f, 11f/255f,  255f/255f),   //綠色
		new Color(43f/255f, 228f/255f, 255f/255f,  255f/255f),  //藍色
		new Color(255f/255f, 108f/255f, 0f/255f,  255f/255f),   //橘色
		new Color(255f/255f, 222f/255f, 0f/255f,  255f/255f),   //黃色
		new Color(164f/255f, 67f/255f, 255f/255f,  255f/255f),  //紫色
		new Color(138f/255f, 255f/255f, 86f/255f,  255f/255f)   //淺綠色
	};
	
	//髮色顏色參數(R,G,B,Brightness)
	public static readonly Vector4[] HairColorParameters = new Vector4[20] {
		new Vector4(150,150,150,100),
		new Vector4(91,159,255,437),
		new Vector4(160,192,255,348),
		new Vector4(255,158,224,225),
		new Vector4(96,81,242,302),
		new Vector4(89,109,255,320),
		new Vector4(88,132,242,290),
		new Vector4(80,221,234,235),
		new Vector4(220,232,198,298),
		new Vector4(193,217,221,275),
		new Vector4(80,221,234,137),
		new Vector4(175,228,255,274),
		new Vector4(95,95,95,100),
		new Vector4(225,225,29,100),
		new Vector4(154,238,203,100),
		new Vector4(135,255,10,100),
		new Vector4(255,173,91,100),
		new Vector4(198,87,104,100),
		new Vector4(198,59,196,100),
		new Vector4(230,230,50,100)
	};
	
	// 亞特測試版
	public const string FB_APPId = "203953249707235";
	public const string FB_APPSecret = "8c60a6b60f35100447391e5813628000";
	
	//Anacin測試版
//	public const string FB_APPId = "331069076926957";
//	public const string FB_APPSecret = "56873fa71bcd7f68a0c06177cec5ea05";
	public const string FB_APPURL = "http://apps.facebook.com/anacapp/";

	#region 聊天功能用
	public const string CHAT_INSTRUCTION_HEAD = "{[(";
	public const string CHAT_INSTRUCTION_TAIL = ")]}";
	public const string CHAT_INSTRUCTION_EMOICON = "emoicon";
	public const string CHAT_INSTRUCTION_ITEM = "itemid";
	public const int CHAT_WORLD_COST = 1000;
	#endregion

	public const byte DICE_LIMIT = 20;
	public const byte LOWEST_ALLOWGROWLV = 15;//最低可開啟成長秘術介面的角色等級
	public const byte LOWEST_ALLOWAUTOGEAR = 16;//最低可以使用蒸汽齒輪的角色等級
	
	public const byte Displaylook_EquipParticleSwitch=13; //裝備外觀顯示 強化光影 Bit設定 (數值為1~16)
	
	public const int EncodingConst = 10000;
	
	//一次性特惠 觸發動標區間
	public enum DispSpecial_DynMarkRange : ushort
	{
		other_s =34301,//其他條件動標 始
		
		LV_s = 34340,//角色等級動標區間 始
		LV_e = 34399,
		
		other_e = 34400,//其他條件動標 結
		
		TOK_s = 34821,//大師動標區間 始
		TOK_e = 34900,//大師動標區間 結
		Assem_s = 34901,//議會動標區間 始
		Assem_e = 34930,
	}
}