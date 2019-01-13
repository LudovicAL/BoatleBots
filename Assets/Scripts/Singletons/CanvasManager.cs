using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

	public GameObject panelMenu;
	public GameObject panelStarting;
	public GameObject panelPlaying;
	public GameObject panelPausing;
	public GameObject panelEnding;
	public GameObject panelJoinInstruction;
	public GameObject panelStartInstruction;
	public Transform panelPlayerListTransform;
	public Transform panelHealthBarsTransform;
	public Text countDownText;
	public Text endingText;

	private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
		GameStatesManager.Instance.GameStateChanged.AddListener(OnGameStateChange);
		OnGameStateChange();
    }

	private void OnGameStateChange() {
		switch (GameStatesManager.gameState) {
			case (StaticData.AvailableGameStates.Menu):
				ShowPanel(panelMenu);
				break;
			case (StaticData.AvailableGameStates.Starting):
				ShowPanel(panelStarting);
				StartCoroutine(CountDown());
				break;
			case (StaticData.AvailableGameStates.Playing):
				ShowPanel(panelPlaying);
				break;
			case (StaticData.AvailableGameStates.Pausing):
				ShowPanel(panelPausing);
				break;
			case (StaticData.AvailableGameStates.Ending):
				if (PlayerListManager.Instance.listOfPlayers.Count > 0) {
					endingText.color = PlayerListManager.Instance.listOfPlayers[0].color;
					endingText.text = "Congratulation " + PlayerListManager.Instance.listOfPlayers[0].playerName + "!";
					PlayerListManager.Instance.RemoveAllPlayers();
					SoundManager.Instance.PlayEndGameSound();
				} else {
					endingText.color = Color.white;
					endingText.text = "You all died. Pretty sad.";
				}
				ShowPanel(panelEnding);
				break;
		}
	}

	private void ShowPanel(GameObject panel) {
		panelMenu.SetActive(false);
		panelStarting.SetActive(false);
		panelPlaying.SetActive(false);
		panelPausing.SetActive(false);
		panelEnding.SetActive(false);
		panel.SetActive(true);
		if (panel == panelPausing) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	private IEnumerator CountDown() {
		SoundManager.Instance.PlayCountDownSound();
		for (int i = 3; i > 0; i--) {
			countDownText.text = i.ToString();
			yield return new WaitForSeconds(0.7f);
		}
		GameStatesManager.Instance.ChangeGameStateTo(StaticData.AvailableGameStates.Playing);
	}
}