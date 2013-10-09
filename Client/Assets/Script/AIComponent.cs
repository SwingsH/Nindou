using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 目前動作的狀態
public enum ActionState
{
	Idle, //閒置
	Busy, //動作中
	Unavailable, //無法使用
}

/// <summary>
/// 動作元件
/// </summary>
public abstract class ActionComponent 
{
	public ActionUnit unit; //執行動作的單位
	public abstract void Active();
	protected float BusyTime; //動作結束時間或動畫的播放時間 Time.time;
	protected float UnavailableTime; //無法動作的時間

	//動作狀態
	public abstract ActionState State
	{
		get;
	}

	public virtual void DrawInfo()
	{
	}
}

/// <summary>
/// 忍豆用攻擊動作元件
/// </summary>
public class NinDoAttackComponent : ActionComponent
{
	public Unit Target; 
	public MainSkill normalAttack; //普通攻擊的技能
	public List<MainSkill> triggerSkills = new List<MainSkill>(); //普通攻擊時機率性觸發的技能
	
	public override void Active()
	{
		if (State == ActionState.Busy)
			return;
		if (Target == null)
			return;
		//攻擊前先面對目標
		unit.Direction = (Target.Pos.x - unit.Pos.x) > 0 ? eDirection.Right : eDirection.Left;

		//無法進行普通攻擊則跳出
		if (normalAttack == null || !normalAttack.Castable)
			return;
		
		//判斷攻擊範圍
		if (BattleManager.CheckInRange(unit.AttackRangeMode, unit.Pos, unit.Direction, unit.AttackRange, Target.Pos))
		{
			float rateFix = 1;
			UnavailableTime = Time.time + normalAttack.CoolDown;
			MainSkill castSkill = normalAttack;
			
			for (int i = 0; i < triggerSkills.Count; i++)
			{
				if (triggerSkills[i].Castable)
				{
					List<Unit> units;
					if (triggerSkills[i].DamageType == SkillDamageType.Damage)
						units = BattleManager.Get_EnemyUnitsInRange(unit.Group, triggerSkills[i].range, unit.Pos, unit.Direction, triggerSkills[i].range);
					else
						units = BattleManager.Get_FriendUnitsInRange(unit.Group, triggerSkills[i].range, unit.Pos, unit.Direction, triggerSkills[i].range);

					if (units.Count == 0)
						continue;

					float r = Random.value;
					if (r < triggerSkills[i].ActivePropability * rateFix)
					{
						castSkill = triggerSkills[i];
						break;
					}
					else
					{
						if (1 - triggerSkills[i].ActivePropability != 0)
							rateFix /= 1 - triggerSkills[i].ActivePropability;
					}
				}
			}
			BusyTime = Time.time + castSkill.CastTime;
			unit.CastSkill(castSkill);
		}
	}
	public override ActionState State
	{
		get
		{
			if (Target == null)
				return ActionState.Unavailable;
			if (BusyTime > Time.time)
				return ActionState.Busy;
			//if (unit.IsCasting)
			//    return ActionState.Busy;
			//if (UnavailableTime > Time.time)
			//    return ActionState.Unavailable;
			if (normalAttack == null || !normalAttack.Castable || !BattleManager.CheckInRange(normalAttack.rangeMode, unit.Pos, (Target.Pos.x - unit.Pos.x) > 0 ? eDirection.Right : eDirection.Left, normalAttack.range, Target.Pos))
				return ActionState.Unavailable;
			return ActionState.Idle; 
		}
	}
}

public class SimpleMoveComponent : ActionComponent
{
	protected List<GridPos> Last = new List<GridPos>();
	GridPos target = GridPos.Null;
	public virtual GridPos Target
	{
		get
		{
			return target;
		}
		set
		{
			if (target != value)
			{
				target = value;
			}
		}
	}
	
	public override void Active()
	{
		if (!Moving())
		{
			if (Target != GridPos.Null)
			{
				if (unit.Pos != Target)
				{
					GridPos tempLast = unit.Pos;
					GridPos tempNext = FindNext(Target);
					if (BattleManager.MoveUnit(unit, tempNext))
					{
						Last.Add(tempLast);
						if (Last.Count > 5)
							Last.RemoveAt(0);
					}
				}
				else
				{
					Target = GridPos.Null;
				}
			}
		}
	}
	protected virtual bool Moving()
	{
		Vector3 nextV3 = BattleManager.Get_GridWorldPos(unit.Pos);
		if (unit.WorldPos != nextV3)
		{
			unit.Direction = (nextV3.x - unit.WorldPos.x) > 0 ? eDirection.Right : ((nextV3.x - unit.WorldPos.x) < 0 ? eDirection.Left : unit.Direction);
			unit.WorldPos = Vector3.MoveTowards(unit.WorldPos, nextV3,unit.MoveSpeed * Time.deltaTime);

			BusyTime = float.MaxValue;
			unit.PlayWalk();
			return true;
		}
		else
		{
			BusyTime = 0;
			unit.StopWalk();
			return false;
		}
	}

