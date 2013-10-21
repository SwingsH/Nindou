﻿using UnityEngine;
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
}

/// <summary>
/// NGUI使用到的UIFont管理器
/// </summary>
public class GUIFontManager
{
    private static GameObject _fontContainer = null; // 將動態字型掛在其上的GameObject
    private static Dictionary<string, UIFont> _uiDynamicFonts = new Dictionary<string, UIFont>();


    private GUIFontManager(){;}
    /// <summary>
    /// 創建存放UIFont用的GameObject
    /// </summary>
    private static void CreateFontContainerObject()
    {
        _fontContainer = NGUITools.AddChild(GameControl.Instance.GUIStation.GUICamera.gameObject); // 將存放UIFont用的GameObject放在UICamera物件下
        _fontContainer.name = "Font Container";
    }


    /// <summary>
    /// 確認傳入的資料是否沒問題，此為Debug模式才使用，正式版的Enum資料必定通過此檢查
    /// </summary>
    /// <param name="fontNameEnum">字型名稱的Enum</param>
    /// <param name="fontSizeEnum">字型大小的Enum</param>
    /// <param name="isOK">資料是否沒問題</param>
    [Conditional("DEVELOP_DEBUG")]
    private static void CheckInpuUIFontData(UIFontName fontNameEnum, UIFontSize fontSizeEnum, ref bool isOK)
    {
        CommonFunction.DebugMsgFormat("Check font");
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
        return string.Format("{0}_{1}_{2}", fontName.Value, fontSize.ToString(), fontStyle.ToString());
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
        CommonFunction.DebugMsgFormat("GUIFontManger.GETUIDynamicFont()");
        bool dataIsOK = true;
        CheckInpuUIFontData(fontNameEnum, fontSizeEnum, ref dataIsOK);
        if (!dataIsOK) { return null; }
        CommonFunction.DebugMsgFormat("{0}_{1}_{2} 通過測試", fontNameEnum, fontSizeEnum, fontStyle);
        // 到此步表示fontName的值沒問題
        StringValue fontName;
        CommonFunction.GetAttribute<StringValue>(fontNameEnum, out fontName);

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