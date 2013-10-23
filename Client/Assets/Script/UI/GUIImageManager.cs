using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// 會使用到的常用Sprite名稱
/// </summary>
public enum SpriteName
{
    NONE, // 空圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_parchment")]         DIALOG_FRAME, // 對話提示框    
    [EnumUISpriteConfig("Atlas_Icons", "bosspic")]                  BOSS_PIC, // BOSS 圖
    [EnumUISpriteConfig("Atlas_Icons", "bossblood1")]               BOSS_HP_BG, // BOSS HP底圖
    [EnumUISpriteConfig("Atlas_Slices", "bossbloodbase")]           HP_FG, // HP的前景圖
    [EnumUISpriteConfig("TestAtlas", "Fast-forward")]               FAST_FORWARD_BTN, // 加速鈕圖
    [EnumUISpriteConfig("Atlas_Icons", "pause")]                    PAUSE, // 暫停圖
    [EnumUISpriteConfig("Atlas_Slices", "plate")]                   ROLE_ICON_PLATE, // 戰鬥UI 放置角色Icon的背景圖版
    [EnumUISpriteConfig("Atlas_Icons", "player")]                   ROLE_ICON, // 角色Icon
}



/// <summary>
/// NGUI的Atlas & Sprite管理器
/// </summary>
public class UIImageManager
{
    /// <summary>
    /// 確認傳入的Enum資料是否沒問題，此為Debug模式才使用，正式版的Enum資料必定通過此檢查
    /// </summary>
    /// <param name="sn">Sprite名稱相關資料</param>
    /// <param name="isOK">是否沒問題</param>
    [Conditional("DEVELOP_DEBUG")]
    private static void CheckInputUISpriteData(SpriteName sn, ref bool isOK)
    {
        //CommonFunction.DebugMsgFormat("Check sprite");

        isOK = false;
        EnumUISpriteConfig uiSpriteConfig;
        if (!CommonFunction.GetAttribute<EnumUISpriteConfig>(sn, out uiSpriteConfig))
        {
            CommonFunction.DebugMsgFormat("無法取到Enum({0})對應Sprite名稱資料", sn.ToString("g"));
            return;
        }

        if (string.IsNullOrEmpty(uiSpriteConfig.AtlasName) || string.IsNullOrEmpty(uiSpriteConfig.SpriteName))
        {
            CommonFunction.DebugMsgFormat("Enum({0})對應的AtlasName({1}) 、SpriteName({2})有一個為空", sn.ToString("g"), uiSpriteConfig.AtlasName, uiSpriteConfig.SpriteName);
            return;
        }

        isOK = true;
    }

    /// <summary>
    /// 依據SpriteName提供資料，在parent下面建立附加了UISprite的物件。parentObj = null，則該物件在最上層
    /// sn資料有問題，則不做事
    /// </summary>
    /// <param name="parentObj">parent物件</param>
    /// <param name="sn">存有Atlas和Sprite名字資料的Enum</param>
    /// <returns>建出的UISprite</returns>
    public static UISprite CreateUISprite(GameObject parentObj, SpriteName sn)
    {
        //CommonFunction.DebugMsgFormat("GUIImageManager.CreateUISprite()");
        bool dataIsOK = true;
        CheckInputUISpriteData(sn, ref dataIsOK);
        if (!dataIsOK) { return null; }
        //CommonFunction.DebugMsgFormat("{0} 通過測試", sn);

        // 到此步表示sn的值沒問題
        EnumUISpriteConfig uiSpriteConfig;
        CommonFunction.GetAttribute<EnumUISpriteConfig>(sn, out uiSpriteConfig);

        UISprite retUISprite = NGUITools.AddSprite(parentObj, ResourceStation.GetUIAtlas(uiSpriteConfig.AtlasName), uiSpriteConfig.SpriteName);
        retUISprite.MakePixelPerfect();
        return retUISprite;
    }
}
