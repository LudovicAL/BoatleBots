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
        StartCoroutine(shootSalvoCoroutine(player.leftCannons));
    }

    public void ShootSalvoRight(PlayerId playerid) {
        StartCoroutine(shootSalvoCoroutine(player.rightCannons));
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
        GameObject smokeAndFire = Instantiate(smokeAndFirePrefab, cannon.transform.position, Quaternion.identity);
       

        cannonball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * cannonForce, ForceMode.Impulse);
        smokeAndFire.transform.TransformVector(cannon.transform.forward);
        smokeAndFire.GetComponent<ParticleSystem>().Play();
        sm.playCannonSound();
    }

}
