using System;
using UnityEngine;

namespace RoundTableStudio.Shared {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour {
		[Tooltip("Projectile damage attributes")]
		public Damage ProjectileDamage;

		private void Start() {
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
