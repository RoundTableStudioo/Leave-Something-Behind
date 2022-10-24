using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "CosplayMago", menuName = "Items/CosplayMago")]
	public class CosplayMago : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.MagicDefense += 0.1f;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.MagicDefense -= 0.1f;
		}
	}
}
