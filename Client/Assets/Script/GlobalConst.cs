using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 全域 const 變數皆放置於此
/// </summary>
public class GLOBALCONST
{
	public const string GAME_MAIN_VERSION = "1";

	public class GameSetting
	{
		public const float UNIT_CAMERA_SIZE = 768;
		public const int LAYER_UNIT = 8;
		public const int LAYER_BACKGROUND = 9;

		public const float TELEPORT_DELAY = 3.5f;
		public static readonly Vector2 GRID_SIZE = new Vector2(8, 6);
		public const int GRID_COUNT_W = 9;
		public const int GRID_COUNT_L = 5;
	}
	public static class BattleSettingValue
	{
		public const ushort DEFAULT_NORMAL_ATTACK = 0;
		public const float CRITICAL_BONUS =0.5f;
		public const int AllInRangeModeGroup = 10;

        public const float AVATAR_DAMAGE_COLOR_TIME = 0.2f; //受擊後變色效果持續時間(秒)
        public const float AVATAR_DAMAGE_SHAKE_RANGE = 10.0f; //受擊後震動效果偏移值 X 
        public static readonly Color FONT_DAMAGE_COLOR = Color.white;
        public static readonly Color AVATAR_DAMAGE_COLOR = CommonFunction.Color256Bit(200, 100, 100, 255); //受擊後變色效果
        public static readonly Color AVATAR_NORMAL_COLOR = CommonFunction.Color256Bit(255, 255, 255, 255); 
    }

	public const string HAND_LEFT = "HandL";
	public const string HAND_RIGHT = "HandR";
	public const string HEAD = "Head";
	public const string LEG_LEFT = "LegL";
	public const string LEG_RIGHT = "LegR";
	public const string BODY = "Body";
	public const string WEAPON_LEFT = "WeaponL";
	public const string WEAPON_RIGHT = "WeaponR";

    public const string BONE_ROOT_NAME = "Root"; // Bone 資訊的主節點 GameObject name
    public const string UNIT_NAME_ENEMY = "Enemy_{0}_{1}";
    public const string UNIT_NAME_PLAYER = "Player_{0}";

	public static readonly string[] BONE_NAME = new string[]
	{
		HEAD,
		BODY,
		HAND_LEFT,
		HAND_RIGHT,
		LEG_LEFT,
		LEG_RIGHT,
		WEAPON_LEFT,
		WEAPON_RIGHT,
	};

    #region UI相關設定
    public const int LAYER_UI_BASE = 31; // 基本ui layer
    // 戰鬥UI
    public const int UI_BATTLE_ROLE_ICON_COUNT = 4; // 戰鬥UI會顯示的角色ICON數量

    public const string DIR_RESOURCES_DATA = "Data/";
    public const string DIR_RESOURCES_PARTICLE = "Particle/";
    public const string DIR_RESOURCES_ATLAS = "Atlas/";
    public const string DIR_RESOURCES_AVATAR_BONE = "BasicBone/";

    // 使用的Atlas名，先集中在此
    public const string ATLAS_SLICES = "Atlas_Slices"; // 有切九宮格的圖片存在的Atlas
    public const string ATLAS_ICONS = "Atlas_Icons";   // 存放icon的Atlas
    public const string ATLAS_BACKGROUNDS = "Atlas_Backgrounds"; // 存放背景的Atlas
    public const string ATLAS_MAIN = "UI_Main_Atlas"; // 主介面用圖的Atlas
    // 測試用的Atlas名，之後要將其刪除，改用美術來的圖
    public const string ATLAS_TEST = "TestAtlas";
    public const string ATLAS_TEST2 = "TestAtlas2";
    public const string ATLAS_SCIFI = "SciFi Atlas";
    // 使用的Sprite名，先集中在此
    public const string SPRITE_PARCHMENT = "slice_parchment";
    public const string SPRITE_FRAME_LIGHTBROWN = "slice_frame_lightbrown";
    public const string SPRITE_FRAME_DARKBROWN = "slice_frame_darkbrown";
    public const string SPRITE_BUTTON_GREY = "slice_button_grey";
    public const string SPRITE_ICON_PERSON = "icon_person";
    public const string SPRITE_ICON_BAG = "icon_backpape";
    public const string SPRITE_ICON_STORE = "icon_store";
    public const string SPRITE_ICON_FRIEND = "icon_friend";
    public const string SPRITE_ICON_EXPLORE = "complete";
    public const string SPRITE_NINDOU_BG = "temp_nindou_bg";
    public const string SPRITE_WORLDMAP_BG = "shape1170";
    public const string SPRITE_WORLDMAP = "nindou_3_bg";
    public const string SPRITE_STAGE_TITLE = "title";
    public const string SPRITE_ICON_CLOSE = "close";
    public const string SPRITE_BUTTON_STAGE = "Night_Blade_1";
    public const string SPRITE_BOSS_PIC = "bosspic";
    // 測試用的Sprite名，之後要將其刪除，改用美術來的圖
    public const string SPRITE_TEST_BUTTON_BACK = "button_back";
    public const string SPRITE_TEST_FAST_FORWARD = "Fast-forward";
    public const string SPRITE_TEST_PAUSE = "pause";
    public const string SPRITE_TEST_BATTLE_ICON_BG = "pachuri";
    public const string SPRITE_TEST_PLAYER_ICON = "chiruno";
    public const string SPRITE_TEST_PLAYER_HEAD = "pachuri";
    public const string SPRITE_TEST_POINT_GRAPH = "gold";
    public const string SPRITE_TEST_BUTTON_INHERIT = "fb_300_main";
    public const string SPRITE_TEST_PROGRESS_BG = "Dark";
    public const string SPRITE_TEST_PROGRESS_FG = "Light";
    #endregion

