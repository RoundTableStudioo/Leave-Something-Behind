using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Ephone", menuName = "Items/Ephone")]
	public class EPhone : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Attack.ManaCost *= 2;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Attack.ManaCost /= 2;
		}
	}
}
