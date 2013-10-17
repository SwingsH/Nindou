using UnityEngine;
using System.Collections;

public class PostEffectTester : MonoBehaviour {
	public Shader GPTest1;
	public Shader GPTest2;
	public Shader PETest1;
	public Shader PETest2;

	public Renderer GrabRenderer;
	// Use this for initialization
	void Start () {
		GPTest1 = Shader.Find("Custom/MaskEffect");
		GPTest2 = Shader.Find("Custom/GrayScaleColorEffect");
		PETest1 = Shader.Find("Custom/MaskPostEffect");
		PETest2 = Shader.Find("Custom/GrayScaleColorPostEffect");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		Rect r = new Rect(Screen.width - 100, Screen.height * 0.5f, 100, 40);
		r.y -= r.height * 2;
		Shader[] sh = new Shader[] { GPTest1, GPTest2, PETest1, PETest2 };
		for (int i = 0; i < sh.Length; i++)
		{
			if(sh[i] != null)
			{
				if (GUI.Button(r, sh[i].name))
				{
					if (i < 2)
						SetGrabEffect(sh[i]);
					else
						SetPostEffect(sh[i]);
				}
			}
			r.y += r.height;
		}
		if (GUI.Button(r, "Clear Effect"))
			ClearEffect();
	}

	void SetPostEffect(Shader s)
	{
		ClearEffect();
		if(BattleManager.UnitCamera == null)
			return;
		PostEffect pe = BattleManager.UnitCamera.GetComponent<PostEffect>();
		if(pe == null)
			pe = BattleManager.UnitCamera.gameObject.AddComponent<PostEffect>();
		if (s == null)
		{
			pe.enabled = false;
		}
		else
		{
			pe.enabled = true;
			pe.EffectShader = s;
		}
	}
	void SetGrabEffect(Shader s)
	{
		ClearEffect();
		if(BattleManager.UnitCamera == null)
		{
			if(GrabRenderer != null)
				GrabRenderer.gameObject.SetActive(false);
			return;
		}
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
			GrabRenderer.transform.parent = BattleManager.UnitCamera.transform;
			GrabRenderer.transform.localPosition = new Vector3(0, 0, 0.31f);
			GrabRenderer.transform.localRotation = Quaternion.identity;
			if (GrabRenderer.sharedMaterial == null)
				GrabRenderer.sharedMaterial = new Material(s);
			else
				GrabRenderer.sharedMaterial.shader = s;
		}
	}
	void ClearEffect()
	{
		if (GrabRenderer != null)
			GrabRenderer.gameObject.SetActive(false);
		if (BattleManager.UnitCamera != null)
		{
			PostEffect pe = BattleManager.UnitCamera.GetComponent<PostEffect>();
			if (pe != null)
				pe.enabled = false;
		}
	}
}
