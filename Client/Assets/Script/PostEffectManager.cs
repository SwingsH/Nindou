using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//一次只會有一種效果
//主要是以grabpass的功能放在攝影機前面來達到全螢幕的效果
public class PostEffectManager : MonoBehaviour {
	
	public Shader Shader_AddColor = Shader.Find("Custom/MaskEffect");
	public Shader Shader_GrayScaleColor = Shader.Find("Custom/GrayScaleColorEffect");

	//號碼牌
	public Queue<int> NumberCard = new Queue<int>();
	//存現在最大是幾號，新增號碼時用
	public int currentMaxNumber = -1;

	public Renderer GrabRenderer;
	public Camera TargetCamera;
	void Start()
	{
		
	}
	/// <summary>
	/// 預設效果一
	/// 注意！效果為持續型，不用要手動關閉
	/// </summary>
	/// <returns>號碼牌號碼</returns>
	public int DefaultEffect_1()
	{
		SetGrabEffect(Shader_GrayScaleColor);
		return 1;
	}

	public int SetGrabEffect(Shader s, Color effectColor, params KeyValuePair<string, object>[] shaderSetting)
	{
		if (TargetCamera == null)
			return -1;
		//未完
		return 0;
	}

	void SetGrabEffect(Shader s)
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
			GrabRenderer.transform.localPosition = new Vector3(0, 0, 0.31f);
			GrabRenderer.transform.localRotation = Quaternion.identity;
			if (GrabRenderer.sharedMaterial == null)
				GrabRenderer.sharedMaterial = new Material(s);
			else
				GrabRenderer.sharedMaterial.shader = s;
		}
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
	public void CloseEffect(int cardNumber)
	{
	}
	void ClearEffect()
	{
		if (GrabRenderer != null)
			GrabRenderer.gameObject.SetActive(false);
	}
}
