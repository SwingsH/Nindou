using UnityEngine;
using UnityEditor;
using SmoothMoves;
using System.Collections;
using System.IO;
public class BoneNodeTextureEditorWindow : EditorWindow
{
	const string HAND_LEFT = "HandL";
	const string PAM_LEFT = "PamL";
	const string HAND_RIGHT = "HandR";
	const string PAM_RIGHT = "PamR";
	const string HEAD = "Head";
	const string HAIR = "Hair";
	const string LEG_LEFT = "LegL";
	const string LEG_RIGHT = "LegR";
	const string BODY = "Body";
	const string WEAPON = "Weapon";
	const string EYES = "Eyes";

	[MenuItem("Assets/SmoothAtlas/GenerateBoneNodeTexture")]
	static void Init()
	{
		BoneNodeTextureEditorWindow window = EditorWindow.GetWindow<BoneNodeTextureEditorWindow>();
		window.atlas = Selection.activeObject as TextureAtlas;
		window.wantsMouseMove = true;
		window.minSize = new Vector2(1100, 600);
	}
	Texture2D tempTexture;
	Texture2D previewTexture;
	TextureAtlas atlas;
	int selectedIndex;
	Vector2 textureScroll;
	bool SelectChange = false;
	void OnGUI ()
	{
		atlas = EditorGUI.ObjectField(new Rect(10, 10, 500, 16), "AtlasName", atlas, typeof(TextureAtlas), false) as TextureAtlas;
		EditorGUI.LabelField(new Rect(510, 10,200, 16), Event.current.mousePosition.ToString());
		if (Event.current.type == EventType.MouseMove)
			Repaint();
		if (atlas)
		{
			int newSelect= EditorGUI.Popup(new Rect(10,30,200,30),selectedIndex,atlas.textureNames.ToArray());
			if (newSelect != selectedIndex)
				SelectChange = true;
			selectedIndex = newSelect;

			GUILayout.BeginArea(new Rect(10, 70, 200, 20));
			if (GUILayout.Button("Save Bone Image"))
			{
				if (!AssetDatabase.LoadAssetAtPath("Assets/TempBoneTexture", typeof(Object)))
					AssetDatabase.CreateFolder("Assets", "TempBoneTexture");
				SaveTempTexture(atlas.textureNames[selectedIndex]);
			}
			GUILayout.EndArea();

			Vector2 offset = atlas.defaultPivotOffsets[selectedIndex];
			offset.x += 0.5f;
			offset.y += 0.5f;
			offset.y = 1 - offset.y;
			offset.x *= atlas.textureSizes[selectedIndex].x;
			offset.y *= atlas.textureSizes[selectedIndex].y;

			Texture2D tex = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(atlas.textureGUIDs[selectedIndex]), typeof(Texture2D)) as Texture2D;
			Rect textureRect = new Rect(10, 90, tex.width, tex.height);
			GUILayout.BeginArea(textureRect);
			EditorGUI.DrawTextureTransparent(new Rect(0, 0, tex.width, tex.height), tex, ScaleMode.ScaleAndCrop);
			EditorGUI.DrawRect(new Rect(offset.x - 5, offset.y - 5, 10, 10), new Color(1, 0, 0, 0.5f));
			GUILayout.EndArea();

			switch (atlas.textureNames[selectedIndex])
			{
				case HEAD:
				case LEG_LEFT:
				case LEG_RIGHT:
				case HAND_LEFT:
				case HAND_RIGHT:
					HandLegPart();
					break;
				case BODY:
					BodyPart();
					break;
				case EYES:
					FaceEyePart();
					break;
			}

			if (tempTexture)
			{
				EditorGUI.DrawTextureTransparent(new Rect(30 + tex.width * 2, 90, tex.width, tex.height), tempTexture, ScaleMode.ScaleAndCrop);
				if (previewTexture)
					EditorGUI.DrawTextureTransparent(new Rect(20 + tex.width, 90, tex.width, tex.height), previewTexture, ScaleMode.ScaleAndCrop);
			}
		}
		
	}

	void BodyPart()
	{

		Texture2D tex = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(atlas.textureGUIDs[selectedIndex]), typeof(Texture2D)) as Texture2D;
		string[] otherPartNames = new string[] { HAND_LEFT, HAND_RIGHT, LEG_LEFT, LEG_RIGHT, HEAD };

		if (SelectChange)
		{
			if (tempTexture == null || AssetDatabase.IsMainAsset(tempTexture))
				tempTexture = new Texture2D(tex.width, tex.height);
			else
				tempTexture.Resize(tex.width, tex.height);

			if (previewTexture == null)
				previewTexture = new Texture2D(tex.width, tex.height);
			else
				previewTexture.Resize(tex.width, tex.height);

			previewTexture.SetPixels(tex.GetPixels());
			tempTexture.SetPixels(new Color[tex.width * tex.height]);

			for (int partIndex = 0; partIndex < otherPartNames.Length; partIndex++)
			{
				int atlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(otherPartNames[partIndex]));
				if (atlasIndex < 0)
					continue;
				Vector2 offset = atlas.defaultPivotOffsets[atlasIndex];
				offset.x += 0.5f;
				offset.y += 0.5f;
				offset.x *= atlas.textureSizes[atlasIndex].x;
				offset.y *= atlas.textureSizes[atlasIndex].y;
				DrawCircle(tempTexture, (int)offset.x, (int)offset.y, 10);
				DrawCircle(previewTexture, (int)offset.x, (int)offset.y, 10);
			}
			tempTexture.Apply();
			previewTexture.Apply();
			SelectChange = false;
		}
		
		Rect textureRect = new Rect(10, 90, tex.width, tex.height);
		GUILayout.BeginArea(textureRect);
		for (int partIndex = 0; partIndex < otherPartNames.Length; partIndex++)
		{
			int atlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(otherPartNames[partIndex]));
			if(atlasIndex < 0)
				continue;
			Vector2 offset = atlas.defaultPivotOffsets[atlasIndex];
			offset.x += 0.5f;
			offset.y += 0.5f;
			offset.y = 1 - offset.y;
			offset.x *= atlas.textureSizes[atlasIndex].x;
			offset.y *= atlas.textureSizes[atlasIndex].y;
			EditorGUI.DrawRect(new Rect(offset.x - 5, offset.y - 5, 10, 10), new Color(1, 0, 0, 0.5f));
		}
		GUILayout.EndArea();
	}

	void HandLegPart()
	{
		GUILayout.BeginArea(new Rect(10, 70, 200, 20));
		if (GUILayout.Button("Save Bone Image") && tempTexture)
		{
			if (!AssetDatabase.LoadAssetAtPath("Assets/TempBoneTexture", typeof(Object)))
				AssetDatabase.CreateFolder("Assets", "TempBoneTexture");

			SaveTempTexture(atlas.textureNames[selectedIndex]);

		}
		GUILayout.EndArea();

		Vector2 offset = atlas.defaultPivotOffsets[selectedIndex];
		offset.x += 0.5f;
		offset.y += 0.5f;
		offset.y = 1 - offset.y;
		offset.x *= atlas.textureSizes[selectedIndex].x;
		offset.y *= atlas.textureSizes[selectedIndex].y;


		Texture2D tex = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(atlas.textureGUIDs[selectedIndex]), typeof(Texture2D)) as Texture2D;
		Rect textureRect = new Rect(10, 90, tex.width, tex.height);

		if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
		{
			if (textureRect.Contains(Event.current.mousePosition))
			{
				if (tempTexture == null || AssetDatabase.IsMainAsset(tempTexture))
					tempTexture = new Texture2D(tex.width, tex.height);
				else
					tempTexture.Resize(tex.width, tex.height);
				if(previewTexture == null)
					previewTexture = new Texture2D(tex.width, tex.height);
				else
					previewTexture.Resize(tex.width, tex.height);
				previewTexture.SetPixels(tex.GetPixels());
				tempTexture.SetPixels(new Color[tex.width * tex.height]);
				//計算圖片錨點對應到原圖的位置
				offset = atlas.defaultPivotOffsets[selectedIndex];
				offset.x += 0.5f;
				offset.y += 0.5f;
				offset.x *= atlas.textureSizes[selectedIndex].x;
				offset.y *= atlas.textureSizes[selectedIndex].y;
				DrawCircle(tempTexture, (int)offset.x, (int)offset.y, 10);
				DrawCircle(previewTexture, (int)offset.x, (int)offset.y, 10);
				//計算滑鼠遊標對應到原圖的位置
				Vector2 lineEnd = Event.current.mousePosition;
				lineEnd.x -= textureRect.x;
				lineEnd.y -= textureRect.y;
				lineEnd.y = atlas.textureSizes[selectedIndex].y - lineEnd.y;
				DrawLine(tempTexture, (int)offset.x, (int)offset.y, (int)lineEnd.x, (int)lineEnd.y, 5);
				DrawLine(previewTexture, (int)offset.x, (int)offset.y, (int)lineEnd.x, (int)lineEnd.y, 5);
				tempTexture.Apply();
				previewTexture.Apply();
			}
		}
	}

	void FaceEyePart()
	{
		Texture2D tex = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(atlas.textureGUIDs[selectedIndex]), typeof(Texture2D)) as Texture2D;

		if (SelectChange)
		{
			if (tempTexture == null || AssetDatabase.IsMainAsset(tempTexture))
				tempTexture = new Texture2D(tex.width, tex.height);
			else
				tempTexture.Resize(tex.width, tex.height);

			if (previewTexture == null)
				previewTexture = new Texture2D(tex.width, tex.height);
			else
				previewTexture.Resize(tex.width, tex.height);

			previewTexture.SetPixels(tex.GetPixels());
			tempTexture.SetPixels(new Color[tex.width * tex.height]);

			
			Vector2 offset = atlas.defaultPivotOffsets[selectedIndex];
			offset.x += 0.5f;
			offset.y += 0.5f;
			offset.x *= atlas.textureSizes[selectedIndex].x;
			offset.y *= atlas.textureSizes[selectedIndex].y;
			DrawCircle(tempTexture, (int)offset.x, (int)offset.y, 10);
			DrawCircle(previewTexture, (int)offset.x, (int)offset.y, 10);

			int atlasIndex = atlas.GetTextureIndex(atlas.GetTextureGUIDFromName(HEAD));
			if (atlasIndex >= 0)
			{
				Vector2 headOffset = atlas.defaultPivotOffsets[atlasIndex];
				headOffset.x += 0.5f;
				headOffset.y += 0.5f;
				headOffset.x *= atlas.textureSizes[atlasIndex].x;
				headOffset.y *= atlas.textureSizes[atlasIndex].y;
				DrawCircle(tempTexture, (int)headOffset.x, (int)headOffset.y, 10);
				DrawCircle(previewTexture, (int)headOffset.x, (int)headOffset.y, 10);

				//DrawLine(tempTexture, (int)offset.x, (int)offset.y, (int)headOffset.x, (int)headOffset.y, 2);
				//DrawLine(previewTexture, (int)offset.x, (int)offset.y, (int)headOffset.x, (int)headOffset.y, 2);
			}
			tempTexture.Apply();
			previewTexture.Apply();
			SelectChange = false;
		}
	}
	//畫圓
	static void DrawCircle(Texture2D tex, int posx, int posy, int size)
	{
		for (int i = 0; i <= size; i++)
		{
			for (int j = 0; j <= size; j++)
			{
				float distance = Mathf.Pow(Mathf.Pow(i - size/2f,2) + Mathf.Pow(j - size/2f,2),0.5f);

				Color orgin = tex.GetPixel((int)(posx - size / 2 + i), (int)(posy - size / 2 + j));
				Color circleColor = Color.Lerp(Color.black, orgin, Mathf.Clamp01(distance - size / 2));
				tex.SetPixel((int)(posx - size / 2 + i), (int)(posy - size / 2 + j), circleColor);
			}
		}
	}

	static void DrawLine(Texture2D tex, int posx1, int posy1, int posx2, int posy2,int size)
	{
		int diffx = posx2 - posx1;
		int diffy = posy2 - posy1;

		float slope = (float)diffx / diffy;
		float slopeVerticle = 0;
		if (slope != 0)
			slopeVerticle = -1 / slope;
		for (int sizei = -size / 2; sizei < size / 2; sizei++)
		{
			int xStart = posx1;
			int yStart = posy1;
			if (slope == 0)
			{
				xStart = posx1 + sizei;
				yStart = posy1;
			}
			else if (Mathf.Abs(slopeVerticle) < 1)
			{
				xStart = posx1 + Mathf.FloorToInt(sizei * slopeVerticle);
				yStart = posy1 + sizei;
			}
			else
			{
				xStart = posx1 + sizei;
				yStart = posy1 + Mathf.FloorToInt(sizei / slopeVerticle);
			}
			if (Mathf.Abs(slope) < 1)
			{
				for (int i = 0; i < Mathf.Abs(diffy); i++)
				{
					int x = xStart + Mathf.FloorToInt(Mathf.Sign(diffy) * i * slope);
					int y = yStart + (int)Mathf.Sign(diffy) * i;

					tex.SetPixel(x, y, Color.black);
					x = xStart + Mathf.CeilToInt(Mathf.Sign(diffy) * i * slope);
					tex.SetPixel(x, y, Color.black);
				}
			}
			else
			{
				for (int i = 0; i < Mathf.Abs(diffx); i++)
				{
					int x = xStart + (int)Mathf.Sign(diffx) * i;
					int y = yStart + Mathf.FloorToInt(Mathf.Sign(diffx) * i / slope);

					tex.SetPixel(x, y, Color.black);

					y = yStart + Mathf.CeilToInt(Mathf.Sign(diffx) * i / slope);
					tex.SetPixel(x, y, Color.black);
				}
			}

		}
	}


	void SaveTempTexture(string name)
	{
		if (!AssetDatabase.LoadAssetAtPath("Assets/TempBoneTexture", typeof(Object)))
			AssetDatabase.CreateFolder("Assets", "TempBoneTexture");

		string path = Application.dataPath + "/TempBoneTexture/" + name + ".png";
		Debug.Log(path);
		FileStream fs = File.Open(path, FileMode.Create);
		byte[] png = tempTexture.EncodeToPNG();
		fs.Write(png, 0, png.Length);
		fs.Close();

		AssetDatabase.Refresh();
	}
}
