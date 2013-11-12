using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色圖示
/// </summary>
public class SubUI_RoleIcon : GUISubFormBase
{
    readonly Color NormalRoleBtnBGColor = Color.white;
    readonly Color DeadRoleBtnBGColor = CommonFunction.Color256Bit(28, 28, 28, 255);
    readonly Color DeadRoleGraphColor = new Color(0.3f, 0.3f, 0.3f);

    readonly Vector3 EffectScale = new Vector3(1.2f, 1.2f, 1.0f); // 按下/經過放大特效倍率

    UIButton _roleIconBtn; // 角色按鈕
    UIButtonScale _roleIconBtnScale; // 角色按鈕效果
    UITexture _roleHead;
    UITexture _roleEye;
    UITexture _roleHair;

    SubUI_HPBar _roleHPBar;

    UILabel _roleName;

    UISlider _roleCD;
    float _curCD = 0.0f;
    public float CurCD
    {
        get {return _curCD;}
    }
    float _maxCD = 1.0f;

    List<TweenColor> _roleCDTweenColors = new List<TweenColor>();
    /// <summary>
    /// CD時間是否到了
    /// </summary>
    public bool IsCDTimeUp
    {
        get
        {
            // 沒有顯示CD時間則一律回傳false
            if (!NGUITools.GetActive(_roleCD.gameObject)) { return false; }
            return Mathf.Approximately(_roleCD.value, 1.0f);
        }
    }

    /// <summary>
    /// 是否已經死亡，若血條不可見當作不是。
    /// </summary>
    bool IsDead
    {
        get
        {
            return _roleHPBar.Visible ? _roleHPBar.CurHP <= 0 : false;
        }
    }

