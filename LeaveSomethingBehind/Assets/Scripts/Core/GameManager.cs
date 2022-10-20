using System.Collections.Generic;
using UnityEngine;

using RoundTableStudio.Enemies;
using RoundTableStudio.Items;
using RoundTableStudio.Player;

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
        public List<Enemy> Enemies;

        [HideInInspector] 
        public bool GameStarted;
        [HideInInspector] 
        public bool GameEnded;
        
        private bool _gamePause;

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
        }

        private void RespawnPlayer() {
            bool placed = false;

            while (!placed) {
                // TO DO - Bug: Look [x-1, y-1], [x-1, y+1]... Solve with a while
                int x = Random.Range(_map.GridWidth / 2 - 5, _map.GridWidth / 2 + 5);
                int y = _map.GridHeight / 2;
                int i = -1;

                if (_map.GetGridPosition(x, y).IsEmpty && 
                    _map.GetGridPosition(x + 1, y).IsEmpty && _map.GetGridPosition(x - 1, y).IsEmpty
                    && _map.GetGridPosition(x, y + 1).IsEmpty && _map.GetGridPosition(x, y - 1).IsEmpty) 
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

        public void SetPauseState(bool pauseState) {
            _gamePause = pauseState;
        }

        public bool GetPauseState() {
            return _gamePause;
        }
    }
}
