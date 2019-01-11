using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerSounds))]
public class PlayerSoundsEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PlayerSounds playerSounds = (PlayerSounds)target;
		if (GUILayout.Button("OnPlayerAttacking")) {
			playerSounds.OnPlayerAttacking(playerSounds.player.playerActions.player.playerId);
		}
		if (GUILayout.Button("OnPlayerTakingDamage")) {
			playerSounds.OnPlayerTakingDamage(playerSounds.player.playerActions.player.playerId, 10);
		}
		if (GUILayout.Button("OnPlayerDying")) {
			playerSounds.OnPlayerDying(playerSounds.player.playerActions.player.playerId);
		}
	}
}