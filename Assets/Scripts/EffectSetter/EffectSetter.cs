using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace jCaballol94.Leaves
{
    [ExecuteAlways]
    [RequireComponent(typeof(VisualEffect))]
    public class EffectSetter : MonoBehaviour
    {
        private readonly int SPHERE_RIGHT = Shader.PropertyToID("SphereRight");
        private readonly int FORCE_RIGHT = Shader.PropertyToID("ForceRight");
        private readonly int SPHERE_LEFT = Shader.PropertyToID("SphereLeft");
        private readonly int FORCE_LEFT = Shader.PropertyToID("ForceLeft");
        private readonly int WIND = Shader.PropertyToID("Wind");

        public CharacterInteractionData interaction;
        public float windScale = 1;

        private VisualEffect m_effect;

        private void Awake()
        {
            m_effect = GetComponent<VisualEffect>();
        }

        private void LateUpdate()
        {
            if (interaction.rightFoot)
            {
                var rightFoot = interaction.rightFoot.GetData();
                m_effect.SetVector4(SPHERE_RIGHT, rightFoot.shpere);
                m_effect.SetVector3(FORCE_RIGHT, rightFoot.force);
            }

            if (interaction.leftFoot)
            {
                var leftFoot = interaction.leftFoot.GetData();
                m_effect.SetVector4(SPHERE_LEFT, leftFoot.shpere);
                m_effect.SetVector3(FORCE_LEFT, leftFoot.force);
            }

            m_effect.SetVector3(WIND, Shader.GetGlobalVector(Wind.GLOBAL_WIND) * windScale);
        }
    }
}