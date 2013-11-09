using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// NGUI使用到的字型名
/// </summary>
public enum UIFontName
{
    [StringValue("MSJH")]             MSJH,
    [StringValue("dragonword")]       DragonWord,
    [StringValue("BigAppleNF")]       BigAppleNF,
}
/// <summary>
/// 字型大小
/// </summary>
public enum UIFontSize
{
    SMALL = 16,
    MEDIUM = 30,
    LARGE = 50,
    HUD = 30,
    UI_BATTLE_BOSS_NAME = 40, // 戰鬥UI中，boss名字的文字大小
    UI_BATTLE_ROLE_NAME = 38, // 戰鬥UI中，我方角色名字的文字大小
    UI_BATTLE_START = 120, // 戰鬥UI中，開始文字的文字大小
    UI_BATTLE_END_WIN_OR_LOSE = 120,
    UI_BATTLE_END_TAP = 30,
}

/// <summary>
/// NGUI使用到的UIFont管理器
/// </summary>
public static class UIFontManager
{
    private static GameObject _fontContainer = null; // 將動態字型掛在其上的GameObject
    private static Dictionary<string, UIFont> _uiDynamicFonts = new Dictionary<string, UIFont>();

/// <summary>
    /// 創建存放UIFont用的GameObject
    /// </summary>
    private static void CreateFontContainerObject()
    {
        _fontContainer = NGUITools.AddChild(GameControl.Instance.GUIStation.GUICamera.gameObject); // 將存放UIFont用的GameObject放在UICamera物件下
        _fontContainer.name = "Font Container";
    }


    /// <summary>
    /// 確認傳入的Enum資料是否沒問題，此為Debug模式才使用，正式版的Enum資料必定通過此檢查
    /// </summary>
    /// <param name="fontNameEnum">字型名稱的Enum</param>
    /// <param name="fontSizeEnum">字型大小的Enum</param>
    /// <param name="isOK">資料是否沒問題</param>
    [Conditional("DEVELOP_DEBUG")]
    private static void CheckInputUIFontData(UIFontName fontNameEnum, UIFontSize fontSizeEnum, ref bool isOK)
    {
        //CommonFunction.DebugMsgFormat("Check font");
        isOK = false;
        StringValue fontName;
        if (!CommonFunction.GetAttribute<StringValue>(fontNameEnum, out fontName))
        {
            CommonFunction.DebugMsgFormat("無法取到Enum({0})對應字型字串", fontNameEnum.ToString("g"));
            return;
        }
        if (string.IsNullOrEmpty(fontName.Value))
        {
            CommonFunction.DebugMsgFormat("Enum ({0})對應的字型名稱字串為空", fontNameEnum.ToString("g"));
            return;
        }
        if ((int)fontSizeEnum <= 0)
        {
            CommonFunction.DebugMsgFormat("Enum ({0}) 對應的字型大小為不合法", fontSizeEnum.ToString("g"));
            return;
        }
        isOK = true;
    }
    /// <summary>
    /// 取得查詢_uiDynamicFont的key
    /// </summary>
    /// <param name="fontName">字型名稱</param>
    /// <param name="fontSize">字型大小</param>
    /// <param name="fontStyle">字型style</param>
    /// <returns>對應的key</returns>
    private static string GetUIDynamicFontsKey(StringValue fontName, UIFontSize fontSize, FontStyle fontStyle)
    {
        return string.Format("{0}_{1}_{2}", fontName.Value, (int)fontSize, fontStyle.ToString());
    }

    /// <summary>
    /// 取得動態字型
    /// </summary>
    /// <param name="fontName">字型名稱的Enum</param>
    /// <param name="fontSize">字型大小的Enum</param>
    /// <param name="fontStyle">字型stype</param>
    /// <returns></returns>
    public static UIFont GetUIDynamicFont(UIFontName fontNameEnum, UIFontSize fontSizeEnum = UIFontSize.MEDIUM, FontStyle fontStyle = FontStyle.Normal)
    {
        //CommonFunction.DebugMsgFormat("GUIFontManger.GETUIDynamicFont()");
        bool dataIsOK = true;
        CheckInputUIFontData(fontNameEnum, fontSizeEnum, ref dataIsOK);
        if (!dataIsOK) { return null; }
        //CommonFunction.DebugMsgFormat("{0}_{1}_{2} 通過測試", fontNameEnum, fontSizeEnum, fontStyle);
        // 到此步表示fontName的值沒問題
        StringValue fontName;
        CommonFunction.GetAttribute<StringValue>(fontNameEnum, out fontName);

        //CommonFunction.DebugMsgFormat("Key = {0}", GetUIDynamicFontsKey(fontName, fontSizeEnum, fontStyle));
        UIFont retUIFont;
        if (!_uiDynamicFonts.TryGetValue(GetUIDynamicFontsKey(fontName, fontSizeEnum, fontStyle), out retUIFont))
        {
            // 快取中不存在，取Resource中的
            UIFont tempFont = ResourceStation.GetUIFont(fontName.Value);
            if (tempFont != null)
            {
                if (tempFont.dynamicFontSize != (int)fontSizeEnum || tempFont.dynamicFontStyle != fontStyle)
                {
                    if (_fontContainer == null) { CreateFontContainerObject(); }
                    retUIFont = _fontContainer.AddComponent<UIFont>();
                    retUIFont.dynamicFont = tempFont.dynamicFont;
                    retUIFont.material = tempFont.material;
                    retUIFont.dynamicFontSize = (int)fontSizeEnum;
                    retUIFont.dynamicFontStyle = fontStyle;
                }
                else
                {
                    retUIFont = tempFont;
                }
                _uiDynamicFonts.Add(GetUIDynamicFontsKey(fontName, fontSizeEnum, fontStyle), retUIFont);
            }
        }
        return retUIFont;
    }
}
