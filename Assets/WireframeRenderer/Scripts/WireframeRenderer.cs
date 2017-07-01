using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WireframeRenderer : MonoBehaviour
{
	[SerializeField,HideInInspector]
	private Renderer originalRenderer;
	[SerializeField,HideInInspector]
	private Mesh processedMesh;
	[SerializeField,HideInInspector]
	private MeshRenderer wireframeRenderer;
	
	void Awake()
	{
		Validate();
	}

	private void OnDestroy()
	{
		if (wireframeRenderer != null)
		{
			if (Application.isPlaying)
			{
				Destroy(wireframeRenderer.gameObject);
			}
			else
			{
				DestroyImmediate(wireframeRenderer.gameObject);	
			}
			
			wireframeRenderer = null;
		}
	}

	void Validate()
	{
		if (wireframeRenderer != null)
		{	
			//If the wireframe rendere is not null at this point, it means that everything is already setup
			return;
		}

		var meshFilter = GetComponentInChildren<MeshFilter>();

		if (meshFilter == null)
		{	
			Debug.Log("Wireframe renderer requires a mesh filter in its gameobject or in one of its children");
			enabled = false;
			return;
		}

		originalRenderer = meshFilter.GetComponent<Renderer>();
		processedMesh = GetProcessedMesh(meshFilter.sharedMesh);

		if (processedMesh == null)
		{
			return;
		}

		var wireframeGO = new GameObject("Wireframe renderer");
		wireframeGO.transform.SetParent(originalRenderer.transform);
		wireframeGO.transform.localPosition = Vector3.zero;

		wireframeGO.AddComponent<MeshFilter>().mesh = processedMesh;
		wireframeRenderer = wireframeGO.AddComponent<MeshRenderer>();
		wireframeRenderer.material = CreateWireframeMaterial();
	}

	Material CreateWireframeMaterial()
	{
		var shader = Shader.Find("Custom/Wireframe/BarycentricCoordinates");
		var material = new Material(shader);
		return material;
	}

	private void OnEnable()
	{
		if (wireframeRenderer != null)
		{
			originalRenderer.enabled = false;
			wireframeRenderer.enabled = true;
		}
	}

	private void OnDisable()
	{
		if (wireframeRenderer != null)
		{
			originalRenderer.enabled = true;
			wireframeRenderer.enabled = false;
		}
	}

	Mesh GetProcessedMesh(Mesh mesh)
	{
		var maximumNumberOfVertices = 65534; //Since unity uses a 16-bit indices, not sure if this is still the case. http://answers.unity3d.com/questions/255405/vertex-limit.html
		var meshTriangles = mesh.triangles;
		var meshVertices = mesh.vertices;
		var meshNormals = mesh.normals;
		
		var numberOfVerticesRequiredForTheProcessedMesh = meshTriangles.Length;
		if (numberOfVerticesRequiredForTheProcessedMesh > maximumNumberOfVertices)
		{	
			Debug.LogError("Wireframe renderer can't safely create the processed mesh it needs because the resulting number of vertices would surpass unity vertex limit!");
			return null;
		}

		var processedMesh = new Mesh();
		
		var processedVertices = new Vector3[numberOfVerticesRequiredForTheProcessedMesh];
		var processedUVs = new Vector2[numberOfVerticesRequiredForTheProcessedMesh];
		var processedTriangles = new int[meshTriangles.Length];
		var processedNormals = new Vector3[numberOfVerticesRequiredForTheProcessedMesh];
		
		for (var i = 0; i < meshTriangles.Length; i+=3)
		{
			processedVertices[i] = meshVertices[meshTriangles[i]];
			processedVertices[i+1] = meshVertices[meshTriangles[i+1]];
			processedVertices[i+2] = meshVertices[meshTriangles[i+2]];
			
			processedUVs[i] = new Vector2(0f,0f);
			processedUVs[i+1] = new Vector2(1f,0f);
			processedUVs[i+2] = new Vector2(0f,1f);

			processedTriangles[i] = i;
			processedTriangles[i+1] = i+1;
			processedTriangles[i+2] = i+2;

			processedNormals[i] = meshNormals[meshTriangles[i]];
			processedNormals[i+1] = meshNormals[meshTriangles[i+1]];
			processedNormals[i+2] = meshNormals[meshTriangles[i+2]];
		}

		processedMesh.vertices = processedVertices;
		processedMesh.uv = processedUVs;
		processedMesh.triangles = processedTriangles;
		processedMesh.normals = processedNormals;

		return processedMesh;
	}

}
