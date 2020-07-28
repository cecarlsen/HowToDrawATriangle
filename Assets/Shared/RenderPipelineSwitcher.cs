/*
	Copyright © Carl Emil Carlsen 2020
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

public class RenderPipelineSwitcher : MonoBehaviour
{
	[SerializeField] Pipeline _pipeline = Pipeline.BuiltIn;
	[SerializeField] RenderPipelineAsset _urpAsset = null;
	[SerializeField] RenderPipelineAsset _hdrpAsset = null;

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