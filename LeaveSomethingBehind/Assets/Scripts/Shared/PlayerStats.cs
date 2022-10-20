using UnityEngine;

namespace RoundTableStudio.Shared
{
    [System.Serializable]
    public class PlayerStats : GenericStats {
        [Space(10)]
        [Header("Defense Stats")]
        [Tooltip("Damage reduction from physical attacks")]
        public int PhysicalDefense;
        [Tooltip("Damage reduction from projectiles")]
        public int RangeDefense;
        [Tooltip("Damage reduction from magic attacks")]
        public int MagicDefense;
        [Tooltip("Damage reduction against human units")]
        public int HumanDefense;
        [Tooltip("Damage reduction against elf units")]
        public int GoblinDefense;
            
        [Space(10)]
        [Header("Stamina Stats")]
        [Tooltip("Total stamina of the player")]
        public int Stamina;
        [Tooltip("Amount of range damage which the player will do")]
        public float RangeDamage;
        
        [Space(10)]
        [Header("Mana Stats")]
        [Tooltip("Total mana of the player")]
        public int Mana;
        [Tooltip("Amount of magic damage which the player will do")]
        public float MagicDamage;
        
        [Space(10)]
        [Header("Regeneration Stats")]
        [Tooltip("Life regeneration of the player")]
        public float LifeRegeneration;
        [Tooltip("Mana regeneration of the player")]
        public float ManaRegeneration;
        [Tooltip("Stamina regeneration of the player")]
        public float StaminaRegeneration;
    }
}
