using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SmoothMoves;
using Resources = UnityEngine.Resources;
using Object = UnityEngine.Object;
using System.Reflection;


/// <summary>
/// 中文
/// </summary>
public static class EditorTools {

    //重新命名設定組
    public enum RenameConfig
    {
        // 字串與其他設定重複, 希望優先被搜索到, 並更名的的請擺前面. EX : 右手手掌 > 右手
        [DoubleStringValue("右手手掌", "PalmR")]		    PALM_RIGHT,
        [DoubleStringValue("左手手掌", "PalmL")]			PALM_LEFT,
        [DoubleStringValue("右手掌", "PalmR")]		    PALM_RIGHT_2,
        [DoubleStringValue("左手掌", "PalmL")]			PALM_LEFT_2,
        [DoubleStringValue("左手", "HandL")]				HAND_LEFT ,
        [DoubleStringValue("右手", "HandR")]			    HAND_RIGHT ,
		[DoubleStringValue("頭髮", "Hair")]				HAIR ,
        [DoubleStringValue("頭", "Head")]				HEAD ,
		[DoubleStringValue("臉", "Head")]				FACE,
		[DoubleStringValue("眼睛", "Eyes")]				Eyes,
        [DoubleStringValue("左腳", "LegL")]				LEG_LEFT ,
        [DoubleStringValue("右腳", "LegR")]				LEG_RIGHT ,
        [DoubleStringValue("身體", "Body")]				BODY ,
        [DoubleStringValue("武器", "WEAPON")]			WEAPON,
		[DoubleStringValue("001-手", "HandR")]			Hand001,
		[DoubleStringValue("002-手", "HandR")]			Hand002,
		[DoubleStringValue("003-手", "HandL")]			Hand003,
		[DoubleStringValue("004-手", "HandL")]			Hand004,
		[DoubleStringValue("005-手", "HandL")]			Hand005,
		[DoubleStringValue("004-腳", "LegR")]			Leg004,
		[DoubleStringValue("005-腳", "LegL")]			Leg005,
    }

	public const string HEAD = "Head";
	public const string EYES = "Eyes";
	public const string HAIR = "Hair";
	public const string HEADDRESS = "HeadDress";
	public const string LEG_LEFT = "LegL";
	public const string LEG_RIGHT = "LegR";
	public const string HAND_LEFT = "HandL";
	public const string HAND_RIGHT = "HandR";
	public static readonly string[] HEAD_PART_NAMES = new string[] { HEAD, EYES, HAIR, HEADDRESS };
	public const string SETS_POSTFIX = "Set";
	public const string SETS_SEPERATER = "_";
	public const string DefaultAtlasDir = "Assets/ArtResources/Atlas";

	[MenuItem("Tools/ModelTexture/RenameModelTexture")]
	public static void RenameModelTexture()
	{
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);

