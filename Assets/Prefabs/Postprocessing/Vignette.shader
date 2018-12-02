Shader "Vignette/VignetteAndGray"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            
            uniform float intensity;

            fixed4 frag (v2f i) : SV_Target
            {                    
                float3 color = tex2D(_MainTex, i.uv);
                float gray = 0.3f * color.r + 0.6f * color.g + 0.1f * color.b;
                float3 grayColor = float3(gray, gray, gray);
                float3 mixedColor = lerp(color, grayColor, intensity);
                
                float distanceFromCenter = min(length(i.uv-float2(0.5f, 0.5f)), 1.0f);
                float3 vignetterized = lerp(mixedColor, float3(0,0,0), distanceFromCenter*intensity);         
                if (color.r == 1.0f && color.g == 0.0f && color.b == 0.0f){
                    return float4(color, 1);
                }
                return float4(vignetterized, 1);
            }
            ENDCG
        }
    }
}
