﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public int timeoutDestructor;
	[HideInInspector]
	public PlayerId sourcePlayerId;

    void Start()
    {
        Destroy(gameObject, timeoutDestructor);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

