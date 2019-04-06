Shader "Custom/ADVShaderCutout"
{
	Properties{
	_Color("Main Color", Color) = (1,1,1,1)
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_Highlight("Highlight", Color) = (1,1,1,1)
		_HighlightIntensity("Highlight Intensity", Range(0, 10)) = 1
		_Shadow("Shadow", Color) = (0,0,0,0)
		_HSM("Height Shading Map", 2D) = "white" {}
	_testVal("Test val", Range(0,10)) = 0.5
	}

		SubShader{
			Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
			LOD 200
			Cull Off

		CGPROGRAM
		#pragma surface surf ADVToon noambient alphatest:_Cutoff
		sampler2D _HSM;
		sampler2D _MainTex;
		fixed4 _Color;
		fixed4 _Highlight;
		fixed4 _Shadow;
		float _HighlightIntensity;
		float _testVal;
		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed Alpha;
			fixed3 Emission;
			fixed4 HSMValue;
		};
		half4 LightingADVToon(SurfaceOutputCustom s, half3 lightDir, half atten) {
			/*half NdotL = dot(s.Normal, lightDir);
			half4 c;
			if ((NdotL + (s.HSMValue.r - _testVal)) / 2 > 0.5) {
				c.rgb = s.Albedo * _Highlight.rgb * _HighlightIntensity;
			}
			else {
				c.rgb = s.Albedo * _Shadow.rgb;
			}
			if (atten < 0.5) {
				c.rgb = s.Albedo * _Shadow.rgb;
			}*/
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			if ((NdotL + (s.HSMValue.r - _testVal)) / 2 > 0.5) {
				c.rgb = s.Albedo * _Highlight.rgb * _HighlightIntensity * (NdotL);
			}
			else {
				c.rgb = s.Albedo * _Shadow.rgb;
			}
			if (atten < 0.5) {
				c.rgb = s.Albedo * _Shadow.rgb;
			}
			
			c.a = s.Alpha;
			return c;
		}
		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputCustom o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.HSMValue = tex2D(_HSM, IN.uv_MainTex);
		}
		ENDCG
	}

		Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
