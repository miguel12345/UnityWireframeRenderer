using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateObjectInLoop : MonoBehaviour
{

	public Vector3 RotationVector;
	public float Duration;

	void Start()
	{
		transform.DOLocalRotate(RotationVector, Duration).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
	}
}
