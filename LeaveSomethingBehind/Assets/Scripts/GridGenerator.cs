using UnityEngine;
using UnityEngine.Tilemaps;

namespace RoundTableStudio
{
    public class Cell {
        public bool IsBush;
        public bool IsTreeBottom;
        public bool IsTreeTop;
    }
    
    public class GridGenerator : MonoBehaviour {
        
        [Header("Grid Proprieties")]
        public int GridHeight = 100;
        public int GridWidth = 100;
        [Range(0, 1)]
        public float Scale;
        [Range(0, 1)]
        public float BushDensity;
        [Range(0, 1)] 
        public float TreeDensity;

        [Header("Tiles")] 
        public Tilemap BushTileMap;
        public Tilemap GrassTileMap;
        public Tilemap TreeTileMap;
        public Tile GrassTile;
        public Tile BushTile;
        public Tile[] TreeTiles;

        private Cell[,] _grid;

        private void Start() {
            GenerateRandomGrid();
            ColorGrid();
        }

        private void GenerateRandomGrid() {
            _grid = new Cell[GridWidth, GridHeight];
            float xOffset = Random.Range(-10000f, 10000f);
            float yOffset = Random.Range(-10000f, 10000f);
            
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {

                    float bushNoiseValue = Mathf.PerlinNoise(y * Scale + xOffset, x * Scale + yOffset);
                    float treeNoiseValue = Mathf.PerlinNoise(y * Scale + xOffset, x * Scale * yOffset);

                    Cell cell = new Cell();

                    if (x != 0 && y != 0) {
                        if (!_grid[x, y - 1].IsTreeBottom)
                            cell.IsBush = bushNoiseValue < BushDensity;
                        if (!cell.IsBush && !cell.IsTreeTop && !cell.IsTreeBottom && !_grid[x, y - 1].IsTreeBottom)
                            cell.IsTreeBottom = treeNoiseValue < TreeDensity;
                    }
                    
                    

                    _grid[x, y] = cell;
                    
                }
            }
        }

        private void ColorGrid() {
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {
                    Cell cell = _grid[x, y];
                    Vector3Int pos = new Vector3Int(-x + GridWidth / 2, -y + GridHeight / 2, 0);

                    if (cell.IsBush) 
                        BushTileMap.SetTile(pos, BushTile);
                    if (cell.IsTreeBottom) {
                        if (x != GridWidth - 1 && y != GridHeight - 1) {

                            TreeTileMap.SetTile(pos, TreeTiles[0]);

                            TreeTileMap.SetTile(pos + new Vector3Int(0, 1, 0), TreeTiles[1]);
                            _grid[x, y + 1].IsTreeTop = true;
                        }
                    }
                    
                    GrassTileMap.SetTile(pos, GrassTile);
                }
            }
        }
    }
}
