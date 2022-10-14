using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RoundTableStudio
{

    public class Cell {
        public bool IsRock;
        public bool IsFlower;
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
        public float FlowerDensity;
        [Range(0, 1)] 
        public float RockDensity;
        [Range(0, 1)] 
        public float TreeDensity;

        [Header("Tiles")]
        public Tilemap GrassTileMap;
        public Tilemap DecorationTileMap;
        public Tilemap StructureTileMap;
        public Tile[] GrassTile;
        public Tile[] RockTiles;
        public Tile[] TreeTiles;

        private Cell[,] _grid;

        private void Start() {
            GenerateRandomGrid();
            StartCoroutine(ColorGrid());
        }

        private void GenerateRandomGrid() {
            _grid = new Cell[GridWidth, GridHeight];
            float xOffset = Random.Range(-10000f, 10000f);
            float yOffset = Random.Range(-10000f, 10000f);
            
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {

                    float flowerNoiseValue = Mathf.PerlinNoise(y * Scale + xOffset, x * Scale + yOffset);
                    float rockNoiseValue = Mathf.PerlinNoise(y * Scale + xOffset, x + Scale * yOffset);
                    float treeNoiseValue = Mathf.PerlinNoise(y * Scale + xOffset, x * Scale * yOffset);

                    Cell cell = new Cell();

                    if (x != 0 && y != 0) {
                        if (!_grid[x, y - 1].IsTreeBottom) {
                            cell.IsFlower = flowerNoiseValue < FlowerDensity;

                            if (!cell.IsFlower)
                                cell.IsRock = rockNoiseValue < RockDensity;
                        }
                        if (!cell.IsFlower && !cell.IsRock && !_grid[x, y - 1].IsTreeBottom && !cell.IsTreeTop && !cell.IsTreeBottom)
                            cell.IsTreeBottom = treeNoiseValue < TreeDensity;
                    }

                    _grid[x, y] = cell;
                }
            }
        }

        private IEnumerator ColorGrid() {
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {
                    Cell cell = _grid[x, y];
                    Vector3Int pos = new Vector3Int(-x + GridWidth / 2, -y + GridHeight / 2, 0);
                    
                    int rotationAmount = 0;
                    int rotation = Random.Range(-1, 1);

                    if (rotation == -1) rotationAmount = -90;
                    else if (rotation == 1) rotationAmount = 90;

                    if (cell.IsFlower) {
                        int flowerNum = Random.Range(1, GrassTile.Length - 1);
                        
                        DecorationTileMap.SetTile(pos, GrassTile[flowerNum]);
                        DecorationTileMap.SetTransformMatrix(pos, Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, rotationAmount)));
                    }
                    if (cell.IsRock) {
                        int rockNum = Random.Range(0, RockTiles.Length - 1);

                        DecorationTileMap.SetTile(pos, RockTiles[rockNum]);
                        DecorationTileMap.SetTransformMatrix(pos, Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, rotationAmount)));
                    }
                    if (cell.IsTreeBottom) {
                        if (x != GridWidth - 1 && y != GridHeight - 1) {

                            StructureTileMap.SetTile(pos, TreeTiles[0]);

                            StructureTileMap.SetTile(pos + new Vector3Int(0, 1, 0), TreeTiles[1]);
                            _grid[x, y + 1].IsTreeTop = true;
                        }
                    }
                    
                    GrassTileMap.SetTile(pos, GrassTile[0]);
                    
                    yield return new WaitForSeconds(0.005f);
                }
            }
        }
    }
}
