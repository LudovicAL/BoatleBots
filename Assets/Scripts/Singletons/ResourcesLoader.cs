using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour {

    public GameObject panelPlayerJoinedPrefab;
	public GameObject panelHealthBarPrefab;
	public GameObject avatarPrefab;
	public GameObject panelJoinInstruction {get; private set;}
    public Transform panelPlayerListTransform {get; private set;}
	public Transform panelHealthBarsTransform {get; private set;}

	public static ResourcesLoader Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            performLoad();
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void performLoad() {
        panelJoinInstruction = GameObject.Find("Panel JoinInstruction");
        panelPlayerListTransform = GameObject.Find("Panel Join").transform;
		panelHealthBarsTransform = GameObject.Find("Panel HealthBars").transform;

	}
}
