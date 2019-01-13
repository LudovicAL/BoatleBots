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

    void OnCollisionEnter(Collision collision)
    {
		Debug.Log("he");
		if (collision.gameObject.CompareTag("CanonBall")){
			Debug.Log("ha");
            playerHitByCannonBall.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
