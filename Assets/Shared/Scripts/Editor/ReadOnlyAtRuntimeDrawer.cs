/*
	Copyright © Carl Emil Carlsen 2018
	http://cec.dk

	Brillinant solution by rsodre: https://answers.unity.com/questions/442342/

	Beware that if you have special property drawers for the field they will be overrided by this.
*/

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAtRuntimeAttribute))]
public class ReadOnlyAtRuntimeAttributeDrawer : PropertyDrawer
{
	public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

		
	public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	{
		GUI.enabled = !Application.isPlaying;
		EditorGUI.PropertyField( position, property, label, true );
		GUI.enabled = true;
	}
}