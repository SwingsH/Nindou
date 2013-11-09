using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
public class InformalDataBase{
	static InformalDataBase _Instance;
	public static InformalDataBase Instance
	{
		get
		{
			if (_Instance == null)
			    _Instance = new InformalDataBase();
			return _Instance;
		}
	}
	public static void IniInstance()
	{
		if (_Instance == null)
			_Instance = new InformalDataBase();
	}
	Dictionary<uint, SkillData> SkillDataBase;
	Dictionary<uint, NPCData> NpcDataBase;
	Dictionary<uint, List<Battle>> BattleDatas;
	Dictionary<uint, SpecialEffect> SPEffectDatas;
    Dictionary<uint, ItemData> ItemDataBase; // todo, 移到 table manager  sh131108

	public InformalDataBase()
	{
        ItemDataBase = LoadData<uint, ItemData>(GLOBALCONST.FILENAME_ITEM, "ID");
		SkillDataBase = LoadData<uint, SkillData>(GLOBALCONST.FILENAME_SKILL, "ID");
		//SkillDataBase[5].Range = 1;
		//SkillDataBase[5].RangeMode = 3;

		//SkillDataBase[6].Range = 1;
		//SkillDataBase[6].RangeMode = 3;
		NpcDataBase = LoadData<uint, NPCData>(GLOBALCONST.FILENAME_NPC, "NPCID");
		SPEffectDatas = LoadData<uint, SpecialEffect>(GLOBALCONST.FILENAME_SKILLEFFECT, "ID");
		BattleDatas = new Dictionary<uint,List<Battle>>();
        TextAsset ta = Resources.Load(GLOBALCONST.DIR_RESOURCES_DATA + GLOBALCONST.FILENAME_BATTLE, typeof(TextAsset)) as TextAsset;
		object obj = new List<Battle>();
		if (ta)
		{
			DataUtility.DeserializeObject(ta.text, ref obj);
			foreach (Battle bd in obj as List<Battle>)
			{
				List<Battle> tempList;
				if (!BattleDatas.TryGetValue(bd.ID, out tempList))
				{
					tempList = new List<Battle>();
					BattleDatas.Add(bd.ID, tempList);
				}
				tempList.Add(bd);
			}
		}
		#region Test Player Info
		int tempIndex = 0;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 0;
		playerInfo[tempIndex].SkillID = new ushort[] { 1, 5 };
		playerInfo[tempIndex].MaxLife = 300;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].BoneName = TestBoneAnimationName[0];
		playerInfo[tempIndex].spriteNames = new string[GLOBALCONST.TOTAL_BONE_NUMBER];
		for (int i = 0; i < playerInfo[tempIndex].spriteNames.Length; i++)
			playerInfo[tempIndex].spriteNames[i] = InformalDataBase.TestPlayAtlasName[1];

		tempIndex = 1;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 1, 5 };
		playerInfo[tempIndex].MaxLife = 250;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].BoneName = TestBoneAnimationName[0];
		playerInfo[tempIndex].spriteNames = new string[GLOBALCONST.TOTAL_BONE_NUMBER];
		for (int i = 0; i < playerInfo[tempIndex].spriteNames.Length; i++)
			playerInfo[tempIndex].spriteNames[i] = InformalDataBase.TestPlayAtlasName[1];
		playerInfo[tempIndex].spriteNames[1] = InformalDataBase.TestPlayAtlasName[1];
		tempIndex = 2;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 9, 10 };
		playerInfo[tempIndex].MaxLife = 200;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].BoneName = TestBoneAnimationName[0];
		playerInfo[tempIndex].spriteNames = new string[GLOBALCONST.TOTAL_BONE_NUMBER];
		for (int i = 0; i < playerInfo[tempIndex].spriteNames.Length; i++)
			playerInfo[tempIndex].spriteNames[i] = InformalDataBase.TestPlayAtlasName[1];
		playerInfo[tempIndex].spriteNames[1] = InformalDataBase.TestPlayAtlasName[1];
		tempIndex = 3;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 11,12 };
		playerInfo[tempIndex].MaxLife = 200;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].BoneName = TestBoneAnimationName[0];
		playerInfo[tempIndex].spriteNames = new string[GLOBALCONST.TOTAL_BONE_NUMBER];
		for (int i = 0; i <= (int)GLOBALCONST.eModelPartName.HEADDRESS; i++)
			playerInfo[tempIndex].spriteNames[i] = InformalDataBase.TestPlayAtlasName[1];
		for (int i =(int)GLOBALCONST.eModelPartName.BODY; i < playerInfo[tempIndex].spriteNames.Length; i++)
			playerInfo[tempIndex].spriteNames[i] = InformalDataBase.TestPlayAtlasName[2];

		playerInfo[tempIndex].spriteNames[1] = InformalDataBase.TestPlayAtlasName[1];
		#endregion

	}
    // todo, 移到 table manager  sh131108
    public ItemData GetItemData(uint ItemID)
    {
        ItemData result;
        if (ItemDataBase == null)
            return null;
        ItemDataBase.TryGetValue(ItemID, out result);
        return result;
    }

	public SkillData GetSkillData(uint SkillID)
	{
		SkillData result;
		if (SkillDataBase == null)
			return null;
		SkillDataBase.TryGetValue(SkillID, out result);
		return result;
	}
	public List<Battle> GetBattleData(uint BattleID)
	{
		if (BattleDatas == null)
			return null;
		List<Battle> result;
		BattleDatas.TryGetValue(BattleID, out result);
		return result;
	}
	public NPCData GetNPCData(uint NPCID)
	{
		NPCData result;
		NpcDataBase.TryGetValue(NPCID, out result);
		return result;
	}
	public SpecialEffect GetSPEffect(uint EffectID)
	{
		SpecialEffect result;
		if (SPEffectDatas.TryGetValue(EffectID, out result))
			return result;
		else
			return new SpecialEffect();
	}
	public UnitInfo[] playerInfo = new UnitInfo[4];
	public static Dictionary<KeyType, DataType> LoadData<KeyType, DataType>(string dataName,string KeyFieldName)
	{
		Dictionary<KeyType, DataType> result = new Dictionary<KeyType, DataType>();
		object obj = new List<DataType>();
        TextAsset ta = Resources.Load(GLOBALCONST.DIR_RESOURCES_DATA + dataName, typeof(TextAsset)) as TextAsset;
		FieldInfo fi = typeof(DataType).GetField(KeyFieldName);
		if(fi == null)
		{
			Debug.LogError("指定的KeyField不存在");
			return result;
		}
		if(fi.FieldType != typeof(KeyType))
		{
			Debug.LogError("指定的KeyField type跟KeyType不同");
			return result;
		}
		if (ta)
		{
			DataUtility.DeserializeObject(ta.text, ref obj);
			foreach (DataType sd in obj as List<DataType>)
			{
				KeyType key = (KeyType)fi.GetValue(sd);
				if (!result.ContainsKey(key))
					result.Add(key, sd);
			}
		}
		else
			Debug.LogError("檔案不存在");
		return result;
	}
	public static string[] TestAtlasName = new string[] { "NindoTestSprite", "GrayKappa", "BlueKappa", "RedKappa" };
	public static string[] TestPlayAtlasName = new string[] {"Basic", "Monotaro","NinJaBlack" };
	public static string[] TestBoneAnimationName = new string[] { "BasicFaceLeft" };
}
