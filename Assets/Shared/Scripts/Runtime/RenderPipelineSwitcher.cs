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
		// At the time of writing (2021.2.14), QualitySettings is overriding GraphicsSettings, so yuo have to change both, in that order.
		// This will probably change in the future.
		// https://forum.unity.com/threads/bug-change-of-urp-asset-do-not-work.1055405/
		switch( _pipeline )
		{
			case Pipeline.BuiltIn:
				QualitySettings.renderPipeline = null;
				GraphicsSettings.renderPipelineAsset = null;
				break;
			case Pipeline.Universal:
				QualitySettings.renderPipeline = _urpAsset;
				GraphicsSettings.renderPipelineAsset = _urpAsset;
				break;
			case Pipeline.HighDefinition:
				QualitySettings.renderPipeline = _hdrpAsset;
				GraphicsSettings.renderPipelineAsset = _hdrpAsset;
				break;
		}
	}
}