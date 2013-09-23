using System;
using System.IO;
using System.Collections.Generic;

using NPOI.SS.UserModel;

/// <summary>
/// 讀取excel檔案轉換成json字串的Error類型
/// </summary>
public enum ReadExcelToJsonStringError
{
    // 檔案開啟問題
    NONE = 0, // 沒有問題
    FILE_NOT_EXIST = 1, // 檔案不存在        debugMsg = string.Format("{0} 轉換失敗：檔案不存在\n", filePath);
    FILE_OPEN_ERROR = 2, // 檔案開啟有問題    debugMsg = string.Format("{0} 轉換失敗：打開有問題\n", dci.FileName);
    // 開始符號
    CANT_FIND_START_TOKEN = 10, // 找不到開始符號   debugMsg = string.Format("{0} 轉換失敗：找不到開始符號[{1}]\n", dci.FileName, START_OF_TABLE);
    // COLUMN相關
    CANT_FIND_END_OF_COL_TOKEN = 20, // 找不到行結尾符號   debugMsg = string.Format("{0} 轉換失敗：找不到行結尾符號[{1}]\n", dci.FileName, END_OF_COLUMN);
    TABEL_COL_NUM_IS_ZERO = 21, // 欄位數為0          debugMsg = string.Format("{0} 轉換失敗：表格欄位數為0\n",  dci.FileName);
    TYPE_COL_NUM_NOT_ENOUGH = 22, // 指示型別欄位不足   debugMsg = string.Format("{0} 轉換失敗：欄位型別資料不足\n", dci.FileName);
    INSTRUCT_IGNORE_COL_NOT_ENOUGH = 23, // 指示可忽略欄位的數量不足 debugMsg = string.Format("{0} 轉換失敗：可忽略欄位資料不足\n", dci.FileName);
    // ROW相關
    END_OF_ROW_TOKEN_TO_EARLY = 30, // 太早遇到列結尾符號 debugMsg = string.Format("{0} 轉換失敗：太早遇到列結尾符號[{1}]\n", dci.FileName, END_OF_ROW);
    DONT_HAVE_TYPE_ROW = 31, // 沒有指示型別的列   debugMsg = string.Format("{0} 轉換失敗：沒有指示型別的列\n", dci.FileName);
    DONT_INSTRUCT_NEED_ROW = 32, // 沒指示需要欄位     debugMsg = string.Format("{0} 轉換失敗：沒有指示各欄位是誰所需要\n", dci.FileName);
    CANT_FIND_END_OF_ROW_TOKEN = 33, // 找不到列結尾符號   debugMsg = string.Format("{0} 轉換失敗：找不到列結尾符號[{2}]\n", dci.FileName, END_OF_ROW)

    //
    TABLE_TYPE_IS_NOT_CONFORM = 40, // 資料結構不合     debugMsg = string.Format("{0} 轉換失敗：表格與資料結構({1})內容不符", dci.FileName, dci.DataType);
    HAS_EMPTY_ROW = 41, // 資料有空行       debugMsg = string.Format("{0} 轉換失敗：表格有空行", dci.FileName);
    DATA_COL_NUM_IS_NOT_ENOUGH = 42, // 資料欄位不足     debugMsg = string.Format("{0} 轉換失敗：資料欄位不足", dci.FileName);
    DATA_CANT_BE_NULL = 43, // 資料不能為空     debugMsg = string.Format("{0} 轉換失敗：表格有空行", dci.FileName)
    GET_BASE_TYPE_ERROR = 44, // 取得基本型別錯誤 debugMsg = string.Format("{0} 轉換失敗：{1} 欄位轉換失敗\n", _debugMessage, dataType.GetFields()[0].Name);
    NOT_ARRAY_TYPE_USE_GET_ARRAY_METHOD = 45, // 非陣列的類型想解成陣列 debugMsg = string.Format("{0} 轉檔錯誤：非陣列的類型({1})想解成陣列\n", _debugMessage, dataType.Name);
};

