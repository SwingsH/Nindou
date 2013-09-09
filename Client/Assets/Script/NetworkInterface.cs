using UnityEngine;
using System.Collections;

/// <summary>
/// 網路相關功能介面, 需要網路功能的透過此 class 可以不用處理 連線方式的差異 ex:
/// 1. WWW (http request)
/// 2. socket connect (後續聊天功能可能會用到)
/// </summary>
public class NetworkInterface
{

    //constructor
    public NetworkInterface()
    {
    }

    // destructor
    ~NetworkInterface()
    {
    }
}