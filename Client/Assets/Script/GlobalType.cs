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