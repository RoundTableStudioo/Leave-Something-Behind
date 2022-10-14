using UnityEngine;

namespace RoundTableStudio.Player {
	public class PlayerMagicAttack : MonoBehaviour
	{
		public GameObject ProjectilePrefab;
		public Transform FirePoint;
		public float ManaCost;
		public float ProjectileSpeed;

		public void Spell() {
			GameObject arrow = Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
			
			rb.AddForce(FirePoint.up * ProjectileSpeed, ForceMode2D.Impulse);
		}	
	}
	
}
