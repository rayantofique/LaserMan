using UnityEngine;
using System.Collections;

public class MeshDeformerInput : MonoBehaviour {

	public float radius = 1f;
	public float pull = 10f;
	private Mesh meshA;
	private MeshFilter unappliedMesh;

	AudioSource aud;
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;
	private AudioClip prevAudio;

	public ParticleSystemRenderer particleSys;

	[SerializeField] private bool m_ShowDebugRay; 
	[SerializeField] private Transform m_Camera;
	[SerializeField] private LayerMask m_ExclusionLayers;  
	[SerializeField] private float m_DebugRayLength = 5f;           // Debug ray length.
	[SerializeField] private float m_DebugRayDuration = 1f;         // How long the Debug ray will remain visible.
	[SerializeField] private float m_RayLength = 500f;              // How far into the scene the ray is cast.


	// Use this for initialization
	void Start () {
	
		aud = this.gameObject.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (OVRInput.GetDown (OVRInput.Button.Two)) {
			pull = -pull;
			if (aud.clip != sound2) {
				aud.clip = sound2;
			} else {
				aud.clip = sound1;
			}
		}
		if (OVRInput.Get (OVRInput.Button.One)) {

			/*if (aud.clip == sound3) {
				aud.clip = prevAudio;
			}*/
			if (!aud.isPlaying) {
				aud.Play ();
			}
			particleSys.enabled = true;
		}

		if (OVRInput.GetUp (OVRInput.Button.One)) {


			if (aud.isPlaying) {
			//	prevAudio = aud.clip;
				//aud.clip = sound3;
				//aud.Play();	
				aud.Stop ();

			}
			particleSys.enabled = false;

		}

		if ((!OVRInput.Get(OVRInput.Button.One)))
		{
			ApplyMeshCollider ();
			return;
		}
			


		EyeRaycast ();
	}

	public float LinearFalloff(float distance, float inRadius)
	{
		return Mathf.Clamp01 (1.0f - distance / inRadius);
	}

	void DeformMesh (Mesh mesh, Vector3 position, float power, float inRadius)
	{
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		float sqrRadius = inRadius * inRadius;

		//calculate average normal from vertices surrounding sculpting point
		Vector3 averageNormal = Vector3.zero;
		for (int i = 0; i < vertices.Length; i++) 
		{
			float sqrMag = (vertices [i] - position).sqrMagnitude;
			if (sqrMag > sqrRadius) {
				//skip vertice deformation if too far
				continue;
			}
			float distance = Mathf.Sqrt (sqrMag); 
			float falloff = LinearFalloff(distance, inRadius);  //how much surrounding vertices need to be sculpted 
			averageNormal += falloff * normals[i];
			averageNormal = averageNormal.normalized;

		}

		for (int i = 0; i < vertices.Length; i++) {
			//sculpting individual vertices according to average normal
			float sqrMag = (vertices[i] - position).sqrMagnitude;
			if (sqrMag > sqrRadius) 
			{
				//skip deformation if distance too great
				continue;
			}
			float distance = Mathf.Sqrt (sqrMag);
			float fallOff = LinearFalloff (distance, inRadius);
			vertices [i] += averageNormal * fallOff * power;

		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	public void ApplyMeshCollider()
	{
		if (unappliedMesh && unappliedMesh.GetComponent<MeshCollider>()) {
			unappliedMesh.GetComponent<MeshCollider>().sharedMesh = unappliedMesh.mesh;
		}
		unappliedMesh = null;
	}


	private void EyeRaycast()
	{
		// Show the debug ray if required
		if (m_ShowDebugRay)
		{
			Debug.DrawRay(m_Camera.position, m_Camera.forward * m_DebugRayLength, Color.blue, m_DebugRayDuration);
			print ("true");
		}

		// Create a ray that points forwards from the camera.

		Ray ray = new Ray (m_Camera.position, m_Camera.forward);
		RaycastHit hit;

		// Do the raycast forweards to see if we hit an interactive item
		if (Physics.Raycast (ray, out hit, m_RayLength)) 
		{	

			MeshFilter filter = hit.collider.GetComponent<MeshFilter> ();

			if (filter) 
			{
				if (filter != unappliedMesh) 
				{
					ApplyMeshCollider ();
					unappliedMesh = filter;
				}

				Vector3 relPoint = filter.transform.InverseTransformPoint (hit.point);
				DeformMesh (filter.mesh, relPoint, pull * Time.deltaTime, radius);


			}
		}
			
	}

}
