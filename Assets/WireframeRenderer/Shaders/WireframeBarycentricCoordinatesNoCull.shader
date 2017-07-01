Shader "Wireframe/NoCull"
{
	Properties
	{
	    _LineColor ("Line color", Color) = (0, 0, 0, 1)
	    _LineSize ("Line size", float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front

			CGPROGRAM
			#include "UnityCG.cginc"
			#include "WireframeBarycentricCoordinatesCore.cginc"
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		
		Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back

            CGPROGRAM
			#include "UnityCG.cginc"
			#include "WireframeBarycentricCoordinatesCore.cginc"
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
	}
}
