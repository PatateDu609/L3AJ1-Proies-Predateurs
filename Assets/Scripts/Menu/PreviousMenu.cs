using UnityEngine;
using System.Collections.Generic;
using System;

namespace Menu
{
    class PreviousMenu : MonoBehaviour
    {
        public static Stack<string> sceneStack = new Stack<string>();
        public GameObject action;

        public void Update()
        {
            if (Input.GetButtonDown("Back"))
            {
                if (action != null)
                {
                    action.GetComponent<ButtonAction>().Action();
                    Environment.Parameters p = EditAction.parameters;

                    Debug.Log("aridity : " + p.aridity);
                    Debug.Log("amplitude : " + p.amplitude);
                    Debug.Log("fertility : " + p.fertility);
                    Debug.Log("resourcesQuantity : " + p.resourcesQuantity);
                    Debug.Log("seed : " + p.seed);
                }
                LoadScene.Load(sceneStack.Pop(), true);
            }
        }
    }
}
