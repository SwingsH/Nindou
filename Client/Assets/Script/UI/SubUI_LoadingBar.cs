using UnityEngine;
using System.Collections;

/// <summary>
/// 讀取條
/// </summary>
public class SubUI_LoadingBar : GUISubFormBase
{
    UISlider _progressBar; // 讀取進度條
    UILabel _progressText; // 讀取進度的數值文字

    //TODO: 將忍者奔跑動畫加上
    #region 物件建立
    public SubUI_LoadingBar(GameObject parent, string subUILoadingBarName, Vector3 relativePos, int depth)
        : base(parent, subUILoadingBarName, relativePos)
    {
        // 讀取進度條
        _progressBar = GUIStation.CreateUIProgressBar(_subUIRoot.gameObject, "Progress Bar_Build", Vector3.zero, depth,
                                                    SpriteName.SLICE_LOADING_BAR_FG,
                                                    SpriteName.SLICE_LOADING_BAR_BG,
                                                    1535, 91);
        _progressBar.foreground.localPosition = new Vector3(27, 0, 0);
        _progressBar.fullSize = new Vector2(1385, 28);
        // 讀取進度的數值文字
        _progressText = GUIStation.CreateUILabel(_subUIRoot.gameObject, "LoadingProgressText", UIWidget.Pivot.Center, new Vector3(678, -4, 0), depth + 2,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.MEDIUM, FontStyle.Bold),
            Color.white, "0%");
        _progressText.overflowMethod = UILabel.Overflow.ResizeFreely; // fs: 讓文字佔的空間自由地重新配置
        // 讀取條左邊的沙漏圖
        UISprite sandFilter = UIImageManager.CreateUISprite(_subUIRoot.gameObject, SpriteName.ICON_SANDFILTER);
        sandFilter.Init(UISprite.Type.Simple, depth+2, UIWidget.Pivot.Center, 138, 135);
        sandFilter.name = "LoadingSandFilter";
        sandFilter.transform.localPosition = Vector3.zero;  // 因為調整pivot會影響localPosition，所以需要再次重設
        // 「讀取進度」文字
        GUIStation.CreateUILabel(_subUIRoot.gameObject, "LoadingProgress", UIWidget.Pivot.Center, new Vector3(0, -7, 0), depth+3,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.LARGE, FontStyle.Bold),
            Color.white, "讀取進度");
    }
    #endregion
    #region Dispose -- 資源釋放
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            NGUITools.Destroy(_progressBar);
        }
        _progressBar = null;

        base.Dispose(disposing);
    }
    #endregion

    /// <summary>
    /// 設定讀取條的進度百分比
    /// </summary>
    public float ProgressPercent
    {
        set
        {
            _progressBar.value = value / 100.0f;
            _progressText.text = string.Format("{0:00}%", value);
        }
        get
        {
            return _progressBar.value * 100.0f;
        }
    }

}
