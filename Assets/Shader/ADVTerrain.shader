Shader "Custom/ADVTerrain"
{
    Properties
    {
		_Highlight("Highlight", Color) = (1,1,1,1)
		_HighlightIntensity("Highlight Intensity", Range(0, 10)) = 1
		_Shadow("Shadow", Color) = (0,0,0,0)
		_Splat0Tilling("Splat 0 Tilling", Range(0, 100)) = 1
		_Splat1Tilling("Splat 1 Tilling", Range(0, 100)) = 1
		_Splat2Tilling("Splat 2 Tilling", Range(0, 100)) = 1
		_Splat3Tilling("Splat 3 Tilling", Range(0, 100)) = 1
			// Splat Map Control Texture
		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		// Textures
		[HideInInspector] _Splat3("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0("Layer 0 (R)", 2D) = "white" {}

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

        sampler2D _Control;
		sampler2D _Splat0;
		sampler2D _Splat1;
		sampler2D _Splat2;
		sampler2D _Splat3;
		fixed4 _Highlight;
		fixed4 _Shadow;
		float _HighlightIntensity;
        struct Input
        {
            float2 uv_Control;
        };

		half4 LightingADVToon(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			if (NdotL > 0.5) {
				c.rgb = s.Albedo * _Highlight.rgb * _HighlightIntensity * atten;
			}
			else {
				c.rgb = s.Albedo * _Shadow.rgb * atten;
			}
			if (atten < 0.5) {
				c.rgb = s.Albedo * _Shadow.rgb * atten;
			}
			c.a = s.Alpha;
			return c;
		}
        
		half _Splat0Tilling;
		half _Splat1Tilling;
		half _Splat2Tilling;
		half _Splat3Tilling;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_Control, IN.uv_Control);
			fixed4 splat0 = tex2D(_Splat0, IN.uv_Control * _Splat0Tilling);
			fixed4 splat1 = tex2D(_Splat1, IN.uv_Control * _Splat1Tilling);
			fixed4 splat2 = tex2D(_Splat2, IN.uv_Control * _Splat2Tilling);
			fixed4 splat3 = tex2D(_Splat3, IN.uv_Control * _Splat3Tilling);
            o.Albedo = splat0 * c.r + splat1 * c.g + splat2 * c.b + splat3 * c.a;
            // Metallic and smoothness come from slider variables
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
