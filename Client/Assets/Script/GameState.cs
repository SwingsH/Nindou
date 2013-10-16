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

    bool _canChangeState = false; // fs : 此處以點擊畫面後才能轉換狀態（開始更新or登入）的作法

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        control.DownloadUpdateInfo();
        // fs: Show開始介面
        control.GUIStation.Form<UI_Start>().Show();
        control.GUIStation.Form<UI_Start>().LoginBtnClick = StartLogin;
    }

    public void Update(GameControl control)
    {
        if (!control.IsUpdateInfoReady) //更新資訊尚未取得
            return;
        if (_canChangeState)
        {
            control.GUIStation.Form<UI_Start>().LoginBtnClick = null;
            if (control.IsNeedToUpdate) // 需要更新, 切換至更新狀態            
                control.ChangeGameState(GameResourceUpdating.Instance);
            else // 不需要更新, 切換至 尚未登入狀態
                control.ChangeGameState(GameLoginNone.Instance);
        }
    }

    /// <summary>
    /// 暫時用，讓GameDetectUpdate知道已發生「使用者按下背景的事件」
    /// </summary>
    public void StartLogin()
    {
        _canChangeState = true;
    }


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

// 檔案已經就緒, 尚未從 game server 取得 login session
public class GameLoginNone : IGameState
{
    private static GameLoginNone _instance = null;

    ~GameLoginNone()
    {
        _instance = null;
    }

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        // fs: 隱藏進度條
        control.GUIStation.Form<UI_Start>().SetProgressVisible(false);
    }

    public void Update(GameControl control)
    {
        if (control.IsLoginSessionValid)
            control.ChangeGameState(GameEntered.Instance);
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
        //control.GUIStation.Form<UI_Start>().Hide();
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
        control.GUIStation.Form<UI_Start>().Hide();
        control.GUIStation.Form<UI_Main_WorldMap>().Show();

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
