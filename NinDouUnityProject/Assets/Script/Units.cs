using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SmoothMoves;

public abstract class Unit
{
	public virtual GameObject Entity
	{
		get;
		set;
	}

	public abstract void Run();
	public GridPos Pos;
	public eDirection Direction;
	public Vector3 WorldPos;
	public Color c = Color.white;
	public eGroup Group;

	public virtual int MaxLife
	{
		get;
		set;
	}
	public virtual int Life
	{
		get;
		set;
	}

	public abstract void Damaged(DamageInfo info);

	public virtual void Draw(Color color)
	{
		if (Entity)
			return;
		Gizmos.color = color * c;
		Gizmos.DrawSphere(WorldPos + Vector3.up *0.15f, 0.2f);
		Gizmos.color = Color.blue;
		switch (Direction)
		{
			case eDirection.Left:
				Gizmos.DrawLine(WorldPos + Vector3.up * 0.4f, WorldPos + Vector3.up * 0.4f + Vector3.left * 0.2f);
				break;
			case eDirection.Right:
				Gizmos.DrawLine(WorldPos + Vector3.up * 0.4f, WorldPos + Vector3.up * 0.4f + Vector3.right * 0.2f);
				break;
		}
	}

	public override string ToString()
	{
		string result = base.ToString();
		result += string.Format("\nEntity:{0}", Entity == null ? "null":Entity.name);
		result += string.Format("\nGridPos:{0}", Pos);
		result += string.Format("\nGridWorldPos:{0}", BattleManager.Get_GridWorldPos(Pos));
		result += string.Format("\nWorldPos:{0}", WorldPos);
		if(Entity != null)
			result += string.Format("\nEntityPos:{0}", Entity.transform.position);
		result += string.Format("\nLife:{0}", Life);
		return result;
	}
}

public class AnimUnit : Unit
{
	public override GameObject Entity
	{
		get
		{
			return base.Entity;
		}
		set
		{
			base.Entity = value;
			Anim = Entity.GetComponentInChildren<BoneAnimation>();
		}
	}
	public virtual BoneAnimation Anim
	{
		get;
		protected set;
	}

	public override int MaxLife
	{
		get
		{
			return base.MaxLife;
		}
		set
		{
			
			base.MaxLife = value;
			Life = Mathf.Clamp(Life, 0, MaxLife);
		}
	}
	public override int Life
	{
		get
		{
			return base.Life;
		}
		set
		{
			base.Life =Mathf.Clamp(value, 0, MaxLife);
		}
	}

	/// <summary>
	/// 播放warpmode為Once的動畫
	/// </summary>
	public void PlayAnim(AnimInfo animInfo)
	{
		if (Anim == null)
			return;
		if (!Anim.AnimationClipExists(animInfo.clipName))
			return;
		if (animInfo.totalTime <= 0)
		{
			Debug.LogError("TotalTime <= 0");
			return;
		}
		if (animInfo.times <= 1)
		{
			AnimationState state = Anim.PlayQueued(animInfo.clipName, QueueMode.CompleteOthers);
			float ns = state.length / animInfo.totalTime;
			state.speed = ns;
		}
		else
		{
			AnimationState state = Anim.PlayQueued(animInfo.clipName, QueueMode.CompleteOthers);
			float ns = state.length / (animInfo.totalTime / animInfo.times);
			state.speed = ns;
			for (int i = 1; i < animInfo.times; i++)
			{
				state = Anim.PlayQueued(animInfo.clipName, QueueMode.CompleteOthers);
				state.speed = ns;
			}
				
		}
	}

	public override void Run()
	{
		
	}
	public override void Damaged(DamageInfo info)
	{
		if (Random.value < info.Accuracy)
		{
			float rate = 1;
			if (Random.value < info.Critical)
			{
				rate = 1 + info.CriticalBonus;
			}
			Life -= Mathf.CeilToInt(rate * info.Power);

			if (Life <= 0)
				BattleManager.Unit_Dead(this);
		}
	}
}

public class ActionUnit : AnimUnit
{
	protected ActionComponent currentAction;
	SimpleMoveComponent _moveAction;
	public SimpleMoveComponent MoveAction
	{
		get { return _moveAction; }
		set
		{
			if (_moveAction != null)
				_moveAction.unit = null;
			_moveAction = value;
			if (_moveAction != null)
				_moveAction.unit = this;
		}
	}

	public eMoveState MoveState
	{
		get
		{
			if (_moveAction is MoveInRangeComponent)
			{
				return (_moveAction as MoveInRangeComponent).MoveState;
			}
			return eMoveState.Closer;
		}
		set
		{
			if (_moveAction is MoveInRangeComponent)
				(_moveAction as MoveInRangeComponent).MoveState = value;
		}
	}

	protected NinDoAttackComponent AttackAction;
	public GridPos TestOrderMove = GridPos.Null;
	public Unit TargetUnit = null;
	public override BoneAnimation Anim
	{
		get
		{
			return base.Anim;
		}
		protected set
		{
			if (Anim != null)
				Anim.UnregisterUserTriggerDelegate(UserTriggerDelegate);
			base.Anim = value;
			Anim.RegisterUserTriggerDelegate(UserTriggerDelegate);
			StopWalk();
		}
	}

