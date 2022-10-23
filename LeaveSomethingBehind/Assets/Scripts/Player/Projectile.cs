using System;
using RoundTableStudio.Shared;
using UnityEngine;

namespace RoundTableStudio.Player {
	public enum ProjectileType {
		Arrow,
		Magic
	}
	
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour {
		[Tooltip("Type of the projectile")] 
		public ProjectileType Type;
		[Tooltip("Force given to the projectile")]
		public float Speed;
		[Tooltip("Projectile damage attributes")]
		public Damage ProjectileDamage;
		[Tooltip("Radius of the magic explosion")]
		public float ExplosionRadius;
		[HideInInspector] 
		public Rigidbody2D Rigidbody2D;

		private PlayerManager _playerManager;

		private void OnEnable() {
			Rigidbody2D = GetComponent<Rigidbody2D>();
			_playerManager = FindObjectOfType<PlayerManager>();
			
			ProjectileDamage.PushOrigin = transform.position;

			if (Type == ProjectileType.Arrow)
				ProjectileDamage.Amount += _playerManager.Stats.RangeDamage;
			else if (Type == ProjectileType.Magic) 
				ProjectileDamage.Amount += _playerManager.Stats.MagicDamage;
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if (col.collider.CompareTag("Enemy")) {
				if(Type == ProjectileType.Arrow)
					col.collider.SendMessage("TakeDamage", ProjectileDamage);
				else if (Type == ProjectileType.Magic) {
					
					Collider2D[] colliders = Physics2D.OverlapCircleAll(col.transform.position, ExplosionRadius);

					foreach (Collider2D collider in colliders) {
						if (collider.CompareTag("Enemy")) {
							collider.SendMessage("TakeDamage", ProjectileDamage);
						}
					}
				}
			}
			
			if(!col.collider.CompareTag("Player"))
				Destroy(gameObject);
		}

		private void OnDrawGizmos() {
			if (Type != ProjectileType.Magic) return;
			
			Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
			Gizmos.color = Color.red;
		}
	}
}
