// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "Final Exodus/Star"
{
	Properties
	{
		_MainTex						("Base (RGB)",											2D)						= "white" { }
		_MainBlackColor					("Black Color",											Color)					= (0.5, 0.6, 0.7, 1.0)
		_MainWhiteColor					("White Color",											Color)					= (1.0, 1.0, 1.0, 1.0)
		_NoiseTex						("Noise texture (A) (mapping ignored)",					2D)						= "white" { }
		_AtmoOverModelOffset			("Atmosphere over model offset",						Range(-0.03, 0.1))		= 0.05
		_AtmoColor						("Atmosphere inside color",								Color)					= (0.8, 0.9, 1.0, 1.0)
		_AtmoSize						("Atmosphere size",										Range(0.1, 2.0))		= 0.3
		_AtmoRamp						("Atmosphere outside ramp (RGB)",						2D)						= "white" { }
		_AtmoRampColor					("Atmosphere outside ramp color",						Color)					= (1.0, 1.0, 1.0, 1.0)
		_AtmoPowIn						("Atmosphere power inside",								Range(20, 0.3))			= 3
		_AtmoPowOut						("Atmosphere power outside",							Range(20, 2.0))			= 3
		_AtmoRaysSize					("Atmosphere rays size",								Range(1, 0))			= 1
		_AtmoRaysPower					("Atmosphere rays power",								Range(0, 2))			= 0.5
		_AtmoRaysSpeed					("Atmosphere rays speed",								Range(0, 1))			= 0.1
	}

	SubShader
	{ 
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Transparent"
		}

		Fog { Mode Off }

		Cull Back

		//star body draws here

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#define TRANSFORM_TEX(tex, name) (tex.xy * name##_ST.xy + name##_ST.zw)
			//#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _MainBlackColor;
			fixed4 _MainWhiteColor;

			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				half3 c = lerp(_MainBlackColor.rgb, _MainWhiteColor.rgb, tex2D(_MainTex, i.uv).rgb);
				return half4(c, 1);
			}

			ENDCG
		}

		//star illumination out from geometry and on top of geometry draws here

		Pass
		{
			ZWrite Off

			Blend SrcAlpha One  

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _NoiseTex;
			float _AtmoOverModelOffset;
			fixed4 _AtmoColor;
			float _AtmoSize;
			sampler2D _AtmoRamp;
			fixed4 _AtmoRampColor;
			float _AtmoPowIn;
			float _AtmoPowOut;
			float _AtmoRaysSize;
			float _AtmoRaysPower;
			float _AtmoRaysSpeed;

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
			};

			v2f vert(appdata v)
			{
				v.vertex.xyz *= 1.0 + _AtmoSize;
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.wPos = mul(unity_ObjectToWorld, v.vertex);
				o.wNor = mul(unity_ObjectToWorld, float4(v.normal.xyz, 0));
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				half4 answer;

				float3 worldNormal = normalize(i.wNor.xyz);
				float3 viewDirection = normalize(i.wPos.xyz - _WorldSpaceCameraPos);

				//value indicates screen-based angle from center of star
				float3 illuminationAngle = normalize(cross(worldNormal, viewDirection));

				//normal2view dot product - 1 at star center and 0 at atmospere visible edge
				fixed n2v = saturate(-dot(worldNormal, viewDirection));

				//relative to illumination size of star, also known as sine of illumination angle
				fixed r = 1.0 / (1.0 + _AtmoSize + _AtmoOverModelOffset);
				//normal2view value where star's edge draws - need for make pressure to both sides inside and outside calculate different
				fixed n2vX = sqrt(1.0 - r * r);
				
				if (n2v >= n2vX)
				{
					answer.rgb = _AtmoColor.rgb;
					//alpha - atmosphere quantity from 0 in the visible center of the star to 1 on the star surface
					answer.a = pow(1.0 - (n2v - n2vX) / (1.0 - n2vX), _AtmoPowIn) * _AtmoColor.a;
				}
				else
				{
					//atmosphere offset from 0 far from star to 1 on the star surface
					fixed ao = n2v / n2vX;
					//pow to atmosphere out power
					ao = pow(ao, _AtmoPowOut);
					//get ray noise texture coordinate in this view point based on view angle
					half2 rayTexCoord1 = (illuminationAngle.xy + illuminationAngle.xz + illuminationAngle.yz) * _AtmoRaysSize;
					//get ray noise texture coordinate offset by time
					half2 rayTexCoord2 = float2(-0.547, 0.843) * _Time.x * _AtmoRaysSpeed;
					//get ray power by texture coordinate
					float rayPower = tex2D(_NoiseTex, rayTexCoord1 + rayTexCoord2).a * _AtmoRaysPower;
					//update atmosphere offset by ray power
					ao = saturate(ao * (1 + rayPower));
					//fill the answer
					answer.rgb = tex2D(_AtmoRamp, half2(1 - ao, 1 - ao)).rgb * _AtmoRampColor.rgb;
					answer.a = ao * _AtmoRampColor.a;
				}

				return answer;
			}

			ENDCG
		}
	}
} 