using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SimpleExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public bool DisplayExit = false;
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			DisplayExit = true;
		}
	}
	public Rect BoxRect = new Rect(0.3f, 0.3f, 0.4f, 0.4f);
	public Rect LabelRect = new Rect(0.1f, 0.1f, 0.8f, 0.4f);
	public Rect ButtonRect = new Rect(0.1f, 0.6f, 0.3f, 0.3f);
	void OnGUI()
	{
		if (DisplayExit)
		{
			Rect ScreeRect = new Rect(Screen.width * BoxRect.x, Screen.height * BoxRect.y, Screen.width * BoxRect.width, Screen.height * BoxRect.height);
			GUI.Box(ScreeRect, "");
			GUI.BeginGroup(ScreeRect);
			Rect tempRect = LabelRect;
			tempRect.x *= ScreeRect.width;
			tempRect.y *= ScreeRect.height;
			tempRect.width *= ScreeRect.width;
			tempRect.height *= ScreeRect.height;

			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(tempRect, "Exit");

			tempRect = ButtonRect;
			tempRect.x *= ScreeRect.width;
			tempRect.y *= ScreeRect.height;
			tempRect.width *= ScreeRect.width;
			tempRect.height *= ScreeRect.height;
			if (GUI.Button(tempRect, "Yes"))
				Application.Quit();
			tempRect.x = ScreeRect.width * (1 - (ButtonRect.width + ButtonRect.x));
			if(GUI.Button(tempRect, "No"))
				DisplayExit = false;
			GUI.EndGroup();
		}
	}
}
