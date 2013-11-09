using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * 新版的ParticleSystem沒有自我刪除的功能，加上手持不適合一直實體化物件，所以在這裡做觀察回收的動作
 * 為了要重複使用同一個ParticleSystem,故不要修改參數
 */
public class ParticleManager : MonoBehaviour {

	public const int RESERVE_AMOUNT = 5;
	private static ParticleManager Instance;
	
	//以 particle 名稱當 key , 儲存一所有在場景中運作中的 particle
	static Dictionary<string, List<ParticleSystem>> AvailableParticles = new Dictionary<string, List<ParticleSystem>>();
	/*
	 * 存放掛載型particle的資料，方便刪除用
	 * 用Transform當key，這樣比較容易發現Transform被刪掉
	 */
	static Dictionary<Transform, List<ParticleSystem>> mountedParticle = new Dictionary<Transform, List<ParticleSystem>>();

	static Dictionary<ParticleSystem, float> continuedParticle = new Dictionary<ParticleSystem, float>();
	float mountedCheckTime = 0;
	List<ParticleSystem> actingParticleSystem = new List<ParticleSystem>();
#if UNITY_EDITOR
	public ParticleSystem[] displayingArray; //目前顯示中的
#endif
	void Awake()
	{
		if (Instance)
		{
			Debug.LogError("重複的ParticleManager");
			DestroyImmediate(this);
		}
		else
			Instance = this;

	}
	// Use this for initialization
	void Start () {


	}


	/*
	 * 觀察所有的particle，不是Alive的就回收
	 * 不是Alive經測試要兩個條件都符合，一個是所有的粒子都消失了，一個是ParticleSystem的duration過了
	 */
	void LateUpdate()
	{
		int i = 0 ;
		while (i < actingParticleSystem.Count)
		{
			if (actingParticleSystem[i] == null)
			{
				actingParticleSystem.RemoveAt(i);
				continue;
			}
			if (actingParticleSystem[i].IsAlive())
				i++;
			else
			{
				Recycle(actingParticleSystem[i]);
				actingParticleSystem.RemoveAt(i);
			}
		}
		//檢查有掛載Particle的Transform有沒有被刪除的，有的話就回收並清除紀錄
		//雖然應該是會一起被砍了
		if (Time.time > mountedCheckTime)
		{
			mountedCheckTime +=3;
			if (mountedParticle.Count > 0)
			{
				List<Transform> removeKey = new List<Transform>();
				foreach (KeyValuePair<Transform,List<ParticleSystem>> kvp in mountedParticle)
				{
					if (kvp.Key == null)
					{
						removeKey.Add(kvp.Key);
						foreach (ParticleSystem ps in kvp.Value)
						{
							Debug.Log(ps);
							if(ps!=null)
								Recycle(ps);
						}
						kvp.Value.Clear();
					}
				}
				foreach (Transform trans in removeKey)
				{
					mountedParticle.Remove(trans);
				}
			}
		}
		if(continuedParticle.Count > 0)
		{
			List<ParticleSystem> removedPSkey = null;
			foreach (KeyValuePair<ParticleSystem, float> kvp in continuedParticle)
			{
				if (kvp.Value < Time.time)
				{
					if (removedPSkey == null)
						removedPSkey = new List<ParticleSystem>();
					removedPSkey.Add(kvp.Key);
				}
			}
			if (removedPSkey != null)
				foreach (ParticleSystem psi in removedPSkey)
				{
					if(psi != null)
						psi.Stop(true);//停止，等粒子都播完之後就會回收了
					continuedParticle.Remove(psi);
				}
		}

#if UNITY_EDITOR
		displayingArray = actingParticleSystem.ToArray();
#endif
	}

	void KeepWatchParticle(ParticleSystem particle)
	{
		if (!actingParticleSystem.Contains(particle))
			actingParticleSystem.Add(particle);
	}

