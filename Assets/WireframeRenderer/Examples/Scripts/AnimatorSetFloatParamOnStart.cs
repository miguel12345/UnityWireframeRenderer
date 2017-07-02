using System;
using UnityEngine;

public class AnimatorSetFloatParamOnStart : MonoBehaviour
{
	public String AnimatorParamName;
	public float AnimatorParamValue;
	
	// Use this for initialization
	void Start ()
	{
		Animator animator = GetComponent<Animator>();
		animator.SetFloat(AnimatorParamName,AnimatorParamValue);
	}
}