/// <summary>
/// 需要讀取此資料的是server還client
/// </summary>
public enum NeedReadSite
{
    CLIENT,
    SERVER,
};


/// <summary>
/// 將excel讀入取得所需的table
/// </summary>
public class ExcelToTable
{
    readonly string EXCEL_EXT = ".xlsx"; // excel檔案副檔名

    readonly string START_OF_TABLE = "#"; // 表示表格開始的識別字
    readonly string END_OF_COLUMN = "EOC";// 表示為最後column（不包含此column）的識別字
    readonly string END_OF_ROW = "EOR";   // 表示為最後row（不包含此row）的識別字
    readonly string NEED_READ_SITE_IS_ALL = "A";    // 「都需要讀」的欄位識別字
    readonly string NEED_READ_SITE_IS_SERVER = "S"; // 「只有Server需要讀」的欄位識別字
    readonly string NEED_READ_SITE_IS_CLIENT = "C"; // 「只有Client需要讀」的欄位識別字

    ISheet _sheet = null;
    int _currentSheetRowNum;

    int _columnCount;
    List<int> _dontNeedColumnIndexes; // table不需要的欄位Index

    public ExcelToTable()
    {
        _currentSheetRowNum = -1;

        _columnCount = 0;
        _dontNeedColumnIndexes = new List<int>();
    }

    ~ExcelToTable()
    {
        _sheet = null;

        _dontNeedColumnIndexes = null;
    }

    #region 確認table header正確性
    /// <summary>
    /// 確認excel檔案有正確的table header 且取得相關資訊
    /// </summary>
    /// <returns>可能有的錯誤類型</returns>
    public ReadExcelToJsonStringError CheckAndReadTableHeader(NeedReadSite nrs, out List<string> allType)
    {
        allType = null;
        ReadExcelToJsonStringError ree = CheckTableStartAndCountTableColumn();
        if (ree != ReadExcelToJsonStringError.NONE) { return ree; }
        ree = GetTableAllColumnType(out allType);
        if (ree != ReadExcelToJsonStringError.NONE) { return ree; }
        ree = GetTableIgnoreColumn(nrs);
        if (ree != ReadExcelToJsonStringError.NONE) { return ree; }
        DeleteIgnoreCol(ref allType);
        return ReadExcelToJsonStringError.NONE;
    }

    /// <summary>
    /// 確認table開始符號存在&計算table的column數
    /// </summary>
    /// <returns>可能有的錯誤訊息</returns>
    ReadExcelToJsonStringError CheckTableStartAndCountTableColumn()
    {
        bool hasContent = false;
        while (!hasContent) // 如果沒找到Content則要一直尋找
        {
            List<string> getData = GetNextRow();
            if (getData == null) { return ReadExcelToJsonStringError.CANT_FIND_START_TOKEN; } // 表示已經讀到檔案結尾依舊沒東西or沒讀取檔案
            // 如果是空行會回傳空的list
            if (getData.Count > 0 && !string.IsNullOrEmpty(getData[0]) && getData[0].Equals(START_OF_TABLE))
            {
                // [0] = "#" 所以從第一個開始檢查
                for (_columnCount = 1; _columnCount < getData.Count; ++_columnCount)
                {
                    if (!string.IsNullOrEmpty(getData[_columnCount]) && getData[_columnCount].Equals(END_OF_COLUMN)) { break; } // 遇到END_OF_COLUMN跳離，此時_columnCount即欄位數
                }
                if (_columnCount == getData.Count) // 表示中途都未跳離
                {
                    _columnCount = 0; // 將column數量重設回0
                    return ReadExcelToJsonStringError.CANT_FIND_END_OF_COL_TOKEN;
                }
                hasContent = true;
            }
        }
        return ReadExcelToJsonStringError.NONE;
    }

