using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//一次只會有一種效果
//主要是以grabpass的功能放在攝影機前面來達到全螢幕的效果
public class PostEffectManager : MonoBehaviour {
	
	public Shader Shader_AddColor = Shader.Find("Custom/MaskEffect");
	public Shader Shader_GrayScaleColor = Shader.Find("Custom/GrayScaleColorEffect");
	public Shader Shader_GrayScaleTexSpot = Shader.Find("Custom/GrayScaleTexSpot");
	const string EXTRIM_SKILL_EFFECT_BORDER_MATERIAL = "ExtrimSkillBorder";
	const string EFFECT_COLOR_PROPERTY = "_EffectColor";

	List<PostEffectInfo> EffectStack = new List<PostEffectInfo>();
	Dictionary<int, PostEffectInfo> CallerList = new Dictionary<int, PostEffectInfo>();

	//大絕特效
	Camera ExtraCamera;
	Renderer ExtraRenderer;
	const float PART2_STARTTIME = 1f;
	const float EXTRIM_TIME = 2f;
	readonly Vector3 EXTRIM_RENDERER_BORDER_START_POS = new Vector3(-600, -85, 60);
	readonly Vector3 EXTRIM_RENDERER_BORDER_END_POS = new Vector3(0, -85, 60);
	bool isPlayingExtrimeSkill = false;
	Vector3 Extrim_StartPos;
	Vector3 Extrim_EndPos;
	Unit Extrim_CastUnit;

	float ExtrimEffect_StartTime;
	AnimationCurve Extrim_AnimCurve = new AnimationCurve(new Keyframe(0.00f, 0.00f, 5.35f, 5.35f), new Keyframe(0.60f, 1.00f, 0.00f, 0.00f));

	//存現在最大是幾號，新增號碼時用
	public int currentMaxNumber = -1;

	public Renderer GrabRenderer;
	public Camera TargetCamera;
	
	void Start()
	{
		
	}

	void Update()
	{
		//判斷計時型效果的結束時間，到了就換成前一個效果，都沒效果就結束所有效果
		if (EffectStack.Count > 0 && Time.realtimeSinceStartup > EffectStack[0].EndTime)
		{
			do
			{
				CallerList.Remove(EffectStack[0].CallerID);
				EffectStack.RemoveAt(0);
			} while (EffectStack.Count > 0 && Time.realtimeSinceStartup > EffectStack[0].EndTime);
			if (EffectStack.Count > 0)
			{
				PostEffectInfo info = EffectStack[0];
				SetGrabEffect(info.shader, info.effectColor, info.shaderSetting);
			}
			else
				ClearEffect();
		}

		if (isPlayingExtrimeSkill)
			ExtrimSkillEffect();
	}
	/// <summary>
	/// 預設效果一
	/// 注意！效果為持續型，不用要手動關閉
	/// </summary>
	/// <returns>號碼牌號碼</returns>
	public void SetDefaultEffect_1(object Caller)
	{
		SetDefaultEffect_1(Caller, float.PositiveInfinity);
	}
	public void SetDefaultEffect_1(object Caller, float Duration)
	{
		SetPostEffect(Caller,Shader_GrayScaleColor,Color.white,Duration);
	}
	public void SetDefaultEffect_2(object Caller)
	{
		SetDefaultEffect_2(Caller, float.PositiveInfinity);
	}
	public void SetDefaultEffect_2(object Caller, float Duration)
	{
		SetPostEffect(Caller, Shader_AddColor, Color.gray, Duration);
	}
	public void SetDefaultEffect_3(object Caller)
	{
		Vector3 v3 = TargetCamera.ScreenToWorldPoint(Vector3.zero);
		SetPostEffect(Caller, Shader_GrayScaleTexSpot, Color.clear, new KeyValuePair<string, object>("_SpotCenter", v3), new KeyValuePair<string, object>("_SpotSize", 100));
	}
	/// <summary>
	/// 全畫面效果，永久型，記得不用要手動取消
	/// </summary>
	/// <param name="Caller">呼叫者</param>
	/// <param name="s">指定的shader</param>
	/// <param name="effectColor">效果顏色</param>
	/// <param name="shaderSetting">shader的參數設定</param>
	public void SetPostEffect(object Caller, Shader s, Color effectColor, params KeyValuePair<string, object>[] shaderSetting)
	{
		SetPostEffect(Caller, s, effectColor, float.PositiveInfinity, shaderSetting);
	}

