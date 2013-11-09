using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
public class RemoveColorEditor : EditorWindow {
	[MenuItem("Assets/RemoveTextureColorWindow")]
	static void Init()
	{
		RemoveColorEditor window = EditorWindow.GetWindow<RemoveColorEditor>();

		if (Selection.activeObject is Texture2D)
			window.sourceTex = Selection.activeObject as Texture2D;
	}
	Texture2D sourceTex;
	Texture2D removedTex;
	float differValue;
	void OnGUI()
	{
		sourceTex = EditorGUI.ObjectField(new Rect(10, 10, 256, 256), sourceTex, typeof(Texture2D),false) as Texture2D;
		Rect textureRect = new Rect(10,270,100,100);
		if (sourceTex)
		{
			textureRect.width = sourceTex.width;
			textureRect.height = sourceTex.height;
			EditorGUI.DrawTextureTransparent(textureRect, sourceTex);
			if (Event.current.isMouse)
			{
				if (textureRect.Contains(Event.current.mousePosition))
				{
					TextureImporter ti = TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(sourceTex)) as TextureImporter;
					if (ti)
					{
						ti.isReadable = true;
						ti.textureFormat = TextureImporterFormat.ARGB32;
						AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(sourceTex));
					}
					Vector2 posOnTex = new Vector2(Event.current.mousePosition.x - textureRect.x, Event.current.mousePosition.y - textureRect.y);
					Color c = sourceTex.GetPixel((int)posOnTex.x, (int)posOnTex.y);

					Color[] cols = sourceTex.GetPixels();
					for (int i = 0; i < cols.Length; i++)
					{

						if (cols[i] == c)
						{
							cols[i].a = 0;
						}
						else
						{
							float colordiffer = Mathf.Abs(cols[i].r - c.r);
							colordiffer += Mathf.Abs(cols[i].g - c.g);
							colordiffer += Mathf.Abs(cols[i].b - c.b);
							if (colordiffer < differValue)
								cols[i].a = 0;
						}
					}
					if(!removedTex)
						removedTex = new Texture2D(sourceTex.width,sourceTex.height);
					else
						removedTex.Resize(sourceTex.width, sourceTex.height);
					removedTex.SetPixels(cols);
					removedTex.Apply();
				}
			}
			if (removedTex)
			{
				EditorGUI.DrawTextureTransparent(new Rect(textureRect.x + textureRect.width, textureRect.y, textureRect.width, textureRect.height), removedTex);

				if (GUI.Button(new Rect(10, textureRect.yMax + 10, 200, 30), "Save Removed Color Picture"))
				{
					string nPath = AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(sourceTex));
					nPath = Path.ChangeExtension(nPath, ".png");
					nPath = Application.dataPath.Replace("Assets", nPath);
					FileStream fs = new FileStream(nPath, FileMode.Create);
					byte[] tb = removedTex.EncodeToPNG();
					fs.Write(tb, 0, tb.Length);
					fs.Close();
					AssetDatabase.Refresh();
				}
			}

			differValue = EditorGUI.FloatField(new Rect(210, textureRect.yMax + 10, 100, 30), differValue);
		}
	}
}
