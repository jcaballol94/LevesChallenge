using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.Leaves
{
    [ExecuteAlways]
    public class Wind : MonoBehaviour
    {
        public static readonly int GLOBAL_WIND = Shader.PropertyToID("_globalWind");

        [Min(0f)] public float strength;

        private void Update()
        {
            Shader.SetGlobalVector(GLOBAL_WIND, transform.forward * strength);
        }
    }
}