using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInputController : MonoBehaviour {

    private bool mainMenuActive;

    public enum InputState { playerMenu, exploringArea }
    
    private InputState currentInputState;
    public InputState CurrentInputState {
        get {
            return currentInputState;
        }

        set {
            currentInputState = value;
        }
    }

    private InputInterface explorationController;
    public InputInterface ExplorationController {
        get {
            return explorationController;
        }
        set {
            explorationController = value;
        }
    }

    private InputInterface playerMenuController;
    public InputInterface PlayerMenuController {
        get {
            return playerMenuController;
        }

        set {
            playerMenuController = value;
        }
    }

    void Start () {
        currentInputState = InputState.exploringArea;
        mainMenuActive = false;
	}
	
	void Update () {
        if (mainMenuActive) {
            // handle the top level game menu for options/quit etc
        } else {
            switch (currentInputState) {
                case InputState.playerMenu:
                    break;

                case InputState.exploringArea:
                    explorationController.HandlePlayerInput();
                    break;

                default:
                    break;
            }
        }
	}
}
