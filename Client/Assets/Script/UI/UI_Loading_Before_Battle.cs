using UnityEngine;
using System.Collections;

/// <summary>
/// 進入戰鬥前的讀取介面
/// </summary>
public class UI_Loading_Before_Battle: GUIFormBase
{
    const int ROLE_INFO_X = -151;
    const int ROLE_INFO_Y_BASE = 421;
    const int ROLE_INFO_Y_PADDING = -233;

    const int ROLE_INFO_BG_WIDTH = 1133;
    const int ROLE_INFO_BG_HEIGHT = 162;

    const int ROLE_ICON_BG_WIDTH = 220;
    const int ROLE_ICON_BG_HEIGHT = 219;

    readonly Vector3 LOADING_BAR_POS = new Vector3(-685, 0, 0); // new Vector3(-685, -464, 0);

    SubUI_LoadingBar _loadingBar; // 讀取進度條


    #region 繼承自GUIFormBase的method
    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

#if KOREAN_GSTAR

        // 讀取進度條
        _loadingBar = new SubUI_LoadingBar(panel.gameObject, "SubUI_LoadingBar", LOADING_BAR_POS, 1);
#else
        UISprite background = UIImageManager.CreateUISprite(new GORelativeInfo(panel.gameObject, "Background"),
            new UISpriteInfo(NGUISpriteData.TILE_BG_PATTERN, GUIStation.MANUAL_SCREEN_WIDTH, GUIStation.MANUAL_SCREEN_HEIGHT, 0, UISprite.Type.Tiled, UIWidget.Pivot.Center));

        // 讀取進度條
        _loadingBar = new SubUI_LoadingBar(background.gameObject, "SubUI_LoadingBar", LOADING_BAR_POS, 1);
#endif


        // TODO : G-STAR 先不需要
        //// Boss圖
        //UISprite bossBG = UIImageManager.CreateUISprite(background.gameObject, NGUISpriteData.ICON_BOSS_BEFORE_BATTLE);
        //bossBG.name = "bossBG";
        //bossBG.SetEffectSizeParameter(UISprite.Type.Sliced, UIWidget.Pivot.Center,
        //    (int)(bossBG.GetAtlasSprite().width * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
        //    (int)(bossBG.GetAtlasSprite().height * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI));

        //// 角色資訊
        //for (int roleIndex = 0; roleIndex < GLOBALCONST.MAX_BATTLE_ROLE_COUNT; ++roleIndex)
        //{
        //    GameObject roleInfo = NGUITools.AddChild(background.gameObject);
        //    roleInfo.name = string.Format("Role_Info_{0}", roleIndex + 1);
        //    roleInfo.transform.localPosition = new Vector3(ROLE_INFO_X, ROLE_INFO_Y_BASE + roleIndex * ROLE_INFO_Y_PADDING, 0);
        //    // 角色資訊底圖
        //    UISprite roleInfoBG = UIImageManager.CreateUISprite(roleInfo, NGUISpriteData.ROLE_INFO_BASE);
        //    roleInfoBG.name = "Role_Info_BG";
        //    roleInfoBG.depth = 1;
        //    roleInfoBG.SetEffectSizeParameter(UISprite.Type.Sliced, UISprite.Pivot.Left, ROLE_INFO_BG_WIDTH, ROLE_INFO_BG_HEIGHT);
        //    roleInfoBG.transform.localPosition = Vector3.zero;
        //    // 角色圖示
        //    UISprite roleIconBG = UIImageManager.CreateUISprite(roleInfo, NGUISpriteData.ROLE_ICON);
        //    roleIconBG.name = "Role_Icon_BG";
        //    roleIconBG.depth = 2;
        //    roleIconBG.SetEffectSizeParameter(UISprite.Type.Sliced, UIWidget.Pivot.Center, ROLE_ICON_BG_WIDTH, ROLE_ICON_BG_HEIGHT);
        //    roleIconBG.transform.localPosition = new Vector3(-17, 0, 0);
        //    // 裝備
        //    for (int equipIndex = 0; equipIndex < GLOBALCONST.MAX_EQUIP_COUNT; ++equipIndex)
        //    {
        //        // 裝備底圖
        //        UISprite equipBG = UIImageManager.CreateUISprite(roleInfoBG.gameObject, NGUISpriteData.ICON_EQUIP_BG);
        //        equipBG.name = string.Format("icon_equip_{0}", equipIndex + 1);
        //        equipBG.depth = 3;
        //        equipBG.SetEffectSizeParameter(UISprite.Type.Sliced, UIWidget.Pivot.Center,
        //            (int)(equipBG.GetAtlasSprite().width * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
        //            (int)(equipBG.GetAtlasSprite().height * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI));
                
        //    }
        //}

        AddBeforeShowOrHideDelegate(false, (sender)=> _loadingBar.ProgressPercent = 0);
    }

    
    #endregion
    #region 固定函式
    // Use this for initialization
	void Start () 
    {
#if UI_OFFLINE_TEST
        UISlider[] sliders = FindObjectsOfType(typeof(UISlider)) as UISlider[];
        foreach (UISlider slider in sliders)
        {
            if (slider.name.Equals("LoadingBar"))
            {
                _loadingBar = new SubUI_LoadingBar(slider.transform.parent.gameObject);
                break;
            }
        }
#endif
	}
	
	// Update is called once per frame
	void Update () 
    {
    }

    protected override void OnDestroy()
    {
        if (_loadingBar != null)
        {
            _loadingBar.Dispose();
            _loadingBar = null;
        }
        base.OnDestroy();
    }
    #endregion
    /// <summary>
    /// 設定讀取條的進度百分比
    /// </summary>
    public float ProgressPercent
    {
        set { _loadingBar.ProgressPercent = value; }
        get { return _loadingBar.ProgressPercent; }
    }

}
