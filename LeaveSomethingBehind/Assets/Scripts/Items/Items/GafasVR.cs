using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "GafasVR", menuName = "Items/GafasVR")]
	public class GafasVR : Item {
		public override void ItemFunction() {
			Debug.LogWarning("TO DO - Show corruption before it appears");
		}

		public override void ReverseItemFunction() {
			Debug.LogWarning("TO DO - Stop seeing the corruption before it appears");
		}
	}
}
