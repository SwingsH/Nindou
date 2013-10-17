using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HTTP 協定資料  send & receive  緩衝區
/// </summary>
public class NetworkHTTPBuffer
{
    private static WWWForm _currentPostForm; //目前即將用 POST method 傳送給 server 的資料
    private static int _currentMainKind = 0;  //目前即將用 POST method 傳送給 server 的資料
    private static int _currentSubKind = 0;   //目前即將用 POST method 傳送給 server 的資料

    // Debug 用的 get url
    private static Dictionary<string, string> _debugGETURL = new Dictionary<string, string>();

    public static void Packaging(int serial, int kind, int subKind)
    {
         if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentMainKind = kind;
        _currentSubKind = subKind;
        _currentPostForm.AddField("mainkind", _currentMainKind); //post var is case sensitive !!
        _currentPostForm.AddField("subkind", _currentSubKind);
        _currentPostForm.AddField("serial", serial);

        _debugGETURL.Add("mainkind", _currentMainKind.ToString() );
        _debugGETURL.Add("subkind", _currentSubKind.ToString() );
        _debugGETURL.Add("serial", serial.ToString());
    }

    public static void ClearSendBuffer()
    {
        _currentPostForm = null;
        _debugGETURL.Clear();
        _debugGETURL = new Dictionary<string, string>();
    }

    public static void AddString(string fieldName, string fieldvalue)
    {
        if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentPostForm.AddField(fieldName, fieldvalue);
        _debugGETURL.Add(fieldName, fieldvalue);
    }

    public static void AddInteger(string fieldName, int fieldvalue)
    {
         if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentPostForm.AddField(fieldName, fieldvalue);
        _debugGETURL.Add(fieldName, fieldvalue.ToString());
    }

    public static WWWForm Form
    {
        get
        {
            return _currentPostForm;
        }
    }

    /// <summary>
    /// 回傳方便 Debug 用的 GET method URL
    /// </summary>
    public static string DumpDebugURL()
    {
        string paramString = string.Empty;
        int i = 0;
        int count = _debugGETURL.Count;
        foreach (KeyValuePair<string, string> item in _debugGETURL)
        {
            paramString = paramString + string.Format("{0}={1}", item.Key, item.Value);
            i++;
            if (i < count)
                paramString = paramString + "&";
        }

        return paramString;
    }
}