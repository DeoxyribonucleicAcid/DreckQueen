using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrownController : MonoBehaviour {

    public Sprite[] crowns;

    Image crownImg;
    public static QueenController queen;

    private void Awake() {
        
    }

    // Use this for initialization
    void Start () {
        
        crownImg = GetComponent<Image>();
        Debug.Log("image Comp: " + crownImg);
    }
	
	// Update is called once per frame
	void Update () {
        if (queen != null && queen.LifePoints >= 0 && crowns.Length > 0)
            crownImg.sprite = crowns[queen.LifePoints];
            //crownImg.sprite = Sprite.Create(crowns[queen.LifePoints], crownImg.rectTransform.rect, crownImg.sprite.pivot);
	}
}
