using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	bool hitted = true;
	public Room room;
	// Use this for initialization
	void Start () {
		SetHit(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetHit(bool hit) {
		if (hit == hitted) { return; }

		Vector3 position = transform.localPosition;
		if (hit) {
			position.z = 0;
			room.HitBrick();
		}
		else {
			position.z = -1.2f * transform.localScale.x;
		}
		transform.localPosition = position;
		hitted = hit;
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "ball") {
			SetHit(true);
		}
	}

	public bool IsHit() {
		return hitted;
	}
}
