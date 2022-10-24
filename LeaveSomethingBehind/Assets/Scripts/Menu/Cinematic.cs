using System.Collections.Generic;
using System.Collections;
using RoundTableStudio.Core;
using RoundTableStudio.Input;
using RoundTableStudio.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoundTableStudio.UI {
	public class Cinematic : MonoBehaviour {
		[Header("Text fields")]
		[Tooltip("Text that will appear")]
		[TextArea(3, 10)] public string[] Sentences;
		[Space(10)] 
		[Header("Reference fields")]
		public TextMeshProUGUI CinematicText;

		private Animator _animator;
		private SoundManager _soundManager;
		private Queue<string> _sentences;
		private bool _finished;

		private void Start() {
			_animator = GetComponent<Animator>();
			_sentences = new Queue<string>();
			_soundManager = SoundManager.Instance;

			InputHandler.Instance.Control.Interaction.Escape.performed += i => SkipCinematic();
			
			StartCoroutine(StartCinematic());
		}

		private void SkipCinematic() {
			_soundManager.Stop("IntroductionMusic");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			GameManager.Instance.StartGame();
		}

		private IEnumerator StartCinematic() {
			_sentences.Clear();

			foreach (string sentence in Sentences) {
				_sentences.Enqueue(sentence);
			}

			foreach (string s in _sentences) {
				
				_finished = false;
				DisplayNextSentence(s);
				
				while (!_finished) {
					yield return null;
				}
				
				yield return new WaitForSeconds(1.5f);
			}
			
			EndCinematic();
		}

		private void DisplayNextSentence(string sentence) {
			StartCoroutine(TypeSentence(sentence));
		}

		private IEnumerator TypeSentence(string sentence) {
			CinematicText.text = "";

			foreach (char letter in sentence) {
				CinematicText.text += letter;
				yield return new WaitForSeconds(0.1f);
			}
			
			_finished = true;
		}

		private void EndCinematic() {
			_animator.SetTrigger(Animator.StringToHash("Finish"));
		}

		public IEnumerator OnAnimationFinish(string musicName) {
			Sound.Sound sound = _soundManager.GetSound(musicName);

			while (sound.Source.volume > 0) {
				sound.Source.volume -= 0.01f;
				yield return new WaitForSeconds(0.1f);
			}

			_soundManager.Stop(musicName);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			GameManager.Instance.StartGame();
		}
	}
}
