using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//HUD (head-up display) 
//目前只有跳血數字顯示
public class HUDManager : MonoBehaviour {

	static int _displayLayer;
	public static int DisplayLayer
	{
		set
		{
			_displayLayer = value;
		}
		get
		{
			return _displayLayer;
		}
	}
	static int _fontSize;
	public static int FontSize
	{
		set
		{
			_fontSize = value;
		}
		get
		{
			return _fontSize;
		}
	}

	const float ShiftUnit = 20;
	const int POOL_SIZE = 10;
	AnimationCurve ColorFadeOutCurve = new AnimationCurve(new Keyframe(0.8f, 0f, 0f, 0f), new Keyframe(1f, 1f, 2f, 2f));
	/// <summary>
	/// 產生一個HUDManager
	/// </summary>
	/// <param name="cam">顯示HUD的camera</param>
	public static HUDManager Create(Camera displayCamera)
	{
		if (displayCamera == null)
			return null;

		HUDManager hud = displayCamera.gameObject.AddComponent<HUDManager>();

		return hud;
	}

	Font font;
	Dictionary<long, HUDTextInfo> GroupText = new Dictionary<long, HUDTextInfo>();
	List<HUDTextInfo> DisplayText = new List<HUDTextInfo>();
	//暫存不用的text
	//用QUEUE比較好取出一個移出清單
	Queue<HUDTextInfo> RecyclePool = new Queue<HUDTextInfo>();
	// Use this for initialization
	void Start () {

        UIFont uifont = UIFontManager.GetUIDynamicFont(UIFontName.BigAppleNF, UIFontSize.HUD, FontStyle.Bold);
		if(uifont!=null) 
			font = uifont.dynamicFont;
		else
		{
			Object[] obj = Resources.FindObjectsOfTypeAll(typeof(Font));
			if(obj.Length > 0)
				font = obj[0]as Font;
			else
				CommonFunction.DebugError("找不到可用的font");
		}
	}
	
	
	void Update () {

		//播放動畫
		int i = 0;
		while (i < DisplayText.Count)
		{
			if (!DisplayText[i].RunAnimation())
			{
				Recycle(DisplayText[i]);
				DisplayText.RemoveAt(i);
				continue;
			}
			i++;
		}

		List<long> removeKey = null;
		foreach (KeyValuePair<long, HUDTextInfo> kvp in GroupText)
		{
			if (!kvp.Value.RunAnimation())
			{
				if (removeKey == null)
					removeKey = new List<long>();
				Recycle(kvp.Value);
				removeKey.Add(kvp.Key);
			}
		}
		//移除播放完的
		if (removeKey != null)
			foreach (long key in removeKey)
				GroupText.Remove(key);
	}

	void Recycle(HUDTextInfo text)
	{
		if (text != null)
		{
			text.Hide();
			if (RecyclePool.Count < POOL_SIZE)
				RecyclePool.Enqueue(text);
			else
				Destroy(text.textComponent.gameObject);
		}
	}
	//建新的text元件，用複製的比較慢，所以用新建的
	HUDTextInfo CreateTextComponent()
	{
		HUDTextInfo result = new HUDTextInfo();
		TextMesh tm = new GameObject("HUDText").AddComponent<TextMesh>();
		tm.font = font;
		tm.fontSize = 30;
		tm.characterSize = 20;
		tm.anchor = TextAnchor.MiddleCenter;
		tm.renderer.sharedMaterial = font.material;
		result.textComponent = tm;
		tm.transform.parent = this.transform;
		tm.transform.localRotation = Quaternion.identity;
		return result;
	}
	//取得字型，從回收區，沒有回收的就產生一個新的
	HUDTextInfo GetText()
	{
		HUDTextInfo result = null;
		while (RecyclePool.Count > 0)
		{
			result = RecyclePool.Dequeue();
			if (result != null)
				break;
		}
		if (result == null)
			result = CreateTextComponent();
		result.IntValue = 0;
		return result;
	}

	void SetDamageAnim(HUDTextInfo info, Vector3 position)
	{
		info.PosSeperateXY = true;
		info.PosStart = position;
		info.PosEnd = (Vector2)position + new Vector2(ShiftUnit * Random.Range(-2f,2f),ShiftUnit * -Random.Range(0.5f,3f));
		info.PosXCurve = new AnimationCurve(new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f));
		info.PosYCurve = new AnimationCurve(new Keyframe(0f, 0f, -2f, -2f), new Keyframe(1f, 1f, 6f, 6f));
		info.Duration = 1;

        info.ColorStart = info.ColorEnd = GLOBALCONST.BattleSettingValue.FONT_DAMAGE_COLOR;
		info.ColorEnd.a = 0;
		info.ColorCurve = new AnimationCurve(new Keyframe(0.5f, 0f, 0f, 0f), new Keyframe(1f, 1f, 3f, 3f));

