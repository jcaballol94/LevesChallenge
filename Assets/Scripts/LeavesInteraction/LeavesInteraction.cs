using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace jCaballol94.Leaves
{
    public class LeavesInteraction : MonoBehaviour
    {
        public CharacterInteractionData interaction;
        public bool isRightFoot;

        public struct Data
        {
            public Vector4 shpere;
            public Vector3 force;
        }

        public float Radius => Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        private Vector3 m_previousPos;

        private void Awake()
        {
            if (interaction)
            {
                if (isRightFoot)
                    interaction.rightFoot = this;
                else
                    interaction.leftFoot = this;
            }    
        }
        private void Start()
        {
            m_previousPos = transform.position;
        }

        public Data GetData()
        {
            var dist = transform.position - m_previousPos;
            var velocity = dist / Time.deltaTime;
            m_previousPos = transform.position;

            var data = new Data()
            {
                shpere = new Vector4(transform.position.x, transform.position.y, transform.position.z, Radius),
                force = Vector3.ClampMagnitude(velocity * interaction.forceScale, interaction.forceLimit)
            };

            return data;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}