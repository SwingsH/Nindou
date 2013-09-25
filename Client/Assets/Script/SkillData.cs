using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class SkillData
{
	public ushort ID;
	public byte SkillType;
	public string Name = "";
	public byte DamageType;
	public int Power;
	public ushort Critcal;
	public ushort Accuracy;
	public ushort ActiveRate;
	public int Range;
	public int RangeMode;
	public ushort Cooldown;
	public SpecialEffect[] SPEffect = new SpecialEffect[3];
	
	public string AnimName = "";
	public int AnimPlayTimes;
	public ushort CastTime;
	public string ParticleAttackStart = "";
	public string ParticleAttackEnd = "";
	public string ParticleHit = "";
	
	public override string ToString()
	{
		return string.Format("ID:{0}\nName:{1}\nPower:{2}", ID, Name, Power);
	}
}

public enum SkillDamageType: byte
{
	Damage = 0,
	Heal = 1,
}
public enum SkillType : byte
{
	None = 0,
	Weapon = 1,
	Active,
	Passive,
}
[Serializable]
public class SpecialEffect
{
	public ushort EffectType;
	public ushort EffectPower;
	public ushort EffectChance;
	public ushort EffectDuration;
}

[System.Xml.Serialization.XmlType(TypeName = "SkillData")]
[Serializable]
public class OldSkillData
{
	public ushort ID;
	public string Name = "";
	public byte DamageType;
	public int Power;
	public ushort Critcal;
	public ushort Accuracy;
	public ushort ActiveRate;
	public int Range;
	public int RangeMode;
	public ushort Cooldown;
	public SpecialEffect SP1 = new SpecialEffect();
	public SpecialEffect SP2 = new SpecialEffect();

	public string AnimName = "";
	public int AnimPlayTimes;
	public ushort CastTime;
	public string ParticleAttackStart = "";
	public string ParticleAttackEnd = "";
	public string ParticleHit = "";

	public override string ToString()
	{
		return string.Format("ID:{0}\nName:{1}\nPower:{2}", ID, Name, Power);
	}
}
