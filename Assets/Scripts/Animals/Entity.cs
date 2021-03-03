using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string id;
    public int age; // actual age 
    public int ADULT_AGE; // indicate if able to reproduce
    public int MAX_AGE; // age of death
    public bool isEdible; // can be eaten or not
    public bool isAlive; // alive or dead
}
