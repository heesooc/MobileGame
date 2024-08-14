Shader "Supercent/ShadowColor" 
{
	Properties 
	{
		_Color 			("Main Color", Color) = (1,1,1,1)
		_SpecColor 		("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess 		("Shininess", Range (0.03, 1)) = 0.078125
		_MainTex 		("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap 		("Normalmap", 2D) = "bump" {}
		_Threshold 		("Shadow Threshold", Range(0,2)) = 1
		_ShadowSoftness ("Shadow Smoothness", Range(0.5, 1)) = 0.6
		_ShadowColor 	("Shadow Color", Color) = (0,0,0,1)
		_RimPower 		("Rim Power", Range(0, 1)) = 1
		_RimWeight      ("Rim Weight", Range(0, 1)) = 0.5
		_RimColor       ("Rim Color", Color) = (0,0,0,1)
	}

	CGINCLUDE

	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	half _Shininess;

	half	_Threshold;
	half	_ShadowSoftness;
	half3	_ShadowColor;
	half    _RimPower;
	half    _RimWeight;
	half3   _RimColor;

	struct Input 
	{
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};

	inline half4 LightingToon(SurfaceOutput s, half3 lightDir, float3 viewDir, half atten)
	{
#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize(lightDir);
#endif
		half shadowDot = pow(dot(s.Normal, lightDir) * 0.5 + 0.5, _Threshold);
		float threshold = smoothstep(0.5, _ShadowSoftness, shadowDot);
		half3 diffuseTerm = saturate(threshold * atten);
		half3 diffuse = lerp(_ShadowColor, _LightColor0.rgb, diffuseTerm);

		// float3 rimColor;
		// float rim = abs(dot(viewDir, s.Normal));
		// float invrim = 1 - rim;
		// rimColor = _RimColor * (pow(invrim, 6 * (1 - _RimWeight)) * _RimPower);

		float4 final;
		final.rgb = s.Albedo.rgb * diffuse;
		final.rgb = final.rgb;// + rimColor;
		final.a = s.Alpha;

		return final;
	}

	void surf (Input IN, inout SurfaceOutput o) 
	{
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG

	SubShader 
	{ 
		Tags { "RenderType"="Opaque" }
		LOD 400
	
		CGPROGRAM
		#pragma surface surf Toon addshadow
		#pragma multi_compile_instancing
		ENDCG
	}
	Fallback "Diffuse"
}
