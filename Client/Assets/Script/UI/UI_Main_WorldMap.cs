using UnityEngine;
using System.Collections;

/// <summary>
/// 主介面 - 世界地圖
/// </summary>
public class UI_Main_WorldMap : GUIFormBase
{
    public BtnClick stageClick;
    #region 每個主介面都會有的部分
    private UIButton _characterBtn; // 「人物」按鈕
    private UIButton _bagBtn; // 「背包」按鈕
    private UIButton _shopBtn; // 「商店」按鈕
    private UIButton _friendBtn; // 「好友」按鈕
    #endregion
    #region 世界地圖部分
    private UISlider _stamina; // 「體力」的slider
    private UILabel _staminaText; // 「體力」的slider內的說明文字
    private UIButton _headPictureBtn; // 「人物頭像」按鈕
    private UILabel _pointText; // 「點數」說明文字
    private UIButton _menuBtn; // 「選單」按鈕
    private UILabel _warningText; // 「強敵發現！！」文字
    private UIButton _stageBtn; // 「關卡」按鈕
    #endregion

    private int _staPoint = 5; // 體力(應該要從帳號取得
    private int _staMaxPoint = 10; // 體力最大值(應該要從帳號取得
    private int _gamePoint = 0; // 遊戲點數(應該要從帳號取得