	public static void Emit(string particleName, Vector3 worldPos, Vector3? direction)
	{
		if (string.IsNullOrEmpty(particleName))
			return;
		ParticleSystem ps = GetParticle(particleName);
		if (ps)
		{
			if(direction != null)
				ps.transform.forward = direction.Value;
			ps.transform.position = worldPos;
			ps.Play();
		}
	}
	/// <summary>
	/// 連續播放一個光影 n 秒
	/// </summary>
	//ParticleSystem的Duration不能改，據說可以用動畫來改參數，還沒試過，先自己計算時間
	public static void Emit_Continue(string particleName, Vector3 worldPos, Vector3? direction, float Duration)
	{
		if (string.IsNullOrEmpty(particleName))
			return;
		ParticleSystem ps = GetParticle(particleName);
		if (ps)
		{
			if (direction != null)
				ps.transform.forward = direction.Value;
			ps.transform.position = worldPos;
			ps.loop = true;
			ps.Play();
			//這個應該是不會發生，不過還是檢查一下
			if (continuedParticle.ContainsKey(ps))
				continuedParticle[ps] = Time.time + Duration;
			else
				continuedParticle.Add(ps, Time.time + Duration);
		}

	}
	public static void MountParticle(Transform trans, string particleName)
	{
		MountParticle(trans, particleName, null);
	}
	/*
	 * 掛載持續播放的particle
	 * 需要手動呼叫才會停止
	 * 同一個Transform可以掛載多種，不過一種（同名稱）只能掛一個
	 */
	/// <summary>
	/// 掛載持續播放的particle
	/// </summary>
	/// <param name="trans">欲掛載的Transform</param>
	public static void MountParticle(Transform trans, string particleName , Vector3? worldDirection)
	{
		if (string.IsNullOrEmpty(particleName))
			return;
		List<ParticleSystem> tempList;
		if (mountedParticle.TryGetValue(trans, out tempList))
		{
			foreach (ParticleSystem ps in tempList)
			{
				if (ps.name == particleName)
					return;
			}
		}
		else
		{
			tempList = new List<ParticleSystem>();
			mountedParticle.Add(trans, tempList);
		}
		ParticleSystem particle = GetParticle(particleName);
		if (particle != null)
		{
			/* Emission種類為Distance的Particle雖然已經sotp可是如果改變位置後再play的話，中間這段還是會產生particle
			 * 所以先把active設為false再改位置，然後再設為true再play就會好了
			 */
			particle.gameObject.SetActive(false);
			particle.transform.parent = trans;
			particle.transform.localPosition = Vector3.zero;
			if (worldDirection == null)
				particle.transform.localRotation = Quaternion.identity;
			else
				particle.transform.forward = worldDirection.Value;
			particle.gameObject.SetActive(true);
			particle.Play(true);

			tempList.Add(particle);
		}
	}
	/// <summary>
	/// 卸載指定particle
	/// </summary>
	/// <param name="trans">欲卸載的Transform</param>	
	public static void UnmountParticle(Transform trans, string particleName)
	{
		List<ParticleSystem> tempList;
		if (mountedParticle.TryGetValue(trans, out tempList))
		{
			ParticleSystem target = null;
			for (int i = 0; i < tempList.Count;i++ )
			{

				if (tempList[i].name == particleName)
				{
					target = tempList[i];
					tempList.RemoveAt(i);
					break;
				}
			}
			if (target != null)
			{
				target.transform.parent = null;
				//Recycle(target);//先不要回收，先停止然後等他播完後自動會回收
				target.Stop(true);
			}
		}
	}
	/// <summary>
	/// 卸載particle
	/// </summary>
	public static void UnmountAllParticle(Transform trans)
	{
		List<ParticleSystem> tempList;
		if (mountedParticle.TryGetValue(trans, out tempList))
		{
			for (int i = 0; i < tempList.Count; i++)
			{
				//Recycle(tempList[i]);
				//先不要回收，先停止然後等他播完後再回收
				if (tempList[i] != null)
				{
					tempList[i].Stop(true);
					tempList[i].transform.parent = null;
				}
			}
		}
		mountedParticle.Remove(trans);
		tempList.Clear();
	}

	/// <summary>
	/// 取得一個實體化的ParticleSystem
	/// 回傳的ParticleSystem會同時被加到監視行列，如果沒有任何顯示的粒子，會被回收
	/// 所以如果取得之後沒有play他，下個update就會被回收
	/// </summary>
	static ParticleSystem GetParticle(string name)
	{
		List<ParticleSystem> tempList;
		ParticleSystem tempps;

		if (!AvailableParticles.TryGetValue(name, out tempList) || tempList == null || tempList.Count ==0)
		{
			tempps = ResourceStation.GetParticle(name);
		}
		else
		{
			tempps = null;
			while (tempList.Count >0 && tempList[0] == null)
				tempList.RemoveAt(0);
			if (tempList.Count > 0)
			{
				tempps = tempList[0];
				tempList.RemoveAt(0);
			}else
				tempps = ResourceStation.GetParticle(name);
		}
		if(tempps != null)
		{
			if(Instance != null )
				Instance.KeepWatchParticle(tempps);
			tempps.Stop(true);
			tempps.gameObject.SetActive(true);
		}
		return tempps;
	}

	/// <summary>
	/// 回收一個 particle , 如果未超過上限 RESERVE_AMOUNT, 則把該 particle  隱藏, 下次使用
	/// </summary>
	public static void Recycle(ParticleSystem ps)
	{
		if (ps == null)
			return;
		ps.transform.parent = null;
		List<ParticleSystem> tempList;
		if (!AvailableParticles.TryGetValue(ps.name, out tempList))
		{
			tempList = new List<ParticleSystem>();
			AvailableParticles.Add(ps.name, tempList);
		}
		ps.Stop(true);
		ps.gameObject.SetActive(false);
		if (tempList.Count < RESERVE_AMOUNT)
			tempList.Add(ps);
		else
			DestroyImmediate(ps.gameObject);
	}
}

