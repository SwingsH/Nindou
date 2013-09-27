using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class ClickTest : MonoBehaviour {
    public GameObject obj;
	public int count;
	// Use this for initialization
	void Start () {
		obj = new GameObject("obj");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < count; i++)
				Instantiate(obj);
			sw.Stop();
			Debug.Log(sw.ElapsedMilliseconds);
		}

		if (Input.GetMouseButtonDown(1))
		{
			Stopwatch sw = new Stopwatch();
			for (int i = 0; i < count; i++)
				new GameObject("new obj");
			sw.Stop();
			Debug.Log(sw.ElapsedMilliseconds);
		}


	}

    void OnMouseUp()
    {
        Instantiate(obj);
    }
}
