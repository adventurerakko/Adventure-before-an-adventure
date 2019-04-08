Shader "Custom/ADVShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Highlight("Highlight", Color) = (1,1,1,1)
		_HighlightIntensity("Highlight Intensity", Range(0, 10)) = 1
		_Shadow ("Shadow", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf ADVToon noambient

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		fixed4 _Highlight;
		fixed4 _Shadow;
		float _HighlightIntensity;
		//optimize this shader code **********
		half4 LightingADVToon(SurfaceOutput s, half3 lightDir, half atten) {

			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			if (_WorldSpaceLightPos0.w == 0) {// if is using directional light 

				if (NdotL > 0.5) {
					c.rgb = s.Albedo * _Highlight.rgb * _HighlightIntensity * NdotL;
				}
				else {
					c.rgb = s.Albedo * _Shadow.rgb;
				}
				c.a = s.Alpha;
		}
		else { // if using point light
				c.rgb = s.Albedo * _Highlight.rgb * _HighlightIntensity * atten * _LightColor0.rgb;
			}
	
			
			return c;
		}

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
