// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PerVertexBRDFWaterAnimated"
{
    Properties
    {
        _WaterColor("Water Color", Color) = (0.5, 0.5, 0.7, 1.0)
        _BRDFTex("BRDF Ramp", 2D) = "white" {}
        _RandomVectors("RandomVectors Texture", 2D) = "gray" {}
        _RandomWavesAplitude("Random Waves Amplitude", Range(0, 10)) = 1
        _RandomWavesScale("Random Waves Scale", Range(0, 0.01)) = 0.001
        _RegularWavesAplitude("Regular Waves Amplitude", Range(0, 10)) = 1
        _RegularWavesScale("Regular Waves Scale", Range(0, 0.2)) = 0.01
        _WaveSpeed("Wave Speed", Float) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
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
                    float2 vertex1xz : TEXCOORD0;
                    float2 vertex2xz : TEXCOORD1;
                };

                struct v2f
                {
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                };

                fixed4 _WaterColor;
                sampler2D _BRDFTex;
                sampler2D _RandomVectors;
                float _RandomWavesAplitude;
                float _RandomWavesScale;
                float _RegularWavesAplitude;
                float _RegularWavesScale;
                float _WaveSpeed;

                float4 displace(float4 input)
                {
                    float3 randomDisplacement = (tex2Dlod(_RandomVectors, float4(_Time.y *_WaveSpeed * 0.01 + input.x * _RandomWavesScale, input.z * _RandomWavesScale, 0, 0)).rgb * 2 - 1) * _RandomWavesAplitude;
                    randomDisplacement.y += sin(_Time.y * _WaveSpeed + (input.x + input.z * 0.5) * _RegularWavesScale) * _RegularWavesAplitude;
                    return float4(input.xyz + randomDisplacement, input.w);
                }

                v2f vert(appdata v)
                {
                    float4 vDisplaced = displace(v.vertex);
                    float4 vDisplacedNeighborLeft = displace(float4(v.vertex1xz.x, 0, v.vertex1xz.y, v.vertex.w));
                    float4 vDisplacedNeighborRight = displace(float4(v.vertex2xz.x, 0, v.vertex2xz.y, v.vertex.w));

                    fixed3 normal = normalize(cross(vDisplacedNeighborLeft - vDisplaced, vDisplacedNeighborRight - vDisplaced));

                    float3 worldSpaveViewDir = normalize(WorldSpaceViewDir(vDisplaced));
                    float3 worldSpaveLightDir = normalize(WorldSpaceLightDir(vDisplaced));
                    float3 worldNormal = normalize(UnityObjectToWorldNormal(normal));
                    float NdotL = dot(worldNormal, worldSpaveLightDir);
                    float NdotE = dot(worldNormal, worldSpaveViewDir);
                    v2f o;

                    o.vertex = UnityObjectToClipPos(vDisplaced);
                    fixed4 col = _WaterColor + (tex2Dlod(_BRDFTex, fixed4(NdotL * 0.5 + 0.5, saturate(NdotE), 0, 0)) * 2 - 1);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    UNITY_APPLY_FOG(o.fogCoord, col);
                    o.color = col;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    return i.color;
                }
                ENDCG
            }
        }
}