using UnityEngine;
using System.Collections;
using SmoothMoves;
using System.Collections.Generic;

public class UnitGenerater{

    //已經被回收的場景單位, 下次有同 外觀 unit 時重複使用
	Dictionary<string, List<SmoothMoves.BoneAnimation>> UnitGrave = new Dictionary<string, List<SmoothMoves.BoneAnimation>>();

	Dictionary<BoneAnimation, Sprite[]> BoneSprites = new Dictionary<BoneAnimation, Sprite[]>();

    /// <summary>
    /// 產生一個場景單位 (unit)
    /// </summary>
    public Unit GenerateUnit(UnitInfo info)
	{
		return GenerateUnit(info, false);
	}

    /// <summary>
    /// 產生一個場景單位 (unit)
    /// </summary>
	public Unit GenerateUnit(UnitInfo info, bool isPreview)
	{
		if (info == null)
			return null;
		ActionUnit actUnit = new ActionUnit(isPreview);
		List<MainSkill> activeSkill = new List<MainSkill>();
		actUnit.NormalAttack = new MainSkill(InformalDataBase.Instance.GetSkillData(info.AttackID));
		foreach (ushort skillID in info.SkillID)
		{
			activeSkill.Add(new MainSkill(InformalDataBase.Instance.GetSkillData(skillID)));
		}
		actUnit.ActiveSkills = activeSkill;
		actUnit.MaxLife = info.MaxLife;
		actUnit.Life = info.MaxLife;
		actUnit.MoveSpeed = info.MoveSpeed;
		if (info.MoveMode == 1)
			actUnit.MoveAction = new TeleportInRangeComponent();
		else
			actUnit.MoveAction = new MoveInRangeComponent();
		Sprite[] sprites;
		actUnit.Entity = GenerateEntity(info.BoneName, info.spriteNames, out sprites);
		actUnit.Sprites = sprites;
		if(actUnit.Entity != null)
			actUnit.SpriteBounds = GetSpriteBounds(actUnit.Entity.transform, sprites);
		return actUnit;
	}

    /// <summary>
    /// 產生一個場景單位所需元件, bone, sprite
    /// </summary>
	public GameObject GenerateEntity(string BoneAnimName, string[] spriteInfo ,out Sprite[] sprites)
	{
		SmoothMoves.BoneAnimation boneAnim;
        GameObject unitContainer = new GameObject(BoneAnimName);

		List<SmoothMoves.BoneAnimation> tempList;
        if (UnitGrave.TryGetValue(BoneAnimName, out tempList) && tempList.Count > 0)
        {
            boneAnim = tempList[0];
            boneAnim.gameObject.SetActive(true);
            boneAnim.transform.parent = unitContainer.transform;
            tempList.RemoveAt(0);
            boneAnim.gameObject.name = BoneAnimName;
        }
        else
        {
            boneAnim = ResourceStation.GetBone(unitContainer, BoneAnimName);
        }
        boneAnim.gameObject.transform.localRotation = Quaternion.identity;

		if (boneAnim == null)
		{
			Debug.LogError("Cant Find Bone Data");
			sprites = null;
			return null;
		}
		if (!BoneSprites.TryGetValue(boneAnim, out sprites))
		{
			sprites = GenerateModelSprite(boneAnim);
			BoneSprites.Add(boneAnim, sprites);
		}
		SetModelSprite(sprites, spriteInfo);

        return unitContainer;
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
		AnimUnit actUnit = unit as AnimUnit;
		if (actUnit == null || actUnit.Anim == null)
			return;

        GameObject container = actUnit.Anim.transform.parent.gameObject;
        actUnit.Anim.transform.parent = null;
        GameObject.Destroy(container);

        foreach (SmoothMoves.Sprite sprite in actUnit.Anim.GetComponentsInChildren<SmoothMoves.Sprite>())
		{
			sprite.SetTextureName(string.Empty);
			sprite.SetAtlas(null);
		}
		List<SmoothMoves.BoneAnimation> tempList;
		if (!UnitGrave.TryGetValue(actUnit.Anim.name, out tempList))
		{
			tempList = new List<SmoothMoves.BoneAnimation>();
			UnitGrave.Add(actUnit.Anim.name, tempList);

            CommonFunction.DebugMsg(" UnitGrave : " + actUnit.Anim.name);
		}
		if(!tempList.Contains(actUnit.Anim))
			tempList.Add(actUnit.Anim);
		//au.Anim.name = au.Anim.GetInstanceID().ToString();
		actUnit.Anim.transform.localScale = Vector3.one;
		actUnit.Anim.gameObject.SetActive(false);

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