	/// <summary>
	/// 全畫面效果
	/// </summary>
	/// <param name="Caller">呼叫者</param>
	/// <param name="s">指定的shader</param>
	/// <param name="effectColor">效果顏色</param>
	/// <param name="Duration">持續時間</param>
	/// <param name="shaderSetting">shader的參數設定</param>
	public void SetPostEffect(object Caller, Shader s, Color effectColor, float Duration, params KeyValuePair<string, object>[] shaderSetting)
	{
		if (TargetCamera == null)
			return ;
		PostEffectInfo info = new PostEffectInfo();
		info.CallerID = Caller.GetHashCode();
		info.shader = s;
		info.effectColor = effectColor;
		info.EndTime = Time.realtimeSinceStartup + Duration;
		info.shaderSetting = shaderSetting;
		EffectStack.Insert(0,info);

		//一個呼叫者一次只能用一個效果
		PostEffectInfo oldInfo = null;
		if (CallerList.TryGetValue(info.CallerID, out oldInfo))
		{
			CallerList.Remove(info.CallerID);
			EffectStack.Remove(oldInfo);
		}
		CallerList.Add(info.CallerID, info);
		SetGrabEffect(s, effectColor, shaderSetting);
	}

	void SetGrabEffect(Shader s, Color effectColor, params KeyValuePair<string, object>[] shaderSetting)
	{
		ClearEffect();
		if (TargetCamera == null)
			return;
		if (GrabRenderer == null)
		{
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.localScale = new Vector3(3000, 3000, 1);
			quad.renderer.castShadows = false;
			quad.renderer.receiveShadows = false;
			Destroy(quad.collider);
			GrabRenderer = quad.renderer;
			GrabRenderer.sharedMaterial = new Material(s);
		}

		if (s == null)
			GrabRenderer.gameObject.SetActive(false);
		else
		{
			GrabRenderer.gameObject.SetActive(true);
			GrabRenderer.transform.parent = TargetCamera.transform;
			GrabRenderer.transform.localPosition = new Vector3(0, 0, TargetCamera.nearClipPlane + 0.1f);
			GrabRenderer.transform.localRotation = Quaternion.identity;
			for (int i = 0; i < 32; i++)
			{
				if ((TargetCamera.cullingMask & (1 << i)) > 0)
				{
					GrabRenderer.gameObject.layer = i;
					break;
				}
			}
			if (GrabRenderer.sharedMaterial == null)
				GrabRenderer.sharedMaterial = new Material(s);
			else
				GrabRenderer.sharedMaterial.shader = s;

			Material m = GrabRenderer.sharedMaterial;
			m.SetColor(EFFECT_COLOR_PROPERTY, effectColor);
			if (shaderSetting != null)
			{
				foreach (KeyValuePair<string, object> setting in shaderSetting)
				{
					Debug.Log(setting.Key);
					Debug.Log(setting.Value);
					Debug.Log(setting.Value.GetType());
					if (m.HasProperty(setting.Key))
					{
						Debug.Log("HasProperty");
						if (setting.Value is float || setting.Value is int)
							m.SetFloat(setting.Key, (float)setting.Value);
						else if (setting.Value is Color)
							m.SetColor(setting.Key, (Color)setting.Value);
						else if (setting.Value is Vector4)
							m.SetVector(setting.Key, (Vector4)setting.Value);
						else if (setting.Value is Vector3)
						{
							Vector3 v3 = (Vector3)setting.Value;
							m.SetVector(setting.Key, new Vector4(v3.x,v3.y,v3.z));
						}
						else if (setting.Value is Matrix4x4)
							m.SetMatrix(setting.Key, (Matrix4x4)setting.Value);
						else if (setting.Value is Texture)
							m.SetTexture(setting.Key, setting.Value as Texture);
					}
				}
			}
		}
	}
	void SetGrabEffect(Shader s)
	{
		SetGrabEffect(s, Color.white);
	}


