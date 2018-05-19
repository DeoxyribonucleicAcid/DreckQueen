using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public static System.Action OnWaveEnd;

    [System.Serializable]
    public class Wave {
        public Enemy enemy;
        public int spawnCount;
        public float timeBetweenSpawns;
    }

    public Transform[] spawners;

    public Wave[] waves;
    
    public float timeBetweenWaves = 10f;

    Queue<Wave> waveQueue;
    Wave currentWave;
    public static int waveCounter;
    int currentDiedEnemies;
    bool waveRunning;

    static float waitTimeToNextWave;

	// Use this for initialization
	void Start () {
        waveQueue = new Queue<Wave>();
        waitTimeToNextWave = timeBetweenWaves;
        for (int i = 0; i < waves.Length; i++) {
            waveQueue.Enqueue(waves[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(!waveRunning && waveQueue.Count > 0) {
            if(waveCounter == 0 || waitTimeToNextWave <= 0) {
                StartCoroutine(SpawnWave(waveQueue.Dequeue()));
                waitTimeToNextWave = timeBetweenWaves;
            }
            // Wait till starting next wave
            waitTimeToNextWave -= Time.deltaTime;
        }

        if(waveRunning && currentDiedEnemies / 2 >= currentWave.spawnCount) {
            if (OnWaveEnd != null) {
                OnWaveEnd();
            }
            waveRunning = false;
            currentDiedEnemies = 0;
            waveCounter++;
        }
        Debug.Log("Current wave: " + waveCounter);
	}

    IEnumerator SpawnWave(Wave wave) {
        currentWave = wave;
        waveRunning = true;
        int remainingEnemies = wave.spawnCount * 2;
        float waveStartTime = Time.time;

        Debug.Log("Starting wave " + waveCounter);
        while(remainingEnemies > 0) {
            Enemy newEnemy1 = Instantiate<Enemy>(wave.enemy, spawners[0].position, spawners[0].rotation);
            Enemy newEnemy2 = Instantiate<Enemy>(wave.enemy, spawners[1].position, spawners[1].rotation);
            Debug.Log("Spawned two Enemies.");
            newEnemy1.OnDeath += EnemyDied;
            newEnemy2.OnDeath += EnemyDied;
            newEnemy2.movementSpeed *= -1;
            remainingEnemies -= 2;
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

    }

    public static int GetWaitTimeToNextWave() {
        return Mathf.RoundToInt(waitTimeToNextWave);
    }


    void EnemyDied() {
        Debug.Log("Enemy Died");
        currentDiedEnemies++;
    }
}
