Shader "Custom/ShojiBlackOutBehind"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _Transparency ("Transparency", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }

        // 最初のパス：障子の透過部分を描画
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            // 透過部分を描画
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Transparency;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency;  // 透明度を適用
                return texColor;
            }
            ENDCG
        }

        // 2番目のパス：障子の後ろの部分を黒く塗りつぶす
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            // 後ろのオブジェクトを黒で塗りつぶす
            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            // 塗りつぶしの色を黒に設定
            Color (0, 0, 0, 1)
        }

        // 3番目のパス：他のオブジェクトを描画
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Transparency;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency;
                return texColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
