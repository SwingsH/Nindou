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

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
        control.DownloadUpdateInfo();
    }

    public void Update(GameControl control)
    {
        if (!control.IsUpdateInfoReady) //更新資訊尚未取得
            return;
        if (control.IsNeedToUpdate) // 需要更新, 切換至更新狀態
            control.ChangeGameState(GameResourceUpdating.Instance);
        else // 不需要更新, 切換至 尚未登入狀態
            control.ChangeGameState(GameLoginNone.Instance);
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

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
    }

    public void Update(GameControl control)
    {
        if (!control.IsUpdateFinished)
            return;
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
