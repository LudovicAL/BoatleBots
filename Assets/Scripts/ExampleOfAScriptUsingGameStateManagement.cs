//Here is an example of how to implement game state management on an object that inherit MonoBehaviour.
using UnityEngine;

public class ExampleOfAScriptUsingGameStateManagement : MonoBehaviour {

	//A listener is placed on Start()
	void Start () {
        GameStatesManager.Instance.GameStateChanged.AddListener(OnGameStateChange);
	}
		
	//Here is an example of how to execute stuff only when a specified GameState is on
	void Update () {
        if (GameStatesManager.gameState == StaticData.AvailableGameStates.Playing) {
            //Do stuff...
		}
	}

	//Called when the GameState changes
	private void OnGameStateChange() {
		switch (GameStatesManager.gameState) {
            case (StaticData.AvailableGameStates.Menu):
                break;
            case (StaticData.AvailableGameStates.Starting):
                break;
            case (StaticData.AvailableGameStates.Playing):
                break;
            case (StaticData.AvailableGameStates.Pausing):
                break;
            case (StaticData.AvailableGameStates.Ending):
                break;
        }
    }
}
