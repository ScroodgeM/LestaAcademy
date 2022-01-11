Shader "Silhoulette"
{
    Properties
    {
        _SilhouletteColor1 ("Silhoulette Color 1", Color) = (1,1,1,1)
        _SilhouletteColor2 ("Silhoulette Color 2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            ZTest Always
            ZWrite Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                half3 worldNormal : TEXCOORD1;
            };

            fixed4 _SilhouletteColor1;
            fixed4 _SilhouletteColor2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));

                float h = dot(i.worldNormal, worldViewDir);

                return lerp(_SilhouletteColor1, _SilhouletteColor2, h);
            }

            ENDCG
        }
    }
}
