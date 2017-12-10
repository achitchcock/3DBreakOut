using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour {
	public Wall[] walls;
	public Ball[] balls; 

	public delegate void RoomBrickCallback();
	public delegate void RoomBallCallback(Ball ball);
	public RoomBrickCallback brickDidHitHandler;
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

	public void HitBrick(Brick brick) {
		if (brickDidHitHandler != null) {
			if (brick.isPowerup) {
				if (Random.Range(0, 2) <= 1) {
					PowerUpBalls();
					Invoke("ResetBalls", 10);
				}
				else {
					AddBall();
					Invoke("RemoveBall", 10);
				}
			}
			else if (brick.isBomb) {
				brick.wall.HitAllBricks();
			}
			brickDidHitHandler();
		}
	}

	void PowerUpBalls() {
		foreach(Ball ball in balls) {
			ball.Zoom(5.0f);
		}
	}

	void ResetBalls() {
		foreach(Ball ball in balls) {
			ball.Zoom(1.0f);
		}
	}

	void AddBall() {
		GameObject newBall = Instantiate(balls[0].gameObject);
		List<Ball> newBalls = new List<Ball>();
		newBalls.AddRange(balls);
		newBalls.Add(newBall.GetComponent<Ball>());
		balls = newBalls.ToArray();

		ballDidResetHandler(balls[balls.Length - 1]);
		ResetBall(balls[balls.Length - 1]);
	}

	void RemoveBall() {
		List<Ball> newBalls = new List<Ball>();
		newBalls.AddRange(balls);
		newBalls.RemoveAt(balls.Length - 1);

		Destroy(balls[balls.Length - 1].gameObject);
		balls = newBalls.ToArray();
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
