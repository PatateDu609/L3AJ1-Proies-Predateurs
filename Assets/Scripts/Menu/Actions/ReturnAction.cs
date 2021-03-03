using UnityEngine;

namespace Menu
{
    class ReturnAction : MonoBehaviour, ButtonAction
    {
        public void Action()
        {
            SaveEnvironmentalParams save = GameObject.Find("SaveParameters").GetComponent<SaveEnvironmentalParams>();
            save.Action();

            Environment.Parameters p = EditAction.parameters;

            Debug.Log("aridity : " + p.aridity);
            Debug.Log("amplitude : " + p.amplitude);
            Debug.Log("fertility : " + p.fertility);
            Debug.Log("resourcesQuantity : " + p.resourcesQuantity);
            Debug.Log("seed : " + p.seed);

            if (PreviousMenu.sceneStack.Count != 0)
                LoadScene.Load(PreviousMenu.sceneStack.Pop(), true);
            else
                LoadScene.Load("ParameterMenu", true);
        }
    }
}
