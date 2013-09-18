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
	
	public Dictionary<ushort, SkillData> SkillDataBase;
	public TestDataBase()
	{

		SkillDataBase = new Dictionary<ushort, SkillData>();
		TextAsset ta = Resources.Load("Data/TestSkillData", typeof(TextAsset)) as TextAsset;
		if (ta != null)
		{
			//System.IO.FileStream fs = new System.IO.FileStream("D:\\Test2.xml", System.IO.FileMode.Open);
			System.IO.StringReader sr = new System.IO.StringReader(ta.text);
			XmlSerializer xs = new XmlSerializer(typeof(SkillData[]));
			try
			{
				SkillData[] sd = xs.Deserialize(sr) as SkillData[];
				foreach (SkillData sdi in sd)
				{
					if (!SkillDataBase.ContainsKey(sdi.ID))
						SkillDataBase.Add(sdi.ID, sdi);
				}
			}
			catch (System.Exception e)
			{
				Debug.Log(e.Message);
			}
			//Resources.UnloadAsset(ta);
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

	public UnitInfo[] playerInfo = new UnitInfo[4];

	public static string[] TestAtlasName = new string[] { "NindoTestSprite", "GrayKappa", "BlueKappa", "RedKappa" };
}
