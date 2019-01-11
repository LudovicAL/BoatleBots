using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		player.playerActions.playerAttacking.AddListener(OnPlayerAttacking);
		player.playerHealth.playerTakingDamage.AddListener(OnPlayerTakingDamage);
		player.playerHealth.playerDying.AddListener(OnPlayerDying);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayerAttacking(PlayerId playerid) {
		player.audioSource.clip = playerid.setOfSoundsFx.GetAttackSoundFx();
		if (player.audioSource.clip != null) {
			player.audioSource.Play();
		}
	}

	public void OnPlayerTakingDamage(PlayerId playerid, float healthRatio) {
		player.audioSource.clip = playerid.setOfSoundsFx.GetDamageSoundFx();
		if (player.audioSource.clip != null) {
			player.audioSource.Play();
		}
	}

	public void OnPlayerDying(PlayerId playerid) {
		player.audioSource.clip = playerid.setOfSoundsFx.GetDeathSoundFx();
		if (player.audioSource.clip != null) {
			player.audioSource.Play();
		}
	}
}