using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        _onShowFinished = null;
        onBeforeHideDelegate = null;
        _onHideFinished = null;

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
        // 剛建立好時先隱藏
        NGUITools.SetActive(gameObject, false);
    }

    #endregion

    void EnableEventReceiver(bool enable)
    {
        // 關閉UICamera的事件接收
        _guistation.UICameraEventReceiverMask = (enable) ? 1 << GLOBALCONST.LAYER_UI_BASE : 0;
        //Collider[] allCollider = GetComponentsInChildren<Collider>(true);
        //foreach (Collider oneCollider in allCollider)
        //{
        //    oneCollider.enabled = enable;
        //}
    }
    void EnableEventReceiver()
    {
        EnableEventReceiver(true);
    }

    #region 顯示/隱藏UI相關
    public delegate void FormNotifyDelegate(GUIFormBase sender);

    //開啟或關閉介面時呼叫
    private FormNotifyDelegate onBeforeShowDelegate = null;
    private FormNotifyDelegate onBeforeHideDelegate = null;

    List<EventDelegate> _onShowFinished = new List<EventDelegate>();
    List<EventDelegate> _onHideFinished = new List<EventDelegate>();

    /// <summary>
    /// 現在是否有顯示（不管看不看的到）
    /// </summary>
    public bool Visible
    {
        get { return NGUITools.GetActive(gameObject); }
    }

    public void Show()
    {
        if (onBeforeShowDelegate != null) { onBeforeShowDelegate(this); }
        // 如果有設定顯示/隱藏的tween時，使用該tween做顯示/隱藏，否則直接設定active
        GUIStation.currentShowUI = this; // 讓delegate可取得正在顯示的UI為此UI
        EnableEventReceiver(false); // 關閉事件接收
        AddShowOrHideFinishedDelegate(true, new EventDelegate(this, "EnableEventReceiver"), true); //  加入「開啟事件接收」的delegate
        if (_hasShowOrHideTween)
        {
            _uiPlayTween.SetForShowOrHideTween(); // 避免有人中途拿去播其他tween
            // 讓播完Tween之後，只呼叫對應的delegate
            _uiPlayTween.onFinished.Clear();
            _uiPlayTween.onFinished.AddRange(_onShowFinished);            
            _uiPlayTween.Play(true);
        }
        else
        {
            NGUITools.SetActive(gameObject, true);
            EventDelegate.Execute(_onShowFinished);
        }
        ClearOneShotDelegate(_onShowFinished);
        GUIStation.currentShowUI = null;
    }

    public void Hide()
    {
        if (onBeforeHideDelegate != null) { onBeforeHideDelegate(this); }
        // 如果有設定顯示/隱藏的tween時，使用該tween做顯示/隱藏，否則直接設定active
        GUIStation.currentHideUI = this; // 讓delegate可取得正在隱藏的UI為此UI
        EnableEventReceiver(false); // 關閉所有碰撞盒，避免在顯示動畫中就偵測到Click等動作
        AddShowOrHideFinishedDelegate(false, new EventDelegate(this, "EnableEventReceiver"), true); //  加入「開啟事件接收」的delegate
        if (_hasShowOrHideTween)
        {
            _uiPlayTween.SetForShowOrHideTween(); // 避免有人中途拿去播其他tween
            // 讓播完Tween之後，只呼叫對應的delegate
            _uiPlayTween.onFinished.Clear();
            _uiPlayTween.onFinished.AddRange(_onHideFinished);
            _uiPlayTween.Play(false);
        }
        else
        {
            NGUITools.SetActive(gameObject, false);
            EventDelegate.Execute(_onHideFinished);
        }
        ClearOneShotDelegate(_onHideFinished);
        GUIStation.currentHideUI = null;
    }

    public void AddBeforeShowOrHideDelegate(bool isShow, FormNotifyDelegate del)
    {
        if (isShow) { onBeforeShowDelegate += del; }
        else { onBeforeHideDelegate += del; }
    }

    public void RemoveBeforeShowOrHideDelegate(bool isShow, FormNotifyDelegate del)
    {
        if (isShow) { onBeforeShowDelegate -= del; }
        else { onBeforeHideDelegate -= del; }
    }

    /// <summary>
    /// 加入顯示/隱藏Tween結束後，要執行的EventDelegate
    /// </summary>
    /// <param name="isShow">是顯示還是隱藏Tween之後的動作</param>
    /// <param name="finishedDelegate">要執行的EventDelegate</param>
    /// <param name="fromFirst">是否從開頭插入</param>
    /// <param name="oneShot">是否為OneShot，預設是，免得多次到該介面時可能會重複添加</param>
    public void AddShowOrHideFinishedDelegate(bool isShow, EventDelegate finishedDelegate, bool fromFirst = false, bool oneShot = true)
    {
        finishedDelegate.oneShot = oneShot;
        List<EventDelegate> temp = (isShow) ? _onShowFinished : _onHideFinished;
        if (fromFirst) { temp.Insert(0, finishedDelegate); }
        else { temp.Add(finishedDelegate); }
    }

    public void RemoveShowOrFinishedDelegate(bool isShow, EventDelegate finishedDelegate)
    {
        // EventDelegate比對不考慮OneShot，故不需要特地傳入
        if (isShow) { _onShowFinished.Remove(finishedDelegate); }
        else { _onHideFinished.Remove(finishedDelegate); }
    }

    void ClearOneShotDelegate(List<EventDelegate> list)
    {
        list.RemoveAll(ed => ed.oneShot == true);
    }

    #endregion
}

/// <summary>
/// 子UI的基礎類別，為一些基本UI的集合，不能獨立產生，必須在繼承GUIFormBase的類別中產生
/// 刪除此類型物件時，記得呼叫 Dispose()函式
/// <para>By Feles</para>
/// </summary>
public class GUISubFormBase : System.IDisposable
{
    protected GameObject _subUIRoot; // 最上層物件

    #region 物件建立

#if UI_OFFLINE_TEST
    /// <summary>
    /// 建構式，只將傳入的物件設給_subUIRoot
    /// </summary>
    /// <param name="root">root</param>
    protected GUISubFormBase(GameObject root)
    {
        _subUIRoot = root;
    }
#endif

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

    /// <summary>
    /// 現在是否有顯示（不管看不看的到）
    /// </summary>
    public bool Visible
    {
        get { return NGUITools.GetActive(_subUIRoot); }
    }

    public virtual void SetVisible(bool isVisible)
    {
        NGUITools.SetActive(_subUIRoot, isVisible);
    }
}