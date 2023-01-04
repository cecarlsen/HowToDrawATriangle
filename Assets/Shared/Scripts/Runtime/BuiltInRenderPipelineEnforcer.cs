/*
	Copyright © Carl Emil Carlsen 2023
	http://cec.dk
*/

using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class BuiltInRenderPipelineEnforcer : MonoBehaviour
{

	void Awake()
	{
		// At the time of writing (2021.2.14), QualitySettings is overriding GraphicsSettings, so you have to change both, in that order.
		// This will probably change in the future.
		// https://forum.unity.com/threads/bug-change-of-urp-asset-do-not-work.1055405/
		if( QualitySettings.renderPipeline || GraphicsSettings.renderPipelineAsset ) {
			QualitySettings.renderPipeline = null;
			GraphicsSettings.renderPipelineAsset = null;
		}
	}
}