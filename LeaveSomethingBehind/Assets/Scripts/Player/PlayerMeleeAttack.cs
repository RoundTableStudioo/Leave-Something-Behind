using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoundTableStudio.Player {
	public class PlayerMeleeAttack : MonoBehaviour {

		[Header("Attributes")] 
		[Tooltip("Cost of the melee attack")]
		public float StaminaCost;
		[Tooltip("Force that the enemy will be pushed")]
		public float PushForce;
		
		private PlayerManager _manager;
		private bool _isAttacking;

		private void Start() {
			_manager = GetComponentInParent<PlayerManager>();
		}

		private void TickUpdate() {
			if (EventSystem.current.IsPointerOverGameObject()) return;
			
			if(Input.GetMouseButtonDown(0))
				if (_manager.Stats.RemainingStamina < StaminaCost) {
					Debug.LogWarning("TO DO - Show 'not enough stamina' message");
					return;
				}

			if (!_isAttacking)
				StartCoroutine(Attack());
		}

		private IEnumerator Attack() {
			_manager.Stats.RemainingStamina -= StaminaCost;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
}
