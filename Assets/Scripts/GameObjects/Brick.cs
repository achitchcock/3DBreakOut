﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	bool hitted = true;
	public bool isPowerup = false;
	public bool isBomb = false;
	GameObject particle;
	public Wall wall;
	// Use this for initialization
	void Start () {
		SetHit(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayExplosion() {

		particle = (GameObject)Instantiate(Resources.Load("Particle_Dissolve_Shader/Prefabs/Explosion loop mobile"));
		particle.transform.position = transform.position;
		particle.GetComponent<ParticleSystem>().Play();
		Invoke("StopParticle", 1.0f);
	}
	public void SetHit(bool hit, bool passive = false) {
		if (hit == hitted) { return; }

		Vector3 position = transform.localPosition;
		if (hit) {
			gameObject.SetActive(false);
            //position.z = 0;
            if (particle != null)
            {
                Destroy(particle);
            }
            if (!passive) {
				wall.room.HitBrick(this);
                wall.room.GetComponent<AudioSource>().Play();
            }
            PlayExplosion();
            if (isBomb)
            {
                print("bomb Block Hit:"+this.wall.ToString());
            }
            
		}
		else {
			gameObject.SetActive(true);
			position.z = -0.6f * transform.localScale.x;
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

	public void StopParticle() {
		Destroy(particle);
	}

	public void SetPowerUp() {
		isPowerup = true;
		particle = (GameObject)Instantiate(Resources.Load("Particle_Dissolve_Shader/Prefabs/Soul mobile"));
		particle.transform.position = transform.position - transform.localToWorldMatrix.MultiplyPoint(transform.forward).normalized * 0.6f;
		particle.GetComponent<ParticleSystem>().Play();	
	}

	public void SetBomb() {
		isBomb = true;
		particle = (GameObject)Instantiate(Resources.Load("Particle_Dissolve_Shader/Prefabs/Fire fuel fallback"));
		particle.transform.position = transform.position - transform.localToWorldMatrix.MultiplyPoint(transform.forward).normalized * 0.6f;
		particle.GetComponent<ParticleSystem>().Play();	
	}
}
