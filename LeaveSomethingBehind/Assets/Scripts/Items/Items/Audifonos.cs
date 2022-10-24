using UnityEngine;
using RoundTableStudio.Core;

namespace RoundTableStudio.Items {
	[CreateAssetMenu(fileName = "Audifonos", menuName = "Items/Audifonos")]
	public class Audifonos : Item {
		public override void ItemFunction() {
			Debug.Log("No item function");
		}

		public override void ReverseItemFunction() {
			GameManager.Instance.StopMainTheme();
		}
	}
}
