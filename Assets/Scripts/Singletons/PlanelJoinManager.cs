using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanelJoinManager : MonoBehaviour {

	public GameObject panelPlayerJoinedPrefab;
    public static PlanelJoinManager Instance {get; private set;}

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
        playerId.panelJoin = Instantiate(panelPlayerJoinedPrefab, CanvasManager.Instance.panelPlayerListTransform);
        playerId.panelJoin.GetComponent<RectTransform>().localScale = Vector3.one;
        playerId.panelJoin.transform.Find("Text").GetComponent<Text>().text = playerId.playerName + " joined the game!";
        if (gameFull) {
            CanvasManager.Instance.panelJoinInstruction.SetActive(false);
        }
		if (PlayerListManager.Instance.listOfPlayers.Count > 1) {
			CanvasManager.Instance.panelStartInstruction.SetActive(true);
		}
	}

	private void OnPlayerLeaving(PlayerId playerId) {
		if (playerId.panelJoin != null) {
			Destroy(playerId.panelJoin);
		}
		if (PlayerListManager.Instance.listOfPlayers.Count < 2) {
			CanvasManager.Instance.panelStartInstruction.SetActive(false);
		}
        CanvasManager.Instance.panelJoinInstruction.SetActive(true);
	}
}
