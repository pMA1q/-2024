Shader "Custom/TransparentShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1, 1, 1, 1)   // ベースカラー
        _MainTex ("Base Texture", 2D) = "white" { }   // メインテクスチャ
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }

        Pass
        {
            // シェーダーの主要な部分
            CGPROGRAM
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
                float4 pos : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            // パラメータ
            float4 _Color;
            sampler2D _MainTex;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _Color;
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 texColor = tex2D(_MainTex, i.uv);
                return texColor * i.color;  // テクスチャの色にベースカラーを掛け合わせる
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
