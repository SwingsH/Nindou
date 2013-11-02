Shader "Custom/SingleColorShader" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			Settexture[_MainTex]
			{
				constantcolor[_Color]
				combine constant,texture alpha
			}
		}
	} 
	FallBack "Diffuse"
}
