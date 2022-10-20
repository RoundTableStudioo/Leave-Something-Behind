using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "CafeCatbucks", menuName = "Items/CafeCatbucks")]
	public class CafeCatbucks : Item {
		public override void ItemFunction() {
			float regeneration = ItemManager.Instance.Player.Stats.MaxHp * 0.25f;
			
			ItemManager.Instance.Player.Stats.LifeRegeneration += regeneration;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.Player.Stats.LifeRegeneration = 0;
		}
	}
}