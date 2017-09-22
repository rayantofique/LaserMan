using UnityEngine;
using System.Collections;

public class LaserEffect : MonoBehaviour {
	
	[SerializeField] private Transform m_Camera;
	public Material material;
	LineRenderer line;
	float range = 100f;

	// Use this for initialization
	void Start () {
	
		line = GetComponent<LineRenderer> ();
		//line.SetVertexCount (2);
		//line.GetComponent<Renderer> ().material = material;
		line.SetWidth (0.05f, 0.03f);
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = new Ray (m_Camera.position, m_Camera.forward);
		RaycastHit hit;
		if (OVRInput.Get (OVRInput.Button.One)) {

			line.enabled = true;
		//	line.SetPosition (0, transform.position);
			//line.SetPosition (
		}
		if (OVRInput.GetUp (OVRInput.Button.One)) 
		{
			line.enabled = false;
		}
	
	}
}
