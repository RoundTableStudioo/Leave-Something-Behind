using System.Collections;
using RoundTableStudio.Core;
using RoundTableStudio.Player;
using UnityEngine;
using RoundTableStudio.Shared;
using RoundTableStudio.Sound;
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

		protected Rigidbody2D Rb;
		private SpriteRenderer _spriteRenderer;

		private Image _healthBar;

		private float _immuneTime;
		private float _lastImmune;
		protected PlayerManager PlayerManager;
		
		protected Transform Player;
		protected Vector3 PushDirection;

		protected bool Damaged;
		
		private float _currentHp;

		private const float _DROP_PROBABILITY = 0.08f;
		private const float _MANA_DROP_PROBABILITY = 0.65f;

		private void OnEnable() {
			Rb = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			if (GameObject.FindGameObjectWithTag("Player").transform != null) {
				Player = GameObject.FindGameObjectWithTag("Player").transform;
				PlayerManager = FindObjectOfType<PlayerManager>();
			}

			_currentHp = Stats.MaxHp;
		}

		private void FixedUpdate() {
			if (GameManager.Instance.IsGamePaused() || GameManager.Instance.IsGameEnded()) return;

			if (Vector3.Distance(transform.position, Player.position) >= 20f) {
				Destroy(gameObject);
				FindObjectOfType<EnemyRespawn>().EnemyCount--;
				return;
			}

			EnemyBehavior();
		}

		private void TakeDamage(Damage damage) {
			if (Time.time - _lastImmune <= _immuneTime) return;

			_lastImmune = Time.time;
			_currentHp -= damage.Amount;
			PushDirection = (transform.position - damage.PushOrigin).normalized * damage.PushForce;

			StartCoroutine(ChangeColor());
			Damaged = true;

			if (_currentHp <= 0) {
				Die();
			}

		}

		protected virtual void EnemyBehavior() {
			Debug.Log("To implement Behavior on " + name);
		}

		private void Die() {
			HandleEnemyDrop();
			FindObjectOfType<EnemyRespawn>().EnemyCount--;
			Destroy(gameObject);
		}

		private void HandleEnemyDrop() {
			float dropProbability = Random.Range(0f, 1f);

			if (dropProbability > _DROP_PROBABILITY) return;
			
			float potionProbability = Random.Range(0f, 1f);

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

		protected virtual  void OnCollisionStay2D(Collision2D col) {
			Debug.Log("To implement collider on " + name);
		}

		protected void DoEnemySound(string sound) {
			SoundManager.Instance.Play(sound);
		}
	}
}
