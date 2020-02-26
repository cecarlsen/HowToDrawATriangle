/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;

public class MeshDemo : MonoBehaviour
{
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

		// Create mesh and set content.
		_mesh = new Mesh();
		_mesh.vertices = _vertices;
		_mesh.triangles = new int[]{ 0, 1, 2 };

		// Create material.
		Material material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );

		// Create necessary components and set references.
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer render = gameObject.AddComponent<MeshRenderer>();
		filter.sharedMesh = _mesh;
		render.material = material;
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) {
			_vertices[v] = rotation * _vertices[v];
		}

		// Apply to mesh.
		_mesh.vertices = _vertices;
	}
}