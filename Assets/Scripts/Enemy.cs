using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable {

    public System.Action<Enemy> OnDeath;

    public int lifePoints;
    public float movementSpeed;
    public int damage;

    public LayerMask wayMask;

    public float distancer = 0f;

    TrashMountain trashMountain;
    float spawnTime;

    Vector2 velocity;
    private Quaternion facing;
    private bool reachedTrashMountain = false;

    // Use this for initialization
    void Start () {
        spawnTime = Time.timeSinceLevelLoad;
        trashMountain = TrashMountain.GetInstance();
        facing = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = Vector2.right * movementSpeed * Time.deltaTime;

        ApplyRaycastRotation();
        

        transform.Translate(velocity, Space.Self);

        if(GameManager.gameOver && Time.timeSinceLevelLoad - spawnTime >= 10f ) {
            lifePoints = 0;
        }
        

		if(lifePoints <= 0) {
            Die();
        }
	}

    void ApplyRaycastRotation() {
        Vector2 horizontalRayDir;
        
        float angleFactor = 1f;

        if(velocity.x > 0) {
            horizontalRayDir = Vector2.right;
        } else {
            horizontalRayDir = Vector2.left;
            angleFactor = -1f;
        }

        Vector2 verticalRayDir1 = new Vector2(angleFactor * 0.5f, -1);
        Vector2 verticalRayDir2 = new Vector2(angleFactor * 0.75f, -1);

        RaycastHit2D hitHorizontal;
        RaycastHit2D hitVertical1;
        RaycastHit2D hitVertical2;

        // Physics2D.Raycast()
        hitHorizontal = Physics2D.Raycast(transform.position, horizontalRayDir, 0.5f, wayMask);
        hitVertical1 = Physics2D.Raycast(transform.position, verticalRayDir1, 0.5f, wayMask);
        hitVertical2 = Physics2D.Raycast(transform.position, verticalRayDir2, 0.5f, wayMask);
        

        if(hitHorizontal.collider != null) {
           // Debug.Log("Ray hit");
            Debug.DrawRay(transform.position, horizontalRayDir, Color.red);
            transform.rotation = Quaternion.Euler(0, 0, angleFactor * 60);
            reachedTrashMountain = true;
        }
        else if(reachedTrashMountain) {
            Debug.DrawRay(transform.position, verticalRayDir1, Color.blue);
            Debug.DrawRay(transform.position, verticalRayDir2, Color.blue);
            if (hitVertical1.collider != null || hitVertical2.collider != null) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                transform.rotation = Quaternion.Euler(0, 0, -angleFactor * 60);
            }

        }
    }

    void Die() {
        if(OnDeath != null) {
            OnDeath(this);
            Destroy(gameObject);
        }
    }

    public void Hit(int damage) {
        Debug.Log("Applying damage after hit");
        lifePoints -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Queen") {
            lifePoints = 0;
            collision.GetComponent<QueenController>().LifePoints--;
        }
    }
}
