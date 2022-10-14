using UnityEngine;

namespace RoundTableStudio.Player {
	public class PlayerRangeAttack : MonoBehaviour {
		public GameObject ArrowPrefab;
		public Transform FirePoint;
		public float StaminaCost;
		public float ArrowSpeed;

		public void Shoot() {
			GameObject arrow = Instantiate(ArrowPrefab, FirePoint.position, FirePoint.rotation);
			Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
			
			rb.AddForce(FirePoint.up * ArrowSpeed, ForceMode2D.Impulse);
		}
	}
	
}
