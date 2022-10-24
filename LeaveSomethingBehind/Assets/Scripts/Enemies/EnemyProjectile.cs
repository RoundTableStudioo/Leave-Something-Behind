using System;
using UnityEngine;

namespace RoundTableStudio.Shared {
    public enum Element {
        Fire,
        Water,
        Ground,
        Wind
    }
    public class EnemyProjectile : MonoBehaviour {
        [Tooltip("Type of the projectile")]
        public Element ProjectileType;
        [Tooltip("Speed of the projectile")]
        public float Speed;
        [Tooltip("Damage of the projectile")]
        public Damage ProjectileDamage;

        [HideInInspector] 
        public Rigidbody2D Rigidbody2D;

        private void OnEnable() {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            Destroy(gameObject, 6);
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.collider.CompareTag("Player")) {
                switch (ProjectileType) {
                    case Element.Fire: // Bola de fuego normal
                        col.collider.SendMessage("TakeDamage", ProjectileDamage);
                        break;
                    case Element.Ground: // Te dejan paralizado 1 segundo
                        col.collider.SendMessage("TakeDamage", ProjectileDamage);
                        break;
                    case Element.Water: // Te reducen la velocidad un 25% durante 1 segundo
                        col.collider.SendMessage("TakeDamage", ProjectileDamage);
                        break;
                    case Element.Wind: // Bola de viento normal
                        col.collider.SendMessage("TakeDamage", ProjectileDamage);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                Destroy(gameObject);
            }
            
            Destroy(gameObject);
        }
    }
}
