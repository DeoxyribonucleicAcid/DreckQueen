using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlace : MonoBehaviour {

    public GameObject towerHolder;
    public bool empty = true;

    Sprite originalSprite;

	// Use this for initialization
	void Start () {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        Tower placedTower = towerHolder.GetComponent<Tower>();

        if (!empty && placedTower != null && currentSprite != placedTower.GetSprite()) {
            Debug.Log("Applying tower sprite");
            currentSprite = placedTower.GetSprite();
            GetComponent<SpriteRenderer>().sprite = placedTower.GetSprite();
        } else if(currentSprite != originalSprite) {
            currentSprite = originalSprite;
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
            Tower newTower = TowerPlacer.towerToPlace;
            Tower tower = towerHolder.AddComponent<Tower>();
            tower = newTower;
            towerHolder.GetComponent<SpriteRenderer>().sprite = tower.GetSprite();

            // Remove resources from tower
            TowerPlacer.towerResController.SubractBuildCosts();

            empty = false;
        }

        GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.Idle);
    }
}
