using UnityEngine;
using UnityEngine.Tilemaps;

namespace RoundTableStudio.Core
{
    public class GridGenerator : MonoBehaviour {
        #region Unity Fields
        
        [Header("Unity Fields")] 
        [Tooltip("Main camera of the scene")]
        public Camera MainCamera;

        #endregion

        #region Grid Proprieties

        [Space(10)]
        [Header("Grid Proprieties")]
        [Tooltip("Height of the map")]
        [Range(10, 100)]
        public int GridHeight = 100;
        [Tooltip("Width of the map")]
        [Range(10, 100)]
        public int GridWidth = 100;
        [Tooltip("Scale of the Perlin Noise maps")]
        [Range(0, 1)]
        public float Scale;
        
        #endregion

        #region Density Properties

        
        [Space(10)]
        [Header("Density Properties")]
        [Tooltip("Percentage of flowers around the map")]
        [Range(0, 1)]
        public float FlowerDensity;
        [Tooltip("Percentage of rocks around the map")]
        [Range(0, 1)] 
        public float RockDensity;
        [Tooltip("Percentage of trees around the map")]
        [Range(0, 1)]
        public float TreeDensity;
        [Tooltip("Number of hays")]
        [Range(0, 7)]
        public int HaysNumber;
        [Tooltip("Number of cars")] 
        [Range(0, 7)]
        public int CarsNumber;
        [Tooltip("Number of lights")] 
        [Range(0, 7)]
        public int LightsNumbers;
        [Tooltip("Number of towers")] 
        [Range(0, 5)]
        public int TowersNumber;

        #endregion

        #region TileMaps

        [Space(10)]
        [Header("TileMaps")]
        [Tooltip("Tilemap where the grass will be drawn")]
        public Tilemap GrassTileMap;
        [Tooltip("Tilemap where the decoration tiles will be drawn")]
        public Tilemap DecorationTileMap;
        [Tooltip("Tilemap where the structures will be drawn")]
        public Tilemap StructureTileMap;
        
        #endregion

        #region Tiles

        [Space(10)]
        [Header("Tiles")]
        [Tooltip("Tiles of the grass")]
        public Tile[] GrassTile;
        [Tooltip("Tiles of the rocks")]
        public Tile[] RockTiles;
        [Tooltip("Tiles of the trees")]
        public Tile[] TreeTiles;
        [Tooltip("Tiles of the props")]
        public Tile[] PropsTiles;

        #endregion

        private Cell[,] _grid;

        [SerializeField]
        private float _minX;
        [SerializeField]
        private float _minY;
        [SerializeField]
        private float _maxX;
        [SerializeField]
        private float _maxY;

        public void CalculateCameraBounds() {
            float verticalExtend = MainCamera.orthographicSize;
            float horizontalExtend = verticalExtend * Screen.width / Screen.height;

            _minX = horizontalExtend - GridWidth / 2;
            _maxX = GridWidth / 2 - horizontalExtend;
            _minY = verticalExtend - GridHeight / 2;
            _maxY = GridHeight - verticalExtend / 2;

        }

        public void GenerateMap() {
            CalculateCameraBounds();
            GenerateRandomGrid();
            ColorGrid();
        }

        public Cell GetGridPosition(int x, int y) {
            return _grid[x, y];
        }

        private void InfiniteGeneration() {
            Cell[,] newGrid = new Cell[GridWidth, GridHeight];

            if (true) {
                
            }
        }

        private void GenerateRandomGrid() {
            _grid = new Cell[GridWidth, GridHeight];
            float xOffset = Random.Range(-10000f, 10000f);
            float yOffset = Random.Range(-10000f, 10000f);
            
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {

                    float flowerNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y * Scale + yOffset);
                    float rockNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y + Scale * yOffset);
                    float treeNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y * Scale * yOffset);

                    Cell cell = new Cell();

                    if (x != 0 && y != 0) {
                        if (!_grid[x, y - 1].IsTreeBottom) {
                            cell.IsFlower = flowerNoiseValue < FlowerDensity;

                            if (!cell.IsFlower) {
                                cell.IsRock = rockNoiseValue < RockDensity;
                            }
                        }

                        if (!cell.IsFlower && !cell.IsRock
                                           &&!_grid[x, y - 1].IsTreeBottom && !cell.IsTreeTop && !cell.IsTreeBottom)
                            cell.IsTreeBottom = treeNoiseValue < TreeDensity;

                        if (cell.IsTreeBottom || cell.IsProp)
                            cell.IsEmpty = false;
                        else
                            cell.IsEmpty = true;
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
                }
            }
        }
    }
}
