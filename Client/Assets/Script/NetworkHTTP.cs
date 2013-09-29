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
		CommonFunction.DebugMsg(_currentURL);
		//_currentURL = "http://tw.knowledge.yahoo.com/question/question?qid=1610082401742";
        _currentWWW = new WWW(_currentURL, postForm);
        //_currentWWW = new WWW(_currentURL);

        while (!_currentWWW.isDone)
        {
            yield return null;
        }

        _responseMethod.Invoke(_currentWWW.text);
        yield break;
    }
}