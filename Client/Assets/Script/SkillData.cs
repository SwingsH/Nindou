using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class SkillData
{
	public ushort ID;
	public string Name = "";
	public byte SkillType;
	public byte DamageType;
	public ushort Power;
	public ushort Critical;
	public ushort Accuracy;
	public ushort ActiveRate;
	public ushort Range;
	public ushort RangeMode;
	public ushort Cooldown;
	public SpecialEffect[] SPEffect = new SpecialEffect[3];
	
	public string AnimName = "";
	public ushort AnimPlayTimes;
	public ushort CastTime;
	public string ParticleAttackStart = "";
	public string ParticleAttackEnd = "";
	public string ParticleHit = "";
}

[StructLayout(LayoutKind.Sequential)]
public class SpecialEffect
{
	public byte EffectType;
	public ushort EffectPower;
	public ushort EffectChance;
	public ushort EffectDuration;
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
	Extrim = 255,
}
public enum SPEffectType : byte
{
	None = 0,
	PowerBuffer,
	CriticalBuffer,
	AccuracyBuffer,
	Posion = 10,
	Fire,
	Water,
	Earth,
	//以下為觸發類效果，目前只有攻擊時觸發
	AdditionalEffect = 100,
	AddPowerBuffer,
	AddCriticalBuffer,
	AddAccuracyBuffer,
	AddPosion = 110,
	AddFire,
	AddWater,
	AddEarth,
	//武器技能上附加的特殊技能
	ExtrimSkill = 255,
}

