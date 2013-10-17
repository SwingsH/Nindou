using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class MainSkill
{
	public void ResetBuffer()
	{
	}
	public void SetPassive(PassiveEffectInfo info)
	{
		Passive = info.GetBonusValue();
	}
	public void SetState(StateInfo info)
	{
		State = info.GetBonusValue();
	}
	AttrbuteBonus Passive;
	AttrbuteBonus State;

	public MainSkill()
	{
		SkillData = new SkillData();
		_speffect = new SpecialEffect[0];
	}

	public MainSkill(ushort SkillID)
	{
		SkillData = TestDataBase.Instance.GetSkillData(SkillID);
		_speffect = new SpecialEffect[SkillData.SPEffect.Length];
		for (int i = 0; i < SkillData.SPEffect.Length; i++)
			_speffect[i] = TestDataBase.Instance.GetSPEffect(SkillData.SPEffect[i]);
		Passive.Reset();
		State.Reset();
	}
	public MainSkill(SkillData skillData)
	{
		SkillData = skillData;
		_speffect = new SpecialEffect[SkillData.SPEffect.Length];
		for (int i = 0; i < SkillData.SPEffect.Length; i++)
			_speffect[i] = TestDataBase.Instance.GetSPEffect(SkillData.SPEffect[i]);
		Passive.Reset();
		State.Reset();
	}
	protected global::SkillData SkillData
	{
		get;
		set;
	}
	public uint SkillID
	{
		get { return SkillData.ID; }
	}
	public string Name
	{
		get { return SkillData.Name; }
	}
	public SkillType Type
	{
		get
		{
			if (Enum.IsDefined(typeof(SkillType), SkillData.SkillType))
				return (SkillType)SkillData.SkillType;
			else
			{
				Debug.LogError("Undefined SkillType Value");
				return SkillType.None;
			}
		}
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

	public int Power
	{
		get { return SkillData.Power + Passive.Power + State.Power; }
	}
	public float Accuracy
	{
		get { return SkillData.Accuracy / 100f + Passive.Accuracy + State.Accuracy; }
	}
	public float Critical
	{
		get { return SkillData.Critical / 100f + Passive.Critical + State.Critical; }
	}
	public float CriticalBonus
	{
		get { return Critical * GLOBALCONST.BattleSettingValue.CriticalBonus; }
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
		get { return SkillData.Cooldown / 100f * Passive.ASpeedRate * State.ASpeedRate; }
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

	SpecialEffect[] _speffect = new SpecialEffect[0];
	public SpecialEffect[] SPEffect
	{
		get { return _speffect; }
	}

	public string ParticleAttackStart
	{
		get { return SkillData.ParticleAttackStart; }
	}
	public string ParticleAttackEnd
	{
		get { return SkillData.ParticleAttackEnd; }
	}
	public string ParticleHit
	{
		get { return SkillData.ParticleHit; }
	}
	public DamageInfo GenerateDamageInfo()
	{
		return GenerateDamageInfo(null);
	}
	public DamageInfo GenerateDamageInfo(ActionUnit caster)
	{
		DamageInfo di = new DamageInfo();
		di.Attacker = caster;
		di.DamageType = DamageType;
		di.Accuracy = Accuracy;
		di.Power = Power;
		di.Critical = Critical;
		di.CriticalBonus = CriticalBonus;
		di.HitParticle = ParticleHit;
		di.SPEffects = new List<SpecialEffect>(SPEffect);
		if (caster != null && caster.Passive != null)
		{
			di.SPEffects.AddRange(caster.Passive.attackEffect);
		}
		int DamageTimes = AnimationData.GetAnimClipTriggerEventCount((caster as AnimUnit).Anim, AnimClipName, AnimationSetting.HIT_TAG);
		if (DamageTimes != 0)
			di.Power = Mathf.Clamp(Power / DamageTimes, 1, int.MaxValue);

		return di;
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


public class PassiveEffectInfo
{
	//List<MainSkill> passiveSkill = new List<MainSkill>();
	public List<SpecialEffect> attackEffect = new List<SpecialEffect>();


	protected AttrbuteBonus BonusValue;
	public AttrbuteBonus GetBonusValue()
	{
		return BonusValue;
	}

	public ushort Power
	{
		get { return BonusValue.Power; }
	}
	public float Accuracy
	{
		get { return BonusValue.Accuracy; }
	}
	public float Critical
	{
		get { return BonusValue.Critical; }
	}
	public void AddPassiveSkills(List<MainSkill> skills)
	{
		foreach (MainSkill skill in skills)
		{
			
			foreach(SpecialEffect se in skill.SPEffect)
				switch ((SPEffectType)se.EffectType)
				{
					case SPEffectType.PowerBuffer:
					case SPEffectType.AccuracyBuffer:
					case SPEffectType.CriticalBuffer:
						BonusValue.AddValue((SPEffectType)se.EffectType,System.Convert.ToUInt16(se.EffectPower));
						break;
					default:
						attackEffect.Add(se);
						break;
				}
		}
					
	}
	public void Reset()
	{
		//passiveSkill.Clear();
		attackEffect.Clear();
		BonusValue = new AttrbuteBonus();
	}
}
public class StateInfo
{
	List<StateData> states = new List<StateData>();
	public float DoT
	{
		get;
		protected set;
	}
	
	protected AttrbuteBonus BonusValue;
	public AttrbuteBonus GetBonusValue()
	{
		return BonusValue;
	}
	public ushort Power
	{
		get { return BonusValue.Power; }
	}
	public float Accuracy
	{
		get { return BonusValue.Accuracy; }
	}
	public float Critical
	{
		get { return BonusValue.Critical; }
	}
	public void UpdateDuration()
	{
		int index = 0;
		bool needReflash = false;
		while (index < states.Count)
		{
			states[index].Duration -= Time.deltaTime;
			if (states[index].Duration <= 0)
			{
				needReflash = true;
				states.RemoveAt(index);
				continue;
			}

			index++;
		}
		if (needReflash)
			Reflash();
	}
	public void Reflash()
	{
		BonusValue = new AttrbuteBonus();
		BonusValue.ASpeedRate = 1;
		BonusValue.MSpeedRate = 1;


		DoT = 0;

		foreach(StateData sd in states)
		{			
			switch (sd.EffectType)
			{
				case SPEffectType.PowerBuffer:
				case SPEffectType.AccuracyBuffer:
				case SPEffectType.CriticalBuffer:
				case SPEffectType.Water:
				case SPEffectType.Earth:
					BonusValue.AddValue(sd.EffectType, sd.EffectPower);
					break;
				case SPEffectType.Posion:
				case SPEffectType.Fire:
					DoT += sd.EffectPower * Time.deltaTime;
					break;

			}
		}
	}
	public void AddState(SpecialEffect se)
	{
		StateData sd = new StateData(se);
		AddState(sd);
	}
	public void AddState(StateData sd)
	{
		states.Add(sd);
		Reflash();
	}
}
public struct AttrbuteBonus
{
	
	public ushort Power;
	public float Critical;
	public float Accuracy;

	public float ASpeedRate; //攻擊速度加成
	public float MSpeedRate; //移動速度加成

	public void AddValue(SPEffectType type, ushort value)
	{
		switch (type)
		{
			case SPEffectType.PowerBuffer:
				Power += value;
				break;
			case SPEffectType.AccuracyBuffer:
				Accuracy += value / 100f;
				break;
			case SPEffectType.CriticalBuffer:
				Critical += value / 100f;
				break;
			case SPEffectType.Water:
				if (value != 0)
					ASpeedRate *= value / 100f;
				break;
			case SPEffectType.Earth:
				if (value != 0)
					MSpeedRate *= value / 100f;
				break;
		}
	}
	
	public void Reset()
	{
		ASpeedRate = 1;
		MSpeedRate = 1;
		Power = 0;
		Critical = 0;
		Accuracy = 0;
	}
	public static AttrbuteBonus operator +(AttrbuteBonus b1,AttrbuteBonus b2)
	{
		AttrbuteBonus result= new AttrbuteBonus();
		result.Power =(ushort)(b1.Power + b2.Power);
		result.Accuracy = (b1.Accuracy + b2.Accuracy);
		result.Critical = (b1.Critical + b2.Critical);
		result.ASpeedRate = (b1.ASpeedRate * b2.ASpeedRate);
		result.MSpeedRate = (b1.MSpeedRate * b2.MSpeedRate);

		return result;
	}
}
public class StateData
{
	public SPEffectType EffectType;
	public ushort EffectPower;
	public float Duration;
	public StateData(SpecialEffect se)
	{
		if (Enum.IsDefined(typeof(SPEffectType), se.EffectType))
			EffectType = (SPEffectType)se.EffectType;
		else
			EffectType = SPEffectType.None;
		EffectPower = System.Convert.ToUInt16(se.EffectPower);
		Duration = se.EffectDuration;
	}
}