        foreach (Texture t in objs)
        {
            if (t)
            {
                string newName = t.name;

                foreach (RenameConfig set in Enum.GetValues(typeof(RenameConfig)))
                {
                    DoubleStringValue twoString = CommonFunction.RetrieveEnum<DoubleStringValue>(set);
                    if (t.name.Contains(twoString.Kind))
                    {
                        newName = twoString.Content;
                        break;
                    }
                }

                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t), newName);
            }
        }
	}
	//將所選資料夾底下所有的圖建成一個atlas，名稱為該資料夾名稱
	//Selection.GetFiltered沒辦法只取一層，所以不要建成有多層資料夾的以免發生問題
	[MenuItem("Tools/ModelTexture/BuildModelAtlas")]
	public static void BuildModelAtlas()
	{
		
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
		Dictionary<string, List<Texture2D>> GroupTexture = new Dictionary<string, List<Texture2D>>();
		if (objs.Length != 0)
		{
			
			foreach (Texture2D t2di in objs)
			{
				if (t2di == null)
					continue;
				//用資料夾名稱來當atlas的名稱，用atlas的名稱分組
				string atlasName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(AssetDatabase.GetAssetPath(t2di)));
				if (GroupTexture.ContainsKey(atlasName))
					GroupTexture[atlasName].Add(t2di);
				else
				{
					List<Texture2D> tempList = new List<Texture2D>();
					tempList.Add(t2di);
					GroupTexture.Add(atlasName, tempList);
				}
			}

			foreach (KeyValuePair<string, List<Texture2D>> kvp in GroupTexture)
			{
				if (kvp.Value.Count > 0)
				{
					BuildAtlas(kvp.Value.ToArray(), kvp.Key, 0, 2048);
				}
			}
		}
	}

	public static void BuildAtlas(Texture2D[] textures, string atlasName, int padding, int maxSize)
	{
		//定位點，下面是一般玩家角色套用的，桃太郎那種是不一樣的值
		Dictionary<string, Vector2> defaultPivotOffsets = new Dictionary<string, Vector2>();
		defaultPivotOffsets.Add("HeadDress", new Vector2(-0.06964286f, -0.2276786f));
		defaultPivotOffsets.Add("Hair", new Vector2(-0.04821428f, -0.2662743f));
		defaultPivotOffsets.Add("Eyes", new Vector2(-0.07386363f, -0.125f));
		defaultPivotOffsets.Add("HandR", new Vector2(-0.06964286f, -0.2276786f));
		defaultPivotOffsets.Add("Head", new Vector2(-0.03214286f, -0.2366883f));
		defaultPivotOffsets.Add("LegR", new Vector2(-0.075f, -0.3122971f));
		defaultPivotOffsets.Add("HandL", new Vector2(0f, -0.2465503f));
		defaultPivotOffsets.Add("Body", new Vector2(0.04821428f, -0.06903409f));
		defaultPivotOffsets.Add("LegL", new Vector2(-0.02142857f, -0.3188717f));

		if (AssetDatabase.LoadAssetAtPath(DefaultAtlasDir, typeof(Object)) == null)
			AssetDatabase.CreateFolder("Assets/ArtResources", "Atlas");

		foreach (Texture2D t2di in textures)
		{
			if (t2di == null)
				continue;
			string iPath = AssetDatabase.GetAssetPath(t2di);
			TextureImporter ti = AssetImporter.GetAtPath(iPath) as TextureImporter;
			if (ti)
			{
				ti.isReadable = true;
				AssetDatabase.ImportAsset(iPath);
			}
		}
		if (textures.Length > 0)
		{
			TextureAtlas atlas = ScriptableObject.CreateInstance<TextureAtlas>();
			atlas.padding = padding;
			atlas.maxAtlasSize = maxSize;
			atlas.material = new Material(Shader.Find("Particles/Alpha Blended"));

			atlas.textureNames = new List<string>();
			atlas.texturePaths = new List<string>();
			atlas.textureGUIDs = new List<string>();
			atlas.textureSizes = new List<Vector2>();

			atlas.defaultPivotOffsets = new List<Vector2>(new Vector2[textures.Length]);
			for (int i = 0; i < textures.Length; i++)
			{
				atlas.textureNames.Add(textures[i].name);
				atlas.texturePaths.Add(AssetDatabase.GetAssetPath(textures[i]));
				atlas.textureGUIDs.Add(AssetDatabase.AssetPathToGUID(atlas.texturePaths[i]));
				atlas.textureSizes.Add(new Vector2(textures[i].width, textures[i].height));
				foreach (KeyValuePair<string, Vector2> kvp in defaultPivotOffsets)
				{

					if (textures[i].name.StartsWith(kvp.Key, true, null))
					{
						atlas.defaultPivotOffsets[i] = kvp.Value;
						continue;
					}
				}
			}

			Texture2D newTex = new Texture2D(atlas.maxAtlasSize, atlas.maxAtlasSize);
			atlas.uvs = new List<Rect>(newTex.PackTextures(textures, atlas.padding, atlas.maxAtlasSize));

			AssetDatabase.CreateAsset(atlas, DefaultAtlasDir + "/" + atlasName + ".Asset");
			AssetDatabase.CreateAsset(atlas.material, DefaultAtlasDir + "/" + atlasName + ".Mat");

			string nPath = DefaultAtlasDir + "/" + atlasName + ".png";
			//Debug.Log(nPath);
			FileStream fs = new FileStream(Application.dataPath.Replace("Assets", nPath), FileMode.Create);
			byte[] tb = newTex.EncodeToPNG();
			fs.Write(tb, 0, tb.Length);
			fs.Close();
			AssetDatabase.ImportAsset(nPath);

			newTex = AssetDatabase.LoadAssetAtPath(nPath, typeof(Texture2D)) as Texture2D;
			atlas.material.mainTexture = newTex;
		}
	}

	//頭、頭飾、眼睛（五官）部位，這幾個部份比較獨立，沒有一整組的圖，大部份的時候都是單張，所以atlas建立的時候另外各建一個群組
	//檔名用固定名稱加編號(部位_X)，每n個編號包成一組
	//每個部位要先用資料夾分類好，不然會錯
	[MenuItem("Tools/ModelTexture/RenameHeadPartTexture")]
	public static void RenameHeadPartTexture()
	{
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);

		foreach (Texture t in objs)
		{
			if (t)
			{
				string path =AssetDatabase.GetAssetPath(t);
				string dirName = Path.GetDirectoryName(path);
				string partName = "";
				foreach (string defaultPartName in HEAD_PART_NAMES)
				{
					if (dirName.EndsWith(defaultPartName, true, null))
					{
						Debug.Log(dirName);
						Debug.Log(defaultPartName);
						partName = defaultPartName;
					}
				}
				if (string.IsNullOrEmpty(partName))
					continue;
				if (!t.name.StartsWith(partName, true, null))
				{
					string newpath = AssetDatabase.GenerateUniqueAssetPath(Path.GetDirectoryName(path) + "/" + partName + SETS_SEPERATER + "1" + Path.GetExtension(path));
					AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(t), newpath);
				}
			}
		}
	}

	//特殊用途，把多層資料夾更名減少一層
	[MenuItem("Tools/ModelTexture/RenameFolder")]
	public static void RenameFolder()
	{
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

		//先取得選取的所有資料夾
		List<string> folderNames = new List<string>();
		foreach (Object obji in objs)
		{
			if (obji.GetType().ToString() == "UnityEngine.Object")
			{
				folderNames.Add(AssetDatabase.GetAssetPath(obji));
			}
		}
		
		//再看現有的資料夾中有沒有父資料夾是包含在選取的資料夾中
		foreach (Object obji in objs)
		{
			if (obji.GetType().ToString() == "UnityEngine.Object")
			{
				Debug.Log(obji);
				string fName = Path.GetDirectoryName(AssetDatabase.GetAssetPath(obji));
				if (folderNames.Contains(fName))
				{
					string newPath = AssetDatabase.GenerateUniqueAssetPath(fName + "_1");
					Debug.Log(AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(obji), newPath));
				}
			}
		}
	}
	[MenuItem("Tools/ModelTexture/CombineSelectTexture")]
	public static void CombineSelectTexture()
	{
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

		List<Texture2D> textures = new List<Texture2D>();
		foreach (Texture2D t in objs)
		{
			if (t)
			{
				textures.Add(t);
			}
		}
		if (textures.Count > 1)
		{
			Texture2D t2d = new Texture2D(textures[0].width, textures[0].height);
			Color[] cols = new Color[t2d.width * t2d.height];
			foreach (Texture2D t2di in textures)
			{
				if (t2d.width != t2di.width || t2d.height != t2di.height)
					continue;
				string iPath = AssetDatabase.GetAssetPath(t2di);
				TextureImporter ti = AssetImporter.GetAtPath(iPath) as TextureImporter;
				if (ti)
				{
					ti.isReadable = true;
					ti.textureFormat = TextureImporterFormat.ARGB32;
					AssetDatabase.ImportAsset(iPath);
				}
				Color[]  tcols = t2di.GetPixels();
				for (int i = 0; i < tcols.Length; i++)
				{
					cols[i] = Color.Lerp(cols[i], tcols[i], tcols[i].a);
				}
			}
			t2d.SetPixels(cols);
			t2d.Apply();
			string nPath = AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(textures[0]));
			nPath = Path.ChangeExtension(nPath, ".png");
			nPath = Application.dataPath.Replace("Assets", nPath);
			Debug.Log(nPath);
			FileStream fs = new FileStream(nPath, FileMode.Create);
			byte[] tb = t2d.EncodeToPNG();
			fs.Write(tb, 0, tb.Length);
			fs.Close();
			AssetDatabase.Refresh();
		}

		
	}

	[MenuItem("Tools/ModelTexture/SwapLeftRight")]
	public static void SwapLR()
	{
		UnityEngine.Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
		List<Texture2D> LTexture = new List<Texture2D>();
		List<Texture2D> RTexture = new List<Texture2D>();
		foreach (Texture2D t in objs)
		{
			if (t)
			{
				switch (t.name)
				{
					case HAND_LEFT:
					case LEG_LEFT:
						LTexture.Add(t);
						break;
					case HAND_RIGHT:
					case LEG_RIGHT:
						RTexture.Add(t);
						break;
				}
			}
		}

		foreach (Texture2D t in LTexture)
		{
			AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t), t.name+"RR");
		}
		foreach (Texture2D t in RTexture)
		{
			string newName;
			switch (t.name)
			{
				case HAND_RIGHT:
					newName = HAND_LEFT;
					break;
				case LEG_RIGHT:
					newName = LEG_LEFT;
					break;
				default:
					continue;
			}
			AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t), newName);
		}
		foreach (Texture2D t in LTexture)
		{
			if (t.name.Contains(HAND_LEFT))
				AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t), HAND_RIGHT);
			else if (t.name.Contains(LEG_LEFT))
				AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t), LEG_RIGHT);
		}

	}
    [MenuItem("Tools/RemoveWhite")]
    public static void RemoveWhite()
    {
        if (Selection.activeObject is Texture2D)
        {
            Texture2D ot2d = Selection.activeObject as Texture2D;
			Texture2D t2d = new Texture2D(ot2d.width, ot2d.height);
			TextureImporter ti = TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(ot2d)) as TextureImporter;
            if (ti)
            {
                ti.isReadable = true;
				ti.textureFormat = TextureImporterFormat.ARGB32;
				AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ot2d));
            }
			Color[] colors = ot2d.GetPixels();

			for (int i = 0; i < colors.Length; i++)
			{
				if (Mathf.Approximately(colors[i].grayscale, 1f))
				{
					colors[i].a = 0;
				}
				else
					colors[i].a = 1 - colors[i].grayscale;
			}
            t2d.SetPixels(colors);
			t2d.Apply();
			string nPath = AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(ot2d));
			nPath = Path.ChangeExtension(nPath, ".png");
			nPath = Application.dataPath.Replace("Assets", nPath);
			Debug.Log(nPath);
			FileStream fs = new FileStream(nPath, FileMode.Create);
			byte[] tb = t2d.EncodeToPNG();
			fs.Write(tb, 0, tb.Length);
			fs.Close();
			AssetDatabase.Refresh();
        }
		
    }

	[MenuItem("Tools/HalfAlpha")]
	public static void HalfAlpha()
	{
		if (Selection.activeObject is Texture2D)
		{
			Texture2D ot2d = Selection.activeObject as Texture2D;
			Texture2D t2d = new Texture2D(ot2d.width, ot2d.height);
			TextureImporter ti = TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(ot2d)) as TextureImporter;
			if (ti)
			{
				ti.isReadable = true;
				ti.textureFormat = TextureImporterFormat.ARGB32;
				AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ot2d));
			}
			Color[] colors = ot2d.GetPixels();

			for (int i = 0; i < colors.Length; i++)
			{
				colors[i].a *= 0.5f;
			}
			t2d.SetPixels(colors);
			t2d.Apply();
			string nPath = AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(ot2d));
			nPath = Path.ChangeExtension(nPath, ".png");
			nPath = Application.dataPath.Replace("Assets", nPath);
			Debug.Log(nPath);
			FileStream fs = new FileStream(nPath, FileMode.Create);
			byte[] tb = t2d.EncodeToPNG();
			fs.Write(tb, 0, tb.Length);
			fs.Close();
			AssetDatabase.Refresh();
		}
	}

	public static Vector3 TranslateCamera(Camera current, Vector3 currentPos, Camera targetCam)
	{
		if (current == null || targetCam == null)
			return currentPos;
		Debug.Log(currentPos);
		Debug.Log(current.worldToCameraMatrix);
		Debug.Log(current.worldToCameraMatrix.MultiplyPoint(currentPos));
		Debug.Log(current.projectionMatrix);
		Debug.Log(current.projectionMatrix.MultiplyPoint(currentPos));
		Debug.Log(current.WorldToScreenPoint(currentPos));
		Debug.Log(targetCam.worldToCameraMatrix);
		Debug.Log(targetCam.worldToCameraMatrix.MultiplyPoint(currentPos));
		Debug.Log(targetCam.projectionMatrix);
		Debug.Log(targetCam.projectionMatrix.MultiplyPoint(currentPos));
		Debug.Log(targetCam.WorldToScreenPoint(currentPos));
		Vector3 result = targetCam.ScreenToWorldPoint(current.WorldToScreenPoint(currentPos));
		Debug.Log(result);
		return result;
	}

	[MenuItem("Tools/ChangeColor")]
	public static void ChangeColor()
	{
		foreach (Texture2D t2d in Selection.objects)
		{

			if (t2d)
			{
				string iPath = AssetDatabase.GetAssetPath(t2d);
				//string opath = Application.dataPath.Replace("Assets", Path.GetDirectoryName(iPath) + "/" + Path.GetFileNameWithoutExtension(iPath) + ".png");

				TextureImporter ti = AssetImporter.GetAtPath(iPath) as TextureImporter;
				if (ti)
				{
					ti.isReadable = true;
					AssetDatabase.ImportAsset(iPath);
				}
				Color[] c = t2d.GetPixels();
				for (int i = 0; i < c.Length; i++)
				{
					if (c[i].a > 0)
					{
						//if (c[i].g > c[i].b)
						//{
							float t = c[i].b;
							c[i].b = c[i].g;
							c[i].g = t;
						//}
						//c[i].r = c[i].grayscale;
						//c[i].g = c[i].b = c[i].r;
					}
				}
				t2d.SetPixels(c);
				t2d.Apply();

				FileStream fs = new FileStream(iPath, FileMode.Create);
				byte[] tb = t2d.EncodeToPNG();
				fs.Write(tb, 0, tb.Length);
				fs.Close();
			}
		}
		AssetDatabase.Refresh();
	}

	//因為particle system不會讀scale，據說可以改shader來處理，不過還沒研究，目前先放大參數的來做比較快
	//注意直事項：emission有兩種模式，當模式是distance的時候，放大之後要把這個值縮小同樣的比例，不過我找不到模式的參數，所以只好手動改了
	[MenuItem("Tools/Enlarge Particle 2")]
	public static void Enlarge_Particle_2()
	{
		if (Selection.activeGameObject)
		{
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(true))
			{
				ps.startSize *= 2;
				ps.startSpeed *= 2;
			}
		}
	}
	[MenuItem("Tools/Enlarge Particle 100")]
	public static void Enlarge_Particle_100()
	{
		if (Selection.activeGameObject)
		{
			Debug.Log(Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(true).Length);
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(true))
			{
				ps.startSize *= 100;
				ps.startSpeed *= 100;
			}
		}
	}
	[MenuItem("Tools/Enlarge Particle 200")]
	public static void Enlarge_Particle_200()
	{
		if (Selection.activeGameObject)
		{
			Debug.Log(Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(true).Length);
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(true))
			{
				ps.startSize *= 200;
				ps.startSpeed *= 200;
			}
		}
	}
	[MenuItem("Tools/Narrow Particle 2")]
	public static void Narrow_Particle_2()
	{
		if (Selection.activeGameObject)
		{
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>())
			{
				ps.startSize /= 2;
				ps.startSpeed /= 2;
			}
		}
	}
	[MenuItem("Tools/Narrow Particle 100")]
	public static void Narrow_Particle_100()
	{
		if (Selection.activeGameObject)
		{
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>())
			{
				ps.startSize /= 100;
				ps.startSpeed /= 100;
			}
		}
	}
	[MenuItem("Tools/Narrow Particle 200")]
	public static void Narrow_Particle_200()
	{
		if (Selection.activeGameObject)
		{
			foreach (ParticleSystem ps in Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>())
			{
				ps.startSize /= 200;
				ps.startSpeed /= 200;
			}
		}
	}

	[MenuItem("Tools/Remove Particle AutoDestroyComponent")]
	public static void Remove_AutoDestroy()
	{
		foreach(GameObject go in Selection.gameObjects)
		{
			foreach (CFX_AutoDestructShuriken adi in go.GetComponentsInChildren<CFX_AutoDestructShuriken>(true))
			{
				GameObject.DestroyImmediate(adi, true);
			}

			foreach (CFX_ShurikenThreadFix adi in go.GetComponentsInChildren<CFX_ShurikenThreadFix>(true))
			{
				GameObject.DestroyImmediate(adi, true);
			}
		}
	}
	#region XML
	[MenuItem("Tools/Rebuild Class Xml For Excel")]
	public static void RebuildXml()
	{

		System.Type T = System.Type.GetType("SkillData, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
		
		object[] objs = System.Array.CreateInstance(T, 2) as object[];
		objs.SetValue(System.Activator.CreateInstance(T), 0);
		objs.SetValue(System.Activator.CreateInstance(T), 1);
		XmlSerializer xml = new XmlSerializer(objs.GetType());

		StringWriter sw = new StringWriter();
		xml.Serialize(sw, objs);
		Debug.Log(sw);

		FileStream fs = new FileStream(Application.dataPath + "/Test/Resources/Data/NewBasicSkillData.xml", FileMode.Create);
		xml.Serialize(fs, objs);
		fs.Close();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="OT">OldType</typeparam>
	/// <typeparam name="NT">NewType</typeparam>
	public static object TranslateType(object obj,System.Type newType)
	{
		System.Type oldType = obj.GetType();
		
		
		object newobj = System.Activator.CreateInstance(newType);
		foreach (FieldInfo fi in oldType.GetFields())
		{
			FieldInfo newfi = newType.GetField(fi.Name);
			if(newfi != null)
				newfi.SetValue(newobj, fi.GetValue(obj));
		}
		return newobj;
	}
	#endregion

	[MenuItem("Tools/UnloadUnuseAsset")]
	public static void UnloadUnuseAsset()
	{
		Resources.UnloadUnusedAssets();
	}
	[MenuItem("Tools/BoneNodeTexture")]
	public static void BoneNodeTexture()
	{
		Texture2D circle = AssetDatabase.LoadAssetAtPath("Assets/Circle.Asset", typeof(Texture2D)) as Texture2D;
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if (!AssetDatabase.LoadAssetAtPath("Assets/TempBoneTexture", typeof(Object)))
			AssetDatabase.CreateFolder("Assets", "TempBoneTexture");

		if (ta && circle)
		{
			for (int index = 0; index < ta.textureGUIDs.Count; index++)
			{
				Texture2D ot2d = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(ta.textureGUIDs[index]), typeof(Texture2D)) as Texture2D;
				if (ot2d)
				{
					GameObject newGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
					newGO.name = ot2d.name;
					newGO.renderer.sharedMaterial = new Material(Shader.Find("Particles/Alpha Blended"));

					Texture2D t2d = new Texture2D(ot2d.width, ot2d.height);
					Vector2 offset = ta.defaultPivotOffsets[index];
					offset.x += 0.5f;
					offset.y += 0.5f;

					offset.x *= ta.textureSizes[index].x;
					offset.y *= ta.textureSizes[index].y;

					int circleSize = 16;
					t2d.SetPixels(new Color[t2d.width * t2d.height]);
					for (int i = 0; i < circleSize; i++)
					{
						for (int j = 0; j < circleSize; j++)
						{
							t2d.SetPixel((int)(offset.x - circleSize / 2 + i), (int)(offset.y - circleSize / 2 + j), circle.GetPixelBilinear((float)i / circleSize, (float)j / circleSize));
						}
					}
					t2d.Apply();
					newGO.renderer.sharedMaterial.mainTexture = t2d;


					AssetDatabase.CreateAsset(t2d, "Assets/TempBoneTexture/" + ot2d.name + ".Asset");
				}
			}
		}
	}

	[MenuItem("Tools/QuickTest")]
	public static void QuickTest()
	{
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		Dictionary<string, Vector2> defaultPivotOffsets = new Dictionary<string, Vector2>();
		if (ta)
		{

			string copyCode = @"Dictionary<string,Vector2> defaultPivotOffsets = new Dictionary<string,Vector2>();";
			for (int i = 0; i < ta.textureNames.Count; i++)
			{
				copyCode += "\n";
				copyCode += string.Format("defaultPivotOffsets.Add(\"{0}\",new Vector2({1}f,{2}f));", ta.textureNames[i], ta.defaultPivotOffsets[i].x, ta.defaultPivotOffsets[i].y);
			}
			TextEditor te = new TextEditor();
			te.content = new GUIContent(copyCode);
			te.SelectAll();
			te.Copy();
		}
	}

	[MenuItem("Tools/QuickTest1")]
	public static void QuickTest1()
	{
		Color[] png = null;
		Texture2D temp = null;
		foreach (Texture2D t2d in Selection.objects)
		{
			if (t2d)
			{
				if (png == null)
				{
					png = t2d.GetPixels();
					temp = new Texture2D(t2d.width, t2d.height);
					continue;
				}
				Color[] t2dColor = t2d.GetPixels();
				if (t2dColor.Length != png.Length)
					continue;
				for (int i = 0; i < png.Length; i++)
				{
					png[i] = Color.Lerp(png[i], t2dColor[i], t2dColor[i].a);
				}
			}
		}
		if (!AssetDatabase.LoadAssetAtPath("Assets/TempBoneTexture", typeof(Object)))
			AssetDatabase.CreateFolder("Assets", "TempBoneTexture");
		if (png != null)
		{
			string path = Application.dataPath + "/TempBoneTexture/BoneCombine.png";
			temp.SetPixels(png);
			temp.Apply();

			FileStream fs = File.Open(path, FileMode.Create);
			byte[] pngs = temp.EncodeToPNG();
			fs.Write(pngs, 0, pngs.Length);
			fs.Close();

			AssetDatabase.Refresh();
		}
	}
	[MenuItem("Tools/QuickTest2")]
	public static void QuickTest2()
	{
		string path = EditorUtility.SaveFilePanel ("Save Resource", Application.dataPath + @"\Test\Resources\Assetbundles\", "New Resource", "unity3d");
		if(string.IsNullOrEmpty(path))
			return;
		BuildPipeline.BuildAssetBundle(Selection.activeObject, null, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
		
	}

	[MenuItem("Tools/CopyLocalPosition")]
	public static void CopyLocalPosition()
	{
		if(Selection.activeGameObject)
		{
			TextEditor te = new TextEditor();
			Vector3 v3 = Selection.activeGameObject.transform.localPosition;
			te.content = new GUIContent(string.Format("({0:0.00}f,{1:0.00}f,{2:0.00}f)", v3.x, v3.y, v3.z));
			te.SelectAll();
			te.Copy();
		}
	}
}
