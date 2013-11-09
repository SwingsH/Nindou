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
    // 加速使用的三階段速率常數
    const float NORMAL_SPEED = 1;
    const float FAST_SPEED = 5;
    const float VERY_FAST_SPEED = 10;

	static float TimeScaleSetting =1;
	
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

    public static void FastForward()
    {
        float nextTimeScale = TimeScaleSetting;
        if (Mathf.Approximately(TimeScaleSetting, NORMAL_SPEED)) { nextTimeScale = FAST_SPEED; }
        else if (Mathf.Approximately(TimeScaleSetting, FAST_SPEED)) { nextTimeScale = VERY_FAST_SPEED; }
        else if (Mathf.Approximately(TimeScaleSetting, VERY_FAST_SPEED)) { nextTimeScale = NORMAL_SPEED; }
        SetTimeScale(nextTimeScale);
    }

    public static void Resume()
    {
        if (_instance == null || Time.realtimeSinceStartup > _instance.stopTime)
        {
            Time.timeScale = TimeScaleSetting;
        }
    }

    public static void Pause()
    {
        if (_instance == null || Time.realtimeSinceStartup > _instance.stopTime)
        {
            Time.timeScale = 0;
        }
    }

	public static void SetTimeScale(float scale)
	{
		TimeScaleSetting = scale;
		if (_instance == null || Time.realtimeSinceStartup > _instance.stopTime)
			Time.timeScale = TimeScaleSetting;
	}

	/// <summary>
	/// 暫時變更TimeScale一小段時間
	/// </summary>
	/// <param name="scale"></param>
	/// <param name="duration"></param>
	public static void ChangeTimeScale(float scale, float duration)
	{
		Instance.enabled = true;
		Time.timeScale = scale;
		Instance.stopTime = Time.realtimeSinceStartup + duration;
	}
}
