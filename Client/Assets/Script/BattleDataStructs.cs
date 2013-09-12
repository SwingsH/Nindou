using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * 戰鬥相關資料格式
 */ 

public struct DamageInfo
{
	public Unit Attacker;
	public int Power;
	public SkillDamageType DamageType;
	public float Accuracy;
	public float Critical;
	public float CriticalBonus;
}

public enum eDirection
{
	Both,
	Left,
	Right,
}

public struct AttackInfo
{
	public List<Unit> Target;
	public DamageInfo Damage;
}

public enum eTargetMode
{
	Closest,
}

public class UnitInfo
{
	public string ModelName;
	public int MaxLife;
	public ushort AttackID;
	public ushort[] SkillID = new ushort[0];
	public byte MoveMode;
	public int MoveSpeed;
}

public class StageInfo
{
	public ushort StageID;
	public int TotalPower;
	public ushort[] EnemyData = new ushort[5];
}