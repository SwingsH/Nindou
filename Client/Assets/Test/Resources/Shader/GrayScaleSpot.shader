Shader "Custom/GrayScaleSpot" {
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
		
		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord: TEXCOORD0;
		};
		struct v2f {
			float4 vertex : POSITION;
			half4 pos;
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
			o.pos = mul(_Object2World, v.vertex);
			o.uvgrab.zw = o.vertex.zw;
			return o;
		}
		half4 frag( v2f i ) : COLOR
		{	
			half4 col = tex2Dproj( _GrabTex, i.uvgrab);
			if( distance(i.pos.xy,_SpotCenter.xy) > _SpotSize)
				col.rgb = Luminance(col.rgb);

			return col;
		}
		ENDCG
		}
	} 
	FallBack "Diffuse"
}
