using UnityEngine;
using System.Text;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 讀取資料的狀態
/// </summary>
public enum ReadDataState
{
    Unload, // 未讀
    Loading, // 讀檔中
    ReadError,	//讀檔完成，但解檔失敗(原因可能為檔案格式變換或其他...)
    HaveLoad,	//讀檔完成，且解檔完成
}


/// <summary>
/// 管理遊戲中所有資料表
/// </summary>
public class DataTableManager
{
    public ReadDataState[] DataState;
    public Dictionary<GLOBALCONST.DataLoadTag, ReadDataState> _allDataState;

    #region 表格資料宣告區
    private Dictionary<GLOBALCONST.DataLoadTag, IList> _allDataList = new Dictionary<GLOBALCONST.DataLoadTag, IList>()
    {
        {GLOBALCONST.DataLoadTag.Scene, new List<SceneData>()},     // 場景資料表
    };
    #endregion


    public DataTableManager()
    {
        _allDataState = new Dictionary<GLOBALCONST.DataLoadTag, ReadDataState>();
        foreach (GLOBALCONST.DataLoadTag dlt in _allDataList.Keys)
        {
            _allDataState[dlt] = ReadDataState.Unload;
        }
    }

    ~DataTableManager()
    {
        _allDataList = null;
    }

    /// <summary>
    /// 將所有資料List內容輸出
    /// </summary>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DataTableManager:\n");
        sb.Append(CommonFunction.ListDataToString(_allDataList[GLOBALCONST.DataLoadTag.Scene] as IList, "_allDataList[Scene]"));
        sb.Append("End Of DataTableManager\n");
        return sb.ToString();
    }

    bool DeserializeObject(string encodingString, ref object refObj)
    {
        try
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            settings.CheckAdditionalContent = false;
            Newtonsoft.Json.JsonConvert.PopulateObject(encodingString, refObj); // 將encoding的資料填充到refObj內
            return true;
        }
        catch (Exception e)
        {
            CommonFunction.DebugMsgFormat("JSONDeserializeObject error!(type = {0})\n{1}\n{2}\n", refObj.GetType().ToString(), e.Message, e.StackTrace);
            return false;
        }
    }

    // 得處理一下非同步狀況處理
    //IEnumerator Load(int tag, LoadAttribute loadAttr)
    IEnumerator Load(GLOBALCONST.DataLoadTag dataLoadTag)
    {
        string filePath = GLOBALCONST.DIR_DATA_JSON + EnumClassValue.GetFileName(dataLoadTag) + GLOBALCONST.EXT_JSON;
        string encodingStr = string.Empty;
        if (filePath.Contains("://"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            encodingStr = www.text;
        }
        else
        {
            encodingStr = File.ReadAllText(filePath);
        }
        try
        {
            CommonFunction.DebugMsgFormat("讀取 {0} 的 Data", dataLoadTag);
            object refObj = Activator.CreateInstance(typeof(List<>).MakeGenericType(EnumClassValue.GetClassType(dataLoadTag)));
            bool isSuccess = DeserializeObject(encodingStr, ref refObj);
            if (isSuccess)
            {
                _allDataList[dataLoadTag] = refObj as IList;
                _allDataState[dataLoadTag] = ReadDataState.HaveLoad;
                // TODO: 讀取事件資料完成用觀察者？
                CommonFunction.DebugMsgFormat("{0}讀取成功", dataLoadTag);
            }
            else
            {
                _allDataState[dataLoadTag] = ReadDataState.ReadError;
                CommonFunction.DebugMsgFormat("{0}讀取失敗", dataLoadTag);
            }


        }
        catch (Exception e)
        {
            CommonFunction.DebugMsgFormat("EventData Read Error:\n");
            CommonFunction.DebugMsgFormat("StackTrace:\n{0}\n", e.StackTrace);
            CommonFunction.DebugMsgFormat("Msg:\n{0}\n", e.Message);
            _allDataState[dataLoadTag] = ReadDataState.ReadError;
        }
    }

    /// <summary>
    /// 讀取所有的table資料
    /// </summary>
    public void LoadAllTable()
    {
        foreach (GLOBALCONST.DataLoadTag dataLoadTag in Enum.GetValues(typeof(GLOBALCONST.DataLoadTag)))
        {
            if (_allDataState[dataLoadTag] == ReadDataState.Unload)
            {
                _allDataState[dataLoadTag] = ReadDataState.Loading;  // 需要先設，免得load跑太快使得已讀完的flag被設回
                // GameMain.Instance.StartCoroutine(Load(dataLoadTag));
            }
        }
    }

    #region 取得SceneData相關
    /// <summary>
    /// 取得所有場景資料
    /// </summary>
    /// <returns>取得的場景資料</returns>
    public List<SceneData> GetAllSceneData()
    {
        return _allDataList[GLOBALCONST.DataLoadTag.Scene] as List<SceneData>;
    }

    /// <summary>
    /// 依據場景ID嘗試取得一筆場景資料
    /// </summary>
    /// <param name="sceneID">要取得場景資料的場景ID</param>
    /// <param name="data">取得的場景資料存放處</param>
    /// <returns>是否有取得場景資料</returns>
    public bool TryGetOneSceneData(ushort sceneID, out SceneData data)
    {
        data = default(SceneData);
        if (DataState[(int)GLOBALCONST.DataLoadTag.Scene] != ReadDataState.HaveLoad) { return false; }
        if (_allDataList[GLOBALCONST.DataLoadTag.Scene] == null) { return false; }

        data = (_allDataList[GLOBALCONST.DataLoadTag.Scene] as List<SceneData>).Find(sd => sd.SceneID == sceneID);

        return data != default(SceneData); // 若data == default(SceneData) 表示沒有搜尋到資料
    }
    #endregion
}
