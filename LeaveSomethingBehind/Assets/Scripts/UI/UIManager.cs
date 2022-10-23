using System.Collections.Generic;
using RoundTableStudio.Core;
using RoundTableStudio.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RoundTableStudio.Shared;
using RoundTableStudio.Items;

namespace RoundTableStudio.UI {
	public class UIManager : MonoBehaviour {
		[Header("Images & Text")]
		[Tooltip("Images representing the objects that you have")]
		public List<Image> ObjectImages;

		[Space(10)]
		[Header("Unity References")]
		[Tooltip("Animator selector")]
		public Animator ItemSelectorAnimator;
		[Tooltip("Pause Menu Animator")] 
		public Animator PauseMenuAnimator;
		[Tooltip("Game timer")] 
		public Timer Timer;

		private ItemButton[] _buttons;
		private ItemManager _itemManager;
		private GameManager _gameManager;

		private bool _itemPicked;
		private int _lastPickMinute;

		private bool _onPause;

		private void Start() {
			_buttons = GetComponentsInChildren<ItemButton>(true);
			_itemManager = ItemManager.Instance;
			_gameManager = GameManager.Instance;
			
			InputHandler.Instance.Control.Interaction.Escape.performed += i => HandlePauseMenu();
			_onPause = false;
			
			HandleItemsImages();
		}

		private void Update() {
			if (Timer.MinutesCount != _lastPickMinute)
				_itemPicked = false;

			if (Timer.MinutesCount % Timer.MinutesPerPhase == 0 && Timer.MinutesCount != 0 && !_itemPicked) {
					HandleItemSelector();
					_itemPicked = true;
					_lastPickMinute = Timer.MinutesCount;
			}
		}
		
		private void ChangeItemImage(Sprite item, int index) {
			ObjectImages[index].sprite = item;
		}

		public void HandlePauseMenu() {
			if (_onPause) {
				PauseMenuAnimator.SetBool(Animator.StringToHash("Pause"), false);
				_onPause = false;
				_gameManager.SetGameState(GameStates.Started);
			}
			else {
				PauseMenuAnimator.SetBool(Animator.StringToHash("Pause"), true);
				_onPause = true;
				_gameManager.SetGameState(GameStates.Paused);
			}

			
		}

		private void HandleItemsImages() {
			for (int i = 0; i < _itemManager.UserItems.Count; i++) {
				ChangeItemImage(_itemManager.UserItems[i].Icon, i);
			}
		}

		private void HandleItemSelector() {
			ItemSelectorAnimator.SetTrigger(Animator.StringToHash("Open"));

			int first, second;

			do {
				first = Random.Range(0, _itemManager.UserItems.Count - 1);
				second = Random.Range(0, _itemManager.UserItems.Count - 1);
			} while (first == second);

			_buttons[0].SetContainedItem(_itemManager.UserItems[first]);
			_buttons[1].SetContainedItem(_itemManager.UserItems[second]);

			_gameManager.SetGameState(GameStates.Paused);
		}
	}
	
}
