using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSalvoManager : MonoBehaviour {
    
    public Player player;
    public GameObject cannonBallPrefab;
    public GameObject smokeAndFirePrefab;
    public float cannonForce = 300f;

    // Use this for initialization
    void Start() {
        player.playerActions.playerAttackingLeft.AddListener(ShootSalvoLeft);
        player.playerActions.playerAttackingRight.AddListener(ShootSalvoRight);
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
        cannonball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * cannonForce, ForceMode.Impulse);
        
        GameObject smokeAndFire = Instantiate(smokeAndFirePrefab, cannon.transform.position, Quaternion.identity);
        smokeAndFire.transform.Rotate(0, 90, 0);
        smokeAndFire.GetComponent<ParticleSystem>().Play();
    }

}
