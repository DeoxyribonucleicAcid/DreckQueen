using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

    int collisionCounter;

    public int resources = 5;
    public bool isCentral;
    public bool clickable;

    bool isStatic;
    public bool trashApplied;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(trashApplied) {
            TrashMountain.DestroyTrash(gameObject);
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Trash") {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            TrashMountain.staticTrashCounter++;
        }

        /*if(collision.collider.tag == "Queen") {
            collision.collider.GetComponent<QueenController>().isFalling = false;
        }*/
    }

    private void OnMouseDown() {
        if(clickable) {
            GameManager.GetInstance().SetMouseInputState(GameManager.MouseInputState.DragResource);
            BuildbarController.trashToApply = this;
            
        }
    }

    /*private void OnMouseDrag() {
        Debug.Log("Trash dragged");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);
    }*/
}
