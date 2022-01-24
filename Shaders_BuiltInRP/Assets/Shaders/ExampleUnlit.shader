// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "ExampleUnlit"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _BumpMultiplier ("Normal Map Multiplier", Range(0,1)) = 1
        _MetallicMap ("Mettalic Map", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
                fixed3 diff : COLOR0; // diffuse lighting color
                fixed3 ambient : COLOR1; // diffuse lighting color
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                SHADOW_COORDS(8)
                float3 worldPos : TEXCOORD2;
                half3 worldNormal : TEXCOORD3;
                half3 worldRefl : TEXCOORD4;
                half3 tspace0 : TEXCOORD5; // tangent.x, bitangent.x, normal.x
                half3 tspace1 : TEXCOORD6; // tangent.y, bitangent.y, normal.y
                half3 tspace2 : TEXCOORD7; // tangent.z, bitangent.z, normal.z
            };

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _MainTex_ST;
            float _BumpMultiplier;
            sampler2D _MetallicMap;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.pos).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldViewDir = normalize(UnityWorldSpaceViewDir(o.worldPos));
                o.worldRefl = reflect(-worldViewDir, o.worldNormal);
                half3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(o.worldNormal, wTangent) * tangentSign;
                o.tspace0 = half3(wTangent.x, wBitangent.x, o.worldNormal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, o.worldNormal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, o.worldNormal.z);
                half nl = max(0, dot(o.worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0;
                o.ambient = ShadeSH9(half4(o.worldNormal,1)); // ambient
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 bumpMap = lerp(0.5, tex2D(_BumpMap, i.uv), _BumpMultiplier);
                half3 tnormal = UnpackNormal(bumpMap);
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);

                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                //half3 worldRefl = reflect(-worldViewDir, i.worldNormal);
                half3 worldRefl = reflect(-worldViewDir, worldNormal);

                //half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, i.worldRefl);
                half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
                half3 skyColor = DecodeHDR (skyData, unity_SpecCube0_HDR);

                //c.rgb = i.worldNormal*0.5+0.5;
                //return c;

                fixed3 baseColor = tex2D(_MainTex, i.uv).rgb;
                fixed metallic = tex2D(_MetallicMap, i.uv).r;

                fixed4 c = 0;
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed3 lighting = i.diff * shadow + i.ambient;
                c.rgb = skyColor * metallic + baseColor * lighting;

                UNITY_APPLY_FOG(i.fogCoord, c);
                return c;
            }
            ENDCG
        }

        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
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
