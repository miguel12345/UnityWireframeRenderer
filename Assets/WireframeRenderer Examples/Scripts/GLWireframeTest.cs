using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLWireframeTest : MonoBehaviour {

	void OnPreRender() {
		GL.wireframe = true;
	}
	void OnPostRender() {
		GL.wireframe = false;
	}
}
