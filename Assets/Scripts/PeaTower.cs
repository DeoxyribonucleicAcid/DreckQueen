using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaTower : Tower {

    public Transform peaSpawn;
    public Projectille peaProjectille;

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
        Enemy target = WaveManager.GetFirstLivingEnemy();
        if(target != null) {
            Debug.Log("Found enemy to shoot");
            Projectille newPea = Instantiate<Projectille>(peaProjectille, peaSpawn.position, peaSpawn.rotation);
            newPea.target = target.transform;
        }
       
    }
}
