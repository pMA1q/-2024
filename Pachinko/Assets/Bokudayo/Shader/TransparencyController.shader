Shader "Custom/TransparentShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1, 1, 1, 1)   // �x�[�X�J���[
        _MainTex ("Base Texture", 2D) = "white" { }   // ���C���e�N�X�`��
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }

        Pass
        {
            // �V�F�[�_�[�̎�v�ȕ���
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

            // �p�����[�^
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
                return texColor * i.color;  // �e�N�X�`���̐F�Ƀx�[�X�J���[���|�����킹��
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
