using System;
using UnityEngine;
using UnityEngine.UI;
using Animals;

namespace Menu
{
    class EntityAction : MonoBehaviour, ButtonAction
    {
        public string id;
        public bool active = false;

        private GameObject[] targets = null;
        private GameObject[] animalTargets = null;

        private void Start()
        {
            targets = GameObject.FindGameObjectsWithTag("EntityOptions");
            animalTargets = GameObject.FindGameObjectsWithTag("AnimalOptions");
        }

        public void Action()
        {
            UpdateValues(EditAction.parameters.entities.Find(e => e.id == id));
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
                targets = GameObject.FindGameObjectsWithTag("EntityOptions");

            foreach (GameObject gameObject in animalTargets)
                gameObject.SetActive(entity is Animal);

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
