using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public delegate void OnHTTPWaiting(); // 等待 http server 回應中要處理的 method
public delegate void OnHTTPResponse(HTTPResponse responsePack); // http server 回應後要處理的 method

public delegate void OnSocketWaiting(); // 等待 socket server 回應中要處理的 method
public delegate void OnSocketResponse(MemoryStream Message); // socket server 回應後要處理的 method

struct HTTPProtocolEvent
{
    public OnHTTPWaiting OnWaiting;
    public OnHTTPResponse OnResponse;
}

struct SocketProtocolEvent
{
    public OnSocketWaiting OnWaiting;
    public OnSocketResponse OnResponse;
}

/// <summary>
/// 網路相關功能介面, 需要網路功能的透過此 class 可以不用處理 連線方式的差異 ex:
/// 1. WWW (http request)
/// 2. socket connect (後續聊天功能可能會用到)
/// </summary>
public class NetworkInterface
{
    private const int PROTOCAL_KIND_MAX = 255;
    private const int PROTOCAL_SUBKIND_MAX = 255;

    // HTTP 協定相關 (Server to Client) & (Client to Server)
    public const byte PROTOCOL_KIND_LOGIN = 1;  // 登入相關
    public const byte PROTOCOL_KIND_SYSTEM = 2; // 系統訊息
    public const byte PROTOCOL_KIND_PLAYER = 3; // 玩家資料
    public const byte PROTOCOL_KIND_CARD = 4;   // 卡片相關
    public const byte PROTOCOL_KIND_ITEM = 5;   // 道具相關    
    public const byte PROTOCOL_KIND_BATTLE = 6; // 戰鬥相關
    public const byte PROTOCOL_KIND_COMBAT = 7; //普攻戰鬥
    public const byte PROTOCOL_KIND_MISSION = 8;//偵查進行
    public const byte PROTOCOL_KIND_SHOP = 9;   //商店購物
    public const byte PROTOCOL_KIND_CASH = 10;  //點數購物
    public const byte PROTOCOL_KIND_RANK = 11;  //排行榜
    public const byte PROTOCOL_KIND_FRIEND = 12;  //好友相關

    // Socket 協定相關 (Server to Client)
    public const byte PROTOCOL_KIND_UNKNOW = 101;

    public const int HTTP_PORT = 80;
    public const string HTTP_IP = "122.116.24.125";
    private const int SOCKET_PORT = 17480;
    private const string HTTP_HEAD = "http";
    private const string HTTP_PROTOCOL_PAGE = "{0}://{1}/nindou/protocol.php";

    private const string HTTP_FIELD_STR = "s{0}"; // http post form field, string 名稱
    private const string HTTP_FIELD_INT = "i{0}"; // http post form field, int 名稱

    private HTTPProtocolEvent[,] _httpProtocolEvents     = new HTTPProtocolEvent[PROTOCAL_KIND_MAX, PROTOCAL_SUBKIND_MAX];
    private SocketProtocolEvent[,] _socketProtocolEvents   = new SocketProtocolEvent[PROTOCAL_KIND_MAX, PROTOCAL_SUBKIND_MAX];

    private bool _socketEnable = false; // socket 功能目前預設不開啟
    private static NetworkSocket _gameSocket = null;
    private static NetworkHTTP _gameHTTP = null;
    private GameControl _control = null;

    private byte _currentHTTPKind = 0;
    private byte _currentHTTPSubKind = 0;
    private byte _currentStringNums = 0; // 目前準備送出的 string 數量
    private byte _currentIntegerNums = 0; // 目前準備送出的 int 數量
    private int _sendSerial = 1; //送出資料序列號
    private HTTPResponseMixDatas _currentResponse = null;

    //constructor
    public NetworkInterface(GameControl control)
    {
        _control = control;
        _gameSocket = new NetworkSocket(NetworkSocketBuffer.Identify);
        _gameHTTP = new NetworkHTTP( OnHTTPReceive);
        _gameHTTP.SetConfig(string.Format(HTTP_PROTOCOL_PAGE, HTTP_HEAD, HTTP_IP)); //設置協定網址
        SetAllProtocolEvent();
    }

    // destructor
    ~NetworkInterface()
    {
    }

    // 批次加入所有協定設定
    private void SetAllProtocolEvent()
    {
        //S: 1-1 登入
        AddHTTPProtocol(PROTOCOL_KIND_LOGIN, 1, HTTPHandling_Login_1, HTTPResponse_Login_1);
    }

