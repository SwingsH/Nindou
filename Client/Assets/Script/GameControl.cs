﻿using UnityEngine;
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
    private ResourceStation _resource = null;
    private string _deviceID = string.Empty;
    private string _loginSession = string.Empty;

    private GameControl(GameMain main)
    {
        _main = main;
        _gameState = GameEmpty.Instance;
        _networkInterface = new NetworkInterface(this);
        _resource = new ResourceStation();

        //a36ec54e961ee79e8d92247f8a081b47a4c52e55
        _deviceID = SystemInfo.deviceUniqueIdentifier;
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
        if (_gameState != null)
            _gameState.Update(this);
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
        if (newState == CurrentGameState)
            return;
        CommonFunction.DebugMsg(string.Format("GameState Changed. {0}--->{1}", CurrentGameState, newState));
        _gameState = null;
        _gameState = newState;
        _gameState.OnChangeIn(this);
    }

    //取得遊戲狀態
    public IGameState CurrentGameState
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

    /// <summary>
    /// 留存 login session
    /// todo: 改 json 版本
    /// </summary>
    public void SetLoginSession(string session)
    {
        CommonFunction.DebugMsg("留存 login session : " + session);
        _loginSession = session;
    }

    /// <summary>
    /// 從 file server 下載更新資料
    /// </summary>
    public void DownloadUpdateInfo()
    {
        //todo: 當然是 todo, 接 ResourceUpdater
    }

    /// <summary>
    /// 更新資料比對檔是否 下載完成
    /// </summary>
    /// <returns></returns>
    public bool IsUpdateInfoReady
    {
        //todo: 當然是 todo, 接 ResourceUpdater
        get
        {
            return true;
        }
    }

    /// <summary>
    /// 比對檔 下載完成 檢查是否需要更新
    /// </summary>
    /// <returns></returns>
    public bool IsNeedToUpdate
    {
        get
        {
            CommonFunction.DebugMsg("沒有檔案需要更新");
            return _resource.IsNeedToUpdate; //todo: 當然是 todo, 接 ResourceUpdater
        }
    }

    /// <summary>
    /// 檔案更新是否結束
    /// </summary>
    public bool IsUpdateFinished
    {
        get
        {
            return true; //todo: 當然是 todo, 接 ResourceUpdater
        }
    }

    /// <summary>
    /// 是否取有效的 登入 session
    /// </summary>
    public bool IsLoginSessionValid
    {
        get
        {
            if (_loginSession == string.Empty)
                return false;
            return true; //todo: 當然是 todo, 接 ResourceUpdater
        }
    }
}
