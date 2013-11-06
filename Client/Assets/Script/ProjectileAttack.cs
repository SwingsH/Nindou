using UnityEngine;
using System.Collections;


//飛道具型攻擊
/*
 * 目前規劃不要有任何直接操作particle的行為，只做掛載
 * 所以這個是用來掛particle的，然後讓這個來執行位移跟傷害資訊的傳遞
 */
public class ProjectileAttack : MonoBehaviour
{
	private eProjectileKind _projectileKind= eProjectileKind.None;
	protected eProjectileKind ProjectileKind 
	{
		get { return _projectileKind;}
		set 
		{ 
			_projectileKind = value;
			switch(_projectileKind)
			{
				case eProjectileKind.ToTargetPosition: 
					MoveDelegate = ToPos;
					break;
				case eProjectileKind.ToTargetUnit: 
					MoveDelegate = ToUnit;
					break;
				default :
					MoveDelegate = null;
					break;
			}
		}
	}
	SimpleDelegate MoveDelegate;
	DamageInfo DMGInfo;
	Unit Target;
	Vector3 TargetWorldPos;

	//這個時間點到達目標，不論目標在哪
	//而且超過了particle會被回收
	float ReachTime;

	//測試用
	string EndParticle;
	// Use this for initialization
	void Start () {
		gameObject.layer = GLOBALCONST.GameSetting.LAYER_UNIT;
	}
	
	// Update is called once per frame
	void Update () {
		if(MoveDelegate != null)
			MoveDelegate();
		else
		{
			ParticleManager.UnmountAllParticle(transform);
			Destroy(gameObject);
			
			string attacker = "空的";
			string target = "空的";
			if(DMGInfo.Attacker != null && DMGInfo.Attacker.Entity != null)
				attacker = DMGInfo.Attacker.Entity.name;
			if(Target.Entity != null)
				target = Target.Entity.name;
			CommonFunction.DebugError(string.Format("未指定飛行道具的類型，攻擊者：{0}，目標：{1}",attacker,target));
		}
	}
	void ToPos()
	{
		ReflashSelfPos();
	}
	void ToUnit()
	{
		if (Target == null || !Target.IsAlive)
		{
			OnReach();
			return;
		}
		ReflashTargetPos();
		ReflashSelfPos();
	}
	//重新計算目標的位置，只有目標為單位（Unit）的時候才需要做
	void ReflashTargetPos()
	{
		TargetWorldPos = Target.WorldCenter;
	}
	void OnReach()
	{
		if (ProjectileKind == eProjectileKind.ToTargetUnit)
		{
			if (Target != null)
				Target.Damaged(DMGInfo);
		}
		if (!string.IsNullOrEmpty(EndParticle))
			ParticleManager.Emit(EndParticle, transform.position, transform.forward);
		ParticleManager.UnmountAllParticle(transform);
		this.enabled = false;
		Destroy(gameObject);
	}
	//往目標移動座標，時間到了的話就對目標造成傷害，然後自毀
	void ReflashSelfPos()
	{
		float lastTime = ReachTime - Time.time;
		if (lastTime <= 0)
		{
			transform.localPosition = TargetWorldPos;
			OnReach();
		}
		else
		{
			transform.forward = (TargetWorldPos - transform.position).normalized;
			transform.position = Vector3.Lerp(transform.position, TargetWorldPos, Time.deltaTime / lastTime);
		}
	}
	/// <summary>
	/// 測試用prototype
	/// </summary>
	public static void LaunchProjectile_ForTest(GridPos startPosition,GridPos targetPosition,string projectileParticle,string EndEffect,float Duration)
	{
		ProjectileAttack pAtk = new GameObject().AddComponent<ProjectileAttack>();
		pAtk.ProjectileKind = eProjectileKind.ToTargetPosition;
		pAtk.EndParticle = EndEffect;
		pAtk.ReachTime = Time.time + Duration;
		pAtk.TargetWorldPos = BattleManager.Get_RealWorldPos(targetPosition);
		pAtk.transform.position = BattleManager.Get_RealWorldPos(startPosition);
		ParticleManager.MountParticle(pAtk.transform,projectileParticle);

	}
	public static void LaunchProjectile(DamageInfo DMGInfo, GridPos startPosition, GridPos targetPosition, string projectileParticle, float Duration)
	{
		ProjectileAttack pAtk = new GameObject().AddComponent<ProjectileAttack>();
		pAtk.ProjectileKind = eProjectileKind.ToTargetPosition;
		pAtk.ReachTime = Time.time + Duration;
		pAtk.TargetWorldPos = BattleManager.Get_RealWorldPos(targetPosition);
		pAtk.transform.position = BattleManager.Get_RealWorldPos(startPosition);
		ParticleManager.MountParticle(pAtk.transform, projectileParticle);
	}
	/// <summary>
	/// 發射投射物
	/// </summary>
	/// <param name="DMGInfo">傷害資訊</param>
	/// <param name="startWorldPosition">開始格子座標</param>
	/// <param name="target">目標單位</param>
	/// <param name="projectileParticle">投射物光影名稱</param>
	/// <param name="Duration">飛行時間</param>
	public static void LaunchProjectile(DamageInfo DMGInfo, GridPos startPosition, Unit target, string projectileParticle, float Duration)
	{
		LaunchProjectile(DMGInfo, BattleManager.Get_RealWorldPos(startPosition), target, projectileParticle, Duration);
	}
	/// <summary>
	/// 發射投射物
	/// </summary>
	/// <param name="DMGInfo">傷害資訊</param>
	/// <param name="startWorldPosition">開始世界座標</param>
	/// <param name="target">目標單位</param>
	/// <param name="projectileParticle">投射物光影名稱</param>
	/// <param name="Duration">飛行時間</param>
	public static void LaunchProjectile(DamageInfo DMGInfo, Vector3 startWorldPosition, Unit target, string projectileParticle, float Duration)
	{
		if (target == null || !target.IsAlive)
			return;
		ProjectileAttack pAtk = new GameObject().AddComponent<ProjectileAttack>();
		pAtk.Target = target;
		pAtk.DMGInfo = DMGInfo;
		pAtk.ProjectileKind = eProjectileKind.ToTargetUnit;
		pAtk.ReachTime = Time.time + Duration;
		pAtk.transform.position = startWorldPosition;
		ParticleManager.MountParticle(pAtk.transform, projectileParticle);
	}
}

public enum eProjectileKind
{
	None,
	ToTargetUnit,
	ToTargetPosition,
}
