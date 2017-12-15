Shader "Effects/WaveEffect"
{
	Properties
	{
        _NoiseTex("Texture", 2D) = "white" {}
        _Intensity("Intensity", Float) = 0.1
        _Threshold("Threshold", Float) = 0.1
	}
	SubShader
	{
		// Draw ourselves after all opaque geometry
		Tags{ "Queue" = "Transparent" }

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
        {
            "_BackgroundTexture"
        }

		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Off
		
		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float4 grabPos: TEXCOORD0;
                float2 uv: TEXCOORD1;
            };
    
            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float4 pos : SV_POSITION;
            };
    
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.uv = v.uv;
                return o;
            }
    
            sampler2D _BackgroundTexture;
            sampler2D _NoiseTex;
            float _Intensity;
            float _Threshold;
    
            half4 frag(v2f i) : SV_Target
            {
                half4 d = tex2D(_NoiseTex, i.uv);
                if (d.z >= _Threshold)
                    d = half4(0,0,0,0);
                float4 p = i.grabPos + (d * _Intensity);
                half4 bgcolor = tex2Dproj(_BackgroundTexture, p);
                return bgcolor;
            }
            ENDCG
        }

	}
}