using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateTriangle : MonoBehaviour {

	[MenuItem("Tools/Create Triangle")]
	static void CreateTriangleObject()
	{
		var go = new GameObject("Triangle");
		var meshFilter = go.AddComponent<MeshFilter>();
		meshFilter.mesh = CreateTriangleMesh();
		go.AddComponent<MeshRenderer>();
	}

	static Mesh CreateTriangleMesh()
	{
		var mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-0.5f,-0.5f),
			new Vector3(0.5f,-0.5f),
			new Vector3(-0.5f,0.5f) 
		};
		mesh.triangles = new int[]
		{
			0,1,2
		};
		mesh.uv = new Vector2[]
		{
			new Vector2(0f,0f),
			new Vector2(1f,0f),
			new Vector2(0f,1f), 
		};
		return mesh;
	}
}
