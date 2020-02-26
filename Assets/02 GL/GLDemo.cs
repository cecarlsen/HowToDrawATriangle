/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;

public class GLDemo : MonoBehaviour
{
	Vector3[] _vertices;
	Material _material;


	void Awake()
	{
		// Create vertices.
		_vertices = new Vector3[]{
			Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
		};

		// Create material.
		_material = new Material( Shader.Find( "Hidden/" + GetType().Name ) );
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) {
			_vertices[v] = rotation * _vertices[v];
		}
	}


	void OnRenderObject()
	{
		// Draw vertices.
		_material.SetPass( 0 );
		GL.Begin( GL.TRIANGLES );
		GL.Color( Color.yellow );
		for( int v = 0; v < _vertices.Length; v++ ) {
			GL.Vertex( _vertices[v] );
		}
		GL.End();
	}
}