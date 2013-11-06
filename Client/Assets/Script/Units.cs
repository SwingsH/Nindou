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
	/// <summary>
	/// 最左下角的格子座標
	/// </summary>
	public GridPos BasePos
	{
		get
		{
			if (Pos.Length > 0)
				return Pos[0, 0];
			else
				return GridPos.Null;
		}
	}
	//格子座標←看情況，這個可能不需存在了 2013.10.28
	public GridPos[,] Pos = new GridPos[1,1];

	/// <summary>
	/// 判斷是否為自己的一部份
	/// </summary>
	public bool IsSelf(GridPos gridPos)
	{
		int dx = gridPos.x - BasePos.x;
		int dy = gridPos.y - BasePos.y;
		return dx >= 0 && dy >= 0 && dx < Size && dy < Size;
	}

	protected byte _size;
	//unit佔的格子大小
	//暫時不做resize
	public byte Size
	{
		get { return (byte)Mathf.Max(_size, 1); }
		set { _size = value; }
	}
	public float EntityScale
	{
		get { return 1 + (Size - 1) * 0.8f; }
	}
	private eDirection _direction;
	public eDirection Direction
	{
		get { return _direction; }
		set
		{
			if (_direction != value)
			{
				switch (value)
				{
					case eDirection.Left:
						Entity.transform.localScale = Vector3.one * (1 + (Size - 1) * 0.8f);
						break;
					default:
						Entity.transform.localScale = new Vector3(-1, 1, 1) * (1 + (Size - 1) * 0.8f);
						break;
				}
			}
			_direction = value;
		}
	}
	public Vector3 WorldDirection
	{
		get { return Direction == eDirection.Left ? Vector3.left : Vector3.right; }
	}
	public Vector3 WorldPos;
	public Vector3 ScreenPos
	{
		get { return BattleManager.Get_RealWorldPos(WorldPos); }
	}
	public virtual Vector3 WorldCenter
	{
		get
		{
			if (Entity != null)
				return Entity.transform.position;
			else
				return WorldPos;
		}
	}
	public Color c = Color.white;
	public eGroup Group;

	public bool IsAlive
	{
		get { return Life > 0; }
	}

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
	#if UNITY_EDITOR
	public virtual void Draw(Color color)
	{
		foreach (GridPos gp in Pos)
		{
			if (gp != GridPos.Null)
				BattleManager.Instance.DrawGrid(gp, color);
		}
	}
	#endif
	public override string ToString()
	{
		string result = base.ToString();
		result += string.Format("\nEntity:{0}", Entity == null ? "null":Entity.name);
		result += string.Format("\nGridPos:{0}", BasePos);
		result += string.Format("\nGridWorldPos:{0}", BattleManager.Get_GridWorldPos(BasePos,Size));
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

    public virtual void Update()
    {
    }

	public virtual void RefreshEntity()
	{
		if (Entity)
		{
			Entity.transform.position = BattleManager.Get_RealWorldPos(WorldPos);
			Entity.transform.rotation = Camera.main.transform.rotation;
		}
	}
}

public class AnimUnit : Unit
{
	protected const float BOUND_ZOFFSET = 30;

    public Vector3 LWeaponTip;
    public Vector3 RWeaponTip;
    //角色圖片的範圍
    public Bounds SpriteBounds;
    //角色圖片元件
    public Sprite[] Sprites = new Sprite[0];
    public float _lastDamageTime = 0.0f; // 最後一次收到傷害的 time
    private GameObject _boneContainer = null;

