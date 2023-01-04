/*
	Copyright © Carl Emil Carlsen 2022-2023
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

public class DrawMeshGraphicsBufferDemo : MonoBehaviour
{
	[SerializeField] Material _material = null;
	public Material material { get { return _material; } set { _material = value; } }

	GraphicsBuffer _vertexBuffer;
	GraphicsBuffer _indexBuffer;
	Mesh _mesh;

	Vertex[] _vertices;
	

	struct Vertex
	{
		public Vector3 position;
		public Vector3 normal;
	}


	void Awake()
	{
		int vertexCount = 3;

		// Create vertices.
		_vertices = new Vertex[]{
			new Vertex(){ position = Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f, normal = Vector3.back },
			new Vertex(){ position = Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f, normal = Vector3.back },
			new Vertex(){ position = Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f, normal = Vector3.back },
		};

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

		// Set initial mesh data.
		_indexBuffer.SetData( new int[] { 0, 1, 2 } );
		_vertexBuffer.SetData( _vertices );
	}


	void OnDestroy()
	{
		_vertexBuffer.Release();
		_indexBuffer.Release();
		Destroy( _mesh );
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360 / 8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) {
			Vertex vert = _vertices[ v ];
			vert.position = rotation * vert.position;
			_vertices[ v ] = vert;
		}

		// Apply to mesh.
		_vertexBuffer.SetData( _vertices );

		// Draw.
		Graphics.DrawMesh( _mesh, Matrix4x4.identity, material, gameObject.layer );
	}
}