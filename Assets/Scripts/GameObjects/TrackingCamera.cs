using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour {
	public GameObject trackedObj;

	// Use this for initialization
	void Start () {
		Debug.Assert(trackedObj != null);
		Debug.Assert(trackedObj.GetComponent<Rigidbody>() != null);	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = trackedObj.GetComponent<Rigidbody>().velocity.normalized;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position = trackedObj.transform.position - dir * 2.5f;
        }
		
	}
}
