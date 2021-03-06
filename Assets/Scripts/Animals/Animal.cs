using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    [Serializable]
    public class Animal : Entity
    {
        [NonSerialized]
        public float hunger; // actual hunger (if low will seek a target, if 0 dies)
        public float MAX_HUNGER; // hunger cap (if reached will not try to eat more)
        [NonSerialized]
        public float thirst; // actuel thirst (if low will seek water, if 0 dies)
        public float MAX_THIRST; // thirst cap (if reached will not try to drink more)
        [NonSerialized]
        public float runningSpeed; // the actual speed
        public float MAX_RUN_SPEED; // the maximum speed
        [NonSerialized]
        public bool isMale; // gender (true = male, false = female)
        public float pregnancyTime; // duration of pregnancy
        public int nbOfBabyPerLitter; // how many babies are born in one go
        public float interactionLevel; // measures how the animal interact with other animals (negative = afraid, 0 = neutral, positive = aggressive)
    }
}
