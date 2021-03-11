using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;

namespace Animals
{
    [Serializable]
    public abstract class Animal : Entity
    {
        public List<Species> targets;
        // utilise dans lookAround
        public float minDistance = 1f;
        public float maxDistance = 2f;

        public Animal() : base()
        {
            parameters.Add("hunger", new Parameters.ParameterEntry("hunger", 0, false)); // actual hunger (if low will seek a target, if 0 dies)
            parameters.Add("MAX_HUNGER", new Parameters.ParameterEntry("MAX_HUNGER", "Niveau de satiété", 0, Parameters.ParameterEntry.Type.Slider)); // hunger cap (if reached will not try to eat more)
            parameters.Add("thirst", new Parameters.ParameterEntry("thirst", 0, false)); // actuel thirst (if low will seek water, if 0 dies)
            parameters.Add("MAX_THIRST", new Parameters.ParameterEntry("MAX_THIRST", "Niveau de satiété hydrique", 0, Parameters.ParameterEntry.Type.Slider)); // thirst cap (if reached will not try to drink more)
            parameters.Add("runningSpeed", new Parameters.ParameterEntry("runningSpeed", 0, false)); // the actual speed
            parameters.Add("MAX_RUN_SPEED", new Parameters.ParameterEntry("MAX_RUN_SPEED", "Vitesse maximale", 0, Parameters.ParameterEntry.Type.Slider)); // the maximum speed
            parameters.Add("isMale", new Parameters.ParameterEntry("isMale", false, false)); // gender (true = male, false = female)
            parameters.Add("pregnancyTime", new Parameters.ParameterEntry("pregnancyTime", "Temps de gestation", 0, Parameters.ParameterEntry.Type.Slider)); // duration of pregnancy
            parameters.Add("nbOfBabyPerLitter", new Parameters.ParameterEntry("nbOfBabyPerLitter", "Nombre d'enfant par gestation", 0, Parameters.ParameterEntry.Type.Slider)); // how many babies are born in one go
            parameters.Add("interactionLevel", new Parameters.ParameterEntry("interactionLevel", "Niveau d'interaction", 0, Parameters.ParameterEntry.Type.Slider)); // measures how the animal interact with other animals (negative = afraid, 0 = neutral, positive = aggressive)
         
        }

        private bool isThirsty()
        {
            return parameters["thirst"].value < parameters["MAX_THIRST"].value / 3; // the animal is thirsty if its actual thirst level is under the third of the max
        }

        private bool isHungry()
        {
            return parameters["hunger"].value < parameters["MAX_HUNGER"].value / 3; // the animal is hungry if its actual hunger level is under the third of the max
        }

        public void lookForRessource()
        {
            if (isHungry())
            {
                lookForFood();
            }
            else if (isThirsty())
            {
                lookForWater();
            }
            else
            {
                lookAround();
            }
        }

        // moves in a random direction (a tester)
        public void lookAround()
        {
            GetTransform().Rotate(Vector3.forward, UnityEngine.Random.Range(0, 360));
            GetRigidbody().MovePosition(GetRigidbody().position + GetTransform().up * UnityEngine.Random.Range(minDistance, maxDistance) * parameters["runningSpeed"].value);
        }

        private void lookForFood()
        {
            RaycastHit hit;

            if (Physics.Raycast(GetTransform().position, -Vector3.up, out hit))
            {
                // TODO : a completer
                // spaghetti code : targets.contains(hit.GetSpecies()) && hit.isEdible == true
                // le probleme c'est que hit doit être un RaycastHit
                // comment obtenir un potentiel objet Entity a partir de ça ?
                if (true) 
                {

                }
            }
            else
            {
                lookAround();
            }
        }

        private void lookForWater()
        {
            // TODO : a completer
            // je ne sais pas comment est représentée l'eau dans la scene :x
            if (true)
            {

            }
            else
            {
                lookAround();
            }
        }

        public void eat(Entity e)
        {
            parameters["hunger"].value = parameters["MAX_HUNGER"].value; // hunger is refilled
        }

        public void drink()
        {
            parameters["thirst"].value = parameters["MAX_THIRST"].value; // thirst is refilled
        }

        void FixedUpdate()
        {
            lookForRessource();
        }
    }
}
