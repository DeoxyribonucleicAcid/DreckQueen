﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMountain : MonoBehaviour {

    

    public int levels;
    public GameObject queen;
    public GameObject trash;

    public Vector2 trashOffset;

    static List<GameObject> trashList;
    private List<GameObject> currentLevelTrash;
    public static int currentLevel;

    private static int destroyToNextLevel = 1;
    public static int destroyedTrashElements = 0;
    public static int staticTrashCounter = 0;

    private static QueenController queenController;

    public static TrashMountain instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public static TrashMountain GetInstance() {
        return instance;
    }


    // Use this for initialization
    void Start () {
        trashList = new List<GameObject>();
        GenerateMountain();
        StartCoroutine(SpawnQueen());
        currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(trashList.Count > 0) {
            currentLevelTrash = GetCurrentLevel(transform.position);
            foreach (GameObject go in currentLevelTrash) {
                go.GetComponent<TrashController>().clickable = true;
                go.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
	}

    void GenerateMountain() {
        Vector2 spawnPosition = transform.position;
        float trashWidth = trash.transform.localScale.x;
        float trashHeight = trash.transform.localScale.y;

        for (int i = levels; i > 0; i--) {
            spawnPosition.x = transform.position.x;

            for (int tc = -i / 2; tc <= i / 2; tc++) {
                if (i % 2 == 0 && tc == 0) {
                    continue;
                }


               // Debug.Log("tc: " + tc + " pos: " + spawnPosition);
                spawnPosition.x = tc * 2.5f * trashWidth + tc * trashOffset.x;

                if (i % 2 == 0 && tc != 0) {
                    if (tc < 0)
                        spawnPosition.x += trashWidth + trashOffset.x;
                    else
                        spawnPosition.x -= trashWidth + trashOffset.x;
                }

                GameObject newTrash = Instantiate<GameObject>(trash, spawnPosition, Quaternion.identity);
                newTrash.transform.parent = transform;
                if (tc == 0 && i % 2 != 0)
                    newTrash.GetComponent<TrashController>().isCentral = true;

                trashList.Insert(0, newTrash);
            }
            spawnPosition.y += 2.5f * trashHeight + trashOffset.y * 2;
        }
    }

    IEnumerator SpawnQueen() {

        while (!IsAllTrashStatic()) {
            yield return null;
        }

        Vector2 topTrashPos = trashList[0].transform.position;
        float queenHeight = queen.transform.localScale.y;

        Vector2 queenPos = new Vector2(topTrashPos.x, topTrashPos.y + 2f * queenHeight);
        GameObject newQueen = Instantiate<GameObject>(queen, queenPos, Quaternion.identity);
        newQueen.transform.parent = transform;
        queenController = newQueen.GetComponent<QueenController>();

        yield return new WaitForSeconds(2f);

        WaveManager.wavesCanStart = true;
    }

    public static void DestroyTrash(GameObject trash) {
        destroyedTrashElements++;
        trashList.Remove(trash);
        destroyToNextLevel--;

        if(destroyToNextLevel <= 0) {
            currentLevel++;
            destroyToNextLevel = currentLevel;
            queenController.isFalling = true;
        }
        Debug.Log("currentLevel: " + currentLevel + " destroyToNextLevel: " + destroyToNextLevel);
    }

    bool IsAllTrashStatic() {
        return staticTrashCounter >= trashList.Count;
    }

    public static List<GameObject> GetCurrentLevel(Vector2 middlePos) {
        List<GameObject> currentLevelList = new List<GameObject>();
        GameObject middleTrashElement = null;
        for (int i = 0; i < destroyToNextLevel; i++) {
            GameObject trashElement = trashList[i];
            
            if(trashElement.GetComponent<TrashController>().isCentral) {
                middleTrashElement = trashElement;
            } else {
                currentLevelList.Add(trashList[i]);
            }

            
        }
        if(currentLevelList.Count == 0 && destroyToNextLevel > 0 && middleTrashElement != null) {
            currentLevelList.Add(middleTrashElement);
        }
        return currentLevelList;
    }

}