using UnityEngine;
using System.Collections;

public delegate void ResponseMethod(string responseText); // 所有資料的回應處理 method

/// <summary>
/// HTTP Request & Recieve 連線方式處理, 該 class 只能被 NetworkInterface 使用
/// 主要使用 Unity WWW
/// </summary>
public class NetworkHTTP
{
    private WWW _currentWWW = null;
    private string _currentURL = string.Empty;
    private ResponseMethod _responseMethod = null;

    //constructor
    public NetworkHTTP(ResponseMethod target)
    {
        _responseMethod = target;
    }

    // destructor
    ~NetworkHTTP()
    {
    }

    /// <summary>
    /// 設置協定網址
    /// </summary>
    public void SetConfig(string url)
    {
        _currentURL = url;
    }

    public void Send(WWWForm postForm)
    {
        GameControl.Instance.StartCoroutine(SendAndWait(postForm));
    }

    private IEnumerator SendAndWait(WWWForm postForm)
    {
        _currentWWW = new WWW(_currentURL, postForm);

        while (!_currentWWW.isDone)
        {
            yield return null;
        }

        _responseMethod.Invoke(_currentWWW.text);
		
		_currentWWW.Dispose();
		_currentWWW = null;

        yield break;
    }
}