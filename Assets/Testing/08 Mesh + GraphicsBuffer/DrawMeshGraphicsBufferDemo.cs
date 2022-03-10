/*
	Copyright © Carl Emil Carlsen 2022
	http://cec.dk

	An attempt to use GraphicsBuffer.Target.Raw without a computer shader.
*/

using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DrawMeshGraphicsBufferDemo : MonoBehaviour
{
	GraphicsBuffer _vertexBuffer;
	GraphicsBuffer _indexBuffer;
	Material _material;
	Mesh _mesh;

	Vertex[] _vertices;
	

	struct Vertex
	{
		public Vector3 position;
		//public Vector3 normal;
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
			new VertexAttributeDescriptor( VertexAttribute.Position, VertexAttributeFormat.Float32, 3 )
			//new VertexAttributeDescriptor( VertexAttribute.Normal, VertexAttributeFormat.Float32, 3 )
		);
		_mesh.SetIndexBufferParams( indexCount: vertexCount, IndexFormat.UInt32 );
		_mesh.SetSubMesh( index: 0, new SubMeshDescriptor( indexStart: 0, vertexCount ), MeshUpdateFlags.DontRecalculateBounds );

		// Access Graphic buffers as raw data.
		_mesh.indexBufferTarget |= GraphicsBuffer.Target.Raw;
		_mesh.vertexBufferTarget |= GraphicsBuffer.Target.Raw;

		// Get GraphicBuffers (Mesh will create them internally).
		_indexBuffer = _mesh.GetIndexBuffer();
		_vertexBuffer = _mesh.GetVertexBuffer( index: 0 );


		//NativeArray<Vertex> vertices = new NativeArray<Vertex>( vertexCount, Allocator.Temp );
		//for( int i = 0; i < vertexCount; ++i ) vertices[ i ] = i;
		//_mesh.SetVertexBufferData( indices, dataStart: 0, meshBufferStart: 0, vertexCount, MeshUpdateFlags.DontRecalculateBounds );

		//NativeArray<int> indices = new NativeArray<int>( vertexCount, Allocator.Temp );
		//for( int i = 0; i < vertexCount; ++i ) indices[ i ] = i;
		//_mesh.SetIndexBufferData( indices, dataStart: 0, meshBufferStart: 0, vertexCount, MeshUpdateFlags.DontRecalculateBounds );

		// Set initial data.
		_indexBuffer.SetData( new int[] { 0, 1, 2 } );
		_vertices = new Vertex[]{
			new Vertex(){ position = Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f },
			new Vertex(){ position = Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f },
			new Vertex(){ position = Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f },
		};
		_vertexBuffer.SetData( _vertices );

		// Create material.
		_material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );

		// Add render components.
		var meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.sharedMesh = _mesh;
		var meshrenderer = gameObject.AddComponent<MeshRenderer>();
		meshrenderer.sharedMaterial = _material;
	}


	void OnDestroy()
	{
		_vertexBuffer.Release();
		_indexBuffer.Release();
		Destroy( _mesh );
		Destroy( _material );
	}


	void Update()
	{
		//Quaternion rotation = Quaternion.Euler( 0, 0, 360 / 8f * Time.deltaTime );
		//for( int v = 0; v < _vertices.Length; v++ ) _vertices[ v ].position = rotation * _vertices[ v ].position;
		//_vertexBuffer.SetData( _vertices );

		//Debug.Log( _mesh.HasVertexAttribute( );

		//Graphics.DrawMeshNow( _mesh, Matrix4x4.identity, _material, gameObject.layer );

		//Graphics.DrawMeshNow( _mesh, Matrix4x4.identity, _material, gameObject.layer );
	}
}