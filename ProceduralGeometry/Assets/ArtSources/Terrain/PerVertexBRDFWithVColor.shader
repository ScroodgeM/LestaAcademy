// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Per-Vertex BRDF With Vertex Color"
{
	Properties
	{
		_BRDFTex ("BRDF Ramp", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				half3 normal : NORMAL;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _BRDFTex;

			v2f vert (appdata v)
			{
				float3 worldSpaveViewDir = normalize(WorldSpaceViewDir(v.vertex));
				float3 worldSpaveLightDir = normalize(WorldSpaceLightDir(v.vertex));
				float3 worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
				float NdotL = dot(worldNormal, worldSpaveLightDir);
				float NdotE = dot(worldNormal, worldSpaveViewDir);
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				fixed4 col = v.color + (tex2Dlod(_BRDFTex, fixed4(NdotL * 0.5 + 0.5, saturate(NdotE), 0, 0)) * 2 - 1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				UNITY_APPLY_FOG(o.fogCoord, col);
				o.color = col;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}
	}
}