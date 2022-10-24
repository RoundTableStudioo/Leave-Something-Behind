using System;
using RoundTableStudio.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoundTableStudio.Menu {
	public class End : MonoBehaviour {

		private Animator _animator;

		private void Start() {
			_animator = GetComponent<Animator>();
		}

		public void OnBadEndingButton() {
			_animator.SetTrigger(Animator.StringToHash("BadEnding"));
		}

		public void OnGoodEndingButton() {
			_animator.SetTrigger(Animator.StringToHash("GoodEnding"));
		}

		public void ReturnToMenu() {
			GameManager.Instance.SetGameState(GameStates.Menu);
			SceneManager.LoadScene(0);
		}
	}
}