	MainSkill _normalAttack;
	public MainSkill NormalAttack
	{
		get
		{
			return _normalAttack;
		}
		set
		{
			_normalAttack = value;
			if (AttackAction != null)
				AttackAction.normalAttack = _normalAttack;
			if (_normalAttack != null)
			{
				if (MoveAction is MoveInRangeComponent)
				{
					MoveInRangeComponent mrc = MoveAction as MoveInRangeComponent;
				}
			}
		}
	}
	List<MainSkill> _triggerSkills = new List<MainSkill>();
	public List<MainSkill> triggerSkills
	{
		get
		{
			return _triggerSkills;
		}
		set
		{
			_triggerSkills = value;
			if (AttackAction != null)
				AttackAction.triggerSkills = _triggerSkills;
		}
	}

	public List<AttackInfo> AttackList = new List<AttackInfo>(); //傷害資訊暫存，同步動畫用
	
	public int ActionRangeMode
	{
		get
		{
			if (_normalAttack != null)
				return _normalAttack.rangeMode;
			else
				return 0;
		}
	}
	public int AttackRangeMode
	{
		get
		{
			if (_normalAttack != null)
				return _normalAttack.rangeMode;
			else
				return 0;
		}
	}
	public int AttackRange
	{
		get
		{
			if (_normalAttack != null)
				return _normalAttack.range;
			else
				return 0;
		}
	}
	public int MoveSpeed
	{
		get;
		set;
	}

	public override void Run()
	{
		//if (mc.State == ActionState.Idle)
		//    RandomMove();
		TargetUnit = FindTarget(eTargetMode.Closest);
		AttackAction.Target = TargetUnit;
		if (TargetUnit != null)
		{
			MoveAction.Target = TargetUnit.Pos;
		}
		else
			MoveAction.Target = GridPos.Null;
		if (Entity)
		{
			Entity.transform.position = WorldPos;
			Entity.transform.rotation = Camera.main.transform.rotation;
			switch (Direction)
			{
				case eDirection.Left:
					Entity.transform.localScale =new Vector3(-1,1,1);
					break;
				default:
					Entity.transform.localScale = Vector3.one;
					break;
			}
		}
		if (currentAction != null && currentAction.State == ActionState.Busy)
		{
			currentAction.Active();
			return;
		}
		currentAction = DecideAction(eMoveState.Closer);
		if (currentAction != null)
			currentAction.Active();
	}

	protected virtual Unit FindTarget(eTargetMode mode)
	{
		switch (mode)
		{
			case eTargetMode.Closest:
				{
					Unit result = TargetUnit;
					float rScore = float.MinValue;
					if (TargetUnit != null)
					{
						rScore = -GridPos.SimpleDistance(Pos, TargetUnit.Pos);
						if (BattleManager.CheckInRange(AttackRangeMode, Pos, eDirection.Both, AttackRange, TargetUnit.Pos))
							rScore += 2;
						else if(BattleManager.Get_EmptyGrid(AttackRangeMode,TargetUnit.Pos,eDirection.Both, AttackRange).Count == 0)
							rScore -= 20 ;
					}
					foreach (Unit u in BattleManager.Get_AllEnemyUnits(Group))
					{
						if (u == null)
							continue;
						float uScore = -GridPos.SimpleDistance(Pos, u.Pos);
						if (BattleManager.CheckInRange(AttackRangeMode, Pos, eDirection.Both, AttackRange, u.Pos))
							uScore += 1;
						else if(BattleManager.Get_EmptyGrid(AttackRangeMode, u.Pos, eDirection.Both, AttackRange).Count == 0)
							uScore -= 20;
						if (uScore > rScore)
						{
							result = u;
							rScore = uScore;
						}
					}
					return result;
				}
			default:
				return null;
		}
	}
	protected ActionComponent DecideAction(eMoveState state)
	{
		switch (state)
		{
			case eMoveState.Closer:
				if (AttackAction.State == ActionState.Idle)
					return AttackAction;
				if (MoveAction.State == ActionState.Idle)
					return MoveAction;
				break;
			case eMoveState.KeepRange:
				if (MoveAction.State == ActionState.Idle)
					return MoveAction;
				if (AttackAction.State == ActionState.Idle)
					return AttackAction;
				break;
			default:
				return null;
		}
		return null;
	}

	public void PlayWalk()
	{
		Anim.Blend(AnimationSetting.WALK_ANIM, 1, 0.1f);
	}
	public void StopWalk()
	{
		Anim.Blend(AnimationSetting.WALK_ANIM, 0, 0.1f);
	}

	void UserTriggerDelegate(UserTriggerEvent triggerEvent)
	{
		if (AttackList.Count > 0)
		{
			AttackInfo info = AttackList[0];
			AttackList.RemoveAt(0);
			foreach (Unit u in info.Target)
			{
				u.Damaged(info.Damage);				
			}
		}
	}

	public override void Draw(Color color)
	{
		base.Draw(color);
		if (currentAction != null)
			currentAction.DrawInfo();
	}
}
public enum eGroup
{
	Player,
	Enemy,
}

public class SimpleMoveUnit : ActionUnit
{

	public SimpleMoveUnit()
	{
		MoveSpeed = 1;
		MoveAction = new MoveInRangeComponent();
		MoveAction.unit = this;
		AttackAction = new NinDoAttackComponent();
		AttackAction.normalAttack = NormalAttack;
		AttackAction.triggerSkills = triggerSkills;
		AttackAction.unit = this;
	}


}
