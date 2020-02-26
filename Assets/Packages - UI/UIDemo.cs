/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDemo : MaskableGraphic
{
	List<UIVertex> _vertices;


	protected override void Awake()
	{
		// Create vertices.
		_vertices = new List<UIVertex>( 3 );
		UIVertex vertex = new UIVertex();
		vertex.color = Color.yellow;
		for( int v = 0; v < 3; v++ ) {
			vertex.position = Quaternion.AngleAxis( v/3f * 360, Vector3.forward ) *  Vector3.up * 100;
			_vertices.Add( vertex );
		}
	}


	void Update()
	{
		if( _vertices == null ) return;

		// Update vertices.
		Quaternion rotation = Quaternion.Euler( 0, 0, 360/8f * Time.deltaTime );
		for( int v = 0; v < 3; v++ ) {
			UIVertex vertex = _vertices[v];
			vertex.position = rotation * vertex.position;
			_vertices[v] = vertex;
		}

		// Request OnPopulateMesh call.
		SetVerticesDirty();
	}


	protected override void OnPopulateMesh( VertexHelper vh )
	{
		// Apply vertices.
		vh.Clear();
		vh.AddUIVertexTriangleStream( _vertices );
	}
}