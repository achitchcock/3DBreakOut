using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGame {
	public float remainTime;
	int score;
	public Room gameRoom;

	// Update is called once per frame
	public void Update () {
		remainTime -= Time.deltaTime;
		if (remainTime < 0) {
			remainTime = 0.0f;
		}

		if (gameRoom.ReaminBricks() == 0) {
            resetBricks();
            
		}	
		else if (remainTime <= 0) {
			GameOver();
		}
	}

	public void InitGame(Room room) {
		gameRoom = room;
		gameRoom.brickDidHitHandler = HitBrickHandler;
		score = 0;
		remainTime = 120.0f;
	}

	public void StartGame() {
	}

	public void PauseGame() {

	}

	void GameOver() {
        
    }

    void resetBricks()
    {
        foreach (Wall w in gameRoom.walls)
        {
            foreach (Brick b in w.bricks)
            {
                b.GetComponent<Brick>().SetHit(false);
            }
        }
    }


    public void HitBrickHandler() {
		score += 1;
	}

	public void HitPowerUp() {

	}

	public int CurrentScore() {
		return score;
	}

	public int RemainSeconds() {
		return (int)remainTime;
	}
}
