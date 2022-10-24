using RoundTableStudio.Core;
using UnityEngine;
using RoundTableStudio.Items;
using RoundTableStudio.Sound;
using TMPro;
using UnityEngine.UI;

namespace RoundTableStudio.UI {
	public class ItemButton : MonoBehaviour {
		public Image ItemImage;
		public TextMeshProUGUI NameText;
		public TextMeshProUGUI DescriptionText;
		public TextMeshProUGUI TechnicalDescriptionText;

		[HideInInspector]
		public Item ContainedItem;
		private ItemManager _itemManager;
		private UIManager _ui;

		private void Start() {
			_itemManager = ItemManager.Instance;
			_ui = GetComponentInParent<UIManager>();
		}

		public void ChooseItem() {
			SoundManager.Instance.Play("ObjectSelection");
			_ui.ObjectImages.Find(i => i.sprite == ContainedItem.Icon).color = new Color(1f, 1f, 1f, 0.5f);
			_itemManager.DeleteUserItem(ContainedItem);
			SoundManager.Instance.Stop("TenseMusic");
			SoundManager.Instance.Play("MainTheme");
			GameManager.Instance.SetGameState(GameStates.Started);
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
