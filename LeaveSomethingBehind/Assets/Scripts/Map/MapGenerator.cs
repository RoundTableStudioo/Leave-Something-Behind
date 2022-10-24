using RoundTableStudio.Shared;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

namespace RoundTableStudio.Core
{
    public class MapGenerator : MonoBehaviour {
        #region Unity Fields
        
        [Header("Unity Fields")] 
        [Tooltip("Main camera of the scene")]
        public Camera MainCamera;
        [Tooltip("Prefab of the player")] 
        public GameObject Player;
        [Tooltip("Timer of the game")] 
        public Timer Timer;

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
        [Tooltip("Percentage of flowers and rocks around the map")]
        [Range(0, 1)]
        public float TerrainDensity;
        [Tooltip("Percentage of trees around the map")]
        [Range(0, 1)]
        public float TreeDensity;
        [Tooltip("Percentage of props around the map")] 
        [Range(0, 1)]
        public float PropDensity;
        [Tooltip("Percentage of lights around the map")] 
        [Range(0, 1)]
        public float LightsDensity;
        [Tooltip("Percentage of towers around the map")] 
        [Range(0, 1)]
        public float TowersDensity;

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
        public Tile[] TerrainTiles;
        [Tooltip("Tiles of the trees")]
        public Tile[] TreeTiles;
        [Tooltip("Tiles of the props")] 
        public Tile[] PropTiles;
        [Tooltip("Tiles of the light structure")]
        public Tile[] LightTile;
        [Tooltip("Tiles of the tower structure")]
        public Tile[] TowerTile;
        [Tooltip("Tiles of the corruption")]
        public Tile[] CorruptionTiles;

        #endregion

        #region Private Fields

        private Cell[,] _grid;
        [HideInInspector]
        public Vector3Int PlayerCellPosition;

        private int _currentGridHeightUp;
        private int _currentGridHeightDown;
        private int _currentGridWidthRight;
        private int _currentGridWidthLeft;

        private bool _corrupted;
        private float _lastSecond;
        private bool _generating;

        private bool[,] _corruptMap;  

        private List<Vector3Int> _expansionTiles;

        #endregion

        public void OnEnable() {
            GenerateRandomGrid();
            ColorGrid();

            _currentGridHeightUp = GridHeight / 2;
            _currentGridHeightDown = GridHeight / 2;
            
            _currentGridWidthRight = GridWidth / 2;
            _currentGridWidthLeft = GridWidth / 2;
            
            _corrupted = false;

            RespawnPlayer();
            _expansionTiles = new List<Vector3Int>();
            _expansionTiles.Add(new Vector3Int(0, 0, 0));
        }

        private void Update() {
            PlayerCellPosition =  GrassTileMap.WorldToCell(Player.transform.position);
            
            InfiniteGeneration();

            if (_lastSecond != Timer.SecondsCount)
                _corrupted = false;

            if (Timer.SecondsCount % 30 == 0) {
                //NewExpansionTile();
                _corrupted = true;
                _lastSecond = Timer.SecondsCount;
            }

            if(_generating==false) StartCoroutine(CorruptTerrain());

        }

        private Cell GetGridCell(int x, int y) {
            return _grid[x, y];
        }

