using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class ResourceCenter
{

	static Dictionary<string, ParticleSystem> particles = new Dictionary<string, ParticleSystem>();
	public static ParticleSystem GetParticle(string prefabName)
	{
		ParticleSystem ps;

		if (!particles.TryGetValue(prefabName, out ps))
		{
			Particle_Load(prefabName);
			if (!particles.TryGetValue(prefabName, out ps))
				return null;
		}
		ParticleSystem result =  GameObject.Instantiate(ps) as ParticleSystem;
		result.name = prefabName;
		return result;
	}

	static void Particle_Load(string particleName)
	{
		GameObject prefab = Resources.Load("Particle/" + particleName) as GameObject;
		if (prefab == null || !prefab.particleSystem)
			return;

		//ParticleSystem ps = (GameObject.Instantiate(prefab) as GameObject).GetComponent<ParticleSystem>();
		ParticleSystem ps = prefab.GetComponent<ParticleSystem>(); //存原始物件，不實體化
		ps.name = particleName; //名字統一，方便找到同一個particle
		//ps.Stop();
		particles.Add(particleName, ps);
	}
}
