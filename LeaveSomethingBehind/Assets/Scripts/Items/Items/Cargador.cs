using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Cargador", menuName = "Items/Cargador")]
	public class Cargador : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Mana.RegenerationCooldown -= 2;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Mana.RegenerationCooldown += 2;
		}
	}
}