    #region 物件建立
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="goInfo">和GameObject相關的資訊</param>
    /// <param name="roleTextureAtlas">角色圖在的Atlas，之後應該改成讓外界傳名字就好</param>
    /// <param name="roleIconEventDelegte">點擊角色Icon的反應函式</param>
    /// <param name="roleName">角色名字</param>
    /// <param name="showHPBar">是否顯示HP條</param>
    /// <param name="ga">屬性</param>
    public SubUI_RoleIcon(GORelativeInfo goInfo,
        SmoothMoves.TextureAtlas roleTextureAtlas,
        EventDelegate roleIconEventDelegte,
        string roleName,
        bool showHPBar = true
        ) : base(goInfo.ParentObject, goInfo.ObjectName, goInfo.LocalPosition)
    {
        // 若名字為空，修改為預設名稱
        if (string.IsNullOrEmpty(goInfo.ObjectName)) { _subUIRoot.name = CommonFunction.GetName<SubUI_RoleIcon>(); }

        // 角色按鈕兼底圖
        _roleIconBtn = GUIStation.CreateUIButton(_subUIRoot.gameObject, "Role_Btn", Vector3.zero, 1,
            NGUISpriteData.ROLE_ICON,
            (int)(220 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            (int)(219 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            null, Color.white, string.Empty);
        _roleIconBtn.tweenTarget.name = "Role BG";
        _roleIconBtn.SetColor(NormalRoleBtnBGColor, NormalRoleBtnBGColor, NormalRoleBtnBGColor, NormalRoleBtnBGColor);
        TweenColor btnTC = _roleIconBtn.tweenTarget.AddComponent<TweenColor>();
        btnTC.from = NormalRoleBtnBGColor;
        btnTC.to = CommonFunction.Color256Bit(117, 134, 84, 255);
        btnTC.duration = 0.5f;
        btnTC.style = UITweener.Style.PingPong;
        btnTC.enabled = false;
        // 按鈕效果
        _roleIconBtnScale = _roleIconBtn.gameObject.AddComponent<UIButtonScale>();
        _roleIconBtnScale.tweenTarget = _roleIconBtn.transform;
        _roleIconBtnScale.duration = 0.3f;
        _roleIconBtnScale.hover = new Vector3(1.2f, 1.2f, 1.0f);
        _roleIconBtnScale.pressed = new Vector3(1.2f, 1.2f, 1.0f);
        _roleIconBtn.onClick.Add(roleIconEventDelegte);
        #region 角色圖示(暫定作法)
        // 角色圖示(之後可能會有因為角色而需要變動大小、位置之類的)
        // Root
        GameObject roleGraphGO = NGUITools.AddChild(_roleIconBtn.gameObject);
        roleGraphGO.name = "Role Graph";
        roleGraphGO.transform.localPosition = new Vector3(12, -21, 0);
        // 頭
        _roleHead = NGUITools.AddChild<UITexture>(roleGraphGO);
        _roleHead.name = "Head";
        _roleHead.depth = 2;
        // 眼睛
        _roleEye = NGUITools.AddChild<UITexture>(roleGraphGO);
        _roleEye.name = "Eye";
        _roleEye.depth = 3;
        // 頭髮
        _roleHair = NGUITools.AddChild<UITexture>(roleGraphGO);
        _roleHair.name = "Hair";
        _roleHair.depth = 4;
        // 設定對應圖示
        SetRoleGraph(roleTextureAtlas);
        #endregion
        // 血條
        _roleHPBar = new SubUI_HPBar(_roleIconBtn.gameObject, "Role_HP_Bar", new Vector3(-104, -81, 0), 5,
            (int)(219 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            (int)(41*GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI));
        _roleHPBar.FullSize = new Vector2(205, 28);
        _roleHPBar.SetVisible(showHPBar);
        // 角色名字
        _roleName = GUIStation.CreateUILabel(_roleIconBtn.gameObject, "Role Name", UIWidget.Pivot.Center, new Vector3(3, 129, 0), 6,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_ROLE_NAME, FontStyle.Bold),
            Color.red, roleName);
        // CD
        _roleCD = GUIStation.CreateUISlider(_roleIconBtn.gameObject, "Role CD", new Vector3(-104, -132, 0), 3,
            NGUISpriteData.DATA_SLIDER_FG, NGUISpriteData.ROLE_HP_BG, NGUISpriteData.NONE,
            (int)(219 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            (int)(41 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            false);
        _roleCD.foreground.localPosition = new Vector3(21, 4, 0);
        _roleCD.fullSize = new Vector2(205, 28);
        UISprite roleCDFG = _roleCD.foreground.GetComponent<UISprite>();
        roleCDFG.color = CommonFunction.Color256Bit(0.0f, 163.0f, 163.0f, 255.0f);
        // TweenColor 
        TweenColor roleCDTweenColor = roleCDFG.gameObject.AddComponent<TweenColor>();
        roleCDTweenColor.from = roleCDFG.color;
        roleCDTweenColor.to = CommonFunction.Color256Bit(148.0f, 255.0f, 255.0f, 255.0f);
        roleCDTweenColor.style = UITweener.Style.PingPong;
        roleCDTweenColor.duration = 0.5f;
        roleCDTweenColor.enabled = false;

        _roleCDTweenColors.Add(roleCDTweenColor);
        _roleCDTweenColors.Add(btnTC);
    }
    #endregion
    #region Dispose -- 資源釋放
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            NGUITools.Destroy(_roleHead);
            NGUITools.Destroy(_roleEye);
            NGUITools.Destroy(_roleHair);
            _roleHPBar.Dispose();
            NGUITools.Destroy(_roleName);
            NGUITools.Destroy(_roleIconBtn);
            foreach (TweenColor tc in _roleCDTweenColors)
            {
                NGUITools.Destroy(tc);
            }
            NGUITools.Destroy(_roleCD);
        }
        _roleHead = null;
        _roleEye = null;
        _roleHair = null;
        _roleHPBar = null;
        _roleName = null;
        _roleIconBtn = null;
        _roleCDTweenColors = null;
        _roleCD = null;
        base.Dispose(disposing);
    }
    #endregion

    /// <summary>
    /// 設定角色圖示
    /// </summary>
    /// <param name="newRoleAtlas">新角色的Atlas（之後應該改成傳入角色名稱即可）</param>
    public void SetRoleGraph(SmoothMoves.TextureAtlas newRoleAtlas)
    {
        SetUITextureFromSmoothMovesTextureAtlas(ref _roleHead, newRoleAtlas, GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.HEAD]);
        SetUITextureFromSmoothMovesTextureAtlas(ref _roleEye, newRoleAtlas, GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.EYES]);
        SetUITextureFromSmoothMovesTextureAtlas(ref _roleHair, newRoleAtlas, GLOBALCONST.BONE_NAME[(int)GLOBALCONST.eModelPartName.HAIR]);
    }

