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
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			float angle = Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg;

			GameObject arrow = Instantiate(ArrowPrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
			
			rb.AddForce(-FirePoint.up * ArrowSpeed, ForceMode2D.Impulse);
			arrow.transform.eulerAngles = new Vector3(0, 0, angle);
		}
		
		private void Cast() {
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			float angle = Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg;
			
			GameObject magic = Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
			
			rb.AddForce(-FirePoint.up * ProjectileSpeed, ForceMode2D.Impulse);
			magic.transform.eulerAngles = new Vector3(0, 0, angle);
		}
	}
	
}
