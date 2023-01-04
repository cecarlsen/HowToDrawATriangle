/*
	Copyright © Carl Emil Carlsen 2020-2023
	http://cec.dk
*/

using UnityEngine;

public class MeshDemo : MonoBehaviour
{
	[SerializeField] Material _material = null;
	public Material material { get { return _material; } set { _material = value; } }


	Vector3[] _vertices;
	Mesh _mesh;


	void Awake()
	{
		// Create vertices.
		_vertices = new Vector3[]{
			Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
		};

		// Create mesh.
		_mesh = new Mesh();
		_mesh.name = GetType().Name;

		// Mark it dynamic, to tell Unity that we will update the verticies continously.
		_mesh.MarkDynamic();

		// Set initial mesh data.
		_mesh.vertices = _vertices;
		_mesh.normals = new Vector3[]{ Vector3.back, Vector3.back , Vector3.back };
		_mesh.triangles = new int[]{ 0, 1, 2 };
	}


	void OnDestroy()
	{
		Destroy( _mesh );
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) _vertices[v] = rotation * _vertices[v];

		// Apply to mesh.
		_mesh.vertices = _vertices;

		// Draw.
		Graphics.DrawMesh( _mesh, Matrix4x4.identity, material, gameObject.layer );
	}
}