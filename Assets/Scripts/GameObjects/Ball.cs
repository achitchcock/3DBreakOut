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
		bool moving = rb.velocity.magnitude > 0.5f; 
		if (running && !moving) {
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

	public void Fire(float speed = 20.0f) {
		rb.isKinematic = false;
		rb.velocity = transform.forward * speed;
		running = true;
	}

	public void Zoom(float scale = 1.0f) {
		transform.localScale = new Vector3(scale, scale, scale);
		GetComponent<SphereCollider>().radius = 0.5f * scale;
	}
}
