using System.Collections;
using UnityEngine;

namespace RoundTableStudio.Player {
	public enum AttackType {
		Melee,
		Range,
		Magic
	};
	
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
		[SerializeField]
		private AttackType _selectedAttack;
		private bool _isAttacking;
		private Vector3 _mousePosition;
		private Rigidbody2D _rb;

		private void Start() {
			_manager = GetComponentInParent<PlayerManager>();
			_rb = GetComponentInParent<Rigidbody2D>();
		}

		public void TickUpdate() {
			if (!_manager.Input.AttackInput) return;

			if (_selectedAttack == AttackType.Melee) Attack();
			
			if(_selectedAttack == AttackType.Range) Shoot();
			
			if(_selectedAttack == AttackType.Magic) Cast();
		}

		private IEnumerator Attack() {
			yield return new WaitForSeconds(0.5f);
		}
		
		private void Shoot() {
			// Mouse position
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			
			// Direction of the arrow normalized
			Vector2 direction = (_mousePosition - transform.position).normalized;

			// Arrow facing towards the mouse
			Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

			// Creates the arrow
			GameObject arrow = Instantiate(ArrowPrefab, FirePoint.position, rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

			// Gives speed to the arrow
			rb.velocity = direction * ArrowSpeed;
		}
		
		private void Cast() {
			// Mouse position
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			
			// Direction of the spell normalized
			Vector2 direction = (_mousePosition - transform.position).normalized;
			
			// Spell facing towards the mouse
			Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg);
			
			// Creates the magic spell
			GameObject magic = Instantiate(ProjectilePrefab, FirePoint.position, rotation);
			Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();

			rb.velocity = direction * ProjectileSpeed;
		}
	}
	
}
