using UnityEngine;
using System.Collections;

public class TestUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acc = Input.acceleration;
		
		Quaternion target;
		if (acc.x < acc.y)
			target = Quaternion.Euler(0, 0, 90);
		else
			target= Quaternion.identity;
		Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, target, 20);
	}

	void OnGUI()
	{
		Vector2 center = new Vector2(Screen.width/2,Screen.height/2);
		Rect r = new Rect();
		r.x = center.x - 100;
		r.y =  center.y - Input.accelerationEventCount * 20;
		r.xMax = center.x + 100;
		r.yMax = center.y + Input.accelerationEventCount * 20;

		string content = "";
		foreach (AccelerationEvent ae in Input.accelerationEvents)
		{
			content += (ae.acceleration).ToString() + "\n";
		}
		GUI.Box(r, content);

		r.x = 40;
		r.y = 0;
		r.xMax = Screen.width - 40;
		r.yMax = 40;
		if(GUI.Button(r, ScreenOrientation.PortraitUpsideDown.ToString()))
			Screen.orientation = ScreenOrientation.PortraitUpsideDown;

		r.x = 40;
		r.y = Screen.height - 40;
		r.xMax = Screen.width - 40;
		r.yMax = Screen.height;
		if(GUI.Button(r, ScreenOrientation.Portrait.ToString()))
			Screen.orientation = ScreenOrientation.Portrait;

		r.x = 0;
		r.y = 40;
		r.xMax = 40;
		r.yMax = Screen.height -40;
		if(GUI.Button(r, ScreenOrientation.LandscapeLeft.ToString()))
			Screen.orientation = ScreenOrientation.LandscapeLeft;

		r.x = Screen.width - 40;
		r.y = 40;
		r.xMax = Screen.width;
		r.yMax = Screen.height - 40;
		if (GUI.Button(r, ScreenOrientation.LandscapeRight.ToString()))
			Screen.orientation = ScreenOrientation.LandscapeRight;
	}
}