    void SetUITextureFromSmoothMovesTextureAtlas(ref UITexture tex, SmoothMoves.TextureAtlas sTextureAtlas, string sTextureName)
    {
        tex.material = sTextureAtlas.material;
        int sSpriteIndex = sTextureAtlas.GetTextureIndex(sTextureAtlas.GetTextureGUIDFromName(sTextureName));
        tex.uvRect = sTextureAtlas.uvs[sSpriteIndex];

        Vector2 size = sTextureAtlas.GetTextureSize(sSpriteIndex);
        tex.width = (int)size.x;
        tex.height = (int)size.y;        
    }

    /// <summary>
    /// 設定HPBar是否可見
    /// </summary>
    /// <param name="isVisible">是否可見</param>
    public void SetHPBarVisible(bool isVisible)
    {
        _roleHPBar.SetVisible(isVisible);
    }

    /// <summary>
    /// 設定現在HP和HP最大值(若現在HP歸0，按鈕失效)
    /// </summary>
    /// <param name="curHP">現在值，若小於0，設成0，若大於最大值（修改後），設成最大值</param>
    /// <param name="maxHP">最大值，若為null或小於等於0，則不做修改</param>
    public void SetHP(int curHP, int? maxHP = null)
    {
        bool setBeforeIsDead = IsDead;
        _roleHPBar.SetHP(curHP, maxHP);
        
        // 只有在由生=>死才需要特別設定一次按鈕的enable&Tween
        if (!setBeforeIsDead && IsDead)
        {
            SetBtnEnable(false);
            StopCDTweenColor(); // 角色死亡則關閉顏色Tween
        }
    }

    /// <summary>
    /// 設定按鈕失效與否&對應的畫面效果
    /// </summary>
    /// <param name="isEnable">是否enable</param>
    public void SetBtnEnable(bool isEnable)
    {
        Color disabledGraphColor;
        bool finalSetEnable;
        if (IsDead)
        {
            _roleIconBtn.disabledColor = DeadRoleBtnBGColor;
            _roleIconBtn.UpdateColor(false, false);
            finalSetEnable = false; // 死亡時當然enable是關閉狀態
            disabledGraphColor = DeadRoleGraphColor;
        }
        else
        {
            _roleIconBtn.disabledColor = NormalRoleBtnBGColor;
            _roleIconBtn.UpdateColor(isEnable, true);
            finalSetEnable = isEnable;
            disabledGraphColor = Color.white;
        }

        _roleIconBtn.enabled = finalSetEnable;
        _roleIconBtnScale.enabled = finalSetEnable;
        _roleHead.color = disabledGraphColor;
        _roleEye.color = disabledGraphColor;
        _roleHair.color = disabledGraphColor;
    }

    /// <summary>
    /// 設定角色名字
    /// </summary>
    /// <param name="newRoleName">新角色名字</param>
    public void SetRoleName(string newRoleName)
    {
        _roleName.text = newRoleName;
    }

    /// <summary>
    /// 更新CD值和對應顯示
    /// </summary>
    /// <param name="curCD">現在已等待時間</param>
    /// <param name="maxCD">需等待時間（=null則不做更動）</param>
    public void UpdateCD(float curCD, float? maxCD = null)
    {        
        if (IsDead) { return; } // 角色已經死亡就不更新CD值
        if (maxCD.HasValue) { _maxCD = maxCD.Value; }
        // 如果該角色maxCD <= 0，表示不應該顯示slider有值，將其數值設為0
        if (_maxCD <= 0) 
        { 
            _roleCD.value = 0;
            return;
        }

        _curCD = Mathf.Clamp(curCD, 0.0f, _maxCD);
        _roleCD.value = _curCD / _maxCD;
        if (IsCDTimeUp)
        {
            SetBtnEnable(true); // CD到了，開啟接收事件
            foreach (TweenColor tc in _roleCDTweenColors)
            {
                if (!tc.enabled)
                {
                    tc.enabled = true;
                    tc.Play();
                }
            }
        }
    }

    public void StopCDTweenColor()
    {
        if (NGUITools.GetActive(_roleCD.gameObject))
        {
            foreach (TweenColor tc in _roleCDTweenColors)
            {
                tc.Reset();
                tc.color = tc.from;
                tc.enabled = false;
            }
        }
    }

    /// <summary>
    /// 設定遊戲屬性
    /// </summary>
    /// <param name="ga">要設成的遊戲屬性</param>
    public void SetGameAttribute(GameAttribute ga)
    {
        _roleHPBar.SetGameAttribute(ga);
    }
}
