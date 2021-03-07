using System;
using UnityEngine;
using UnityEngine.UI;
using Animals;

namespace Menu
{
    class EntityAction : MonoBehaviour, ButtonAction
    {
        [HideInInspector]
        public string id;
        [HideInInspector]
        public bool active = false;

        private static GameObject[] targets = null;
        private static GameObject[] entityTargets = null;
        private static GameObject[] animalTargets = null;

        public static bool ResetTargets
        {
            set
            {
                if (value == true)
                {
                    targets = null;
                    entityTargets = null;
                    animalTargets = null;
                }
            }
        }

        private void Start()
        {
            targets = targets ?? GameObject.FindGameObjectsWithTag("EntityOptions");
            entityTargets = entityTargets ?? GameObject.FindGameObjectsWithTag("EntityOptions");
            animalTargets = animalTargets ?? GameObject.FindGameObjectsWithTag("AnimalOptions");
        }

        public void Action()
        {
            Entity entity = EditAction.parameters.entities.Find(e => e.id == id);
            active = !active;

            if (active)
            {
                GameObject.Find("SaveParameters").GetComponent<SaveAgentParams>().Action();
                SaveAgentParams.entityAction = this;
                UpdateValues(entity);
            }
            else
            {
                SaveValues(entity);
                SaveAgentParams.entityAction = null;
                foreach (GameObject go in targets)
                    go.SetActive(false);
            }
        }

        private void SaveValues(Entity entity)
        {
            Debug.Log("Saving : " + id);
            foreach (GameObject gameObject in targets)
            {
                switch (gameObject.name)
                {
                    // Les propriétés suivantes ne sont modifiées que si entity est un animal.
                    case "Hunger":
                        ((Animal)entity).MAX_HUNGER = gameObject.GetComponentInChildren<Slider>().value;
                        break;
                    case "Thirst":
                        ((Animal)entity).MAX_THIRST = gameObject.GetComponentInChildren<Slider>().value;
                        break;
                    case "Speed":
                        ((Animal)entity).MAX_RUN_SPEED = gameObject.GetComponentInChildren<Slider>().value;
                        break;
                    case "Pregnancy":
                        ((Animal)entity).pregnancyTime = gameObject.GetComponentInChildren<Slider>().value;
                        break;
                    case "Litter":
                        ((Animal)entity).nbOfBabyPerLitter = Mathf.RoundToInt(gameObject.GetComponentInChildren<Slider>().value);
                        break;
                    case "Interaction":
                        ((Animal)entity).interactionLevel = gameObject.GetComponentInChildren<Slider>().value;
                        break;

                    // Les propriétés suivantes sont modifiées quelque soit le type réel d'entity.
                    case "MaxAge":
                        entity.MAX_AGE = Mathf.RoundToInt(gameObject.GetComponentInChildren<Slider>().value);
                        break;
                    case "AdultAge":
                        entity.ADULT_AGE = Mathf.RoundToInt(gameObject.GetComponentInChildren<Slider>().value);
                        break;
                }
            }
        }

        private void UpdateValues(Entity entity)
        {
            if (entity is Animal)
            {
                int l = targets.Length;
                Array.Resize(ref targets, targets.Length + animalTargets.Length);
                Array.Copy(animalTargets, 0, targets, l, animalTargets.Length);
            }
            else
                targets = entityTargets;

            foreach (GameObject gameObject in targets)
                gameObject.SetActive(true);

            foreach (GameObject gameObject in targets)
            {
                switch (gameObject.name)
                {
                    // Les propriétés suivantes ne sont modifiées que si entity est un animal.
                    case "Hunger":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).MAX_HUNGER;
                        break;
                    case "Thirst":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).MAX_THIRST;
                        break;
                    case "Speed":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).MAX_RUN_SPEED;
                        break;
                    case "Pregnancy":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).pregnancyTime;
                        break;
                    case "Litter":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).nbOfBabyPerLitter;
                        break;
                    case "Interaction":
                        gameObject.GetComponentInChildren<Slider>().value = ((Animal)entity).interactionLevel;
                        break;

                    // Les propriétés suivantes sont modifiées quelque soit le type réel d'entity.
                    case "MaxAge":
                        gameObject.GetComponentInChildren<Slider>().value = entity.MAX_AGE;
                        break;
                    case "AdultAge":
                        gameObject.GetComponentInChildren<Slider>().value = entity.ADULT_AGE;
                        break;
                }
            }
        }
    }
}
