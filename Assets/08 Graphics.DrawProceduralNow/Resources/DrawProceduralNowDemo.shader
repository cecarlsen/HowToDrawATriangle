
Shader "Hidden/DrawProceduralNowDemo"
{
	CGINCLUDE
			
	#include "UnityCG.cginc"
	
	struct ToFrag
	{
		float4 vertex : SV_POSITION;
	};
	
	StructuredBuffer<float4> _Vertices;
	
	
	ToFrag Vert( uint vi : SV_VertexID )
	{
		ToFrag o;
		o.vertex = UnityObjectToClipPos( _Vertices[ vi ] );
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