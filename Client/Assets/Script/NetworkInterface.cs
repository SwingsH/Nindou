using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public delegate void OnHTTPHandling(); // 等待 http server 回應中要處理的 method
public delegate void OnHTTPResponse(string responseText); // http server 回應後要處理的 method

public delegate void OnSocketHandling(); // 等待 socket server 回應中要處理的 method
public delegate void OnSocketResponse(MemoryStream Message); // socket server 回應後要處理的 method

struct HTTPProtocolEvent
{
    public OnHTTPHandling OnHandling;
    public OnHTTPResponse OnResponse;
}

struct SocketProtocolEvent
{
    public OnSocketHandling OnHandling;
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

    private const int SOCKET_PORT = 17480;
    private const string HTTP_HEAD = "http";
    private const string HTTP_IP = "127.0.0.1";
    private const int HTTP_PORT = 8000;

    private HTTPProtocolEvent[,] _httpProtocolEvents     = new HTTPProtocolEvent[PROTOCAL_KIND_MAX, PROTOCAL_SUBKIND_MAX];
    private SocketProtocolEvent[,] _socketProtocolEvents   = new SocketProtocolEvent[PROTOCAL_KIND_MAX, PROTOCAL_SUBKIND_MAX];

    private bool _socketEnable = false; // socket 功能目前預設不開啟
    private float _tryConnectTime = 0.0f;
    private static NetworkSocket _gameSocket = null;
    private static NetworkHTTP _gameHTTP = null;

    //constructor
    public NetworkInterface()
    {
        _gameSocket = new NetworkSocket(NetworkSocketBuffer.Identify);
        _gameHTTP = new NetworkHTTP();
        _gameHTTP.SetConfig( string.Format("{0}://{1}:{2}/protocol/", HTTP_HEAD, HTTP_IP, HTTP_PORT) );
        SetAllProtocolEvent();
    }

    // destructor
    ~NetworkInterface()
    {
    }

    // 批次加入所有協定設定
    private void SetAllProtocolEvent()
    {
        AddHTTPProtocol(PROTOCOL_KIND_LOGIN, 1, HTTPHandling_Login_1, HTTPResponse_Login_1);
    }

    /// <summary>
    /// 加入一組 HTTP 協定設定
    /// </summary>
    private void AddHTTPProtocol(int kind, int subKind, OnHTTPHandling onHandle, OnHTTPResponse onResponse)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            HTTPProtocolEvent eve = new HTTPProtocolEvent();
            eve.OnHandling = onHandle;
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
    private void AddSocketProtocol(int kind, int subKind, OnSocketHandling onHandle, OnSocketResponse onResponse)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            SocketProtocolEvent eve = new SocketProtocolEvent();
            eve.OnHandling = onHandle;
            eve.OnResponse = onResponse;
            _socketProtocolEvents[kind, subKind] = eve;
        }
        else
        {
            CommonFunction.DebugMsg(" AddHTTPProtocol faild");
        }
    }

    //加入一筆準備傳送 string 的資料
    // todo: fieldName 應該拿掉, 協定常常變動, 總不能一直取不一樣的 field name, 累死
    public void PushString(int kind, int subKind, string fieldName, string fieldvalue)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            CommonFunction.DebugMsg("PushField error.");
            return;
        }

        if (HTTPExist(kind, subKind))
        {
            NetworkHTTPBuffer.AddString(fieldName, fieldvalue);
        }
        else if (SocketExist(kind, subKind))
        {
            NetworkSocketBuffer.Encode_FromString(fieldvalue);
        }
    }

    //加入一筆準備傳送 int 的資料
    // todo: fieldName 應該拿掉, 協定常常變動, 總不能一直取不一樣的 field name, 累死
    public void PushInteger(int kind, int subKind, string fieldName, int fieldvalue)
    {
        if (!HTTPExist(kind, subKind) && !SocketExist(kind, subKind))
        {
            CommonFunction.DebugMsg("PushField error.");
            return;
        }

        if (HTTPExist(kind, subKind))
        {
            NetworkHTTPBuffer.AddInteger(fieldName, fieldvalue);
        }
        else if (SocketExist(kind, subKind))
        {
            NetworkSocketBuffer.Encode_FromUInt( Convert.ToUInt32( fieldvalue ) );
        }
    }

    /// <summary>
    /// HTTP 協定是否設定過
    /// </summary>
    private bool HTTPExist(int kind, int subKind)
    {
        if ((_httpProtocolEvents[kind, subKind].OnHandling != null) && (_httpProtocolEvents[kind, subKind].OnResponse != null))
            return true;
        return false;
    }

    /// <summary>
    /// Socket 協定是否設定過
    /// </summary>
    private bool SocketExist(int kind, int subKind)
    {
        if ((_socketProtocolEvents[kind, subKind].OnHandling != null) && (_socketProtocolEvents[kind, subKind].OnResponse != null))
            return true;
        return false;
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
    /// 處理收到的 HTTP 
    /// </summary>
    private void HandleHTTPProtocal(int kind, int subKind, string responseText)
    {
        if (!SocketExist(kind, subKind))
        {
            CommonFunction.DebugError(string.Format("Socket {0}  {1} 不存在", kind, subKind));
            return;
        }

        HTTPProtocolEvent eve = _httpProtocolEvents[kind, subKind];
        eve.OnResponse(responseText);
    }

    /// <summary>
    /// 主動執行斷線, 釋放資源
    /// </summary>
    public void DoDisconnect()
    {
        _tryConnectTime = 0.0f;
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
    private void SendToSocketServer(byte mainKind, byte iSubKind)
    {
        if (!_gameSocket.Enable())
            return;

        NetworkSocketBuffer.Packaging(mainKind, iSubKind);
        _gameSocket.Send(NetworkSocketBuffer.EncodeStream);
        NetworkSocketBuffer.ClearSendBuffer();
    }

    /// <summary>
    /// 送出資料給 http server
    /// </summary>
    private void SendToHTTPServer(byte mainKind, byte iSubKind)
    {
        CommonFunction.DebugMsg("SendToHTTPServer");

        NetworkHTTPBuffer.Packaging(mainKind, iSubKind);
        _gameHTTP.Send(NetworkHTTPBuffer.Form);
        //NetworkHTTPBuffer.ClearSendBuffer();
    }

    // 協定集中處理區
    #region Protocol Event

    private void HTTPHandling_Login_1()
    {
        //show 個 loading bar
    }

    private void HTTPResponse_Login_1(string responseText)
    {
        CommonFunction.DebugMsg(responseText);
    }

    #endregion
}