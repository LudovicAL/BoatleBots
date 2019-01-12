using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanelJoinManager : MonoBehaviour {

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
        playerId.panelJoin = Instantiate(ResourcesLoader.Instance.panelPlayerJoinedPrefab, ResourcesLoader.Instance.panelPlayerListTransform);
        playerId.panelJoin.GetComponent<RectTransform>().localScale = Vector3.one;
        playerId.panelJoin.transform.Find("Text").GetComponent<Text>().text = playerId.playerName + " joined the game!";
        if (gameFull) {
            ResourcesLoader.Instance.panelJoinInstruction.SetActive(false);
        }
		Canvas.ForceUpdateCanvases();
	}

    private void OnPlayerLeaving(PlayerId playerId) {
        if (playerId.panelJoin != null) {
            Destroy(playerId.panelJoin);
        }
        ResourcesLoader.Instance.panelJoinInstruction.SetActive(true);
		Canvas.ForceUpdateCanvases();
	}
}
