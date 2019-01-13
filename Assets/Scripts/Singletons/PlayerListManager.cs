using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerJoiningEvent : UnityEvent<PlayerId, bool> {}

[System.Serializable]
public class PlayerLeavingEvent : UnityEvent<PlayerId> {}

public class PlayerListManager : MonoBehaviour {
	public int maxNumPlayers;
	public List<PlayerId> listOfPlayers {get; private set;}
    public List<PlayerId> listOfAvailablePlayers;
    public PlayerJoiningEvent playerJoining = new PlayerJoiningEvent();
    public PlayerLeavingEvent playerLeaving = new PlayerLeavingEvent();

    public static PlayerListManager Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
		maxNumPlayers = Mathf.Min(maxNumPlayers, listOfAvailablePlayers.Count);
        listOfPlayers = new List<PlayerId> ();
    }

	void Update () {
		if (GameStatesManager.gameState == StaticData.AvailableGameStates.Menu) {
			for (int i = listOfPlayers.Count - 1; i >= 0; i--) {
				if (listOfPlayers[i].controls.GetButtonBDown()) {
					RemovePlayer(listOfPlayers[i]);
				}
			}
			for (int i = listOfAvailablePlayers.Count - 1; i >= 0; i--) {
				if (listOfAvailablePlayers[i].controls.GetButtonADown()) {
					AddPlayer(listOfAvailablePlayers[i]);
				}
			}
			for (int i = listOfPlayers.Count - 1; i >= 0; i--) {
				if (listOfPlayers[i].controls.GetButtonStartDown()) {
					if (listOfPlayers.Count > 1) {
						GameStatesManager.Instance.ChangeGameStateTo(StaticData.AvailableGameStates.Starting);
					}
				}
			}
		} else if (GameStatesManager.gameState == StaticData.AvailableGameStates.Ending) {
			for (int i = listOfAvailablePlayers.Count - 1; i >= 0; i--) {
				if (listOfAvailablePlayers[i].controls.GetButtonStartDown()) {
					GameStatesManager.Instance.ChangeGameStateTo(StaticData.AvailableGameStates.Menu);
				}
			}
		}
	}

	//Adds a player to the game
	private void AddPlayer(PlayerId playerId) {
		if (listOfPlayers.Count < maxNumPlayers) {
            listOfPlayers.Add(playerId);
            listOfAvailablePlayers.Remove(playerId);
            bool gameFull = (listOfPlayers.Count < maxNumPlayers) ? false : true;
			playerJoining.Invoke(playerId, gameFull);
			SoundManager.Instance.PlayPlayerJoinSound();
        }
	}

	//Removes a player from the game
	public void RemovePlayer(PlayerId playerId) {
        listOfAvailablePlayers.Add(playerId);
        listOfPlayers.Remove(playerId);
		playerLeaving.Invoke(playerId);
		if (listOfPlayers.Count < 2) {
			if (GameStatesManager.gameState == StaticData.AvailableGameStates.Playing) {
				StartCoroutine(EndGame());
			}
		}
		if (GameStatesManager.gameState == StaticData.AvailableGameStates.Menu) {
			SoundManager.Instance.PlayPlayerLeaveSound();
		}
	}

	private IEnumerator EndGame() {
		yield return new WaitForSeconds(1.7f);
		GameStatesManager.Instance.ChangeGameStateTo(StaticData.AvailableGameStates.Ending);
	}

	public void RemoveAllPlayers() {
		for (int i = listOfPlayers.Count - 1; i >= 0; i--) {
			RemovePlayer(listOfPlayers[i]);
		}
	}
}
