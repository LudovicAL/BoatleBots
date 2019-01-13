using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource1;                //Drag a reference to the audio source which will play the sound effects.
    public AudioSource audioSource2;                //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public AudioClip cannonSound;
    public AudioClip splooshSound;
    public AudioClip themeSong;

    public void playCannonSound()
    {
        audioSource2.clip = cannonSound;
        audioSource2.Play();
    }

    public void playSplooshSound()
    {
        audioSource2.clip = splooshSound;
        audioSource2.Play();
    }

    public void playThemeSong()
    {
        Debug.Log("Playing theme song");
        audioSource1.loop = false;
        audioSource1.clip = themeSong;
        audioSource1.Play();
    }

    public void stopThemeSong()
    {
        audioSource1.Stop();
    }

    void Start()
    {
        loadSounds();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void loadSounds()
    {
        cannonSound = Resources.Load<AudioClip>("Audio/cannonShot");
        splooshSound = Resources.Load<AudioClip>("Audio/waterSploosh");
        themeSong = Resources.Load<AudioClip>("Audio/themeSong");
    }

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        audioSource2.clip = clip;

        //Play the clip.
        audioSource2.Play();
    }
}