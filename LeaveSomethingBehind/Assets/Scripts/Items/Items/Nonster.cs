using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Noster", menuName = "Items/Noster")]
	public class Nonster : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stamina.RegenerationCooldown -= 1;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stamina.RegenerationCooldown += 1;
		}
	}
}
