using UnityEngine;

namespace RoundTableStudio.Stats
{
    [System.Serializable]
    public class PlayerStats : Stats {
        [Space(10)]
        [Header("Stamina Stats")]
        public float Stamina;
        public float RemainingStamina;
        [Space(10)]
        [Header("Mana Stats")]
        public float Mana;
        public float RemainingMana;
        public float MagicDamage;
        [Space(10)]
        [Header("Regeneration Stats")]
        public float LifeRegeneration;
        public float ManaRegeneration;
        public float StaminaRegeneration;
    }
}
