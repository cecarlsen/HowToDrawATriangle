
Shader "Hidden/DrawProceduralNowDemo"
{
	CGINCLUDE
			
	#include "UnityCG.cginc"
	
	struct ToFrag
	{
		float4 vertex : SV_POSITION;
	};
	
	StructuredBuffer<float4> _Buffer;
	
	
	ToFrag Vert( uint id : SV_VertexID )
	{
		ToFrag o;
		o.vertex = UnityObjectToClipPos( _Buffer[id] );
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