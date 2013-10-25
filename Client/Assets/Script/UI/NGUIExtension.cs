using UnityEngine;
using System.Collections;


/// <summary>
/// NGUI 元件的擴充方法集中處
/// </summary>
public static class NGUIExtension
{
    #region UISprite

    /// <summary>
    /// 將UISprite的參數初始化
    /// </summary>
    /// <param name="sp">要修改的UISprite</param>
    /// <param name="spriteType">填圖方式</param>
    /// <param name="depth">深度</param>
    /// <param name="pivot">錨點</param>
    /// <param name="width">圖片寬</param>
    /// <param name="height">圖片高</param>
    public static void Init(this UISprite sp, UISprite.Type spriteType, int depth, UISprite.Pivot pivot, int width, int height)
    {
        sp.type = spriteType;
        sp.depth = depth;
        if (sp.pivot != pivot) 
        {
            sp.pivot = pivot;
            sp.MakePixelPerfect(); // 如果type = simple or filled 會改回近原大小，故在呼叫此函式後才修改寬高    
        }
        sp.width = width;
        sp.height = height;
    }
    #endregion
    #region UIButton
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
    #endregion
}
