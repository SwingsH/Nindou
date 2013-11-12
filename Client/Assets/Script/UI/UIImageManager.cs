using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// NGUI介面會使用到的Sprite資料（包含所在的UIAtlas和Sprite名稱），請將所有NGUI會用到的介面圖名稱統整在這
/// </summary>
public enum NGUISpriteData
{
    //  UIAtlas名稱, Sprite名稱
    [EnumUISpriteConfig("", "")]                                    NONE, // 空圖

    // 背景圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "BG_MainWorldMap")]    BG_MainWorldMap, // 主介面 - 世界地圖背景圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "BG_Generic")]         BG_Generic,      // 常用背景圖

    [EnumUISpriteConfig("Atlas_Slices", "slice_LoadingBarBG")]      SLICE_LOADING_BAR_BG, // 讀取條的背景圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_LoadingBarFG")]      SLICE_LOADING_BAR_FG, // 讀取條的前景圖

    [EnumUISpriteConfig("Atlas_Icons", "icon_SandFilter")]          ICON_SANDFILTER, // 讀取條左邊的沙漏圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_loading_anime_1")]     ICON_LOADING_ANIME, // 讀取條的忍者跑步動畫圖（第一張）

    [EnumUISpriteConfig("Atlas_Slices", "slice_parchment")]         DIALOG_FRAME, // 對話提示框    

    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_darkbrown")]   INPUT_NAME_BG, // 輸入名字的背景圖

    [EnumUISpriteConfig("Atlas_Slices", "slice_frame")]             STAGE_FRAME, // 關卡選擇外層框
    [EnumUISpriteConfig("Atlas_Slices", "slice_parchment")]         STAGE_BG, // 關卡選擇背景圖

    [EnumUISpriteConfig("Atlas_Icons", "bosspic")]                  BOSS_PIC, // BOSS 圖
    [EnumUISpriteConfig("Atlas_Icons", "bossblood1")]               BOSS_HP_BG, // BOSS HP底圖
    [EnumUISpriteConfig("Atlas_Slices", "bossbloodbase")]           HP_FG, // HP的前景圖
    [EnumUISpriteConfig("Atlas_Slices", "plate")]                   ROLE_ICON_PLATE, // 戰鬥UI 放置角色Icon的背景圖版
    [EnumUISpriteConfig("Atlas_Icons", "icon_role")]                ROLE_ICON, // 角色Icon

    [EnumUISpriteConfig("Atlas_Backgrounds", "Night_Blade_1")]      MAIN_STAGE_BG, // 主介面進入探索的底圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_friend")]              ICON_FRIEND, // 主介面的好友圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_person")]              ICON_PERSON, // 主介面的人物圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_backpape")]            ICON_BAG, // 主介面的背包圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_store")]               ICON_STORE, // 主介面的商店圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_menu")]                ICON_MENU,  // 主介面的選單圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_em")]                  ICON_EM_WARNING, // 主介面的「強敵發現」警告底圖

    [EnumUISpriteConfig("Atlas_Icons", "icon_gamegold")]            ICON_GAME_GOLD, // 遊戲幣底圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_mallgold")]            ICON_MALL_GOLD, // 商城幣底圖

    [EnumUISpriteConfig("Atlas_Backgrounds", "nindou_3_bg")]        WORLDMAP, // 世界地圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "shape1170")]          WORLDMAP_BG, // 主介面世界底圖
    [EnumUISpriteConfig("Atlas_Backgrounds", "scene_battle_bamboo_UP")]     MAIN_BG, // 遊戲主要底圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_lightbrown")]  STAGE_BG_OPEN, // 選關卡的底圖(關卡開啟)
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_darkbrown")]   STAGE_BG_CLOSE, // 選關卡的底圖(關卡關閉)

    [EnumUISpriteConfig("UI_Main_Atlas", "title")]                  STAGE_TITLE_BG, // 關卡標題背景圖
    [EnumUISpriteConfig("Atlas_Icons", "complete")]                 EXPLORE_PROGRESS_BG, // 選擇子關卡的探索度

