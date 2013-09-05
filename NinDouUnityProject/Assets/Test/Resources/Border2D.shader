Shader "Custom/Border2D" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BorderColor("BorderColor",Color) = (1,1,1,1)
		_BorderWidth("BorderWidth", float) = 1
		_BorderAlphaTest("BorderAlphaTest",float) = 0
	}
	
	CGINCLUDE	
	sampler2D _MainTex;
	half4 _MainTex_TexelSize;
	fixed4 _BorderColor;
	int _BorderWidth;
	float _BorderAlphaTest;
	struct appdata{
	    float4 vertex : POSITION;
	    float2 texcoord : TEXCOORD0;
	};
	struct v2f {
	    float4 vertex : POSITION;
	    float2 texcoord : TEXCOORD0;
	};
	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
		o.texcoord = v.texcoord;
		return o;
	}
	fixed4 frag(v2f i):Color
	{
		
		fixed4 cc = tex2D(_MainTex,i.texcoord);
		fixed4 rc = fixed4(0,0,0,0);
		if(cc.a <= _BorderAlphaTest)
		{
			float2 tt = i.texcoord ;
			tt.x -= _MainTex_TexelSize.x *_BorderWidth;
			fixed4 ct = tex2D(_MainTex,tt);
			if(ct.a >0)
			{
				rc = _BorderColor;
				i.vertex.z -=1;
			}
			tt = i.texcoord ;
			tt.x += _MainTex_TexelSize.x *_BorderWidth;
			ct = tex2D(_MainTex,tt);
			if(ct.a >0)
			{
				rc = _BorderColor;
				i.vertex.z -=1;
			}
			tt = i.texcoord ;
			tt.y -= _MainTex_TexelSize.y *_BorderWidth;
			ct = tex2D(_MainTex,tt);
			if(ct.a >0)
			{
				rc = _BorderColor;
				i.vertex.z -=1;
			}
			tt = i.texcoord ;
			tt.y += _MainTex_TexelSize.y *_BorderWidth;
			ct = tex2D(_MainTex,tt);
			if(ct.a >0)
			{
				rc = _BorderColor;
				i.vertex.z -=1;
			}
		}
		return rc;
	}
	ENDCG
	
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		Pass {
			SetTexture [_MainTex] {
				combine texture * primary
			}
		}
	}
	//FallBack "Mobile/Particles/Alpha Blended" 
}