    /// <summary>
    /// 確認此Excel是否有所需Table，
    /// </summary>
    bool HasTable()
    {
        bool hasContent = false;
        while (!hasContent) // 如果沒找到content則要一直尋找
        {
            List<string> getData = GetNextRow();
            if (getData == null) { break; } // 表示已經讀到excel檔案結尾依舊沒東西 or 根本沒讀取file
            // 如果是空行會回傳空的list
            if (getData.Count > 0 && !string.IsNullOrEmpty(getData[0]) && getData[0].Equals(START_OF_TABLE)) { hasContent = true; }
        }
        return hasContent;
    }

    /// <summary>
    /// 取得excel中table的column數，結果存在_columnCount
    /// </summary>
    /// <returns>可能有的錯誤訊息</returns>
    ReadExcelToJsonStringError GetTableColumnCount()
    {
        List<string> countColumnData = GetNextRow();
        if (countColumnData == null || countColumnData.Count == 0) { return ReadExcelToJsonStringError.TABEL_COL_NUM_IS_ZERO; } // 沒有計算到欄位數
        if (CheckEndOfTable(countColumnData)) { return ReadExcelToJsonStringError.END_OF_ROW_TOKEN_TO_EARLY; } // 太早遇到END_OF_ROW
        for (_columnCount = 0; _columnCount < countColumnData.Count; ++_columnCount)
        {
            if (!string.IsNullOrEmpty(countColumnData[_columnCount]) && countColumnData[_columnCount].Equals(END_OF_COLUMN)) { break; } // 遇到END_OF_COLUMN跳離，此時_columnCount即欄位數
        }
        if (_columnCount == countColumnData.Count) // 表示中途都未跳離 
        {
            _columnCount = 0; // 將column數量重設回0
            return ReadExcelToJsonStringError.CANT_FIND_END_OF_COL_TOKEN;
        }
        return ReadExcelToJsonStringError.NONE;
    }

    /// <summary>
    /// 取得excel中table內所有column對應的type，結果存在_allType
    /// </summary>
    /// <returns>可能有的錯誤訊息</returns>
    ReadExcelToJsonStringError GetTableAllColumnType(out List<string> typeColumnData)
    {
        typeColumnData = GetNextRow();
        if (typeColumnData == null || typeColumnData.Count == 0) { return ReadExcelToJsonStringError.DONT_HAVE_TYPE_ROW; } // 沒有型別row
        if (CheckEndOfTable(typeColumnData)) { return ReadExcelToJsonStringError.END_OF_ROW_TOKEN_TO_EARLY; } // 太早遇到END_OF_ROW
        if (typeColumnData.Count != _columnCount) { return ReadExcelToJsonStringError.TYPE_COL_NUM_NOT_ENOUGH; } // column數量不正確

        return ReadExcelToJsonStringError.NONE;
    }

