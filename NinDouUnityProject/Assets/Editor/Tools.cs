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
		Texture2D t2d = new Texture2D(256,256);
		Vector2 center=  new Vector2(128,128);
		for(int i = 0 ; i < 129 ;i++)
			for(int j = 0 ; j < 129 ;j++)
			{
				Color c =Color.clear;
				float d = Vector2.Distance(new Vector2(i,j),center);
				if(d < 100)
				{
					c.a =Mathf.Clamp01((100 - d + 50) / 100f);
				}
				t2d.SetPixel(i,j,c);
				t2d.SetPixel(256-i,256-j,c);
				t2d.SetPixel(i,256-j,c);
				t2d.SetPixel(256-i,j,c);
			}
		t2d.Apply();
		AssetDatabase.CreateAsset(t2d, AssetDatabase.GenerateUniqueAssetPath("Assets/TestShadow.Asset"));

	}
	[MenuItem("Tools/QuickTest1")]
	public static void QuickTest1()
	{
		if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<ParticleSystem>())
		{
			ParticleSystem ps = Selection.activeGameObject.GetComponent<ParticleSystem>();
			//Particle p = ps.

			ps.Emit(1);
			ps.Emit(1);
			ps.Emit(1);
			ps.Emit(1);
			ps.Emit(Vector3.zero, ps.startSpeed * Vector3.right, ps.startSize, ps.startLifetime, ps.startColor);
		}	
	}
}
