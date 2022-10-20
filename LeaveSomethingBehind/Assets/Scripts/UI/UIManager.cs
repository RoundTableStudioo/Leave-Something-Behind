using System.Collections.Generic;
using RoundTableStudio.Shared;
using RoundTableStudio.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoundTableStudio.UI {
	public class UIManager : MonoBehaviour {
		[Tooltip("Images representing the objects that you have")]
		public List<Image> ObjectImages;
		[Tooltip("Text that represents the timer")]
		public TextMeshProUGUI TimerText;
		[Tooltip("Animator selector")]
		public Animator ItemSelectorAnimator;

		[SerializeField]
		private ItemButton[] _buttons;

		private ItemManager _itemManager;
		private GameStates _gameStates;

		private const int _MINUTES_PER_PHASE = 1;
		private float _timer;
		private float _secondsCount;
		private int _minutesCount;

		private bool _itemPicked;
		private int _lastPickMinute;

		private void Start() {
			_buttons = GetComponentsInChildren<ItemButton>(true);
			_itemManager = ItemManager.Instance;
			_gameStates = GameStates.Instance;
		}

		public void ChangeItemImage(Sprite item, int index) {
			ObjectImages[index].sprite = item;
		}

		private void Update() {
			if (_gameStates.GetPauseState()) return;
			
			HandleTimer();

			if (_minutesCount != _lastPickMinute)
				_itemPicked = false;

			if (_minutesCount % _MINUTES_PER_PHASE == 0 && _minutesCount != 0 && !_itemPicked) {
					HandleItemSelector();
					_itemPicked = true;
					_lastPickMinute = _minutesCount;
			}
		}

		private void HandleTimer() {
			_timer += Time.deltaTime;
			_minutesCount = Mathf.FloorToInt(_timer / 60);
			_secondsCount = Mathf.FloorToInt(_timer % 60);
			
			TimerText.text = $"{_minutesCount:00}:{_secondsCount:00}";
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

			_gameStates.SetPauseState(true);
		}
	}
	
}
