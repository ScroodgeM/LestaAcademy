Shader "DiggerTrain/ColoredUnlit"
{

	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorAdd ("Color Add", Color) = (0.5,0.5,0.5,0.5)
		_ColorMul ("Color Mul", Color) = (0.5,0.5,0.5,0.5)
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
			#pragma target 2.0
			#include "UnityCG.cginc"

			float4 _MainTex_ST;

			struct appdata
			{
				float3 pos : POSITION;
				float3 uv0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv0 : TEXCOORD0;
				float4 pos : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert(appdata IN)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.uv0 = IN.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;

				o.pos = UnityObjectToClipPos(IN.pos);
				return o;
			}

			sampler2D _MainTex;
			fixed4 _ColorAdd;
			fixed4 _ColorMul;
			
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 col;
				col.rgb = tex2D(_MainTex, IN.uv0.xy).rgb * (_ColorMul.rgb * 2) + (_ColorAdd.rgb * 2 - 1);
				col.a = 1;
				return col;
			}

			ENDCG
		}
	}
}
