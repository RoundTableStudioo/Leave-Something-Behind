using UnityEngine;
using UnityEngine.Tilemaps;

namespace RoundTableStudio {
    public class ProceduralGeneration : MonoBehaviour {

        #region Public Fields
        
        [Header("Numeric Values")]
        [Tooltip("Probability that a cell gets an 1")]
        [Range(0, 100)] public int InitChance;
        [Range(1, 8)] public int BirthLimit;
        [Range(1, 8)] public int DeathLimit;
        [Range(1, 8)] public int NumRepetition;
        public Vector3Int TerrainSize;

        [Space(10)] 
        [Header("References")] 
        public Tilemap TopMap;
        public Tilemap BotMap;
        public Tile TopTile;
        public Tile BotTile;

        #endregion

        #region Private Fields
        
        private int[,] _terrainMap;
        private int _width;
        private int _height;

        #endregion

        #region Unity Events

        private void Update() {
            if(Input.GetMouseButtonDown(0))
                DoSimulation(NumRepetition);
            else if (Input.GetMouseButtonDown(1))
                ClearMap(true);
        }

        #endregion
        
        #region Methods & Functions

        private void DoSimulation(int number) {
            ClearMap(false);
            _width = TerrainSize.x;
            _height = TerrainSize.y;

            if (_terrainMap == null) {
                _terrainMap = new int[_width, _height];
                Init();
            }

            for (int i = 0; i < number; i++)
                _terrainMap = GenerateTilePosition(_terrainMap);

            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++) {
                    if (_terrainMap[i, j] == 1) 
                        TopMap.SetTile(new Vector3Int(-i + _width / 2, -j + _height / 2, 0), TopTile);
                    else
                        BotMap.SetTile(new Vector3Int(-i + _width / 2, -j + _height / 2, 0), BotTile);
                }
        }

        private void Init() {
            for (int i = 0; i < _width; i++) 
                for (int j = 0; j < _height; j++) 
                    _terrainMap[i, j] = Random.Range(1, 101) < InitChance ? 1 : 0;
        }

        private int[,] GenerateTilePosition(int[,] oldMap) {
            int[,] newMap = new int[_width, _height];
            BoundsInt mapBounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

            for (int i = 0; i < _width; i++) {
                for (int j = 0; j < _height; j++) {
                    int neighbour = 0;

                    foreach (Vector3Int bound in mapBounds.allPositionsWithin) {
                        if(bound.x == 0 && bound.y == 0) continue;

                        if (i + bound.x >= 0 && i + bound.x < _width && j + bound.y >= 0 && j + bound.y < _height) 
                            neighbour += oldMap[i + bound.x, j + bound.y];
                        else neighbour++;
                    }

                    if (oldMap[i, j] == 1) {
                        if (neighbour < DeathLimit)
                            newMap[i, j] = 0;
                        else newMap[i, j] = 1;
                    } else {
                        if (neighbour > BirthLimit)
                            newMap[i, j] = 1;
                        else newMap[i, j] = 0;
                    }
                }
            }


            return newMap;
        }

        private void ClearMap(bool complete) {
            TopMap.ClearAllTiles();
            BotMap.ClearAllTiles();
            if (complete) {
                _terrainMap = null;
            }
        }
        
        #endregion

    }
}