	public override GameObject Entity
	{
		get
		{
			return base.Entity;
		}
		set
		{
			base.Entity = value;
            if (Entity != null)
            {
                //Anim = Entity.GetComponent<BoneAnimation>();
                Anim = Entity.GetComponentInChildren<BoneAnimation>();
                GameObject go = CommonFunction.FindInChildren(Entity, GLOBALCONST.BONE_ROOT_NAME);
                BoneContainer = go.transform.parent.gameObject;
            }
            else
            {
                Anim = null;
                BoneContainer = null;
            }
		}
	}
	/// <summary>
	/// 取整體圖片的上方邊緣中心點，並往前（攝影機方向）BOUND_ZOFFSET 單位，方便放特效
	/// </summary>
	public Vector3 WorldUpperCenter
	{
		get
		{
			if (Entity != null)
			{
				return Entity.transform.TransformPoint(SpriteBounds.center + new Vector3(0, SpriteBounds.extents.y, -BOUND_ZOFFSET));
			}
			else
				return WorldPos;
		}
	}
	/// <summary>
	/// 取整體圖片的中心點，並往前（攝影機方向）BOUND_ZOFFSET 單位，方便放特效
	/// </summary>
	public override Vector3 WorldCenter
	{
		get
		{
			if (Entity != null)
			{
				return Entity.transform.TransformPoint(SpriteBounds.center + Vector3.back * BOUND_ZOFFSET);
			}
			else
				return WorldPos;
		}
	}
	/// <summary>
	/// 取整體圖片的中心點，並往後（攝影機反方向）BOUND_ZOFFSET 單位，方便放特效
	/// </summary>
	public Vector3 WorldCenter_Back
	{
		get
		{
			if (Entity != null)
			{
				return Entity.transform.TransformPoint(SpriteBounds.center + Vector3.forward * BOUND_ZOFFSET);
			}
			else
				return WorldPos;
		}
	}
	/// <summary>
	/// 取整體圖片的左邊界中心點，並往前（攝影機方向）BOUND_ZOFFSET 單位，方便放特效
	/// </summary>
	public Vector3 WorldLeftCenter
	{
		get
		{
			if (Entity != null)
			{
				return Entity.transform.TransformPoint(SpriteBounds.center - new Vector3(SpriteBounds.extents.x, 0, BOUND_ZOFFSET));
			}
			else
				return WorldPos;
		}
	}
	/// <summary>
	/// 取整體圖片的左邊界中心點，並往後（攝影機反方向）BOUND_ZOFFSET 單位，方便放特效
	/// </summary>
	public Vector3 WorldLeftCenter_Back
	{
		get
		{
			if (Entity != null)
			{
				return Entity.transform.TransformPoint(SpriteBounds.center - new Vector3(SpriteBounds.extents.x, 0, -BOUND_ZOFFSET));
			}
			else
				return WorldPos;
		}
	}
	public virtual BoneAnimation Anim
	{
		get;
		protected set;
	}

    /// <summary>
    /// 包含完整的 smooth moves Bone 的 GameObject
    /// </summary>
    public virtual GameObject BoneContainer
    {
        get{return _boneContainer;}
        set{ _boneContainer = value; }
    }

    public virtual GameObject Container
    {
        get;
        set;
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
		RefreshEntity();
	}
	public override void Damaged(DamageInfo info)
	{
		if (info == null)
			return;

		if (Random.value < info.Accuracy)
		{
			float rate = 1;
			if (Random.value < info.Critical)
			{
				rate = 1 + info.CriticalBonus;
			}
			float value = Mathf.Ceil(rate * info.Power * Random.Range(GLOBALCONST.BattleSettingValue.ATTACK_RANDOMRATE_MIN, GLOBALCONST.BattleSettingValue.ATTACK_RANDOMRATE_MAX));

			//跳血跟受擊效果
			if (Entity)
			{
				ParticleManager.Emit(info.HitParticle, WorldCenter + Vector3.back, WorldDirection);

				switch (info.TargetType)
				{
					case SkillTargetType.Friend:
						BattleManager.ShowHealText(Mathf.RoundToInt(value), WorldUpperCenter + Vector3.back);
						break;
					case SkillTargetType.Enemy:
						if (info.MultiHit)
							BattleManager.ShowDamageGroupText(info, this, Mathf.RoundToInt(value), WorldUpperCenter + Vector3.back);
						else
							BattleManager.ShowDamageText(Group, Mathf.RoundToInt(value), WorldUpperCenter + Vector3.back);

                        StartDamageEffect();
						break;
				}
			}
			if (info.TargetType == SkillTargetType.Friend)
				value *= -1;
			Damaged(value);
		}
		else if (Entity)
			BattleManager.ShowMissText(WorldUpperCenter);
		
	}
	protected void Damaged(float value)
	{
		if (Life > 0)
		{
			Life -= value;
			if (Life <= 0)
				BattleManager.Unit_Dead(this);
		}
	}

