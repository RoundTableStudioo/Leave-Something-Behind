using System;
using System.Collections;
using UnityEngine;
using RoundTableStudio.Player;
using RoundTableStudio.Shared;

namespace RoundTableStudio.Enemies {
    public class Mage : Enemy {
        [Tooltip("Projectile that the mage will shoot")]
        public GameObject ProjectilePrefab;
        [Tooltip("Time between shoots")]
        public float ShootingCooldown;
        
        private float _shootingTick;
        private bool shooting;

        protected override void EnemyBehavior() {
            Vector3 objective = Stats.Speed * Time.fixedDeltaTime * (Player.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, Player.position);

            if (!Damaged) {
                if (distance > 2.5f) {
                    Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Rb.MovePosition(transform.position + objective);
                }
                else if(!shooting) {
                    Rb.constraints = RigidbodyConstraints2D.FreezePosition;
                    DoEnemySound("Human");
                    StartCoroutine(Shoot());
                }
            }
            else {
                Rb.MovePosition(transform.position + (PushDirection * Time.fixedDeltaTime));
                Damaged = false;
            }
        }

        private IEnumerator Shoot() {
            shooting = true;
            
            Vector2 dir = Player.position - transform.position;

            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

            GameObject projectileGo = Instantiate(ProjectilePrefab, transform.position, rotation);
            EnemyProjectile projectile = projectileGo.GetComponent<EnemyProjectile>();

            projectile.Rigidbody2D.velocity = new Vector2(dir.x, dir.y) * projectile.Speed;

            yield return new WaitForSeconds(ShootingCooldown);

            shooting = false;
        }

        protected override void OnCollisionStay2D(Collision2D col) {
            if (!col.collider.CompareTag("Player")) return;
			
            Damage damage = new Damage { Amount = Stats.Damage * (1 - PlayerManager.Stats.HumanDefense), 
                PushOrigin = transform.position, 
                PushForce = PushForce,
                isPhysical = true,
                isMagical = false
            };

            col.collider.SendMessage("TakeDamage", damage);
        }
    }
}
