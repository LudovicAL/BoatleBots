using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSalvoManager : MonoBehaviour {
    
    public Player player;
    public GameObject cannonBallPrefab;
    public GameObject smokeAndFirePrefab;
    public float cannonForce = 300f;

    SoundManager sm;

    // Use this for initialization
    void Start() {
        player.playerActions.playerAttackingLeft.AddListener(ShootSalvoLeft);
        player.playerActions.playerAttackingRight.AddListener(ShootSalvoRight);

        sm = GameObject.Find("Main Camera").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update () {

    }

    public void ShootSalvoLeft(PlayerId playerid) {
        StartCoroutine(shootSalvoCoroutine(player.leftCannons, true));
    }

    public void ShootSalvoRight(PlayerId playerid) {
        StartCoroutine(shootSalvoCoroutine(player.rightCannons, false));
    }

    private IEnumerator shootSalvoCoroutine(List<GameObject> cannons, bool isLeft)
    {
        foreach (GameObject cannon in cannons) {
            instantiateAndShootCannonball(cannon, isLeft);
            yield return new WaitForSeconds(.15f);
        }
    }

    private void instantiateAndShootCannonball(GameObject cannon, bool isLeft) {
        GameObject cannonball = Instantiate(cannonBallPrefab, cannon.transform.position, Quaternion.identity);
        GameObject smokeAndFire = Instantiate(smokeAndFirePrefab, cannon.transform.position, Quaternion.identity);

        if(isLeft)
        {
            smokeAndFire.transform.Rotate(0, 180, 0);
        } else
        {
            cannon.transform.Rotate(0, 180, 0);
        }

        cannonball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * cannonForce, ForceMode.Impulse);
        smokeAndFire.GetComponent<ParticleSystem>().Play();
        sm.playCannonSound();
    }

}
