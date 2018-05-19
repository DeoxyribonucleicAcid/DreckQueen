using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour {

    Animator animator;
    bool flipped = false;
    public bool isFalling;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isFalling) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        animator.SetBool("isFalling", isFalling);
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Trash" || collision.collider.tag == "Ground") {
            isFalling = false;
        }
    }

}
