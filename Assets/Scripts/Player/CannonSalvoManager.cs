using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSalvoManager : MonoBehaviour {
    
    public Player player;
    public GameObject cannonBallPrefab;

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
            yield return new WaitForSeconds(.1f);
        }
    }

    private void instantiateAndShootCannonball(GameObject cannon) {

        for (int i = 0; i < 10; i++)
        {
            
        }
    }
}
