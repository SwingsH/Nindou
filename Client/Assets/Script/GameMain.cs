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
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (_control != null)
            _control.Update();
	}

    void OnGUI()
    {
        
        // todo: 此區是 NGUI 未完成前暫代
        if (_control.CurrentGameState != null)
        {
            if (_control.CurrentGameState == GameLoginNone.Instance)
            {
                if (GUI.Button(new Rect((Screen.width - 100) / 2, (Screen.height - 40) / 2, 100, 40), GLOBAL_STRING.UI_BUTTON_1))
                {
                    _control.DoLogin();
                }
            }
        }
		#if UNITY_EDITOR
		if (_control.CurrentGameState != null)
			GUI.Box(new Rect((Screen.width - 200), 0, 200, 30), _control.CurrentGameState.ToString());
		#endif
		
    }
}
