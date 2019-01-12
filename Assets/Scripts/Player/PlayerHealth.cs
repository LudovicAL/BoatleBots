using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour {

    public int currentHealth;
    public Player player;
    public PlayerTakingDamage playerTakingDamage = new PlayerTakingDamage();
    public PlayerDying playerDying = new PlayerDying();

    [System.Serializable]
    public class PlayerTakingDamage : UnityEvent<PlayerId, float> { }

    [System.Serializable]
    public class PlayerDying : UnityEvent<PlayerId> { }

    // Use this for initialization
    void Start() {
        currentHealth = player.playerId.maxHealth;
        player.playerCollision.playerHitByCannonBall.AddListener(OnTakeDamageCannonBall);
    }

    // Update is called once per frame
    void Update() {
     
    }

    public void OnTakeDamageCannonBall() {
        TakeDamage(15);
    }

	//Call this function when dealing damage to this player
	public void TakeDamage(int amount) {
		currentHealth -= amount;
		playerTakingDamage.Invoke(player.playerId, (float)currentHealth/(float)player.playerId.maxHealth);
		if (currentHealth <= 0) {
			currentHealth = 0;
			playerDying.Invoke(player.playerId);
		}
	}
}
