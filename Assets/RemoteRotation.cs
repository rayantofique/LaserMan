using UnityEngine;
using System.Collections;

public class RemoteRotation : MonoBehaviour {

public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (OVRInput.Get(OVRInput.Button.DpadUp) | Input.GetKey(KeyCode.UpArrow)) {
			print ("Dpadup");
			gameObject.transform.Rotate (-speed * Time.deltaTime, 0, 0, Space.World);
		}

		if (OVRInput.Get(OVRInput.Button.DpadDown) | Input.GetKey(KeyCode.DownArrow)) {
			print ("Dpaddown");
			gameObject.transform.Rotate (speed * Time.deltaTime, 0, 0, Space.World);
		}

		if (OVRInput.Get(OVRInput.Button.DpadLeft) | Input.GetKey(KeyCode.LeftArrow)) {
			print ("Dpadleft");
			gameObject.transform.Rotate (0, -speed * Time.deltaTime, 0, Space.World);
		}

		if (OVRInput.Get(OVRInput.Button.DpadRight) | Input.GetKey(KeyCode.RightArrow)) {
			print ("Dpadright");
			gameObject.transform.Rotate (0, speed * Time.deltaTime, 0, Space.World);
		}
	}
}