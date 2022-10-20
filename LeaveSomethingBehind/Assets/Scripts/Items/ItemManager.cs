using System.Collections.Generic;
using RoundTableStudio.Player;
using UnityEngine;

namespace RoundTableStudio.Items {
    public class ItemManager : MonoBehaviour {
        #region Singleton

        public static ItemManager Instance;
        
        private void Awake() {
            if (Instance != null) return;
                
            Instance = this;
        }

        #endregion
        
        [Header("Lists")]
        [Tooltip("Items that gives buffs to the player")]
        public List<Item> BuffItems;
        [Tooltip("Items that are funny or give an disadvantage to the player")]
        public List<Item> HistoryItems;

        // References
        [HideInInspector]
        public PlayerManager Player;
        // Booleans
        [HideInInspector] 
        public bool ShowEnemyHealth;
        // Const
        private const int _BUFFS_ITEMS_NUMBER = 4;
        private const int _HISTORY_ITEMS_NUMBER = 2;
        // Lists
        private List<Item> _initialBuffItems;
        private List<Item> _initialHistoryItems;
        [SerializeField]
        private List<Item> _userItems;

        private void Start() {
            _initialBuffItems = BuffItems;
            _initialHistoryItems = HistoryItems;
        }

        public void InitializeUserItems() {
            _userItems = new List<Item>();
            
            // Buffs items
            for (int i = 0; i < _BUFFS_ITEMS_NUMBER; i++) {
                int itemNumber = Random.Range(0, BuffItems.Count);
                
                _userItems.Add(BuffItems[itemNumber]);
                BuffItems.RemoveAt(itemNumber);
            }
            
            // History Items
            for (int i = 0; i < _HISTORY_ITEMS_NUMBER; i++) {
                int itemNumber = Random.Range(0, HistoryItems.Count);
                
                _userItems.Add(HistoryItems[itemNumber]);
                HistoryItems.RemoveAt(itemNumber);
            }
        }

        public void ApplyItemFunctions() {
            foreach (Item item in _userItems) {
                item.ItemFunction();
            }
        }

        public void DeleteUserItem(Item item) {
            _userItems.Find(i => i.Name == item.Name).ReverseItemFunction();
            _userItems.Remove(item);
        }

        public void RestartItems() {
            _userItems.Clear();
            BuffItems = _initialBuffItems;
            HistoryItems = _initialHistoryItems;
        }
    }
}