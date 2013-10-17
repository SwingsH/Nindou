Shader "Custom/MaskPostEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MaskColor("MaskColor",Color) = (1,0.3,0.3,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		pass{			
			SetTexture[_MainTex]{
				constantColor [_MaskColor]
				combine texture * constant
			}
		}
	} 
	FallBack "Diffuse"
}
