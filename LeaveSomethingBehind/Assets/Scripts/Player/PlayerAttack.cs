using System.Collections;
using UnityEngine;
using RoundTableStudio.Shared;

namespace RoundTableStudio.Player {
	public class PlayerAttack : MonoBehaviour {
		[Header("Prefabs")]
		[Tooltip("Prefab of the arrow that will be shot")]
		public GameObject ArrowPrefab;
		[Tooltip("Prefab of the magic projectile")]
		public GameObject ProjectilePrefab;
		[Tooltip("Place where the arrow or the magic projectile will be shot")]
		public Transform FirePoint;
		
		[Space(10)]
		[Header("Range Attributes")]
		[Tooltip("Cost of the range attack")]
		public float RangeStaminaCost;
		[Tooltip("Cooldown of the range attack")]
		public float RangeCooldown;

		[Space(10)]
		[Header("Magic Attributes")]
		[Tooltip("Cost of the magic attack")]
		public float ManaCost;
		[Tooltip("Cooldown of the magic attack")]
		public float MagicCooldown;

		// References
		private PlayerManager _manager;
		private Rigidbody2D _rb;
		
		// Numeric values
		private Vector3 _mousePosition;
		private float _rangeCooldownTick;
		private float _magicCooldownTick;
		
		// Booleans
		private bool _isAttacking;

		private void OnEnable() {
			_manager = GetComponentInParent<PlayerManager>();
			_rb = GetComponentInParent<Rigidbody2D>();
		}

		public void TickUpdate() {
			if (_rangeCooldownTick > 0)
				_rangeCooldownTick -= Time.deltaTime;

			if (_magicCooldownTick > 0)
				_magicCooldownTick -= Time.deltaTime;
			
			if (_manager.Input.RangeAttackInput && _rangeCooldownTick <= 0) {
				if(_manager.Stamina.UseStamina(2)) {
					Shoot();
					_rangeCooldownTick = RangeCooldown;
				}
			}

			if (_manager.Input.MagicAttackInput && _magicCooldownTick <= 0) {
				if(_manager.Mana.UseMana(3)) {
					Cast();
					_magicCooldownTick = MagicCooldown;
				}
			}
		}

		private void Shoot() {
			// Mouse position
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			
			// Direction of the arrow normalized
			Vector2 direction = _mousePosition - transform.position;

			// Arrow facing towards the mouse
			Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

			// Creates the arrow
			GameObject arrowGo = Instantiate(ArrowPrefab, FirePoint.position, rotation);
			Projectile arrow = arrowGo.GetComponent<Projectile>();

			// Gives speed to the arrow
			arrow.Rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * arrow.Speed;
		}
		
		private void Cast() {
			// Mouse position
			_mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
			
			// Direction of the spell normalized
			Vector2 direction = _mousePosition - transform.position;
			
			// Spell facing towards the mouse
			Quaternion rotation = Quaternion.Euler(0, 0, 
				Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg);
			
			// Creates the magic spell
			GameObject magicGo = Instantiate(ProjectilePrefab, FirePoint.position, rotation);
			Projectile magic = magicGo.GetComponent<Projectile>();

			// Gives speed to the projectile
			magic.Rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * magic.Speed;
		}
	}
	
}
