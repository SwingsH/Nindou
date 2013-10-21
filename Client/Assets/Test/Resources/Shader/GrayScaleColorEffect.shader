Shader "Custom/GrayScaleColorEffect" {
	Properties {
		_EffectColor("MaskColor",Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Overlay"}
		LOD 200
		GrabPass {							
			"_GrabTex"
 		}
		pass
		{
		CGPROGRAM
		#pragma target 2.0
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		
		sampler2D _GrabTex;
		float4 _GrabTex_TexelSize;
		float4 _EffectColor;
		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord: TEXCOORD0;
		};
		struct v2f {
			float4 vertex : POSITION;
			float4 uvgrab : TEXCOORD0;
		};
		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
			//o.uvgrab.xy += float2(0.5,-0.4)*_GrabTex_TexelSize.xy;
			o.uvgrab.zw = o.vertex.zw;
			return o;
		}
		half4 frag( v2f i ) : COLOR
		{	
			half4 col = tex2Dproj( _GrabTex, i.uvgrab);
			col.rgb = Luminance(col.rgb) * _EffectColor.rgb;
			return col;
		}
		ENDCG
		}
	} 
	FallBack "Diffuse"
}
