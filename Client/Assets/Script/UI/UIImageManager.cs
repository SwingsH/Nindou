using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// NGUI介面會使用到的Sprite名稱，請將所有NGUI會用到的介面圖名稱統整在這
/// </summary>
public enum SpriteName
{
    //  UIAtlas名稱, Sprite名稱
    [EnumUISpriteConfig("", "")]                                    NONE, // 空圖

    // 背景圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "BG_MainWorldMap")]    BG_MainWorldMap, // 主介面 - 世界地圖背景圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "BG_Generic")]         BG_Generic,      // 常用背景圖

    [EnumUISpriteConfig("Atlas_Slices", "slice_LoadingBarBG")]      SLICE_LOADING_BAR_BG, // 讀取條的背景圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_LoadingBarFG")]      SLICE_LOADING_BAR_FG, // 讀取條的前景圖

    [EnumUISpriteConfig("Atlas_Slices", "slice_parchment")]         DIALOG_FRAME, // 對話提示框    

    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_darkbrown")]   INPUT_NAME_BG, // 輸入名字的背景圖

    [EnumUISpriteConfig("Atlas_Slices", "slice_parchment")]         STAGE_FRAME, // 關卡選擇外層框

    [EnumUISpriteConfig("Atlas_Icons", "bosspic")]                  BOSS_PIC, // BOSS 圖
    [EnumUISpriteConfig("Atlas_Icons", "bossblood1")]               BOSS_HP_BG, // BOSS HP底圖
    [EnumUISpriteConfig("Atlas_Slices", "bossbloodbase")]           HP_FG, // HP的前景圖
    [EnumUISpriteConfig("Atlas_Icons", "pause")]                    PAUSE, // 暫停圖
    [EnumUISpriteConfig("Atlas_Slices", "plate")]                   ROLE_ICON_PLATE, // 戰鬥UI 放置角色Icon的背景圖版
    [EnumUISpriteConfig("Atlas_Icons", "icon_role")]                ROLE_ICON, // 角色Icon
    [EnumUISpriteConfig("Atlas_Icons", "role_hp_bg")]               ROLE_HP_BG, // 角色HP底圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "Night_Blade_1")]      MAIN_STAGE_BG, // 主介面進入探索的底圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_friend")]              ICON_FRIEND, // 主介面的好友圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_person")]              ICON_PERSON, // 主介面的人物圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_backpape")]            ICON_BAG, // 主介面的背包圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_store")]               ICON_STORE, // 主介面的商店圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_menu")]                ICON_MENU,  // 主介面的選單圖

    [EnumUISpriteConfig("Atlas_Backgrounds", "nindou_3_bg")]        WORLDMAP, // 世界地圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "shape1170")]          WORLDMAP_BG, // 主介面世界底圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "temp_nindou_bg")]     MAIN_BG, // 遊戲主要底圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_lightbrown")]  STAGE_BG_OPEN, // 選關卡的底圖(關卡開啟)
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_darkbrown")]   STAGE_BG_CLOSE, // 選關卡的底圖(關卡關閉)

    [EnumUISpriteConfig("UI_Main_Atlas", "title")]                  STAGE_TITLE_BG, // 關卡標題背景圖
    [EnumUISpriteConfig("Atlas_Icons", "complete")]                 EXPLORE_PROGRESS_BG, // 選擇子關卡的探索度



    [EnumUISpriteConfig("Atlas_Slices", "slice_button_grey")]       BTN_GENERIC_BG, // 一般的按鈕底圖
    [EnumUISpriteConfig("UI_Main_Atlas", "close")]                  BTN_CLOSE, // 關閉按鈕圖

    // TODO：改成正式用圖
    [EnumUISpriteConfig("TestAtlas", "gold")]                       POINT_PIC, // 點數指示圖
    [EnumUISpriteConfig("TestAtlas", "fb_300_main")]                BTN_INHERIT, // 繼承按鈕圖
    [EnumUISpriteConfig("TestAtlas", "Fast-forward")]               BTN_FAST_FORWARD, // 加速鈕圖
}



/// <summary>
/// NGUI的Atlas & Sprite管理器
/// </summary>
public static class UIImageManager
{
    private static Dictionary<string, UIAtlas> _uiAtlases = new Dictionary<string, UIAtlas>();
    /// <summary>
    /// 取得指定的UIAtlas，若_uiAtlas中沒有會嘗試從Resource取
    /// </summary>
    /// <param name="uiAtlasName">要取的UIAtlas名稱</param>
    /// <returns>取得的UIAtlas</returns>
    private static UIAtlas GetUIAtlas(string uiAtlasName)
    {
        if (string.IsNullOrEmpty(uiAtlasName)) { return null; }
        UIAtlas retUIAtlas;
        if (!_uiAtlases.TryGetValue(uiAtlasName, out retUIAtlas))
        {
            retUIAtlas = ResourceStation.LoadUIAtlasFromResource(uiAtlasName);
            if (retUIAtlas != null) { _uiAtlases.Add(uiAtlasName, retUIAtlas); }
        }
        return retUIAtlas;
    }

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

        UISprite retUISprite = NGUITools.AddSprite(parentObj, GetUIAtlas(uiSpriteConfig.AtlasName), uiSpriteConfig.SpriteName);
        retUISprite.MakePixelPerfect();
        return retUISprite;
    }
}
