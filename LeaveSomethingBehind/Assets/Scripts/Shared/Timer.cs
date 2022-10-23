using RoundTableStudio.Core;
using UnityEngine;
using TMPro;

namespace RoundTableStudio.Shared {
	public class Timer : MonoBehaviour {
		[Tooltip("Text that represents the timer")]
		public TextMeshProUGUI TimerText;
		[Tooltip("How much time a phase remains")]
		public int MinutesPerPhase = 1;
		
		private float _timer;
		private float _secondsCount;
		[HideInInspector]
		public int MinutesCount;

		private void Update() {
			if (GameManager.Instance.IsGamePaused()) {
				return;
			}
			
			HandleTimer();
			
			if(MinutesCount == GameManager.Instance.GameTime)
				GameManager.Instance.EndGame();
		}

		private void HandleTimer() {
			_timer += Time.deltaTime;
			MinutesCount = Mathf.FloorToInt(_timer / 60);
			_secondsCount = Mathf.FloorToInt(_timer % 60);
			
			TimerText.text = $"{MinutesCount:00}:{_secondsCount:00}";
		}
	}
}
