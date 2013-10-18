using UnityEngine;
using System.Collections;
using SmoothMoves;
using System.Collections.Generic;

public class UnitGenerater{
	Dictionary<string, List<SmoothMoves.BoneAnimation>> UnitGrave = new Dictionary<string, List<SmoothMoves.BoneAnimation>>();
	
	public Unit GenerateUnit(UnitInfo info)
	{
		return GenerateUnit(info, false);
	}
	public Unit GenerateUnit(UnitInfo info, bool isPreview)
	{
		if (info == null)
			return null;
		ActionUnit au = new ActionUnit(isPreview);
		List<MainSkill> activeSkill = new List<MainSkill>();
		au.NormalAttack = new MainSkill(TestDataBase.Instance.GetSkillData(info.AttackID));
		foreach (ushort skillID in info.SkillID)
		{
			activeSkill.Add(new MainSkill(TestDataBase.Instance.GetSkillData(skillID)));
		}
		au.ActiveSkills = activeSkill;
		au.MaxLife = info.MaxLife;
		au.Life = info.MaxLife;
		au.MoveSpeed = info.MoveSpeed;
		if (info.MoveMode == 1)
			au.MoveAction = new TeleportInRangeComponent();
		else
			au.MoveAction = new MoveInRangeComponent();
		au.Entity = GenerateEntity(info.BoneName, info.spriteNames);

		return au;
	}
	public GameObject GenerateEntity(string BoneAnimName, string[] spriteInfo)
	{
		SmoothMoves.BoneAnimation ba;
		//= ResourceStation.GetBone(BoneAnimName);
		List<SmoothMoves.BoneAnimation> tempList;
		if (UnitGrave.TryGetValue(BoneAnimName, out tempList) && tempList.Count > 0)
		{
			ba = tempList[0];
			ba.gameObject.SetActive(true);
			tempList.RemoveAt(0);
			ba.gameObject.name = BoneAnimName;
		}
		else
			ba = ResourceStation.GetBone(BoneAnimName);
		GenerateModelSprite(ba, spriteInfo);
		GameObject go = new GameObject(BoneAnimName);
		ba.transform.parent = go.transform;
		ba.transform.localRotation = Quaternion.identity;
		ba.transform.localPosition = new Vector3(0, 125, 0);
		return go;
	}
	//public static Sprite[] GetSprites(BoneAnimation boneData)
	//{
	//    Sprite[] result = new Sprite[GLOBALCONST.BONE_NAME.Length];
	//    for (int i = 0; i < GLOBALCONST.BONE_NAME.Length; i++)
	//    {
	//        string boneName = GLOBALCONST.BONE_NAME[i];

	//        Transform spriteTrans = boneData.GetSpriteTransform(boneName);
	//        if (spriteTrans == null)
	//            continue;
	//        Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();

	//        if (sprite == null)
	//            sprite = spriteTrans.gameObject.AddComponent<Sprite>();
	//        sprite.SetPivotOffset(Vector2.zero, true);
	//        //sprite.useDefaultPivot = true;
	//        result[i] = sprite;
	//    }
	//    return result;
	//}
	public static void GenerateModelSprite(SmoothMoves.BoneAnimation boneData, string[] atlasInfo)
	{
		if (boneData == null || atlasInfo == null)
			return;
		for (int i = 0; i < atlasInfo.Length && i < GLOBALCONST.BONE_NAME.Length; i++)
		{
			string boneName = GLOBALCONST.BONE_NAME[i];
			string atlasName = atlasInfo[i];

			Transform spriteTrans = boneData.GetSpriteTransform(boneName);
			if (spriteTrans == null)
				continue;
			TextureAtlas atlas = ResourceStation.GetAtlas(atlasName);
			Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();
			
			if (sprite == null)
				sprite = spriteTrans.gameObject.AddComponent<Sprite>();
			sprite.SetAtlas(atlas);
			sprite.SetTextureName(boneName);
			//sprite.useDefaultPivot = true;←這個沒有用，要用下面這個才行
			sprite.SetPivotOffset(Vector2.zero, true);
		}
	}
	public void Recycle(Unit unit)
	{
		AnimUnit au = unit as AnimUnit;
		if (au == null || au.Anim == null)
			return;
		au.Anim.transform.parent = null;
		foreach (SmoothMoves.Sprite s in au.Anim.GetComponentsInChildren<SmoothMoves.Sprite>())
		{
			s.SetTextureName(string.Empty);
			s.SetAtlas(null);
		}
		List<SmoothMoves.BoneAnimation> tempList;
		if (!UnitGrave.TryGetValue(au.Anim.name, out tempList))
		{
			tempList = new List<SmoothMoves.BoneAnimation>();
			UnitGrave.Add(au.Anim.name, tempList);
		}
		if(!tempList.Contains(au.Anim))
			tempList.Add(au.Anim);
		//au.Anim.name = au.Anim.GetInstanceID().ToString();
		au.Anim.transform.localScale = Vector3.one;
		au.Anim.gameObject.SetActive(false);
		if(au.Entity != null)
			GameObject.Destroy(au.Entity);

		unit.ClearReference();
	}

	public void ClearGrave()
	{
		foreach (List<SmoothMoves.BoneAnimation> baLIst in UnitGrave.Values)
		{
			foreach (SmoothMoves.BoneAnimation ba in baLIst)
			{
				GameObject.Destroy(ba);
			}
		}
		UnitGrave.Clear();
	}
}
