#define	_DEVELOP_DEBUG //開發版本
#define SHOW_VERSION_NUMBER //顯示版號
#define _SHOW_FPS //顯示FPS
//#define _UNIT_HIDDEN_CACHE // 是否延遲消滅UNIT

#if UNITY_EDITOR
	//#define  USE_SIMPLE_MODEL  //是否顯示簡易模型
#else
	//#define USE_SIMPLE_MODEL
#endif

#if (_DEVELOP_DEBUG)
	//	#define OFFLINE
	//	#define CAN_FLY_ANYWHERE
	//	#define CAN_WALK_ON_OB
	// #define DEBUG_RAYCAST  //是否開啟Debug.Ray功能
		#define _DUMP_DEBUG_MESSAGE // 是否輸出 開發除錯用 Log (不影響unity強制輸出的 exception log) (產外部 正式版勿開) 
		#define _SHOW_ERROR_MESSAGE // 是否顯示 Error 於聊天視窗, 與中央訊息 (產外部 正式版勿開) 
		#define _SHOW_DEBUG_GUI
		#define _FACEBOOK_DEBUG //是否開啟 facebook 回應 dump 的功能
		#define _FACEBOOK_RESPONSEFIX //facebook 向 facebook 索取動態資料,回應為空時, 是否讀取固定資料 (for dev & debug)
		//#define _SKIP_FTP_CHECK //是否略過 FTP 檢查, (產外部PC 版勿開) 
	//#define _FILTERMODE_POINT //圖filtermode 不變更為 bilinear

		//#define _DEBUG_UNIT_HIDDEN // 是否輸出延遲消滅UNIT相關Debug訊息
		#define DEBUG_PROTOCOL_AREA  //是否輸出區域相關協定
		#define DEBUG_PROTOCOL_COMBAT  //是否輸出普攻相關協定
		#define DEBUG_PROTOCOL_MOVE  //是否輸出移動相關協定
		#define DEBUG_PROTOCOL_SKILLCOMBAT  //是否輸出技能相關協定
		#define DEBUG_PROTOCOL_STATE  //是否輸出狀態相關協定
		#define DEBUG_PROTOCOL_TOTEM  //是否輸出圖騰相關協定
		#define DEBUG_SCENEMANAGER // 是否輸出SceneManager相關Debug訊息
		#define DEBUG_EYESIGHT //是否輸出視野相關協定訊息
		#define DEBUG_PROTOCOL_TEAM //是否輸出隊伍資料異動相關協定
		#define DEBUG_PROTOCOL_LEADERBOARD // 是否輸出皇家俱樂部(排行榜)相關協定
		#define DEBUG_PROTOCOL_SUNDRY  //是否輸出雜項相關協定
		#define DEBUG_PROTOCOL_NPC  //是否輸出NPC相關協定
		#define DEBUG_PROTOCOL_EVENT  //是否輸出事件相關協定

		#define PROGRAM_END_DRAMA // 程式強制跳離動畫 (12/11/26 企劃說要開了！ 12/11/29 弄錯了 關閉！ )
// [DEBUG版]下載功能開關區
		#if UNITY_EDITOR
			//#define _DOWNLOAD_LOG_ENABLE			//是否開啟下載日誌功能
			//#define _PRELOAD					//是否背景預載
			//#define _CACHING_ENABLE //是否使用Unity Caching
			//#define _START_LOADING		//是否前導包下載
		#elif UNITY_WEBPLAYER
			//#define _DOWNLOAD_LOG_ENABLE		//是否開啟下載日誌功能
			#define _PRELOAD					//是否背景預載
			#define _CACHING_ENABLE //是否使用開啟快取功能 (include Unity Caching Licence)
			#define _START_LOADING		//是否前導包下載
		#elif UNITY_STANDALONE_WIN
			//#define _DOWNLOAD_LOG_ENABLE			//是否開啟下載日誌功能
			//#define _PRELOAD					//是否背景預載
			//#define _CACHING_ENABLE	//是否使用Unity Caching
			//#define _START_LOADING		//是否前導包下載
		#endif
