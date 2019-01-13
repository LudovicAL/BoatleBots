using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerJoiningEvent : UnityEvent<PlayerId, bool> {}

[System.Serializable]
public class PlayerLeavingEvent : UnityEvent<PlayerId> {}

[System.Serializable]
public class GameStartingEvent : UnityEvent { }

public class PlayerListManager : MonoBehaviour {
	public int maxNumPlayers;
	public List<PlayerId> listOfPlayers {get; private set;}
    public List<PlayerId> listOfAvailablePlayers;
    public PlayerJoiningEvent playerJoining = new PlayerJoiningEvent();
    public PlayerLeavingEvent playerLeaving = new PlayerLeavingEvent();
    public GameStartingEvent gameStartingEvent = new GameStartingEvent();
    SoundManager sm;

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
        sm = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        listOfPlayers = new List<PlayerId> ();
    }

	void Update () {
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
        for (int i = listOfPlayers.Count - 1; i >= 0; i--)
        {
            if (listOfPlayers[i].controls.GetButtonStartDown())
            {
                Debug.Log("Start button pressed, starting game...");
                gameStartingEvent.Invoke();
                sm.playThemeSong();
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
        }
	}

	//Removes a player from the game
	private void RemovePlayer(PlayerId playerId) {
        listOfAvailablePlayers.Add(playerId);
        listOfPlayers.Remove(playerId);
		playerLeaving.Invoke(playerId);
    }
}
