Shader "Demo Screen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WarningLevel ("Warning Level", Range(0,1)) = 0
        _WarningColor ("Warning Color", Color) = (1,0,0,0)
        _ScannerDistanceMin ("Scanner Distance Min", Range(0,300)) = 50
        _ScannerDistanceMax ("Scanner Distance Max", Range(0,300)) = 200
        _ScannerColor ("Scanner Color", Color) = (0,1,0,0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            fixed _WarningLevel;
            fixed4 _WarningColor;
            float _ScannerDistanceMin;
            float _ScannerDistanceMax;
            fixed4 _ScannerColor;

            fixed4 frag(v2f i) : SV_Target
            {
                half depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
                depth = LinearEyeDepth(depth);

                fixed4 col = tex2D(_MainTex, i.uv);

                float dist = lerp(_ScannerDistanceMin, _ScannerDistanceMax, sin(_Time.w * 0.15) * 0.5 + 0.5);
                col += saturate(1.0 - 0.5 * abs(depth - dist)) * _ScannerColor;

                col += (_WarningLevel * length(i.uv - 0.5) * abs(_SinTime.w)) * _WarningColor;

                return col;
            }
            ENDCG
        }
    }
}