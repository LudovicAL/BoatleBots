using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject avatarPrefab;
	public static PlayerSpawner Instance {get; private set;}

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		PlayerListManager.Instance.playerJoining.AddListener(OnPlayerJoining);
		PlayerListManager.Instance.playerLeaving.AddListener(OnPlayerLeaving);
	}

	private void OnPlayerJoining(PlayerId playerId, bool gameFull) {
		playerId.avatar = Instantiate(avatarPrefab.gameObject, playerId.spawnPoint, Quaternion.identity);
		playerId.player = playerId.avatar.GetComponent<Player>();
		playerId.player.playerId = playerId;
		foreach (GameObject sail in playerId.player.sails) {
            sail.GetComponent<MeshRenderer>().material.color = playerId.color;
        }
	}

    private void OnPlayerLeaving(PlayerId playerId) {
		if (playerId.avatar != null) {
			Destroy(playerId.avatar);
		}
	}
}
