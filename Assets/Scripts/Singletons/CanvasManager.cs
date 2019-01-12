using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    public GameObject panelGameStarting;
    public GameObject panelPlayerJoin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayerListManager.Instance.gameStartingEvent.AddListener(AppearBriefly);
        panelGameStarting.SetActive(false);
    }

    private void AppearBriefly()
    {
        Debug.Log("Show starting message and hide the other.");
        //AppearCoroutine(10);
        panelPlayerJoin.SetActive(false);
        panelGameStarting.SetActive(true);
        StartCoroutine(HideCoroutine(3));
    }

    IEnumerator HideCoroutine(float seconds)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(seconds);
        //Cacher le nouveau panel
        Debug.Log("Coroutine ending");
        panelGameStarting.SetActive(false);
    }

}