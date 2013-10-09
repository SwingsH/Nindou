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

	public virtual uint MaxLife
	{
		get;
		set;
	}
	public virtual float Life
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

	public override uint MaxLife
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
	public override float Life
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
		//if (Life > 0)
		//{
			if (Random.value < info.Accuracy)
			{
				float rate = 1;
				if (Random.value < info.Critical)
				{
					rate = 1 + info.CriticalBonus;
				}
				Damaged(Mathf.Ceil(rate * info.Power));
				if(Entity)
					ParticleManager.Emit(info.HitParticle, Entity.transform.position + Vector3.back*10, Entity.transform.up);
			}
		//}
	}
	protected void Damaged(float value)
	{
		Life -= value;
		if (Life <= 0)
			BattleManager.Unit_Dead(this);
	}
}

public class ActionUnit : AnimUnit
{
	public static int totalCount = 0;
	public bool PreviewUnit =false;
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
		protected set
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
				Anim.UnregisterUserTriggerDelegate(AnimUserTriggerDelegate);
			base.Anim = value;
			if(value !=null)
				Anim.RegisterUserTriggerDelegate(AnimUserTriggerDelegate);
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
			if (value.Type == SkillType.Weapon)
			{
				foreach(SpecialEffect spe in value.SPEffect)
					if (spe.EffectType == (byte)SPEffectType.ExtrimSkill)
					{
						ExtrimSkill = new MainSkill(TestDataBase.Instance.GetSkillData(spe.EffectPower));
					}
				_normalAttack = value;
			}
			else
			{
				Debug.LogError("NormalAttack Type Error");
				_normalAttack = new MainSkill(GLOBALCONST.BattleSettingValue.DEFAULT_NORMAL_ATTACK);
			}
			if (AttackAction != null)
				AttackAction.normalAttack = _normalAttack;	
			if (_normalAttack != null)
			{
				if (_normalAttack.range > 2)
					MoveState = eMoveState.KeepRange;
				else
					MoveState = eMoveState.Closer;
			}
		}
	}
	MainSkill _extrimSkill;
	public MainSkill ExtrimSkill
	{
		get
		{
			return _extrimSkill;
		}
		protected set
		{
			if (value.Type == SkillType.Extrim)
				_extrimSkill = value;
			else
				_extrimSkill = null;
		}
	}
	List<MainSkill> _triggerSkills = new List<MainSkill>();
	public List<MainSkill> triggerSkills
	{
		get
		{
			return new List<MainSkill>(_triggerSkills);
		}
		set
		{
			_triggerSkills = value;
			CheckSkillList(ref _triggerSkills, SkillType.Active);
			if (AttackAction != null)
			{
				AttackAction.triggerSkills = _triggerSkills;
			}
		}
	}

	List<MainSkill> _passiveSkill = new List<MainSkill>();

	public List<MainSkill> PassiveSkill
	{
		get
		{
			return new List<MainSkill>(_passiveSkill);
		}
		set
		{
			_passiveSkill = value;
			CheckSkillList(ref _passiveSkill, SkillType.Passive);
			if (Passive != null)
			{
				Passive.Reset();
				Passive.AddPassiveSkills(_passiveSkill);
			}
		}
	}
	public PassiveEffectInfo Passive = new PassiveEffectInfo();
	public StateInfo State = new StateInfo();

	Queue<CastInfo> CurrentCast = new Queue<CastInfo>();
	
	public bool IsCasting
	{
		get
		{
			if (CurrentCast.Count == 0)
				return false;
			else
				return true;
		}
	}

	public ActionUnit()
	{
		AttackAction = new NinDoAttackComponent();
		AttackAction.normalAttack = NormalAttack;
		AttackAction.triggerSkills = triggerSkills;
		AttackAction.unit = this;

		totalCount++;
	}
	public ActionUnit(bool isPreview):this()
	{
		totalCount++;
		PreviewUnit = isPreview;
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
	public int _moveSpeed;
	public int MoveSpeed
	{
		get
		{
			return _moveSpeed;
		}
		set
		{
			_moveSpeed = value;
		}
	}

	public override void Run()
	{
		//if (mc.State == ActionState.Idle)
		//    RandomMove();
		this.State.Reflash();
		Damaged(this.State.DoT);

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
		if (IsCasting)
			return null;
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
	//播放技能，差別只在於沒有施放目標，為了要放particle所以也要建CastInfo
	public void PlaySkill(MainSkill skill)
	{
		if (PreviewUnit)
		{
			PlayAnim(skill.GenerateAnimInfo());
			CurrentCast.Clear();
			CurrentCast.Enqueue(new CastInfo(skill));
		}
	}
	public void CastSkill(MainSkill skill)
	{
		if (skill == null)
			return;
		CastInfo info = new CastInfo(skill);
		
		CurrentCast.Enqueue(info);
		if (CurrentCast.Count == 1)
			StartCast();
	}
	void StartCast()
	{
		while (CurrentCast.Count > 0)
		{
			CastInfo info = CurrentCast.Peek();
			if (info.skill == null || !info.skill.Castable)
			{
				CurrentCast.Dequeue();
				continue;
			}
			info.Target = GetTarget(info.skill);
			if (info.Target.Count > 0)
			{
				info.skill.CastableTime = Time.time + info.skill.CoolDown;
				PlayAnim(info.skill.GenerateAnimInfo());
				break;
			}
			else
			{
				CurrentCast.Dequeue();
			}
		}
	}
	public void CastExtrimSkill()
	{
		if (ExtrimSkill != null && ExtrimSkill.Castable)
			CastSkill(ExtrimSkill);
	}

	public List<Unit> GetTarget(MainSkill skill)
	{
		if (skill == null)
			return new List<Unit>();
		List<Unit> units;
		if (skill.DamageType == SkillDamageType.Damage)
			units = BattleManager.Get_EnemyUnitsInRange(Group, skill.range, Pos, Direction, skill.range);
		else
			units = BattleManager.Get_FriendUnitsInRange(Group, skill.range, Pos, Direction, skill.range);
		if (skill.rangeMode <= GLOBALCONST.BattleSettingValue.AllInRangeModeGroup)
		{
			if (!units.Contains(TargetUnit))
			{
				units.Clear();
				units.Add(TargetUnit);
			}
		}
		return units;
	}

	void AnimUserTriggerDelegate(UserTriggerEvent triggerEvent)
	{
		if (CurrentCast.Count > 0)
		{
			CastInfo info = CurrentCast.Peek();
			if (triggerEvent.animationName != info.skill.AnimClipName)
				return;

			switch (triggerEvent.tag)
			{
				case AnimationSetting.HIT_TAG:
					DamageInfo di = info.skill.GenerateDamageInfo(this);
					if (info.Target != null)
					{
						foreach (Unit u in info.Target)
							u.Damaged(di);
					}
					break;
				case AnimationSetting.ATKSTART_TAG:
					ParticleManager.Emit(info.skill.ParticleAttackStart, triggerEvent.boneTransform.position, WorldDirection);
					break;
				case AnimationSetting.ATKEND_TAG:
					ParticleManager.Emit(info.skill.ParticleAttackEnd, triggerEvent.boneTransform.position, WorldDirection);
					break;
				case AnimationSetting.END_TAG:
					info.EndCount--;
					if (!CurrentCast.Peek().Casting)
					{
						CurrentCast.Dequeue();
						if (CurrentCast.Count > 0)
							StartCast();
					}
					break;
				case AnimationSetting.START_TAG:
					if (info.StartCount == 0)
					{
						if (info.skill.Type == SkillType.Extrim)
							TimeMachine.ChangeTimeScale(0.1f,0.5f);
					}
					info.StartCount++;
					break;
			}
		}
	}

	public override void Damaged(DamageInfo info)
	{
		base.Damaged(info);
		foreach (SpecialEffect se in info.SPEffects)
		{
			if (Random.value < se.EffectChance)
				State.AddState(se);
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
		Anim.UnregisterUserTriggerDelegate(AnimUserTriggerDelegate);
	}
	
	public static void CheckSkillList(ref List<MainSkill> skills, SkillType specifiedType)
	{
		if(skills == null)
			return;
		int index = 0;

		while (index < skills.Count)
		{
			if (skills[index].Type != specifiedType)
			{
				skills.RemoveAt(index);
				continue;
			}
			index++;
		}
	}
}
public enum eGroup
{
	Player,
	Enemy,
}

public class CastInfo
{
	public MainSkill skill;
	public bool Casting
	{
		get { return EndCount > 0; }
	}
	public List<Unit> Target;
	public int StartCount;
	public int EndCount;
	public CastInfo(MainSkill sk)
	{
		skill = sk;
		EndCount = skill.AnimPlayTimes;
	}
	public void CancelCast()
	{
		EndCount = 0;
	}
}
