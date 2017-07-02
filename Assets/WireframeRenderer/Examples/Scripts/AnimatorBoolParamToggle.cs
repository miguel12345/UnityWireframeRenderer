using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoolParamToggle : MonoBehaviour
{

	public string ParamName;
	public float TimeBetweenToggles;
	
	private IEnumerator Start()
	{
		Animator animator = GetComponent<Animator>();
		WaitForSeconds wait = new WaitForSeconds(TimeBetweenToggles);

		while (true)
		{
			yield return wait;
			animator.SetBool(ParamName, !animator.GetBool(ParamName));
		}
	}
}
