using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoundTableStudio.UI {
	public class UIManager : MonoBehaviour {
		public List<Image> ObjectImages;
		public TextMeshProUGUI TimerText;

		private float _timer;
		private float _secondsCount;
		private float _minutesCount;

		public void ChangeItemImage(Sprite item, int index) {
			ObjectImages[index].sprite = item;
		}

		private void Update() {
			HandleTimer();
		}

		private void HandleTimer() {
			_timer += Time.deltaTime;
			_minutesCount = Mathf.FloorToInt(_timer / 60);
			_secondsCount = Mathf.FloorToInt(_timer % 60);
			
			TimerText.text = $"{_minutesCount:00}:{_secondsCount:00}";
		}
	}
	
}
