using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Animals;

namespace Environment
{
    class Stats : MonoBehaviour
    {
        private Dictionary<string, Parameters.ParameterEntry> parameters;

        public GameObject basicStats;
        public GameObject midStats;
        public GameObject advancedStats;

        private void Start()
        {
            parameters = (Menu.EditAction.parameters ?? Parameters.Load()).parameters;

            SetBasicStats();

            basicStats.SetActive(false);
            midStats.SetActive(false);
            advancedStats.SetActive(false);
        }

        private void Update()
        {
            bool f3 = Input.GetKeyDown(KeyCode.F3);
            bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

            if (!ctrl && !shift && f3)
                basicStats.SetActive(!basicStats.activeSelf);
            else if (!ctrl && shift && f3)
            {
                SetMidStats();

                basicStats.SetActive(!midStats.activeSelf);
                midStats.SetActive(!midStats.activeSelf);
            }
            else if (ctrl && shift && f3)
            {
                SetAdvancedStats();
                basicStats.SetActive(!advancedStats.activeSelf);
                midStats.SetActive(!advancedStats.activeSelf);
                advancedStats.SetActive(!advancedStats.activeSelf);
            }
        }

        private void SetMidStats()
        {
            //TODO : link animal's spawn to stats
        }

        private void SetAdvancedStats()
        {

        }

        private void SetBasicStats()
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("BasicStats");

            foreach (GameObject go in gos)
            {
                go.GetComponent<Text>().text += " ";
                if (go.name == "entities")
                {
                    go.GetComponent<Text>().text += ((List<Entity>)parameters["entities"].value).Count;
                    continue;
                }
                go.GetComponent<Text>().text += parameters[go.name].value;
            }
        }
    }
}
