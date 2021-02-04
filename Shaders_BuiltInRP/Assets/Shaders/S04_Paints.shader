Shader "Final Exodus/S04"
{
	Properties
	{
		_BumpMap				("BumpMap (tile/offset works here)",						2D)						= "bump" { }

		_SurfacesMaskTex		("Surfaces mask (RGBA) (tile/offset from BumpMap)",			2D)						= "black" { }

		_SurfaceRSpecGlossiness	("Surface R specular glossiness",							Range(0, 1))			= 0.1
		_SurfaceRSpecShininess	("Surface R specular shininess",							Range(0.01, 1))			= 0.1
		_SurfaceRReflection		("Surface R reflection power",								Range(0, 3))			= 0.1
		_SurfaceRColor			("Surface R color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_SurfaceRTex			("Surface R texture (tile/offset works OK)",				2D)						= "white" { }

		_SurfaceGSpecGlossiness	("Surface G specular glossiness",							Range(0, 1))			= 0.1
		_SurfaceGSpecShininess	("Surface G specular shininess",							Range(0.01, 1))			= 0.1
		_SurfaceGReflection		("Surface G reflection power",								Range(0, 3))			= 0.1
		_SurfaceGColor			("Surface G color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_SurfaceGTex			("Surface G texture (tile/offset from R channel)",			2D)						= "white" { }

		_SurfaceBSpecGlossiness	("Surface B specular glossiness",							Range(0, 1))			= 0.1
		_SurfaceBSpecShininess	("Surface B specular shininess",							Range(0.01, 1))			= 0.1
		_SurfaceBReflection		("Surface B reflection power",								Range(0, 3))			= 0.1
		_SurfaceBColor			("Surface B color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_SurfaceBTex			("Surface B texture (tile/offset from R channel)",			2D)						= "white" { }

		_SurfaceASpecGlossiness	("Surface A specular glossiness",							Range(0, 1))			= 0.1
		_SurfaceASpecShininess	("Surface A specular shininess",							Range(0.01, 1))			= 0.1
		_SurfaceAReflection		("Surface A reflection power",								Range(0, 3))			= 0.1
		_SurfaceAColor			("Surface A color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_SurfaceATex			("Surface A texture (tile/offset from R channel)",			2D)						= "white" { }

		_PaintsMaskTex			("Paints mask (RGB) (tile/offset from BumpMap)",			2D)						= "black" { }

		_PaintRColor			("Paint R color",											Color)					= (1.0, 0.0, 0.0, 0.5)
		_PaintGColor			("Paint G color",											Color)					= (0.0, 1.0, 0.0, 0.5)
		_PaintBColor			("Paint B color",											Color)					= (0.0, 0.0, 1.0, 0.5)

		_Cube					("Reflection Cubemap",										CUBE)					= "" { }
		_ReflectionPowerDirect	("Reflection Power Direct",									Range(0.00, 3.00))		= 0.5
		_ReflectionPowerEdges	("Reflection Power Edges",									Range(0.00, 3.00))		= 0.0
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
		sampler2D _SurfacesMaskTex;

		float _SurfaceRSpecGlossiness;
		float _SurfaceRSpecShininess;
		float _SurfaceRReflection;
		fixed4 _SurfaceRColor;
		sampler2D _SurfaceRTex;

		float _SurfaceGSpecGlossiness;
		float _SurfaceGSpecShininess;
		float _SurfaceGReflection;
		fixed4 _SurfaceGColor;
		sampler2D _SurfaceGTex;

		float _SurfaceBSpecGlossiness;
		float _SurfaceBSpecShininess;
		float _SurfaceBReflection;
		fixed4 _SurfaceBColor;
		sampler2D _SurfaceBTex;

		float _SurfaceASpecGlossiness;
		float _SurfaceASpecShininess;
		float _SurfaceAReflection;
		fixed4 _SurfaceAColor;
		sampler2D _SurfaceATex;

		sampler2D _PaintsMaskTex;

		fixed4 _PaintRColor;
		fixed4 _PaintGColor;
		fixed4 _PaintBColor;

		samplerCUBE _Cube;
		float _ReflectionPowerDirect;
		float _ReflectionPowerEdges;

		//internal data
		float4 specMaskPowers;

		struct Input
		{
			float2 uv_BumpMap;
			float2 uv_SurfaceRTex;
			float3 viewDir;
			float3 worldRefl;
			INTERNAL_DATA
		};

		inline fixed4 CalcPaintColor(float2 texMapping)
		{
			//calc paint mask by RGB
			fixed3 paintsMask = tex2D(_PaintsMaskTex, texMapping).rgb;

			//applying alpha-power from channel-colors
			paintsMask.rgb *= fixed3(_PaintRColor.a, _PaintGColor.a, _PaintBColor.a);
			
			//calc final paint color
			fixed4 paintColor;
			paintColor.rgb = _PaintRColor.rgb * paintsMask.r + _PaintGColor.rgb * paintsMask.g + _PaintBColor.rgb * paintsMask.b;
			paintColor.a = 1 - (1 - paintsMask.r) * (1 - paintsMask.g) * (1 - paintsMask.b);
			return paintColor;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 paintColor = CalcPaintColor(IN.uv_BumpMap);

			//calc surface color
			fixed4 surfacesMask = tex2D(_SurfacesMaskTex, IN.uv_BumpMap);
			fixed3 surfaceColor = 0;
			//surfaces mask
			surfaceColor += (tex2D(_SurfaceRTex, IN.uv_SurfaceRTex).rgb * _SurfaceRColor.rgb) * (_SurfaceRColor.a * surfacesMask.r);
			surfaceColor += (tex2D(_SurfaceGTex, IN.uv_SurfaceRTex).rgb * _SurfaceGColor.rgb) * (_SurfaceGColor.a * surfacesMask.g);
			surfaceColor += (tex2D(_SurfaceBTex, IN.uv_SurfaceRTex).rgb * _SurfaceBColor.rgb) * (_SurfaceBColor.a * surfacesMask.b);
			surfaceColor += (tex2D(_SurfaceATex, IN.uv_SurfaceRTex).rgb * _SurfaceAColor.rgb) * (_SurfaceAColor.a * surfacesMask.a);

			o.Albedo = lerp(surfaceColor.rgb, paintColor.rgb, paintColor.a);

			//alpha
			//o.Alpha = 1;

			//specular
			specMaskPowers = surfacesMask * (1 - paintColor.a * 0.5);

			//normal
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

			//emission - reflection
			half reflectionPower = dot(half4(_SurfaceRReflection, _SurfaceGReflection, _SurfaceBReflection, _SurfaceAReflection), surfacesMask) * (1 - paintColor.a);
			fixed3 reflectionColor = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb;
			o.Emission += reflectionColor * lerp(_ReflectionPowerEdges, _ReflectionPowerDirect, saturate(dot(o.Normal, normalize(IN.viewDir)))) * reflectionPower;
		}

		half4 LightingShipLight (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 normal = normalize(s.Normal);
			half3 answer;

			answer = s.Albedo * saturate(dot(normal, lightDir));

			float nh = max(0, dot(normal, normalize(lightDir + viewDir)));
			specMaskPowers = specMaskPowers * fixed4(_SurfaceRSpecGlossiness, _SurfaceGSpecGlossiness, _SurfaceBSpecGlossiness, _SurfaceASpecGlossiness);
			float4 specShininess = float4(_SurfaceRSpecShininess, _SurfaceGSpecShininess, _SurfaceBSpecShininess, _SurfaceASpecShininess) * 128.0;
			specMaskPowers = specMaskPowers * pow(float4(nh, nh, nh, nh), specShininess);
			answer += _SurfaceRColor.rgb * specMaskPowers.r + _SurfaceGColor.rgb * specMaskPowers.g + _SurfaceBColor.rgb * specMaskPowers.b + _SurfaceAColor.rgb * specMaskPowers.a;

			answer *= _LightColor0.rgb * atten * 2;

			return half4(answer, 1);
		}

		ENDCG
	}
}
