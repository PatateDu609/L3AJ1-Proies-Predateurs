using UnityEngine;

namespace Menu
{
    class ReturnAction : MonoBehaviour, ButtonAction
    {
        public void Action()
        {
            LoadScene.Load(PreviousMenu.sceneStack.Pop(), true);
        }
    }
}
