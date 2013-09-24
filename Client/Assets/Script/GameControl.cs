using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 遊戲邏輯主控台, 各系統溝通銜接口
/// </summary>
public class GameControl{
    private static GameControl _instance = null;
    private GameMain _main = null;
    private NetworkInterface _networkInterface = null;

    private GameControl(GameMain main)
    {
        _main = main;
    }
	
	public void test()
	{
	        _networkInterface = new NetworkInterface();

        _networkInterface.PushString(1, 1, "ss", "aa");
		
		CommonFunction.DebugMsg("PushString");
        _networkInterface.Send(1, 1);	
	}

    public static GameControl Instance
    {
        get
        {
            if (_instance == null)
            {
                GameMain main = GameObject.FindObjectOfType(typeof(GameMain)) as GameMain;
                _instance = new GameControl(main);
            }

            return _instance;
        }
    }

    // update per-frame
    public void Update()
    {
    }

    /// <summary>
    /// 為了掌握整個遊戲 Coroutine 使用量, 請集中使用此 method 進行 Coroutine
    /// todo: 可以考慮使用 CoroutineManager
    /// </summary>
    public void StartCoroutine(IEnumerator routine)
    {
        _main.StartCoroutine(routine);
    }
}
