using System;
using UnityEngine;

namespace RoundTableStudio.Shared {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour {
		[Tooltip("Force given to the projectile")]
		public float Speed;
		[Tooltip("Projectile damage attributes")]
		public Damage ProjectileDamage;
		[HideInInspector] 
		public Rigidbody2D Rigidbody2D;

		private void OnEnable() {
			Rigidbody2D = GetComponent<Rigidbody2D>();
			ProjectileDamage.PushOrigin = transform.position;
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if (col.collider.CompareTag("Enemy")) {
				col.collider.SendMessage("TakeDamage", ProjectileDamage);
			}
			
			if(!col.collider.CompareTag("Player"))
				Destroy(gameObject);
		}
	}
}
