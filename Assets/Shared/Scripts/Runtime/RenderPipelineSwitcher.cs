/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class RenderPipelineSwitcher : MonoBehaviour
{
	[SerializeField,ReadOnlyAtRuntime] Pipeline _pipeline = Pipeline.BuiltIn;
	[SerializeField,ReadOnlyAtRuntime] RenderPipelineAsset _urpAsset = null;
	[SerializeField,ReadOnlyAtRuntime] RenderPipelineAsset _hdrpAsset = null;

	[Header("Material")]
	[SerializeField,ReadOnlyAtRuntime] Material _birpMaterial = null;
	[SerializeField,ReadOnlyAtRuntime] Material _urpMaterial = null;
	[SerializeField,ReadOnlyAtRuntime] Material _hdrpMaterial = null;
	[SerializeField] UnityEvent<Material> _materialEvent = new UnityEvent<Material>(); 

	public enum Pipeline
	{
		BuiltIn,
		Universal,
		HighDefinition
	}


	void OnValidate()
	{
		// At the time of writing (2021.2.14), QualitySettings is overriding GraphicsSettings, so you have to change both, in that order.
		// This will probably change in the future.
		// https://forum.unity.com/threads/bug-change-of-urp-asset-do-not-work.1055405/
		Material material = null;
		switch( _pipeline )
		{
			case Pipeline.BuiltIn:
				QualitySettings.renderPipeline = null;
				GraphicsSettings.renderPipelineAsset = null;
				material = _birpMaterial;
				break;
			case Pipeline.Universal:
				QualitySettings.renderPipeline = _urpAsset;
				GraphicsSettings.renderPipelineAsset = _urpAsset;
				material = _urpMaterial;
				break;
			case Pipeline.HighDefinition:
				QualitySettings.renderPipeline = _hdrpAsset;
				GraphicsSettings.renderPipelineAsset = _hdrpAsset;
				material = _hdrpMaterial;
				break;
		}
		if( material ) _materialEvent.Invoke( material );
	}
}