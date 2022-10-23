using System.Collections.Generic;
using System.Collections;
using RoundTableStudio.Core;
using RoundTableStudio.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoundTableStudio.UI {
	public class IntroductionScreen : MonoBehaviour {
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
			
			StartCoroutine(StartCinematic());
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

		public IEnumerator OnAnimationFinish() {
			Sound.Sound sound = _soundManager.GetSound("IntroductionMusic");

			while (sound.Source.volume > 0) {
				sound.Source.volume -= 0.01f;
				yield return new WaitForSeconds(0.1f);
			}

			_soundManager.Stop("IntroductionMusic");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			GameManager.Instance.StartGame();
		}
	}
}
