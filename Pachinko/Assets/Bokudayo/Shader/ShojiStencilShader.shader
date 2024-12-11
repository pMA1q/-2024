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
            // Stencil�̐ݒ�: ��q�����Ƀ}�X�N��K�p
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            // ���ߐݒ��ǉ�
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            // �J�X�^���V�F�[�_�[: ��q�̓����x�𐧌�
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // �v���p�e�B
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

            // ���_�V�F�[�_�[
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // �t���O�����g�V�F�[�_�[
            half4 frag(v2f i) : COLOR
            {
                // �e�N�X�`���̐F���擾
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency; // �����x��ݒ�
                return texColor; // ���ߐF��K�p
            }
            ENDCG
        }

        // �X�e���V�����g���āA��q�̕������}�X�N���ĕ`��
        Pass
        {
            // Stencil�ݒ�: 1�����s�N�Z���ɂ̂ݕ`��
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
            }

            // �ʏ�̕`�揈��
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // �e�N�X�`���Ɠ����x
            sampler2D _MainTex;
            float _Transparency;

            // ���_�f�[�^
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

            // ���_�V�F�[�_�[
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // �t���O�����g�V�F�[�_�[
            half4 frag(v2f i) : COLOR
            {
                // �ʏ�̕`�揈��
                half4 texColor = tex2D(_MainTex, i.uv);
                texColor.a *= _Transparency; // �����x��K�p
                return texColor;
            }
            ENDCG
        }
    }

    // �t�H�[���o�b�N
    FallBack "Diffuse"
}
