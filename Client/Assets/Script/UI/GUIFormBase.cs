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
        onShowDelegate = null;
        onHideDelegate = null;
    }
    #endregion

    #region 建立UI相關

    /// <summary>
    /// 建立UI所有元件
    /// </summary>
    protected abstract void CreateAllComponent(Camera uiCamera);

    /// <summary>
    /// 建立此UI
    /// </summary>
    /// <param name="uiCamera">看此UI的攝影機</param>
    public void CreateUI(Camera uiCamera)
    {
        CreateAllComponent(uiCamera);
    }
    #endregion
    #region 顯示/隱藏UI相關
    public delegate void FormNotifyDelegate(GUIFormBase sender);

    //開啟或關閉介面時呼叫
    private FormNotifyDelegate onShowDelegate = null;
    private FormNotifyDelegate onHideDelegate = null;


    public bool Visible
    {
        get { return NGUITools.GetActive(gameObject); }
    }

    public void Show()
    {
        NGUITools.SetActive(gameObject, true);
        if (onShowDelegate != null) { onShowDelegate(this); }
    }

    public void Hide()
    {
        NGUITools.SetActive(gameObject, false);
        if (onHideDelegate != null) { onHideDelegate(this); }
    }

    public void AddFormShowDelegate(FormNotifyDelegate del)
    {
        onShowDelegate += del;
    }
    public void RemoveFormShowDelegate(FormNotifyDelegate del)
    {
        onShowDelegate -= del;
    }
    public void AddFormHideDelegate(FormNotifyDelegate del)
    {
        onHideDelegate += del;
    }
    public void RemoveFormHideDelegate(FormNotifyDelegate del)
    {
        onHideDelegate -= del;
    }
    #endregion
}
