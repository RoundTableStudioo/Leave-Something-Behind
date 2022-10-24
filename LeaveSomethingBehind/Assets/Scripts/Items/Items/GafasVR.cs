using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "GafasVR", menuName = "Items/GafasVR")]
	public class GafasVR : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.OrcDefense += 0.1f;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.OrcDefense -= 0.1f;
		}
	}
}
