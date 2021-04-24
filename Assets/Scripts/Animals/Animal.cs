using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using Agents;
using UnityEngine.AI;

namespace Animals
{
    [Serializable]
    public abstract class Animal : Entity
    {
        public List<Species> targets = new List<Species>();
        public Transform transform;
        public Transform target;
        public GameObject targetGO;
        private Animator animator;

        public Animal() : base()
        {
            parameters.Add("hunger", new Parameters.ParameterEntry("hunger", 0.0, false)); // actual hunger (if low will seek a target, if 0 dies)
            parameters.Add("MAX_HUNGER", new Parameters.ParameterEntry("MAX_HUNGER", "Niveau de satiété", 0, Parameters.ParameterEntry.Type.Slider)); // hunger cap (if reached will not try to eat more)
            parameters.Add("thirst", new Parameters.ParameterEntry("thirst", 0.0, false)); // actuel thirst (if low will seek water, if 0 dies)
            parameters.Add("MAX_THIRST", new Parameters.ParameterEntry("MAX_THIRST", "Niveau de satiété hydrique", 0, Parameters.ParameterEntry.Type.Slider)); // thirst cap (if reached will not try to drink more)
            parameters.Add("runningSpeed", new Parameters.ParameterEntry("runningSpeed", 0.0, false)); // the actual speed
            parameters.Add("MAX_RUN_SPEED", new Parameters.ParameterEntry("MAX_RUN_SPEED", "Vitesse maximale", 0, Parameters.ParameterEntry.Type.Slider)); // the maximum speed
            parameters.Add("isMale", new Parameters.ParameterEntry("isMale", false, false)); // gender (true = male, false = female)
            parameters.Add("pregnancyTime", new Parameters.ParameterEntry("pregnancyTime", "Temps de gestation", 0, Parameters.ParameterEntry.Type.Slider)); // duration of pregnancy
            parameters.Add("nbOfBabyPerLitter", new Parameters.ParameterEntry("nbOfBabyPerLitter", "Nombre d'enfant par gestation", 0, Parameters.ParameterEntry.Type.Slider)); // how many babies are born in one go
            parameters.Add("interactionLevel", new Parameters.ParameterEntry("interactionLevel", "Niveau d'interaction", 0, Parameters.ParameterEntry.Type.Slider)); // measures how the animal interact with other animals (negative = afraid, 0 = neutral, positive = aggressive)
            parameters.Add("HPMax", new Parameters.ParameterEntry("HPMax", "Niveau de vie maximal", 0, Parameters.ParameterEntry.Type.Slider)); // Hit points of the animal
            parameters.Add("HP", new Parameters.ParameterEntry("HP", 0.0, false)); // Current hit points of the animal
            parameters.Add("Atk", new Parameters.ParameterEntry("Atk", "Niveau d'attaque maximal", 0, Parameters.ParameterEntry.Type.Slider)); // Atk points of the animal
        }

        public void Start()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public bool isThirsty()
        {
            return parameters["thirst"].value < parameters["MAX_THIRST"].value / 3; // the animal is thirsty if its current thirst level is under the third of the max
        }

        public bool isHungry()
        {
            return false;
            //return parameters["hunger"].value < parameters["MAX_HUNGER"].value / 3; // the animal is hungry if its current hunger level is under the third of the max
        }

        public bool needsToReproduce()
        {
            return parameters["age"].value >= parameters["ADULT_AGE"].value;
        }

        public bool targetIsMate()
        {
            return targetGO.GetComponent<NEAT>().Animal.GetType() == GetType();
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
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();

            if (!agent.hasPath)
            {
                MapGenerator mapGenerator = MapGenerator.instance;
                Vector3 dest;
                do
                {
                    dest = MapGenerator.GetRandomPointOnMesh(gameObject.transform.position, gameObject.transform.localScale.z * parameters["MAX_RUN_SPEED"].value);
                } while (!agent.SetDestination(dest));

                //animator.SetBool("isWalking", true);
                //animator.SetBool("isRunning", false);
            }
        }

        private void lookForMate()
        {
            SightManager sightManager = gameObject.GetComponent<SightManager>();
            List<GameObject> sight = sightManager.gameObjects;

            sight.RemoveAll(item => item == null);

            foreach (GameObject sightable in sight)
            {
                if (gameObject.tag == sightable.tag && parameters["isMale"].value != sightable.gameObject.GetComponent<NEAT>().Animal.parameters["isMale"].value)
                {
                    target = sightable.transform;
                    targetGO = sightable;
                    break;
                }
            }
        }

