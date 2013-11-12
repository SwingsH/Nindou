using UnityEngine;
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
    // Bar的顏色(五層血條顏色：紅、黃、藍、橘、綠）
    readonly Color[] barColors = new Color[GLOBALCONST.MAX_LAYER_OF_SUBUI_HP] { Color.red, Color.yellow, Color.blue, CommonFunction.Color256Bit(231,101,26,255), Color.green};
    
    UISprite _gameAttributeImage;
    UISlider _baseSilder; // 底下的Slider
    UISprite _frontBar; // 前層的Bar
    UISprite _backBar; // 後層的Bar
    Vector3 _followScreenPos; // 跟隨物件的螢幕座標

    ShowMode _showMode = ShowMode.STATIC; // 顯示位置的方式

    GameAttribute _gameAttribute;

    int _backDepth; // 下面那層血的depth

    bool _sliderForegroundIsFrontBar = true; // slider的foreground是否為_frontBar

    int _oneLayerMaxHP;
    int _maxHP = 1; // 最大值
    public int MaxHP
    {
        get { return _maxHP; }
        set
        {
            if (value <= 0) { CommonFunction.DebugErrorFormat("MaxHP值({0})給予錯誤，不做修改", value); }
            else
            {
                _maxHP = value;
                _oneLayerMaxHP = _maxHP / _layerNum;
            }
        }
    }
    int _curHP=0; // 現在值
    public int CurHP
    {
        get { return _curHP; }
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
                _layerNum = Mathf.Clamp(value, 1, barColors.Length);
                _oneLayerMaxHP = _maxHP / _layerNum;
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

    Vector2 _fullSize;
    public Vector2 FullSize
    {
        set
        {
            _fullSize = value;
            _baseSilder.fullSize = _fullSize;
            if (_sliderForegroundIsFrontBar)
            {
                _backBar.SetEffectSizeParameter((int)_fullSize.x, (int)_fullSize.y);
            }
            else
            {
                _frontBar.SetEffectSizeParameter((int)_fullSize.x, (int)_fullSize.y);
            }
            //_backBar.width = (int)_fullSize.x;
            //_backBar.height = (int)_fullSize.y;
        }
    }
    #region 物件建立
    public SubUI_HPBar(GameObject parent, string dataSliderName, Vector3 relativePos, int depth, int width, int height, int layerNum = 1,
        GameAttribute ga = GameAttribute.NONE, ShowMode mode = ShowMode.STATIC)
        : base(parent, dataSliderName, relativePos)
    {
        _showMode = mode;
        _gameAttribute = ga;
        LayerNum = layerNum;

        _baseSilder = GUIStation.CreateUISlider(_subUIRoot, "Slider", Vector3.zero, depth, NGUISpriteData.DATA_SLIDER_FG, NGUISpriteData.ROLE_HP_BG, 
            NGUISpriteData.NONE, width, height, false);
        _baseSilder.foreground.localPosition = new Vector3(21, 4, 0);
        _baseSilder.fullSize = new Vector2(width, height);

        _frontBar = _baseSilder.foreground.gameObject.GetComponent<UISprite>();
        _frontBar.color = barColors[0];
        _frontBar.depth += 1;

        _gameAttributeImage = UIImageManager.CreateUISprite(new GORelativeInfo(_baseSilder.gameObject, "AttributeImage"),
            new UISpriteInfo(_gameAttribute.GetCorrespondingNGUISpriteData(), 62, 61));
        
        // 多層血條
        _backBar = UIImageManager.CreateUISprite(new GORelativeInfo(_baseSilder.gameObject, new Vector3(21, 4, 0), "BackBar"),
            new UISpriteInfo(NGUISpriteData.DATA_SLIDER_FG, width, height, _frontBar.depth - 1, pivot: UIWidget.Pivot.Left));
        _backDepth = _backBar.depth;
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
    /// 設定現在HP和HP最大值
    /// </summary>
    /// <param name="curHP">現在值，若小於0，設成0，若大於最大值（修改後），設成最大值</param>
    /// <param name="maxHP">最大值，若為null或小於等於0，則不做修改</param>
    public void SetHP(int curHP, int? maxHP = null)
    {
        if (maxHP.HasValue)
        {
            if (maxHP.Value <= 0) { CommonFunction.DebugErrorFormat("MaxDataNum值({0})給予錯誤，不做修改", maxHP.Value); }
            else { MaxHP = maxHP.Value; }
        }        
        // 限制cur值在0~_maxDataNum
        int realHP = Mathf.Clamp(curHP, 0, _maxHP);
        _curHP = realHP;
        SetCurrentUIState();
    }

    /// <summary>
    /// 依據現在的_maxDataNum、_curDataNum、_layerNum，設定應該顯示的樣子
    /// </summary>
    void SetCurrentUIState()
    {
        // _layerNum = 1的情形
        //if (_layerNum == 1)
        //{
            _baseSilder.value = (float)_curHP / (float)_maxHP;
            NGUITools.SetActive(_backBar.gameObject, false);
        //}
        //else 
        //{
        //    CommonFunction.DebugMsgFormat("_curHP = {0} _maxHP = {1} _layerNum = {2} _oneLayerNum = {3}", _curHP, _maxHP, _layerNum, _oneLayerMaxHP);
        //    // 假設layerNum = 3
        //    if (_curHP < _oneLayerMaxHP) // 最後一條
        //    {
        //        CommonFunction.DebugMsgFormat("HP_NUM = {0}", 1);
        //        _sliderForegroundIsFrontBar = true;
        //        _baseSilder.foreground = _frontBar.transform;
        //        _frontBar.color = barColors[0];
        //        _frontBar.depth = _backDepth + 1;
        //        NGUITools.SetActive(_backBar.gameObject, false);
        //        _baseSilder.value = (float)_curHP / (float)_oneLayerMaxHP;
        //    }
        //    else if (_curHP < _oneLayerMaxHP * 2) // 倒數第二條
        //    {
        //        CommonFunction.DebugMsgFormat("HP_NUM = {0}", 2);
        //        _sliderForegroundIsFrontBar = false;
        //        _baseSilder.foreground = _backBar.transform;
        //       _frontBar.color = barColors[0];
        //        _backBar.color = barColors[1];
        //        _frontBar.SetEffectSizeParameter((int)_fullSize.x, (int)_fullSize.y);
        //        _frontBar.depth = _backDepth;
        //        _backBar.depth = _backDepth + 1;
        //        _baseSilder.value = (float)(_curHP - _oneLayerMaxHP) / (float)_oneLayerMaxHP;
        //    }
        //    else // 假設layer = 3 故為第一條
        //    {
        //        CommonFunction.DebugMsgFormat("HP_NUM = {0}", 3);
        //        _sliderForegroundIsFrontBar = true;
        //        _baseSilder.foreground = _frontBar.transform;
        //        _frontBar.color = barColors[2];
        //        _backBar.color = barColors[1];
        //        _backBar.SetEffectSizeParameter((int)_fullSize.x, (int)_fullSize.y);
        //        _frontBar.depth = _backDepth + 1;
        //        _backBar.depth = _backDepth;
        //        NGUITools.SetActive(_backBar.gameObject, true);
        //        _baseSilder.value = (float)(_curHP - _oneLayerMaxHP * 2) / (float)_oneLayerMaxHP;
        //    }
        //}
    }
    // 限制血條高度
    const int POS_Y_LIMIT = GUIStation.MANUAL_SCREEN_HEIGHT / 2 - 5;
    /// <summary>
    /// 依據傳入的新跟隨螢幕位置，更新血條位置
    /// </summary>
    /// <param name="newFollowScreenPos">新的更新螢幕位置</param>
    public void UpdatePos(Vector3 newFollowScreenPos)
    {
        if (_showMode == ShowMode.WITH_AVATAR)
        {
            _followScreenPos = newFollowScreenPos;
            // 將垂直位置限制在畫面內(似乎壓制的位置很奇怪 先註解)
            //_followScreenPos.y = Mathf.Clamp(_followScreenPos.y, -POS_Y_LIMIT, POS_Y_LIMIT);
            Vector3 uiScreenPos = GameControl.Instance.GUIStation.GUICamera.ScreenToWorldPoint(_followScreenPos);
            uiScreenPos.z = 0;
            _subUIRoot.transform.position = uiScreenPos;
        }
    }

    /// <summary>
    /// 設定遊戲屬性
    /// </summary>
    /// <param name="ga">要設定的遊戲屬性</param>
    public void SetGameAttribute(GameAttribute ga)
    {
        _gameAttribute = ga;
        if (_gameAttributeImage == null)
        {
            _gameAttributeImage = UIImageManager.CreateUISprite(new GORelativeInfo(_baseSilder.gameObject, "AttributeImage"),
            new UISpriteInfo(_gameAttribute.GetCorrespondingNGUISpriteData(), 62, 61));
        }
        else
        {
            _gameAttributeImage.spriteName = ga.GetCorrespondingSpriteName();
        }
    }
}