		info.ScaleStart = info.ScaleEnd = Vector3.one;
		info.ScaleXCurve = new AnimationCurve();
		info.ScaleYCurve = new AnimationCurve();
	}
	//多段攻擊的總傷害文字
	void SetDamageGroupAnim(HUDTextInfo info, Vector3 position)
	{
		info.PosStart = position;
		info.PosEnd = position + new Vector3(0, 0.5f * ShiftUnit);
		info.PosXCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		info.PosYCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		info.Duration = 1.5f;

		info.ColorStart = info.ColorEnd = Color.yellow;
		info.ColorEnd.a = 0;
		info.ColorCurve = ColorFadeOutCurve;

		info.ScaleSeperateXY = false;
		info.ScaleStart = Vector2.one; 
		info.ScaleEnd = Vector2.one * 1.5f;
		info.ScaleXCurve = new AnimationCurve(new Keyframe(0.0f, 0f, 5f, 5f), new Keyframe(0.5f, 1f, 0f, 0f));
	}
	void SetHealAnim(HUDTextInfo info, Vector3 position)
	{
		info.PosSeperateXY = true;
		info.PosStart = position;
		info.PosEnd = (Vector2)position + new Vector2(ShiftUnit * Random.Range(-1f, 1f), ShiftUnit * Random.Range(0.5f, 1f));
		info.PosXCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		info.PosYCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		info.Duration = 1;

		info.ColorStart = info.ColorEnd = Color.green;
		info.ColorEnd.a = 0;
		info.ColorCurve = ColorFadeOutCurve;

		info.ScaleStart = info.ScaleEnd = Vector3.one;
		info.ScaleXCurve = new AnimationCurve();
		info.ScaleYCurve = new AnimationCurve();
	}


	/// <summary>
	/// 一般文字
	/// </summary>
	/// <param name="text">文字</param>
	/// <param name="worldPosition">位置（世界座標）</param>
	/// <param name="moveDirection">往哪個方向移動</param>
	/// <param name="color">顏色</param>
	/// <param name="Duration">顯示時間</param>
	public void ShowText(string text, Vector3 worldPosition, Vector3 moveDirection, Color color, float Duration)
	{
		Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
		HUDTextInfo textinfo = GetText();
		textinfo.ClearAnimation();

		textinfo.ColorStart = color;
		textinfo.ColorCurve = ColorFadeOutCurve;
		color.a = 0;
		textinfo.ColorEnd = color;

		textinfo.PosXCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		textinfo.PosStart = localPosition;
		textinfo.PosEnd = localPosition + moveDirection.normalized * ShiftUnit * 3;
		textinfo.Text = text;
		textinfo.Play();
		DisplayText.Add(textinfo);
	}

	public void ShowDamageText(int value, Vector3 worldPosition)
	{
		ShowDamageText(SkillDamageType.Damage, value, worldPosition);
	}
	public void ShowHealText(int value, Vector3 worldPosition)
	{
		ShowDamageText(SkillDamageType.Heal, value, worldPosition);
	}
	public void ShowDamageText(SkillDamageType damageType, int value, Vector3 worldPosition)
	{
		Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
		HUDTextInfo text = GetText();
		switch (damageType)
		{
			case SkillDamageType.Damage:
				SetDamageAnim(text, localPosition);
				break;
			case SkillDamageType.Heal:
				SetHealAnim(text, localPosition);
				break;
			default:
				SetDamageAnim(text, localPosition);
				break;
		}
		text.IntValue = value;
		text.Play();
		DisplayText.Add(text);
	}
	/// <summary>
	/// 同一個ID的呼叫在文字消失前都會加總起來
	/// 只要間隔不要超過目前設定為1.5秒就都會加起來
	/// </summary>
	public void ShowDamageGroupText(long callerID, int value, Vector3 position)
	{
		Vector3 localPosition = transform.InverseTransformPoint(position);
		//一樣跳單次的
		ShowDamageText(value, localPosition);

		HUDTextInfo text;
		if (!GroupText.TryGetValue(callerID, out text))
		{
			text = GetText();
			GroupText.Add(callerID, text);
		}
		//一起跳會被蓋住，所以群組的往前一點
		SetDamageGroupAnim(text, localPosition - Vector3.forward * 10);
		text.IntValue += value;
		text.Play();
	}
}
/// <summary>
/// 存放text的動畫資訊
/// </summary>
public class HUDTextInfo
{
	public TextMesh textComponent;
	int _intValue = 0;
	public int IntValue
	{
		get
		{
			return _intValue;
		}
		set
		{
			_intValue = value;
			Text = _intValue.ToString();
		}
	}
	public string Text
	{
		get
		{
			if (textComponent != null)
				return textComponent.text;
			else
				return string.Empty;
		}
		set
		{
			if (textComponent != null)
				textComponent.text = value;
		}
	}
	public int FontSize
	{
		get
		{
			if (textComponent != null)
				return textComponent.fontSize;
			else
				return 0;
		}
		set
		{
			if (textComponent != null)
				textComponent.fontSize = value;
		}
	}
	public float CharacterSize
	{
		get
		{
			if (textComponent != null)
				return textComponent.characterSize;
			else
				return 0f;
		}
		set
		{
			if (textComponent != null)
				textComponent.characterSize = value;
		}
	}

