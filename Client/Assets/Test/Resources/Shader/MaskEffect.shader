Shader "Custom/MaskEffect" {
	Properties {
		_MaskColor("MaskColor",Color) = (1,0.3,0.3,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Overlay"}
		LOD 200
		GrabPass{"_GrabTex"}
		pass{			
			SetTexture[_GrabTex]{
				constantColor [_MaskColor]
				combine texture * constant
			}
		}
	} 
	FallBack "Diffuse"
}
