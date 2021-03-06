using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    [Serializable]
    public class Entity : ScriptableObject
    {
        public string id;
        [NonSerialized]
        public int age; // actual age 
        public int ADULT_AGE; // indicate if able to reproduce
        public int MAX_AGE; // age of death

        [NonSerialized]
        public bool isEdible; // can be eaten or not
        [NonSerialized]
        public bool isAlive; // alive or dead
    }
}
