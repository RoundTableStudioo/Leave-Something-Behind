using UnityEngine;

namespace RoundTableStudio.Core {
	public class Cell {
		// If the tile is empty
		public bool IsEmpty;
		
		// Terrain
		public bool IsTerrain;
		// Tree
		public bool IsTreeBottom;
		public bool IsTreeTop;
		// Props
		public bool IsProp;
		// Structures
		public bool IsLightBottom;
		public bool IsLightTop;
		public bool IsTowerBottom;
		public bool IsTowerTop;
	}
	
}
