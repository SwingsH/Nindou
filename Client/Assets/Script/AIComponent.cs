using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	public NinDoAttackComponent(ActionUnit aunit)
	{
		this.unit = aunit;
	}

	public MainSkill normalAttack; //普通攻擊的技能
	public List<MainSkill> triggerSkills = new List<MainSkill>(); //普通攻擊時機率性觸發的技能
	
	public override void Active()
	{
		if (State == ActionState.Busy)
			return;
		Unit Target = unit.TargetUnit;
		if (Target == null)
			return;
		//攻擊前先面對目標
		if (Target.BasePos.x == unit.BasePos.x)
			//x相等面對哪邊都沒差了，用畫面上的位置看起來比較不會那麼奇怪
			unit.Direction = Target.ScreenPos.x - unit.ScreenPos.x > 0 ? eDirection.Right : eDirection.Left;
		else
			unit.Direction = Target.BasePos.x - unit.BasePos.x > 0 ? eDirection.Right : eDirection.Left;

		//無法進行普通攻擊則跳出
		if (unit.CurrentCast == null || !unit.CurrentCast.Castable)
			return;
		
		//判斷攻擊範圍
		if (BattleManager.CheckInRange(unit.AttackRangeMode, unit, unit.Direction, unit.AttackRange, Target))
		{
			unit.CastCurrentSkill();
		}
	}
	public override ActionState State
	{
		get
		{
			Unit Target = unit.TargetUnit;
			if (unit == null)
				return ActionState.Unavailable;
			if (unit.IsCasting)
				return ActionState.Busy;
			if (unit.CurrentCast == null || !unit.CurrentCast.Castable || !BattleManager.CheckInRange(unit.AttackRangeMode, unit, eDirection.Both, unit.AttackRange, Target))
				return ActionState.Unavailable;
			return ActionState.Idle; 
		}
	}
}