        private void InfiniteGeneration() {
            if (PlayerCellPosition.y >= _currentGridHeightUp - 3) { // UPPER MAP
                for (int y = 1; y < 3; y++) 
                    for (int x = -_currentGridWidthLeft + 1; x <= _currentGridWidthRight + 1; x++) {
                        Vector3Int pos = new Vector3Int(x, _currentGridHeightUp + y, 0);
                        Vector3Int deletePos = new Vector3Int(x, -_currentGridHeightDown + y, 0);
                        
                        GenerateTerrain(pos, deletePos);
                    }
                _currentGridHeightUp += 2;
                _currentGridHeightDown -= 2;
            } 
            
            if (PlayerCellPosition.y <= -_currentGridHeightDown + 3) { // LOWER MAP
                for(int y = 0; y < 2; y++)
                    for (int x = -_currentGridWidthLeft + 1; x <= _currentGridWidthRight + 1; x++) {
                        Vector3Int pos = new Vector3Int(x, -_currentGridHeightDown - y, 0);
                        Vector3Int deletePos = new Vector3Int(x, _currentGridHeightUp + y, 0);
                        
                        GenerateTerrain(pos, deletePos);
                    }

                _currentGridHeightDown += 2;
                _currentGridHeightUp -= 2;

            }
            
            if (PlayerCellPosition.x >= _currentGridWidthRight - 3) { // RIGHT MAP
                for (int y = -_currentGridHeightDown + 1; y <= _currentGridHeightUp + 1; y++)
                    for (int x = 1; x < 3; x++) {
                        Vector3Int pos = new Vector3Int(_currentGridWidthRight + x, y, 0);
                        Vector3Int deletePos = new Vector3Int(-_currentGridWidthLeft + x, y, 0);
                        
                        GenerateTerrain(pos, deletePos);
                    }

                _currentGridWidthRight += 2;
                _currentGridWidthLeft -= 2;
            }
            
            if (PlayerCellPosition.x <= -_currentGridWidthLeft + 3) { // LEFT MAP
                for (int y = -_currentGridHeightDown + 1; y <= _currentGridHeightUp + 1; y++) 
                    for (int x = 0; x < 2; x++) {
                        Vector3Int pos = new Vector3Int(-_currentGridWidthLeft - x, y, 0);
                        Vector3Int deletePos = new Vector3Int(_currentGridWidthRight + x, y, 0);

                        GenerateTerrain(pos, deletePos);
                    }
                
                _currentGridWidthLeft += 2;
                _currentGridWidthRight -= 2;
            }

 
        }

        private void GenerateTerrain(Vector3Int pos, Vector3Int deletePos) {
            // Generation
            GrassTileMap.SetTile(pos, TerrainTiles[0]);
            
            // Delete
            GrassTileMap.SetTile(deletePos, null);
            DecorationTileMap.SetTile(deletePos, null);
            StructureTileMap.SetTile(deletePos, null);
        }

        private IEnumerator CorruptTerrain() {
            _generating = true;
            int initCount = _expansionTiles.Count;

            for (int i = 0; i < initCount; i++)
            {
                int x = Random.Range(-1, 2);
                int y = 0;
                if (x == 0)
                    y = Random.Range(-1, 2);
                else
                    do
                    {
                        y = Random.Range(-1, 2);
                    } while (y == 0);

                Vector3Int tile = new Vector3Int(_expansionTiles[i].x + x, _expansionTiles[i].y + y, 0);
                GrassTileMap.SetTile(tile, CorruptionTiles[_grid[x, y].numTerrain]);
                Debug.Log("FOR: " + i + " generado  tile en:" + (_expansionTiles[i].x + x) + "," + (_expansionTiles[i].y + y) + "," + "con el sprite: " + _grid[x,y].numTerrain);
                Debug.Log("     A PARTIR DE: " + _expansionTiles[i].x + "," + _expansionTiles[i].y);

                _expansionTiles.RemoveAt(i);
                _expansionTiles.Add(tile);

                yield return new WaitForSeconds(1);
            }
            _generating = false;
        }

        private void NewExpansionTile()
        {
            int x = Random.Range(-6, 7);
            int y = Random.Range(-6, 7);

            _expansionTiles.Add(new Vector3Int(PlayerCellPosition.x + x, PlayerCellPosition.y + y, 0));
        }

