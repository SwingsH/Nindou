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
    static Dictionary<string, BoneAnimation> boneAnimations = new Dictionary<string, BoneAnimation>();
    static Dictionary<string, ParticleSystem> particles = new Dictionary<string, ParticleSystem>();
    static Dictionary<string, TextureAtlas> Atlases = new Dictionary<string, TextureAtlas>();

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
		GameObject prefab = Resources.Load(GLOBALCONST.DIR_RESOURCES_PARTICLE + particleName) as GameObject;
		if (prefab == null || !prefab.particleSystem)
		{
			CommonFunction.DebugError( " Particle_Load : " + particleName + "  Failed.");
			return;
		}
		//ParticleSystem ps = (GameObject.Instantiate(prefab) as GameObject).GetComponent<ParticleSystem>();
		ParticleSystem ps = prefab.GetComponent<ParticleSystem>(); //存原始物件，不實體化
		ps.name = particleName; //名字統一，方便找到同一個particle
		//ps.Stop();
		particles.Add(particleName, ps);
	}
	#endregion

	#region Atlas
	
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
		TextureAtlas prefab = Resources.Load(GLOBALCONST.DIR_RESOURCES_ATLAS + atlasName, typeof(TextureAtlas)) as TextureAtlas;
		if (prefab == null)
			return;
		Atlases.Add(atlasName, prefab);
	}

	#endregion

	#region BoneAnimation
    /// <summary>
    /// 取得 smoothMoves 的 BoneAnimation
    /// </summary>
    /// <param name="parent"> 讓 BoneAnimation 可以知道該 attach 到哪個 gameObject </param>
	public static BoneAnimation GetBone(GameObject parent, string prefabName)
	{
		BoneAnimation bone;

		if (!boneAnimations.TryGetValue(prefabName, out bone))
		{
			Bone_LoadFromResource(prefabName);
			if(!boneAnimations.TryGetValue(prefabName, out bone))
				return null;
		}
		BoneAnimation result = GameObject.Instantiate(bone) as BoneAnimation;
		result.name = prefabName ;
        result.transform.parent = parent.transform;
        result.transform.localPosition = Vector3.zero; // 強制歸 0, 防止 prefab 存檔時沒有歸 0

		return result;
	}

	static void Bone_LoadFromResource(string prefabName)
	{
		GameObject prefab = Resources.Load(GLOBALCONST.DIR_RESOURCES_AVATAR_BONE + prefabName) as GameObject;

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
    /// <summary>
    /// 從Resource取得指定的UIAtlas，除了UIImageManager Or Editor功能需要之外請別呼叫此函式，基本上也不應有需要取UIAtlas來直接修改的時候
    /// 若有取圖需求，請在設定好Enum SpriteName的資料後，呼叫UIImageManager.CreateUISprite()
    /// </summary>
    /// <param name="uiAtlasName"></param>
    public static UIAtlas LoadUIAtlasFromResource(string uiAtlasName)
    {
        return Resources.Load(GLOBALCONST.DIR_RESOURCES_NGUI_ATLAS + uiAtlasName, typeof(UIAtlas)) as UIAtlas;
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