	protected GridPos FindNext(GridPos targetPos)
	{
		GridPos tempNext = unit.Pos;
		List<GridPos> empty = Get_CanMoveGrid();
		RandomizeOrderList(ref empty);
		float Score = GetDistanceScore(unit.Pos, targetPos);
		foreach (GridPos gp in empty)
		{
			float tempScore = GetDistanceScore(gp, targetPos);
			if (Last.Contains(gp))
				tempScore -= 2;
			if (tempScore > Score)
			{
				Score = tempScore;
				tempNext = gp;
			}
		}
		return tempNext;
	}
	protected virtual List<GridPos> Get_CanMoveGrid()
	{
		return BattleManager.Get_SurroundEmptyGrid(unit.Pos);
	}
	public override ActionState State
	{
		get
		{
			if (Target == GridPos.Null)
				return ActionState.Idle;
			if (unit.Pos != Target)
				return ActionState.Busy;
			if (unit.WorldPos != BattleManager.Get_GridWorldPos(unit.Pos))
				return ActionState.Busy;
			return ActionState.Idle;
		}
	}

	protected float GetDistanceScore(GridPos pos1, GridPos pos2)
	{
		return -GridPos.SimpleDistance(pos1,pos2);
	}

	protected void RandomizeOrderList<T>(ref List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			T tempT = list[i];
			int RandomI = Random.Range(0, list.Count);
			list[i] = list[RandomI];
			list[RandomI] = tempT;
		}
	}
}

//移動的模式
public enum eMoveState : int
{
	Closer = 3, //靠近目標
	KeepRange = -4, //在範圍內盡可能的遠離目標
}
/// <summary>
/// 移動到指定目標在範圍內
/// </summary>
public class MoveInRangeComponent : SimpleMoveComponent
{

	//中繼目標，依移動模式決定移動到哪裡
	protected GridPos SubTarget = GridPos.Null;

	protected eMoveState _moveState = eMoveState.Closer;
	public eMoveState MoveState
	{
		get { return _moveState; }
		set
		{
			if (_moveState != value)
			{
				SubTarget = GridPos.Null;
				_moveState = value;
			}
		}
	}
	public override GridPos Target
	{
		get
		{
			return base.Target;
		}
		set
		{
			if (Target != value)
			{
				SubTarget = GridPos.Null;
				Last.Clear();
				base.Target = value;
			}
		}
	}

	int ResetMoveCount = 3;

