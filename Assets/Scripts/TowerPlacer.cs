using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {

    public BuildPlace[] buildPlaces;

    public static GameObject towerToPlace;
    public static TowerResourcesController towerResController;

    Color orignalBuildPlaceColor;

    // Use this for initialization
    void Start () {
        if(buildPlaces.Length > 0)
            orignalBuildPlaceColor = buildPlaces[0].GetComponent<SpriteRenderer>().color;

        GameManager.OnInputIdle += UnhighlightBuildPlaces;
        GameManager.OnInputPlaceTower += HighlightBuildPlaces;
       
	}
	
	// Update is called once per frame
	void Update () {
	}

    void HighlightBuildPlaces() {
        Debug.Log("Highlighting empty build places now");
        foreach(BuildPlace bp in buildPlaces) {
            if(bp.empty) {
                bp.GetComponent<SpriteRenderer>().color = Color.red;
            }
            
        }
    }

    void UnhighlightBuildPlaces() {
        foreach (BuildPlace bp in buildPlaces) {
            bp.GetComponent<SpriteRenderer>().color = orignalBuildPlaceColor;
        }
    }
}