    /// <summary>
    /// 取得excel中table內所有忽略的column index，結果存在ignoreColumnData
    /// </summary>
    /// <returns>可能有的錯誤訊息</returns>
    ReadExcelToJsonStringError GetTableIgnoreColumn(NeedReadSite nrs)
    {
        _dontNeedColumnIndexes.Clear();
        List<string> ignoreColumnData = GetNextRow();
        if (ignoreColumnData == null || ignoreColumnData.Count == 0) { return ReadExcelToJsonStringError.DONT_INSTRUCT_NEED_ROW; } // 沒指示不需讀入欄位
        if (CheckEndOfTable(ignoreColumnData)) { return ReadExcelToJsonStringError.END_OF_ROW_TOKEN_TO_EARLY; } // 太早遇到END_OF_ROW
        if (ignoreColumnData.Count < _columnCount) { return ReadExcelToJsonStringError.INSTRUCT_IGNORE_COL_NOT_ENOUGH; } //column數量不正確
        for (int col = 0; col < _columnCount; ++col)
        {
            if (string.IsNullOrEmpty(ignoreColumnData[col])) { return ReadExcelToJsonStringError.INSTRUCT_IGNORE_COL_NOT_ENOUGH; } // column數量不正確
            if (!(ignoreColumnData[col].Equals(NEED_READ_SITE_IS_ALL) ||
                (ignoreColumnData[col].Equals(NEED_READ_SITE_IS_SERVER) && nrs == NeedReadSite.SERVER) ||
                (ignoreColumnData[col].Equals(NEED_READ_SITE_IS_CLIENT) && nrs == NeedReadSite.CLIENT)))
            { _dontNeedColumnIndexes.Add(col); }
        }
        return ReadExcelToJsonStringError.NONE;
    }
    #endregion
    #region 確認table結尾
    /// <summary>
    /// 確定是否為table結尾
    /// </summary>
    /// <param name="rowData">row資料，不處理本身為null，或count = 0的狀況</param>
    /// <returns>是否為table結尾</returns>
    public bool CheckEndOfTable(List<string> rowData)
    {
        return !string.IsNullOrEmpty(rowData[0]) && rowData[0].Equals(END_OF_ROW);
    }
    #endregion
    #region 開關檔、讀一行資料、刪除不需要資料
    /// <summary>
    /// 開啟一excel檔案，開啟成功（回傳值為ReadExcelError.NONE）則_excelReader可讀取資料
    /// </summary>
    /// <param name="directoryPath">資料夾路徑</param>
    /// <param name="fileName">該資料夾下的檔案名稱</param>    
    /// <returns>可能有的錯誤</returns>
    public ReadExcelToJsonStringError OpenExcelFile(string directoryPath, string fileName)
    {
        string filePath = directoryPath + Path.DirectorySeparatorChar + fileName + EXCEL_EXT;
        if (!File.Exists(filePath)) { return ReadExcelToJsonStringError.FILE_NOT_EXIST; }

        try
        {
            IWorkbook workBook = WorkbookFactory.Create(filePath);
            _sheet = workBook.GetSheetAt(0);
            workBook = null;
        }
        catch { return ReadExcelToJsonStringError.FILE_OPEN_ERROR; }
        return ReadExcelToJsonStringError.NONE;
    }
    /// <summary>
    /// 關閉開啟的excel資源
    /// </summary>
    public void Close()
    {
        _currentSheetRowNum = -1;
        _sheet = null;

        _columnCount = 0;
        _dontNeedColumnIndexes.Clear();
    }

    /// <summary>
    /// 取得下一行資料
    /// </summary>
    public List<string> GetNextRow()
    {
        if (_sheet == null) { return null; }

        if (_currentSheetRowNum < _sheet.LastRowNum) { ++_currentSheetRowNum; }
        else { return null; }

        List<string> retStrList = new List<string>();

        IRow currentRow = _sheet.GetRow(_currentSheetRowNum);
        // 讀到空行currentRow會是null
        if (currentRow == null) { return retStrList; }

        int realColumnCount = (_columnCount == 0) ? currentRow.LastCellNum : _columnCount;
        for (int colCount = 0; colCount < realColumnCount; ++colCount)
        {
            if (!_dontNeedColumnIndexes.Contains(colCount)) { retStrList.Add((currentRow.GetCell(colCount) == null) ? null : currentRow.GetCell(colCount).ToString()); }
        }

        return retStrList;
    }

    /// <summary>
    /// 刪除忽略資料
    /// </summary>
    /// <param name="waitDeleteData">準備要被刪除的資料</param>
    void DeleteIgnoreCol(ref List<string> waitDeleteData)
    {
        // 由於先讀type，再讀忽略欄位index，所以得再此才能依據忽略的欄位index調整allType
        _dontNeedColumnIndexes.Sort();
        for (int index = _dontNeedColumnIndexes.Count - 1; index >= 0; --index) // 由大往小刪除，避免刪錯
        {
            waitDeleteData.RemoveAt(_dontNeedColumnIndexes[index]);
        }
    }
    #endregion

}
