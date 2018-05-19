using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

    public Text waveCounter;
    public Text waveCountdown;

    bool waitForNextWave = false;

	// Use this for initialization
	void Start () {
        waveCountdown.enabled = false;

        WaveManager.OnWaveEnd += UpdateWaveUI;
	}
	
	// Update is called once per frame
	void Update () {
        waveCounter.text = "Wave: " + (WaveManager.waveCounter + 1);

        if(waitForNextWave) {
            int waitTime = WaveManager.GetWaitTimeToNextWave();
            waveCounter.text = "" + waitTime; //"Next Wave in " + waitTime + " seconds";
            if(waitTime <= 0) {
                waitForNextWave = false;
                waveCountdown.enabled = false;
            }
        }
        
	}

    void UpdateWaveUI() {
        waitForNextWave = true;
        waveCountdown.enabled = true;
    } 
}
