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

        // �ŏ��̃p�X�F��q�̓��ߕ�����`��
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            // ���ߕ�����`��
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
                texColor.a *= _Transparency;  // �����x��K�p
                return texColor;
            }
            ENDCG
        }

        // 2�Ԗڂ̃p�X�F��q�̌��̕����������h��Ԃ�
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            // ���̃I�u�W�F�N�g�����œh��Ԃ�
            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            // �h��Ԃ��̐F�����ɐݒ�
            Color (0, 0, 0, 1)
        }

        // 3�Ԗڂ̃p�X�F���̃I�u�W�F�N�g��`��
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
