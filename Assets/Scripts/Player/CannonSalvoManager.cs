using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSalvoManager : MonoBehaviour {
    
    public Player player;
    public GameObject cannonBallPrefab;
    public GameObject smokeAndFirePrefab;
    public float cannonForce = 300f;
    
    public float coolingDownTime = 3.0f;
    private bool isCoolingDownLeft;
    private bool isCoolingDownRight;
    private float endOfCoolingDownLeft;
    private float endOfCoolingDownRight;

    SoundManager sm;

    // Use this for initialization
    void Start() {
        player.playerActions.playerAttackingLeft.AddListener(ShootSalvoLeft);
        player.playerActions.playerAttackingRight.AddListener(ShootSalvoRight);

        isCoolingDownLeft = false;
        isCoolingDownRight = false;

        sm = GameObject.Find("Main Camera").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update () {
        if(isCoolingDownRight) {
            if (Time.time > endOfCoolingDownRight)
            {
                isCoolingDownRight = false;
            }
            if (Time.time > endOfCoolingDownLeft)
            {
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
        if (!isCoolingDownRight)
        {
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
        GameObject cannonball = Instantiate(cannonBallPrefab, cannon.transform.position, Quaternion.identity);       
        cannonball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * cannonForce, ForceMode.Impulse);

		cannon.GetComponentInChildren<ParticleSystem>().Play(true);

        sm.playCannonSound();
    }

}
