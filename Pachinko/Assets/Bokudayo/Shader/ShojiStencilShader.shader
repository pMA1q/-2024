Shader "Custom/ShojiStencilShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _Transparency ("Transparency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        
        Pass
        {
            // Stencilの設定: 障子部分にマスクを適用
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            // 透過設定を追加
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            // カスタムシェーダー: 障子の透明度を制御
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // プロパティ
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

            // 頂点シェーダー
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // フラグメントシェーダー
            half4 frag(v2f i) : COLOR
            {
                // テクスチャの色を取得
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency; // 透明度を設定
                return texColor; // 透過色を適用
            }
            ENDCG
        }

        // ステンシルを使って、障子の部分をマスクして描画
        Pass
        {
            // Stencil設定: 1を持つピクセルにのみ描画
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            // 通常の描画処理
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // テクスチャと透明度
            sampler2D _MainTex;
            float _Transparency;

            // 頂点データ
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

            // 頂点シェーダー
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // フラグメントシェーダー
            half4 frag(v2f i) : COLOR
            {
                // 通常の描画処理
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency; // 透明度を適用
                return texColor;
            }
            ENDCG
        }
    }

    // フォールバック
    FallBack "Diffuse"
}
