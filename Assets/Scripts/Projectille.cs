using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector2 movementDirection;
    public float movementSpeed;

    public bool followTarget;
    public Transform target;

    string[] hitTags;

	// Use this for initialization
	void Start () {
        movementDirection = movementDirection.normalized;
        if (followTarget) {
            movementDirection = Vector2.zero;
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		if(!followTarget) {
            Vector2 velocity = movementDirection * movementSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(inHitTags(collision.tag)) {
            Destroy(gameObject);
        }
    }

    bool inHitTags(string tag) {
        foreach(string s in hitTags) {
            if (s == tag)
                return true;
        }
        return false;
    }
}