        private void lookForFood()
        {
            SightManager sightManager = gameObject.GetComponent<SightManager>();
            List<GameObject> sight = sightManager.gameObjects;

            sight.RemoveAll(item => item == null);

            foreach (GameObject sightable in sight)
            {
                if (targets.Contains(sightable.GetComponent<NEAT>().species))
                {
                    target = sightable.transform;
                    targetGO = sightable;
                    break;
                }
            }
        }
        /*
        private void flee()
        {
            SightManager sightManager = gameObject.GetComponent<SightManager>();
            List<GameObject> sight = sightManager.gameObjects;

            foreach (GameObject sightable in sight)
            {
                if (targets.Contains(sightable.GetComponent<NEAT>().species) && sightable.GetComponent<NEAT>().Animal)
                {
                    target = sightable.transform;
                    targetGO = sightable;
                    break;
                }
            }
        }*/

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
            gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
            //animator.SetBool("isWalking", false);
            //animator.SetBool("isRunning", true);

            Entity entity = target.gameObject.GetComponent<NEAT>().Animal;
            if (targetIsMate())
                reproduce(entity as Animal);
            else
                attack(entity);
        }

        float timeSinceRep = 0;

        private void reproduce(Animal animal)
        {
            if ((timeSinceRep == 0 || timeSinceRep - Time.realtimeSinceStartup >= 30) && Vector3.Distance(gameObject.transform.position, target.position) <= target.localScale.z)
            {
                GameObject.Instantiate(gameObject, gameObject.transform.parent);
                target = null;
                targetGO = null;

                timeSinceRep = Time.realtimeSinceStartup;
            }
        }

        private void attack(Entity entity)
        {
            if (entity is Carrot)
            {
                entity.parameters["isAlive"].value = false;
                return;
            }
            if (Vector3.Distance(gameObject.transform.position, target.position) <= 10)
            {
                int HP;
                int ATK;
                if (entity.parameters.ContainsKey("HP"))
                {
                    HP = entity.parameters["HP"].value;

                }
                else HP = 1;

                if (parameters.ContainsKey("ATK"))
                {
                    ATK = parameters["ATK"].value;

                }
                else ATK = 1;

                HP -= ATK;
                entity.parameters["HP"].value = HP;
            }
        }

        public void eat(GameObject go)
        {
            parameters["hunger"].value = parameters["MAX_HUNGER"].value; // hunger is refilled
            gameObject.GetComponent<SightManager>().gameObjects.Remove(targetGO);
            Destroy(go);

            target = null;
            targetGO = null;
            gameObject.GetComponent<NavMeshAgent>().ResetPath();
        }

        public void drink()
        {
            parameters["thirst"].value = parameters["MAX_THIRST"].value; // thirst is refilled
        }

        private void death()
        {
            if (!parameters["isAlive"].value)
                return;
            if (parameters["HP"].value <= 0 || parameters["age"].value >= parameters["MAX_AGE"].value
                || parameters["hunger"].value <= 0 || parameters["thirst"].value <= 0)
            {
                animator.SetBool("died", true);
                parameters["isAlive"].value = false;
                parameters.Add("timeSinceDeath", new Parameters.ParameterEntry("timeSinceDeath", 0, false));

                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }

        private void time()
        {
            //parameters["thrist"].value -= Time.fixedDeltaTime;
            parameters["hunger"].value -= Time.fixedDeltaTime;
            if (parameters["isAlive"].value)
                parameters["age"].value += Time.fixedDeltaTime;
            else
                parameters["timeSinceDeath"].value += Time.fixedDeltaTime;

            death();
        }

        public float lastAction = -1;

        override public void FixedUpdate()
        {
            base.FixedUpdate();

            time();

            if (!parameters["isAlive"].value)
            {
                if (parameters["timeSinceDeath"].value >= 30)
                    Destroy(gameObject);
                return;
            }

            if (target == null)
            {
                if (isHungry())
                    lookForFood();
                if (needsToReproduce())
                    lookForMate();
                lookAround();
            }
            else
            {
                moveToTarget();

                if (targetGO != null && !targetIsMate() && Vector3.Distance(target.position, gameObject.transform.position) < 2 && !target.gameObject.GetComponent<NEAT>().Animal.parameters["isAlive"].value)
                    eat(target.gameObject);
            }


            ////Debug.Log("time : " + Time.realtimeSinceStartup);
            //if (lastAction == -1 || lastAction <= Time.realtimeSinceStartup - 3)
            //{
            //    if (target == null)
            //    {
            //        lookForRessource();
            //    }
            //    else
            //    {
            //        moveToTarget();
            //    }

            //    lastAction = Time.realtimeSinceStartup;

            //    //parameters["thirst"].value--;
            //    parameters["hunger"].value--;

            //    //Debug.Log("thirst : " + parameters["thirst"].value);
            //    //if (this is Wolf)
            //    //    Debug.Log("hunger : " + parameters["hunger"].value + ", is Hungry ? " + (isHungry() ? "yes" : "no") + ", found target ? " + (target == null ? "no" : "yes"));

            //    //if (parameters["thirst"].value == 0 || parameters["hunger"].value == 0)
            //    //if (parameters["hunger"].value == 0)
            //}
        }
    }
}

