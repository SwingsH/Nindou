using UnityEngine;
using System.Collections;


/// <summary>
/// NGUI 元件的擴充方法集中處
/// </summary>
public static class NGUIExtension
{
    #region UISprite
    /// <summary>
    /// 設定會影響圖大小的相關參數
    /// </summary>
    /// <param name="sp">要修改的UISprite</param>
    /// <param name="width">圖片寬</param>
    /// <param name="height">圖片高</param>
    /// <param name="spriteType">Sprite繪製方式</param>
    /// <param name="pivot">錨點(=null則維持原值)</param>
    public static void SetEffectSizeParameter(this UISprite sp, int width, int height, UISprite.Type spriteType , UISprite.Pivot pivot)
    {
        sp.type = spriteType;
        sp.pivot = pivot;
        sp.MakePixelPerfect(); // 如果type = simple or filled 會改回近原大小，故在呼叫此函式後才修改寬高  
        sp.width = width;
        sp.height = height;
    }

    public static void SetEffectSizeParameter(this UISprite sp, int width, int height, UISprite.Pivot pivot)
    {
        sp.SetEffectSizeParameter(width, height, sp.type, pivot);
    }

    public static void SetEffectSizeParameter(this UISprite sp, int width, int height, UISprite.Type type)
    {
        sp.SetEffectSizeParameter(width, height, type, sp.pivot);
    }

    public static void SetEffectSizeParameter(this UISprite sp, int width, int height)
    {
        sp.SetEffectSizeParameter(width, height, sp.type, sp.pivot);
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
    #region UIPlayTween
    /// <summary>
    /// 將UIPlayTween參數設定成播放ShowOrHideTween的狀態
    /// </summary>
    public static void SetForShowOrHideTween(this UIPlayTween pt)
    {
        pt.tweenGroup = GLOBALCONST.UI_ShowOrHide_TweenGroup; // 預設播放 顯示/隱藏 用的Tween Group
        pt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;  // 在disable時播放的話，將tweenTarget先enable再播放
        pt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterReverse; // 如果反向播放（預設顯示時為正向播放，隱藏時則為相同Tween反向播放），則播放完畢隱藏 
    }

    #endregion
}
