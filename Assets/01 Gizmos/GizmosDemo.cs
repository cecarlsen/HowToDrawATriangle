/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/


using UnityEngine;

public class GizmosDemo : MonoBehaviour
{
	Vector3[] _vertices;


	void Awake()
	{
		// Create vertices.
		_vertices = new Vector3[]{
			Quaternion.AngleAxis( 0/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 2/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
			Quaternion.AngleAxis( 1/3f * 360, Vector3.forward ) *  Vector3.up * 0.5f,
		};
	}


	void Update()
	{
		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < _vertices.Length; v++ ) {
			_vertices[v] = rotation * _vertices[v];
		}
	}


	void OnDrawGizmos()
	{
		if( _vertices == null ) return;

		// Draw vertices.
		Gizmos.color = Color.yellow;
		for( int v = 0; v < _vertices.Length; v++ ) {
			Gizmos.DrawLine( _vertices[v], _vertices[(v+1)%_vertices.Length] );
		}
	}
}