using System.Collections.Generic;
using UnityEngine;

using RoundTableStudio.Enemies;
using RoundTableStudio.Items;
using RoundTableStudio.Player;
using RoundTableStudio.UI;

namespace RoundTableStudio.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
        }

        #endregion

        public GameObject Player;
        public UIManager UI;
        public List<Enemy> Enemies;
        
        private GridGenerator _map;
        private ItemManager _itemManager;

        private void Start() {
            _map = GetComponent<GridGenerator>();
            _itemManager = ItemManager.Instance;

            _map.GenerateMap();
            RespawnPlayer();

            _itemManager.Player = Player.GetComponent<PlayerManager>();
            
            _itemManager.InitializeUserItems();
            _itemManager.ApplyItemFunctions();
            
            UI.HandleItemsImages();
        }

        private void RespawnPlayer() {
            bool placed = false;

            while (!placed) {
                // TO DO - Bug: Look [x-1, y-1], [x-1, y+1]... Solve with a while
                int x = Random.Range(_map.GridWidth / 2 - 5, _map.GridWidth / 2 + 5);
                int y = _map.GridHeight / 2;

                if (_map.GetGridCell(x, y).IsEmpty && 
                    _map.GetGridCell(x + 1, y).IsEmpty && _map.GetGridCell(x - 1, y).IsEmpty
                    && _map.GetGridCell(x, y + 1).IsEmpty && _map.GetGridCell(x, y - 1).IsEmpty) 
                {
                    Vector3Int pos = new Vector3Int(-x + _map.GridWidth / 2, -y + _map.GridHeight / 2, 0);
                    Player = Instantiate(Player, pos, Quaternion.identity);
                    placed = true;
                }
            }
        }

        private void RespawnEnemies() {
            // TO DO
        }
    }
}
