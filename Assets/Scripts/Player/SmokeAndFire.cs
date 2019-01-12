using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAndFire : MonoBehaviour
{
    public int timeoutDestructor;

    void Start()
    {
        Destroy(gameObject, timeoutDestructor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

