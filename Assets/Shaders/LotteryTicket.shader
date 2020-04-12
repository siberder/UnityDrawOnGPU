Shader "Custom/LotteryTicket"
{
    Properties
    {
        _MainTex ("Back Texture", 2D) = "white" {}
        _BackColor("Back Color", Color) = (1,1,1,1)

        _IconTex ("Icon Texture", 2D) = "white" {}
        _IconColor("Icon Color", Color) = (1,1,1,1)

        _ScratchTex ("Scratch Texture", 2D) = "white" {}
        _Glossiness ("Smoothness (Scratch Texture)", Range(0,1)) = 0.5
        _Metallic ("Metallic (Scratch Texture)", Range(0,1)) = 0.0

        _MaskTex ("Mask Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _ScratchTex;
        sampler2D _MaskTex;
        sampler2D _IconTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_ScratchTex;
            float2 uv_MaskTex;
            float2 uv_IconTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _BackColor;
        fixed4 _IconColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 icon = tex2D(_IconTex, IN.uv_IconTex) * _IconColor;
            fixed4 backColor = tex2D(_MainTex, IN.uv_MainTex) * _BackColor;
            fixed4 c = lerp(backColor, icon, icon.a);
            fixed4 f = tex2D (_ScratchTex, IN.uv_ScratchTex);
            fixed4 m = tex2D (_MaskTex, IN.uv_MaskTex);

            o.Albedo = lerp(c.rgb, f.rgb, m.r);

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic * m.r;
            o.Smoothness = _Glossiness * m.r;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
