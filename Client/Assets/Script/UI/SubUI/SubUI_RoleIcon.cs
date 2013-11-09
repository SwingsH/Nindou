using UnityEngine;
using System.Collections;


/// <summary>
/// 角色圖示
/// </summary>
public class SubUI_RoleIcon : GUISubFormBase
{
    readonly Color DeadRoleBtnBGColor = CommonFunction.Color256Bit(28, 28, 28, 255);
    readonly Color DeadRoleGraphColor = new Color(0.3f, 0.3f, 0.3f);

    UIButton _roleIconBtn; // 角色按鈕
    UIButtonScale _roleIconBtnScale; // 角色按鈕效果
    UITexture _roleHead;
    UITexture _roleEye;
    UITexture _roleHair;

    SubUI_HPBar _roleHPBar;
    UILabel _roleName;
    #region 物件建立
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="goInfo">和GameObject相關的資訊</param>
    /// <param name="roleTextureAtlas">角色圖在的Atlas，之後應該改成讓外界傳名字就好</param>
    /// <param name="roleIconEventDelegte">點擊角色Icon的反應函式</param>
    /// <param name="roleName">角色名字</param>
    /// <param name="showHPBar">是否顯示HP條</param>
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
        _roleIconBtn.SetColor(Color.white, DeadRoleBtnBGColor, Color.white, Color.white);
        // 按鈕效果
        _roleIconBtnScale = _roleIconBtn.gameObject.AddComponent<UIButtonScale>();
        _roleIconBtnScale.tweenTarget = _roleIconBtn.transform;
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
        _roleHPBar = new SubUI_HPBar(_roleIconBtn.gameObject, "Role_HP_Bar", new Vector3(-104, -93, 0), 5,
            (int)(219 * GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI),
            (int)(41*GUIStation.RESOLUTION_SCALE_BETWEEN_ART_AND_UI));
        _roleHPBar.FullSize = new Vector2(205, 28);
        _roleHPBar.SetVisible(showHPBar);
        // 角色名字
        _roleName = GUIStation.CreateUILabel(_roleIconBtn.gameObject, "Role Name", UIWidget.Pivot.Center, new Vector3(3, 129, 0), 5,
            UIFontManager.GetUIDynamicFont(UIFontName.MSJH, UIFontSize.UI_BATTLE_ROLE_NAME, FontStyle.Bold),
            Color.red, roleName);
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
        }
        _roleHead = null;
        _roleEye = null;
        _roleHair = null;
        _roleHPBar = null;
        _roleName = null;
        _roleIconBtn = null;
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
        _roleHPBar.SetHP(curHP, maxHP);
        // 有顯示血條時，按鈕是否有效才會被影響
        if (_roleHPBar.Visible)
        {
            SetBtnEnable(_roleHPBar.CurHP > 0);
            //_roleIconBtn.enabled = (_roleHPBar.CurHP > 0);
            //_roleIconBtnScale.enabled = (_roleHPBar.CurHP > 0);
            //_roleHead.color = (_roleHPBar.CurHP > 0) ? Color.white : DeadRoleGraphColor;
            //_roleEye.color = (_roleHPBar.CurHP > 0) ? Color.white : DeadRoleGraphColor;
            //_roleHair.color = (_roleHPBar.CurHP > 0) ? Color.white : DeadRoleGraphColor;
        }
    }

    /// <summary>
    /// 設定按鈕失效與否&對應的畫面效果
    /// </summary>
    /// <param name="isEnable">是否enable</param>
    void SetBtnEnable(bool isEnable)
    {
        _roleIconBtn.enabled = isEnable;
        // fs : 如果設為true，則需要立刻更改顏色，否則會有前一場戰鬥死亡變暗，導致下一場該角色會產生由暗變亮的效果
        if (isEnable) { _roleIconBtn.UpdateColor(true, true); }
        _roleIconBtnScale.enabled = isEnable;
        
        _roleHead.color = isEnable ? Color.white : DeadRoleGraphColor;
        _roleEye.color = isEnable ? Color.white : DeadRoleGraphColor;
        _roleHair.color = isEnable ? Color.white : DeadRoleGraphColor;
    }
    /// <summary>
    /// 設定角色名字
    /// </summary>
    /// <param name="newRoleName">新角色名字</param>
    public void SetRoleName(string newRoleName)
    {
        _roleName.text = newRoleName;
    }
}
