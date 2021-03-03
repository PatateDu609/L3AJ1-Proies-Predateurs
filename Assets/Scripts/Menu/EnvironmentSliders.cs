using UnityEngine;
using UnityEngine.UI;
using Environment;

namespace Menu
{
    class EnvironmentSliders : MonoBehaviour
    {
        public Slider aridity;
        public Slider amplitude;
        public Slider fertility;
        public Slider resourcesQuantity;
        public InputField seed;

        private void Start()
        {
            Parameters param = EditAction.parameters == null ? Parameters.Load() : EditAction.parameters;

            aridity.value = param.aridity;
            fertility.value = param.fertility;
            amplitude.value = param.amplitude;
            resourcesQuantity.value = param.resourcesQuantity;
            seed.text = param.seed.ToString();
        }
    }
}
