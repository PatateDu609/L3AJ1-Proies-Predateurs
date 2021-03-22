using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public GameObject cloud;
    GameObject[] clouds;

    public int nbClouds;



    // Start is called before the first frame update
    private void Start()
    {
        System.Random rnd = new System.Random();
        clouds = new GameObject[nbClouds];
        for (int i = 0; i < nbClouds; i++)
        {
            int X = rnd.Next(-602, 602);
            int Z = rnd.Next(-602, 602);
            GameObject obj = Instantiate(cloud);
            obj.transform.position = new Vector3(X, 100, Z);
            clouds[i] = obj;
            clouds[i].GetComponent<Rain>().isRaining = false;
        }
    }

    private void Update()
    {
        int tme = (int)(Time.realtimeSinceStartup / 60f);
        if (tme % 2 == 0)
        {
            for (int i = 0; i < nbClouds; i++)
            {
                clouds[i].GetComponent<Rain>().isRaining = i % 2 == 1;
            }
        }
        else
        {
            for (int i = 0; i < nbClouds; i++)
            {
                clouds[i].GetComponent<Rain>().isRaining = i % 2 == 0;
            }
        }
    }

}
