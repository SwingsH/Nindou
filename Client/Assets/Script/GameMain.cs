using UnityEngine;
using System.Collections;

/// <summary>
/// Client 端進入點, 只有 GameLaunch.unity 場景能掛載此 Script 
/// 不用 instance 存取 GameMain(MonoBehaviour), 這樣流程只被 unity 得知
/// 可以透過 game control, 才利於掌控code流程
/// </summary>
public class GameMain : MonoBehaviour {
    private GameControl _control = null;


	// Use this for initialization
	void Start () {
        _control = GameControl.Instance; // initialize
		//_control.test();
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (_control != null)
            _control.Update();
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Protocol Test"))
        {
            _control.DoLogin();
        }
    }
}