    #region 繼承自GUIFormBase的method
    protected override void  CreateAllComponent()
    {

        throw new System.NotImplementedException();
    }
    #endregion
    #region 固定函式
    // Use this for initialization
	void Start ()
    {
        #region 每個主介面都有的部分
        // 背景圖
        UISprite backgroundPic = CommonFunction.CreateUISprite(gameObject, "Background", UISprite.Type.Simple, 0,
            ResourceStation.GetUIAtlas("TestAtlas"),
           "pachuri", UIWidget.Pivot.Center, 1920, 1080);
        // 「人物」按鈕
        _characterBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Character", new Vector3(-701, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.CHARACTER_BTN_TEXT);
        _characterBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _characterBtn.onClick.Add(new EventDelegate(this, "CharacterBtnClick"));
        // 「背包」按鈕
        _bagBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Bag", new Vector3(-274.2f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.BAG_BTN_TEXT);
        _bagBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _bagBtn.onClick.Add(new EventDelegate(this, "BagBtnClick"));
        // 「商店」按鈕
        _shopBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Shop", new Vector3(191.78f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.SHOP_BTN_TEXT);
        _shopBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _shopBtn.onClick.Add(new EventDelegate(this, "ShopBtnClick"));
        // 「好友」按鈕
        _friendBtn = CommonFunction.CreateUIButton(backgroundPic.gameObject, "Friend", new Vector3(653.42f, -449, 0), 1,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", 300, 80,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, GLOBAL_STRING.FRIEND_BTN_TEXT);
        _friendBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _friendBtn.onClick.Add(new EventDelegate(this, "FriendBtnClick"));
        #endregion
        #region 世界地圖部分
        // 世界地圖的背景圖
        UISprite worldMap = CommonFunction.CreateUISprite(backgroundPic.gameObject, "WorldMap", UISprite.Type.Simple, 1,
            ResourceStation.GetUIAtlas("TestAtlas2"),
            "chiruno", UIWidget.Pivot.Center, 1760, 838);
        worldMap.transform.localPosition = new Vector3(10, 35, 0);
        // 體力條
        _stamina = CommonFunction.CreateUIProgressBar(worldMap.gameObject, "Stamina", new Vector3(-698.7f, 329.38f, 0), 8,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", "button_back", 418, 84);
        // 此處暫時作法，一般來說前景和背景圖會是不同的
        UISprite[] tempSprites = _stamina.gameObject.GetComponentsInChildren<UISprite>();
        foreach (UISprite oneSprite in tempSprites)
        {
            if (oneSprite.name.Equals("Foreground"))
            {
                oneSprite.color = new Color(28.0f / 255.0f, 255.0f / 255.0f, 124.0f / 255.0f);
                break;
            }
        }
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        // 體力條文字
        _staminaText = CommonFunction.CreateUILabel(_stamina.gameObject, "StaminaText", UIWidget.Pivot.Left, new Vector3(16.6f, -5.93f, 0), 10,
            ResourceStation.GetUIFont("MSJH_30"),
            Color.red, string.Format("目前體力：{0}/{1}", _staPoint, _staMaxPoint));
        // 「人物頭像」按鈕
        _headPictureBtn = CommonFunction.CreateUIButton(worldMap.gameObject, "HeadPicture", new Vector3(0, 318.37f, 0), 5,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "pachuri", 150, 150,
            null, Color.white, string.Empty);
        _headPictureBtn.SetColor(Color.white, Color.white, Color.white, Color.white);
        _headPictureBtn.onClick.Add(new EventDelegate(this, "HeadPictureBtnClick"));
        // 「點數」
        UISprite pointBasePic = CommonFunction.CreateUISprite(worldMap.gameObject, "Point", UISprite.Type.Sliced, 6,
            ResourceStation.GetUIAtlas("TestAtlas"),
            "button_back", UIWidget.Pivot.Center, 418, 84);
        pointBasePic.transform.localPosition = new Vector3(347, 328, 0);
        //_pointText
        #endregion
        foreach (UIButton btn in GetComponentsInChildren<UIButton>())
        {
          //  if (btn.name.Equals("Character")) { _characterBtn = btn; }
            //if (btn.name.Equals("Bag")) { _bagBtn = btn; }
            //if (btn.name.Equals("Shop")) { _shopBtn = btn; }
            //if (btn.name.Equals("Friend")) { _friendBtn = btn; }
            //if (btn.name.Equals("HeadPicture")) { _headPictureBtn = btn; }
            if (btn.name.Equals("Menu")) { _menuBtn = btn; }
            if (btn.name.Equals("Stage")) { _stageBtn = btn; }
        }

        foreach (UILabel label in GetComponentsInChildren<UILabel>())
        {
            //if (label.name.Equals("StaminaText")) { _staminaText = label; }
            if (label.name.Equals("PointText")) { _pointText = label; }
            if (label.name.Equals("Warning")) { _warningText = label; }
        }
        foreach (UISlider slider in GetComponentsInChildren<UISlider>())
        {
            //if (slider.name.Equals("Stamina")) { _stamina = slider; }
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {

    }

    protected override void OnDestroy()
    {
        NGUITools.Destroy(_characterBtn.gameObject);
        _characterBtn = null;
        NGUITools.Destroy(_bagBtn.gameObject);
        _bagBtn = null;
        NGUITools.Destroy(_shopBtn.gameObject);
        _shopBtn = null;
        NGUITools.Destroy(_friendBtn.gameObject);
        _friendBtn = null;
        NGUITools.Destroy(_stamina.gameObject);
        _stamina = null;
        NGUITools.Destroy(_staminaText.gameObject);
        _staminaText = null;
        NGUITools.Destroy(_headPictureBtn.gameObject);
        _headPictureBtn = null;
        NGUITools.Destroy(_pointText.gameObject);
        _pointText = null;
        NGUITools.Destroy(_menuBtn.gameObject);
        _menuBtn = null;
        NGUITools.Destroy(_warningText.gameObject);
        _warningText = null;
        NGUITools.Destroy(_stageBtn.gameObject);
        _stageBtn = null;
        base.OnDestroy();
    }
    #endregion

    /// <summary>
    /// 按下「人物」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    //public 
        void CharacterBtnClick()
    {
        CommonFunction.DebugMsg("按下 「人物」按鈕");
        // test : 隱藏警告提示
        NGUITools.SetActive(_warningText.gameObject, false);
        CommonFunction.DebugMsg("隱藏警告提示");
    }
    /// <summary>
    /// 按下「背包」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    //public 
        void BagBtnClick()
    {
        CommonFunction.DebugMsg("按下「背包」按鈕");
        // test : 顯示警告提示
        NGUITools.SetActive(_warningText.gameObject, true);
        CommonFunction.DebugMsg("顯示警告提示");
    }
    /// <summary>
    /// 按下「商店」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    //public 
        void ShopBtnClick()
    {
        CommonFunction.DebugMsg("按下「商店」按鈕");
        // test : 增加體力（超過最大值改成0）
        _staPoint = (_staPoint >= _staMaxPoint) ? 0 : _staPoint + 1;
        _staminaText.text = string.Format("目前體力：{0}/{1}", _staPoint, _staMaxPoint);
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        CommonFunction.DebugMsg("增加體力（超過最大值改成0）");
    }
    /// <summary>
    /// 按下「好友」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    //public 
        void FriendBtnClick()
    {
        CommonFunction.DebugMsg("按下「好友」按鈕");
        // test : 減少體力（低於0改成最大值）
        _staPoint = (_staPoint == 0) ? _staMaxPoint : _staPoint - 1;
        _staminaText.text = string.Format("目前體力：{0}/{1}", _staPoint,_staMaxPoint);
        _stamina.value = (float)_staPoint / (float)_staMaxPoint;
        CommonFunction.DebugMsg("減少體力（低於0改成最大值）");
    }

    /// <summary>
    /// 按下「頭像」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    //public 
        void HeadPictureBtnClick()
    {
        CommonFunction.DebugMsg("按下「頭像」按鈕");
        // test : 減少遊戲點數
        --_gamePoint;
        _pointText.text = string.Format("目前點數：{0}", _gamePoint);
        CommonFunction.DebugMsg("減少遊戲點數");
    }

    /// <summary>
    /// 按下「選單」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    public void MenuBtnClick()
    {
        CommonFunction.DebugMsg("按下「選單」按鈕");
        // test : 增加遊戲點數
        ++_gamePoint;
        _pointText.text = string.Format("目前點數：{0}", _gamePoint);
        CommonFunction.DebugMsg("增加遊戲點數");

    }

    /// <summary>
    /// 按下「關卡」按鈕的反應函式
    /// TODO : public 只是暫時為了確認用，之後要改成private
    /// </summary>
    public void StageBtnClick()
    {
        CommonFunction.DebugMsg("按下「關卡」按鈕");
    }
}
