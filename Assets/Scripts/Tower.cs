using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tower : MonoBehaviour {

    public float fireRate = 2f;
    public int requriredResources = 1;
 
    public Sprite GetSprite() {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