    [EnumUISpriteConfig("Atlas_Slices", "slice_bgcbase")]           SLICE_BAG_FRAME_BG, // 背包物品圖的底框
    [EnumUISpriteConfig("Atlas_Slices", "slice_select")]            SLICE_BAG_ITEM_SELECT, // 背包物品圖選擇後的底框
    [EnumUISpriteConfig("Atlas_Icons", "icon_confirm")]             BTN_CONFIRM,            // 確認圖示型按鈕
    [EnumUISpriteConfig("Atlas_Icons", "icon_previous")]            BTN_PREVIOUS,           // 返回圖示型按鈕
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_darkbrown")]   SLICE_BAG_TITLE,        // 選關卡的底圖(關卡關閉)

    [EnumUISpriteConfig("Atlas_Slices", "slice_button_grey")]       BTN_GENERIC_BG, // 一般的按鈕底圖
    [EnumUISpriteConfig("UI_Main_Atlas", "close")]                  BTN_CLOSE, // 關閉按鈕圖

    #region 戰鬥前讀取介面用圖
    [EnumUISpriteConfig("Atlas_Icons", "tile_bg_pattern")]          TILE_BG_PATTERN, // 要鋪成背景圖的基本圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_boss_before_battle")]  ICON_BOSS_BEFORE_BATTLE, // BOSS底圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_frame_lightbrown")]  ROLE_INFO_BASE, // 角色資訊底圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_equip")]               ICON_EQUIP_BG, // 裝備底圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_equipwbg")]            ICON_EQUIPPED_TEXT_BG, // 「已裝備」文字底圖
    #endregion
    #region 戰鬥介面用圖
    [EnumUISpriteConfig("Atlas_Icons", "role_hp_bg")]               ROLE_HP_BG, // 角色HP底圖
    [EnumUISpriteConfig("Atlas_Slices", "slice_blood_white_obase")] DATA_SLIDER_FG, // 資料Slider的前景圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_sun")]                 ICON_ATTRIBUTE_SUN, // 屬性：陽的圖示
    [EnumUISpriteConfig("Atlas_Icons", "icon_moon")]                ICON_ATTRIBUTE_MOON, // 屬性：陰的圖示
    [EnumUISpriteConfig("Atlas_Icons", "icon_man")]                 ICON_ATTRIBUTE_MAN, // 屬性：體的圖示
    [EnumUISpriteConfig("Atlas_Icons", "icon_pause")]               ICON_PAUSE, // 暫停圖
    [EnumUISpriteConfig("Atlas_Icons", "icon_play")]                ICON_PLAY, // 播放圖

    #endregion
    #region UI_ShowLogo
    [EnumUISpriteConfig("Atlas_Backgrounds_2", "BG_Logo")] LOGO, // LOGO圖
    #endregion
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
    private const string ATLAS_CARD_MATERIAL    = "Atlas_CardIconMaterials";    // 合成材料圖's Atlas
    private const string ATLAS_CARD_WEAPONS     = "Atlas_CardIconWeapons";      // 武器圖's Atlas

    private const string ICON_CARD_MATERIAL = "cardicon_material_{0:000}";  // 合成材料圖
    private const string ICON_CARD_WEAPONS  = "cardicon_weapon_{0:000}";    // 武器圖

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
    private static void CheckInputUISpriteData(NGUISpriteData sn, ref bool isOK)
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

    [System.Obsolete("已過時，請改用CreateUISprite(GORelativeInfo, UISpriteInfo)")]
    /// <summary>
    /// 依據SpriteName提供資料，在parent下面建立附加了UISprite的物件。parentObj = null，則該物件在最上層
    /// sn資料有問題，則不做事
    /// </summary>
    /// <param name="parentObj">parent物件</param>
    /// <param name="sn">存有Atlas和Sprite名字資料的Enum</param>
    /// <returns>建出的UISprite</returns>
    public static UISprite CreateUISprite(GameObject parentObj, NGUISpriteData sn)
    {
        return CreateUISprite(new GORelativeInfo(parentObj), new UISpriteInfo(sn));
    }