#else
		// [正式版]下載功能開關區, 此區勿隨意更動
		#if UNITY_WEBPLAYER
				//#define _DOWNLOAD_LOG_ENABLE		//是否開啟下載日誌功能
				#define _PRELOAD					//是否背景預載
				#define _CACHING_ENABLE //是否使用開啟快取功能 (include Unity Caching Licence)
				#define _START_LOADING		//是否前導包下載
		#elif UNITY_STANDALONE_WIN
				//#define _DOWNLOAD_LOG_ENABLE			//是否開啟下載日誌功能
				//#define _PRELOAD					//是否背景預載
				//#define _CACHING_ENABLE	//是否使用Unity Caching
				//#define _START_LOADING		//是否前導包下載
		#endif
#endif



//#define _USE_CIRCLE_SHADOW // 是否使用有效能問題的 Unity Projector

#if UNITY_STANDALONE_WIN
	//#define _MICRO_CLIENT_VERSION // PC 版的微型客戶端版本定義
#endif

#if OFFLINE
	#define _DUMP_DEBUG_MESSAGE
//	#define CAN_WALK_ON_OB
#endif

//#define LOCAL_SERVER          //本地端開伺服器

//#define _LOAD_LOCALHOST_FONT  //下載本地端字型

#define _HIDE_PASSWORD

#define _DISPLAY_GROUND_TARGET   //顯示點擊地面的光影

//#define _MEMORY_WINDOW //顯示記憶體相關資訊

//#define _MOUSE_POSITION //顯示滑鼠座標

#define _SYSCONFIG_DEBUG //系統功能debug

#define _EVENT_FLOW_RECORD //事件除錯

#define _DISPLAY_PLAYER_POSITION //顯示玩家位置

//#define _MEMORY_USAGE_TEST //記憶體使用量測試

//三選一
//#define _XML_SERIALIZE 
//#define _JSON_SERIALIZE
#define _BINARY_SERIALIZE



#define _READDATA

//#define CREATE_CHARACTER_ACTIVITY //創角特別活動

#define CLOSE_BETA        //封測

#define CLOSE_DOWNLOAD_FINISH_QUEUE // 關閉下載完成Queue(預設所有版本都先關閉)

public static class GlobalDefine
{
	#if _DEVELOP_DEBUG 
	public static bool DEVELOP_DEBUG = true;
	#else 
	public static bool DEVELOP_DEBUG = false;
	#endif

	#if OFFLINE
	public static bool OFFLINE = true;
	#else
	public static bool OFFLINE = false;
	#endif

	#if _DUMP_DEBUG_MESSAGE
	public static bool DUMP_DEBUG_MESSAGE = true;
	#else  
	public static bool DUMP_DEBUG_MESSAGE = false;
	#endif
	
	#if _SHOW_DEBUG_GUI 
	public static bool SHOW_DEBUG_GUI = true;
	#else  
	public static bool SHOW_DEBUG_GUI = false;
	#endif
	
