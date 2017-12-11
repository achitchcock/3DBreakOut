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
    public RoomBallCallback ballDidDestroyHandler;
    public GameObject handTip;
    public HierarchyControl hControl;
    GameObject pointer;
    

	// Use this for initialization
	void Awake () {
        //pointer = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        foreach (Wall wall in walls) {
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
				if (Random.Range(0, 2) < 1) {
					PowerUpBalls();
					Invoke("ResetBalls", 10);
				}
				else {
					AddBall();
					Invoke("RemoveBall", 10);
				}
			}
			else if (brick.isBomb) {
                balls[0].GetComponent<AudioSource>().Play();
                foreach(Ball ball in balls)
                {
                    ResetBall(ball);
                    ballDidResetHandler(ball);
                }
				brick.wall.HitAllBricks();
     
			}
			brickDidHitHandler();
		}
	}

	void PowerUpBalls() {
		foreach(Ball ball in balls) {
			ball.Zoom(2.0f);
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
        ballDidDestroyHandler(balls[balls.Length - 1]);

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
        //Vector3 cameraPos = Camera.main.transform.position;
        Vector3 handPos = handTip.transform.TransformPoint(handTip.transform.localPosition);
        //cameraPos += Camera.main.transform.forward * 2;
		ball.transform.position = handPos + ball.transform.forward;
        //Quaternion rot = Quaternion.LookRotation(hControl.wristPivot.forward);
        //rot.eulerAngles = -handTip.transform.TransformDirection(handTip.transform.localRotation.eulerAngles);
        //pointer.transform.rotation = hControl.dir;
        //pointer.transform.localScale = new Vector3(0.5f, 4, 0.5f);
        //pointer.transform.localPosition = handPos;
        ball.transform.rotation = hControl.dir; //Camera.main.transform.rotation;// Quaternion.Euler(Random.Range(-60, 60), Random.Range(-30,30), 0);
	}
}
