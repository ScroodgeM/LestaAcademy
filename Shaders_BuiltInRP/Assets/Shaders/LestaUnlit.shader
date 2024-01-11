Shader "Lesta/Unlit"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("NormalMap", 2D) = "bump" {}
        _BumpMultiplier ("NormalMap Multiplier", Range(0,1)) = 1
        [NoScaleOffset] _MetallicMap ("Metallic Map", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "LightMode"="ForwardBase"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD2;
                half3 tspace0 : TEXCOORD3;
                half3 tspace1 : TEXCOORD4;
                half3 tspace2 : TEXCOORD5;
                SHADOW_COORDS(6)
            };

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float _BumpMultiplier;
            sampler2D _MetallicMap;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 worldBiTangent = cross(worldNormal, worldTangent) * tangentSign;

                o.tspace0 = half3(worldTangent.x, worldBiTangent.x, worldNormal.x);
                o.tspace1 = half3(worldTangent.y, worldBiTangent.y, worldNormal.y);
                o.tspace2 = half3(worldTangent.z, worldBiTangent.z, worldNormal.z);

                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 bumpMap = tex2D(_BumpMap, i.uv);
                half3 normal = lerp(half3(0, 0, 1), UnpackNormal(bumpMap), _BumpMultiplier);

                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, normal);
                worldNormal.y = dot(i.tspace1, normal);
                worldNormal.z = dot(i.tspace2, normal);

                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                half3 worldRefl = reflect(-worldViewDir, worldNormal);

                half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
                half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);

                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

                fixed metallic = tex2D(_MetallicMap, i.uv).r;
                fixed3 reflection = skyColor * metallic;
                fixed3 unlit = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
                fixed3 ambient = ShadeSH9(half4(worldNormal, 1));
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed3 diffuse = unlit * (nl * _LightColor0 * shadow + ambient);
                fixed4 col = fixed4(reflection + diffuse, 1);
                return col;
            }
            ENDCG
        }

        Pass
        {
            Tags
            {
                "LightMode"="ShadowCaster"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f
            {
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
