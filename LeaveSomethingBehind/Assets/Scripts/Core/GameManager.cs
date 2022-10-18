using System.Collections.Generic;
using UnityEngine;
using RoundTableStudio.Player;
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
                int xPosition = Random.Range(0, _map.GridWidth);
                int yPosition = Random.Range(0, _map.GridHeight);

                if (_map.GetGridPosition(xPosition, yPosition).IsEmpty) {
                    Vector3Int pos = new Vector3Int(-xPosition + _map.GridWidth / 2, -yPosition + _map.GridHeight / 2, 0);
                    
                    GameObject instantiatedPlayer =
                        Instantiate(Player, pos, Quaternion.identity);
                    
                    placed = true;
                }
            }
        }
    }
}
