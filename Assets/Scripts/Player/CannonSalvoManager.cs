using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonSalvoManager : MonoBehaviour {
    
    public Player player;
    public GameObject cannonBallPrefab;
    public GameObject smokeAndFirePrefab;
    public float cannonForce = 300f;

	public float coolingDownTime = 2.5f;
    private bool isCoolingDownLeft;
    private bool isCoolingDownRight;
    private float endOfCoolingDownLeft;
    private float endOfCoolingDownRight;

	private void Awake() {
	}

	// Use this for initialization
	void Start() {
        player.playerActions.playerAttackingLeft.AddListener(ShootSalvoLeft);
        player.playerActions.playerAttackingRight.AddListener(ShootSalvoRight);

        isCoolingDownLeft = false;
        isCoolingDownRight = false;
    }

    // Update is called once per frame
    void Update () {
		if (isCoolingDownRight) {
			if (Time.time > endOfCoolingDownRight) {
				isCoolingDownRight = false;
			}
		}
		if (isCoolingDownLeft) {
			if (Time.time > endOfCoolingDownLeft) {
				isCoolingDownLeft = false;
			}
		}
    }

    public void ShootSalvoLeft(PlayerId playerid) {

        if (!isCoolingDownLeft) {
            StartCoroutine(shootSalvoCoroutine(player.leftCannons));
            isCoolingDownLeft = true;
            endOfCoolingDownLeft = Time.time + coolingDownTime;
        }
    }

    public void ShootSalvoRight(PlayerId playerid)
    {
        if (!isCoolingDownRight) {
            StartCoroutine(shootSalvoCoroutine(player.rightCannons));
            isCoolingDownRight = true;
            endOfCoolingDownRight = Time.time + coolingDownTime;
        }        
    }

    private IEnumerator shootSalvoCoroutine(List<GameObject> cannons)
    {
        foreach (GameObject cannon in cannons) {
            instantiateAndShootCannonball(cannon);
            yield return new WaitForSeconds(.15f);
        }
    }

    private void instantiateAndShootCannonball(GameObject cannon) {
        GameObject cannonball = Instantiate(cannonBallPrefab, cannon.transform.position + cannon.transform.forward * 0.5f, Quaternion.identity);       
        cannonball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * cannonForce, ForceMode.Impulse);
		cannonball.GetComponent<Cannonball>().sourcePlayerId = player.playerId;
		cannon.GetComponentInChildren<ParticleSystem>().Play(true);
		SoundManager.Instance.PlayCannonSound();
    }

}
