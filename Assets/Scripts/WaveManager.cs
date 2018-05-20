using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public static System.Action OnWaveEnd;

    public class Wave {
        public Enemy enemy;
        public int spawnCount;
        public float timeBetweenSpawns;

        public Wave(Enemy e, int sc, float tbs) {
            enemy = e;
            spawnCount = sc;
            timeBetweenSpawns = tbs;
        }
    }

    public Enemy enemy;

    public Transform[] spawners;

    // public Wave[] waves;
    
    public float timeBetweenWaves = 10f;

    public static bool wavesCanStart;

    // Queue<Wave> waveQueue;
    public static List<Enemy> activeEnemies;
    Wave currentWave;
    public static int waveCounter;
    int currentDiedEnemies;
    public static bool waveRunning;

    static float waitTimeToNextWave;

	// Use this for initialization
	void Start () {
        wavesCanStart = false;
        waveRunning = false;
        // waveQueue = new Queue<Wave>();
        activeEnemies = new List<Enemy>();
        waitTimeToNextWave = 30;
        /*for (int i = 0; i < waves.Length; i++) {
            waveQueue.Enqueue(waves[i]);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
        if (wavesCanStart) {
            Debug.Log("Waves can start");
            if (!waveRunning) {
                if (waitTimeToNextWave <= 0) {

                    Wave nextWave = GenerateNextWave();

                    StartCoroutine(SpawnWave(nextWave));
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

    Wave GenerateNextWave() {
        int nextWaveSpawnCount = waveCounter * 2;
        nextWaveSpawnCount = Mathf.Clamp(nextWaveSpawnCount, 1, 50);
        float nextTimeBetweenSpawns = 3f - (waveCounter - 1) * 2f / 40f;
        nextTimeBetweenSpawns = Mathf.Clamp(nextTimeBetweenSpawns, 0.5f, 3f);
        // enemy.lifePoints = 
        Wave wave = new Wave(enemy, nextWaveSpawnCount, nextTimeBetweenSpawns);
        return wave;
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
