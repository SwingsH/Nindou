using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;

/// <summary>
/// 共用method的class
/// </summary>
public static class CommonFunction
{
    #region Debug相關
    /// <summary>
    /// 輸出Debug訊息
    /// </summary>
    /// <param name="msg">待輸出的訊息</param>
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
    /// <summary>
    /// 輸出Error訊息
    /// </summary>
    /// <param name="errorMsg">待輸出的訊息</param>
    public static void DebugError(string errorMsg)
    {
        Debug.LogError(errorMsg);
    }

    /// <summary>
    /// 依照string.Format的格式輸入參數，印出由string.Format回傳的錯誤訊息
    /// </summary>
    public static void DebugErrorFormat(string format, params object[] args)
    {
        Debug.LogError(string.Format(format, args));
    }
    #endregion

    #region 轉換成字串
    /// <summary>
    /// 將dataList的內容轉換成字串，方便輸出
    /// </summary>
    /// <param name="dataList">待轉換成字串的List</param>
    /// <param name="listName">List前面要加的名字</param>
    /// <param name="firstIndex">第一筆資料的index</param>
    /// <returns>轉換後的字串</returns>
    public static string ListDataToString(IList dataList, string listName, int firstIndex = 0)
    {
        StringBuilder sb = new StringBuilder();
        if (dataList != null && dataList.Count > 0)
        {
            for (int index = firstIndex; index < dataList.Count; ++index)
            {
                sb.AppendFormat("{0}[{1}] =\n{2}", listName, index, dataList[index]);
            }
            sb.Append("********************************\n");
        }
        return sb.ToString();
    }
    #endregion

    #region 列舉相關
    /// <summary>
    /// 取得Enum的Attribute
    /// </summary>
    /// <typeparam name="T">要取得的Attribute型別</typeparam>
    /// <param name="value">列舉值</param>
    /// <param name="outAttr">輸出的Attribute</param>
    /// <returns>是否有成功取得</returns>
    public static bool GetAttribute<T>(System.Enum value, out T outAttr) where T : System.Attribute
    {
        outAttr = default(T);
        System.Type curType = value.GetType();
        System.Reflection.FieldInfo curFieldInfo = curType.GetField(value.ToString());
        if (curFieldInfo != null)
        {
            T[] curAttrs = curFieldInfo.GetCustomAttributes(typeof(T), false) as T[];
            if (curAttrs != null && curAttrs.Length > 0)
            {
                outAttr = curAttrs[0];
                return true;
            }
        }
        return false;
    }
    #endregion

    #region 路徑取得相關
    /// <summary>
    /// 取得專案絕對路徑（若在非Editor環境下，則回傳空字串）
    /// </summary>
    /// <returns>專案路徑</returns>
    public static string GetProjectPath()
    {
        if (!Application.isEditor) { return string.Empty; }
        StringBuilder result = new StringBuilder(Application.dataPath);
        result.Replace("/Assets", "/");

        if (!result.ToString().EndsWith(Path.AltDirectorySeparatorChar.ToString())) { result.Append(Path.AltDirectorySeparatorChar); }
        return result.ToString();
    }
    /// <summary>
    /// 取得專案輸出的絕對路徑（若在非Editor環境下，回傳空字串）
    /// </summary>
    /// <param name="relatedPath">相對於專案輸出路徑的路徑</param>
    /// <returns>relatedPath對應的絕對路徑</returns>
    public static string GetProjectOutputPath(string relatedPath)
    {
        if (!Application.isEditor) { return string.Empty; }
        CreateAssetBundleOutputDir();

        StringBuilder result = new StringBuilder(GetProjectPath()); // 專案資料夾
        result.Append(GLOBALCONST.DIR_EDITOR_OUTPUT); // 專案資料夾/NindouOutput/
        result.Append(relatedPath);

        if (!Directory.Exists(result.ToString()))
        {
            Directory.CreateDirectory(result.ToString());
            DebugMsgFormat("Editor 建立資料夾 : {0}", relatedPath);
        }

        if (!result.ToString().EndsWith(Path.AltDirectorySeparatorChar.ToString())) { result.Append(Path.AltDirectorySeparatorChar); }
        return result.ToString();        
    }
    /// <summary>
    /// 建立Assetbundl檔案輸出的Root資料夾（專案資料夾/NindouOutput/assetbundle/）
    /// </summary>
    public static void CreateAssetBundleOutputDir()
    {
        if (!Application.isEditor) { return; }
        try
        {
            string path = GetProjectPath() + GLOBALCONST.DIR_EDITOR_OUTPUT;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                DebugMsgFormat("Editor 建立資料夾 : {0}", path);
            }
            path = path + GLOBALCONST.DIR_ASSETBUNDLE;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                DebugMsgFormat("Editor 建立資料夾 : {0}", path);
            }
        }
        catch (System.Exception e)
        {
            DebugMsgFormat("建立AB輸出資料夾錯誤 : {0}", e.Message);
        }
    }
    #endregion

}
