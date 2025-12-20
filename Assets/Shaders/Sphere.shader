Shader "Custom/SphereCameraFisheye"
{
    Properties
    {
        _MainTex ("Camera Feed", 2D) = "black" {}
        _Distortion ("Fisheye Strength", Range(-0.5,0)) = -0.5 //-0.5< goes crazy ... positive does nothing, then breaks 
        _Vingete ("Vingette Strength", Range(0,0.2)) = 0.05
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Distortion;
            float _Vingete;
            float4 _BgColor;
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = o.pos; // pass clip space to fragment
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Convert clip space to UV 0..1
                float2 uv = i.screenPos.xy / i.screenPos.w;
                uv = uv * 0.5 + 0.5;

                // Center UV at 0,0
                float2 cUV = uv * 2.0 - 1.0;

                // Correct for screen aspect
                float aspect = _ScreenParams.x / _ScreenParams.y;
                float2 cUV_aspect = float2(cUV.x * aspect, cUV.y);

                // Polar coordinates
                float r = length(cUV_aspect);
                float theta = atan2(cUV_aspect.y, cUV_aspect.x);

                // Apply distortion <--- it works very shitty 
                if(r > 0)
                {
                    float rDist = r * (1.0 + _Distortion * (1.0 - r * r));
                    cUV_aspect.x = rDist * cos(theta);
                    cUV_aspect.y = rDist * sin(theta);
                }
                
                
                // Convert back to normalized coordinates
                cUV.x = cUV_aspect.x / aspect;
                cUV.y = cUV_aspect.y;
                uv = cUV * 0.5 + 0.5;
                uv = clamp(uv, 0.0, 1.0);

                // Sample texture
                fixed4 col = tex2D(_MainTex, uv);

                col.rgb -= 1.0 - smoothstep(_Vingete, 0.8, 1-r); 
                col.rgb = (_BgColor.rgb + col.rgb/2);
                return fixed4(col.rgb, col.a);

                return col;
            }

            ENDCG
        }
    }
}
