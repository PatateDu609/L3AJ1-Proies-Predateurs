using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Environment;

namespace Animals
{
    [Serializable]
    public abstract class Entity : ScriptableObject
    {
        public Dictionary<string, Parameters.ParameterEntry> parameters = new Dictionary<string, Parameters.ParameterEntry>();
        private Transform transform;
        private Rigidbody rigidbody;
        private Species species;

        public GameObject gameObject;

        public Entity()
        {
            parameters.Add("id", new Parameters.ParameterEntry("id", ""));
            parameters.Add("age", new Parameters.ParameterEntry("age", 0, false)); // actual age
            parameters.Add("ADULT_AGE", new Parameters.ParameterEntry("ADULT_AGE", "Âge adulte", 0, Parameters.ParameterEntry.Type.Slider)); // indicate if able to reproduce
            parameters.Add("MAX_AGE", new Parameters.ParameterEntry("MAX_AGE", "Âge maximal", 0, Parameters.ParameterEntry.Type.Slider)); // age of death
            parameters.Add("thirst", new Parameters.ParameterEntry("thirst", 0, false)); // actuel thirst (if low will seek water, if 0 dies)
            parameters.Add("MAX_THIRST", new Parameters.ParameterEntry("MAX_THIRST", "Niveau de satiété hydrique", 0, Parameters.ParameterEntry.Type.Slider)); // thirst cap (if reached will not try to drink more)
            parameters.Add("isEdible", new Parameters.ParameterEntry("isEdible", true, false)); // can be eaten or not
            parameters.Add("isAlive", new Parameters.ParameterEntry("isAlive", true, false)); // alive or dead
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Rigidbody GetRigidbody()
        {
            return rigidbody;
        }

        public Species GetSpecies()
        {
            return species;
        }

        virtual public void FixedUpdate()
        {
            parameters["age"].value++;
            if (parameters["age"].value >= parameters["MAX_AGE"])
            {
                Destroy(gameObject); // die of old age
            }
        }
    }
}
