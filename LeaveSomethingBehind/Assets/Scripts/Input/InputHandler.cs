using UnityEngine;

namespace RoundTableStudio.Input {
	public class InputHandler : MonoBehaviour {
		[HideInInspector]
		public Vector2 Movement;
		[HideInInspector]
		public Vector2 MousePosition;
		[HideInInspector] 
		public bool RangeAttackInput;
		[HideInInspector]
		public bool MagicAttackInput;
		
		private InputActions _control;
		
		private void Awake() {
			if (_control == null) {
				_control = new InputActions();
			}

			_control.Locomotion.Movement.performed += i => Movement = i.ReadValue<Vector2>();
			_control.Locomotion.Mouse.performed += i => MousePosition = i.ReadValue<Vector2>();
		}

		public void TickUpdate() {
			HandleRangeInput();
			HandleMagicInput();
		}

		public void LateTickUpdate() {
			RangeAttackInput = false;
			MagicAttackInput = false;
		}

		private void HandleRangeInput() {
			_control.Interaction.LeftMouse.performed += i => RangeAttackInput = true;
		}

		private void HandleMagicInput() {
			_control.Interaction.RightMouse.performed += i => MagicAttackInput = true;
		}

		private void OnEnable() {
			_control.Enable();
		}

		private void OnDisable() {
			_control.Disable();
		}
	}
	
}
