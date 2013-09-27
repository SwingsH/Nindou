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
        control.ChangeGameState(GameConnectNone.Instance); //尚未連線狀態
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
}

// 尚未連線到 game server
public class GameConnectNone : IGameState
{
    private static GameConnectNone _instance = null;

    ~GameConnectNone()
    {
        _instance = null;
    }

    //game server 進入沒有連線階段, 顯示伺服器選擇
    public void OnChangeIn(GameControl control)
    {
    }

    public void Update(GameControl control)
    {
    }

    public static GameConnectNone Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameConnectNone();
            return _instance;
        }
    }
}

// game server 連線中, 等待回應
public class GameConnecting : IGameState
{
    private static GameConnecting _instance = null;

    ~GameConnecting()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
    }

    public void Update(GameControl control)
    {
        control.ChangeGameState(GameLogin.Instance);
    }

    public static GameConnecting Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameConnecting();
            return _instance;
        }
    }
}

// game server 已經連線, 尚未驗證登入
public class GameLogin : IGameState
{
    private static GameLogin _instance = null;

    ~GameLogin()
    {
        _instance = null;
    }

    public void OnChangeIn(GameControl control)
    {
    }
    public void Update(GameControl control)
    {
    }
    public static GameLogin Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameLogin();
            return _instance;
        }
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
        //進入遊戲, 下載玩家動作
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
}
