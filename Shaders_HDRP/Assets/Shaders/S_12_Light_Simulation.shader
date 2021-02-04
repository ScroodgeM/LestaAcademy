Shader "WGA/S_12"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _LightPositionX ("Light Position X", Range(-5, 5)) = 0
        _LightPositionY ("Light Position Y", Range(0, 10)) = 0
        _LightPositionZ ("Light Position Z", Range(0, 10)) = 0
        _LightIntensity ("Light Intensity", Range(0, 10)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend One Zero

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                half3 worldPosition : TEXCOORD1;
                half3 worldNormal : TEXCOORD2;
                float2 uv : TEXCOORD0;
            };

            fixed4 _MainColor;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _LightPositionX;
            float _LightPositionY;
            float _LightPositionZ;
            float _LightIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightPosition = float3(_LightPositionX, _LightPositionY, _LightPositionZ);
                float3 obj2light = lightPosition - i.worldPosition;
                float lightValue = _LightIntensity * dot(obj2light, i.worldNormal) / dot(obj2light, obj2light);
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor * lightValue;
                return col;
            }
            ENDHLSL
        }
    }
}
