using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderDistanceThresholdPrinter : MonoBehaviour
{
	public MeshRenderer MeshRenderer;
	[Range(0.0f,0.4f)]
	public float Threhsold = 0.1f;
	public Text Text;
	
	void Update()
	{
		Text.text = String.Format("Threshold set to {0}", MeshRenderer.sharedMaterial.GetFloat("_Threshold"));
	}

	private void OnValidate()
	{
		MeshRenderer.sharedMaterial.SetFloat("_Threshold",Threhsold);
	}
}
