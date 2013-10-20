/// <summary>
///  * State Pattern for gameplay state
///  遊戲進行狀態 Interface declaration.
///  特定狀態進行的功能盡量寫在此處, 不要在其他的class撰寫 if (GameState==xxx) 相關的敘述
/// </summary>

using System.Collections.Generic;

public interface IGameState
{
    void OnChangeIn(GameControl control); // 切換狀態後, 只做一次的 method
    void Update(GameControl control); // 切換狀態後, 持續執行的 method
	void OnChangeOut(GameControl control); // 切換成其他狀態後, 只做一次的 method
}

// 什麼都沒有
public class GameEmpty : IGameState
{
    private static GameEmpty _instance = null;

    ~GameEmpty()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
    }
    public void Update(GameControl control)
    {
        control.ChangeGameState(GameDetectUpdate.Instance); //尚未連線狀態
    }
    public static GameEmpty Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameEmpty();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}

// 檢查是否有檔案更新
public class GameDetectUpdate : IGameState
 {
    private static GameDetectUpdate _instance = null;

    ~GameDetectUpdate()
    {
        _instance = null;
    }

    //sh131019 marked, 轉接到正式流程, remove shortly
    //bool _canChangeState = false; // fs : 此處以點擊畫面後才能轉換狀態（開始更新or登入）的作法

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        control.DownloadUpdateInfo();
        // fs: Show開始介面
        control.GUIStation.ShowAndHideOther(typeof(UI_Start));
        NetworkConnectTester.StartTest(ConnectType.HTTP, NetworkInterface.HTTP_PORT); //開始網路測試
    }

    public void Update(GameControl control)
    {
        if (!control.IsUpdateInfoReady) //更新資訊尚未取得
            return;

        NetworkConnectTester.Update();
        if (NetworkConnectTester.Status != TestStatus.Done)
            return;
        NetworkConnectTester.EndTest(); //關閉持續測試連線
        if (NetworkConnectTester.ServerConnectCapability == false) // 網路無法連線
        {
            control.GUIStation.Form<UI_Dialog_Simple>().ShowMessage(GLOBAL_STRING.DIALOG_NETWORK_FAILED, NetworkConnectTester.RetryTest);
        }
        else // 網路連線正常
        {
            control.GUIStation.Form<UI_Start>().LoginBtnClick = null;
            if (control.IsNeedToUpdate) // 需要更新, 切換至更新狀態
            {
                control.ChangeGameState(GameReadyToUpdate.Instance);
            }
            else // 不需要更新, 切換至 尚未登入狀態
            {
                control.ChangeGameState(GameLoginNone.Instance);
            }
        }
    }

    // sh131019 marked, 轉接到正式流程, remove shortly
    /// <summary>
    /// 暫時用，讓GameDetectUpdate知道已發生「使用者按下背景的事件」
    /// </summary>
    //public void StartLogin()
    //{
    //    _canChangeState = true;
    //}

    public static GameDetectUpdate Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameDetectUpdate();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}

// 進行檔案更新
public class GameReadyToUpdate : IGameState
{
    private static GameReadyToUpdate _instance = null;
    private static GameControl _control;

    ~GameReadyToUpdate()
    {
        _instance = null;
    }

    // fs : 此處先暫代，進度的資訊應該由GameControl那邊提供
    float progressPercent = 0.0f;

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        _control = control;
        control.GUIStation.Form<UI_Start>().ShowNeedUpdateMode();
        control.GUIStation.Form<UI_Start>().LoginBtnClick = GameReadyToUpdate.Change;
        // fs: 設定進度為0
        control.GUIStation.Form<UI_Start>().progressPercent = progressPercent;
    }

    private static void Change()
    {
        _control.ChangeGameState(GameResourceUpdating.Instance); //切換到登入狀態
    }

    public void Update(GameControl control)
    {
    }

    public static GameReadyToUpdate Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameReadyToUpdate();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}

// 進行檔案更新
public class GameResourceUpdating : IGameState
{
    private static GameResourceUpdating _instance = null;

    ~GameResourceUpdating()
    {
        _instance = null;
    }

