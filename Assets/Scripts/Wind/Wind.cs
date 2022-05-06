using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.Leaves
{
    [ExecuteAlways]
    public class Wind : MonoBehaviour
    {
        public static readonly int GLOBAL_WIND = Shader.PropertyToID("_GlobalWind");
        public static readonly int GLOBAL_PARAMS = Shader.PropertyToID("_GlobalWindParams");

        [Min(0f)] public float strength;

        [Header("Tree Noise")]
        public float mainPeriod = 2;
        public float mainAmplitude = 1;
        public float secondaryPeriod = 1;
        public float secondaryAmplitude = 0.5f;

        private void Update()
        {
            Shader.SetGlobalVector(GLOBAL_WIND, transform.forward * strength);
            Shader.SetGlobalVector(GLOBAL_PARAMS, new Vector4(mainPeriod, mainAmplitude, secondaryPeriod, secondaryAmplitude));
        }
    }
}