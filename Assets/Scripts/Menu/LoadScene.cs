using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class LoadScene
    {
        public static void Load(string scene, bool previous = false)
        {
            if (!previous)
                PreviousMenu.sceneStack.Push(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(scene);
        }
    }
}