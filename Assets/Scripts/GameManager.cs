using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static System.Action OnInputIdle;
    public static System.Action OnInputPlaceTower;
    public static System.Action OnInputDragResource;

    public enum MouseInputState {
        Idle,
        PlaceTower,
        DragResource,
    }

    private static MouseInputState mouseInputState;

    #region singleton
    private static GameManager instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public static GameManager GetInstance() {
        return instance;
    }
    #endregion

    // Use this for initialization
    void Start () {
		mouseInputState = MouseInputState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMouseInputState(MouseInputState newState) {
        mouseInputState = newState;
        switch(mouseInputState) {
            case MouseInputState.Idle:
                CallAction(OnInputIdle);
                break;
            case MouseInputState.PlaceTower:
                CallAction(OnInputPlaceTower);
                break;
            case MouseInputState.DragResource:
                CallAction(OnInputDragResource);
                break;
        }
    }

    public MouseInputState GetMouseInputState() {
        return mouseInputState;
    }

    private void CallAction(System.Action action) {
        if(action != null) {
            action();
        }
    }
}
