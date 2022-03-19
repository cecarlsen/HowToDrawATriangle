/*
	Copyright © Carl Emil Carlsen 2022
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

public class DrawMeshGraphicsBufferComputeDemo : MonoBehaviour
{
	public Material material;

	Mesh _mesh;
	GraphicsBuffer _vertexBuffer;
	GraphicsBuffer _indexBuffer;

	ComputeShader _computeShader;
	int _awakeKernel;
	int _updateKernel;


	static class ShaderIDs
	{
		public static int indices = Shader.PropertyToID( "_Indices" );
		public static int vertices = Shader.PropertyToID( "_Vertices" );
		public static int deltaTime = Shader.PropertyToID( "_DeltaTime" );
	}


	void Awake()
	{
		int vertexCount = 3;

		// Create mesh.
		_mesh = new Mesh();
		_mesh.name = GetType().Name;
		_mesh.bounds = new Bounds( Vector3.zero, Vector3.one * 1000 );
		_mesh.SetVertexBufferParams(
			vertexCount: 3,
			new VertexAttributeDescriptor( VertexAttribute.Position, VertexAttributeFormat.Float32, 3 ),
			new VertexAttributeDescriptor( VertexAttribute.Normal, VertexAttributeFormat.Float32, 3 )
		);
		_mesh.SetIndexBufferParams( indexCount: vertexCount, IndexFormat.UInt32 );
		_mesh.SetSubMesh( index: 0, new SubMeshDescriptor( indexStart: 0, vertexCount ), MeshUpdateFlags.DontRecalculateBounds );

		// Access Graphic buffers as raw data.
		_mesh.indexBufferTarget |= GraphicsBuffer.Target.Raw;
		_mesh.vertexBufferTarget |= GraphicsBuffer.Target.Raw;

		// Get GraphicBuffers (Mesh will create them internally).
		_indexBuffer = _mesh.GetIndexBuffer();
		_vertexBuffer = _mesh.GetVertexBuffer( index: 0 );

		// Create compute shader.
		_computeShader = Instantiate( Resources.Load<ComputeShader>( GetType().Name ) );
		_awakeKernel = _computeShader.FindKernel( "Awake" );
		_updateKernel = _computeShader.FindKernel( "Update" );

		// Set resources.
		_computeShader.SetBuffer( _awakeKernel, ShaderIDs.indices, _indexBuffer );
		_computeShader.SetBuffer( _awakeKernel, ShaderIDs.vertices, _vertexBuffer );
		_computeShader.SetBuffer( _updateKernel, ShaderIDs.vertices, _vertexBuffer );

		// Create initial mesh data.
		_computeShader.Dispatch( _awakeKernel, vertexCount, 1, 1 );
	}


	void OnDestroy()
	{
		_vertexBuffer.Release();
		_indexBuffer.Release();
		Destroy( _mesh );
		Destroy( _computeShader );
	}


	void Update()
	{
		// Update vertices.
		_computeShader.SetFloat( ShaderIDs.deltaTime, Time.deltaTime );
		_computeShader.Dispatch( _updateKernel, _vertexBuffer.count, 1, 1 );

		// Draw.
		Graphics.DrawMesh( _mesh, Matrix4x4.identity, material, gameObject.layer );
	}
}