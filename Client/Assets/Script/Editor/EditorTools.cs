using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
		Dictionary<Transform, List<int>> td = new Dictionary<Transform, List<int>>();
		Transform t = new GameObject().transform;
		td.Add(t, new List<int>());
		foreach (KeyValuePair<Transform, List<int>> kvp in td)
		{
			Debug.Log(kvp.Key);
			Debug.Log(kvp.Value);
		}
		Debug.Log(GameObject.FindObjectsOfType(typeof(Transform)).Length);
		GameObject.DestroyImmediate(t.gameObject);
		Debug.Log(GameObject.FindObjectsOfType(typeof(Transform)).Length);
		Resources.UnloadUnusedAssets();
		Debug.Log(GameObject.FindObjectsOfType(typeof(Transform)).Length);
		List<Transform> removeKey = new List<Transform>();
		foreach (KeyValuePair<Transform, List<int>> kvp in td)
		{
			Debug.Log(kvp.Key);
			Debug.Log(kvp.Value);
			if (kvp.Key == null)
				removeKey.Add(kvp.Key);
		}
		foreach (Transform trans in removeKey)
		{
			Debug.Log(trans);
			Debug.Log(trans == null);
			Debug.Log(object.Equals(trans,null));
			Debug.Log(object.ReferenceEquals(trans, null));
			Debug.Log(td.Remove(trans));
		}
		try
		{
			Debug.Log(td.Remove(null));
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
		Debug.Log(GameObject.FindObjectsOfType(typeof(Transform)).Length);
		Resources.UnloadUnusedAssets();
		Debug.Log(GameObject.FindObjectsOfType(typeof(Transform)).Length);
		Transform tn = null;
		Debug.Log(td.ContainsKey(t));
		//Debug.Log(td.ContainsKey(tn));
		
		Debug.Log(t == null);
		Debug.Log(t.name);
		Debug.Log(td.ContainsKey(null));
	}


	[MenuItem("Tools/QuickTest1")]
	public static void QuickTest1()
	{
		object obj = new List<SkillData>();
		TextAsset ta = Resources.Load("Data/skilldata", typeof(TextAsset)) as TextAsset;
		
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
