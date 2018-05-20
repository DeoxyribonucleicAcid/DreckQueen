using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildbarController : MonoBehaviour {

    public TowerResourcesController[] towerResources;
    public Image[] towerUIBackgrounds;
    public static TrashController trashToApply;

    private Color[] originalColors;

    GameObject dragTrashObject;

	// Use this for initialization
	void Start () {
        dragTrashObject = null;
        originalColors = new Color[towerUIBackgrounds.Length];
        for (int i = 0; i < towerUIBackgrounds.Length; i++) {
            originalColors[i] = towerUIBackgrounds[i].color;
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(trashToApply != null) {

            HighlightTowerButtons();

            /*
            if(dragTrashObject == null) {
                 dragTrashObject = new GameObject();
                //dragTrashObject = Instantiate<GameObject>(;

                SpriteRenderer renderer = dragTrashObject.AddComponent<SpriteRenderer>();
                renderer.sprite = trashToApply.GetComponent<SpriteRenderer>().sprite;
                renderer.sortingLayerName = "Foreground";
                // renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.75f);
            } else {
                dragTrashObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            */
           
        } else {
            UnhighlightTowerButtons();
            Destroy(dragTrashObject);
            dragTrashObject = null;
        }
	}

    void HighlightTowerButtons() {
        for (int i = 0; i < towerUIBackgrounds.Length; i++) {
            towerUIBackgrounds[i].color = Color.yellow;
        }
    }

    void UnhighlightTowerButtons() {
        for (int i = 0; i < towerUIBackgrounds.Length; i++) {
            towerUIBackgrounds[i].color = originalColors[i];
        }
    }
}
