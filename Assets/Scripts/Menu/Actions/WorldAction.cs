using UnityEngine;

namespace Menu
{
    class WorldAction : MonoBehaviour, ButtonAction
    {
        public enum WorldOptions { WorldPreset1, WorldPreset2, WorldPreset3, NewWorld }

        public WorldOptions worldPreset;
        public static WorldOptions preset;

        public void Action()
        {
            preset = worldPreset;
            switch (worldPreset)
            {
                case WorldOptions.WorldPreset1:
                case WorldOptions.WorldPreset2:
                case WorldOptions.WorldPreset3:
                case WorldOptions.NewWorld:
                    LoadScene.Load("World");
                    break;
                default:
                    Debug.LogError("This option is not supported.");
                    break;
            }
        }
    }
}
