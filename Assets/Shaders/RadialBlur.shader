Shader "PP/RadialBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Blur ("Blur", float) = 0
		_Bright ("Bright", float) = 0
		_Origin ("Origin", Vector) = (0,0,0,0)
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
			Vector _Origin;
			float _Blur;
			float _Bright;

			fixed4 frag (v2f i) : SV_Target
			{
			    float4 sumColor = float4(0,0,0,0);
			    i.uv -= _Origin;
			    for (int n = 0; n < 12; ++n)
			    {
			        float scale = 1 - _Blur * (float(n) / 11.0);
			        sumColor += tex2D(_MainTex, i.uv * scale + _Origin);
			    } 
				fixed4 col = sumColor / 12.0 * _Bright;
				return col;
			}
			ENDCG
		}
	}
}
