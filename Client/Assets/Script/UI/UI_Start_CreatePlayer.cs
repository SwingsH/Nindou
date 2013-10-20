using UnityEngine;
using System.Collections;

/// <summary>
/// 開始畫面
/// </summary>
public class UI_Start_CreatePlayer : GUIFormBase 
{
    private UIButton _submitButton; // 確認送出
    private UISprite _background; // 背景圖
    private UIInput _accountNameInput;
    private BtnClick _clickEvent = null;

    protected override void CreateAllComponent()
    {

        UIAnchor anchor = NGUITools.AddChild<UIAnchor>(gameObject);
        anchor.uiCamera = _guistation.GUICamera;

        UIPanel panel = NGUITools.AddChild<UIPanel>(anchor.gameObject);

        _background = GUIComponents.MainBackground(panel.gameObject, 1);

        _accountNameInput = GUIComponents.NameInputField(panel.gameObject, GLOBAL_STRING.UI_INPUT_HINT_1, 2);
        _accountNameInput.transform.localPosition = new Vector3(0, -GUIStation.MANUAL_SCREEN_HEIGHT * 0.2f, 0);

        _submitButton = GUIComponents.DialogButton(panel.gameObject, GLOBAL_STRING.UI_BUTTON_3, 3);
        _submitButton.transform.localPosition = new Vector3(135, -GUIStation.MANUAL_SCREEN_HEIGHT * 0.2f, 0);
        _submitButton.onClick.Add(new EventDelegate(this, "OnButtonClick"));
    }

    public void OnButtonClick()
    {
        if (_clickEvent != null)
            _clickEvent.Invoke();

        Hide();
    }

    public void SetClickEvent(BtnClick clickEvent)
    {
        _clickEvent = clickEvent;
    }

    protected override void OnDestroy()
    {
        NGUITools.Destroy(_background.gameObject);
        _background = null;
        base.OnDestroy();
    }

    public string CurrentInputAccountName
    {
        get
        {
            if(_accountNameInput == null)
            {
                CommonFunction.DebugMsg( " GetInputAccountName is null ");
                return string.Empty;
            }
            return _accountNameInput.value;
        }
    }
}
