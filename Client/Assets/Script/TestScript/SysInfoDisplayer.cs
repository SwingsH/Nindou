using UnityEngine;
using System.Collections;

public class SysInfoDisplayer : MonoBehaviour {

	float time;
	float ezFPS;
	float FPSCounter;
	const float ReflashRate = 0.13f;
	const float FPSRate = 1 / ReflashRate;
	// Use this for initialization
	void Start () {
		time = Time.realtimeSinceStartup + ReflashRate;
	}
	
	// Update is called once per frame
	void Update ()
	{
		FPSCounter++;
		if (Time.realtimeSinceStartup > time)
		{
			ezFPS = FPSCounter * FPSRate;
			FPSCounter = 0;
			time = Time.realtimeSinceStartup + ReflashRate;
		}
	}
	void OnGUI()
	{
		int y = 10;
		GUI.Label(new Rect(Screen.width - 200, y += 30, 200, 30), SystemInfo.graphicsShaderLevel.ToString("GraphicsShaderLevel #.#"));
		GUI.Label(new Rect(Screen.width - 200, y += 30, 200, 30), Profiler.usedHeapSize.ToString("UsedHeapSize #,##0"));
		GUI.Label(new Rect(Screen.width - 200, y += 30, 200, 30), System.GC.GetTotalMemory(false).ToString("GCTotalMemory #,##0"));
		GUI.Label(new Rect(Screen.width - 200, y += 30, 200, 30), ezFPS.ToString("FPS #.#")); 
	}
}
