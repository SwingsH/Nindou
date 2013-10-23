using UnityEngine;
using System.Collections;
using SmoothMoves;
using System.Collections.Generic;

public class UnitGenerater{
	Dictionary<string, List<SmoothMoves.BoneAnimation>> UnitGrave = new Dictionary<string, List<SmoothMoves.BoneAnimation>>();

	Dictionary<BoneAnimation, Sprite[]> BoneSprites = new Dictionary<BoneAnimation, Sprite[]>();
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
		au.NormalAttack = new MainSkill(InformalDataBase.Instance.GetSkillData(info.AttackID));
		foreach (ushort skillID in info.SkillID)
		{
			activeSkill.Add(new MainSkill(InformalDataBase.Instance.GetSkillData(skillID)));
		}
		au.ActiveSkills = activeSkill;
		au.MaxLife = info.MaxLife;
		au.Life = info.MaxLife;
		au.MoveSpeed = info.MoveSpeed;
		if (info.MoveMode == 1)
			au.MoveAction = new TeleportInRangeComponent();
		else
			au.MoveAction = new MoveInRangeComponent();
		Sprite[] sprites;
		au.Entity = GenerateEntity(info.BoneName, info.spriteNames, out sprites);
		au.Sprites = sprites;
		if(au.Entity != null)
			au.SpriteBounds = GetSpriteBounds(au.Entity.transform, sprites);
		return au;
	}
	public GameObject GenerateEntity(string BoneAnimName, string[] spriteInfo ,out Sprite[] sprites)
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
		if (ba == null)
		{
			Debug.LogError("Cant Find Bone Data");
			sprites = null;
			return null;
		}
		if (!BoneSprites.TryGetValue(ba, out sprites))
		{
			sprites = GenerateModelSprite(ba);
			BoneSprites.Add(ba, sprites);
		}
		SetModelSprite(sprites, spriteInfo);
		
		ba.transform.localRotation = Quaternion.identity;
		ba.transform.localPosition = new Vector3(0, 125, 0);
		ba.transform.localScale = Vector3.one;
		return ba.gameObject;
	}
	/// <summary>
	/// 設定角色每個部位的圖片，陣列資料的部位順序跟長度要跟GLOBALCONST.BONE_NAME裡的一樣
	/// </summary>
	/// <param name="sprites">Sprite元件</param>
	/// <param name="spriteInfo">圖片名稱</param>
	public static void SetModelSprite(Sprite[] sprites, string[] spriteInfo)
	{
		if (sprites == null || spriteInfo == null || sprites.Length != spriteInfo.Length)
		{
			Debug.LogError("Set Sprite Data Error");
			return;
		}
		for(int i = 0 ; i < sprites.Length ; i++)
		{
			if(sprites[i] == null)
				continue;
			string boneName = GLOBALCONST.BONE_NAME[i];

			TextureAtlas atlas = ResourceStation.GetAtlas(spriteInfo[i]);
			sprites[i].SetAtlas(atlas);
			sprites[i].SetTextureName(boneName);
			//sprites[i].useDefaultPivot = true;←這個沒有用，要用下面這個才行
			sprites[i].SetPivotOffset(Vector2.zero, true);
		}
	}
	//建立每個部份的sprite元件
	public static Sprite[] GenerateModelSprite(SmoothMoves.BoneAnimation boneData)
	{
		if (boneData == null)
			return new Sprite[0];
		Sprite[] result = new Sprite[GLOBALCONST.BONE_NAME.Length];
		for (int i = 0; i < result.Length; i++)
		{
			string boneName = GLOBALCONST.BONE_NAME[i];

			Transform spriteTrans = boneData.GetSpriteTransform(boneName);
			if (spriteTrans == null)
				continue;
			Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();
			
			if (sprite == null)
				sprite = spriteTrans.gameObject.AddComponent<Sprite>();
			//sprite.useDefaultPivot = true;←這個沒有用，要用下面這個才行
			sprite.SetPivotOffset(Vector2.zero, true);
			result[i] = sprite;
		}
		return result;
	}

	//計算所有sprite（不包含武器）合成的Bounds
	Bounds GetSpriteBounds(Transform baseTransform, Sprite[] sprites)
	{
		if (baseTransform == null || sprites == null)
			return new Bounds();
		Vector3 MaxV3 = new Vector3(float.NegativeInfinity,float.NegativeInfinity,float.NegativeInfinity);
		Vector3 MinV3 = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		//0 ~ 5 為頭手身體腳，不包含武器
		for (int i = 0; i < sprites.Length && i < 6; i++)
		{
			if (sprites[i] == null)
				continue;
			Vector3 spriteMin = baseTransform.transform.InverseTransformPoint(sprites[i].transform.TransformPoint(sprites[i]._bottomLeft));
			Vector3 spriteMax = baseTransform.transform.InverseTransformPoint(sprites[i].transform.TransformPoint(sprites[i]._topRight));
			MinV3 = Vector3.Min(MinV3, spriteMin);
			MaxV3 = Vector3.Max(MaxV3, spriteMax);
		}

		return new Bounds(Vector3.Lerp(MinV3, MaxV3, 0.5f), MaxV3 - MinV3);
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
		unit.ClearReference();
	}

	public void ClearGrave()
	{
		foreach (List<SmoothMoves.BoneAnimation> baLIst in UnitGrave.Values)
		{
			foreach (SmoothMoves.BoneAnimation ba in baLIst)
			{
				GameObject.Destroy(ba.gameObject);
			}
		}
		UnitGrave.Clear();
	}
}
