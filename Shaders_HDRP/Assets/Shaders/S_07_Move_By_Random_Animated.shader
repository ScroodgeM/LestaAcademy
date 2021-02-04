Shader "WGA/S_07"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _NormalOffset ("Normal Offset", Range(0, 1)) = 0
        _AnimationSpeed ("Animation Speed", Range(0, 5)) = 0
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
                float2 uv : TEXCOORD0;
                uint vertexId : SV_VertexID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _MainColor;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _NormalOffset;
            float _AnimationSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                fixed3 randomNormal = fixed3(sin(v.vertexId), cos(v.vertexId), cos(v.vertexId * 3 + 1));
                fixed normalOffset = _NormalOffset * sin(_Time.z * _AnimationSpeed);
                o.vertex = UnityObjectToClipPos(v.vertex + randomNormal * normalOffset);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
                return col;
            }
            ENDHLSL
        }
    }
}
