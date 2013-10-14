using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

/// <summary>
/// 自定義屬性宣告, StringValue
/// </summary>
public class StringValue : System.Attribute
{
    private string _value;
    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }
}

/// <summary>
// 自定義屬性宣告, StringValue for File
/// </summary>
public class DoubleStringValue : System.Attribute
{
    private string _kind;
    private string _content;
    public DoubleStringValue(string kind, string content)
    {
        _kind = kind;
        _content = content;
    }

    public string Content
    {
        get { return _content; }
    }

    public string Kind
    {
        get { return _kind; }
    }
}

/// <summary>
/// 自定義屬性宣告, FileValue, 路徑與副檔名相關, 以小寫為基準
/// </summary>
public class EnumResourceConfig : System.Attribute
{
    private bool _versionControl;
    private bool _useLanguagePostfix; //是否使用語系不同的後綴詞
    private int _defaultPriority;
    private string _path;
    private string _extension;
    public EnumResourceConfig(bool versionControl, bool useLanguagePostfix, int defaultPriority, string path, string extension)
    {
        _versionControl = versionControl;
        _useLanguagePostfix = useLanguagePostfix;
        _defaultPriority = defaultPriority;
        _path = path.ToLower();
        _extension = extension.ToLower();
    }

    public string Extension
    {
        get { return _extension; }
    }

    public string Path
    {
        get { return _path; }
    }

    public bool VersionControlled
    {
        get { return _versionControl; }
    }

    public bool UseLanguagePostfix
    {
        get { return _useLanguagePostfix; }
    }

    public int DefaultPriority
    {
        get { return _defaultPriority; }
    }
}

/// <summary>
///  自定義屬性宣告, 綁定檔案名稱與類別
/// </summary>
public class EnumClassValue : System.Attribute
{
    protected string _fileName;  // assetbundle(or others) 檔名, 依據小寫
    protected Type _classType;    // class 類別W
    public EnumClassValue(Type classType, string fileName)
    {
        _fileName = fileName.ToLower();
        _classType = classType;
    }
    public EnumClassValue(Type classType)
    {
        _fileName = "";
        _classType = classType;
    }
    public EnumClassValue() { }

    public string FileName
    {
        get { return _fileName; }
    }

    public Type ClassType
    {
        get { return _classType; }
    }

    /// <summary>
    ///  從 Enum 取得 name
    /// </summary>
    public static string GetFileName(Enum value)
    {
        string output = null;
        EnumClassValue enumType = null;
        if (Retrieve(value, out enumType))
            output = enumType.FileName;
        return output;
    }

    /// <summary>
    ///  從 Enum 取得 class 類別
    /// </summary>
    public static Type GetClassType(Enum value)
    {
        Type output = null;
        EnumClassValue enumType = null;
        if (Retrieve(value, out enumType))
            output = enumType.ClassType;
        return output;
    }

    /// <summary>
    /// 將 Enum 復原為 EnumClassValue
    /// </summary>
    private static bool Retrieve(Enum value, out EnumClassValue output)
    {
        output = null;
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        EnumClassValue[] attrs = fi.GetCustomAttributes(typeof(EnumClassValue), false) as EnumClassValue[]; // Retrieve to self-def object
        if (attrs.Length > 0)
        {
            output = attrs[0];
            return true;
        }
        return false;
    }
}

/// <summary>
/// 
/// </summary>
public struct AccountData
{
    public ushort PlayerName;           // 玩家名稱
    public ushort MaxActionPoint;       // max行動點數
    public ushort CurrentActionPoint;   // 目前行動點數
    public ushort MaxCardSlot;          //目前卡片背包最大格數
    public uint[] Cards;                //目前持有卡片
    public ushort MaxFriendSlot;        //目前朋友最大格數
    public uint[] Friends;              //目前持有卡片
}

/// <summary>
/// 禮物資訊
/// </summary>
public struct GiftsData
{
    // LastUpdateTime : 目前C端資料最後一次更新時間 ( 指 server 時間), 無法由C端得知曾變動過的資料都要有此欄位
    public DateTime LastUpdateTime;     
    public uint[] GiftsID;            // 禮物 ID
}

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class HTTPResponseMixDatas
{
    public int Serial;
    public int RequestMain;
    public int RequestSub;
    public HTTPResponse[] Packages;
}

[StructLayout(LayoutKind.Sequential)]
public class HTTPResponse
{
    public int MainKind;
    public int SubKind;
    public int[] Ints;
    public string[] Strs;
}

// TODO: 測試用
/// <summary>
/// 從表格來的場景資料
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class SceneData
{
    public ushort SceneID;       // 場景ID
    public string SceneName;     // 場景名稱（中地圖or剛換到該場景顯示的名稱）
    public string SceneFileName; // 場景檔案名稱

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("=========== SceneData =============\n");
        sb.AppendFormat("場景ID = {0}\n", SceneID);
        sb.AppendFormat("場景名稱 = {0}\n", SceneName);
        sb.AppendFormat("場景檔案名稱 = {0}\n", SceneFileName);
        sb.Append("===================================\n");
        return sb.ToString();
    }
}

/// <summary>
/// 區域類型
/// </summary>
enum BattleType : byte
{
    Nonrmal = 1, // 一般戰鬥
    Boss = 2     // 一般 BOSS
}

/// <summary>
/// 戰鬥配置表
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Battle
{
    public uint ID;         //劇情群組 ID
    public uint NPCID;      //NPC ID
    public byte Type;       //戰鬥類型
    public byte Numbers;    //出現數量
}

/// <summary>
/// 劇情對話資料 (主鍵 = 劇情 ID + 順序 ID)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Story
{
    public uint ID; //劇情群組 ID
    public uint ShowOrder; //執行順序 ID
    public string Content; //對話內容
}

/// <summary>
/// 區域類型
/// </summary>
enum AreaEventType : byte
{
    Battle = 1, // 戰鬥 (一般+ BOSS)
    Item = 2,   // 發現物品
    Card = 3,   // 發現技能卡片
    Story = 4   // 觸發劇情
}

/// <summary>
/// 區域事件資料
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class AreaEvent
{
    public uint ID; //區域事件序號 ID
    public ushort AreaID; //區域事件場景 ID
    public ushort RatioProportion; //區域事件機率權重
    public byte EventType; //區域事件類型
    public uint EventValue; //區域事件共用欄位
    public uint CompleteMissionID; //觸發後將會完成的任務 ID
}

/// <summary>
/// 區域資料
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Area
{
    public ushort AreaID;       // 區域ID
    public ushort AreaGroupID;       // 區域群組ID
    public byte AreaLayer;       // 區域層級
    public byte AreaType;     // 區域種類
    public string AreaName;     // 區域名稱
    public byte AreaLevel;    // 區域難度星數
    public ushort Cost;         //區域消耗體力
    // 進入所需完成的任務 , 型別應為 uint[], 但 excel to json 似乎有問題, 待查
    public uint[] RequireMission = new uint[5];
    public uint BeginDate;         //活動型任務開始日期
    public uint CloseDate;         //活動型任務結束日期
}

/// <summary>
/// 區域類型
/// </summary>
enum AreaType : byte
{
    Normal = 1, // 一般關卡
    Activity = 2 //活動關卡
}