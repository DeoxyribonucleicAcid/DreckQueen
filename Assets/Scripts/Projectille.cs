using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour {

    public int damage;
    public Vector2 movementDirection;
    public float movementSpeed;

    public bool followTarget;
    public Transform target;

    public string[] hitTags;

    private bool hasTarget;

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
            Debug.Log("FollowTarget: false");
            Vector2 velocity = movementDirection * movementSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }
        else if (target != null) {
                Debug.Log("Following: " + target);
                hasTarget = true;
                Vector2 dirToTarget = (target.position - transform.position).normalized;
                Vector2 velocity = dirToTarget * movementSpeed * Time.deltaTime;

                transform.Translate(velocity);
        } else {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(inHitTags(collision.tag) || (target == null && hasTarget) ) {
            IHitable hitableObject = collision.GetComponent<IHitable>();
            if(hitableObject != null) {
                hitableObject.Hit(damage);
            }
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
