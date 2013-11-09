Shader "Custom/GrayScaleTexSpot" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpotCenter ("Spot Center (World Position)", Vector) = (0,0,0,0)
		_SpotSize ("Spot Size",float) = 0.5
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
		// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members vertex)
		#pragma exclude_renderers d3d11 xbox360
		#pragma target 2.0
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		
		sampler2D _GrabTex;
		float4 _GrabTex_TexelSize;
		half4 _SpotCenter;
		float _SpotSize;
  		sampler2D _MainTex;
  		float4 _MainTex_ST;
		struct appdata_t {
			float4 vertex : POSITION;
			float4 texcoord: TEXCOORD0;
		};
		struct v2f {
			float4 vertex : POSITION;
			half4 pos;
			float4 uv_main : TEXCOORD0;
			float4 uvgrab : TEXCOORD1;
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
			o.pos = mul(UNITY_MATRIX_MV, v.vertex);
			o.uvgrab.zw = o.vertex.zw;
			o.uv_main = v.texcoord;
			return o;
		}
		half4 frag( v2f i ) : COLOR
		{	
			half4 col = tex2Dproj( _GrabTex, i.uvgrab);
//			if(tex2D(_MainTex,i.uv_main.xy).r > 0.5)
//				col.rgb = Luminance(col.rgb);
			float2 uvmain = ((i.pos.xy - _SpotCenter.xy) ) / _SpotSize+ float2(0.5,0.5);
			
			if(tex2Dproj(_MainTex,float4(uvmain,i.uv_main.zw)).a < 0.5)
				col.rgb = Luminance(col.rgb);
			return col;
		}
		ENDCG
		}
//		Pass
//		{
//			CGPROGRAM
//			#pragma vertex vert
//      		#pragma fragment frag
//      		#include "UnityCG.cginc"
//      		float4 _ShadowColor;
//      		sampler2D _MainTex;
//      		float4 _MainTex_ST;
//			struct appdata_simple {
//			    float4 vertex : POSITION;
//			    float4 texcoord : TEXCOORD0;
//			};
//			struct v2f
//			{
//				float4 pos : POSITION;
//				float2 texcoord : TEXCOORD0;
//			};
//			v2f vert( appdata_simple v )
//			{
//				v2f o;
//				o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
//				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
//				
//				return o;
//			}
//			
//			float4 frag( v2f i ) : COLOR
//			{
//				float4 c =tex2D(_MainTex, i.texcoord.xy);
//				return c;
//			}
//			ENDCG
//		}
	} 
	FallBack "Diffuse"
}
