using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Animals;

namespace Environment
{
    [Serializable]
    class Parameters
    {
        public int aridity;
        public int fertility;
        public int amplitude;
        public int resourcesQuantity;
        public int seed;
        public List<Entity> entities = new List<Entity>();

        public static Parameters Load()
        {
            Carrot carrot = ScriptableObject.CreateInstance<Carrot>();
            carrot.id = "carrot";
            carrot.ADULT_AGE = 1;
            carrot.MAX_AGE = 2;

            Wolf wolf = ScriptableObject.CreateInstance<Wolf>();
            wolf.id = "wolf";
            wolf.ADULT_AGE = 4;
            wolf.MAX_AGE = 20;
            wolf.MAX_HUNGER = 40;
            wolf.MAX_THIRST = 20;
            wolf.MAX_RUN_SPEED = 50;
            wolf.pregnancyTime = 4;
            wolf.nbOfBabyPerLitter = 3;
            wolf.interactionLevel = 5;

            Rabbit rabbit = ScriptableObject.CreateInstance<Rabbit>();
            rabbit.id = "rabbit";
            rabbit.ADULT_AGE = 2;
            rabbit.MAX_AGE = 18;
            rabbit.MAX_HUNGER = 25;
            rabbit.MAX_THIRST = 15;
            rabbit.MAX_RUN_SPEED = 45;
            rabbit.pregnancyTime = 4;
            rabbit.nbOfBabyPerLitter = 3;
            rabbit.interactionLevel = -8;
            return new Parameters
            {
                aridity = 20,
                fertility = 50,
                amplitude = 15,
                resourcesQuantity = 70,
                seed = 0,
                entities =
                {
                    rabbit,
                    wolf,
                    carrot
                }
            };
        }

        public static Parameters Load(string name)
        {
            try
            {
                string json = File.ReadAllText(name);
                return JsonUtility.FromJson<Parameters>(json);
            }
            catch
            { }

            return Load();
        }

        public static void Save(Parameters param, string name)
        {
            File.WriteAllText(name, JsonUtility.ToJson(param));
        }
    }
}
