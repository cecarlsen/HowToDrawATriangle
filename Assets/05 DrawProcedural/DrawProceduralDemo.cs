/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;

public class DrawProceduralDemo : MonoBehaviour
{
	const int vertCount = 3;

	ComputeBuffer _buffer;
	ComputeShader _computeShader;
	Material _material;

	int _awakeKernel;
	int _updateKernel;

	static class ShaderIDs {
		public static int buffer = Shader.PropertyToID( "_Buffer" );
		public static int deltaTime = Shader.PropertyToID( "_DeltaTime" );
	}


	void Awake()
	{
		_material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );

		_buffer = new ComputeBuffer( vertCount, sizeof(float)*4 );

		_computeShader = Instantiate( (ComputeShader) Resources.Load( GetType().Name ) );
		_awakeKernel = _computeShader.FindKernel( "Awake" );
		_updateKernel = _computeShader.FindKernel( "Update" );

		_computeShader.SetBuffer( _awakeKernel, ShaderIDs.buffer, _buffer );
		_computeShader.SetBuffer( _updateKernel, ShaderIDs.buffer, _buffer );
		_material.SetBuffer( ShaderIDs.buffer, _buffer );

		_computeShader.Dispatch( _awakeKernel, vertCount, 1, 1 );
	}


	void OnDestroy()
	{
		_buffer.Release();
	}


	void Update()
	{
		_computeShader.SetFloat( ShaderIDs.deltaTime, Time.deltaTime );
		_computeShader.Dispatch( _updateKernel, vertCount, 1, 1 );
	}


	void OnRenderObject()
	{
		_material.SetPass( 0 );
		Graphics.DrawProceduralNow( MeshTopology.Triangles, vertCount );
	}
}