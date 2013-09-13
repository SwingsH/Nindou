using UnityEngine;
using System.Collections;

public class TimeMachine : MonoBehaviour {

	static TimeMachine _instance;
	static TimeMachine Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = new GameObject("TimeMachine").AddComponent<TimeMachine>();
			}
			return _instance;
		}
	}

	public float TimeScaleSetting =1;
	
	protected float stopTime;
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > stopTime)
		{
			enabled = false;
		}
	}
	void OnDisable()
	{
		Time.timeScale = TimeScaleSetting;
	}
	public static void ChangeTimeScale(float scale, float duration)
	{
		Instance.enabled = true;
		Time.timeScale = scale;
		Instance.stopTime = Time.realtimeSinceStartup + duration;
	}
}
