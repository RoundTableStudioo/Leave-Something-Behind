using System.Collections;
using UnityEngine;
using RoundTableStudio.Shared;

namespace RoundTableStudio.Enemies {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Enemy : MonoBehaviour {
		public GenericStats Stats;
		public float PushForce;

		private Rigidbody2D _rb;
		private SpriteRenderer _spriteRenderer;

		private float _immuneTime;
		private float _lastImmune;
		
		private Vector3 _playerPosition;
		private Vector3 _pushDirection;
		private Vector2 _movement;

		private bool _damaged;
		
		private float _currentHp;

		private void Start() {
			_rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			
			if (GameObject.FindGameObjectWithTag("Player").transform != null)
				_playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

			_currentHp = Stats.MaxHp;
		}

		private void TakeDamage(Damage damage) {
			if (Time.time - _lastImmune <= _immuneTime) return;

			_lastImmune = Time.time;
			_currentHp -= damage.Amount;
			_pushDirection = (transform.position - damage.PushOrigin).normalized * damage.PushForce;

			StartCoroutine(ChangeColor());
			_damaged = true;

			if(_currentHp <= 0)
				Die();
		}

		private void FollowPlayer(Transform playerPosition) {

			Vector3 objective = Stats.Speed * Time.fixedDeltaTime * (_playerPosition - transform.position).normalized;
			
			if(!_damaged)
				_rb.MovePosition(transform.position + objective);
			else {
				_rb.MovePosition(transform.position + (_pushDirection * Time.fixedDeltaTime));
				_damaged = false;
			}
		}

		private void Die() {
			Destroy(gameObject);
		}

		private IEnumerator ChangeColor() {
			_spriteRenderer.color = Color.red;
			yield return new WaitForSeconds(0.2f);
			_spriteRenderer.color = Color.white;
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if (!col.collider.CompareTag("Player")) return;

			Damage damage = new Damage{ Amount = Stats.Damage, PushOrigin = transform.position, PushForce = PushForce };

			col.collider.SendMessage("TakeDamage", damage);
			
		}
	}
}
