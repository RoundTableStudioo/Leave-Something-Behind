using System;
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

        private void Update() {
            if (_shootingTick <= 0) return;

            _shootingTick -= Time.deltaTime;
        }

        protected override void EnemyBehavior() {
            Vector3 objective = (Player.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, Player.position);

            if (!Damaged) {
                if (distance > 2.5f) {
                    Rb.MovePosition(transform.position + Stats.Speed * Time.fixedDeltaTime * objective);
                }
                else {
                    Rb.MovePosition(transform.position);
                    if (_shootingTick <= 0) ;
                    //Shoot();
                }
            }
            else {
                Rb.MovePosition(transform.position + (PushDirection * Time.fixedDeltaTime));
                Damaged = false;
            }
        }

        private void Shoot() {
            Vector2 dir = Player.position - transform.position;

            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

            GameObject projectileGo = Instantiate(ProjectilePrefab, transform.position, rotation);
            Projectile projectile = projectileGo.GetComponent<Projectile>();

            projectile.Rigidbody2D.velocity = new Vector2(dir.x, dir.y) * projectile.Speed;
            _shootingTick = ShootingCooldown;
        }

        protected override void OnCollisionStay2D(Collision2D col) {
            if (!col.collider.CompareTag("Player")) return;
			
            Damage damage = new Damage { Amount = Stats.Damage - PlayerManager.Stats.HumanDefense - PlayerManager.Stats.PhysicalDefense, 
                PushOrigin = transform.position, 
                PushForce = PushForce,
                isPhysical = true,
                isMagical = false
            };

            col.collider.SendMessage("TakeDamage", damage);
        }
    }
}
