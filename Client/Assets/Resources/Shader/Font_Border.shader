Shader "Custom/Text Border(Only) Shader" {
	//因為pass的順序沒有固定，所以要分成兩個Material來做，這裡只做描邊的部份
	Properties {
		_MainTex ("Font Texture", 2D) = "white" {}
		_BorderSize ("Border Size", float) = 3
		_BorderColor ("Border Color", Color) = (0,0,0,1)
	}

	SubShader {

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off Cull Off ZWrite Off Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		CGINCLUDE
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		
		sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform fixed4 _BorderColor;
		fixed _BorderSize;
		
		fixed4 border_frag (v2f i) : COLOR
		{
			fixed4 col = _BorderColor;
			col.a = i.color.a;
			col.a *= tex2D(_MainTex, i.texcoord).a * 1.75;
			return col;
		}
		ENDCG 
		
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment border_frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			
			v2f vert (appdata_t v)
			{
				v2f o;
				v.vertex.xyz += fixed3(_BorderSize,-_BorderSize,0.1);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			
			ENDCG 
		}
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment border_frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			
			v2f vert (appdata_t v)
			{
				v2f o;
				v.vertex.xyz += fixed3(_BorderSize,_BorderSize,0.1);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			
			ENDCG 
		}
		
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment border_frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			
			v2f vert (appdata_t v)
			{
				v2f o;
				v.vertex.xyz += fixed3(-_BorderSize,-_BorderSize,0.1);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			
			ENDCG 
		}
		
		
		
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment border_frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			
			v2f vert (appdata_t v)
			{
				v2f o;
				v.vertex.xyz += fixed3(-_BorderSize,_BorderSize,0.1);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			
			ENDCG 
		}
		

		
	} 	
	FallBack "Unlit/Transparent"
}
