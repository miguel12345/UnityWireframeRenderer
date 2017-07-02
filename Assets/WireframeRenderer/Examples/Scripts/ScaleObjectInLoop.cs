using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleObjectInLoop : MonoBehaviour
{

	public float EndValue;
	public float Duration;

	void Start()
	{
		transform.DOScale(EndValue, Duration).SetLoops(-1,LoopType.Yoyo);
	}

}
