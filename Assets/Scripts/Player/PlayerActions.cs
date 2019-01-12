using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour {

	public Player player;
	public PlayerAttacking playerAttacking = new PlayerAttacking();

	[System.Serializable]
	public class PlayerAttacking : UnityEvent<PlayerId> {}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (player.playerId.controls.GetButtonXDown()) {
			Attack();
		}
	}

	public void Attack() {
		playerAttacking.Invoke(player.playerId);
	}
}
