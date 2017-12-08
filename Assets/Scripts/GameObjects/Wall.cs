﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	float brickScale;
	float brickInterval;
	public GameObject backSurface;
	public Room room;
	private List<Brick> bricks = new List<Brick>();

	// Use this for initialization
	void Start () {
		
	}

	public void InitializeBricks(int row, int cols) {
		float scaleY = backSurface.transform.localScale.y;
		float scaleX = backSurface.transform.localScale.x;
		brickScale = 1.0f;
		brickInterval = 0.7f;
		float step = brickScale + brickInterval;
		for(float xOffset = -0.5f * scaleX + 0.5f * step; xOffset <= 0.5f * scaleX - 0.5f * step; xOffset += step) {
			for(float yOffset = -0.5f * scaleY + 0.5f * step; yOffset <= 0.5f * scaleY - 0.5f * step; yOffset += step) {
				GameObject brickObject = (GameObject)Instantiate(Resources.Load("WoodenCrate/Prefab/WoodenBrick"));
				brickObject.transform.parent = transform;
				brickObject.transform.localScale = new Vector3(brickScale, brickScale, brickScale);
				brickObject.transform.localPosition = new Vector3(xOffset, yOffset, -0.6f);
				Brick brick = brickObject.AddComponent<Brick>();
				bricks.Add(brick);
				brick.room = room;
				BoxCollider coll = brickObject.GetComponent<BoxCollider>();
				coll.size = new Vector3(step * 1.22119f, step * 1.216303f, step * 1.124114f);
				coll.center = new Vector3(0, step * 0.6093565f, 0);
			}
		}
		foreach(Brick brick in bricks) {
			if (Random.Range(0, 10) == 1) {
				brick.SetPowerUp();
			}
		}
		
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public int UnhitBricks() {
		int count = 0;
		foreach(Brick b in bricks) {
			if (b.IsHit()) {
				count++;
			}
		}
		return count;
	}
}
