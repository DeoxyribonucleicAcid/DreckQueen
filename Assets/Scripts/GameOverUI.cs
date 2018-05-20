using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = (WaveManager.waveCounter) + " Waves survived";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
