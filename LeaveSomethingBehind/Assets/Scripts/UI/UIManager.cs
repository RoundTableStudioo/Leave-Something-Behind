using System.Collections.Generic;
using RoundTableStudio.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoundTableStudio.UI {
	public class UIManager : MonoBehaviour {
		public List<Image> ObjectImages;
		public TextMeshProUGUI TimerText;
		public Animator ItemSelectorAnimator;

		private float _timer;
		private float _secondsCount;
		private float _minutesCount;

		public void ChangeItemImage(Sprite item, int index) {
			ObjectImages[index].sprite = item;
		}

		private void Update() {
			if (GameManager.Instance.GetPauseState()) return;
			
			HandleTimer();

			if ((_minutesCount + 1) % 2 == 0) {
				ItemSelectorAnimator.SetTrigger(Animator.StringToHash("Open"));
				
				GameManager.Instance.SetPauseState(true);
			}
		}

		private void HandleTimer() {
			_timer += Time.deltaTime;
			_minutesCount = Mathf.FloorToInt(_timer / 60);
			_secondsCount = Mathf.FloorToInt(_timer % 60);
			
			TimerText.text = $"{_minutesCount:00}:{_secondsCount:00}";
		}
	}
	
}
