﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public Text scoreValue;
    public Text highScoreValue;

	void Start () {
        scoreValue.text = GameManager.instance.score.ToString();
        highScoreValue.text = GameManager.instance.highScore.ToString();
    }
	
	public void RestartGame()
    {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Level 1");
    }
}
