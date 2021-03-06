﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class GameController : MonoBehaviour {
	public Room gameRoom;
	public Text scoreText;
	public Text timeText;
    public Text countDown;
	BrickGame currentGame;
    public GameObject hand;
    private float initTime;

	List<Ball> ballsToFire = new List<Ball>();
	// Use this for initialization
	void Start () {

        initTime = 5;
        countDown.text = ((int)initTime).ToString()+":00";
		currentGame = new BrickGame();
		currentGame.InitGame(gameRoom);
		currentGame.StartGame();
		
		
        foreach(Ball ball in gameRoom.balls)
        {
            ballsToFire.Add(ball);
        }

		gameRoom.ballDidResetHandler = delegate (Ball ball) {
			ballsToFire.Add(ball);
		};
        gameRoom.ballDidDestroyHandler = delegate (Ball ball)
        {
            ballsToFire.Remove(ball);
        };

        UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateUI();	

        initTime -= Time.deltaTime;
        countDown.text = ((int)initTime).ToString() + ":"+ ((int)((initTime-(int)initTime)*100)).ToString();
        if(currentGame.remainTime <= 0)
        {
            countDown.transform.gameObject.SetActive(true);
            countDown.text = "GAME\nOVER";
            return;
        }
        if(initTime < 0)
        {
            countDown.transform.gameObject.SetActive(false);
        }
        else
        {
            return;
        }

        currentGame.Update();
        if (ballsToFire.Count != 0 && hand.GetComponent<HierarchyControl>().handOpen != HandState.Open)
        {
            foreach (Ball ball in ballsToFire)
            {
                gameRoom.ResetBall(ball);
            }
        }
		if ((Input.GetKey(KeyCode.Space) || hand.GetComponent<HierarchyControl>().handOpen!=HandState.Closed) && ballsToFire.Count > 0) {
			ballsToFire[0].Fire();
			ballsToFire.RemoveAt(0);
		}

		ManupulateCamera();
	}

	void ManupulateCamera() {
		float step = 2.0f;
		Vector3 angles = Camera.main.transform.eulerAngles;
		if(Input.GetKey(KeyCode.LeftArrow)) {
			angles.y -= step;
		}
		else if(Input.GetKey(KeyCode.RightArrow)) {
			angles.y += step;
		}	
		else if(Input.GetKey(KeyCode.UpArrow)) {
			angles.x -= step;
		}
		else if(Input.GetKey(KeyCode.DownArrow)) {
			angles.x += step;
		}
		else {
			return;
		}
		if (angles.x > 180.0f) { angles.x -= 360.0f;}
		if (angles.y > 180.0f) { angles.y -= 360.0f;}
		if (angles.x > 50.0f) {
			angles.x = 50.0f;
		}
		if (angles.x < -50.0f) {
			angles.x = -50.0f;
		}
		if (angles.y > 40.0f) {
			angles.y = 40.0f;
		}
		if (angles.y < -40.0f) {
			angles.y = -40.0f;
		}
		Camera.main.transform.eulerAngles = angles;
        if (ballsToFire.Count != 0)
        {
            gameRoom.ResetBall(ballsToFire[0]);

        }
    }
	void UpdateUI() {
		int seconds = currentGame.RemainSeconds();
		timeText.text = string.Format("Time: {0:D2}:{1:D2}", seconds / 60, seconds % 60);
		scoreText.text = string.Format("Score: {0:D3}", currentGame.CurrentScore());
	}
}
