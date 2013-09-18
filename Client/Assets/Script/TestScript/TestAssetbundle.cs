using UnityEngine;
using System.Collections;
using System.IO;
public class TestAssetbundle : MonoBehaviour {
	public Object[] objs;
	public System.WeakReference[] wkObjs;
	// Use this for initialization
	void Start () {
		StartCoroutine(LoadFormByte());
	}
	IEnumerator LoadFormByte()
	{
		FileStream fs = new FileStream(@"D:\Document(D)\NinDou\Client\Assets\Test\Resources\Assetbundles\KappaAnim.unity3d", FileMode.Open);
		if (fs != null)
		{
			byte[] abb = new byte[fs.Length];
			fs.Read(abb, 0, (int)fs.Length);
			fs.Close();
			AssetBundleCreateRequest abc = AssetBundle.CreateFromMemory(abb);
			yield return abc;
			Debug.Log(abc.assetBundle);
			foreach (Object obj in abc.assetBundle.LoadAll())
			{
				Debug.Log(obj);
				if (obj is GameObject)
				{
					Instantiate(obj);
				}
			}
			objs = abc.assetBundle.LoadAll();
			wkObjs = new System.WeakReference[objs.Length];
			for (int i = 0; i < objs.Length; i++)
			{
				wkObjs[i] = new System.WeakReference(objs[i]);

			}
			//objs = null;
			Resources.UnloadUnusedAssets();
		}
		
		
	}
	// Update is called once per frame
	void OnGUI () {
		string content = "";
		if (wkObjs != null)
		{
			for (int i = 0; i < wkObjs.Length; i++)
			{
				if (wkObjs[i].IsAlive)
					content += wkObjs[i].Target.ToString();
				else
					content += "Null";
				content += "\n";
			}
		}
		GUI.Box(new Rect(10, 10, 200, 500), content);
	}

}
