Shader "Custom/Checkerboard"
{
    Properties
    {
        _ColorA ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (0.4,0.2,0.1,1)
        _TileSize ("Tile Size", Float) = 10
        _Opacity ("Opacity", Range(0,1)) = 1
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue"="Transparent" 
        }

        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
                float3 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            fixed4 _ColorA;
            fixed4 _ColorB;
            float _TileSize;
            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Checkerboard pattern in xz plane
                float2 pos = i.worldPos.xz;
                float checker = fmod(floor(pos.x * _TileSize) + floor(pos.y * _TileSize), 2);

                fixed4 color = lerp(_ColorA, _ColorB, checker);
                color.a *= _Opacity;

                return color;
            }
            ENDCG
        }
    }
}
