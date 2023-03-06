Shader "Example Unlit"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,0,1)
        _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("NormalMap", 2D) = "bump" {}
        _BumpMultiplier ("NormalMap Multiplier", Range(0,1)) = 1
        [NoScaleOffset] _MetallicMap ("Metallic Map", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "LightMode"="ForwardBase"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                half3 tspace0 : TEXCOORD3;
                half3 tspace1 : TEXCOORD4;
                half3 tspace2 : TEXCOORD5;
                SHADOW_COORDS(6)
                UNITY_FOG_COORDS(7)
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
                o.pos = UnityObjectToClipPos(v.pos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.pos);
                half3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 wBiTangent = cross(o.worldNormal, wTangent) * tangentSign;
                o.tspace0 = half3(wTangent.x, wBiTangent.x, o.worldNormal.x);
                o.tspace1 = half3(wTangent.y, wBiTangent.y, o.worldNormal.y);
                o.tspace2 = half3(wTangent.z, wBiTangent.z, o.worldNormal.z);
                TRANSFER_SHADOW(o);
                UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 bumpMap = tex2D(_BumpMap, i.uv);
                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                half3 tnormal = lerp(fixed4(0, 0, 1, 0), UnpackNormal(bumpMap), _BumpMultiplier);
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);

                half3 worldRefl = reflect(-worldViewDir, worldNormal);

                half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
                half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);

                fixed metallic = tex2D(_MetallicMap, i.uv).r;

                fixed4 unlit = tex2D(_MainTex, i.uv) * _Color;

                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

                fixed3 ambient = ShadeSH9(half4(worldNormal, 1));

                fixed shadow = SHADOW_ATTENUATION(i);

                fixed4 result = fixed4(skyColor * metallic + unlit * (nl * _LightColor0 * shadow + ambient), 1);

                UNITY_APPLY_FOG(i.fogCoord, result);

                return result;
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