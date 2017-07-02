using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReadmeInstructionsHelper : MonoBehaviour
{


	public Vector2 Point;
	public Vector2 LineStart;
	public Vector2 LineEnd;

	void Start()
	{
		CaluclatePointToLineDistance();
	}

	public void CaluclatePointToLineDistance()
	{
		Debug.Log(HandleUtility.DistancePointLine(Point, LineStart, LineEnd));
	}

}
