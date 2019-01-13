using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceClip1;
	public AudioSource audioSourceClip2;
	public AudioSource audioSourceClip3;
	public AudioSource audioSourceSong;
	public static SoundManager Instance { get; private set; }
	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public AudioClip cannonSound;
    public AudioClip splooshSound;
    public AudioClip themeSong;
	public AudioClip playerJoinSound;
	public AudioClip playerLeaveSound;
	public AudioClip endGameSound;
	public AudioClip countDownSound;
	public AudioClip boatExplosionSound;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		LoadSounds();
		audioSourceSong.loop = true;
		audioSourceSong.clip = themeSong;
		GameStatesManager.Instance.GameStateChanged.AddListener(OnGameStateChange);
	}

	private void OnGameStateChange() {
		switch (GameStatesManager.gameState) {
			case (StaticData.AvailableGameStates.Menu):
				StopThemeSong();
				break;
			case (StaticData.AvailableGameStates.Starting):
				PlayThemeSong();
				break;
			case (StaticData.AvailableGameStates.Playing):
				PlayThemeSong();
				break;
			case (StaticData.AvailableGameStates.Pausing):
				PauseThemeSong();
				break;
			case (StaticData.AvailableGameStates.Ending):
				StopThemeSong();
				break;
		}
	}

    public void PlayCannonSound() {
		PlaySingle1(cannonSound);
    }

    public void PlaySplooshSound() {
		PlaySingle2(splooshSound);
    }

	public void PlayBoatExplosionSound() {
		PlaySingle3(boatExplosionSound);
	}

	public void PlayPlayerJoinSound() {
		PlaySingle1(playerJoinSound);
	}

	public void PlayPlayerLeaveSound() {
		PlaySingle2(playerLeaveSound);
	}

	public void PlayCountDownSound() {
		PlaySingle3(countDownSound);
	}

	public void PlayEndGameSound() {
		PlaySingle2(endGameSound);
	}

	public void PlayThemeSong() {
		audioSourceSong.Play();
    }

	public void PauseThemeSong() {
		audioSourceSong.Pause();
	}

    public void StopThemeSong() {
		audioSourceSong.Stop();
    }

    void LoadSounds() {
		boatExplosionSound = Resources.Load<AudioClip>("Audio/BoatExplosion");
		cannonSound = Resources.Load<AudioClip>("Audio/cannonShot");
        splooshSound = Resources.Load<AudioClip>("Audio/waterSploosh");
		playerJoinSound = Resources.Load<AudioClip>("Audio/PlayerJoin");
		playerLeaveSound = Resources.Load<AudioClip>("Audio/PlayerLeave");
		countDownSound = Resources.Load<AudioClip>("Audio/CountDown");
		endGameSound = Resources.Load<AudioClip>("Audio/EndGame");
		themeSong = Resources.Load<AudioClip>("Audio/themeSong");
    }

    public void PlaySingle1(AudioClip clip) {
        audioSourceClip1.clip = clip;
		audioSourceClip1.Play();
    }

	public void PlaySingle2(AudioClip clip) {
		audioSourceClip2.clip = clip;
		audioSourceClip2.Play();
	}

	public void PlaySingle3(AudioClip clip) {
		audioSourceClip3.clip = clip;
		audioSourceClip3.Play();
	}
}