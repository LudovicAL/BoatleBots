using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerId", menuName = "PlayerId")]
public class PlayerId : ScriptableObject {
    public string playerName;
    public Controls controls;
	public SetOfSoundsFx setOfSoundsFx;
	public int maxHealth = 100;
	public Color color;
	public Vector3 spawnPoint;

	[HideInInspector]
	public Player player;
	[HideInInspector]
    public GameObject panelJoin;
	[HideInInspector]
	public GameObject panelHealthBar;
	[HideInInspector]
	public Image greenHealthBar;
	[HideInInspector]
	public GameObject avatar;
}