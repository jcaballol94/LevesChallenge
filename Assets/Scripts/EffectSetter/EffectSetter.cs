using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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

        private readonly int INTERACTIONS_BUFFER = Shader.PropertyToID("Interactions");
        private readonly int INTERACTIONS_COUNT = Shader.PropertyToID("NumInteractions");

        private readonly int WIND = Shader.PropertyToID("Wind");

        public static List<LeavesInteraction> Interactions { get; private set; } = new List<LeavesInteraction>();

        public float windScale = 1;

        private VisualEffect m_effect;
        private GraphicsBuffer m_interactionsBuffer;
        private NativeArray<Vector4> m_interactionsData;
        private Bounds m_effectBounds;

        private void Awake()
        {
            m_effect = GetComponent<VisualEffect>();
        }

        private void OnEnable()
        {
            var names = new List<string>();
            m_effect.GetParticleSystemNames(names);
            bool first = true;
            foreach (var name in names)
            {
                var info = m_effect.GetParticleSystemInfo(name);
                if (first)
                    m_effectBounds = info.bounds;
                else
                    m_effectBounds.Encapsulate(info.bounds);
                first = false;
            }

            var numInteractions = m_effect.GetUInt(INTERACTIONS_COUNT);
            m_interactionsData = new NativeArray<Vector4>((int)numInteractions * 2, Allocator.Persistent);
            m_interactionsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, (int)numInteractions * 2, 16);
            m_interactionsBuffer.SetData(m_interactionsData);
            m_effect.SetGraphicsBuffer(INTERACTIONS_BUFFER, m_interactionsBuffer);
        }

        private void OnDisable()
        {
            m_interactionsBuffer.Dispose();
            m_interactionsData.Dispose();
        }

        private void LateUpdate()
        {
            var idx = 0;
            foreach (var interaction in Interactions)
            {
                if (m_effectBounds.Contains(interaction.Sphere))
                {
                    m_interactionsData[idx++] = interaction.Force;
                    m_interactionsData[idx++] = interaction.Sphere;
                }
            }

            for (; idx < m_interactionsData.Length; idx++)
            {
                m_interactionsData[idx] = Vector4.zero;
            }
            m_interactionsBuffer.SetData(m_interactionsData);

            m_effect.SetVector3(WIND, Shader.GetGlobalVector(Wind.GLOBAL_WIND) * windScale);
        }
    }
}