	#if _SHOW_ERROR_MESSAGE 
	public static bool SHOW_ERROR_MESSAGE = true;
	#else  
	public static bool SHOW_ERROR_MESSAGE = false;
	#endif

#if (_WWWDebug)
	public static bool WWW_DEBUG = true;
#else
	public static bool WWW_DEBUG = false;
#endif

#if (CAN_WALK_ON_OB)
	public static bool WALK_ON_OB = true;
#else
	public static bool WALK_ON_OB = false;
#endif
	
#if (CAN_FLY_ANYWHERE)
	public static bool FLY_ANYWHERE = true;
#else
	public static bool FLY_ANYWHERE = false;
#endif
	
#if (_FACEBOOK_DEBUG)
	public static bool FACEBOOK_DEBUG = true;
#else
	public static bool FACEBOOK_DEBUG = false;
#endif

#if (_FACEBOOK_RESPONSEFIX)
        public static bool FACEBOOK_RESPONSEFIX = true;
#else
	    public static bool FACEBOOK_RESPONSEFIX = false;
#endif

#if _LOAD_LOCALHOST_FONT
	public static bool LOAD_LOCALHOST_FONT = true;
#else
	public static bool LOAD_LOCALHOST_FONT = false;
#endif

#if (_HIDE_PASSWORD)
    public static bool HIDE_PASSWORD = true;
#else
	public static bool HIDE_PASSWORD = false;
#endif

#if (_DISPLAY_GROUND_TARGET)
	public static bool DISPLAY_GROUND_TARGET = true;
#else
	public static bool DISPLAY_GROUND_TARGET = false;
#endif

#if (_MEMORY_WINDOW)
    public static bool MEMORY_WINDOW = true;
#else
	public static bool MEMORY_WINDOW = false;
#endif

#if (_MOUSE_POSITION)
    public static bool MOUSE_POSITION = true;
#else
	public static bool MOUSE_POSITION = false;
#endif
	
#if (_SYSCONFIG_DEBUG)
	public static bool _SYSCONFIG_DEBUG = true;
	public static bool _NoTestServer = true;//false 為測勢server,意旨client不黨(但要有訊息), 反之亦然(正式版要設true)
#else
	public static bool _SYSCONFIG_DEBUG = false;
	public static bool _NoTestServer = true;
#endif
	
#if (_EVENT_FLOW_RECORD)
	public static bool EVENT_FLOW_RECORD = true;
#else
	public static bool EVENT_FLOW_RECORD = false;
#endif
	
#if (_DISPLAY_PLAYER_POSITION)
	public static bool DISPLAY_PLAYER_POSITION = true;
#else
	public static bool DISPLAY_PLAYER_POSITION = false;
#endif

#if (DEBUG_RAYCAST)
	public static bool DEBUG_RAYCAST = true;
#else
	public static bool DEBUG_RAYCAST = false;
#endif
	
#if _USE_CIRCLE_SHADOW
	public static bool USE_CIRCLE_SHADOW = true;
#else
	public static bool USE_CIRCLE_SHADOW = false;
#endif
	
#if _MEMORY_USAGE_TEST
	public static bool MEMORY_USAGE_TEST = true;
#else
	public static bool MEMORY_USAGE_TEST = false;
#endif

#if (LOCAL_SERVER)
	public static bool LOCAL_SERVER = true;
#else
	public static bool LOCAL_SERVER = false;
#endif

#if _FILTERMODE_POINT //圖filtermode 不變更為 bilinear
	public static bool FILTERMODE_POINT = true;
#else
	public static bool FILTERMODE_POINT = false;
#endif

#if (_MICRO_CLIENT_VERSION)
	public static bool MICRO_CLIENT_VERSION = true;
#else
	public static bool MICRO_CLIENT_VERSION = false;
#endif

#if(_SKIP_FTP_CHECK)
	public static bool SKIP_FTP_CHECK = true;
#else
	public static bool SKIP_FTP_CHECK = false;
#endif

#if _BINARY_SERIALIZE
	public static byte DATA_SERIALIZE = 3;//BINARY
#elif _JSON_SERIALIZE
	public static byte DATA_SERIALIZE = 2;//JSON
#else
	public static byte DATA_SERIALIZE = 1;//XML
#endif
#if PROGRAM_END_DRAMA
	public static bool PROGRAM_END_DRAMA = true;
#else
	public static bool PROGRAM_END_DRAMA = false;
#endif
#if _READDATA
	public static bool READDATA = true;//BINARY
#else
	public static bool READDATA = false;//BINARY
#endif
#if CREATE_CHARACTER_ACTIVITY
	public static bool CREATE_CHARACTER_ACTIVITY = true;
#else
	public static bool CREATE_CHARACTER_ACTIVITY = false;
#endif

#if DEBUG_PROTOCOL_AREA
	public static bool DebugProtocolArea = true;
#else
	public static bool DebugProtocolArea = false;
#endif

#if DEBUG_PROTOCOL_COMBAT
	public static bool DebugProtocolCombat = true;
#else
	public static bool DebugProtocolCombat = false;
#endif

#if DEBUG_PROTOCOL_SKILLCOMBAT
	public static bool DebugProtocolSkillCombat = true;
#else
	public static bool DebugProtocolSkillCombat = false;
#endif

#if DEBUG_PROTOCOL_MOVE
	public static bool DebugProtocolMove = true;
#else
	public static bool DebugProtocolMove = false;
#endif

#if DEBUG_PROTOCOL_STATE
	public static bool DebugProtocolState = true;
#else
	public static bool DebugProtocolState = false;
#endif

#if DEBUG_PROTOCOL_TOTEM
	public static bool DebugProtocolTotem = true;
#else
	public static bool DebugProtocolTotem = false;
#endif

#if DEBUG_SCENEMANAGER
	public static bool DebugSceneManager = true;
#else
	public static bool DebugSceneManager = false;
#endif
	
#if DEBUG_EYESIGHT
	public static bool DebugEyeSightMsg = true;
#else
	public static bool DebugEyeSightMsg = false;
#endif
	
#if DEBUG_PROTOCOL_TEAM
	public static bool DebugProtocolTeam = true;
#else
	public static bool DebugProtocolTeam = false;
#endif	

#if DEBUG_PROTOCOL_LEADERBOARD
	public static bool DebugProtocolLeaderBoard = true;
#else
	public static bool DebugProtocolLeaderBoard = false;
#endif

#if DEBUG_PROTOCOL_SUNDRY
	public static bool DebugProtocolSundry = true;
#else
	public static bool DebugProtocolSundry = false;
#endif

#if DEBUG_PROTOCOL_NPC
	public static bool DebugProtocolNPC = true;
#else
	public static bool DebugProtocolNPC = false;
#endif

#if DEBUG_PROTOCOL_EVENT
	public static bool DebugProtocolEvent = true;
#else
	public static bool DebugProtocolEvent = false;
#endif
	
#if _DEBUG_UNIT_HIDDEN
	public static bool DEBUG_UNIT_HIDDEN = true;
#else
	public static bool DEBUG_UNIT_HIDDEN = false;
#endif
	
#if CLOSE_BETA
	public static bool CLOSE_BETA = true;
#else
	public static bool CLOSE_BETA = false;
#endif
	
#if SHOW_VERSION_NUMBER
	public static bool SHOW_VERSION_NUMBER = true;
#else
	public static bool SHOW_VERSION_NUMBER = false;
#endif

#if _SHOW_FPS	//顯示FPS
	public static bool SHOW_FPS = true;
#else
	public static bool SHOW_FPS = false;
#endif

#if _DOWNLOAD_LOG_ENABLE			//是否開啟下載日誌功能
	public static bool DOWNLOAD_LOG_ENABLE = true;
#else
	public static bool DOWNLOAD_LOG_ENABLE = false;
#endif

#if _PRELOAD					//是否背景預載
	public static bool PRELOAD = true;
#else
	public static bool PRELOAD = false;
#endif

#if _CACHING_ENABLE					//是否使用開啟快取功能 (include Unity Caching Licence)
	public static bool CACHING_ENABLE = true;
#else
	public static bool CACHING_ENABLE = false;
#endif

#if _START_LOADING					//是否前導包下載
	public static bool START_LOADING = true;
#else
	public static bool START_LOADING = false;
#endif

#if _UNIT_HIDDEN_CACHE
	public static bool UNIT_HIDDEN_CACHE = true;
#else
	public static bool UNIT_HIDDEN_CACHE = false;
#endif

#if USE_SIMPLE_MODEL
	public static bool UseSimpleModel = true;
#else
	public static bool UseSimpleModel = false;
#endif

#if CLOSE_DOWNLOAD_FINISH_QUEUE
	public static bool CloseDownloadFinishQueue = true;
#else
	public static bool CloseDownloadFinishQueue = false;
#endif
}
