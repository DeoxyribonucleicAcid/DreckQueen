using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    TowerResourcesController resController;
    GameObject tower;

    private void Start() {
        resController = GetComponentInParent<TowerResourcesController>();
        tower = resController.tower;
    }

    public void OnClick() {
        Debug.Log("Clicked on button");
        if(GameManager.GetInstance().GetMouseInputState() == GameManager.MouseInputState.Idle || GameManager.GetInstance().GetMouseInputState() == GameManager.MouseInputState.PlaceTower) { 
            if(resController.CanBuild()) {
                TowerPlacer.towerToPlace = tower;
                TowerPlacer.towerResController = resController;
                GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.PlaceTower);
            }
        }
        else if(GameManager.GetInstance().GetMouseInputState() == GameManager.MouseInputState.DragResource) {
            resController.AddResources(BuildbarController.trashToApply.resources);
            BuildbarController.trashToApply.trashApplied = true;
            BuildbarController.trashToApply = null;
            GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.Idle);
        }
    }
}
