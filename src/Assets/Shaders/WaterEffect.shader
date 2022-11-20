Shader "Kijam/WaterEffect"
{
    Properties
    {
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_TexEffect("Effect Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_MoveSpeedU("U Move Speed", Range(-6,6)) = 0.5
		_MoveSpeedV("V Move Speed", Range(-6,6)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };

            fixed4 _Color;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

			sampler2D _MainTex;
			sampler2D _TexEffect;
			fixed _MoveSpeedU;
			fixed _MoveSpeedV;

            fixed4 frag(v2f IN) : SV_Target
            {
				fixed2 MoveScrolledUV = IN.texcoord;

				fixed MoveU = _MoveSpeedU * _Time.z;
				fixed MoveV = _MoveSpeedV * _Time.z;

				MoveScrolledUV += fixed2(MoveU, MoveV);

                fixed4 c = tex2D(_TexEffect, MoveScrolledUV) * IN.color;
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}