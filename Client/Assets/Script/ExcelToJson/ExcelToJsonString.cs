using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

/// <summary>
/// 負責將table內容(一連串的string)轉換成json格式。
/// </summary>
public class ExcelToJsonString
{
    readonly Dictionary<Type, string> _baseTypeString = new Dictionary<Type, string>()
    {
        {typeof(byte), "BYTE"},
        {typeof(ushort), "USHORT"},
        {typeof(uint), "UINT"},
        {typeof(string), "STRING"},
    };

    string _debugMessage = string.Empty;
    ExcelToTable _excelToTable;

    public ExcelToJsonString()
    {
        _excelToTable = new ExcelToTable();
    }
    ~ExcelToJsonString()
    {
        _excelToTable = null;
    }

    #region 物件轉json字串
    /// <summary>
    /// 將object資料轉成Json字串
    /// </summary>
    /// <param name="ob">待轉換的object</param>
    /// <returns>對應的Json字串</returns>
    public string ObjectToJsonString(object ob)
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        settings.CheckAdditionalContent = false;
        return Newtonsoft.Json.JsonConvert.SerializeObject(ob, settings);
    }
    #endregion

    /// <summary>
    /// 讀取excel檔案，不論是否有錯誤，回傳前都會關閉檔案
    /// </summary>
    /// <param name="directoryPath">資料夾路徑</param>
    /// <param name="dataLoadTag">資料轉換的資訊</param>
    /// <param name="needReadSite">轉出資料是哪方（Server/Client）需要</param>
    /// <param name="jsonString">對應輸出的json字串</param>
    /// <param name="debugString">偵錯字串</param>
    /// <returns>可能有的錯誤訊息</returns>
    public ReadExcelToJsonStringError ReadExcelFile(string directoryPath, GLOBALCONST.DataLoadTag dataLoadTag, NeedReadSite needReadSite, out string jsonString, out string debugString)
    {
        string fileName = EnumClassValue.GetFileName(dataLoadTag);
        Type dataType = EnumClassValue.GetClassType(dataLoadTag);
        jsonString = null;
        debugString = _debugMessage;
        if (string.IsNullOrEmpty(fileName) || dataType == null) { return ReadExcelToJsonStringError.ENUM_ATTRIBUTE_ERROR; }

        ReadExcelToJsonStringError readExcelError = _excelToTable.OpenExcelFile(directoryPath, fileName);
        if (readExcelError != ReadExcelToJsonStringError.NONE)
        {
            _excelToTable.Close();
            debugString = _debugMessage;
            return readExcelError;
        }
        List<string> allType;
        readExcelError = _excelToTable.CheckAndReadTableHeader(needReadSite, out allType);
        if (readExcelError != ReadExcelToJsonStringError.NONE)
        {
            _excelToTable.Close();
            debugString = _debugMessage;
            return readExcelError;
        }
        #region 確認各欄位和要被寫入的物件欄位Type有對應
        object checkObject = Activator.CreateInstance(dataType);
        List<string>.Enumerator tableTypeEnumerator = allType.GetEnumerator();
        bool isConform = CheckObjectTypeCorrect(dataType, checkObject, ref tableTypeEnumerator);
        if (!isConform)
        {
            _debugMessage = string.Format("{0}{1} 轉換失敗：表格與資料結構({2})內容不符\n", _debugMessage, fileName, dataType);
            _excelToTable.Close();
            debugString = _debugMessage;
            return ReadExcelToJsonStringError.TABLE_TYPE_IS_NOT_CONFORM;
        }
        #endregion
        #region 抓取資料
        List<object> allData = new List<object>();

        bool hasEOR = false;

        List<string> tableRowData = _excelToTable.GetNextRow();
        while (tableRowData != null) // 還有資料
        {
            if (_excelToTable.CheckEndOfTable(tableRowData))
            { // 有結尾符號，正常結束
                hasEOR = true;
                break;
            }
            if (CheckEmptyRow(tableRowData))
            {
                _excelToTable.Close();
                debugString = _debugMessage;
                return ReadExcelToJsonStringError.HAS_EMPTY_ROW;
            }
            object obj = Activator.CreateInstance(dataType);
            List<string>.Enumerator rowDataEnumerator = tableRowData.GetEnumerator();
            ReadExcelToJsonStringError error = GetObjectTypeDataFromExcel(dataType, ref obj, ref rowDataEnumerator);
            if (error != ReadExcelToJsonStringError.NONE)
            {
                _excelToTable.Close();
                debugString = _debugMessage;
                return error;
            }
            allData.Add(obj);
            // 取得下一行資料
            tableRowData = _excelToTable.GetNextRow();
        }

        if (!hasEOR)
        {
            _excelToTable.Close();
            debugString = _debugMessage;
            return ReadExcelToJsonStringError.CANT_FIND_END_OF_ROW_TOKEN;
        }
        #endregion
        _excelToTable.Close();

        jsonString = ObjectToJsonString(allData);
        debugString = _debugMessage;
        return ReadExcelToJsonStringError.NONE;
    }

    #region 確認型別對應
    /// <summary>
    /// 確認excel表格內定義的Type是否和給予的物件資料結構有對應
    /// </summary>
    /// <param name="checkType">給予的type定義</param>
    /// <param name="checkObject">對應的物件(不可為null)</param>
    /// <param name="tableTypeEnumerator">table內文</param>
    /// <returns>是否有對應</returns>
    bool CheckObjectTypeCorrect(Type checkType, object checkObject, ref List<string>.Enumerator tableTypeEnumerator)
    {
        CommonFunction.DebugMsgFormat("type is {0}", checkType);
        FieldInfo[] allFieldInfo = checkType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        bool isConform = true;

        for (int fiIndex = 0; fiIndex < allFieldInfo.Length; ++fiIndex)
        {
            Type curType = allFieldInfo[fiIndex].FieldType;
            // 避免curObj為Null，否則可能會讓後面method取不到資訊(string為例外狀況)
            object curObj = allFieldInfo[fiIndex].GetValue(checkObject);
            if (curType != typeof(string) && curObj == null) { curObj = Activator.CreateInstance(curType); }

            if (curType.IsArray)
            {
                isConform = CheckArrayTypeCorrect(curType, curObj, ref tableTypeEnumerator);
            }
            else if (curType.IsClass && curType != typeof(string))
            {
                isConform = CheckObjectTypeCorrect(curType, curObj, ref tableTypeEnumerator);
            }
            else
            {
                isConform = CheckBaseTypeCorrect(curType, curObj, ref tableTypeEnumerator);
            }
            if (!isConform)
            {
                CommonFunction.DebugMsgFormat("type = {0} excelType = {1}", checkType, tableTypeEnumerator.Current);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 確認excel表格內定義的Type是否和給予的陣列資料結構有對應
    /// </summary>
    /// <param name="checkType">給予的type定義</param>
    /// <param name="checkObject">對應的物件</param>
    /// <param name="tableTypeEnumerator">table內文</param>
    /// <returns>是否有對應</returns>
    bool CheckArrayTypeCorrect(Type checkType, object checkObject, ref List<string>.Enumerator tableTypeEnumerator)
    {
        CommonFunction.DebugMsgFormat("type is {0}", checkType);
        if (!checkType.IsArray || !(checkObject is Array))
        {
            CommonFunction.DebugMsgFormat("type = {0} excelType = {1}", checkType, tableTypeEnumerator.Current);
            return false;
        }

        Type elementType = checkType.GetElementType();
        Array checkArray = checkObject as Array;
        bool isConform = true;
        for (int elementCount = 0; elementCount < checkArray.Length; ++elementCount)
        {
            // 避免element為Null，否則可能會讓後面method取不到資訊(string為例外狀況)
            object elementObject = checkArray.GetValue(elementCount);
            if (elementObject != typeof(string) && elementObject == null) { elementObject = Activator.CreateInstance(elementType); }

            if (elementType.IsArray)
            {
                isConform = CheckArrayTypeCorrect(elementType, elementObject, ref tableTypeEnumerator);
            }
            else if (elementType.IsClass && elementType != typeof(string))
            {
                isConform = CheckObjectTypeCorrect(elementType, elementObject, ref tableTypeEnumerator);
            }
            else
            {
                isConform = CheckBaseTypeCorrect(elementType, elementObject, ref tableTypeEnumerator);
            }
            if (!isConform)
            {
                CommonFunction.DebugMsgFormat("type = {0} excelType = {1}", checkType, tableTypeEnumerator.Current);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 確認excel表格內定義的Type是否和給予的基本資料結構有對應
    /// </summary>
    /// <param name="checkType">給予的type定義</param>
    /// <param name="checkObject">對應的物件</param>
    /// <param name="tableTypeEnumerator">table內文</param>
    /// <returns>是否有對應</returns>
    bool CheckBaseTypeCorrect(Type checkType, object checkObject, ref List<string>.Enumerator tableTypeEnumerator)
    {
        if (!tableTypeEnumerator.MoveNext()) { return false; } // 沒有下一個，表示沒有對應
        // 由於可能有nullable型態，取得對應的非nullable型態再比較
        bool isNullabelType = checkType.IsGenericType && checkType.GetGenericTypeDefinition() == typeof(Nullable<>);
        Type realType = (isNullabelType) ? Nullable.GetUnderlyingType(checkType) : checkType;

        string compareStr;
        if (_baseTypeString.TryGetValue(realType, out compareStr)) // 是基本四型態之一
        {
            if (tableTypeEnumerator.Current.ToUpper().Equals(compareStr.ToUpper()))
            {
                return true;
            }
            else  // 沒對應到正確的基本型態
            {
                CommonFunction.DebugMsgFormat("base error : Type = {1} excelType = {2}", _debugMessage, realType, tableTypeEnumerator.Current);
                _debugMessage = string.Format("{0} base error : Type = {1} excelType = {2}", _debugMessage, realType, tableTypeEnumerator.Current);
                return false;
            }
        }
        else  // 不是基本四型態之一的話直接跳沒對應
        {
            CommonFunction.DebugMsgFormat("not base error : Type = {1}", _debugMessage, realType);
            _debugMessage = string.Format("{0} not base error : Type = {1}", _debugMessage, realType);
            return false;
        }
    }
    #endregion
    /// <summary>
    /// 確定是否為空行（整行資料都是沒文字或null）
    /// </summary>
    /// <param name="rowData">整行的資料</param>
    /// <returns>是否為空行</returns>
    bool CheckEmptyRow(List<string> rowData)
    {
        if (rowData == null || rowData.Count == 0) { return true; }
        foreach (string data in rowData)
        {
            if (!string.IsNullOrEmpty(data)) { return false; }
        }
        return true;
    }
    #region 取得對應型別資料
    /// <summary>
    /// 從excel檔案中取得物件型別的資料
    /// </summary>
    /// <param name="type">物件型別的type</param>
    /// <param name="retObj">存放取得的資料</param>
    /// <param name="rowDataEnumerator">由excel來的row Data</param>
    /// <returns>可能的錯誤</returns>
    ReadExcelToJsonStringError GetObjectTypeDataFromExcel(Type type, ref object retObj, ref List<string>.Enumerator rowDataEnumerator)
    {
        ReadExcelToJsonStringError error = ReadExcelToJsonStringError.NONE;
        bool isNull = true;
        FieldInfo[] allFieldInfo = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        for (int fiIndex = 0; fiIndex < allFieldInfo.Length; ++fiIndex)
        {
            Type curFieldType = allFieldInfo[fiIndex].FieldType;
            // 避免curFieldObj為Null，否則可能會讓後面method取不到資訊(string為例外狀況)
            object curFieldObj = allFieldInfo[fiIndex].GetValue(retObj);
            if (curFieldType != typeof(string) && curFieldObj == null) { curFieldObj = Activator.CreateInstance(curFieldType); }

            if (curFieldType.IsArray)
            {
                error = GetArrayTypeDataFromExcel(curFieldType, ref curFieldObj, ref rowDataEnumerator);
            }
            else if (curFieldType.IsClass && curFieldType != typeof(string))
            {
                error = GetObjectTypeDataFromExcel(curFieldType, ref curFieldObj, ref rowDataEnumerator);
            }
            else
            {
                error = GetBaseTypeDataFromExcel(curFieldType, ref curFieldObj, ref rowDataEnumerator);
            }
            if (error != ReadExcelToJsonStringError.NONE)
            {
                retObj = null;
                return error;
            }
            if (curFieldObj != null) { isNull = false; }
            allFieldInfo[fiIndex].SetValue(retObj, curFieldObj);
        }
        if (isNull) { retObj = null; }
        return error;
    }

    /// <summary>
    /// 從excel檔案中取得陣列型別的資料
    /// </summary>
    /// <param name="type">陣列型別的type</param>
    /// <param name="retObj">存放取得的資料</param>
    /// <param name="rowDataEnumerator">由excel來的row Data</param>
    /// <returns>可能的錯誤</returns>
    ReadExcelToJsonStringError GetArrayTypeDataFromExcel(Type type, ref object retObj, ref List<string>.Enumerator rowDataEnumerator)
    {
        ReadExcelToJsonStringError error = ReadExcelToJsonStringError.NONE;
        if (!type.IsArray || !(retObj is Array))
        {
            _debugMessage = string.Format("{0} 轉檔錯誤：非陣列的類型({1})想解成陣列\n", _debugMessage, type.Name);
            return ReadExcelToJsonStringError.NOT_ARRAY_TYPE_USE_GET_ARRAY_METHOD;
        }

        Array retArray = retObj as Array;
        bool isNull = true;
        // TODO: 多維陣列可能需要特別處理，現在先不管
        Type elementType = type.GetElementType();
        for (int elementCount = 0; elementCount < retArray.Length; ++elementCount)
        {
            // 避免element為Null，否則可能會讓後面method取不到資訊(string為例外狀況)
            object elementObj = retArray.GetValue(elementCount);
            if (elementObj != typeof(string) && elementObj == null) { elementObj = Activator.CreateInstance(elementType); }

            if (elementType.IsArray)
            {
                error = GetArrayTypeDataFromExcel(elementType, ref elementObj, ref rowDataEnumerator);
            }
            else if (elementType.IsClass && elementType != typeof(string))
            {
                error = GetObjectTypeDataFromExcel(elementType, ref elementObj, ref rowDataEnumerator);
            }
            else
            {
                error = GetBaseTypeDataFromExcel(elementType, ref elementObj, ref rowDataEnumerator);
            }
            if (error != ReadExcelToJsonStringError.NONE)
            {
                retObj = null;
                return error;
            }
            if (elementObj != null) { isNull = false; }
            retArray.SetValue(elementObj, elementCount);
        }
        if (isNull) { retObj = null; }
        return error;
    }

    /// <summary>
    /// 從excel檔案中取得基本型別的資料
    /// </summary>
    /// <param name="type">基本型別的type</param>
    /// <param name="retObj">存放取得的資料</param>
    /// <param name="rowDataEnumerator">由excel來的row Data</param>
    /// <returns>可能的錯誤</returns>
    ReadExcelToJsonStringError GetBaseTypeDataFromExcel(Type type, ref object retObj, ref List<string>.Enumerator rowDataEnumerator)
    {
        if (!rowDataEnumerator.MoveNext())
        {
            retObj = null;
            return ReadExcelToJsonStringError.DATA_COL_NUM_IS_NOT_ENOUGH;
        }

        bool isNull = string.IsNullOrEmpty(rowDataEnumerator.Current);
        if (type == typeof(string))
        {
            retObj = (isNull) ? null : rowDataEnumerator.Current;
            return ReadExcelToJsonStringError.NONE;
        }
        else
        {
            bool isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isNull) // 資料是空的
            {
                retObj = null;
                return (isNullableType) ? ReadExcelToJsonStringError.NONE : ReadExcelToJsonStringError.DATA_CANT_BE_NULL; // type可為null，則資料為空沒問題
            }
            else // 資料非空
            {
                string[] para = new string[1] { rowDataEnumerator.Current };
                Type[] transferType = new Type[1] { typeof(string) };
                Type realType = (isNullableType) ? Nullable.GetUnderlyingType(type) : type;
                try
                {
                    retObj = realType.GetMethod("Parse", transferType).Invoke(null, para);
                    return ReadExcelToJsonStringError.NONE;
                }
                catch (Exception e)
                {
                    CommonFunction.DebugMsgFormat("取得基本型別時出錯\n{0}\n{1}", e.Message, e.StackTrace);
                    retObj = null;
                    return ReadExcelToJsonStringError.GET_BASE_TYPE_ERROR;
                }
            }
        }
    }
    #endregion
}
