using UnityEngine;
using System.Collections;

/// <summary>
/// 共用method的class
/// </summary>
public class CommonFunction
{
    public static void DebugMsg(string msg)
    {
        Debug.Log(msg);
    }

    /// <summary>
    /// 依照string.Format的格式輸入參數，印出由string.Format回傳的訊息
    /// </summary>
    public static void DebugMsgFormat(string format, params object[] args)
    {
        Debug.Log(string.Format(format, args));
    }

    public static void DebugError(string errorMsg)
    {
        Debug.LogError(errorMsg);
    }
}
