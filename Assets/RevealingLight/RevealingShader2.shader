Shader "Custom/RevealingShader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightDirection ("Light Direction", Vector) = (0,0,1,0)
        _LightPosition ("Light Position", Vector) = (0,0,0,0)
        _LightAngle ("Light Angle", Range(0,120)) = 45
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Enable alpha blending

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightPosition;
            float4 _LightDirection; 
            float _LightAngle;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // Calculate world position
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // calcluate the alpha value
                float3 direction = normalize(_LightPosition - i.worldPos);
                float scale = dot(direction, _LightDirection);
                float strength = scale - cos(_LightAngle * (6.28/360.0));
                strength = min(max(strength * 10, 0), 1);
                float alphaValue = strength;
                // apply the alpha value
                col.a = alphaValue;
                return col;
            }
            ENDCG
        }
    }
}
