﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 用將數值用Slider顯示(EX:血條、鬥氣條...等等）的子介面
/// 血條的子介面
/// </summary>
public class SubUI_HPBar : GUISubFormBase
{

    public enum ShowMode
    {
        STATIC, // 固定位置
        WITH_AVATAR, // 跟隨avater
    }
    // Bar的顏色
    readonly Color[] barColors = new Color[] { Color.white, Color.red };

    UISprite _gameAttributeImage;
    UISlider _baseSilder; // 底下的Slider
    UISprite _frontBar; // 前層的Bar
    UISprite _backBar; // 後層的Bar
    Vector3 _followScreenPos; // 跟隨物件的螢幕座標

    ShowMode _showMode = ShowMode.STATIC; // 顯示位置的方式

    GameAttribute _gameAttribute; 

    
    int _maxDataNum=1; // 最大值
    public int MaxDataNum
    {
        get { return _maxDataNum; }
    }
    int _curDataNum=0; // 現在值
    public int CurDataNum
    {
        get { return _curDataNum; }
    }
    int _layerNum=1; // 資料條分成幾層
    public int LayerNum
    {
        get { return _layerNum; }
        set
        {
            if (value <= 0) { CommonFunction.DebugErrorFormat("LayerNum值({0})給予錯誤，不做修改", value); }
            else
            {
                _layerNum = value;
                NGUITools.SetActive(_backBar.gameObject, (_layerNum > 1));
            }
        }
    }

    /// <summary>
    /// 跟隨位置和實際所需位置的偏差值
    /// </summary>
    public Vector2 ShiftPos
    {
        set
        {
            _baseSilder.transform.localPosition = value;
        }
    }

    public Vector3 ForegroundPos
    {
        set
        {
            if (_frontBar != null) { _frontBar.transform.localPosition = value; }
            if (_backBar != null) { _backBar.transform.localPosition = value; }
        }
    }

    public Vector2 FullSize
    {
        set
        {
            _baseSilder.fullSize = value;

            _backBar.width = (int)value.x;
            _backBar.height = (int)value.y;
        }
    }
    #region 物件建立
    public SubUI_HPBar(GameObject parent, string dataSliderName, Vector3 relativePos, int depth, int width, int height, 
        GameAttribute ga = GameAttribute.NONE, ShowMode mode = ShowMode.STATIC)
        : base(parent, dataSliderName, relativePos)
    {
        _showMode = mode;
        _gameAttribute = ga;



        _baseSilder = GUIStation.CreateUISlider(_subUIRoot, "Slider", Vector3.zero, depth, NGUISpriteData.DATA_SLIDER_FG, NGUISpriteData.ROLE_HP_BG, 
            NGUISpriteData.NONE, width, height, false);
        _baseSilder.foreground.localPosition = new Vector3(21, 4, 0);
        _baseSilder.fullSize = new Vector2(width, height);

        _frontBar = _baseSilder.foreground.gameObject.GetComponent<UISprite>();
        _frontBar.color = Color.red;

        _gameAttributeImage = UIImageManager.CreateUISprite(new GORelativeInfo(_baseSilder.gameObject, "AttributeImage"),
            new UISpriteInfo(_gameAttribute.GetCorrespondingNGUISpriteData(), 62, 61));

        
        // 多層血條
        _backBar = UIImageManager.CreateUISprite(new GORelativeInfo(_baseSilder.gameObject, new Vector3(21, 4, 0), "BackBar"),
            new UISpriteInfo(NGUISpriteData.DATA_SLIDER_FG, width, height, depth - 1, pivot: UIWidget.Pivot.Left));
        NGUITools.SetActive(_backBar.gameObject, false);
    }
    #endregion
    #region Dispose -- 資源釋放
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            NGUITools.Destroy(_gameAttributeImage);
            NGUITools.Destroy(_backBar);
            NGUITools.Destroy(_frontBar);
            NGUITools.Destroy(_baseSilder);
        }
        _gameAttributeImage = null;
        _backBar = null;
        _frontBar = null;
        _baseSilder = null;

        base.Dispose(disposing);
    }
    #endregion

    /// <summary>
    /// 設定資料數值
    /// </summary>
    /// <param name="curHP">現在值，若小於0，設成0，若大於最大值（修改後），設成最大值</param>
    /// <param name="maxHP">最大值，若為null或小於等於0，則不做修改</param>
    public void SetHP(int curHP, int? maxHP = null)
    {
        if (maxHP.HasValue)
        {
            if (maxHP.Value <= 0) { CommonFunction.DebugErrorFormat("MaxDataNum值({0})給予錯誤，不做修改", maxHP.Value); }
            else { _maxDataNum = maxHP.Value; }
        }
        // 限制cur值在0~_maxDataNum
        int realCur = Mathf.Clamp(curHP, 0, _maxDataNum);
        _curDataNum = realCur;
        SetCurrentUIState();
    }

    /// <summary>
    /// 依據現在的_maxDataNum、_curDataNum、_layerNum，設定應該顯示的樣子
    /// </summary>
    void SetCurrentUIState()
    {
        // _layerNum = 1的情形
        _baseSilder.value = (float)_curDataNum / (float)_maxDataNum;
    }

    /// <summary>
    /// 依據傳入的新跟隨螢幕位置，更新血條位置
    /// </summary>
    /// <param name="newFollowScreenPos">新的更新螢幕位置</param>
    public void UpdatePos(Vector3 newFollowScreenPos)
    {
        if (_showMode == ShowMode.WITH_AVATAR)
        {
            _followScreenPos = newFollowScreenPos;
            Vector3 uiScreenPos = GameControl.Instance.GUIStation.GUICamera.ScreenToWorldPoint(_followScreenPos);
            uiScreenPos.z = 0;
            _subUIRoot.transform.position = uiScreenPos;
        }
    }
}
