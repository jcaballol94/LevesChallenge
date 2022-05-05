using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace jCaballol94.Leaves
{
    public class LeavesInteraction : MonoBehaviour
    {
        private readonly int SPHERE_RIGHT = Shader.PropertyToID("SphereRight");
        private readonly int VELOCITY_RIGHT = Shader.PropertyToID("ForceRight");
        private readonly int SPHERE_LEFT = Shader.PropertyToID("SphereLeft");
        private readonly int VELOCITY_LEFT = Shader.PropertyToID("ForceLeft");

        public VisualEffect effect;
        [Min(0f)] public float forceScale = 1f;
        [Min(0f)] public float forceLimit = 10f;
        public bool right;

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
            velocity = Vector3.ClampMagnitude(velocity, forceLimit);

            effect.SetVector4(right ? SPHERE_RIGHT : SPHERE_LEFT, new Vector4(transform.position.x, transform.position.y, transform.position.z, Radius));
            effect.SetVector3(right ? VELOCITY_RIGHT : VELOCITY_LEFT, velocity * forceScale);

            m_previousPos = transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}