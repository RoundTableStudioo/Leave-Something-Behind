using UnityEngine;

namespace RoundTableStudio.Items
{
    public abstract class Item : ScriptableObject {
        [Header("General")]
        [Tooltip("Icon of the item shown at the UI")]
        public Sprite Icon;
        [Tooltip("Name of the item")] 
        public string Name;
        [Tooltip("Description of the item")] 
        [Multiline]
        public string Description;
        [Tooltip("What the item does")]
        public string TechnicalDescription;

        public abstract void ItemFunction();

        public abstract void ReverseItemFunction();
    }
}
