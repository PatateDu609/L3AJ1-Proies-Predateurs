using System;
using UnityEngine;

public class Rain : MonoBehaviour
{

    public bool isRaining;



    private void Update()
    {
        if (isRaining) GetComponentInChildren<ParticleSystem>().Play();
        else GetComponentInChildren<ParticleSystem>().Stop();
    }

}
