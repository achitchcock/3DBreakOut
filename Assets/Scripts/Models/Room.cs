using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour {
	public Wall[] walls;
	public Ball[] balls; 

	public delegate void RoomCallback();
	public delegate void RoomBallCallback(Ball ball);
	public RoomCallback brickDidHitHandler;
	public RoomBallCallback ballDidResetHandler;

	// Use this for initialization
	void Awake () {
		foreach(Wall wall in walls) {
			wall.room = this;
			wall.InitializeBricks(20, 20);
		}	
		foreach(Ball ball in balls) {
			ball.room = this;
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

	public void BallStop(Ball ball) {
		if (ballDidResetHandler != null) {
			ballDidResetHandler(ball);
		}
		ResetBall(ball);
	}
	public void ResetBall(Ball ball) {
		// reset ball position
		Vector3 cameraPos = Camera.main.transform.position;
		cameraPos += Camera.main.transform.forward * 2;
		ball.transform.position = cameraPos;
		ball.transform.rotation = Camera.main.transform.rotation;// Quaternion.Euler(Random.Range(-60, 60), Random.Range(-30,30), 0);
	}
}
