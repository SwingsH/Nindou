﻿using UnityEngine;
using System.Collections;

/// <summary>
/// HTTP 協定資料  send & receive  緩衝區
/// </summary>
public class NetworkHTTPBuffer
{
    private static WWWForm _currentPostForm; //目前即將用 POST method 傳送給 server 的資料
    private static int _currentMainKind = 0;  //目前即將用 POST method 傳送給 server 的資料
    private static int _currentSubKind = 0;   //目前即將用 POST method 傳送給 server 的資料

    public static void Packaging(int kind, int subKind)
    {
         if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentPostForm.AddField("MainKind", kind);
        _currentPostForm.AddField("SubKind", subKind);
    }

    public static void ClearSendBuffer()
    {
       _currentPostForm = null;
    }

    public static void AddString(string fieldName, string fieldvalue)
    {
        if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentPostForm.AddField(fieldName, fieldvalue);
    }

    public static void AddInteger(string fieldName, int fieldvalue)
    {
         if (_currentPostForm == null)
            _currentPostForm = new WWWForm();
        _currentPostForm.AddField(fieldName, fieldvalue);
    }

    public static WWWForm Form
    {
        get
        {
            return _currentPostForm;
        }
    }
}