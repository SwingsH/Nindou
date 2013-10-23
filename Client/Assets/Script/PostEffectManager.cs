using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//一次只會有一種效果
//主要是以grabpass的功能放在攝影機前面來達到全螢幕的效果
public class PostEffectManager : MonoBehaviour {
	
	public Shader Shader_AddColor = Shader.Find("Custom/MaskEffect");
	public Shader Shader_GrayScaleColor = Shader.Find("Custom/GrayScaleColorEffect");
	const string EFFECT_COLOR_PROPERTY = "_EffectColor";

	List<PostEffectInfo> EffectStack = new List<PostEffectInfo>();
	Dictionary<int, PostEffectInfo> CallerList = new Dictionary<int, PostEffectInfo>();
	
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
		SetPostEffect(Caller, Shader_AddColor, Color.red, Duration);
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
			quad.transform.localScale = new Vector3(3000, 1550, 1);
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
					if (m.HasProperty(setting.Key))
					{
						if (setting.Value is float)
							m.SetFloat(setting.Key, (float)setting.Value);
						else if (setting.Value is Color)
							m.SetColor(setting.Key, (Color)setting.Value);
						else if (setting.Value is Vector4)
							m.SetVector(setting.Key, (Vector4)setting.Value);
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