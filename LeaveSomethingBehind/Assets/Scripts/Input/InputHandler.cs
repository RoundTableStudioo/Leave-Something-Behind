using UnityEngine;
using RoundTableStudio.Shared;

namespace RoundTableStudio.Input {
	public class InputHandler : MonoBehaviour {
		public static InputHandler Instance;
		[HideInInspector]
		public Vector2 Movement;
		[HideInInspector] 
		public float MovementAmount;
		[HideInInspector]
		public Vector2 MousePosition;
		[HideInInspector] 
		public bool RangeAttackInput;
		[HideInInspector]
		public bool MagicAttackInput;
		[HideInInspector]
		public bool PauseInput;
		[HideInInspector]
		public InputActions Control;
		
		private void Awake() {
			if (Control == null) {
				Control = new InputActions();
			}

			Control.Locomotion.Movement.performed += i => Movement = i.ReadValue<Vector2>();
			Control.Locomotion.Mouse.performed += i => MousePosition = i.ReadValue<Vector2>();

			if (Instance != null) return;

			Instance = this;
			
			Control.Enable();
		}

		public void TickUpdate() {
			HandleRangeInput();
			HandleMagicInput();
			HandleMovementInput();
		}

		public void LateTickUpdate() {
			RangeAttackInput = false;
			MagicAttackInput = false;
		}

		private void HandleMovementInput() {
			MovementAmount = Mathf.Clamp01(Mathf.Abs(Movement.x) + Mathf.Abs(Movement.y));
		}

		private void HandleRangeInput() {
			Control.Interaction.LeftMouse.performed += i => RangeAttackInput = true;
		}

		private void HandleMagicInput() {
			Control.Interaction.RightMouse.performed += i => MagicAttackInput = true;
		}

		private void OnDisable() {
			Control.Disable();
		}
	}
	
}
