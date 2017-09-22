using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UndoState : MonoBehaviour {

	Vector3[] globalVerts;
	int count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*if (OVRInput.GetUp (OVRInput.Button.One) && count <= 0) 
		{
			globalVerts = GetComponent<MeshFilter> ().mesh.vertices;
			count = 1;
		}*/

		if (Input.GetKeyDown ("c")) 
		{
			/*Mesh mesh = GetComponent<MeshFilter> ().mesh;
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();
			count = 0;*/

			SceneManager.LoadScene (0);

		}
	}
}
