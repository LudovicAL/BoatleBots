using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerId playerId;
    public PlayerHealth playerHealth;
    public PlayerHealthBar playerHealthBar;
    public PlayerActions playerActions;
    public PlayerSounds playerSounds;
    public AudioSource audioSource;
    public List<GameObject> leftCannons;
    public List<GameObject> rightCannons;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}
}