	int RangeMode
	{
		get
		{
			if (unit != null)
				return unit.AttackRangeMode;
			else
				return 0;
		}
	}
	int Range
	{
		get
		{
			if (unit != null)
				return unit.AttackRange;
			else
				return 0;
		}
	}
	/*
	 * 依照目標的位置跟移動模式來決定subtarget，然後移動到subtarget
	 */
	public override void Active()
	{
		if (!Moving())
		{
			if (Target != GridPos.Null)
			{	
				//決定下一格的位置
				//判斷SubTarget的格子是不是空的，不是的話重新找一個SubTarget
				if (ResetMoveCount < 0 
					|| SubTarget == GridPos.Null 
					|| (!BattleManager.Get_IsGridEmpty(SubTarget) && SubTarget != unit.Pos)
					|| !BattleManager.CheckInRange(RangeMode,Target,eDirection.Both,Range,SubTarget))
				{
					FindSubTarget();	
				}

				//找下一格要移到哪
				if (SubTarget != GridPos.Null && SubTarget != unit.Pos)
				{
					if (unit.MoveSpeed == 0)
						return;
					//如果已經移動到範圍內的話，計算一下現在的位置跟SubTarget的分數，較高就停止移動
					if (BattleManager.Get_Grids(RangeMode, Target, eDirection.Both, Range).Contains(unit.Pos))
					{
						float targetScore = GetDistanceScore(SubTarget, Target) * (int)MoveState + GetDistanceScore(SubTarget, unit.Pos) * 2;
						float curScore = GetDistanceScore(unit.Pos, Target) * (int)MoveState;

						if (curScore >= targetScore)
						{
							SubTarget = unit.Pos;
							return;
						}
					}

					//找下一格
					GridPos tempLast = unit.Pos;
					GridPos tempNext = FindNext(SubTarget);
					if (unit.Pos != tempNext)
					{
						if (BattleManager.MoveUnit(unit, tempNext))
						{
							Last.Add(tempLast);
							if (Last.Count > 5)
								Last.RemoveAt(0);
							BusyTime = float.MaxValue;
							UnavailableTime = Get_UnavailableTime();
							ResetMoveCount--;
						}
					}

				}
			}
		}
	}
	protected void FindSubTarget()
	{
		//重設，找不到空格的話會停止移動
		//SubTarget = GridPos.Null;
		int MoveStateFix = 1;
		int outRangeFix = 0;

		//因為我可以打到他的話他也可以打到我，所以以目標為中心，找範圍內的空格
		List<GridPos> emptyGrid = BattleManager.Get_EmptyGrid(unit.AttackRangeMode, Target, eDirection.Both, unit.AttackRange);
		//沒空格就到預設的範圍內找分數最高的格子
		if (emptyGrid.Count == 0)
		{
			emptyGrid = BattleManager.Get_EmptyGrid(0, Target, eDirection.Both, unit.AttackRange + 1);
			MoveStateFix = (int)Mathf.Sign((int)MoveState);
			outRangeFix = -Range * 5;
		}
		//隨機list的順序，這樣同分的話不會每次都走同一個
		RandomizeOrderList(ref emptyGrid);
		float Score = float.MinValue;
		if (SubTarget == unit.Pos || BattleManager.Get_IsGridEmpty(SubTarget))
		{
			Score = GetDistanceScore(SubTarget, Target) * (int)MoveState * MoveStateFix + GetDistanceScore(SubTarget, unit.Pos) * 2;
			if (!BattleManager.CheckInRange(RangeMode, Target, eDirection.Both, Range, SubTarget))
				Score -= Range * 5;
		}
		foreach (GridPos gp in emptyGrid)
		{
			//分數，簡單計算x差幾格y差幾格
			float tempScore = GetDistanceScore(gp, Target) * (int)MoveState * MoveStateFix + GetDistanceScore(gp, unit.Pos) * 2;
			tempScore += outRangeFix;
			if (tempScore > Score)
			{
				Score = tempScore;
				SubTarget = gp;
			}
		}
		//閒晃太多次移動不到目標的話就重新找一個
		ResetMoveCount = 10;
	}
	public override ActionState State
	{
		get
		{
			if (BusyTime > Time.time)
				return ActionState.Busy;
			return ActionState.Idle;
		}
	}

	protected virtual float Get_UnavailableTime()
	{
		return 0;
	}
}

public class TeleportInRangeComponent : MoveInRangeComponent
{
	const float DefaultAnimTime = 0.1f;
	const float MaxWaitingTime = 1.5f;
	float MoveTimeCounter;
	protected override bool Moving()
	{
		if (unit == null)
			return false;
		Vector3 nextV3 = BattleManager.Get_GridWorldPos(unit.Pos);
		if (unit.WorldPos != nextV3)
		{
			unit.Direction = (nextV3.x - unit.WorldPos.x) > 0 ? eDirection.Right : ((nextV3.x - unit.WorldPos.x) < 0 ? eDirection.Left : unit.Direction);
			if (MoveTimeCounter < DefaultAnimTime)
				unit.WorldPos = Vector3.MoveTowards(unit.WorldPos, nextV3, Vector3.Distance(nextV3, unit.WorldPos) * Time.deltaTime / (DefaultAnimTime - MoveTimeCounter));
			else
				unit.WorldPos = nextV3;
			if (unit.WorldPos != nextV3)
			{
				MoveTimeCounter += Time.deltaTime;
				BusyTime = float.MaxValue;
			}
			else
			{
				MoveTimeCounter = 0;
				BusyTime = 0;
			}
			return true;
		}
		else
		{
			return false;
		}
	}
	protected override List<GridPos> Get_CanMoveGrid()
	{
		if (unit == null)
			return new List<GridPos>();
		return BattleManager.Get_EmptyGrid(0, unit.Pos, eDirection.Both, unit.MoveSpeed);	
	}				

	public override ActionState State
	{
		get
		{
			//if (SubTarget != GridPos.Null && SubTarget != unit.Pos)
			//    return ActionState.Busy;
			if (BusyTime > Time.time)
				return ActionState.Busy;
			if (UnavailableTime > Time.time)
				return ActionState.Unavailable;
			return ActionState.Idle;
		}
	}

	protected override float Get_UnavailableTime()
	{
		if (Last.Count > 0)
		{
			if (unit.MoveSpeed != 0)
				return Time.time + DefaultAnimTime + MaxWaitingTime * GridPos.SimpleDistance(Last[Last.Count - 1], unit.Pos) / unit.MoveSpeed;
		}
		return 0;
	}
}