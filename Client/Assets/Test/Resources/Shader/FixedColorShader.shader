Shader "Custom/FixedColorShader" {
	Properties {
		_Color ("Main Color", Color) = (0,0,0,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		pass
		{
			settexture[_MainTex]
			{
				constantcolor[_Color]
				combine constant,texture
			}
		}
	} 
	FallBack "Diffuse"
}
