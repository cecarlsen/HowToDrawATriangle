Shader "Hidden/MeshDemo"
{
	CGINCLUDE
			
	#include "UnityCG.cginc"

	struct ToVert
	{
		float4 vertex : POSITION;
	};
	
	struct ToFrag
	{
		float4 vertex : SV_POSITION;
	};
	
	
	ToFrag Vert( ToVert i )
	{
		ToFrag o;
		o.vertex = UnityObjectToClipPos( i.vertex );
		return o;
	}
	
	fixed4 Frag( ToFrag i ) : SV_Target
	{
		return fixed4( 1, 1, 0, 1 ); // Yellow
	}
	
	ENDCG
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			ENDCG
		}
	}
}
