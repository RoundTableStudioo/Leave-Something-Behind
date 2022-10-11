using UnityEngine;

namespace RoundTableStudio
{
    public class Grid : MonoBehaviour {
        
        public int GridSize = 100;
        [Range(0, 1)]
        public float Scale = 0.1f;
        [Range(0, 1)]
        public float WaterLevel = 0.4f;
        
        private Cell[,] _grid;

        private void Start() {
            _grid = new Cell[GridSize, GridSize];
            float xOffset = Random.Range(-10000f, 10000f);
            float yOffset = Random.Range(-10000f, 10000f);

            for (int i = 0; i < GridSize; i++) {
                for (int j = 0; j < GridSize; j++) {
                    float noiseValue = Mathf.PerlinNoise(i * Scale + xOffset, j * Scale + yOffset);

                    Cell cell = new Cell { isWater = noiseValue < WaterLevel };
                    _grid[i, j] = cell;
                }
            }
        }

        private void OnDrawGizmos() {
            if(!Application.isPlaying) return;

            for (int x = 0; x < GridSize; x++) {
                for (int y = 0; y < GridSize; y++) {
                    Cell cell = _grid[x, y];
                    
                    Gizmos.color = cell.isWater ? Color.blue : Color.green;

                    Vector3 pos = new Vector3(x, y, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
