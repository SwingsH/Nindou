using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class MainSkill
{
	public MainSkill()
	{
		SkillData = new SkillData();
	}
	public MainSkill(SkillData skillData)
	{
		SkillData = skillData;
	}
	public global::SkillData SkillData
	{
		protected get;
		set;
	}
	public int Power
	{
		get { return SkillData.Power; }
	}
	public SkillDamageType DamageType
	{
		get 
		{
			if (Enum.IsDefined(typeof(SkillDamageType), SkillData.DamageType))
				return (SkillDamageType)SkillData.DamageType;
			else
			{
				Debug.LogError("Undefined DamageType Value");
				return SkillDamageType.Damage;
			}
		}
	}
	public float Accuracy
	{
		get { return SkillData.Accuracy / 100f; }
	}
	public float Critical
	{
		get { return SkillData.Critcal / 100f; }
	}
	public float CriticalBonus
	{
		get { return SkillData.Critcal / 100f * BattleSettingValue.CriticalBonus; }
	}
	public int range
	{
		get { return SkillData.Range; }
	}

	public int rangeMode
	{
		get { return SkillData.RangeMode; }
	}

	public float CoolDown
	{
		get { return SkillData.Cooldown / 100f; }
	}
	public float CastableTime;
	public float CastTime //施展時間，技能動畫播放時間
	{
		get { return SkillData.CastTime / 100f; }
	}
	public string AnimClipName
	{
		get { return SkillData.AnimName; }
	}
	public int AnimPlayTimes //動畫播放次數，要不要跟攻擊力有關聯再議
	{
		get { return SkillData.AnimPlayTimes; }
	}
	public bool Castable
	{
		get { return Time.time > CastableTime; }
	}

	public float ActivePropability
	{
		get { return SkillData.ActiveRate / 100f; }
	}

	public List<DamageInfo> GenerateDamageInfo(Unit caster)
	{
		if (!(caster is AnimUnit))
			return new List<DamageInfo>();

		int DamageTimes = AnimationData.GetAnimClipTriggerEventCount((caster as AnimUnit).Anim, AnimClipName, AnimationSetting.HIT_TAG);
		if (DamageTimes == 0)
			return new List<DamageInfo>();
		int seperatePow = Mathf.Clamp(Power / DamageTimes, 1, int.MaxValue);
		
		DamageInfo di = new DamageInfo();
		di.Attacker = caster;
		di.DamageType = DamageType;
		di.Accuracy = Accuracy;
		di.Power = seperatePow;
		di.Critical = Critical;
		di.CriticalBonus = CriticalBonus;
		List<DamageInfo> result = new List<DamageInfo>();
		for (int i = 0; i < DamageTimes; i++)
			result.Add(di);
		return result;
	}
	public AnimInfo GenerateAnimInfo()
	{
		AnimInfo info = new AnimInfo();
		info.clipName = AnimClipName;
		info.totalTime = CastTime;
		info.times = AnimPlayTimes;
		return info;
	}
	public MainSkill Clone()
	{
		return MemberwiseClone() as MainSkill;
	}
}

public class SlotSkill
{
	public global::SkillData SkillData
	{
		get;
		set;
	}
}