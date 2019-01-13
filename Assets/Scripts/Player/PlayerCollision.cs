using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    public Player player;
    public PlayerHitByCannonball playerHitByCannonBall = new PlayerHitByCannonball();

    [System.Serializable]
    public class PlayerHitByCannonball : UnityEvent { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("CanonBall")){
			if (collision.gameObject.GetComponent<Cannonball>().sourcePlayerId != player.playerId) {
				playerHitByCannonBall.Invoke();
				Destroy(collision.gameObject);
			}
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
