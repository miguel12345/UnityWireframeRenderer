Shader "Wireframe/ImplementationGuide/ShaderDerivative"
{
	Properties
	{
		_Threshold ("Threshold", float) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
        
		LOD 100

		Pass
		{
		        Blend SrcAlpha OneMinusSrcAlpha
                ZWrite Off
                
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			float _Threshold;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
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
            
            fixed4 frag (v2f i) : SV_Target
            {   
                fixed col = ddx(i.uv.x) * 60;
                return fixed4(col,col,col,1);
            }
			ENDCG
		}
	}
}
