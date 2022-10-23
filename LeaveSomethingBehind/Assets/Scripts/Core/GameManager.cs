using RoundTableStudio.Sound;
using UnityEngine;

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

        [Header("References")]
        [Tooltip("Player Prefab")]
        public GameObject Player;
        
        private GridGenerator _map;

        private void Start() {
            _map = GetComponent<GridGenerator>();

            RespawnPlayer();
            SoundManager.Instance.Play("MainTheme");
        }

        private void RespawnPlayer() {
            bool placed = false;

            while (!placed) {
                // TO DO - Bug: Look [x-1, y-1], [x-1, y+1]... Solve with a while
                int x = Random.Range(_map.GridWidth / 2 - 5, _map.GridWidth / 2 + 5);
                int y = _map.GridHeight / 2;

                if (_map.GetGridCell(x, y).IsEmpty && 
                    _map.GetGridCell(x + 1, y).IsEmpty && _map.GetGridCell(x - 1, y).IsEmpty
                    && _map.GetGridCell(x, y + 1).IsEmpty && _map.GetGridCell(x, y - 1).IsEmpty
                    && _map.GetGridCell(x + 1, y + 1).IsEmpty && _map.GetGridCell(x - 1, y - 1).IsEmpty
                    && _map.GetGridCell(x + 1, y - 1).IsEmpty && _map.GetGridCell(x - 1, y + 1).IsEmpty)
                {
                    Vector3Int pos = new Vector3Int(-x + _map.GridWidth / 2, -y + _map.GridHeight / 2, 0);
                    Player = Instantiate(Player, pos, Quaternion.identity);
                    placed = true;
                }
            }
        }

       
    }
}
