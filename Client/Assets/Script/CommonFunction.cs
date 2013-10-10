using UnityEngine;
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

    #region UI相關
    /// <summary>
    /// 依據設定 建立一個UIButton
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="btnName">按鈕名稱</param>
    /// <param name="relativePos">按鈕位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="atlas">使用的Atlas</param>
    /// <param name="spriteName">使用的Sprite名稱</param>
    /// <param name="width">按鈕寬度</param>
    /// <param name="height">按鈕高度</param>
    /// <returns>建出的UIButton</returns>
    public static UIButton CreateUIButton(GameObject parentObj, string btnName, Vector3 relativePos, int depth, UIAtlas atlas, string spriteName, UIFont font, 
        int width, int height)
    {
        GameObject retButtonObj = NGUITools.AddChild(parentObj);
        retButtonObj.name = btnName;
        retButtonObj.transform.localPosition = relativePos;
        // 設定按鈕背景圖
        UISprite bg = NGUITools.AddWidget<UISprite>(retButtonObj);
        bg.type = UISprite.Type.Sliced;
        bg.name = "Background";
        bg.depth = depth;
        bg.atlas = atlas;
        bg.spriteName = spriteName;
        bg.width = width;
        bg.height = height;
        bg.MakePixelPerfect();
        // font
        if (font != null)
        {
            UILabel lbl = NGUITools.AddWidget<UILabel>(retButtonObj);
            lbl.font = font;
            lbl.text = retButtonObj.name;
            lbl.MakePixelPerfect();
        }
        // Add a Collider
        NGUITools.AddWidgetCollider(retButtonObj);
        // Add the scripts
        retButtonObj.AddComponent<UIPlaySound>();
        UIButton retBtn = retButtonObj.AddComponent<UIButton>();
        retBtn.tweenTarget = bg.gameObject;

        return retBtn;
    }
    /// <summary>
    /// 設定UIButton的各種情況顏色
    /// </summary>
    /// <param name="btn">要設定的UIButton</param>
    /// <param name="normalColor">沒任何事件時的顏色</param>
    /// <param name="disableColor">Disable的顏色</param>
    /// <param name="pressedColor">按下時的顏色</param>
    /// <param name="hoverColor">滑鼠經過時的顏色</param>
    public static void SetColor(this UIButton btn, Color normalColor, Color disableColor, Color pressedColor, Color hoverColor)
    {
        btn.defaultColor = normalColor;
        btn.UpdateColor(true, true);
        btn.disabledColor = disableColor;
        btn.pressed = pressedColor;
        btn.hover = hoverColor;
    }
    /// <summary>
    /// 依據設定 建立一個Progress Bar
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="progressBarName">Progress Bar名稱</param>
    /// <param name="relativePos">Progress Bar位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="atlas">使用的Atlas</param>
    /// <param name="backgroundName">background使用的Sprite名稱</param>
    /// <param name="foregroundName">foreground使用的Sprite名稱</param>
    /// <param name="width">Progress Bar寬度</param>
    /// <param name="height">Progress Bar高度</param>
    /// <returns>建出的 ProgressBar</returns>
    public static UISlider CreateProgressBar(GameObject parentObj, string progressBarName, Vector3 relativePos, int depth,
        UIAtlas atlas, string backgroundName, string foregroundName, int width, int height)
    {
        GameObject progressBarObject = NGUITools.AddChild(parentObj);
        progressBarObject.name = progressBarName;
        progressBarObject.transform.localPosition = relativePos;
        // Background sprite
        //UIAtlas.Sprite bgs = atlas.GetSprite(backgroundName);
        UISpriteData bgs = atlas.GetSprite(backgroundName);
        UISprite back = (UISprite)NGUITools.AddWidget<UISprite>(progressBarObject);

        back.type = bgs.hasBorder ? UISprite.Type.Sliced : UISprite.Type.Simple;
        back.name = "Background";
        back.depth = depth;
        back.pivot = UIWidget.Pivot.Left;
        back.atlas = atlas;
        back.spriteName = backgroundName;
        back.width = width;
        back.height = height;
        back.transform.localPosition = Vector3.zero;
        back.MakePixelPerfect();

        // Foreground sprite
        UISpriteData fgs = atlas.GetSprite(foregroundName);
        UISprite front = NGUITools.AddWidget<UISprite>(progressBarObject);
        front.type = fgs.hasBorder ? UISprite.Type.Sliced : UISprite.Type.Simple;
        front.name = "Foreground";
        front.pivot = UIWidget.Pivot.Left;
        front.atlas = atlas;
        front.spriteName = foregroundName;
        front.width = width;
        front.height = height;
        front.transform.localPosition = Vector3.zero;
        front.MakePixelPerfect();

        // Add the slider script
        UISlider retSilder = progressBarObject.AddComponent<UISlider>();
        retSilder.foreground = front.transform;
        retSilder.value = 0.0f;

        return retSilder;
    }

    /// <summary>
    /// 依據設定 建立一個Label
    /// </summary>
    /// <param name="parentObj">parent的gameObject</param>
    /// <param name="labelName">label名稱</param>
    /// <param name="relativePos">label位置（相對於preantObj）</param>
    /// <param name="depth">深度</param>
    /// <param name="font">使用的Font</param>
    /// <param name="labelColor">文字顏色</param>
    /// <param name="labelText">文字內容</param>
    /// <returns>建出的Label</returns>
    public static UILabel CreateLabel(GameObject parentObj, string labelName, Vector3 relativePos, int depth, UIFont font, Color labelColor, string labelText)
    {
        UILabel retLabel = NGUITools.AddWidget<UILabel>(parentObj);
        retLabel.transform.localPosition = relativePos;
        retLabel.name = labelName;
        retLabel.depth = depth;
        retLabel.font = font;
        retLabel.text = labelText;
        retLabel.color = labelColor;
        retLabel.MakePixelPerfect();
        return retLabel;
    }
    #endregion
}