    /// <summary>
    /// 加入一組 HTTP 協定設定
    /// </summary>
    private void AddHTTPProtocol(int kind, int subKind, OnHTTPWaiting onWait, OnHTTPResponse onResponse)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            HTTPProtocolEvent eve = new HTTPProtocolEvent();
            eve.OnWaiting = onWait;
            eve.OnResponse = onResponse;
            _httpProtocolEvents[kind, subKind] = eve;
        }
        else
        {
            CommonFunction.DebugMsg(" AddHTTPProtocol faild");
        }
    }

    /// <summary>
    /// 加入一組 Socket 協定設定
    /// </summary>
    private void AddSocketProtocol(int kind, int subKind, OnSocketWaiting onWait, OnSocketResponse onResponse)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            SocketProtocolEvent eve = new SocketProtocolEvent();
            eve.OnWaiting = onWait;
            eve.OnResponse = onResponse;
            _socketProtocolEvents[kind, subKind] = eve;
        }
        else
        {
            CommonFunction.DebugMsg(" AddHTTPProtocol faild");
        }
    }

    /// <summary>
    /// 加入一筆準備傳送 string 的資料
    /// </summary>
    public void PushString(int kind, int subKind, string fieldvalue)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            CommonFunction.DebugMsg("PushField error.");
            return;
        }

        if (HTTPExist(kind, subKind))
        {
            _currentStringNums++;
            string fieldName = string.Format(HTTP_FIELD_STR, _currentStringNums);
            NetworkHTTPBuffer.AddString(fieldName, fieldvalue);
        }
        else if (SocketExist(kind, subKind))
        {
            NetworkSocketBuffer.Encode_FromString(fieldvalue);
        }
    }

    /// <summary>
    /// 加入一筆準備傳送 int 的資料
    /// </summary>
    public void PushInteger(int kind, int subKind, int fieldvalue)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            CommonFunction.DebugMsg("PushField error.");
            return;
        }

        if (HTTPExist(kind, subKind))
        {
            _currentIntegerNums++;
            string fieldName = string.Format(HTTP_FIELD_INT, _currentIntegerNums);
            NetworkHTTPBuffer.AddInteger(fieldName, fieldvalue);
        }
        else if (SocketExist(kind, subKind))
        {
            NetworkSocketBuffer.Encode_FromUInt(Convert.ToUInt32(fieldvalue));
        }
    }

    /// <summary>
    /// HTTP 協定是否設定過
    /// </summary>
    private bool HTTPExist(int kind, int subKind)
    {
        if ((_httpProtocolEvents[kind, subKind].OnWaiting != null) && (_httpProtocolEvents[kind, subKind].OnResponse != null))
            return true;
        return false;
    }

    /// <summary>
    /// Socket 協定是否設定過
    /// </summary>
    private bool SocketExist(int kind, int subKind)
    {
        if ((_socketProtocolEvents[kind, subKind].OnWaiting != null) && (_socketProtocolEvents[kind, subKind].OnResponse != null))
            return true;
        return false;
    }

    /// <summary>
    /// http 處理完畢後回傳 
    /// </summary>
    public void OnHTTPReceive(string responseText)
    {
        if (_httpProtocolEvents[_currentHTTPKind, _currentHTTPSubKind].OnResponse != null)
        {
            object refObj = Activator.CreateInstance(typeof(HTTPResponseMixDatas));
            bool isSuccess = DataUtility.DeserializeObject(responseText, ref refObj);
            _currentResponse = refObj as HTTPResponseMixDatas;
            CommonFunction.DebugMsg(string.Format("OnHTTPReceive {0},{1},{2}", _currentHTTPKind, _currentHTTPSubKind, responseText));
        }
        else
        {
            CommonFunction.DebugMsg(" OnHTTPReceive ProtocolEvents null.");
        }

        // no Interface "Command Pattern"
        DispatchResponseHTTPCommands(_currentResponse);

        // clear previous
        _currentIntegerNums = 0;
        _currentStringNums = 0;
        NetworkHTTPBuffer.ClearSendBuffer();
    }

    /// <summary>

    /// http 處理中
    /// </summary>
    public void OnHTTPWait()
    {
        if (_httpProtocolEvents[_currentHTTPKind, _currentHTTPSubKind].OnWaiting != null)
            _httpProtocolEvents[_currentHTTPKind, _currentHTTPSubKind].OnWaiting();
    }


    /// <summary>
    /// 檢查 os network 緩衝區是否有 socket 準備接收的資料 
    /// </summary>
    public void UpdateSocketReceive()
    {
        if (!_socketEnable)
            return;

        MemoryStream stream = null;
        if (_gameSocket.Enable())
        {
            while ((stream = _gameSocket.PopMessage()) != null)
            {
                byte Main = NetworkSocketBuffer.Decode_ToByte(stream);
                byte Sub = NetworkSocketBuffer.Decode_ToByte(stream);

                if(!SocketExist(Main, Sub))
                {
                    CommonFunction.DebugError(string.Format("Socket {0}  {1} 不存在", Main, Sub));
                    return;
                }
                try
                {
                    HandleSocketProtocal(Main, Sub, stream);
                }
                catch (Exception e)
                {
                    CommonFunction.DebugError(string.Format("{0}\n{1}", e.Message, e.StackTrace));
                }
            }
        }
    }

    /// <summary>
    /// 處理收到的 socket 
    /// </summary>
    private void HandleSocketProtocal(int kind, int subKind, MemoryStream stream)
    {
        if (!SocketExist(kind, subKind))
        {
            CommonFunction.DebugError(string.Format("Socket {0}  {1} 不存在", kind, subKind));
            return;
        }

        SocketProtocolEvent eve = _socketProtocolEvents[kind, subKind];
        eve.OnResponse(stream);
    }

    /// <summary>
    /// 依據回應的綜合 http 資料, 帶有多個 command 指示, 來分派處理 method
    /// </summary>
    public void DispatchResponseHTTPCommands(HTTPResponseMixDatas mixDatas)
    {
        if (mixDatas == null)
        {
            CommonFunction.DebugMsg(" OnHTTPReceive HTTPResponseMixDatas null.");
            return;
        }
        if (mixDatas.Packages == null)
        {
            CommonFunction.DebugMsg(" OnHTTPReceive HTTPResponseMixDatas null.");
            return;
        }

        int mainKind = 0;
        int subKind = 0;
        for (int i = 0; i < mixDatas.Packages.Length; i++)
        {
            mainKind    = mixDatas.Packages[i].MainKind;
            subKind     = mixDatas.Packages[i].SubKind;
            if (_httpProtocolEvents[mainKind, subKind].OnResponse == null)
            {
                CommonFunction.DebugMsg( string.Format(" OnHTTPReceive ProtocolEvents {0},{1} null.", mainKind, subKind) );
                continue;
            }

            _httpProtocolEvents[mainKind, subKind].OnResponse(mixDatas.Packages[i]);
        }
    }

    /// <summary>
    /// 主動執行斷線, 釋放資源
    /// </summary>
    public void DoDisconnect()
    {
        _gameSocket.Disconnect();
    }

    public void ChangeGameServerConfig(string iConnIP)
    {
        _gameSocket.SetConnect(iConnIP, SOCKET_PORT);
    }

    public void UpdateSocket()
    {
        _gameSocket.Update();
    }

    public void Send(byte mainKind, byte subKind)
    {
        if (HTTPExist(mainKind, subKind))
        {
            SendToHTTPServer(mainKind, subKind);
        }
        else if (SocketExist(mainKind, subKind))
        {
            SendToSocketServer(mainKind, subKind);
        }
    }

    /// <summary>
    /// 送出資料給 socket server
    /// </summary>
    private void SendToSocketServer(byte mainKind, byte subKind)
    {
        if (!_gameSocket.Enable())
            return;

        NetworkSocketBuffer.Packaging(mainKind, subKind);
        _gameSocket.Send(NetworkSocketBuffer.EncodeStream);
        NetworkSocketBuffer.ClearSendBuffer();
    }

    /// <summary>
    /// 送出資料給 http server
    /// </summary>
    private void SendToHTTPServer(byte mainKind, byte subKind)
    {
        CommonFunction.DebugMsg("SendToHTTPServer");

        _currentHTTPKind = mainKind;
        _currentHTTPSubKind = subKind;
        NetworkHTTPBuffer.Packaging(_sendSerial, mainKind, subKind);

        CommonFunction.DebugMsg( " URL for debugging : " + string.Format(HTTP_PROTOCOL_PAGE, HTTP_HEAD, HTTP_IP) + "?" + NetworkHTTPBuffer.DumpDebugURL());
        _gameHTTP.Send(NetworkHTTPBuffer.Form);
        _sendSerial++;
        NetworkHTTPBuffer.ClearSendBuffer();
    }


    // 協定集中處理區
    #region Protocol Event
    //C: 1-1 登入 s1:deviceid
    private void HTTPHandling_Login_1()
    {
        //show 個 loading bar
    }

    //S: 1-1 登入, s1:session , i1:登入類型(0=登入失敗,1=新帳號登入,2=快速登入,3=更新session)
    private void HTTPResponse_Login_1(HTTPResponse responsePack)
    {
        string session = responsePack.PopString();
        int kind = responsePack.PopInteger();

        AccountData accountData = new AccountData();
        accountData.PlayerName = responsePack.PopString();

        CommonFunction.DebugMsg(string.Format("登入成功 : {0} , {1}", session, kind));
        _control.SetLoginSession(session);
        _control.SetAccountData(accountData);
    }

    #endregion
}