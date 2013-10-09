using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
public class TestDataBase{
	static TestDataBase _Instance;
	public static TestDataBase Instance
	{
		get
		{
			if (_Instance == null)
			    _Instance = new TestDataBase();
			return _Instance;
		}
	}
	public static void IniInstance()
	{
		if (_Instance == null)
			_Instance = new TestDataBase();
	}
	Dictionary<ushort, SkillData> SkillDataBase;
	Dictionary<uint, NPCData> NpcDataBase;
	Dictionary<uint, List<Battle>> BattleDatas;
	public TestDataBase()
	{
		SkillDataBase = LoadData<ushort, SkillData>(GLOBALCONST.FILENAME_SKILL, "ID");
		NpcDataBase = LoadData<uint, NPCData>("npcdata", "NPCID");
		BattleDatas = new Dictionary<uint,List<Battle>>();
		TextAsset ta = Resources.Load("Data/" + GLOBALCONST.FILENAME_BATTLE, typeof(TextAsset)) as TextAsset;
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
		playerInfo[tempIndex].SkillID = new ushort[] { 2, 3 };
		playerInfo[tempIndex].MaxLife = 300;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].spriteNames = new string[] { TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex],
				TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex] };
		tempIndex = 1;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 1,2};
		playerInfo[tempIndex].MaxLife = 250;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].spriteNames = new string[] { TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex],
				TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex] };
		tempIndex = 2;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 7,100 };
		playerInfo[tempIndex].MaxLife = 200;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].spriteNames = new string[] { TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex],
				TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex] };
		tempIndex = 3;
		playerInfo[tempIndex] = new UnitInfo();
		playerInfo[tempIndex].AttackID = 6;
		playerInfo[tempIndex].SkillID = new ushort[] { 8 };
		playerInfo[tempIndex].MaxLife = 200;
		playerInfo[tempIndex].MoveMode = 1;
		playerInfo[tempIndex].MoveSpeed = 3;
		playerInfo[tempIndex].spriteNames = new string[] { TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex],
				TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex], TestDataBase.TestAtlasName[tempIndex] };
		#endregion

	}
	public SkillData GetSkillData(ushort SkillID)
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
	public UnitInfo[] playerInfo = new UnitInfo[4];
	public static Dictionary<KeyType, DataType> LoadData<KeyType, DataType>(string dataName,string KeyFieldName)
	{
		Dictionary<KeyType, DataType> result = new Dictionary<KeyType, DataType>();
		object obj = new List<DataType>();
		TextAsset ta = Resources.Load("Data/" + dataName, typeof(TextAsset)) as TextAsset;
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
}
