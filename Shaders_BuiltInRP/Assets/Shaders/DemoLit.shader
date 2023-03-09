Shader "Demo Lit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
        _MetallicMap ("Metallic", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _Cube ("Cubemap", CUBE) = "" {}
        _Amount ("Extrusion Amount", Range(0,10)) = 0.5
        _ShieldColor ("Shield Color", Color) = (0.5,0.5,0.5,0.5)
        _ShieldTexture ("Shield", 2D) = "white" {}
        _ShieldScale ("Shield Scale", Range(0.01,1)) = 0.5
        _ShieldSpeed ("Shield Speed", Range(0.1,10)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MetallicMap;
        samplerCUBE _Cube;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_MetallicMap;
            float3 viewDir;
            float3 worldRefl;
            INTERNAL_DATA
        };

        float _Amount;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _RimColor;
        float _RimPower;

        void vert(inout appdata_full v)
        {
            //v.vertex.xyz += v.normal * _Amount;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            float metallic = tex2D(_MetallicMap, IN.uv_MetallicMap).r;
            o.Metallic = _Metallic * metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow(rim, _RimPower);
            o.Emission += texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * metallic;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 localPos : TEXCOORD0;
            };

            sampler2D _ShieldTexture;
            float _Amount;
            fixed4 _ShieldColor;
            float _ShieldScale;
            float _ShieldSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos + v.normal * _Amount);
                o.localPos = v.pos * _ShieldScale - _Time.zzzz * _ShieldSpeed;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_ShieldTexture, i.localPos.yy) * _ShieldColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}