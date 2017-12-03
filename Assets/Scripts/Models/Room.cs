using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour {
	public Wall[] walls;
	public Ball[] balls; 

	public delegate void RoomCallback();
	public RoomCallback brickDidHitHandler;

	// Use this for initialization
	void Start () {
		foreach(Wall wall in walls) {
			wall.room = this;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int ReaminBricks() {
		int count = 0;
		foreach(Wall wall in walls) {
			count += wall.UnhitBricks();
		}
		return count;
	}

	public void HitBrick() {
		if (brickDidHitHandler != null) {
			brickDidHitHandler();
		}
	}
}
