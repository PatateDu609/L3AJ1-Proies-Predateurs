using UnityEngine;
using Environment;

namespace Menu
{
    class EditAction : MonoBehaviour, ButtonAction
    {
        // Monde à éditer.
        public WorldAction.WorldOptions worldPreset;

        public static Parameters parameters;

        public void Action()
        {
            switch (worldPreset)
            {
                case WorldAction.WorldOptions.WorldPreset1:
                    parameters = Parameters.Load(Application.dataPath + "/data/JSON/" + "world1.json");
                    LoadScene.Load("ParameterMenu");
                    break;
                case WorldAction.WorldOptions.WorldPreset2:
                    parameters = Parameters.Load(Application.dataPath + "/data/JSON/" + "world2.json");
                    LoadScene.Load("ParameterMenu");
                    break;
                case WorldAction.WorldOptions.WorldPreset3:
                    parameters = Parameters.Load(Application.dataPath + "/data/JSON/" + "world3.json");
                    LoadScene.Load("ParameterMenu");
                    break;
                default:
                    Debug.LogError("This option is not supported.");
                    break;
            }
        }
    }
}
