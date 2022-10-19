using System.Collections.Generic;
using UnityEngine;
using RoundTableStudio.Enemies;

namespace RoundTableStudio.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
            
            DontDestroyOnLoad(this);
        }

        #endregion

        public GameObject Player;
        public List<Enemy> Enemies;

        private GridGenerator _map;

        private void Start() {
            _map = GetComponent<GridGenerator>();
            
            _map.GenerateMap();
            RespawnPlayer();
        }

        private void RespawnPlayer() {
            bool placed = false;

            while (!placed) {
                int x = Random.Range(_map.GridWidth / 2 - 5, _map.GridWidth / 2 + 5);
                int y = _map.GridHeight / 2;
                
                if (_map.GetGridPosition(x, y).IsEmpty && 
                    _map.GetGridPosition(x + 1, y).IsEmpty && _map.GetGridPosition(x - 1, y).IsEmpty
                    && _map.GetGridPosition(x, y + 1).IsEmpty && _map.GetGridPosition(x, y - 1).IsEmpty) {
                    Vector3Int pos = new Vector3Int(-x + _map.GridWidth / 2, -y + _map.GridHeight / 2, 0);
                    
                    Instantiate(Player, pos, Quaternion.identity);
                    
                    placed = true;
                }
            }
        }
    }
}
