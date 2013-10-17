using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PostEffect : MonoBehaviour {
	[SerializeField]
	protected Shader effectShader;
	public Shader EffectShader
	{
		set 
		{
			effectShader = value;
			if (value != null)
			{
				if (effectMaterial == null)
					effectMaterial = new Material(effectShader);
				else
					effectMaterial.shader = effectShader;
			}
		}
		get
		{
			return effectShader;
		}
	}
	[SerializeField]
	protected Material effectMaterial;
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
#if UNITY_EDITOR
		EffectShader = effectShader;
#endif
		if (effectMaterial != null)
			Graphics.Blit(src, dest, effectMaterial);
		else
			Graphics.Blit(src, dest);
	}
}