    /// <summary>
    /// 開始處理受擊效果
    /// </summary>
    private void StartDamageEffect()
    {
        // 受擊後變色效果   todo: 考慮是否需要配合 hit 點
        _lastDamageTime = Time.time;
        ChangeColor(GLOBALCONST.BattleSettingValue.AVATAR_DAMAGE_COLOR);
    }

    /// <summary>
    /// 結束受擊效果
    /// </summary>
    private void EndDamageEffect()
    {
        ChangeColor(GLOBALCONST.BattleSettingValue.AVATAR_NORMAL_COLOR);

        if (BoneContainer != null) // 震動效果歸位
            BoneContainer.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 更改所有 sprite 的顏色
    /// </summary>
    public void ChangeColor(Color newColor)
    {
        if(Sprites == null)
            return;
        if(Sprites.Length <= 0)
            return;

        for (int i = 0; i < Sprites.Length; i++)
        {
			if(Sprites[i] != null)
				Sprites[i].SetColor(newColor);
        }
    }

    // 震動效果
    public void Shake(float percent)
    {
        if (Entity == null)
        {
            CommonFunction.DebugMsg("Entity is null");
            return;
        }
        if (BoneContainer == null)
        {
            CommonFunction.DebugMsg("BoneContainer is null");
        }

        float range = GLOBALCONST.BattleSettingValue.AVATAR_DAMAGE_SHAKE_RANGE * percent;
        float x = Random.Range(range, -range);
        BoneContainer.transform.localPosition = new Vector3(x,BoneContainer.transform.localPosition.y,BoneContainer.transform.localPosition.z);
    }

    /// <summary>
    /// Every frame-rate 更新一次
    /// </summary>
    public override void Update()
    {
        float interval = Time.time - _lastDamageTime;
        float shakePercent = interval / GLOBALCONST.BattleSettingValue.AVATAR_DAMAGE_COLOR_TIME; //晃動程度, 隨受擊時間點遞減

        if (interval > GLOBALCONST.BattleSettingValue.AVATAR_DAMAGE_COLOR_TIME)
        {
            EndDamageEffect();
            return;
        }

        Shake(shakePercent);
    }

	public void Hide()
	{
		if (Anim)
			Anim.transform.localScale = Vector3.zero;
	}
	public void Show()
	{
		if (Anim)
			Anim.transform.localScale = Vector3.one;
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
	public ActionComponent AttackAction;
	public GridPos TestOrderMove = GridPos.Null;

	eMoveState _moveState;
	public eMoveState MoveState
	{
		get
		{
			return _moveState;
		}
		protected set
		{
			_moveState = value;
		}
	}

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
			if (value != null)
			{
				Anim.RegisterUserTriggerDelegate(AnimUserTriggerDelegate);
				Anim.Play(AnimationSetting.WALK_ANIM);
				Anim.Play(AnimationSetting.IDLE_ANIM);
			}
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
			if (value != null)
			{
				if (value.Type == SkillType.Weapon)
				{
					foreach (SpecialEffect spe in value.SPEffect)
						if (spe.EffectType == (byte)SPEffectType.ExtrimSkill)
						{
							ExtrimSkill = new MainSkill(InformalDataBase.Instance.GetSkillData(spe.EffectPower));
						}
					_normalAttack = value;
				}
				else
				{
					Debug.LogError("NormalAttack Type Error: ID " + value.SkillID.ToString());
					_normalAttack = new MainSkill(GLOBALCONST.BattleSettingValue.DEFAULT_NORMAL_ATTACK);
				}
			}
			else
				_normalAttack = null;
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
	List<MainSkill> _activeSkills = new List<MainSkill>();
	public IList<MainSkill> ActiveSkills
	{
		get
		{
			//回傳一個新複製的list
			//防止被加入不合法的技能，這樣只能用set來設技能，set會做檢查
			//return new List<MainSkill>(_activeSkills);
			return _activeSkills.AsReadOnly();
		}
		set
		{
			_activeSkills = new List<MainSkill>(value);
			CheckSkillList(ref _activeSkills, SkillType.Active);
		}
	}

	List<MainSkill> _passiveSkill = new List<MainSkill>();

	public IList<MainSkill> PassiveSkill
	{
		get
		{
			//回傳一個新複製的list
			//防止被加入不合法的技能，這樣只能用set來設技能，set會做檢查
			//return new List<MainSkill>(_passiveSkill);
			return _passiveSkill.AsReadOnly();
		}
		set
		{
			_passiveSkill =new List<MainSkill>(value);
			CheckSkillList(ref _passiveSkill, SkillType.Passive);
			if (Passive != null)
				Passive.Reset();
			else
				Passive = new PassiveEffectInfo();
			Passive.AddPassiveSkills(_passiveSkill);

		}
	}
	public PassiveEffectInfo Passive = new PassiveEffectInfo();
	public StateInfo State = new StateInfo();

	//施展中技能
	Queue<MainSkill> CastQueue = new Queue<MainSkill>();
	public MainSkill CurrentCast
	{
		get
		{
			if (CastQueue.Count == 0)
				return null;
			return CastQueue.Peek();
		}
	}
	CastInfo CurrentCasting;
	
	public bool IsCasting
	{
		get
		{
			return CurrentCasting != null && CurrentCasting.Casting;
		}
	}

	public ActionUnit()
	{
		AttackAction = new NinDoAttackComponent(this);
		totalCount++;
	}
	public ActionUnit(bool isPreview):this()
	{
		totalCount++;
		PreviewUnit = isPreview;
	}

	public int AttackRangeMode
	{
		get
		{
			if (CastQueue.Count > 0)
			{
				MainSkill temp = CastQueue.Peek();
				if (temp != null)
				{
					return temp.rangeMode;
				}
			}
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
			if (CastQueue.Count > 0)
			{
				MainSkill temp = CastQueue.Peek();
				if (temp != null)
				{
					return temp.range;
				}
			}
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
		
		Damaged(this.State.DoT);

		TargetUnit = FindAttackTarget(eTargetMode.Closest);
		if (TargetUnit != null)
			MoveAction.Target = TargetUnit.BasePos;
		else
			MoveAction.Target = GridPos.Null;

		if (currentAction != null && currentAction.State == ActionState.Busy)
		{
			currentAction.Active();
		}
		else
		{
			DecideSkill();
			currentAction = DecideAction();
			if (currentAction != null)
				currentAction.Active();
		}
		RefreshEntity();

		//更新狀態時間
		this.State.Reflash();
	}

    /// <summary>
    /// 搜索攻擊目標
    /// </summary>
	protected virtual Unit FindAttackTarget(eTargetMode mode)
	{
		switch (mode)
		{
			case eTargetMode.Closest:
				{
					Unit result = TargetUnit;
					float rScore = float.MinValue;
					if (TargetUnit != null && result.IsAlive)
					{
						GridPos closestPos = GridPos.Null;//離目標最近的自己的格子
						GridPos closestTargetPos = GridPos.Null;//離自己最近的目標的格子
						BattleManager.GetClosestPos(this, TargetUnit, out closestPos, out closestTargetPos);
						rScore = -GridPos.SimpleDistance(closestPos, closestTargetPos);
						if (BattleManager.CheckInRange(AttackRangeMode, this, eDirection.Both, AttackRange, TargetUnit))
							rScore += 2;
						else if (BattleManager.Get_MovablePos_AroundTarget_InAttackRange(this,TargetUnit,AttackRangeMode, AttackRange).Count == 0)
							rScore -= 20;
					}
					else
						result = null;
					foreach (Unit u in BattleManager.Get_AllEnemyUnits(Group))
					{
						if (u == null || !u.IsAlive)
							continue;
						GridPos closestPos = GridPos.Null;//離目標最近的自己的格子
						GridPos closestTargetPos = GridPos.Null;//離自己最近的目標的格子
						BattleManager.GetClosestPos(this, u, out closestPos, out closestTargetPos);

						float uScore = -GridPos.SimpleDistance(closestPos, closestTargetPos);
						if (BattleManager.CheckInRange(AttackRangeMode, this, eDirection.Both, AttackRange, u))
							uScore += 1;
						else if (BattleManager.Get_MovablePos_AroundTarget_InAttackRange(this, u, AttackRangeMode, AttackRange).Count == 0) 
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
	protected ActionComponent DecideAction()
	{
		if (IsCasting)
			return null;
		if (MoveState == eMoveState.Closer)
		{
			if (AttackAction.State == ActionState.Idle)
				return AttackAction;
			if (MoveAction.State == ActionState.Idle)
				return MoveAction;
		}
		else if (MoveState == eMoveState.KeepRange)
		{
			//移動優先，避免一直都移動，可是其實沒在移動，先這樣處理
			if (MoveAction.State == ActionState.Idle)
			{
				MoveAction.Active();
				if (MoveAction.State == ActionState.Busy)
					return MoveAction;
			}
			if (AttackAction.State == ActionState.Idle)
				return AttackAction;	
		}

		return null;
	}

	//選擇要施法的技能
	protected void DecideSkill()
	{
		if (IsCasting)
			return;
		while (CastQueue.Count > 0 && (!CastQueue.Peek().Castable || !TargetApprochable(CastQueue.Peek())))
		{
			CastQueue.Dequeue();
		}
		if (CastQueue.Count == 0)
		{
			List<MainSkill> randomList = new List<MainSkill>();
			foreach (MainSkill ms in _activeSkills)
			{
				if (ms.Castable && TargetApprochable(ms))
					randomList.Add(ms);
			}
			MainSkill randomSkill = null;
			if (randomList.Count > 0)
				randomSkill = randomList[Random.Range(0, randomList.Count)];
			if (randomSkill != null)
				CastQueue.Enqueue(randomSkill);
			else if (NormalAttack.Castable)
				CastQueue.Enqueue(NormalAttack);
		}
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
	//播放技能，差別只在於沒有施放目標，為了要播放particle所以也要建CastInfo
	public void PlaySkill(MainSkill skill)
	{
		if (PreviewUnit)
		{
			PlayAnim(skill.GenerateAnimInfo());
			CastInfo info = new CastInfo(skill);

			CurrentCasting = info;
			skill.CastableTime = Time.time + skill.CoolDown;
			PlayAnim(info.skill.GenerateAnimInfo());
		}
	}
	protected void CastSkill(MainSkill skill)
	{
		if (IsCasting)
			return;

		if (skill == null || !skill.Castable)
			return;
		List<Unit> target = GetTarget(skill);
		if (target.Count > 0)
		{
			CastInfo info = new CastInfo(skill);
			info.DMGInfo = skill.GenerateDamageInfo(this);
			CurrentCasting = info;
			CurrentCasting.Target = target;
			skill.CastableTime = Time.time + skill.CoolDown;
			PlayAnim(info.skill.GenerateAnimInfo());
		}
	}
	
	//判斷是否可以靠近現在的目標到skill的射程中
	protected bool TargetApprochable(MainSkill skill)
	{
		if (TargetUnit == null)
			return false;
		//先判斷是不是已經在範圍中了
		//不先判斷這個，接下來判斷是否有空格可以達到會有誤判的情況
		if(BattleManager.CheckInRange(skill.rangeMode,BasePos,eDirection.Both,skill.range,TargetUnit.BasePos))
			return true;
		//判斷是否還有空格可以移動到目標可以進入攻擊範圍的位置
		return BattleManager.Get_EmptyGrid(skill.rangeMode, TargetUnit.BasePos, eDirection.Both, skill.range).Count != 0;
	}

	public void CastCurrentSkill()
	{
		CastSkill(CastQueue.Dequeue());
	}
	//void StartCast()
	//{
	//    while (CurrentCasting.Count > 0)
	//    {
	//        CastInfo info = CurrentCasting.Peek();
	//        if (info.skill == null || !info.skill.Castable)
	//        {
	//            CurrentCasting.Dequeue();
	//            continue;
	//        }
	//        info.Target = GetTarget(info.skill);
	//        if (info.Target.Count > 0)
	//        {
	//            info.skill.CastableTime = Time.time + info.skill.CoolDown;
	//            PlayAnim(info.skill.GenerateAnimInfo());
	//            break;
	//        }
	//        else
	//        {
	//            CurrentCasting.Dequeue();
	//        }
	//    }
	//}
	public void CastExtrimSkill()
	{
		if(ExtrimSkill == null || !ExtrimSkill.Castable)
			return;
		if (!CastQueue.Contains(ExtrimSkill))
			CastQueue.Enqueue(ExtrimSkill);
	}
	void StopAllCast()
	{
		CurrentCasting = null;
		if (Anim != null)
		{
			Anim.Stop();
			StopWalk();
		}
	}
	public List<Unit> GetTarget(MainSkill skill)
	{
		if (skill == null)
			return new List<Unit>();
		List<Unit> units;
		switch(skill.TargetType)
		{
			case SkillTargetType.Enemy:
				units = BattleManager.Get_EnemyUnitsInRange(this, skill.rangeMode, Direction, skill.range);
				break;
			case SkillTargetType.Friend:
				units = BattleManager.Get_FriendUnitsInRange(this, skill.rangeMode, Direction, skill.range);
				break;
			case SkillTargetType.Self:
				units = new List<Unit>();
				units.Add(this);
				break;
			default:
				units = new List<Unit>();
				break;
		}
		if (units.Count == 0)
			return units;
		//單體目標技能，如現目標在範圍內用現在目標，不然用list裡第一個當目標
		if (skill.rangeMode <= GLOBALCONST.BattleSettingValue.AllInRangeModeGroup)
		{
			Unit singleUnit = null;
			if (units.Contains(TargetUnit))
				singleUnit = TargetUnit;
			else
				singleUnit = units[0];
			units.Clear();
			units.Add(singleUnit);
		}
		return units;
	}

	void AnimUserTriggerDelegate(UserTriggerEvent triggerEvent)
	{
		if (CurrentCasting != null)
		{
			CastInfo info = CurrentCasting;
			if (triggerEvent.animationName != info.skill.AnimClipName)
				return;

			switch (triggerEvent.tag)
			{
				case AnimationSetting.HIT_TAG:
					if (info.Target != null && info.DMGInfo.Attacker == this)
					{
						if (info.skill.ProjectileTime <= 0) //瞬發攻擊
						{
							foreach (Unit u in info.Target)
								u.Damaged(info.DMGInfo);
						}
						else
						{
							foreach (Unit u in info.Target)
							{
								float flyTime = info.skill.ProjectileTime;
								if(info.skill.range > 0)
									flyTime *= GridPos.SimpleDistance(u.BasePos, BasePos)/info.skill.range;
								ProjectileAttack.LaunchProjectile(info.DMGInfo, WorldLeftCenter, u, info.skill.ProjectileParticle, flyTime);
							}
						}
					}
					break;
				case AnimationSetting.ATKSTART_TAG:
					ParticleManager.Emit(info.skill.ParticleAttackStart, triggerEvent.boneTransform.position - BattleManager.UnitCamera.transform.forward * BOUND_ZOFFSET, WorldDirection);
					break;
				case AnimationSetting.ATKEND_TAG:
					ParticleManager.Emit(info.skill.ParticleAttackEnd, triggerEvent.boneTransform.position - BattleManager.UnitCamera.transform.forward * BOUND_ZOFFSET, WorldDirection);
					break;
				case AnimationSetting.END_TAG:
					info.EndCount--;
					if(!IsCasting)
						CurrentCasting = null;
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
		if (info == null)
			return;
		base.Damaged(info);
		foreach (SpecialEffect se in info.SPEffects)
		{
			if (Random.value < se.EffectChance)
				State.AddState(se);
		}
	}
	#if UNITY_EDITOR
	public override void Draw(Color color)
	{
		DrawSkillRange(color);
		base.Draw(color);
	}
	public void DrawSkillRange(Color color)
	{
		MainSkill sk = null;
		if (CurrentCasting != null)
			sk = CurrentCasting.skill;
		else if (CurrentCast != null)
			sk = CurrentCast;
		if(sk != null)
		{
			Color temp = color;
			temp *=0.5f;
			foreach (GridPos upos in Pos)
				foreach (GridPos gp in BattleManager.Get_Grids(sk.rangeMode, upos, eDirection.Both, sk.range))
				{
					BattleManager.Instance.DrawGrid(gp, temp);
				}
		}
	}
	#endif

	~ActionUnit()
	{
		totalCount--;	
	}

	public override void ClearReference()
	{
		base.ClearReference();
		if(Anim != null)
			Anim.UnregisterUserTriggerDelegate(AnimUserTriggerDelegate);
		Entity = null;
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

	public DamageInfo DMGInfo;
}
