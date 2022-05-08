using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace jCaballol94.Leaves
{
    public class LeavesInteraction : MonoBehaviour
    {
        [Header("Parameters")]
        [Min(0f)] public float forceScale = 1f;
        [Min(0f)] public float forceLimit = 10f;
        [Min(0f)] public float extraUpForce = 0f;

        public Vector3 Force { get; private set; }
        public Vector4 Sphere { get; private set; }

        public float Radius => Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        private Vector3 m_previousPos;

        private void OnEnable()
        {
            m_previousPos = transform.position;
            EffectSetter.Interactions.Add(this);
        }

        private void OnDisable()
        {
            EffectSetter.Interactions.Remove(this);
        }

        private void LateUpdate()
        {
            var dist = transform.position - m_previousPos;
            var velocity = dist / Time.deltaTime;
            m_previousPos = transform.position;
            velocity.y += velocity.magnitude * extraUpForce;

            Sphere = new Vector4(transform.position.x, transform.position.y, transform.position.z, Radius);
            Force = Vector3.ClampMagnitude(velocity * forceScale, forceLimit);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}