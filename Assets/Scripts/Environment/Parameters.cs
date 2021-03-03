using System;
using System.IO;
using UnityEngine;

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

        public static Parameters Load()
        {
            return new Parameters
            {
                aridity = 20,
                fertility = 50,
                amplitude = 15,
                resourcesQuantity = 70,
                seed = 0
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
