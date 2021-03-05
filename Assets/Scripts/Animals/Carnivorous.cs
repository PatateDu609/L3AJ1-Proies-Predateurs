using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    [Serializable]
    public class Carnivorous : Animal
    {
        public List<Entity> targets;
    }
}
