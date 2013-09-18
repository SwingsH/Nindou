using UnityEngine;
using System.Collections;

public class TestSkillPreview : MonoBehaviour {

	Unit currentUnit;
	UnitGenerater generater;
	// Use this for initialization
	void Start () {
		generater = new UnitGenerater();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		for (int i = 0; i < TestDataBase.Instance.playerInfo.Length; i++)
		{
			if (GUI.Button(new Rect(10, 10 + i * 40, 60, 40), i.ToString()))
			{
				if (currentUnit != null)
				{
					generater.Recycle(currentUnit);
				}
				currentUnit = generater.GenerateUnit(TestDataBase.Instance.playerInfo[i]);
			}
		
		}
		if (GUI.Button(new Rect(10, 10 + 4 * 40, 60, 40), "GC"))
			System.GC.Collect(0, System.GCCollectionMode.Forced);
		if (currentUnit != null && currentUnit is ActionUnit)
		{
			ActionUnit au = currentUnit as ActionUnit;
			if (au.NormalAttack != null)
				if (GUI.Button(new Rect(70, 10, 60, 40), au.NormalAttack.Name))
					au.PlaySkill(au.NormalAttack);
			if(au.triggerSkills != null)
				for (int i = 0; i < au.triggerSkills.Count; i++)
				{
					if(au.triggerSkills[i] != null)
						if (GUI.Button(new Rect(70, 50 + i * 40, 60, 40), au.triggerSkills[i].Name))
							au.PlaySkill(au.triggerSkills[i]);
				}
		}

		GUI.Label(new Rect(Screen.width - 60, 10, 60, 40), ActionUnit.totalCount.ToString());
	}
}
