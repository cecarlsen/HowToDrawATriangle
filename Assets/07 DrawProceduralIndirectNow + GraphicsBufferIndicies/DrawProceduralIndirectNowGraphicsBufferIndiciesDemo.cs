/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;

public class DrawProceduralIndirectNowGraphicsBufferIndiciesDemo : MonoBehaviour
{
	const int vertCount = 3;

	ComputeBuffer _buffer;
	ComputeBuffer _drawArgsBuffer;
	GraphicsBuffer _indexBuffer;
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
		_drawArgsBuffer = new ComputeBuffer( 4, sizeof( int ), ComputeBufferType.IndirectArguments );  // vertex count per instance, instance count, start vertex location, and start instance location
		_drawArgsBuffer.SetData( new int[] { vertCount, 1, 0, 0 } );
		_indexBuffer = new GraphicsBuffer( GraphicsBuffer.Target.Index, 3, sizeof( short ) );
		_indexBuffer.SetData( new short[] { 0, 1, 2 } );

		_computeShader = Instantiate( Resources.Load<ComputeShader>( GetType().Name ) );
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
		_drawArgsBuffer.Release();
	}


	void Update()
	{
		_computeShader.SetFloat( ShaderIDs.deltaTime, Time.deltaTime );
		_computeShader.Dispatch( _updateKernel, vertCount, 1, 1 );
	}


	void OnRenderObject()
	{
		_material.SetPass( 0 );
		Graphics.DrawProceduralIndirectNow( MeshTopology.Triangles, _indexBuffer, _drawArgsBuffer );
	}
}