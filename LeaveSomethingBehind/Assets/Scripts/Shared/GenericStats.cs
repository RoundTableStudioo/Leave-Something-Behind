using UnityEngine;

namespace RoundTableStudio.Shared
{
    [System.Serializable]
    public class GenericStats {
        [Header("Main Stats")]
        [Tooltip("Max health points")]
        public float MaxHp;
        [Tooltip("Total damage dealt")]
        public float Damage;
        [Tooltip("Amount of speed")]
        public float Speed;
    }
}
