using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    public string id;

    public float runningSpeed;
    public static readonly float MAX_RUN_SPEED;

    public float hunger;
    public static readonly float MAX_HUNGER;

    public float thirst;
    public static readonly float MAX_THIRST;

    public int age;
    public static readonly int ADULT_AGE;
    public static readonly int MAX_AGE;

    public readonly bool isMale; // gender
    public readonly float pregnancyTime;
    public readonly int nbOfBabyPerLitter;
}
