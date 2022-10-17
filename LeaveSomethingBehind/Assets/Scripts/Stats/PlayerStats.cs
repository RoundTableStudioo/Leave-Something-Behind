using UnityEngine;

namespace RoundTableStudio.Stats
{
    [System.Serializable]
    public class PlayerStats : Stats {
        [Space(10)]
        [Header("Stamina Stats")]
        public int Stamina;
        [Space(10)]
        [Header("Mana Stats")]
        public int Mana;
        public float MagicDamage;
        [Space(10)]
        [Header("Regeneration Stats")]
        public float LifeRegeneration;
        public float ManaRegeneration;
        public float StaminaRegeneration;
    }
}
