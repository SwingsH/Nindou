using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmoothMoves;
using Resources = UnityEngine.Resources;
/// <summary>
/// 資料存取統一介面, 
/// 統一使用此介面, 索取者可以不用得知資源來源 : Resources/ , unity caching, file system
/// </summary>
public class ResourceStation {

    //constructor
    public ResourceStation()
    {
    }

    // destructor
    ~ResourceStation()
    {
    }


	#region ParticleSystem
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
		ParticleSystem result = GameObject.Instantiate(ps) as ParticleSystem;
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
	#endregion

	#region Atlas
	static Dictionary<string, TextureAtlas> Atlases = new Dictionary<string, TextureAtlas>();
	public static TextureAtlas GetAtlas(string prefabName)
	{
		TextureAtlas atlas;

		if (!Atlases.TryGetValue(prefabName, out atlas))
		{
			Atlas_Load(prefabName);
			Atlases.TryGetValue(prefabName, out atlas);
		}

		return atlas;
	}
	static void Atlas_Load(string atlasName)
	{
		TextureAtlas prefab = Resources.Load("Atlas/" + atlasName, typeof(TextureAtlas)) as TextureAtlas;
		if (prefab == null)
			return;
		Atlases.Add(atlasName, prefab);
	}

	#endregion

	public static void GenerateModel(SmoothMoves.BoneAnimation boneData, Dictionary<string,string> atlasInfo)
	{
		if (boneData == null || atlasInfo == null)
			return;
		foreach (KeyValuePair<string, string> kvp in atlasInfo)
		{
			Transform spriteTrans = boneData.GetSpriteTransform(kvp.Key);
			if (spriteTrans == null)
				continue;
			TextureAtlas atlas = GetAtlas(kvp.Value);
			Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();
			if(sprite == null)
				sprite = spriteTrans.gameObject.AddComponent<Sprite>();
			sprite.SetAtlas(atlas);
			sprite.SetTextureName(kvp.Key);
			sprite.SetPivotOffset(Vector2.zero, true);
		}

	}
}
