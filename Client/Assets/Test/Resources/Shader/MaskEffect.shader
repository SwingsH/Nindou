Shader "Custom/MaskEffect" {
	Properties {
		_EffectColor("MaskColor",Color) = (1,0.3,0.3,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Overlay"}
		LOD 200
		GrabPass{"_GrabTex"}
		pass{			
			SetTexture[_GrabTex]{
				constantColor [_EffectColor]
				combine texture * constant
			}
		}
	} 
	FallBack "Diffuse"
}
