using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePipeTower : Tower {

    public Transform dropSpawn;
    public GameObject drop;

    float startTime;

	// Use this for initialization
	protected void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        startTime += Time.deltaTime;
		if(startTime >= fireRate) {
            startTime = 0;
            Fire();
        }
	}

    void Fire() {
        GameObject newDrop = Instantiate<GameObject>(drop, dropSpawn.position, dropSpawn.rotation);
    }
}
