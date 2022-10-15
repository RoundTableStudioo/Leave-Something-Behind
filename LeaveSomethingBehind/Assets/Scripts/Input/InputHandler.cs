using UnityEngine;

namespace RoundTableStudio.Input {
	public class InputHandler : MonoBehaviour {
		[HideInInspector]
		public Vector2 Movement;
		[HideInInspector] 
		public bool AttackInput;
		
		private InputActions _control;
		
		private void Awake() {
			if (_control == null) {
				_control = new InputActions();
			}

			_control.Locomotion.Movement.performed += i => Movement = i.ReadValue<Vector2>();
		}

		public void TickUpdate() {
			HandleAttackInput();
		}

		public void LateTickUpdate() {
			AttackInput = false;
		}

		private void HandleAttackInput() {
			_control.Interaction.Mouse.performed += i => AttackInput = true;
		}

		private void OnEnable() {
			_control.Enable();
		}

		private void OnDisable() {
			_control.Disable();
		}
	}
	
}
