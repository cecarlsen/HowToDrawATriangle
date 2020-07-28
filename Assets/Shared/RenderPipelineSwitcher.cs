/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

public class RenderPipelineSwitcher : MonoBehaviour
{
	[SerializeField,ReadOnlyAtRuntime] Pipeline _pipeline = Pipeline.BuiltIn;
	[SerializeField,ReadOnlyAtRuntime] RenderPipelineAsset _urpAsset = null;
	[SerializeField,ReadOnlyAtRuntime] RenderPipelineAsset _hdrpAsset = null;

	public enum Pipeline
	{
		BuiltIn,
		Universal,
		HighDefinition
	}


	void OnValidate()
	{
		switch( _pipeline )
		{
			case Pipeline.BuiltIn:
				GraphicsSettings.renderPipelineAsset = null;
				break;
			case Pipeline.Universal:
				GraphicsSettings.renderPipelineAsset = _urpAsset;
				break;
			case Pipeline.HighDefinition:
				GraphicsSettings.renderPipelineAsset = _hdrpAsset;
				break;
		}
	}
}