using UnityEngine;
using RoundTableStudio.Items;

namespace RoundTableStudio.UI {
	public class ItemButton : MonoBehaviour {
		public Item ContainedItem;

		private ItemManager _itemManager;
		
		private void Start() {
			_itemManager = ItemManager.Instance;
		}

		public void ChooseItem() {
			_itemManager.DeleteUserItem(ContainedItem);
		}
	}
}
