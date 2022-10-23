using System.Collections;
using RoundTableStudio.Core;
using UnityEngine;
using RoundTableStudio.Shared;
using UnityEngine.UI;

namespace RoundTableStudio.Enemies {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Enemy : MonoBehaviour {
		[Header("Enemy stats")]
		[Tooltip("All the stats of the enemy")]
		public GenericStats Stats;
		[Tooltip("Force that will push the player")]
		public float PushForce;

		[Space(10)] 
		[Header("Drops prefabs")]
		[Tooltip("Mana potion game object")]
		public GameObject ManaPotion;
		[Tooltip("Life potion game object")] 
		public GameObject LifePotion;

		private Rigidbody2D _rb;
		private SpriteRenderer _spriteRenderer;

		private Image _healthBar;

		private float _immuneTime;
		private float _lastImmune;
		
		private Transform _player;
		private Vector3 _pushDirection;
		private Vector2 _movement;

		private bool _damaged;
		
		private float _currentHp;

		private const float _DROP_PROBABILITY = 0.08f;
		private const float _MANA_DROP_PROBABILITY = 0.65f;

		private void OnEnable() {
			_rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			if (GameObject.FindGameObjectWithTag("Player").transform != null)
				_player = GameObject.FindGameObjectWithTag("Player").transform;

			_currentHp = Stats.MaxHp;
		}

		private void FixedUpdate() {
			if (GameManager.Instance.IsGamePaused()) return;
			
			FollowPlayer();
		}

		private void TakeDamage(Damage damage) {
			if (Time.time - _lastImmune <= _immuneTime) return;

			_lastImmune = Time.time;
			_currentHp -= damage.Amount;
			_pushDirection = (transform.position - damage.PushOrigin).normalized * damage.PushForce;

			StartCoroutine(ChangeColor());
			_damaged = true;

			if (_currentHp <= 0) {
				Die();
			}

		}

		private void FollowPlayer() {

			Vector3 objective = Stats.Speed * Time.fixedDeltaTime * (_player.position - transform.position).normalized;
			
			if(!_damaged)
				_rb.MovePosition(transform.position + objective);
			else {
				_rb.MovePosition(transform.position + (_pushDirection * Time.fixedDeltaTime));
				_damaged = false;
			}
		}

		private void Die() {
			HandleEnemyDrop();
			Destroy(gameObject);
		}

		private void HandleEnemyDrop() {
			float dropProbability = Random.Range(0, 1);

			if (!(dropProbability <= _DROP_PROBABILITY)) return;
			
			float potionProbability = Random.Range(0, 1);

			if (potionProbability <= _MANA_DROP_PROBABILITY)
				DropManaPotion();
			else DropLifePotion();

		}

		private void DropManaPotion() {
			Instantiate(ManaPotion, transform.position, Quaternion.identity);
		}

		private void DropLifePotion() {
			Instantiate(LifePotion, transform.position, Quaternion.identity);
		}

		private IEnumerator ChangeColor() {
			_spriteRenderer.color = Color.red;
			yield return new WaitForSeconds(0.2f);
			_spriteRenderer.color = Color.white;
		}

		private void OnCollisionStay2D(Collision2D col) {
			if (!col.collider.CompareTag("Player")) return;

			Damage damage = new Damage { Amount = Stats.Damage, PushOrigin = transform.position, PushForce = PushForce };

			col.collider.SendMessage("TakeDamage", damage);
			
		}
	}
}
