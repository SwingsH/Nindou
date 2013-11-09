using UnityEngine;
using UnityEditor;
using System.Collections;

public class CurveEditor : EditorWindow
{
	AnimationCurve curve;
	[MenuItem("Tools/CurveEditor")]
	static void Init()
	{
		CurveEditor window = EditorWindow.GetWindow<CurveEditor>();
		window.curve = new AnimationCurve();
		
	}

	void OnGUI()
	{
		Rect curveRect = new Rect(10, 10, position.width - 20, position.height - 50);
		curve = EditorGUI.CurveField(curveRect, curve);
		string curveInfo= "";
		//curve = new AnimationCurve(new Keyframe(0,0,
		foreach (Keyframe kf in curve.keys)
		{
			if (!string.IsNullOrEmpty(curveInfo))
				curveInfo += ",";
			curveInfo += string.Format("new Keyframe({0:0.00}f,{1:0.00}f,{2:0.00}f,{3:0.00}f)", kf.time, kf.value, kf.inTangent, kf.outTangent);
		}

		GUI.SetNextControlName("CopyField");
		EditorGUI.TextField(new Rect(10, curveRect.yMax + 10, curveRect.width, 20), curveInfo);
		if (GUI.GetNameOfFocusedControl() == "CopyField")
		{
			TextEditor te = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.GetControlID(FocusType.Keyboard)) as TextEditor;
			if (te != null)
			{
				te.SelectAll();
			}
		}

		
	}
}
