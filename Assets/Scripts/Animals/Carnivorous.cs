using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivorous : Animal
{
    public readonly float ferocity; // likelyhood to attack other

    public List<Animal> targets;
}
