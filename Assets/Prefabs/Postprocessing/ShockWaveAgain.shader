// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "ShockWave/WaveDistort"
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
            
            uniform float4 _HitPointPositions[10];
            uniform float4 _HitPointData[10];
            
            uniform float4x4 _WorldToProjection; 
            

            fixed4 frag (v2f i) : SV_Target
            {
                
                float2 aspect = _ScreenParams.xy / _ScreenParams.y;
                float totalStrength = 0;
                float2 displacement = float2(0, 0);
                for (int h = 0; h < 10; h++) {
                    float2 origin = _HitPointPositions[h].xy;
                    float age = _HitPointPositions[h].z;
                    float strength = _HitPointPositions[h].w;
                    float randomness =  _HitPointData[h].x;
                    float width = _HitPointData[h].y;
                    float radius = _HitPointData[h].z;
                    float speed = _HitPointData[h].w;
                
                    float2 direction = (i.uv - origin);
                    float2 correctedDirection = direction * aspect;
                    
                    float distance = length(correctedDirection);
                    float2 normalizedDirection = correctedDirection / distance;
                    
                    float donutRadius = speed * age;
                    
                    donutRadius *= 1 + randomness * 0.1 * sin(normalizedDirection.x * 13) * cos(normalizedDirection.y * 43);
                    
                    float progress = min(1, donutRadius / radius);
                    
                    float intensity = (1 - progress) / 40 * strength;
                    
                    float waveWidth = 0.001 + sqrt(donutRadius) / 3 * width;                    
                    
                    
                    float x = 1 - (abs(distance - donutRadius) / waveWidth);
                    x = max(0, x);
                    x = smoothstep(0, 1, x);
                    
                    x *= intensity;
                    totalStrength += x;
                    displacement -= normalize(direction) * x;
                }
                float2 uvPos = i.uv + displacement;
                
                totalStrength = (max(0.01, totalStrength) - 0.01) * 3;
                   
                return /*float4(totalStrength, totalStrength, totalStrength, 0) +*/ tex2D(_MainTex, uvPos);
            }
            ENDCG
        }
    }
}