	//位移
	//xy的動畫曲線是否要分開，不分開都用PosXCurve
	public bool PosSeperateXY = true;
	public Vector3 PosStart = new Vector3(0, 0);
	public Vector3 PosEnd = new Vector3(0, 0);
	Vector3 PosOffset = new Vector2();
	/// <summary>
	/// X方向移動的AnimationCurve
	/// </summary>
	public AnimationCurve PosXCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
	/// <summary>
	/// Y方向移動AnimationCurve
	/// </summary>
	public AnimationCurve PosYCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	//變色
	public Color ColorStart = Color.black;
	public Color ColorEnd = Color.black;
	/// <summary>
	/// 顏色的AnimationCurve
	/// </summary>
	public AnimationCurve ColorCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	//大小
	//xy的動畫曲線是否要分開，不分開都用ScaleXCurve
	public bool ScaleSeperateXY = false;
	public Vector2 ScaleStart = new Vector2(1, 1);
	public Vector2 ScaleEnd = new Vector2(1, 1);
	 Vector2 ScaleOffset = new Vector2(0, 0);

	/// <summary>
	/// X方向縮放的AnimationCurve
	/// </summary>
	public AnimationCurve ScaleXCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
	/// <summary>
	/// Y方向縮放的AnimationCurve
	/// </summary>
	public AnimationCurve ScaleYCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	//播放時間
	public float Duration = 1;
	float timer = 0;
	float normalTimeScale = 0;
	public bool RunAnimation()
	{
		if (textComponent == null)
			return false;
		if (timer > Duration)
			return false;
		timer += Time.deltaTime;
		//播放時間轉為0~1的區間
		float normalTime = timer * normalTimeScale;

		//位移動畫
		if (PosXCurve.length > 0 || PosYCurve.length > 0)
		{
			Vector3 newPos;
			if (PosSeperateXY)
			{
				newPos.x = PosStart.x + PosOffset.x * PosXCurve.Evaluate(normalTime);
				newPos.y = PosStart.y + PosOffset.y * PosYCurve.Evaluate(normalTime);
				newPos.z = PosStart.z;
			}
			else
				newPos = PosStart + PosOffset * PosXCurve.Evaluate(normalTime);
			textComponent.transform.localPosition = newPos;
		}
		else
		{
			float z = textComponent.transform.localPosition.z;
			Vector3 newPos = PosEnd;
			newPos.z = z;
			textComponent.transform.localPosition = newPos;
		}

		//變色動畫
		if (ColorCurve.length > 0)
			textComponent.color = Color.Lerp(ColorStart, ColorEnd, ColorCurve.Evaluate(normalTime));
		else
			textComponent.color = ColorEnd;

		//縮放
		if (ScaleXCurve.length > 0 || ScaleYCurve.length > 0)
		{
			//z 不用變先暫存
			float z = textComponent.transform.localScale.z;
			Vector3 newScale;
			if (ScaleSeperateXY)
			{
				newScale.x = ScaleStart.x + ScaleOffset.x * ScaleXCurve.Evaluate(normalTime);
				newScale.y = ScaleStart.y + ScaleOffset.y * ScaleYCurve.Evaluate(normalTime);
			}
			else
				newScale = ScaleStart + ScaleOffset * ScaleXCurve.Evaluate(normalTime);
			newScale.z = z;
			textComponent.transform.localScale = newScale;
		}
		else
		{
			float z = textComponent.transform.localScale.z;
			Vector3 newScale = ScaleEnd;
			newScale.z = z;
			textComponent.transform.localScale = newScale;
		}
		return true;
	}
	public void Play()
	{
		Show();
		timer = 0;
		if (Duration == 0)
			normalTimeScale = 0;
		else
			normalTimeScale = 1 / Duration;
		PosOffset = PosEnd - PosStart;
		ScaleOffset = ScaleEnd - ScaleStart;
	}

	public void Hide()
	{
		if (textComponent != null)
			textComponent.gameObject.SetActive(false);
	}
	public void Show()
	{
		if (textComponent != null)
			textComponent.gameObject.SetActive(true);
	}

	public void ClearAnimation()
	{
		PosSeperateXY = false;
		PosStart= PosEnd = Vector3.zero;
		PosXCurve = PosYCurve = new AnimationCurve();

		ScaleSeperateXY = false;
		ScaleXCurve = ScaleYCurve = new AnimationCurve();
		ScaleStart = ScaleEnd = Vector3.one;

		ColorStart = ColorEnd = Color.white;
		ColorCurve = new AnimationCurve();
	}
}
