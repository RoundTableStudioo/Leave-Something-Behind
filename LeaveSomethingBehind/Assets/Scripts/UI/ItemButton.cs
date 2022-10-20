using UnityEngine;
using RoundTableStudio.Items;
using TMPro;
using UnityEngine.UI;

namespace RoundTableStudio.UI {
	public class ItemButton : MonoBehaviour {
		public Item ContainedItem;
		public Image ItemImage;
		public TextMeshProUGUI NameText;
		public TextMeshProUGUI DescriptionText;
		public TextMeshProUGUI TechnicalDescriptionText;

		private ItemManager _itemManager;
		private UIManager _uiManager;
		
		private void Start() {
			_itemManager = ItemManager.Instance;
		}

		public void ChooseItem() {
			_itemManager.DeleteUserItem(ContainedItem);
			_uiManager.PhaseNumber++;
		}

		public void SetContainedItem(Item item) {
			ItemImage.sprite = item.Icon;
			NameText.text = item.Name;
			DescriptionText.text = item.Description;
			TechnicalDescriptionText.text = item.TechnicalDescription;
			
			ContainedItem = item;
		}
	}
}
