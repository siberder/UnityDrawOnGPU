Shader "Hidden/DrawOnTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BrushTexture ("Brush Texture", 2D) = "white" {}
        _BrushSize("Brush Size", float) = 0.1
        _BrushColor("Brush Color", Color) = (0,0,0,1)
        _BrushPosition("Brush Position", Vector) = (0,0,0,0)
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            //sampler2D _BrushTexture;

            float4 _BrushColor;
            float _BrushSize;
            float4 _BrushPosition;

            half2 when_eq(half2 x, half2 y) {
                return 1.0 - abs(sign(x - y));
            }

            half2 when_neq(half2 x, half2 y) {
                return abs(sign(x - y));
            }

            half2 when_gt(half2 x, half2 y) {
                return max(sign(x - y), 0.0);
            }

            half2 when_lt(half2 x, half2 y) {
                return max(sign(y - x), 0.0);
            }

            half2 when_ge(half2 x, half2 y) {
                return 1.0 - when_lt(x, y);
            }

            half2 when_le(half2 x, half2 y) {
                return 1.0 - when_gt(x, y);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half2 brushHalf = half2(_BrushSize, _BrushSize) * 0.5;
                half2 brushMin = _BrushPosition - brushHalf;
                half2 brushMax = _BrushPosition + brushHalf;
                float canDraw = when_gt(i.uv.x, brushMin.x) * when_lt(i.uv.x, brushMax.x) * when_gt(i.uv.y, brushMin.y) * when_lt(i.uv.y, brushMax.y);
                fixed4 col = lerp(tex2D(_MainTex, i.uv), _BrushColor, canDraw);

                return col;
            }
            ENDCG
        }
    }
}
