using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tower : MonoBehaviour {

    public float damage = 10f;
    public float fireRate = 2f;
    public int requriredResources = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite GetSprite() {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
