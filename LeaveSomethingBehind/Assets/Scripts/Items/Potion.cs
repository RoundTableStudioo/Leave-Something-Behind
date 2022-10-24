using System;
using RoundTableStudio.Sound;
using UnityEngine;

namespace RoundTableStudio.Player  {
	public enum PotionType {
		Life,
		Mana,
	}
	
	[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
	public class Potion : MonoBehaviour {
		public PotionType Type;

		public float LifeAmount;
		
		private void OnTriggerEnter2D(Collider2D col) {
			if (!col.CompareTag("Player")) return;

			PlayerManager player = col.GetComponent<PlayerManager>();

			if (Type == PotionType.Life) {
				player.Life.RecoverHp(LifeAmount);
			} else if (Type == PotionType.Mana) {
				player.Mana.AddMana();
			}
			
			SoundManager.Instance.Play("Potion");
			Destroy(gameObject);
		}
	}
}
