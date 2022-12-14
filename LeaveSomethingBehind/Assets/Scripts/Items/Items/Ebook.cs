using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Ebook", menuName = "Items/Ebook")]
	public class Ebook : Item {
		public override void ItemFunction() {
			ItemManager.Instance.Player.Stats.MagicDamage += 0.1f;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.MagicDamage -= 0.1f;
		}
	}
}
