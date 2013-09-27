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
    private IGameState _gameState = null; // 遊戲進行狀態
    private string _deviceID = string.Empty;

    private GameControl(GameMain main)
    {
        _main = main;
        _networkInterface = new NetworkInterface();

        //a36ec54e961ee79e8d92247f8a081b47a4c52e55
        _deviceID = SystemInfo.deviceUniqueIdentifier;
    }
	
	public void test()
	{
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

    // 一般登入
    public void DoLogin()
    {
        CommonFunction.DebugMsg(_deviceID);
        _networkInterface.PushString(1, 1, "DeviceID", _deviceID);
        _networkInterface.Send(1, 1);
    }

    public void ChangeGameState(IGameState newState)
    {
        if (newState == CurrentState)
            return;
        CommonFunction.DebugMsg(string.Format("GameState Changed. {0}--->{1}", CurrentState, newState));
        _gameState = null;
        _gameState = newState;
        _gameState.OnChangeIn(this);
    }

    //取得遊戲狀態
    public IGameState CurrentState
    {
        get { return _gameState; }
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