	/// <summary>
	/// 開始播放施放大絕前的效果
	/// </summary>
	public void ExtrimSkillEffectStart(Unit castUnit)
	{
		if (isPlayingExtrimeSkill)
			return;
		if(TargetCamera == null)
			return;
		TimeMachine.ChangeTimeScale(0.001f, EXTRIM_TIME);

		if (ExtraCamera == null)
		{
			ExtraCamera = new GameObject("ExtraCamera").AddComponent<Camera>();
			ExtraCamera.clearFlags = CameraClearFlags.Depth;
			ExtraCamera.cullingMask = 1 << GLOBALCONST.GameSetting.LAYER_EXTRAEFFECT | 1 << GLOBALCONST.GameSetting.LAYER_EXTRAUNIT;
			ExtraCamera.depth = 10;
			ExtraCamera.transform.rotation = TargetCamera.transform.rotation;
			ExtraCamera.orthographic = true;
			ExtraCamera.orthographicSize = 150;
			ExtraCamera.nearClipPlane = 0.3f;
			ExtraCamera.farClipPlane = 70;
		}
		if (ExtraRenderer == null)
		{
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.localScale = new Vector3(3000, 3000, 1);
			quad.renderer.castShadows = false;
			quad.renderer.receiveShadows = false;
			Destroy(quad.collider);
			ExtraRenderer = quad.renderer;
			ExtraRenderer.gameObject.layer = GLOBALCONST.GameSetting.LAYER_EXTRAEFFECT;
			ExtraRenderer.sharedMaterial = Resources.Load("Materials/" + EXTRIM_SKILL_EFFECT_BORDER_MATERIAL) as Material;
			ExtraRenderer.transform.parent = ExtraCamera.transform;
			ExtraRenderer.transform.localRotation = Quaternion.identity;
			ExtraRenderer.transform.localPosition = new Vector3(0, 0, 0);
			ExtraRenderer.transform.localScale = new Vector3(600, 1100, 1);
		}
		if(castUnit == null)
			return;
		Extrim_CastUnit = castUnit;
		if (Extrim_CastUnit.Entity != null)
		{
			NGUITools.SetLayer(Extrim_CastUnit.Entity, GLOBALCONST.GameSetting.LAYER_EXTRAUNIT);
			//特殊需求，大小要回到1
			Extrim_CastUnit.Entity.transform.localScale = Vector3.one;
		}
		Vector3 inTopRightPos = Extrim_CastUnit.WorldUpperLeft;
		Vector3 outTopRightPos = Extrim_CastUnit.WorldUpperRight;
		ExtraCamera.gameObject.SetActive(true);
		Extrim_StartPos = ExtraCamera.transform.position + inTopRightPos - ExtraCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		Extrim_EndPos = ExtraCamera.transform.position + outTopRightPos - ExtraCamera.ScreenToWorldPoint(new Vector3(Screen.width + 60, Screen.height, 0));//+60是目前的模型右邊留白太多，先做一點位移
		Extrim_StartPos.z += ExtraCamera.transform.forward.z * -30;
		Extrim_EndPos.z = Extrim_StartPos.z;

		ExtrimEffect_StartTime = Time.realtimeSinceStartup;



		isPlayingExtrimeSkill = true;
	}
	void ExtrimSkillEffect()
	{
		if (Time.realtimeSinceStartup > ExtrimEffect_StartTime + EXTRIM_TIME)
		{
			isPlayingExtrimeSkill = false;
			ExtraCamera.gameObject.SetActive(false);
			if (Extrim_CastUnit.Entity != null)
			{
				//還原大小跟方向
				eDirection orginDirection = Extrim_CastUnit.Direction;
				Extrim_CastUnit.Direction = eDirection.Both;
				Extrim_CastUnit.Direction = orginDirection;
				NGUITools.SetLayer(Extrim_CastUnit.Entity, GLOBALCONST.GameSetting.LAYER_UNIT);
			}
			return;
		}

		float part1Time = Mathf.Clamp(Time.realtimeSinceStartup - ExtrimEffect_StartTime, 0, PART2_STARTTIME) / PART2_STARTTIME;
		float part2Time = Mathf.Clamp(Time.realtimeSinceStartup - ExtrimEffect_StartTime - PART2_STARTTIME, 0, EXTRIM_TIME - PART2_STARTTIME) / (EXTRIM_TIME - PART2_STARTTIME);
		
		ExtraRenderer.transform.localPosition = Vector3.Lerp(EXTRIM_RENDERER_BORDER_START_POS, EXTRIM_RENDERER_BORDER_END_POS, Extrim_AnimCurve.Evaluate(part1Time));
		ExtraCamera.transform.position = Vector3.Lerp(Extrim_StartPos, Extrim_EndPos, Extrim_AnimCurve.Evaluate(part2Time));
	}

	void OnDestroy()
	{
		if (GrabRenderer != null)
			Destroy(GrabRenderer.gameObject);
	}
	/// <summary>
	/// 結束指定編號的畫面效果
	/// </summary>
	/// <param name="cardNumber">號碼牌編號</param>
	public void CloseEffect(object Caller)
	{
		int CallerID = Caller.GetHashCode();
		PostEffectInfo info;
		if (CallerList.TryGetValue(CallerID, out info))
		{
			int index = EffectStack.IndexOf(info);
			if (index == 0)
			{
				//為現在顯示中的效果，將結束時間設為0然後呼叫update來更新效果
				info.EndTime = 0;
				//更新現在的效果
				Update();
			}
			else
			{
				//非顯示中的效果直接刪就好
				CallerList.Remove(CallerID);
				EffectStack.RemoveAt(index);
			}
		}
	}
	void ClearEffect()
	{
		if (GrabRenderer != null)
			GrabRenderer.gameObject.SetActive(false);
	}
}

public class PostEffectInfo
{
	public int CallerID;
	public Shader shader;
	public Color effectColor;
	public KeyValuePair<string, object>[] shaderSetting;
	public float EndTime;
}
