using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


///// <summary>
///// 介面Type定義
///// </summary>
//public enum UIFormType
//{
//    None = 0,  // 0. 無
//    Start = 1, // 1. 開始介面
//}


/// <summary>
/// 遊戲中的UI管理器，先認定都是2D介面
/// By Feles
/// </summary>
public class GUIStation
{
    private GameControl _gameControl = null;
    private UIRoot _uiRoot = null;

    #region UI攝影機相關
    private Camera _camera = null;  // 顯示UI用的Camera
    private UICamera _uiCamera = null; // 為了事件處理使用

    /// <summary>
    /// 顯示UI用的攝影機
    /// </summary>
    public Camera GUICamera
    {
        get { return _camera; }
    }

    /// <summary>
    /// UICamera的深度
    /// </summary>
    public float UICameraDepth
    {
        set { if (_camera != null) { _camera.depth = value; } }
    }
    #endregion

    private Dictionary<Type, GUIFormBase> _guiReference; // GUIForm參照儲存
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="control"></param>
    public GUIStation(GameControl control)
    {
        _gameControl = control;
        #region 建立必要元件
        // 如果場景上有兩個以上的UIRoot，會管理不到，請將多餘的刪除
        _uiRoot = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
        if (_uiRoot == null)
        {
            // 沒有的話建立Simple2D的UI Root
            GameObject rootObj = new GameObject("UI Root (2D)");
            GameObject.DontDestroyOnLoad(rootObj);
            _uiRoot = rootObj.AddComponent<UIRoot>();
            _uiRoot.scalingStyle = UIRoot.Scaling.FixedSizeOnMobiles;
            _uiRoot.manualHeight = 1080; // TODO:先設死，之後測試用 Screen.height;
        }

        _uiRoot.gameObject.layer = GLOBALCONST.LAYER_UI_BASE;
        // 如果場景上有兩個以上的UICamera會管理不到，請將多餘的刪除
        _uiCamera = GameObject.FindObjectOfType(typeof(UICamera)) as UICamera;
        // 取得UICamera attach上的物件的camera
        //Camera cam;
        if (_uiCamera == null)
        {   // 沒有UICamera attach的物件，則建立一個新物件並且attach 一個Camera 
            _camera = NGUITools.AddChild<Camera>(_uiRoot.gameObject);
            _uiCamera = _camera.gameObject.AddComponent<UICamera>();
        }
        else
        {
            // 將UICamera移到UIRoot底下
            _uiCamera.transform.parent = _uiRoot.transform; 
            // 有UICamera attach的物件，取得該物件上的Camera，若無，attach一個
            _camera = _uiCamera.gameObject.GetComponent<Camera>();
            if (_camera == null) { _camera = _uiCamera.gameObject.AddComponent<Camera>(); }
        }
        // 調整camera參數
        _camera.depth = 0;
        _camera.backgroundColor = Color.grey;
        _camera.cullingMask = 1 << GLOBALCONST.LAYER_UI_BASE;

        _camera.orthographicSize = 1f;
        _camera.orthographic = true;
        _camera.nearClipPlane = -2f;
        _camera.farClipPlane = 2f;
        _camera.clearFlags = CameraClearFlags.Depth;
        #endregion

        _guiReference = new Dictionary<Type, GUIFormBase>();

    }
    /// <summary>
    /// 解構式
    /// </summary>
    ~GUIStation()
    {
        NGUITools.Destroy(_uiCamera);
        _uiCamera = null;
        NGUITools.Destroy(_camera);
        _camera = null;
        NGUITools.Destroy(_uiRoot);
        _uiRoot = null;

        _guiReference = null;
        _gameControl = null;
    }

    /// <summary>
    /// 重新設定場景上所有攝影機，將其cullingMask避開NGUI使用的layer（目前是GLOBALCONST.LAYER_UI_BASE）
    /// 並且重設看UI用的Camera的深度，使UI在最上層
    /// </summary>
    public void ResetAllCamera()
    {
        float depth = -1f;
        int mask = 1 << GLOBALCONST.LAYER_UI_BASE;

        Camera[] allCamera = GameObject.FindObjectsOfType(typeof(Camera)) as Camera[];
        foreach (Camera c in allCamera)
        {
            depth = Mathf.Max(depth, c.depth);
            // 並非只是看UI的攝影機，去掉看ui那層layer
            if (c.cullingMask != mask) { c.cullingMask = c.cullingMask & ~mask; }
        }
        UICameraDepth = depth + 1;   
    }

    #region UI取得相關
    /// <summary>
    /// 傳入 UI Class 型別，取得實體介面
    /// </summary>
    /// <typeparam name="T">UI Class 型別</typeparam>
    /// <returns>實體化之介面</returns>
    public T Form<T>() where T : GUIFormBase
    {
        GUIFormBase retUI;

        if (_guiReference == null) { _guiReference = new Dictionary<Type, GUIFormBase>(); }

        if (!_guiReference.TryGetValue(typeof(T), out retUI))
        {
            retUI = InstantiateContainer(typeof(T));
            retUI.CreateUI(this);
        }
        return (retUI as T);
    }

    /// <summary>
    /// 實體化介面容器（該UI的Root，該UI所有建立的GameObject都放置在其下）
    /// </summary>
    /// <param name="uiType">ui的type</param>
    /// <returns>該UI</returns>
    private GUIFormBase InstantiateContainer(Type uiType)
    {
        GUIFormBase retUI = null;
        _guiReference.TryGetValue(uiType, out retUI);
        if (retUI != null)
        {
            CommonFunction.DebugMsgFormat(" InstantiateContainer failed. 已經實體化過介面: {0}", uiType.ToString());
            return null;
        }
        string componentName = uiType.ToString();
        GameObject uiObject = NGUITools.AddChild(_camera.gameObject);
        uiObject.name = componentName + "Object";
        retUI = uiObject.AddComponent(componentName) as GUIFormBase;
        if (retUI == null)
        {
            CommonFunction.DebugMsgFormat(" AddComponent failed. 介面: {0}", componentName);
            return retUI;
        }

        _guiReference.Add(uiType, retUI);
        return retUI;
    }
    #endregion
    public void TestDestroy<T>()
    {
         GUIFormBase deUI;

         if (_guiReference.TryGetValue(typeof(T), out deUI))
         {
             _guiReference.Remove(typeof(T));
             NGUITools.Destroy(deUI.gameObject);
         }
         deUI = null;
    }

}