public class SimpleMoveComponent : ActionComponent
{
	const int LASTRECORD_SIZE = 5;
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
				if (unit.BasePos != Target)
				{
					GridPos tempLast = unit.BasePos;
					GridPos tempNext = FindNext(Target);
					if (BattleManager.MoveUnit(unit, tempNext))
					{
						AddLast(tempLast);
					}
				}
				else
				{
					Target = GridPos.Null;
				}
			}
		}
	}
	/// <summary>
	/// 移動實際座標
	/// </summary>
	protected virtual bool Moving()
	{
		Vector3 nextV3 = BattleManager.Get_GridWorldPos(unit.BasePos,unit.Size);
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
		GridPos tempNext = unit.BasePos;
		List<GridPos> empty = Get_CanMoveGrid();
		empty.RandomizeListOrder();
		float Score = GetDistanceScore(unit.BasePos, targetPos);
		if (Last.Contains(unit.BasePos))
			Score -= 3;
		foreach (GridPos gp in empty)
		{
			float tempScore = GetDistanceScore(gp, targetPos);
			if (Last.Contains(gp))
				tempScore -= 3;
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
		List<GridPos> result = new List<GridPos>();
		foreach (GridPos selfPos in unit.Pos)
		{
			foreach (GridPos gp in BattleManager.Get_SurroundEmptyGrid(selfPos))
			{
				if (gp == unit.BasePos)
					continue;
				if (BattleManager.Movable(unit, gp))
				{
					if(!result.Contains(gp))
						result.Add(gp);
				}
			}
		}
		return result;
	}
	public override ActionState State
	{
		get
		{
			if (Target == GridPos.Null)
				return ActionState.Idle;
			if (unit.BasePos != Target)
				return ActionState.Busy;
			if (unit.WorldPos != BattleManager.Get_GridWorldPos(unit.BasePos,unit.Size))
				return ActionState.Busy;
			return ActionState.Idle;
		}
	}

	protected float GetDistanceScore(GridPos pos1, GridPos pos2)
	{
		return -GridPos.SimpleDistance(pos1,pos2);
	}

	protected void AddLast(GridPos last)
	{
		Last.Add(last);
		if (Last.Count > LASTRECORD_SIZE)
			Last.RemoveAt(0);
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
public class TracingComponent : SimpleMoveComponent
{
	//中繼目標，依移動模式決定移動到哪裡
	protected GridPos SubTarget = GridPos.Null;

	public eMoveState MoveState
	{
		get { return unit.MoveState; }
		
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
			if (unit !=  null && unit.TargetUnit != null)
			{
				bool alreadyInRange = BattleManager.CheckInRange(RangeMode, unit, eDirection.Both, Range, unit.TargetUnit);

				//決定下一格的位置
				if (ResetMoveCount < 0 //移動多次不到定位重新取得SubTarget
					|| SubTarget == GridPos.Null
					|| (!BattleManager.Get_IsGridEmpty(SubTarget))
					|| !alreadyInRange)
				{
					FindSubTarget();	
				}

				//找下一格要移到哪
				if (SubTarget != GridPos.Null)
				{
					if (unit.MoveSpeed == 0)
						return;
					//如果已經移動到範圍內的話，計算一下現在的位置跟SubTarget的分數，較高就停止移動
					if (alreadyInRange)
					{
						float targetScore = GetDistanceScore(SubTarget, Target) * (int)MoveState + GetDistanceScore(SubTarget, unit.BasePos) * 2;
						float curScore = GetDistanceScore(unit.BasePos, Target) * (int)MoveState;

						if (curScore >= targetScore)
						{
							SubTarget = unit.BasePos;
							return;
						}
					}

					//找下一格
					GridPos tempLast = unit.BasePos;
					GridPos tempNext = FindNext(SubTarget);
					//確認需要移動
					if (unit.BasePos != tempNext)
					{
						if (BattleManager.MoveUnit(unit, tempNext))
						{
							//紀錄過去移動的格子
							AddLast(tempLast);
							
							BusyTime = float.MaxValue;
							UnavailableTime = Get_UnavailableTime();
							ResetMoveCount--;
						}
					}
					else if(!alreadyInRange)
					{
						//避免雖然現在的格子打不到，可是分數是最高，然後就站著不動的情況
						AddLast(tempLast);
					}

				}
			}
		}
	}
	protected void FindSubTarget()
	{
		int MoveStateFix = 1;
		int outRangeFix = 0;

		//因為有多格，先取離對方最近的格子
		GridPos closestPos = GridPos.Null;//離目標最近的自己的格子
		GridPos closestTargetPos = GridPos.Null;//離自己最近的目標的格子

		if(unit == null || unit.TargetUnit == null)
		{
			SubTarget = GridPos.Null;
			return;
		}
		bool cantApproch = true;
		float Score = float.MinValue;

		//計算目前的SubTarget的分數
		if (SubTarget == unit.BasePos || BattleManager.Movable(unit, SubTarget))
		{
			BattleManager.GetClosestPos(SubTarget, unit.Size,unit.TargetUnit.BasePos,unit.TargetUnit.Size, out closestPos,out closestTargetPos);
			Score = GetDistanceScore(closestPos, closestTargetPos) * (int)MoveState * MoveStateFix + GetDistanceScore(SubTarget, unit.BasePos) * 2;
			//不在範圍中的減分
			if (!BattleManager.CheckInRange(RangeMode, closestPos, eDirection.Both, Range, closestTargetPos))
				Score -= Range * 5;
		}

		//因為我可以打到他的話他也可以打到我，所以以目標為中心，找範圍內的空格
		//計算所有可以打到目標的位置的分數

		//取得可移動的位置，回傳值為basePos
		List<GridPos> movableGrid = BattleManager.Get_MovablePos_AroundTarget_InAttackRange(unit, unit.TargetUnit, RangeMode, Range);
		if (movableGrid.Count != 0)
			cantApproch = false;
		//隨機list的順序，這樣同分的話不會每次都走同一個
		movableGrid.RandomizeListOrder();
		foreach (GridPos gp in movableGrid)
		{
			//計算當basepos在gp跟target的最近距離的格子
			BattleManager.GetClosestPos(gp, unit.Size, unit.TargetUnit.BasePos, unit.TargetUnit.Size, out closestPos, out closestTargetPos);
			//分數，簡單計算x差幾格y差幾格
			float tempScore = GetDistanceScore(closestPos, closestTargetPos) * (int)MoveState * MoveStateFix + GetDistanceScore(gp, unit.BasePos) * 2;
			if (tempScore > Score)
			{
				Score = tempScore;
				SubTarget = gp;
			}
		}

		//目前無法靠近對方
		//加大一格範圍，
		if (cantApproch)
		{
			movableGrid = BattleManager.Get_MovablePos_AroundTarget_InAttackRange(unit, unit.TargetUnit, RangeMode, Range + unit.Size + 1);
			//隨機list的順序，這樣同分的話不會每次都走同一個
			movableGrid.RandomizeListOrder();
			MoveStateFix = (int)Mathf.Sign((int)MoveState);
			outRangeFix = -Range * 5; //不在範圍中的減分

			foreach (GridPos gp in movableGrid)
			{
				//計算當basepos在gp跟target的最近距離的格子
				BattleManager.GetClosestPos(gp, unit.Size, unit.TargetUnit.BasePos, unit.TargetUnit.Size, out closestPos, out closestTargetPos);
				//分數，簡單計算x差幾格y差幾格
				float tempScore = GetDistanceScore(closestPos, closestTargetPos) * (int)MoveState * MoveStateFix + GetDistanceScore(gp, unit.BasePos) * 2;
				tempScore += outRangeFix;
				if (tempScore > Score)
				{
					Score = tempScore;
					SubTarget = gp;
				}
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

	protected List<GridPos> Get_MovablePos_AroundTargetPos(GridPos targetPos)
	{
		List<GridPos> result = new List<GridPos>();
		foreach (GridPos gp in BattleManager.Get_Grids(RangeMode, targetPos,eDirection.Both,Range))
		{
			if (BattleManager.Movable(unit, gp))
				result.Add(gp);
		}
		return result;
	}

	protected virtual float Get_UnavailableTime()
	{
		return 0;
	}
}

public class TeleportInRangeComponent : TracingComponent
{
	const float DefaultAnimTime = 0.2f;
	const float MaxWaitingTime = 1.5f;
	float MoveTimeCounter;
	protected override bool Moving()
	{
		if (unit == null)
			return false;
		Vector3 nextV3 = BattleManager.Get_GridWorldPos(unit.BasePos,unit.Size);
		if (unit.WorldPos != nextV3)
		{
			//開始移動，放個效果
			if (MoveTimeCounter == 0)
			{
				ParticleManager.Emit("Teleport", unit.WorldCenter, (nextV3 - unit.WorldPos).normalized);
				unit.Hide();
			}
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
				ParticleManager.Emit("Appear", unit.WorldCenter, Vector3.forward);
				unit.Show();
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
		List<GridPos> result = new List<GridPos>();
		foreach (GridPos gp in BattleManager.Get_EmptyGrid(0, unit.BasePos, eDirection.Both, unit.MoveSpeed))
		{
			if (gp == unit.BasePos)
				continue;
			if (BattleManager.Movable(unit, gp))
				result.Add(gp);
		}
		return result;
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
				return Time.time + DefaultAnimTime + MaxWaitingTime * GridPos.SimpleDistance(Last[Last.Count - 1], unit.BasePos) / unit.MoveSpeed;
		}
		return 0;
	}
}