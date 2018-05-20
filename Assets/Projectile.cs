using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectille : MonoBehaviour {


    public int damage = 10;

    public Vector2 movementDirection;
    public float movementSpeed;

    public bool followTarget;
    public Transform target;

    public string[] hitTags;

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
        Debug.Log("Hit: " + collision.name);
        if(inHitTags(collision.tag)) {
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
