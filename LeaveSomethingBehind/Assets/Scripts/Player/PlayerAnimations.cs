using System.Collections;
using UnityEngine;

namespace RoundTableStudio {
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public class PlayerAnimations : MonoBehaviour {
		private SpriteRenderer _spriteRenderer;
		private Animator _animator;

		private void Start() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_animator = GetComponent<Animator>();
		}

		public IEnumerator ChangePlayerColor(Color color) {
			_spriteRenderer.color = color;
			yield return new WaitForSeconds(0.2f);
			_spriteRenderer.color = Color.white;
		}
	}
}
