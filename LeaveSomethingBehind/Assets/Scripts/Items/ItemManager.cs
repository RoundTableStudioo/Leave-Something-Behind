using System;
using System.Collections.Generic;
using RoundTableStudio.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoundTableStudio.Items {
    public class ItemManager : MonoBehaviour {
        #region Singleton && Initalization

        public static ItemManager Instance;
        
        private List<Item> _initialItems;
        
        private void OnEnable() {
            if (Instance != null) return;
                
            Instance = this;
            
            _initialItems = Items;

            InitializeUserItems();
        }

        #endregion
        
        [Header("Lists")]
        [Tooltip("Items that gives buffs to the player")]
        public List<Item> Items;

        // References
        [HideInInspector]
        public PlayerManager Player;
        // Booleans
        [HideInInspector] 
        public bool ShowEnemyHealth;
        // Const
        private const int _ITEMS_NUMBER = 6;
        // Lists
        public List<Item> UserItems;
        
        private void Start() {
            Player = FindObjectOfType<PlayerManager>();
            
            ApplyItemFunctions();
        }

        private void InitializeUserItems() {
            UserItems = new List<Item>();
            
            // Items
            for (int i = 0; i < _ITEMS_NUMBER; i++) {
                int itemNumber = Random.Range(0, Items.Count);
                
                UserItems.Add(Items[itemNumber]);
                Items.RemoveAt(itemNumber);
            }
        }

        private void ApplyItemFunctions() {
            foreach (Item item in UserItems) {
                item.ItemFunction();
            }
        }

        public void DeleteUserItem(Item item) {
            UserItems.Find(i => i == item).ReverseItemFunction();
            UserItems.Remove(item);
        }

        public void RestartItems() {
            UserItems.Clear();
            Items = _initialItems;
        }
    }
}
