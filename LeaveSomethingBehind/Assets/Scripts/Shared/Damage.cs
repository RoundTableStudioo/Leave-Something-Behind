using UnityEngine;

namespace RoundTableStudio.Shared
{
    [System.Serializable]
    public struct Damage {
        [Tooltip("Total amount of damage that it will be dealt")]
        public float Amount;
        [Tooltip("Where are the objective going to be pushed")]
        [HideInInspector]
        public Vector3 PushOrigin;
        [Tooltip("Force that the attack will push it objective")]
        public float PushForce;
    }
}