        private void GenerateRandomGrid() {
            _grid = new Cell[GridWidth, GridHeight];
            float xOffset = Random.Range(-10000f, 10000f);
            float yOffset = Random.Range(-10000f, 10000f);
            
            for (int y = 0; y < GridHeight; y++) {
                for (int x = 0; x < GridWidth; x++) {
                    Cell cell = new Cell();

                    if (x != 0 && y != 0) {
                        // Checks if the cell below is a tree
                        if (_grid[x, y - 1].IsTreeBottom)
                            cell.IsTreeTop = true;

                        // Checks if the cell below is a light
                        if (_grid[x, y - 1].IsLightBottom)
                            cell.IsLightTop = true;

                        // Checks if the cell below is a tower
                        if (_grid[x, y - 1].IsTowerBottom)
                            cell.IsTowerTop = true;

                        // The cell is not a tree, light or tower
                        if (!cell.IsTreeTop && !cell.IsLightTop && !cell.IsTowerTop) {
                            float flowerNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y * Scale + yOffset);
                            cell.IsTerrain = flowerNoiseValue < TerrainDensity; // Checks if the cell is terrain
                            
                            //Checks if its a tree
                            xOffset = Random.Range(-10000f, 10000f);
                            yOffset = Random.Range(-10000f, 10000f);
                            float treeNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y * Scale * yOffset);
                            cell.IsTreeBottom = treeNoiseValue < TreeDensity;

                            // If is not a tree and is not terrain, checks if its a prop
                            if (!cell.IsTreeBottom) {
                                xOffset = Random.Range(-10000f, 10000f);
                                yOffset = Random.Range(-10000f, 10000f);
                                float propNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y + Scale * yOffset);
                                cell.IsProp = propNoiseValue < PropDensity;
                            }
                            // If is not a prop, terrain or a tree, checks if its a light or a tower
                            if (!cell.IsTreeBottom && !cell.IsProp) {
                                xOffset = Random.Range(-10000f, 10000f);
                                yOffset = Random.Range(-10000f, 10000f);
                                float lightNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y + Scale * yOffset);
                                cell.IsLightBottom = lightNoiseValue < LightsDensity;

                                if (!cell.IsLightBottom) {
                                    xOffset = Random.Range(-10000f, 10000f);
                                    yOffset = Random.Range(-10000f, 10000f);
                                    float towerNoiseValue = Mathf.PerlinNoise(x * Scale + xOffset, y + Scale * yOffset);
                                    cell.IsTowerBottom = towerNoiseValue < TowersDensity;
                                }
                            }
                        }

                        // Checks if the cell is empty
                        if (cell.IsTreeBottom || cell.IsTreeTop || cell.IsProp ||
                            cell.IsLightBottom || cell.IsLightTop || cell.IsTowerBottom || cell.IsTowerTop) {
                            
                            cell.IsEmpty = false;
                        }
                        else cell.IsEmpty = true;
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

                    if (cell.IsTerrain) {
                        
                        int terrainNum = Random.Range(1, TerrainTiles.Length);
                        cell.numTerrain = terrainNum;
                        DecorationTileMap.SetTile(pos, TerrainTiles[terrainNum]);
                        DecorationTileMap.SetTransformMatrix(pos, Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, rotationAmount)));
                        
                    }
                    else if (cell.IsTreeBottom) {
                        if (x != GridWidth - 1 && y != GridHeight - 1) {
                            
                            StructureTileMap.SetTile(pos, TreeTiles[0]);
                            StructureTileMap.SetTile(pos + new Vector3Int(0, 1, 0), TreeTiles[1]);

                        }
                    }
                    else if (cell.IsLightBottom) {
                        if (x != GridWidth - 1 && y != GridHeight - 1) {
                            
                            StructureTileMap.SetTile(pos, LightTile[0]);
                            StructureTileMap.SetTile(pos + new Vector3Int(0, 1, 0), LightTile[1]);

                        }
                    }
                    else if (cell.IsTowerBottom) {
                        if (x != GridWidth - 1 && y != GridHeight - 1) {
                            
                            StructureTileMap.SetTile(pos, TowerTile[0]);
                            StructureTileMap.SetTile(pos + new Vector3Int(0, 1, 0), TowerTile[1]);

                        }
                    }
                    else if (cell.IsProp) {
                        int propNum = Random.Range(0, PropTiles.Length);
                        StructureTileMap.SetTile(pos, PropTiles[propNum]);
                    }

                    GrassTileMap.SetTile(pos, TerrainTiles[0]);
                }
            }
        }
        
        private void RespawnPlayer() {
            bool placed = false;

            while (!placed) {
                // TO DO - Bug: Look [x-1, y-1], [x-1, y+1]... Solve with a while
                int x = Random.Range(GridWidth / 2 - 50, GridWidth / 2 + 50);
                int y = GridHeight / 2;

                if (GetGridCell(x, y).IsEmpty && 
                    GetGridCell(x + 1, y).IsEmpty && GetGridCell(x - 1, y).IsEmpty
                    && GetGridCell(x, y + 1).IsEmpty && GetGridCell(x, y - 1).IsEmpty
                    && GetGridCell(x + 1, y + 1).IsEmpty && GetGridCell(x - 1, y - 1).IsEmpty
                    && GetGridCell(x + 1, y - 1).IsEmpty && GetGridCell(x - 1, y + 1).IsEmpty)
                {
                    Vector3Int pos = new Vector3Int(-x + GridWidth / 2, -y + GridHeight / 2, 0);
                    Player = Instantiate(Player, pos, Quaternion.identity);
                    placed = true;
                }
            }
        }
    }
}
