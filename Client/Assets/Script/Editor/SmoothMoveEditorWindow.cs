using UnityEngine;
using UnityEditor;
using System.Collections;
using SmoothMoves;
public class SmoothMoveEditorWindow : EditorWindow {

	[MenuItem("Assets/AnimationData/SetImage")]
	static void Init()
	{
		SmoothMoveEditorWindow window = EditorWindow.GetWindow<SmoothMoveEditorWindow>();
		window.animData = Selection.activeObject as BoneAnimationData;
		window.minSize = new Vector2(550, 300);
	}
	BoneAnimationData animData;
	TextureAtlas atlas;
	void OnGUI()
	{
		animData = EditorGUI.ObjectField(new Rect(10, 10, 500, 16),"BoneAnimationData", animData, typeof(BoneAnimationData),false) as BoneAnimationData;
		atlas = EditorGUI.ObjectField(new Rect(10, 40, 500, 16), "AtlasName", atlas, typeof(TextureAtlas), false) as TextureAtlas;
		GUILayout.BeginArea(new Rect(10, 70, 500, 40));
		if (GUILayout.Button("Set Image"))
		{
			SmoothMoveEditorTool.SetAnimationData_Image(animData, atlas);
		}
		GUILayout.EndArea();
	}
}
