using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Gafas", menuName = "Items/Gafas")]
	public class Gafas : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.RangeDamage += 2;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.RangeDamage -= 2;
		}
	}
}
