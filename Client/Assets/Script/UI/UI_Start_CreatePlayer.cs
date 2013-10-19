using UnityEngine;
using System.Collections;

/// <summary>
/// 開始畫面
/// </summary>
public class UI_Start_CreatePlayer : GUIFormBase 
{
    private GameObject _uiRoot; // 最上層物件
    private UIButton _submitButton; // 確認送出

    protected override void CreateAllComponent()
    {

        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        // 登入的全畫面圖按鈕
		UIAtlas atlas = ResourceStation.GetUIAtlas("_Atlas/Atlas_Slices");
        _submitButton = GUIStation.CreateUIButton(panel.gameObject, "NameInput", new Vector3(0, 0, 0), 1,
                                atlas, "slice_button_grey",  192, 64, ResourceStation.GetUIFont("MSJH_30"), Color.white, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
