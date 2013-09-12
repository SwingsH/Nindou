using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * 新版的ParticleSystem沒有自我刪除的功能，加上手持不適合直實體化物件，所以在這裡做觀察回收的動作
 * 為了要重複使用同一個ParticleSystem,故不要修改參數
 */
public class ParticleManager : MonoBehaviour {

	public const int RESERVE_AMOUNT = 10;
	public static ParticleManager Instance;

	public static Dictionary<string, List<ParticleSystem>> AvailableParticles = new Dictionary<string, List<ParticleSystem>>();
	List<ParticleSystem> actingParticleSystem = new List<ParticleSystem>();
	public ParticleSystem[] displayingArray; //目前顯示中的
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
			if (actingParticleSystem[i].IsAlive())
				i++;
			else
			{
				Recycle(actingParticleSystem[i]);
				actingParticleSystem.RemoveAt(i);
			}
		}
		displayingArray = actingParticleSystem.ToArray();
	}
	public static ParticleSystem GetParticle(string name)
	{
		List<ParticleSystem> tempList;
		ParticleSystem tempps;
		if (!AvailableParticles.TryGetValue(name, out tempList) || tempList == null || tempList.Count ==0)
		{
			tempps  = ResourceCenter.GetParticle(name);
		}
		else
		{
			tempps = tempList[0];
			tempList.RemoveAt(0);
		}
		if(tempps != null)
		{
			if(Instance != null )
				Instance.actingParticleSystem.Add(tempps);
			tempps.gameObject.SetActive(true);
			tempps.Stop();
		}
		return tempps;
	}
	public static void Recycle(ParticleSystem ps)
	{
		if (ps == null)
			return;
		List<ParticleSystem> tempList;
		if (!AvailableParticles.TryGetValue(ps.name, out tempList))
		{
			tempList = new List<ParticleSystem>();
			AvailableParticles.Add(ps.name, tempList);
		}
		ps.gameObject.SetActive(false);
		if (tempList.Count < RESERVE_AMOUNT)
			tempList.Add(ps);
		else
			DestroyImmediate(ps.gameObject);

		
	}
}

