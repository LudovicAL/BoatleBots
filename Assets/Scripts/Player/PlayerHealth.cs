using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour {

    public Player player;
    public PlayerTakingDamage playerTakingDamage = new PlayerTakingDamage();
    public PlayerDying playerDying = new PlayerDying();
	public GameObject explosionPrefab;

    [System.Serializable]
    public class PlayerTakingDamage : UnityEvent<PlayerId, float> { }

    [System.Serializable]
    public class PlayerDying : UnityEvent<PlayerId> { }

    // Use this for initialization
    void Start() {
        player.playerId.currentHealth = player.playerId.maxHealth;
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
		if (player.playerId.currentHealth > 0) {
			player.playerId.currentHealth -= amount;
			playerTakingDamage.Invoke(player.playerId, (float)player.playerId.currentHealth /(float)player.playerId.maxHealth);
			if (player.playerId.currentHealth <= 0) {
				player.playerId.currentHealth = 0;
				playerDying.Invoke(player.playerId);
				GameObject explosion = Instantiate(explosionPrefab, player.physicPlatform.transform.position, Quaternion.identity);
				Destroy(explosion, 3.0f);
				PlayerListManager.Instance.RemovePlayer(player.playerId);
				SoundManager.Instance.PlayBoatExplosionSound();
			}
		}
	}
}
