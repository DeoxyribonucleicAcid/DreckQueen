using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour {

    public int LifePoints;

    Animator animator;
    bool flipped = false;
    public bool isFalling;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFalling = true;
	}

    private void Update() {
        if(LifePoints <= 0) {
            GameManager.gameOver = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(isFalling) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        animator.SetBool("isFalling", isFalling);
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Queen colided with " + collision.collider.name);
        if (collision.collider.tag == "Trash" || collision.collider.tag == "Ground") {
            isFalling = false;
        }

    }

}