    /// <summary>
    /// 依據SpriteName提供資料，建立附加了UISprite的物件。
    /// </summary>
    /// <param name="goInfo">和GameObject相關的資訊（父物件、相對位置、名字）</param>
    /// <param name="spriteInfo">和UISprite相關的資訊</param>
    /// <returns>建出的UISprite</returns>
    public static UISprite CreateUISprite(GORelativeInfo goInfo, UISpriteInfo spriteInfo)
    {
        // 如果NGUISpriteData為None，則表示不應該產生UISprite，回傳null
        if (spriteInfo.SpriteResourceData == NGUISpriteData.NONE) { return null; }
        bool dataIsOK = true;
        CheckInputUISpriteData(spriteInfo.SpriteResourceData, ref dataIsOK);
        if (!dataIsOK) { return null; }

        // 到此步表示spriteInfo.SpriteResourceData資料沒問題
        EnumUISpriteConfig uiSpriteConfig;
        CommonFunction.GetAttribute<EnumUISpriteConfig>(spriteInfo.SpriteResourceData, out uiSpriteConfig);

        UISprite retUISprite = NGUITools.AddSprite(goInfo.ParentObject, GetUIAtlas(uiSpriteConfig.AtlasName), uiSpriteConfig.SpriteName);
        retUISprite.name = string.IsNullOrEmpty(goInfo.ObjectName) ? CommonFunction.GetName<UISprite>() : goInfo.ObjectName;
        retUISprite.depth = spriteInfo.Depth.HasValue ? spriteInfo.Depth.Value : NGUITools.CalculateNextDepth(goInfo.ParentObject);

        if (spriteInfo.Type.HasValue) { retUISprite.type = spriteInfo.Type.Value; }
        retUISprite.pivot = spriteInfo.Pivot;
        retUISprite.MakePixelPerfect(); // 如果type = simple or filled 會改回近原大小，故在呼叫此函式後才修改寬高  
        if (spriteInfo.Width > 0) { retUISprite.width = spriteInfo.Width; }
        if (spriteInfo.Height > 0) { retUISprite.height = spriteInfo.Height; }
        retUISprite.transform.localPosition = goInfo.LocalPosition; // 修改pivot會更動localPosition，故在修改後才改

        return retUISprite;
    }
	
    // 取得 武器圖 icon
    public static UISprite CreateWeaponIconSprite(GameObject parentObj, int id)
    {
        UIAtlas atlas = GetUIAtlas(ATLAS_CARD_WEAPONS);
        string name = string.Format(ICON_CARD_WEAPONS, id);

        if (atlas == null)
        {
            CommonFunction.DebugError(" CreateWeaponIconSprite. atlas null.");
            return null;
        }

        UISprite retUISprite = NGUITools.AddSprite(parentObj, atlas, name);

        if (atlas == null)
        {
            CommonFunction.DebugError(" CreateWeaponIconSprite. image null.");
            return null;
        }

        retUISprite.MakePixelPerfect();
        return retUISprite;
    }
	
    // 取得 合成材料圖 icon
    public static UISprite CreateMaterialIconSprite(GameObject parentObj, int id)
    {
        UIAtlas atlas = GetUIAtlas(ATLAS_CARD_MATERIAL);
        string name = string.Format(ICON_CARD_MATERIAL, id);

        UISprite retUISprite = NGUITools.AddSprite(parentObj, atlas, name);
        retUISprite.MakePixelPerfect();
        return retUISprite;
    }

    /// <summary>
    /// 從Enum NGUISpriteData中取得對應的Sprite名稱
    /// </summary>
    public static string GetSpriteName(this NGUISpriteData sprite)
    {
        EnumUISpriteConfig uiSpriteConfig;
        // 無法取得資料，回傳空字串
        if (!CommonFunction.GetAttribute<EnumUISpriteConfig>(sprite, out uiSpriteConfig)) { return string.Empty; }

        return string.IsNullOrEmpty(uiSpriteConfig.SpriteName) ? string.Empty : uiSpriteConfig.SpriteName;
    }
}
