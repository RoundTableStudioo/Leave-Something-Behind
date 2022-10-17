using System.Collections;
using UnityEngine;

namespace RoundTableStudio.Player {
	public class PlayerAttack : MonoBehaviour {

		[Header("Melee Attributes")] 
		[Tooltip("Cost of the melee attack")]
		public float MeleeStaminaCost;
		[Tooltip("Force that the enemy will be pushed")]
		public float PushForce;
		
		[Space(10)]
		[Header("Range Attributes")]
		[Tooltip("Place where the arrow or the magic projectile will be shot")]
		public Transform FirePoint;
		[Tooltip("Prefab of the arrow that will be shot")]
		public GameObject ArrowPrefab;
		[Tooltip("Cost of the range attack")]
		public float RangeStaminaCost;
		[Tooltip("Speed of the arrow")]
		public float ArrowSpeed;
		
		[Space(10)]
		[Header("Magic Attributes")]
		[Tooltip("Prefab of the magic projectile")]
		public GameObject ProjectilePrefab;
		[Tooltip("Cost of the magic attack")]
		public float ManaCost;
		[Tooltip("Speed of the magic projectile")]
		public float ProjectileSpeed;
		
		private PlayerManager _manager;
		private bool _isAttacking;

		private void Start() {
			_manager = GetComponentInParent<PlayerManager>();
		}

		public void TickUpdate() {
			if (!_manager.Input.AttackInput) return;
			
			// TO DO - Chose between the attacks
			
			if (_manager.Mana.CurrentMana < MeleeStaminaCost) {
				Debug.LogWarning("TO DO - Show 'not enough mana' message");
				return;
			}

			if (!_isAttacking)
				StartCoroutine(Attack());
		}

		private IEnumerator Attack() {
			yield return new WaitForSeconds(0.5f);
		}
		
		public void Shoot() {
			GameObject arrow = Instantiate(ArrowPrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
			
			rb.AddForce(FirePoint.up * ArrowSpeed, ForceMode2D.Impulse);
		}
		
		public void Cast() {
			GameObject arrow = Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
			
			rb.AddForce(FirePoint.up * ProjectileSpeed, ForceMode2D.Impulse);
		}
	}
	
}
