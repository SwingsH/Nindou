using UnityEngine;
using System.Collections;

/// <summary>
/// HTTP Request & Recieve 連線方式處理, 該 class 只能被 NetworkInterface 使用
/// 主要使用 Unity WWW
/// </summary>
public class NetworkHTTP
{
    private WWW _currentWWW = null;
    private string _currentURL = string.Empty;

    //constructor
    public NetworkHTTP()
    {
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

        while (!_currentWWW.isDone)
        {
            yield return null;
        }

        CommonFunction.DebugMsg(_currentWWW.text);
        yield break;
    }
}