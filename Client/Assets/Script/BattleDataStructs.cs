﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
/*
 * 戰鬥相關資料格式
 */ 

public class DamageInfo
{
	public Unit Attacker;
	public uint Power;
	public SkillTargetType TargetType;
	public List<SpecialEffect> SPEffects;
	public float Accuracy;
	public float Critical;
	public float CriticalBonus;

	public string HitParticle;

	//跳血專用，想不到要放哪了
	public bool MultiHit;
}

public enum eDirection
{
	Both,
	Left,
	Right,
}
public static class eDirectionExtensions
{
	public static eDirection GetOppsiteDirection(this eDirection dir)
	{
		switch (dir)
		{
			case eDirection.Left:
				return eDirection.Right;
			case eDirection.Right:
				return eDirection.Left;
			default:
				return eDirection.Right;
		}
	}
}
public enum eTargetMode
{
	Closest,
}
