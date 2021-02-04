Shader "Final Exodus/S01"
{
	Properties
	{
		_BumpMap				("BumpMap (tile/offset works here)",						2D)						= "bump" { }

		_SurfaceASpecGlossiness	("Surface R specular glossiness",							Range(0, 1))			= 0.1
		_SurfaceASpecShininess	("Surface R specular shininess",							Range(0.01, 1))			= 0.1
		_SurfaceAColor			("Surface R color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_SurfaceATex			("Surface R texture (tile/offset works OK)",				2D)						= "white" { }
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"RenderType"="Opaque"
		}

		CGPROGRAM

		#pragma surface surf ShipLight noambient nolightmap nodirlightmap
		#pragma target 3.0
		#pragma profileoption MaxLocalParams = 42

		sampler2D _BumpMap;

		float _SurfaceASpecGlossiness;
		float _SurfaceASpecShininess;
		fixed4 _SurfaceAColor;
		sampler2D _SurfaceATex;

		struct Input
		{
			float2 uv_BumpMap;
			float2 uv_SurfaceRTex;
			float3 viewDir;
			float3 worldRefl;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			//calc surface color
			fixed3 surfaceColor = 0;
			//surfaces mask
			surfaceColor += (tex2D(_SurfaceATex, IN.uv_SurfaceRTex).rgb * _SurfaceAColor.rgb) * (_SurfaceAColor.a);

			o.Albedo = surfaceColor.rgb;

			//normal
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}

		half4 LightingShipLight (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 normal = normalize(s.Normal);

			half3 answer = s.Albedo * saturate(dot(normal, lightDir));

			float nh = max(0, dot(normal, normalize(lightDir + viewDir)));

			answer += _SurfaceAColor.rgb * _SurfaceASpecGlossiness * pow(nh, _SurfaceASpecShininess * 128.0);

			answer *= _LightColor0.rgb * atten * 2;

			return half4(answer, 1);
		}

		ENDCG
	}
}
