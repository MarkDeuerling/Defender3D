Shader "PP/Distortion"
{
	Properties
	{
		_NoiseTex ("NoiseTex", 2D) = "white" {}
		_Intensity ("Intensity", float) = 0
		_Threshold("Threshold", Float) = 0.1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }
		
		GrabPass
        {
            "_BackgroundTexture"
        }
        
        Cull Off
		ZWrite Off
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
				float2 uv : TEXCOORD1;
			};

			struct v2f
			{
			    float4 pos : SV_POSITION;
    			float4 grab_uv : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.grab_uv = ComputeGrabScreenPos(o.pos);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _BackgroundTexture;
			sampler2D _NoiseTex;
			float _Intensity;
			float _Threshold;

			fixed4 frag (v2f i) : SV_Target
			{
			    fixed4 d = tex2D(_NoiseTex, i.uv);
			    if (d.z >= _Threshold)
                    d = half4(0,0,0,0);
			    float4 uv = i.grab_uv + (d * _Intensity);
				fixed4 bgColor = tex2Dproj(_BackgroundTexture, uv);
				return bgColor;
			}
			ENDCG
		}
	}
}
