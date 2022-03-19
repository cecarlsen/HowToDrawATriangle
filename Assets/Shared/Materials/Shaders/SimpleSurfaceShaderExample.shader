Shader "Examples/SimpleSurfaceShaderExample"
{
	Properties
	{
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};


		void surf( Input IN, inout SurfaceOutputStandard o )
		{
			half4 c = half4( 1, 1, 0, 1 ); // Yellow
			o.Albedo = c.rgb;
			o.Metallic = 0.5;
			o.Smoothness = 0.5;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}