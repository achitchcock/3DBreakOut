using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Debug.Assert(rb != null);

		fire(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fire(float speed) {
		rb.velocity = transform.forward * speed;
	}
}
