using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Room room;
	private Rigidbody rb;
	private bool running = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Debug.Assert(rb != null);
		Reset();
		//Fire(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		bool moving = rb.velocity.magnitude > 1.0f; 
		if (running && !moving || Vector3.Distance(transform.localPosition, new Vector3(0,0,0))>12) {
			room.BallStop(this);
			Reset();
		}
		running = moving;
	}

	void Reset() {
		running = false;
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
	}

	public void Fire(float speed = 15.0f) {
		rb.isKinematic = false;
		rb.velocity = transform.forward * speed;
		running = true;
	}
}
