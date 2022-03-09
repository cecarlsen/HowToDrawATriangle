/*
	Copyright © Carl Emil Carlsen 2020-2022
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

public class GLDemo : MonoBehaviour
{
	Vector3[] _vertices;
	Material _material;

	const string urpAssetTypeName = "UniversalRenderPipelineAsset"; // So we avoid having to import UnityEngine.Rendering.Universal
	const string hdrpAssetTypeName = "HDRenderPipelineAsset";

	void Awake()
	{
		// Create vertices.
		_vertices = new Vector3[]{
			Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
		};

		// Create material.
		_material = new Material( Shader.Find( "Hidden/" + nameof( GLDemo ) ) );
	}


	void OnEnable()
	{
		if( GraphicsSettings.renderPipelineAsset == null ) Camera.onPostRender += OnPostRenderCamera; 
		else if( GraphicsSettings.renderPipelineAsset.GetType().Name == urpAssetTypeName ) RenderPipelineManager.endCameraRendering += EndCameraRendering;
		else Debug.LogWarning( "RenderPipeline of type " + GraphicsSettings.renderPipelineAsset.GetType().Name + " is not supported.\n" );
	}


	void OnDisable()
	{
		if( GraphicsSettings.renderPipelineAsset == null ) Camera.onPostRender -= OnPostRenderCamera;
		else if( GraphicsSettings.renderPipelineAsset.GetType().Name == urpAssetTypeName ) RenderPipelineManager.endCameraRendering -= EndCameraRendering;
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) {
			_vertices[v] = rotation * _vertices[v];
		}
	}


	void OnPostRenderCamera( Camera cam )
	{
		if( CheckFilter( cam ) ) DrawLines();
	}



	void EndCameraRendering( ScriptableRenderContext src, Camera camera )
	{
		if( CheckFilter( camera ) ) DrawLines();
	}
	

	bool CheckFilter( Camera camera )
	{
		return ( camera.cullingMask & ( 1 << gameObject.layer ) ) != 0;
	}


	void DrawLines()
	{
		// Draw vertices.
		_material.SetPass( 0 );
		GL.Begin( GL.TRIANGLES );
		GL.Color( Color.yellow );
		for( int v = 0; v < _vertices.Length; v++ ) {
			GL.Vertex( _vertices[ v ] );
		}
		GL.End();
	}
}