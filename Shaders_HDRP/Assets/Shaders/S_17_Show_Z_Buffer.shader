Shader "WGA/S_17"
{
    Properties
    {
        _NormalOffset ("Normal Offset", Range(0, 1)) = 0
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
                uint vertexId : SV_VertexID;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _NormalOffset;

            v2f vert (appdata v)
            {
                v2f o;
                fixed3 randomNormal = fixed3(sin(v.vertexId), cos(v.vertexId), cos(v.vertexId * 3 + 1));
                o.vertex = UnityObjectToClipPos(v.vertex + randomNormal * _NormalOffset);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed depth = i.vertex.z;
                fixed4 col = fixed4(depth, depth, depth,1);
                return col;
            }
            ENDHLSL
        }
    }
}
