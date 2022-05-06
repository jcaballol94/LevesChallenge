using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.Leaves
{
    [CreateAssetMenu(menuName = "Leaves/Character Interaction Data")]
    public class CharacterInteractionData : ScriptableObject
    {
        [Header("Parameters")]
        [Min(0f)] public float forceScale = 1f;
        [Min(0f)] public float forceLimit = 10f;

        [Header("Objects")]
        public LeavesInteraction rightFoot;
        public LeavesInteraction leftFoot;
    }
}