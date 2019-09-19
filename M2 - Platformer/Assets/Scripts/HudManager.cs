using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public Text scoreLabel;

	void Start () {
        ResetHud();
    }

    public void ResetHud()
    {
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }
	
	
}
