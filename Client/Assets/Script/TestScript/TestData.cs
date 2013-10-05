using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
	public TestDataBase()
	{
	
		SkillDataBase = new Dictionary<ushort, SkillData>();
		object obj = new List<SkillData>();
		TextAsset ta = Resources.Load("Data/skilldata", typeof(TextAsset)) as TextAsset;
		if(ta)
		{
			Profiler.BeginSample("DeserilizeData");
			DataUtility.DeserializeObject(ta.text, ref obj);
			Profiler.EndSample();
			Profiler.BeginSample("ManagerData");
			foreach (SkillData sd in obj as List<SkillData>)
			{
				if (!SkillDataBase.ContainsKey(sd.ID))
					SkillDataBase.Add(sd.ID, sd);
			}
			
			Profiler.EndSample();
		}
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
	}
	public SkillData GetSkillData(ushort SkillID)
	{
		SkillData result;
		if (SkillDataBase == null)
			return null;
		SkillDataBase.TryGetValue(SkillID, out result);
		return result;
	}
	public UnitInfo[] playerInfo = new UnitInfo[4];

	public static string[] TestAtlasName = new string[] { "NindoTestSprite", "GrayKappa", "BlueKappa", "RedKappa" };
}
