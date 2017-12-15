Shader "PP/Transition"
{
	Properties
	{
	     _Cutoff ("Cutoff", float) = 0
	     _Color ("Color", Color) = (0, 0, 0, 1)
	     _Distortion ("Distortion", float) = 0
	     [MaterialToggle]_Normalize ("Normalize", float) = 0
	     [MaterialToggle]_UseBlend ("UseBlend", float) = 0
	     [MaterialToggle]_UseColor ("UseColor", float) = 0
		 _MainTex ("Texture", 2D) = "white" {}
		 _BlendTex ("BlendTex", 2D) = "white" {}
		 _TransitionTex ("TransitionTex", 2D) = "white" {}
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
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv1 = v.uv1;
				o.uv2 = v.uv2;
    		    o.uv1.x = 1 - v.uv1.x;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _TransitionTex;
			sampler2D _BlendTex;
			float _Normalize;
			float _UseBlend;
			float _UseColor;
			float _Cutoff;
			fixed4 _Color;
			float _Distortion;

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 trans = tex2D(_TransitionTex, i.uv1);
                float2 dir = (trans.gb - 0.5) * 2;
                if (_Normalize) dir = normalize(dir);
                if (_UseColor)
                    if (trans.b <= _Cutoff) return _Color;
                if (_UseBlend)
                    if (trans.b <= _Cutoff) return tex2D(_BlendTex, i.uv2);
				return tex2D(_MainTex, i.uv + dir * _Cutoff * _Distortion);
			}
			ENDCG
		}
	}
}
