using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoundTableStudio.Items {
    public class ItemManager : MonoBehaviour {
        [Header("Lists")]
        [Tooltip("Items that gives buffs to the player")]
        public List<Item> BuffItems;
        [Tooltip("Items that are funny or give an disadvantage to the player")]
        public List<Item> HistoryItems;
        
        private const int _BUFFS_ITEMS_NUMBER = 6;
        private const int _HISTORY_ITEMS_NUMBER = 2;
        private List<Item> _initialBuffItems;
        private List<Item> _initialHistoryItems;
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

        public void RestartItems() {
            _userItems.Clear();
            BuffItems = _initialBuffItems;
            HistoryItems = _initialHistoryItems;
        }
    }
}
