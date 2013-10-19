using UnityEngine;
using System.Collections;

/// <summary>
/// 開始畫面
/// </summary>
public class UI_Dialog_Simple : GUIFormBase 
{
    UIButton _dialog = null;
    UILabel _label = null;
    UIButton _okButton = null;
    BtnClick _clickEvent = null;

    protected override void CreateAllComponent()
    {
        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        _dialog = GUIComponents.DialogFrame(panel.gameObject);
        _dialog.transform.localPosition = new Vector3(0, 50, 0);

        _label = GUIComponents.MessageLabel(_dialog.gameObject, string.Empty, Color.black);

        _okButton = GUIComponents.DialogButton(_dialog.gameObject, GLOBAL_STRING.UI_BUTTON_2, 2);
        _okButton.transform.localPosition = new Vector3(0,-60,0);
        _okButton.onClick.Add( new EventDelegate(this, "OnButtonClick") );
    }

    /// <summary>
    /// 顯示一個提示訊息
    /// </summary>
    public void ShowMessage(string message, BtnClick clickEvent)
    {
        Show();
        _label.text = message;
        _clickEvent = clickEvent;
    }

    /// <summary>
    /// 按鈕按下的後的事件
    /// </summary>
    public void OnButtonClick()
    {
        if (_clickEvent != null)
            _clickEvent.Invoke();

        Hide();
    }

    protected override void OnDestroy()
    {
        //NGUITools.Destroy(_loginBtn.gameObject);
        //_loginBtn = null;
        //NGUITools.Destroy(_loginHint.gameObject);
        //_loginHint = null;
        //NGUITools.Destroy(_progressShow.gameObject);
        //_progressShow = null;
        //NGUITools.Destroy(_progress.gameObject);
        //_progress = null;
        //NGUITools.Destroy(_inheritBtn.gameObject);
        //_inheritBtn = null;

        //InheritBtnClick = null;
        //LoginBtnClick = null;
        base.OnDestroy();
    }
}
