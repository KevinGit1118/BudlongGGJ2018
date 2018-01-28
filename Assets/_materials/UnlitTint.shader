Shader "MQShader/UnitTintTransparent" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
    _Brightness("Brightness",Float) = 1
	_Color("Tint",Color) = (1,1,1,1)
}

SubShader {
	Tags { "Queue"="Transparent" "RenderType"="Transparent" }
	LOD 100
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off
	ZWrite Off
	
	Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _Brightness;
			float4 _Color;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

            float getLuminance(float4 color){
                return 0.299f*color.r+0.587f*color.g+0.114f*color.b;
            }
			fixed4 frag (v2f i) : SV_Target
			{
                //float lum = getLuminance(_Color);
				//fixed4 col = clamp(clamp(tex2D(_MainTex, i.texcoord) * _Brightness,0,1)/lum * _Color,0,1);
                fixed4 col = (tex2D(_MainTex,i.texcoord) * _Color);
				return col;
			}
		ENDCG
	}
}

}