    #region 檔名相關常數（無副檔名）
    // TODO: 下面目前都是為了測試用
    public const string FILENAME_SCENE = "SceneData";   // 場景資料
	public const string FILENAME_SKILL = "SkillData"; //技能資料 
	public const string FILENAME_NPC = "NpcData";       // NpcData
    public const string FILENAME_AREA = "Area";             // 區域資料
    public const string FILENAME_AREA_EVENT = "AreaEvent"; // 區域事件資料
    public const string FILENAME_STORY = "Story";           // 劇情資料
    public const string FILENAME_BATTLE = "Battle";           // 戰鬥配置資料
	public const string FILENAME_SKILLEFFECT = "SkillExtraEffect"; //技能附加效果

    #endregion
    #region 副檔名相關常數
    public static readonly string EXT_ASSETBUNDLE = ".unity3d";
    public static readonly string EXT_JSON = ".json";
    public static readonly string EXT_BYTES = ".bytes";
    public static readonly string EXT_ASSET = ".asset";
    #endregion
    #region 路徑相關常數
    public static readonly string DIR_ASSETS = "Assets/";
    // AssetBundle路徑
    public static readonly string DIR_ASSETBUNDLE = "assetbundles" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_ASSETBUNDLE_JSON = DIR_ASSETBUNDLE + "JSON" + Path.AltDirectorySeparatorChar;
    // NGUI資源相關路徑
    public static readonly string DIR_RESOURCES_NGUI = "NGUI" + Path.AltDirectorySeparatorChar;

    public static readonly string DIR_RESOURCES_NGUI_ATLAS = DIR_RESOURCES_NGUI + "_Atlas" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_RESOURCES_NGUI_FONT = DIR_RESOURCES_NGUI + "_Font" + Path.AltDirectorySeparatorChar;

    // 資料路徑
    public static readonly string DIR_DATA_ROOT = Application.dataPath +  Path.AltDirectorySeparatorChar;
    public static readonly string DIR_DATA_TEMP = DIR_DATA_ROOT + "TEMP" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_DATA_JSON = DIR_DATA_ROOT + "JSON" + Path.AltDirectorySeparatorChar;

    // 編輯器輸出路徑
    public static readonly string DIR_EDITOR_OUTPUT = "NindouOutput" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_EDITOR_ASSETBUNDLE_OUTPUT = DIR_EDITOR_OUTPUT +  DIR_ASSETBUNDLE;

    #endregion
    public enum DataLoadTag
    {
        // for test
        [EnumClassValue(typeof(SceneData), FILENAME_SCENE)]				Scene = 0, // 場景
		[EnumClassValue(typeof(SkillData), FILENAME_SKILL)]				Skill,       // 技能
		[EnumClassValue(typeof(NPCData), FILENAME_NPC)]					NPC,         // NpcData
        [EnumClassValue(typeof(Area), FILENAME_AREA)]					Area,        // 區域
        [EnumClassValue(typeof(AreaEvent), FILENAME_AREA_EVENT)]		AreaEvent,   // 區域事件
        [EnumClassValue(typeof(Story), FILENAME_STORY)]					Story,       // 劇情
        [EnumClassValue(typeof(Battle), FILENAME_BATTLE)]				Battle,       // 戰鬥配置表      
 		[EnumClassValue(typeof(SpecialEffect), FILENAME_SKILLEFFECT)]	SPEffect,       // 特殊狀態表
        
    };
}

