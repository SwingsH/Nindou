using UnityEngine;
using System.Collections;

/// <summary>
/// GUI Form的基礎類別，每個UI都繼承自此
/// By Feles
/// </summary>
public abstract class GUIFormBase : MonoBehaviour 
{
    // 因為NGUI內部的EventDelegate只接受MonoBehaviour，故多用一個delegate
    // 使得按鈕被點擊時可以呼叫非MonoBehaviour的methods
    public delegate void BtnClick();

    protected GUIStation _guistation; // 介面管理者
    protected UIPlayTween _uiPlayTween; // 用來播放Tween的元件
    // 是否有顯示/消失的Tween
    protected virtual bool _hasShowOrHideTween
    {
        get
        {
            UITweener[] allUITweener = GetComponentsInChildren<UITweener>();

            foreach (UITweener oneUITweener in allUITweener)
            {
                if (oneUITweener.tweenGroup == GLOBALCONST.UI_ShowOrHide_TweenGroup) {return true;}
            }
            return false;
        }
    }



    #region 固定method
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    /// <summary>
    /// 刪除時必定呼叫，繼承者Override時記得要留base.OnDestroy()
	/// </summary>
    protected virtual void OnDestroy()
    {
        onBeforeShowDelegate = null;
        onAfterShowDelegate = null;
        onBeforeHideDelegate = null;
        onAfterHideDelegate = null;

        NGUITools.Destroy(_uiPlayTween);
        _uiPlayTween = null;
        _guistation = null;
    }
    #endregion

    #region 建立UI相關

    /// <summary>
    /// 建立UI所有元件
    /// </summary>
    protected abstract void CreateAllComponent();

    /// <summary>
    /// 建立此UI
    /// </summary>
    /// <param name="guistation">UI管理者</param>
    public void CreateUI(GUIStation guistation)
    {
        _guistation = guistation;
        // 建立播放Tween的元件
        _uiPlayTween = GUIComponents.AddPlayShowTweenComponent(gameObject);

        CreateAllComponent();
    }

    #endregion
    #region 顯示/隱藏UI相關
    public delegate void FormNotifyDelegate(GUIFormBase sender);

    //開啟或關閉介面時呼叫
    private FormNotifyDelegate onBeforeShowDelegate = null;
    private FormNotifyDelegate onAfterShowDelegate = null;
    private FormNotifyDelegate onBeforeHideDelegate = null;
    private FormNotifyDelegate onAfterHideDelegate = null;


    public bool Visible
    {
        get { return NGUITools.GetActive(gameObject); }
    }

    public void Show()
    {
        if (onBeforeShowDelegate != null) { onBeforeShowDelegate(this); }
        // 如果有設定顯示/隱藏的tween時，使用該tween做顯示/隱藏，否則直接設定active
        if (_hasShowOrHideTween)
        {
            _uiPlayTween.tweenGroup = GLOBALCONST.UI_ShowOrHide_TweenGroup; // 避免有人中途拿去播其他tween
            _uiPlayTween.Play(true);
        }
        else
        {
            NGUITools.SetActive(gameObject, true);
        }
        if (onAfterShowDelegate != null) { onAfterShowDelegate(this); }
    }

    public void Hide()
    {
        if (onBeforeHideDelegate != null) { onBeforeHideDelegate(this); }
        // 如果有設定顯示/隱藏的tween時，使用該tween做顯示/隱藏，否則直接設定active
        if (_hasShowOrHideTween)
        {
            _uiPlayTween.tweenGroup = GLOBALCONST.UI_ShowOrHide_TweenGroup; // 避免有人中途拿去播其他tween
            _uiPlayTween.Play(false);
        }
        else
        {
            NGUITools.SetActive(gameObject, false);
        }
        if (onAfterHideDelegate != null) { onAfterHideDelegate(this); }
    }

    public void AddFormShowDelegate(bool isBefore, FormNotifyDelegate del)
    {
        if (isBefore) { onBeforeShowDelegate += del; }
        else { onAfterShowDelegate += del; }
    }
    public void RemoveFormShowDelegate(bool isBefore, FormNotifyDelegate del)
    {
        if (isBefore) { onBeforeShowDelegate -= del; }
        else { onAfterShowDelegate -= del; }
    }
    public void AddFormHideDelegate(bool isBefore, FormNotifyDelegate del)
    {
        if (isBefore) { onBeforeHideDelegate += del; }
        else { onAfterHideDelegate += del; }
    }
    public void RemoveFormHideDelegate(bool isBefore, FormNotifyDelegate del)
    {
        if (isBefore) { onBeforeHideDelegate -= del; }
        else { onAfterHideDelegate -= del; }
    }
    #endregion
}
