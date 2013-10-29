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
            UITweener[] allUITweener = GetComponentsInChildren<UITweener>(true); // 即便active = false也需要找

            foreach (UITweener oneUITweener in allUITweener)
            {
                if (oneUITweener.tweenGroup == GLOBALCONST.UI_ShowOrHide_TweenGroup) { return true; }
            }
            return false;
        }
    }



    #region 固定method
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        // TODO: 讓onAfterShowDelegate()在上面事情確定完成後執行
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
        // TODO: 讓onAfterHideDelegate()在上面事情確定完成後執行
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

/// <summary>
/// 子UI的基礎類別，為一些基本UI的集合，不能獨立產生，必須在繼承GUIFormBase的類別中產生
/// 刪除此類型物件時，記得呼叫 Dispose()函式
/// By Feles
/// </summary>
public class GUISubFormBase : System.IDisposable
{
    protected GameObject _subUIRoot; // 最上層物件

    #region 物件建立

    protected GUISubFormBase(GameObject parent, string subUIName, Vector3 relativePos)
    {
        // 最上層物件
        _subUIRoot = NGUITools.AddChild(parent);
        _subUIRoot.name = subUIName;
        _subUIRoot.transform.localPosition = relativePos;
    }
    #endregion

    // http://forum.unity3d.com/threads/184069-CompareBaseObjectsInternal-error
    // TODO:? 或許改繼承自UnityEngine.Object來解決「CompareBaseObjectsInternal can only be called from the main thread.」問題比較好？
    //        但是不知道Object內藏了什麼@@

    #region Dispose -- 資源釋放
    /* 直接將物件設為null，使其呼叫解構式來釋放資源會有「CompareBaseObjectsInternal can only be called from the main thread.」問題
        * 為了解決「CompareBaseObjectsInternal can only be called from the main thread.」問題，必須在main thread執行釋放資源，
        * 所以需要繼承IDisposable並實作Disposec函式。
        * http://msdn.microsoft.com/zh-tw/library/b1yfkh5e(v=vs.90).aspx 有提供撰寫範例。 
        * 但是呼叫端在刪除此物件時，需記得呼叫 Dispose()函式。
        */
    public void Dispose()
    {
        Dispose(true);

        System.GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            NGUITools.Destroy(_subUIRoot);
        }
        _subUIRoot = null;
    }

    #endregion

}