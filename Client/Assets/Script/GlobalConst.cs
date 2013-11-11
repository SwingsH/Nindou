using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 全域 const 變數皆放置於此
/// </summary>
public static class GLOBALCONST
{
	public const string GAME_MAIN_VERSION = "1";

	public class GameSetting
	{
		public const float UNIT_CAMERA_SIZE = 768;
		public const int LAYER_UNIT = 8;
		public const int LAYER_BACKGROUND = 9;
		public const int LAYER_EXTRAUNIT = 10;
		public const int LAYER_EXTRAEFFECT = 11;

		public const float TELEPORT_DELAY = 3.5f;
		public static readonly Vector2 GRID_SIZE = new Vector2(8, 6);
		public const int GRID_COUNT_W = 9;
		public const int GRID_COUNT_L = 5;
		public const float ENTITY_SCALE_RATE = 0.5f;
		public const int ENEMY_MAX_NUMBER = 5;
	}
	public static class BattleSettingValue
	{
		public const float ATTACK_RANDOMRATE_MIN = 0.95f;
		public const float ATTACK_RANDOMRATE_MAX = 1.05f;

		public const ushort DEFAULT_NORMAL_ATTACK = 0;
		public const float CRITICAL_BONUS =0.5f;
		public const int AllInRangeModeGroup = 10;

        public const float AVATAR_DAMAGE_COLOR_TIME = 0.2f; //受擊後變色效果持續時間(秒)
        public const float AVATAR_DAMAGE_SHAKE_RANGE = 10.0f; //受擊後震動效果偏移值 X 

		public static readonly Color FONT_PLAYER_DAMAGE_COLOR = Color.red;
        public static readonly Color FONT_ENEMY_DAMAGE_COLOR = Color.white;

        public static readonly Color AVATAR_DAMAGE_COLOR = CommonFunction.Color256Bit(200, 100, 100, 255); //受擊後變色效果
        public static readonly Color AVATAR_NORMAL_COLOR = CommonFunction.Color256Bit(255, 255, 255, 255); 
    }
	public const string TAG_BACKGROUND = "BackGround";
	public const string TAG_GROUND = "Ground";
	public const string BACKGROUND_MATERIAL_POSTFIX = "_BackGround";
	public const string GROUND_MATERIAL_POSTFIX = "_Ground";


	public const int TOTAL_BONE_NUMBER = 11;
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
	public const string EYES = "Eyes";
	public const string HAIR = "Hair";
	public const string HEADDRESS = "Headdress";
	public static readonly string[] BONE_NAME = new string[]
	{
		HEAD,
		EYES,
		HAIR,
		HEADDRESS,
		BODY,
		HAND_LEFT,
		HAND_RIGHT,
		LEG_LEFT,
		LEG_RIGHT,
		WEAPON_LEFT,
		WEAPON_RIGHT,
	};

	/// <summary>
	/// 對應到BONE_NAME的index
	/// </summary>
 	public enum eModelPartName:int
	{
		HEAD,
		EYES,
		HAIR,
		HEADDRESS,
		BODY,
		HAND_LEFT,
		HAND_RIGHT,
		LEG_LEFT,
		LEG_RIGHT,
		WEAPON_LEFT,
		WEAPON_RIGHT,
	}
	public static string GetBoneName(this eModelPartName ePartName)
	{
		return BONE_NAME[(int)ePartName];
	}

    public const int MAX_BATTLE_ROLE_COUNT = 4; // 戰鬥時的角色數量上限
    public const int MAX_EQUIP_COUNT = 5; // 角色最大裝備上限

    public const float BATTLE_START_COOL_DOWN = 3.6f; // 戰鬥開場的停止時間

    #region UI相關設定
    public const int LAYER_UI_BASE = 31; // 基本ui layer
    public const int UI_ShowOrHide_TweenGroup = 1; // 顯示/消失的Tween的Group （自動生成的tween (EX:按鈕變色）的group會是0，故設定為1，若有其他要設定的請避開。）
    public const int UI_Battle_Start_TweenGroup = 2;

    public const int MAX_LAYER_OF_SUBUI_HP = 5; // HP Layer數最大值

    public const int UI_CAMERA_DEPTH = 7; // UI_Camera預設深度

    public const string DIR_RESOURCES_DATA = "Data/";
    public const string DIR_RESOURCES_PARTICLE = "Particle/";
    public const string DIR_RESOURCES_ATLAS = "Atlas/";
    public const string DIR_RESOURCES_AVATAR_BONE = "BasicBone/";
	public const string DIR_RESOURCES_BACKGROUND = "BackGround/";

    #endregion

    #region 檔名相關常數（無副檔名）
    // TODO: 下面目前都是為了測試用，確認要用的資料後，請將不需要的刪除，順道把本行也刪除了
    public const string FILENAME_SCENE = "SceneData";   // 場景資料
	public const string FILENAME_SKILL = "SkillData"; //技能資料 
	public const string FILENAME_NPC = "NpcData";       // NpcData
    public const string FILENAME_AREA = "Area";             // 區域資料
    public const string FILENAME_AREA_EVENT = "AreaEvent"; // 區域事件資料
    public const string FILENAME_STORY = "Story";           // 劇情資料
    public const string FILENAME_BATTLE = "Battle";           // 戰鬥配置資料
	public const string FILENAME_SKILLEFFECT = "SkillExtraEffect"; //技能附加效果
    public const string FILENAME_ITEM = "ItemData";             //物品(卡片)

    #endregion
    #region 副檔名相關常數
    public static readonly string EXT_ASSETBUNDLE = ".unity3d";
    public static readonly string EXT_JSON = ".json";
    public static readonly string EXT_BYTES = ".bytes";
    public static readonly string EXT_ASSET = ".asset";
    #endregion
    #region 路徑相關常數
    public static readonly string DIR_ASSETS = "Assets/";
    public static readonly string DIR_RESOURCES = "Resources" + Path.AltDirectorySeparatorChar;
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
        [EnumClassValue(typeof(SceneData), FILENAME_SCENE)]				Scene = 0, // 場景
		[EnumClassValue(typeof(SkillData), FILENAME_SKILL)]				Skill,       // 技能
		[EnumClassValue(typeof(NPCData), FILENAME_NPC)]					NPC,         // NpcData
        [EnumClassValue(typeof(Area), FILENAME_AREA)]					Area,        // 區域
        [EnumClassValue(typeof(AreaEvent), FILENAME_AREA_EVENT)]		AreaEvent,   // 區域事件
        [EnumClassValue(typeof(Story), FILENAME_STORY)]					Story,       // 劇情
        [EnumClassValue(typeof(Battle), FILENAME_BATTLE)]				Battle,       // 戰鬥配置表      
 		[EnumClassValue(typeof(SpecialEffect), FILENAME_SKILLEFFECT)]	SPEffect,       // 特殊狀態表
        [EnumClassValue(typeof(ItemData), FILENAME_ITEM)]	            Item,           // 物品表
    };
}

