using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
/*
 * 戰鬥相關資料格式
 */ 

public struct DamageInfo
{
	public Unit Attacker;
	public int Power;
	public SkillDamageType DamageType;
	public List<SpecialEffect> SPEffects;
	public float Accuracy;
	public float Critical;
	public float CriticalBonus;

	public string HitParticle;
}

public enum eDirection
{
	Both,
	Left,
	Right,
}

public enum eTargetMode
{
	Closest,
}

[StructLayout(LayoutKind.Sequential)]
public class UnitInfo
{
	public uint MaxLife;
	public ushort AttackID;
	public ushort[] SkillID = new ushort[2];
	public ushort[] PassiveSkillID = new ushort[2];
	public byte MoveMode;
	public ushort MoveSpeed;

	public string BoneName = "NindoTestBone";
	public string[] spriteNames = new string[8];
}

[StructLayout(LayoutKind.Sequential)]
public class NPCData
{
	public uint NPCID;
	public string Name;
	public UnitInfo Info;
}