Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 pos : POSITION;
            };

            float _OutlineWidth;
            float _OutlineColor[4];

            v2f vert(appdata_t v)
            {
                v2f o;
                float3 offset = normalize(v.normal) * _OutlineWidth;
                o.pos = UnityObjectToClipPos(v.vertex + float4(offset, 0.0));
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return half4(_OutlineColor[0], _OutlineColor[0], _OutlineColor[0], _OutlineColor[0]);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
