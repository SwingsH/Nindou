using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class SkillData
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
