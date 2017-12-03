using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Room gameRoom;
	public Text scoreText;
	public Text timeText;
	BrickGame currentGame;

	// Use this for initialization
	void Start () {
		currentGame = new BrickGame();
		currentGame.InitGame(gameRoom);
		currentGame.StartGame();

		UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateUI();	
	}

	void UpdateUI() {
		int seconds = currentGame.RemainSeconds();
		timeText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
		scoreText.text = currentGame.CurrentScore().ToString("D3");
	}
}
