﻿using UnityEngine;
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
	}

	public const string HAND_LEFT = "HandL";
	public const string HAND_RIGHT = "HandR";
	public const string HEAD = "Head";
	public const string LEG_LEFT = "LegL";
	public const string LEG_RIGHT = "LegR";
	public const string BODY = "Body";
	public const string WEAPON_LEFT = "WeaponL";
	public const string WEAPON_RIGHT = "WeaponR";
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

    // TODO: 下面目前都是為了測試用
    #region 檔名相關常數（無副檔名）
    public const string FILENAME_SCENE = "SceneData"; // 場景資料
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
    // 資料路徑
    public static readonly string DIR_DATA_ROOT = Application.dataPath +  Path.AltDirectorySeparatorChar;
    public static readonly string DIR_DATA_TEMP = DIR_DATA_ROOT + "TEMP" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_DATA_JSON = DIR_DATA_ROOT + "JSON" + Path.AltDirectorySeparatorChar;

    // 編輯器輸出路徑
    public static readonly string DIR_EDITOR_OUTPUT = "NindouOutput" + Path.AltDirectorySeparatorChar;
    public static readonly string DIR_EDITOR_ASSETBUNDLE_OUTPUT = DIR_EDITOR_OUTPUT +  DIR_ASSETBUNDLE;

    #endregion
    // for test
    public enum DataLoadTag
    {
        [EnumClassValue(typeof(SceneData), FILENAME_SCENE)]        Scene = 0, // 場景
    };
}

