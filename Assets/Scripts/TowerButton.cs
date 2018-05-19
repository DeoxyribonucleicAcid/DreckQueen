using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    TowerResourcesController resController;
    Tower tower;

    private void Start() {
        resController = GetComponentInParent<TowerResourcesController>();
        tower = resController.tower;
    }

    public void OnClick() {
        Debug.Log("Clicked on button");
        if(resController.CanBuild()) {
            TowerPlacer.towerToPlace = tower;
            TowerPlacer.towerResController = resController;
            GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.PlaceTower);
        }
        
    }
}
