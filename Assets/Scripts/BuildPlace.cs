using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildPlace : MonoBehaviour {

    public GameObject towerHolder;
    public bool empty = true;

    GameObject placedTower;
    Sprite originalSprite;

	// Use this for initialization
	void Start () {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        

        if (!empty && placedTower != null && currentSprite != GetTowerSprite()) {
            Debug.Log("Disable own SpriteRenderer");
            GetComponent<SpriteRenderer>().enabled = false;
            // currentSprite = GetTowerSprite();
            // GetComponent<SpriteRenderer>().sprite = GetTowerSprite();
        } else if(currentSprite != originalSprite) {
            //currentSprite = originalSprite;
            GetComponent<SpriteRenderer>().enabled = true;
        }
	}

    private void OnMouseDown() {
        Debug.Log("Clicked on buildplace");
        switch(GameManager.GetInstance().GetMouseInputState()) {
            case GameManager.MouseInputState.PlaceTower:
                PlaceTower();
                break;
        }
    }

    void PlaceTower() {
        Debug.Log("Try to place tower");
        if(empty) {
            Debug.Log("Build place ist empty, placing now");
            GameObject newTower = TowerPlacer.towerToPlace;

            GameObject tower = Instantiate<GameObject>(newTower, transform.position, transform.rotation);
            tower.transform.parent = transform;
            // tower.transform.localScale = transform.localScale * 2;
            placedTower = tower;

            // Remove resources from tower
            TowerPlacer.towerResController.SubractBuildCosts();

            empty = false;
        }

        GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.Idle);
    }

    private void OnMouseOver() {
        if(GameManager.GetInstance().GetMouseInputState() == GameManager.MouseInputState.PlaceTower) {
            GameObject towerToPlace = TowerPlacer.towerToPlace;
            Sprite previewTowerSprite = towerToPlace.GetComponent<SpriteRenderer>().sprite;
            SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = previewTowerSprite;
            myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.5f);
        }
        
    }

    private void OnMouseExit() {
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sprite = originalSprite;
        myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 1f);
    }

    Sprite GetTowerSprite() {
        if(placedTower != null) {
            return placedTower.GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }
}
