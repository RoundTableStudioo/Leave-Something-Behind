using System.Collections;
using RoundTableStudio.Input;
using UnityEngine;

namespace RoundTableStudio {
	[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
	public class PlayerAnimations : MonoBehaviour {
		private SpriteRenderer _spriteRenderer;
		private Animator _animator;
		private InputHandler _input;
		
		private Vector2 _moveDirection;
		private Vector2 _lastMoveDirection;

		private void OnEnable() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_animator = GetComponent<Animator>();
			_input = GetComponentInParent<InputHandler>();
		}

		public void TickUpdate() {
			Animate();
		}

		public IEnumerator ChangePlayerColor(Color color) {
			_spriteRenderer.color = color;
			yield return new WaitForSeconds(0.2f);
			_spriteRenderer.color = Color.white;
		}

		private void Animate() {
			if (_moveDirection.x != 0 || _moveDirection.y != 0)
				_lastMoveDirection = _moveDirection;

			_moveDirection = new Vector2(_input.Movement.x, _input.Movement.y).normalized;
			
			_animator.SetFloat(Animator.StringToHash("Horizontal"), _input.Movement.x);
			_animator.SetFloat(Animator.StringToHash("Vertical"),  _input.Movement.y);
			_animator.SetFloat(Animator.StringToHash("MoveAmount"), _input.MovementAmount);
			
			_animator.SetFloat(Animator.StringToHash("xDirection"), _lastMoveDirection.x);
			_animator.SetFloat(Animator.StringToHash("yDirection"), _lastMoveDirection.y);
		}
	}
}
