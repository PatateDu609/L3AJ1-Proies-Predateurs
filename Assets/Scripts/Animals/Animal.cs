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
        public List<Species> targets = new List<Species>();
        public Transform transform;
        public Transform target;
        public List<Vector3> vectors = new List<Vector3>();


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

        public void Start()
        {
            for (int i = -30; i <= 30; i += 10)
            {
                for (int j = -20; j <= 20; j += 5)
                {
                    vectors.Add((Quaternion.AngleAxis(i, Vector3.right) * Quaternion.AngleAxis(j, Vector3.up) * -Vector3.forward).normalized * gameObject.transform.localScale.z * 15);
                }
            }
        }

        public bool isThirsty()
        {
            return parameters["thirst"].value < parameters["MAX_THIRST"].value / 3; // the animal is thirsty if its current thirst level is under the third of the max
        }

        public bool isHungry()
        {
            return true;
            //return parameters["hunger"].value < parameters["MAX_HUNGER"].value / 3; // the animal is hungry if its current hunger level is under the third of the max
        }

        public void lookForRessource()
        {
            if (isHungry())
            {
                lookForFood();
            }
            else if (isThirsty())
            {
                //todo
                //lookForWater();
            }
            else
            {
               // lookAround();
            }
        }

        // moves in a random direction
        public void lookAround()
        {
            float speed = parameters.ContainsKey("MAX_RUN_SPEED") ? parameters["MAX_RUN_SPEED"].value : 0;

            float minDistance = 1f;
            float maxDistance = 2f;
            gameObject.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360));
            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.GetComponent<Rigidbody>().position + gameObject.transform.forward * UnityEngine.Random.Range(minDistance, maxDistance) * speed);
        }

        private void lookForFood()
        {
            RaycastHit hit;

            foreach (Vector3 v in vectors)
            {
                Debug.DrawRay(gameObject.transform.position, v, Color.green, 5);
                if (Physics.Raycast(gameObject.transform.position, v, out hit))
                {
                    var hitObj = hit.transform.gameObject.GetComponentInChildren<Agents.NEAT>(); 
                    if (hitObj != null && this.targets.Contains(hitObj.species))
                    {
                        target = hit.transform;
                    }
                }
                else
                {
                    lookAround();
                }
            } 
        }

        /*
        private void lookForWater()
        {
            if ()
            {

            }
            else
            {
                lookAround();
            }
        }
        */

        private void moveToTarget()
        {
            //if (this is Wolf)
            //Debug.Log("vec : " + (gameObject.transform.position - target.position).normalized + ", SPEED : " + parameters["MAX_RUN_SPEED"].value + ", DT : " + Time.deltaTime + " result : " +
            //    ((gameObject.transform.position - target.position).normalized * parameters["MAX_RUN_SPEED"].value * Time.deltaTime));
            //Debug.Log("vec : " + (Vector3.forward * parameters["MAX_RUN_SPEED"].value));
            //gameObject.transform.position += gameObject.transform.TransformDirection((gameObject.transform.position - target.position).normalized * parameters["MAX_RUN_SPEED"].value * Time.deltaTime * gameObject.transform.localScale.z);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.transform.LookAt(target);
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * parameters["MAX_RUN_SPEED"].value * 100, ForceMode.Force);
        }

        public void eat(GameObject go)
        {
            parameters["hunger"].value = parameters["MAX_HUNGER"].value; // hunger is refilled
            Destroy(go);

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        public void drink()
        {
            parameters["thirst"].value = parameters["MAX_THIRST"].value; // thirst is refilled
        }

        public float lastAction = -1;

        override public void FixedUpdate()
        {
            base.FixedUpdate();

            //Debug.Log("time : " + Time.realtimeSinceStartup);
            if (lastAction == -1 || lastAction <= Time.realtimeSinceStartup - 3)
            {
                if (target == null)
                {
                    lookForRessource();
                }
                else
                {
                    moveToTarget();
                }

                lastAction = Time.realtimeSinceStartup;

                //parameters["thirst"].value--;
                parameters["hunger"].value--;

                //Debug.Log("thirst : " + parameters["thirst"].value);
                //if (this is Wolf)
                //    Debug.Log("hunger : " + parameters["hunger"].value + ", is Hungry ? " + (isHungry() ? "yes" : "no") + ", found target ? " + (target == null ? "no" : "yes"));

                //if (parameters["thirst"].value == 0 || parameters["hunger"].value == 0)
                //if (parameters["hunger"].value == 0)
                //    Destroy(gameObject);
            }
        }
    }
}
