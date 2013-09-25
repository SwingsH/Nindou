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
	public Vector3 WorldDirection
	{
		get { return Direction == eDirection.Left ? Vector3.left : Vector3.right; }
	}
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

	//因為不是unity的物件，所以可能會有被unity物件參考或是參考的unity物件的情況造成無法釋放，故需要在此處理
	public virtual void ClearReference()
	{
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
			AnimationState state = Anim.PlayQueued(animInfo.clipName, QueueMode.PlayNow);
			float ns = state.length / animInfo.totalTime;
			state.speed = ns;
		}
		else
		{
			AnimationState state = Anim.PlayQueued(animInfo.clipName, QueueMode.PlayNow);
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
		if (Life > 0)
		{
			if (Random.value < info.Accuracy)
			{
				float rate = 1;
				if (Random.value < info.Critical)
				{
					rate = 1 + info.CriticalBonus;
				}
				Life -= Mathf.CeilToInt(rate * info.Power);
				if(Entity)
					ParticleManager.Emit(info.HitParticle, Entity.transform.position + Vector3.back*10, Entity.transform.up);
				if (Life <= 0)
					BattleManager.Unit_Dead(this);
			}
		}
		
	}
}

public class ActionUnit : AnimUnit
{
	public static int totalCount = 0;
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
			if(value !=null)
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

	CastInfo CurrentCast;
	public bool IsCasting
	{
		get
		{
			if (CurrentCast == null)
				return false;
			return CurrentCast.Casting;
		}
	}

	public ActionUnit()
	{
		totalCount++;
	}

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
			Entity.transform.position =BattleManager.GetRealWorldPos(WorldPos);
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
		if(Anim != null)
			Anim.Blend(AnimationSetting.WALK_ANIM, 1, 0.1f);
	}
	public void StopWalk()
	{
		if (Anim != null)
			Anim.Blend(AnimationSetting.WALK_ANIM, 0, 0.1f);
	}
	public void PlaySkill(MainSkill skill)
	{
		PlayAnim(skill.GenerateAnimInfo());
		CurrentCast = new CastInfo();
		CurrentCast.skill = skill;
	}
	public void CastSkill(MainSkill skill)
	{
		if (IsCasting)
		{
			Debug.Log("CastError");
			return;
		}
		skill.CastableTime = Time.time + skill.CoolDown;
		PlayAnim(skill.GenerateAnimInfo());
		CurrentCast = new CastInfo();
		CurrentCast.skill = skill;
		CurrentCast.EndCount = skill.AnimPlayTimes;
		CurrentCast.Target =GetTarget(skill);
	}
	public List<Unit> GetTarget(MainSkill skill)
	{
		List<Unit> units;
		if (skill.DamageType == SkillDamageType.Damage)
			units = BattleManager.Get_EnemyUnitsInRange(Group, skill.range, Pos, Direction, skill.range);
		else
			units = BattleManager.Get_FriendUnitsInRange(Group, skill.range, Pos, Direction, skill.range);
		if (skill.rangeMode <= BattleSettingValue.AllInRangeModeGroup)
		{
			if (!units.Contains(TargetUnit))
			{
				units.Clear();
				units.Add(TargetUnit);
			}
		}
		return units;
	}

	void UserTriggerDelegate(UserTriggerEvent triggerEvent)
	{
		if (CurrentCast != null)
		{
			if (triggerEvent.animationName != CurrentCast.skill.AnimClipName)
				return;

			switch (triggerEvent.tag)
			{
				case AnimationSetting.HIT_TAG:
					DamageInfo di = CurrentCast.skill.GenerateDamageInfo(this);
					if (CurrentCast.Target != null)
					{
						foreach (Unit u in CurrentCast.Target)
							u.Damaged(di);
					}
					break;
				case AnimationSetting.ATKSTART_TAG:
					ParticleManager.Emit(CurrentCast.skill.ParticleAttackStart, triggerEvent.boneTransform.position, WorldDirection);
					break;
				case AnimationSetting.ATKEND_TAG:
					ParticleManager.Emit(CurrentCast.skill.ParticleAttackEnd, triggerEvent.boneTransform.position,WorldDirection);
					break;
				case AnimationSetting.END_TAG:
					CurrentCast.EndCount--;
					break;
			}
		}
	}

	public override void Draw(Color color)
	{
		base.Draw(color);
		if (currentAction != null)
			currentAction.DrawInfo();
	}

	~ActionUnit()
	{
		totalCount--;	
	}

	public override void ClearReference()
	{
		base.ClearReference();
		Anim.UnregisterUserTriggerDelegate(UserTriggerDelegate);
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

public class CastInfo
{
	public MainSkill skill;
	public bool Casting
	{
		get { return EndCount > 0; }
	}
	public List<Unit> Target;
	public int EndCount;
}
