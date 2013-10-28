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
        [DoubleStringValue("頭", "Head")]				HEAD ,
        [DoubleStringValue("左腳", "LegL")]				LEG_LEFT ,
        [DoubleStringValue("右腳", "LegR")]				LEG_RIGHT ,
        [DoubleStringValue("身體", "Body")]				BODY ,
        [DoubleStringValue("武器", "WEAPON")]			WEAPON,

    }

	[MenuItem("Tools/RenameModelTexture")]
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

	[MenuItem("Tools/QuickTest")]
	public static void QuickTest()
	{
		Camera c = Camera.main;
		PostEffectManager pem = c.gameObject.AddComponent<PostEffectManager>();
		pem.TargetCamera = c;
		pem.SetDefaultEffect_1(c, 6);
		pem.SetDefaultEffect_2(pem, 3);
	}

	static void paramstest(params KeyValuePair<string,object>[] objs)
	{
		Debug.Log(objs.Length);
	}
	[MenuItem("Tools/QuickTest1")]
	public static void QuickTest1()
	{
		object obj = new List<SkillData>();
        TextAsset ta = Resources.Load(GLOBALCONST.DIR_RESOURCES_DATA + GLOBALCONST.FILENAME_SKILL, typeof(TextAsset)) as TextAsset;
		
		DataUtility.DeserializeObject(ta.text,ref obj);
		Debug.Log((obj as List<SkillData>).Count);
		foreach (SkillData sd in obj as List<SkillData>)
		{
			Debug.Log(sd.Name);
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
}
