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
    private ResourceUpdater _updater;

    //constructor
    public ResourceStation()
    {
        _updater = new ResourceUpdater();
    }

    // destructor
    ~ResourceStation()
    {
        _updater = null;
    }

    /// <summary>
    /// 是否需要下載更新資源
    /// </summary>
    public bool IsNeedToUpdate
    {
        get
        {
            return false;
        }
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
		if (string.IsNullOrEmpty(prefabName))
			return null;
		if (!Atlases.TryGetValue(prefabName, out atlas))
		{
			Atlas_LoadFromeResource(prefabName);
			Atlases.TryGetValue(prefabName, out atlas);
		}

		return atlas;
	}
	static void Atlas_LoadFromeResource(string atlasName)
	{
		TextureAtlas prefab = Resources.Load("Atlas/" + atlasName, typeof(TextureAtlas)) as TextureAtlas;
		if (prefab == null)
			return;
		Atlases.Add(atlasName, prefab);
	}

	#endregion

	#region BoneAnimation
	static Dictionary<string, BoneAnimation> boneAnimations = new Dictionary<string, BoneAnimation>();
	public static BoneAnimation GetBone(string prefabName)
	{
		BoneAnimation bone;

		if (!boneAnimations.TryGetValue(prefabName, out bone))
		{
			Bone_LoadFromResource(prefabName);
			if(!boneAnimations.TryGetValue(prefabName, out bone))
				return null;
		}
		BoneAnimation result = GameObject.Instantiate(bone) as BoneAnimation;
		result.name = prefabName;
		return result;
	}
	static void Bone_LoadFromResource(string prefabName)
	{
		GameObject prefab = Resources.Load("BasicBone/" + prefabName) as GameObject;
		if (prefab == null)
			return;

		//ParticleSystem ps = (GameObject.Instantiate(prefab) as GameObject).GetComponent<ParticleSystem>();
		BoneAnimation bone = prefab.GetComponent<BoneAnimation>(); //存原始物件，不實體化
		if (bone == null)
			return;
		bone.name = prefabName; //名字統一，方便找到同一個particle
		//ps.Stop();
		boneAnimations.Add(prefabName, bone);
	}
	#endregion
	public static void GenerateModelSprite(SmoothMoves.BoneAnimation boneData,string[] atlasInfo)
	{
		if (boneData == null || atlasInfo == null)
			return;
		for (int i = 0; i < atlasInfo.Length && i < GLOBALCONST.BONE_NAME.Length; i++)
		{
			string boneName = GLOBALCONST.BONE_NAME[i];
			string atlasName = atlasInfo[i];

			Transform spriteTrans = boneData.GetSpriteTransform(boneName);
			if (spriteTrans == null)
				continue;
			TextureAtlas atlas = GetAtlas(atlasName);
			Sprite sprite = spriteTrans.GetComponent<SmoothMoves.Sprite>();
			if (sprite == null)
				sprite = spriteTrans.gameObject.AddComponent<Sprite>();
			sprite.SetAtlas(atlas);
			sprite.SetTextureName(boneName);
			sprite.SetPivotOffset(Vector2.zero, true);
		}
    }

    #region NGUI
    static Dictionary<string, UIAtlas> _uiAtlases = new Dictionary<string, UIAtlas>();
    public static UIAtlas GetUIAtlas(string uiAtlasName)
    {
        if (string.IsNullOrEmpty(uiAtlasName)) { return null; }
        UIAtlas retUIAtlas;
        if (!_uiAtlases.TryGetValue(uiAtlasName, out retUIAtlas))
        {
            UIAtlas_LoadFromResource(uiAtlasName);
            _uiAtlases.TryGetValue(uiAtlasName, out retUIAtlas);
        }
        return retUIAtlas;
    }
    static void UIAtlas_LoadFromResource(string uiAtlasName)
    {
        UIAtlas uiAtlas = Resources.Load(GLOBALCONST.DIR_RESOURCES_NGUI_ATLAS + uiAtlasName, typeof(UIAtlas)) as UIAtlas;
        if (uiAtlas == null) { return; }
        _uiAtlases.Add(uiAtlasName, uiAtlas);
    }

    static Dictionary<string, UIFont> _uiFonts = new Dictionary<string, UIFont>();
    /// <summary>
    /// 取得Resource中的UI字型，一般情形下請使用GUIFontManager.GetUIDynamicFont()取得字型
    /// </summary>
    /// <param name="uiFontName">字型名稱</param>
    /// <returns>對應的UIFont</returns>
    public static UIFont GetUIFont(string uiFontName)
    {
        if (string.IsNullOrEmpty(uiFontName)) { return null; }
        UIFont retFont;
        if (!_uiFonts.TryGetValue(uiFontName, out retFont))
        {
            UIFont_LoadFromResource(uiFontName);
            _uiFonts.TryGetValue(uiFontName, out retFont);
        }
        return retFont;
    }

    static void UIFont_LoadFromResource(string uiFontName)
    {
        UIFont uiFont = Resources.Load(GLOBALCONST.DIR_RESOURCES_NGUI_FONT + uiFontName, typeof(UIFont)) as UIFont;
        if (uiFont == null) { return; }
        _uiFonts.Add(uiFontName, uiFont);
    }

    #endregion
}
