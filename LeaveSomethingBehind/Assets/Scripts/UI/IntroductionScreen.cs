using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

namespace RoundTableStudio.UI {
	public class IntroductionScreen : MonoBehaviour {
		[Header("Text fields")]
		[Tooltip("Text that will appear")]
		[TextArea(3, 10)] public string[] Sentences;
		[Space(10)] 
		[Header("Reference fields")]
		public TextMeshProUGUI CinematicText;

		private Queue<string> _sentences;

		private void Start() {
			_sentences = new Queue<string>();
			
			StartCoroutine(StartCinematic());
		}

		private IEnumerator StartCinematic() {
			_sentences.Clear();

			foreach (string sentence in Sentences) {
				_sentences.Enqueue(sentence);
			}

			foreach (string s in _sentences) {
				DisplayNextSentence();
				yield return new WaitForSeconds(5f);
			}
			
			EndCinematic();
		}

		private void DisplayNextSentence() {
			string sentence = _sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		private IEnumerator TypeSentence(string sentence) {
			CinematicText.text = "";

			foreach (char letter in sentence) {
				CinematicText.text += letter;
				yield return new WaitForSeconds(0.1f);
			}
		}

		private void EndCinematic() {
			Debug.Log("Cinematic ended");
		}
	}
}
