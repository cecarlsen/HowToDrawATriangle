/*
	Copyright © Carl Emil Carlsen 2020-2022
	http://cec.dk
*/

using UnityEngine;

public class MeshDemo : MonoBehaviour
{
	Vector3[] _vertices;
	Mesh _mesh;
	Material _material;


	void Awake()
	{
		// Create mesh.
		_mesh = new Mesh();
		_mesh.name = GetType().Name;

		// Mark it dynamic, to tell Unity that we will update the verticies continously.
		_mesh.MarkDynamic();

		// Set verticies and indices.
		_vertices = new Vector3[]{
			Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
		};
		_mesh.vertices = _vertices;
		_mesh.triangles = new int[]{ 0, 1, 2 };

		// Create material.
		_material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );
	}


	void OnDestroy()
	{
		Destroy( _material );
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) _vertices[v] = rotation * _vertices[v];

		// Apply to mesh.
		_mesh.vertices = _vertices;

		// Draw.
		Graphics.DrawMesh( _mesh, Matrix4x4.identity, _material, gameObject.layer );
	}
}