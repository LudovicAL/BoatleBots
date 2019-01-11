using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		player.playerHealth.playerTakingDamage.AddListener(OnTakingDamage);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTakingDamage(PlayerId playerId, float healthRatio) {
		playerId.greenHealthBar.fillAmount = Mathf.Clamp(healthRatio, 0.0f, 1.0f);
		Canvas.ForceUpdateCanvases();
	}
}
