using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {

	Mesh deformingMesh;
	Vector3[] originalVerts, displacedVerts;
	Vector3[] vertexVelocities;	
	public float decay = -0.1f;
	float uniformScale = 1f;

	// Use this for initialization
	void Start () {

		deformingMesh = GetComponent<MeshFilter> ().mesh;
		originalVerts = deformingMesh.vertices;
		displacedVerts = new Vector3[originalVerts.Length];
		vertexVelocities = new Vector3[originalVerts.Length];
		for (int i = 0; i < originalVerts.Length; i++) {


			displacedVerts [i] = originalVerts [i];

		}

	
	}

	public void AddDeformingForce(Vector3 point, float force)
	{
		for (int i = 0; i < displacedVerts.Length; i++) 
		{
			
			AddForceVertex (i, point, force);
		}
		for (int i = 0; i < displacedVerts.Length; i++) 
		{
			UpdateVertex (i);
		}

		deformingMesh.vertices = displacedVerts;
		deformingMesh.RecalculateNormals ();
	}

	void AddForceVertex(int i, Vector3 point, float force)
	{
		Vector3 pointToVertex = displacedVerts[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;

	}

	void UpdateVertex (int i) {
		Vector3 velocity = vertexVelocities[i];

		displacedVerts[i] += velocity * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
	
		uniformScale = transform.localScale.x;
	}
}
