using UnityEngine;
using UnityEngine.UI;
using Environment;

namespace Menu
{
    class SaveEnvironmentalParams : MonoBehaviour, ButtonAction
    {
        public Slider aridity;
        public Slider amplitude;
        public Slider fertility;
        public Slider resourcesQuantity;
        public InputField seed;

        public void Action()
        {
            Parameters parameters = EditAction.parameters ?? new Parameters();

            parameters.aridity = Mathf.FloorToInt(aridity.value);
            parameters.amplitude = Mathf.FloorToInt(amplitude.value);
            parameters.fertility = Mathf.FloorToInt(fertility.value);
            parameters.resourcesQuantity = Mathf.FloorToInt(resourcesQuantity.value);

            try
            {
                parameters.seed = Mathf.Max(int.Parse(seed.text), 0);
            }
            catch (System.OverflowException)
            {
                parameters.seed = int.MaxValue;
            }

            EditAction.parameters = parameters;
        }
    }
}
