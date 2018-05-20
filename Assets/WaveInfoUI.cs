using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoUI : MonoBehaviour {

    public Text infoText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float waitTime = WaveManager.GetWaitTimeToNextWave();
		if(!WaveManager.waveRunning) {
            infoText.text = waitTime + " sec";
        } else {
            infoText.text = "Wave: " + (WaveManager.waveCounter+1);
        }
	}


}
