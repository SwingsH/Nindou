using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SmoothMoves;
/// <summary>
/// 中文
/// </summary>
public static class Tools {

	static object buffer;

	const string HAND_LEFT = "HandL";
	const string HAND_RIGHT = "HandR";
	const string HEAD = "Head";
	const string LEG_LEFT = "LegL";
	const string LEG_RIGHT = "LegR";
	const string BODY = "Body";
	const string WEAPON = "WEAPON";
	[MenuItem("Tools/RenameModelTexture")]
	public static void RenameModelTexture()
	{
		Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);

		foreach (Texture t in objs)
		{
			if (t)
			{
				string newName = t.name;
				if (t.name.Contains("右手"))
					newName = HAND_RIGHT;
				else if (t.name.Contains("左手"))
					newName = HAND_LEFT;
				else if (t.name.Contains("頭"))
					newName = HEAD;
				else if (t.name.Contains("左腳"))
					newName = LEG_LEFT;
				else if (t.name.Contains("右腳"))
					newName = LEG_RIGHT;
				else if (t.name.Contains("身體"))
					newName = BODY;
				else if (t.name.Contains("武器"))
					newName = WEAPON;
				AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(t),newName);
			}
		}
	}
    [MenuItem("Tools/RemoveWhite")]
    public static void RemoveWhite()
    {
        if (Selection.activeObject is Texture2D)
        {
            Texture2D t2d = Selection.activeObject as Texture2D;
            TextureImporter ti = TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(t2d)) as TextureImporter;
            if (ti)
            {
                ti.isReadable = true;
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(t2d));
            }
            Color[] colors = t2d.GetPixels();
            for (int i = 0; i < colors.Length; i++)
                if (colors[i].grayscale == 1)
                    colors[i].a = 0;
            t2d.SetPixels(colors);
        }
    }
	[MenuItem("Tools/RandomTest")]
	public static void RandomTest()
	{
		int count1 = 0;
		int count2 = 0;
		float Rate1 = 0.5f;
		float Rate2 = 0.5f;
		for (int i = 0; i < 10000; i++)
		{
			float r = Random.value;
			if (r >= 0 && r < Rate1)
			{
				count1++;
				continue;
			}
			r = Random.value;
			if (r >= 0 && r < Rate2)
				count2++;
			r = Random.value;
		}
		Debug.Log(count1);
		Debug.Log(count2);

		count1 = 0;
		count2 = 0;
		float Rate3 = Rate2 / (1f - Rate1);
		for (int i = 0; i < 10000; i++)
		{
			float r = Random.value;
			if (r >= 0 && r < Rate1)
			{
				count1++;
				continue;
			}
			r = Random.value;
			if (r >= 0 && r < Rate3)
				count2++;
			r = Random.value;
		}
		Debug.Log(count1);
		Debug.Log(count2);
	}

	[MenuItem("Tools/QuickTest")]
	public static void QuickTest()
	{
		BoneAnimation ba = Selection.activeGameObject.GetComponent<BoneAnimation>();
		if (ba)
		{
			TextureAtlas ta = UnityEngine.Resources.Load("Atlas/KappaRed", typeof(ScriptableObject)) as TextureAtlas;
			string[] boneName = new string[]{"Body","HandL","HandR","Head","LegL","LegR"};
			for (int i = 0; i < boneName.Length; i++)
			{
				
			
				Sprite s = ba.GetSpriteTransform(boneName[i]).GetComponent<Sprite>();
				if(!s)
					s = ba.GetSpriteTransform(boneName[i]).gameObject.AddComponent<Sprite>();
				s.SetAtlas(ta);
				s.SetPivotOffset(Vector2.zero, true);
				s.SetTextureName(boneName[i]);
				
			}
		}
	}


	[MenuItem("Assets/SmoothAtlas/CopyAtlasSetting")]
	public static void CopyAtlasSetting()
	{
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if(ta)
			buffer = ta;
	}
	[MenuItem("Assets/SmoothAtlas/PastePivotOffsets")]
	public static void PastePivotOffsets()
	{
		TextureAtlas tabuffer = buffer as TextureAtlas;
		TextureAtlas ta = Selection.activeObject as TextureAtlas;
		if (ta && tabuffer)
		{
			for (int i = 0; i < tabuffer.textureNames.Count; i++)
			{
				for (int j = 0; j < ta.textureNames.Count; j++)
				{
					if (ta.textureNames[j].StartsWith(tabuffer.textureNames[i]))
					{
						ta.defaultPivotOffsets[j] = tabuffer.defaultPivotOffsets[i];
						
						break;
					}
				}
			}
		}
	}
	[MenuItem("Tools/QuickTest1")]
	public static void QuickTest1()
	{
		foreach (Texture2D t2d in Selection.objects)
		{

			if (t2d)
			{
				string iPath = AssetDatabase.GetAssetPath(t2d);
				string opath = Application.dataPath.Replace("Assets", Path.GetDirectoryName(iPath) + "/" + Path.GetFileNameWithoutExtension(iPath) + "r.png");

				TextureImporter ti = AssetImporter.GetAtPath(iPath) as TextureImporter;
				if (ti)
				{
					ti.isReadable = true;
					AssetDatabase.ImportAsset(iPath);
				}
				Texture2D t2d2 = new Texture2D(t2d.width, t2d.height);

				Color[] c = t2d.GetPixels();
				for (int i = 0; i < c.Length; i++)
				{
					if (c[i].a > 0)
					{
						if (c[i].g > c[i].r)
						{
							float t = c[i].r;
							c[i].r = c[i].g;
							c[i].g = t;
						}
					}
				}
				t2d2.SetPixels(c);
				t2d2.Apply();

				FileStream fs = new FileStream(opath, FileMode.Create);
				byte[] tb = t2d2.EncodeToPNG();
				fs.Write(tb, 0, tb.Length);
				fs.Close();

				Object.Destroy(t2d2);
			}
		}
		AssetDatabase.Refresh();
	}
	
	[MenuItem("Tools/QuickTest2")]
	public static void Trans()
	{
		Object[] cams = Object.FindObjectsOfTypeAll(typeof(Camera));
		Camera cam = null;
		foreach(Camera cami in cams)
		{
			if (cami.name == "UnitCamera")
			{
				cam = cami;
				break;
			}
		}
		Debug.Log(cam);
		Selection.activeGameObject.transform.position = TranslateCamera(Camera.main, Selection.activeGameObject.transform.position, cam);
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
	[MenuItem("Assets/AnimationData/TransFormOnly")]
	public static void SetAnimationData_TransFormOnly()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				foreach (AnimationClipBone acb in acsm.bones)
				{
					if (acb.keyframes.Count >= 1)
					{
						acb.keyframes[0].keyframeType = KeyframeSM.KEYFRAME_TYPE.TransformOnly;
					}
					for (int i = 1; i < acb.keyframes.Count; i++)
					{
						acb.keyframes[i].useKeyframeType = false;
						acb.keyframes[i].useAtlas = false;
						acb.keyframes[i].useTextureGUID = false;
						acb.keyframes[i].usePivotOffset = false;
					}
				}
			}
		}
	}

	[MenuItem("Assets/AnimationData/TestImage")]
	public static void SetAnimationData_Image()
	{
		BoneAnimationData baData = Selection.activeObject as BoneAnimationData;
		if (baData)
		{
			TextureAtlas ta = UnityEngine.Resources.Load("Atlas/KappaRed", typeof(ScriptableObject)) as TextureAtlas;
			foreach (AnimationClipSM acsm in baData.animationClips)
			{
				foreach (AnimationClipBone acb in acsm.bones)
				{
					if(acb.keyframes.Count >= 1)
					{
						acb.keyframes[0].keyframeType = KeyframeSM.KEYFRAME_TYPE.Image;
						acb.keyframes[0].atlas = ta;
						acb.keyframes[0].textureGUID = ta.GetTextureGUIDFromName(baData.boneDataList[acb.boneDataIndex].boneName);
					}
					for(int i = 1 ; i < acb.keyframes.Count ; i ++)
					{
						acb.keyframes[i].useKeyframeType = false;
						acb.keyframes[i].useAtlas = false;
						acb.keyframes[i].useTextureGUID = false;
						acb.keyframes[i].usePivotOffset = false;
					}
				}
			}
		}
	}
}
