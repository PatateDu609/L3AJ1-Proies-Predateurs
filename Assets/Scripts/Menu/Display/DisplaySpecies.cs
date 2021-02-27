using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Menu
{
    public class DisplaySpecies : MonoBehaviour
    {
        public enum SpeciesType { Prey, Predator }

        [HideInInspector]
        public SpeciesType speciesType = SpeciesType.Prey;
        [HideInInspector]
        public bool redraw = true;

        private void Start()
        {
        }

        private void Update()
        {
            if (redraw)
            {
                switch (speciesType)
                {
                    case SpeciesType.Predator:
                        DrawPredator();
                        break;
                    case SpeciesType.Prey:
                        DrawPrey();
                        break;
                }
                redraw = false;
            }
        }

        private void DrawPredator()
        {
            List<string> species = new List<string>();
            species.Add("Lion");
            species.Add("Ours");
            species.Add("Tigre");

            DrawSpecies(species);
        }

        private void DrawPrey()
        {
            List<string> species = new List<string>();
            species.Add("Lapin");
            species.Add("Vache");
            species.Add("Mouton");

            DrawSpecies(species);
        }

        private void DrawSpecies(List<string> species)
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);

            foreach (string creature in species)
            {
                GameObject instance = Instantiate(Resources.Load<GameObject>("Prefabs/Type"), transform);
                instance.name = creature;
                instance.GetComponentInChildren<Text>().text = creature;
            }
        }
    }
}
