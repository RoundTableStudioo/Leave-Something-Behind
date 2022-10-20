using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "CascoObra", menuName = "Items/CascoObra")]
	public class CascoObra : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.PhysicalDefense += 1;
			ItemManager.Instance.Player.Stats.RangeDefense += 1;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.PhysicalDefense -= 1;
			ItemManager.Instance.Player.Stats.RangeDefense -= 1;
		}
	}
}