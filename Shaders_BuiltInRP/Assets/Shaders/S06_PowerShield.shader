Shader "Final Exodus/S06"
{
	Properties
	{
		//copy-paste from Ship Shader (not shielded)
		
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

		_EmissionTex			("Emission (RGB) (tile/offset from BumpMap)",				2D)						= "black" { }
		_EmissionRColor			("Emission R color",										Color)					= (1.0, 0.0, 0.0, 1.0)
		_EmissionGColor			("Emission G color",										Color)					= (0.0, 1.0, 0.0, 1.0)
		_EmissionBColor			("Emission B color",										Color)					= (0.0, 0.0, 1.0, 1.0)

		_Cube					("Reflection Cubemap",										CUBE)					= "" { }
		_ReflectionPowerDirect	("Reflection Power Direct",									Range(0.00, 3.00))		= 0.5
		_ReflectionPowerEdges	("Reflection Power Edges",									Range(0.00, 3.00))		= 0.0

		//eof copy-paste

		_PowerShieldOffset		("Power shield offset",										Range(-10, 10))			= 2
		_PowerShieldRefColor	("Power shield reflect color",								Color)					= (0.0, 1.0, 0.0, 1.0)
		_PowerShieldEdgeColor	("Power shield edge color",									Color)					= (0.0, 0.0, 1.0, 1.0)
		_PowerShieldEdgeOffset	("Power shield edge offset",								Range(0, 1))			= 0.5
		_PowerColorsBlur		("Power shield blur",										Range(0.5, 50))			= 10
		_PowerWavesColor		("Power waves color",										Color)					= (1.0, 0.0, 0.0, 1.0)
		_PowerWavesFrequency	("Power waves frequency",									Range(0.01, 3))			= 2
		_PowerHitPoint			("Power hit point",											Vector)					= (0.0, 0.0, 200.0, 0.0)
		_PowerHitTime			("Power hit time",											Float)					= 0.0
		_PowerHitWavesRamp		("Power hit waves ramp (near->far)",						2D)						= "black"
	}

	SubShader
	{
		//copy-paste from Ship Shader (not shielded)
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

		sampler2D _EmissionTex;
		fixed4 _EmissionRColor;
		fixed4 _EmissionGColor;
		fixed4 _EmissionBColor;

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

			//emission - lights
			fixed3 emissionMask = tex2D(_EmissionTex, IN.uv_BumpMap).rgb;
			emissionMask *= fixed3(_EmissionRColor.a, _EmissionGColor.a, _EmissionBColor.a);
			o.Emission = _EmissionRColor.rgb * emissionMask.r + _EmissionGColor.rgb * emissionMask.g + _EmissionBColor.rgb * emissionMask.b;

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
		//eof copy-paste

		//shield pass
		Pass
		{
			Blend OneMinusDstColor One
			
			Cull Back

			Offset -1, -1

			Lighting Off
			
			ZWrite Off
		
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			float _PowerShieldOffset;
			fixed4 _PowerShieldRefColor;
			fixed4 _PowerShieldEdgeColor;
			float _PowerShieldEdgeOffset;
			float _PowerColorsBlur;
			fixed4 _PowerWavesColor;
			float _PowerWavesFrequency;
			float4 _PowerHitPoint;
			float _PowerHitTime;
			sampler2D _PowerHitWavesRamp;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION; // project view position
				float4 wPos : TEXCOORD0; // world-based position
				float4 wNor : TEXCOORD1; // world-based normal
				float4 lPos : TEXCOORD2;
				float3 lNor : TEXCOORD3;
			};

			v2f vert(appdata v)
			{
				v2f o;

				v.vertex.xyz += pow(2, _PowerShieldOffset) * v.normal;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.lPos = v.vertex;
				o.lNor = v.normal;
				o.wPos = mul(unity_ObjectToWorld, v.vertex);
				o.wNor = mul(unity_ObjectToWorld, float4(v.normal.xyz, 0));

				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				float3 worldNormal = normalize(i.wNor.xyz);
				float3 worldToCameraDirection = normalize(_WorldSpaceCameraPos - i.wPos.xyz);
				float angleViewToNormal = acos(dot(worldNormal, worldToCameraDirection)) * 0.63661977236758134307553505349006;
				// 2/PI = 0.63661977236758134307553505349006 - so we got angleViewToNormal = 0 at 0 angle (codirectional), 1 at 90 degress angle, 2 at 180

				float dotNormalToView = dot(worldNormal, worldToCameraDirection);
				float dotNormalToShipForward = dot(normalize(i.lNor), float3(0.0, 0.0, 1.0));
				
				half4 answer = 0;
				fixed colorPower;

				float directKoef = 1 - angleViewToNormal;
				colorPower = pow(directKoef, _PowerColorsBlur) * _PowerShieldRefColor.a;
				answer.rgb += _PowerShieldRefColor.rgb * colorPower;

				float edgeKoef = 1 - abs(1 - angleViewToNormal - _PowerShieldEdgeOffset);
				colorPower = pow(edgeKoef, _PowerColorsBlur) * _PowerShieldEdgeColor.a;
				answer.rgb += _PowerShieldEdgeColor.rgb * colorPower;

				float ringKoef = abs(fmod(dotNormalToShipForward * _PowerWavesFrequency + _Time.z + 100, 2) - 1);
				colorPower = pow(ringKoef, _PowerColorsBlur) * _PowerWavesColor.a;
				answer.rgb += _PowerWavesColor.rgb * colorPower;
				
				float timeAfterHit = _Time.y - _PowerHitTime;
				float distanceToHit = distance(_PowerHitPoint.xyz, i.lPos.xyz) * _PowerHitPoint.w;
				float hitKoef = distanceToHit - timeAfterHit + 1;
				answer.rgb += tex2D(_PowerHitWavesRamp, float2(hitKoef, hitKoef)).rgb * (hitKoef > 1 ? 0 : hitKoef < 0 ? 0 : 1) * saturate(2 - timeAfterHit);

				answer.rgb *= saturate(directKoef);
				return answer;
			}

			ENDCG
		}
	}
}
