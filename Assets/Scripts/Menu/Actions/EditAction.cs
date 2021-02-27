using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    class EditAction : MonoBehaviour, ButtonAction
    {
        public WorldAction.WorldOptions worldPreset;

        public void Action()
        {
            switch (worldPreset)
            {
                case WorldAction.WorldOptions.WorldPreset1:
                case WorldAction.WorldOptions.WorldPreset2:
                case WorldAction.WorldOptions.WorldPreset3:
                case WorldAction.WorldOptions.NewWorld:
                    LoadScene.Load("ParameterMenu");
                    break;
                default:
                    Debug.LogError("This option is not supported.");
                    break;
            }
        }
    }
}
