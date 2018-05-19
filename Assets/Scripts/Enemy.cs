using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public System.Action OnDeath;


    public int lifePoints;
    public float movementSpeed;
    public int damage;

    float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 velocity = Vector2.right * movementSpeed * Time.deltaTime;

        transform.Translate(velocity);

        if(Time.time - spawnTime >= 7f ) {
            lifePoints = 0;
        }

		if(lifePoints <= 0) {
            Die();
        }
	}

    void Die() {
        if(OnDeath != null) {
            OnDeath();
            Destroy(gameObject);
        }
    }
}