    // fs : 此處先暫代，進度的資訊應該由GameControl那邊提供
    float progressPercent = 0.0f;

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        control.StartUpdateFiles();
        control.GUIStation.Form<UI_Start>().ShowUpdatingMode();
        control.GUIStation.Form<UI_Start>().LoginBtnClick = null; //更新中不能亂點唷~
        // fs: 設定進度為0
        control.GUIStation.Form<UI_Start>().progressPercent = progressPercent;
    }

    public void Update(GameControl control)
    {
        // fs : 此處先暫代，應該由control提供目前進度
        if (progressPercent < 100)
        //if (!control.IsUpdateFinished)
        {
            // fs: 將進度百分比送給介面設定
            progressPercent += 1;
            control.GUIStation.Form<UI_Start>().progressPercent = progressPercent;
            return;
        }
        // 更新 over, 切換至 尚未登入狀態
        control.ChangeGameState(GameLoginNone.Instance);
    }

    public static GameResourceUpdating Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameResourceUpdating();
            return _instance;
        }
    }

    public void OnChangeOut(GameControl control)
    {
        //throw new System.NotImplementedException();
    }
}


// ftp檔案已經就緒, 尚未從 game server 取得 login session
public class GameLoginNone : IGameState
{
    private static GameLoginNone _instance = null;
    private static GameControl _control;

    ~GameLoginNone()
    {
        _instance = null;
    }

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        _control = control;
        control.GUIStation.Form<UI_Start>().ShowReadyEnterGame();
        control.GUIStation.Form<UI_Start>().LoginBtnClick = GameLoginNone.Change;

        // fs: 隱藏進度條
        control.GUIStation.Form<UI_Start>().SetProgressVisible(false);
    }

    public static void Change()
    {
        _control.ChangeGameState(GameLoging.Instance); //切換到登入狀態
    }

    public void Update(GameControl control)
    {

    }

    public static GameLoginNone Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameLoginNone();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}

/// <summary>
/// 送資訊給 server 登入中
/// </summary>
public class GameLoging : IGameState
{
    private static GameLoging _instance = null;

    ~GameLoging()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
        #if SKIP_CONNECT_CHECK
            control.ChangeGameState(GameEntered.Instance);
            return;
        #endif
        control.DoLogin();
    }
    public void Update(GameControl control)
    {
        if (control.AccountValid == AccountValidStatus.Unchecked)
            return;

        if (control.AccountValid == AccountValidStatus.Valid)//帳號存在
        {
            control.ChangeGameState(GameEntered.Instance);
            return;
        }

        if (control.AccountValid == AccountValidStatus.Invalid) //帳號不存在
        {
            control.ChangeGameState(GameCreatePlayer.Instance);
            return;
        }
    }
    public static GameLoging Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameLoging();
            return _instance;
        }
    }

    public void OnChangeOut(GameControl control)
    {
        //throw new System.NotImplementedException();
    }
}
// game server 沒有帳號資料, 建立帳號
public class GameCreatePlayer : IGameState
{
    private static GameCreatePlayer _instance = null;

    ~GameCreatePlayer()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
        //進入遊戲
        control.GUIStation.ShowAndHideOther(typeof(UI_Start_CreatePlayer));
        control.GUIStation.Form<UI_Start_CreatePlayer>().SetClickEvent( control.DoCreatePlayer );
    }
    public void Update(GameControl control)
    {
    }
    public static GameCreatePlayer Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameCreatePlayer();
            return _instance;
        }
    }

    public void OnChangeOut(GameControl control)
    {
        //throw new System.NotImplementedException();
    }
}

// game server 已經登入, 遊戲中
public class GameEntered : IGameState
{
    private static GameEntered _instance = null;

    ~GameEntered()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
        //進入遊戲
        control.GUIStation.ShowAndHideOther(typeof(UI_Main_WorldMap));
    }
    public void Update(GameControl control)
    {
    }
    public static GameEntered Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameEntered();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}

/// <summary>
/// 選擇關卡的狀態，此為了讓從戰鬥結束後回到開啟關卡選擇介面
/// </summary>
public class GameStageSelect : IGameState
{
    private static GameStageSelect _instance = null;

    ~GameStageSelect()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
        // 開啟選關卡介面
        control.GUIStation.ShowAndHideOther(typeof(UI_Main_StageSelect));
    }
    public void Update(GameControl control)
    {
    }
    public static GameStageSelect Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStageSelect();
            return _instance;
        }
    }

	public void OnChangeOut(GameControl control)
	{
		//throw new System.NotImplementedException();
	}
}