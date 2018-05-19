using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public System.Action OnDeath;

    public int lifePoints;
    public float movementSpeed;
    public int damage;

    public LayerMask wayMask;

    public float distancer = 0f;

    TrashMountain trashMountain;
    float spawnTime;

    Vector2 velocity;
    private Quaternion facing;
    private bool isOnTrashMountain = false;

    // Use this for initialization
    void Start () {
        spawnTime = Time.time;
        trashMountain = TrashMountain.GetInstance();
        facing = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = Vector2.right * movementSpeed * Time.deltaTime;

        Debug.DrawRay(transform.position, -transform.up, Color.blue);

        if (isOnTrashMountain) {
            Debug.Log("Using raycast for walking");
            ApplyRaycastRotation();
        }

        transform.Translate(velocity, Space.Self);

        /*if(Time.time - spawnTime >= 7f ) {
            lifePoints = 0;
        }*/

        

		if(lifePoints <= 0) {
            Die();
        }
	}

    void ApplyRaycastRotation() {
        Vector2 up = transform.up;
        RaycastHit2D hit;

        
        // Physics2D.Raycast()
        hit = Physics2D.Raycast(transform.position, -up, 1f, wayMask);

        if(hit.collider != null) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } 
    }

    void Die() {
        if(OnDeath != null) {
            OnDeath();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collision with " + collision.name);
        if (collision.tag == "Trash" && !isOnTrashMountain) {
            Debug.Log("Colliding with TrashMountain");

            if (velocity.x > 0) {
                transform.rotation = Quaternion.Euler(0, 0, 60);
            }
            else {
                transform.rotation = Quaternion.Euler(0, 0, -60);
            }
            isOnTrashMountain = true;
        }

        if (collision.tag == "Queen") {
            lifePoints = 0;
        }
    }

}
