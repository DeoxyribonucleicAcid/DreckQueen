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

    public static bool wavesCanStart;

    Queue<Wave> waveQueue;
    public static List<Enemy> activeEnemies;
    Wave currentWave;
    public static int waveCounter;
    int currentDiedEnemies;
    bool waveRunning;

    static float waitTimeToNextWave;

	// Use this for initialization
	void Start () {
        wavesCanStart = false;
        waveQueue = new Queue<Wave>();
        activeEnemies = new List<Enemy>();
        waitTimeToNextWave = timeBetweenWaves;
        for (int i = 0; i < waves.Length; i++) {
            waveQueue.Enqueue(waves[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (wavesCanStart) {
            if (!waveRunning && waveQueue.Count > 0) {
                if (waveCounter == 0 || waitTimeToNextWave <= 0) {
                    StartCoroutine(SpawnWave(waveQueue.Dequeue()));
                    waitTimeToNextWave = timeBetweenWaves;
                }
                // Wait till starting next wave
                waitTimeToNextWave -= Time.deltaTime;
            }

            if (waveRunning && currentDiedEnemies / 2 >= currentWave.spawnCount) {
                if (OnWaveEnd != null) {
                    OnWaveEnd();
                }
                waveRunning = false;
                currentDiedEnemies = 0;
                waveCounter++;
            }

            Enemy first = GetFirstLivingEnemy();
            if(first != null)
                first.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        // Debug.Log("Current wave: " + waveCounter);
	}

    IEnumerator SpawnWave(Wave wave) {
        currentWave = wave;
        waveRunning = true;
        int remainingEnemies = wave.spawnCount * 2;
        float waveStartTime = Time.timeSinceLevelLoad;

        Debug.Log("Starting wave " + waveCounter);
        while(remainingEnemies > 0) {
            Enemy newEnemy1 = Instantiate<Enemy>(wave.enemy, spawners[0].position, spawners[0].rotation);
            Enemy newEnemy2 = Instantiate<Enemy>(wave.enemy, spawners[1].position, spawners[1].rotation);


            Debug.Log("Spawned two Enemies.");
            newEnemy1.OnDeath += EnemyDied;
            newEnemy2.OnDeath += EnemyDied;
            newEnemy2.movementSpeed *= -1;
            remainingEnemies -= 2;

            activeEnemies.Add(newEnemy1);
            activeEnemies.Add(newEnemy2);

            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

    }

    public static int GetWaitTimeToNextWave() {
        return Mathf.RoundToInt(waitTimeToNextWave);
    }

    public static Enemy GetFirstLivingEnemy() {
        if(activeEnemies.Count > 0)
            return activeEnemies[0];
        return null;
    }

    void EnemyDied(Enemy e) {
        Debug.Log("Enemy Died");
        activeEnemies.Remove(e);
        currentDiedEnemies++;
    }
}
