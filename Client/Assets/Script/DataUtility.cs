﻿using UnityEngine;
using System.Collections;


/// <summary>
/// 負責資料轉換的class
/// </summary>
public class DataUtility
{
    /// <summary>
    /// 將encoding後的字串所含有的資料反序列化存入refObj
    /// </summary>
    /// <returns>是否反序列化成功</returns>
    public static bool DeserializeObject(string encodingString, ref object refObj)
    {
        try
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            settings.CheckAdditionalContent = false;
            Newtonsoft.Json.JsonConvert.PopulateObject(encodingString, refObj); // 將encoding的資料填充到refObj內
            return true;
        }
        catch (System.Exception e)
        {
            CommonFunction.DebugMsgFormat("JSONDeserializeObject error!(type = {0})\n{1}\n{2}\n", refObj.GetType().ToString(), e.Message, e.StackTrace);
            return false;
        }
    }

}
