/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;

public class DrawProceduralNowDemo : MonoBehaviour
{
	Material _material;

	ComputeShader _computeShader;
	int _awakeKernel;
	int _updateKernel;
	
	ComputeBuffer _vertexBuffer;


	static class ShaderIDs
	{
		public static int vertices = Shader.PropertyToID( "_Vertices" );
		public static int deltaTime = Shader.PropertyToID( "_DeltaTime" );
	}


	void Awake()
	{
		const int vertexCount = 3;

		// Create custum material.
		_material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );

		// Create vertex buffer.
		_vertexBuffer = new ComputeBuffer( vertexCount, sizeof(float)*4 );

		// Create comute shader and find kernels.
		_computeShader = Instantiate( Resources.Load<ComputeShader>( GetType().Name ) );
		_awakeKernel = _computeShader.FindKernel( "Awake" );
		_updateKernel = _computeShader.FindKernel( "Update" );

		// Set shader resources.
		_computeShader.SetBuffer( _awakeKernel, ShaderIDs.vertices, _vertexBuffer );
		_computeShader.SetBuffer( _updateKernel, ShaderIDs.vertices, _vertexBuffer );
		_material.SetBuffer( ShaderIDs.vertices, _vertexBuffer );

		// Create initial vertex data.
		_computeShader.Dispatch( _awakeKernel, vertexCount, 1, 1 );
	}


	void OnDestroy()
	{
		_vertexBuffer.Release();
		Destroy( _computeShader );
		Destroy( _material );
	}


	void Update()
	{
		// Update vertices.
		_computeShader.SetFloat( ShaderIDs.deltaTime, Time.deltaTime );
		_computeShader.Dispatch( _updateKernel, _vertexBuffer.count, 1, 1 );
	}


	void OnRenderObject()
	{
		// Draw.
		_material.SetPass( 0 );
		Graphics.DrawProceduralNow( MeshTopology.Triangles, _vertexBuffer.count );
	}
}