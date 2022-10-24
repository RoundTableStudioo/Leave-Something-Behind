using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "BotasVaquero", menuName = "Items/BotasVaquero")]
	public class BotasVaquero : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.GoblinDefense += 0.1f;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.GoblinDefense -= 0.1f;
		}
	}
}
