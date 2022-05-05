using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace jCaballol94.Leaves
{
    public class LeavesInteraction : MonoBehaviour
    {
        private readonly int SPHERE = Shader.PropertyToID("InteractionSphere");
        private readonly int VELOCITY = Shader.PropertyToID("InteractionSpeed");

        public VisualEffect effect;
        [Min(0f)] public float forceScale = 1f;

        public float Radius => Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        private Vector3 m_previousPos;

        private void Start()
        {
            m_previousPos = transform.position;
        }

        private void LateUpdate()
        {
            var dist = transform.position - m_previousPos;
            var velocity = dist / Time.deltaTime;

            effect.SetVector4(SPHERE, new Vector4(transform.position.x, transform.position.y, transform.position.z, Radius));
            effect.SetVector3(VELOCITY, velocity * forceScale);

            m_previousPos = transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}