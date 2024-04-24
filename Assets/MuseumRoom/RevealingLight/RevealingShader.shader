Shader "Custom/RevealingShader"
{
    // Credit to World of Zero on YouTube for a tutorial
    // https://www.youtube.com/watch?v=b4utgRuIekk
    //
    // Modified slightly by X-Scape team.
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _LightDirection ("Light Direction", Vector) = (0,0,1,0)
        _LightPosition ("Light Position", Vector) = (0,0,0,0)
        _LightAngle ("Light Angle", Range(0,180)) = 45
    }
    SubShader
    {
        Tags { "RenderType"="Tansparent" "RenderPipeline" = "UniversalPipeline"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _LightPosition;
        float4 _LightDirection; 
        float _LightAngle;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 direction = normalize(_LightPosition - IN.worldPos);
            float scale = dot(direction, _LightDirection);
            float strength = scale - cos(_LightAngle);

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb * strength;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
