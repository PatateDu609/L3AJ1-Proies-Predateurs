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
        private List<Species> targets = new List<Species>();
        private List<Vector3> vision = new List<Vector3>();
        private float lastAction = -1;

        public Animal() : base()
        {
            parameters.Add("hunger", new Parameters.ParameterEntry("hunger", 0, false)); // actual hunger (if low will seek a target, if 0 dies)
            parameters.Add("MAX_HUNGER", new Parameters.ParameterEntry("MAX_HUNGER", "Niveau de satiété", 0, Parameters.ParameterEntry.Type.Slider)); // hunger cap (if reached will not try to eat more)
            parameters.Add("runningSpeed", new Parameters.ParameterEntry("runningSpeed", 0, false)); // the actual speed
            parameters.Add("MAX_RUN_SPEED", new Parameters.ParameterEntry("MAX_RUN_SPEED", "Vitesse maximale", 0, Parameters.ParameterEntry.Type.Slider)); // the maximum speed
            parameters.Add("isMale", new Parameters.ParameterEntry("isMale", true, false)); // gender (true = male, false = female)
            parameters.Add("isPregnant", new Parameters.ParameterEntry("isPregnant", true, false));
            parameters.Add("pregnancyTime", new Parameters.ParameterEntry("pregnancyTime", "Temps de gestation", 0, Parameters.ParameterEntry.Type.Slider)); // duration of pregnancy
            parameters.Add("nbOfBabyPerLitter", new Parameters.ParameterEntry("nbOfBabyPerLitter", "Nombre d'enfant par gestation", 0, Parameters.ParameterEntry.Type.Slider)); // how many babies are born in one go
            parameters.Add("interactionLevel", new Parameters.ParameterEntry("interactionLevel", "Niveau d'interaction", 0, Parameters.ParameterEntry.Type.Slider)); // measures how the animal interact with other animals (negative = afraid, 0 = neutral, positive = aggressive)
        }

        public void Start()
        {
            for (int i = -30; i <= 30; i += 10)
            {
                for (int j = -20; j <= 20; j += 5)
                {
                    vision.Add((Quaternion.AngleAxis(i, Vector3.right) * Quaternion.AngleAxis(j, Vector3.up) * -Vector3.forward).normalized * 5);
                }
            }
        }

        public bool isHungry()
        {
            return parameters["hunger"].value < parameters["MAX_HUNGER"].value / 3; // the animal is hungry if its current hunger level is under the third of the max
        }

        public bool isThirsty()
        {
            return parameters["thirst"].value < parameters["MAX_THIRST"].value / 3; // the animal is thirsty if its current thirst level is under the third of the max
        }

        public bool wantBaby()
        {
            return parameters["age"].value >= parameters["ADULT_AGE"] && !isHungry() && !isThirsty();
        }

        public Transform look()
        {
            Vector3 front = gameObject.transform.TransformDirection(Vector3.forward * 2 * gameObject.transform.localScale.y);
            RaycastHit hit;

            foreach (Vector3 v in vision)
            {
                Debug.DrawRay(gameObject.transform.position, v, Color.green, 5, false);
                if (Physics.Raycast(gameObject.transform.position, v, out hit))
                {
                    if (this.targets.Contains(hit.transform.gameObject.GetComponentInChildren<Agents.NEAT>().species) && isHungry()) // is target in diet ?
                    {
                        return hit.transform;
                    }
                    if (hit.transform.gameObject.tag.Equals("Water") && isThirsty())
                    {
                        return hit.transform;
                    }
                    if (hit.transform.gameObject.GetComponentInChildren<Agents.NEAT>().species.Equals(this.GetSpecies()) && wantBaby()) // is target same species ?
                    {
                        if (parameters["isMale"].value && !hit.transform.gameObject.GetComponentInChildren<Entity>().parameters["isMale"].value) // male and female
                        {
                            return hit.transform;
                        }
                        if (!parameters["isMale"].value && hit.transform.gameObject.GetComponentInChildren<Entity>().parameters["isMale"].value) // female and male
                        {
                            return hit.transform;
                        }
                    }
                }
            }
            return null;
        }

        public void moveAround()
        {
            float speed = parameters.ContainsKey("MAX_RUN_SPEED") ? parameters["MAX_RUN_SPEED"].value : 0;
            float minDistance = 1f;
            float maxDistance = 2f;
            gameObject.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360));
            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.GetComponent<Rigidbody>().position + gameObject.transform.forward * UnityEngine.Random.Range(minDistance, maxDistance) * speed);
        }

        private void moveToTarget(Transform target)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.transform.LookAt(target);
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * parameters["MAX_RUN_SPEED"].value * 100, ForceMode.Force);
        }

        public void eat(GameObject go)
        {
            if (go.GetComponent<Entity>().parameters["isEdible"].value)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Destroy(go); // eat target
                parameters["hunger"].value = parameters["MAX_HUNGER"].value; // hunger is refilled
            }

        }

        public void drink()
        {
            parameters["thirst"].value = parameters["MAX_THIRST"].value; // thirst is refilled
        }

        public void baby()
        {
            
        }

        override public void FixedUpdate()
        {
            base.FixedUpdate();

            parameters["thirst"].value--;
            parameters["hunger"].value--;

            if (parameters["thirst"].value == 0 || parameters["hunger"].value == 0)
            {
                Destroy(gameObject); // die of thirst or hunger
            }

            if (lastAction == -1 || lastAction <= Time.realtimeSinceStartup - 3)
            {
                Transform target = look();

                if (target == null)
                {
                    moveAround();
                }
                else
                {
                    moveToTarget(target);
                }

                lastAction = Time.realtimeSinceStartup;  
            }

            if (parameters["isPregnant"].value)
            {
                baby();
            }
        }

        public List<Species> getTargets()
        {
            return targets;
        }

        public List<Vector3> getVision()
        {
            return vision;
        }
    }
}
