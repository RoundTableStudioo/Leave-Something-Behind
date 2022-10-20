using UnityEngine;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Android", menuName = "Items/Android")]
	public class Android : Item {
		
		public override void ItemFunction() {
			ItemManager.Instance.ShowEnemyHealth = true;
		}

		public override void ReverseItemFunction() {
			ItemManager.Instance.ShowEnemyHealth = false;
		}
	}
}
