using RoundTableStudio.Core;
using UnityEngine;
using TMPro;

namespace RoundTableStudio.Shared {
	public class Timer : MonoBehaviour {
		[Tooltip("Text that represents the timer")]
		public TextMeshProUGUI TimerText;
		[Tooltip("How much time a phase remains")]
		public float MinutesPerPhase = 2;
		[Tooltip("Hoy much seconds a phase remains")]
		public int SecondsPerPhase = 30;
		
		private float _timer;
		[HideInInspector]
		public float SecondsCount;
		[HideInInspector]
		public int MinutesCount;

		private void Update() {
			if (GameManager.Instance.IsGamePaused() || GameManager.Instance.IsGameEnded()) {
				return;
			}
			
			HandleTimer();
			
			if(MinutesCount == GameManager.Instance.GameTime)
				GameManager.Instance.WinGame();
		}

		private void HandleTimer() {
			_timer += Time.deltaTime;
			MinutesCount = Mathf.FloorToInt(_timer / 60);
			SecondsCount = Mathf.FloorToInt(_timer % 60);
			
			TimerText.text = $"{MinutesCount:00}:{SecondsCount:00}";
		}
	}
}
