using UnityEngine;
using System.Collections.Generic;

namespace Menu
{
    class PreviousMenu : MonoBehaviour
    {
        public static Stack<string> sceneStack = new Stack<string>();

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                LoadScene.Load(sceneStack.Pop(), true);
        }
    }
}
