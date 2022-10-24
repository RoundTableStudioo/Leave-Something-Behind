using RoundTableStudio.Shared;
using UnityEngine;

namespace RoundTableStudio.Enemies {
    public class Orc : Enemy {
        protected override void EnemyBehavior() {
            Vector3 objective = Stats.Speed * Time.fixedDeltaTime * (Player.position - transform.position).normalized;
			
            if(!Damaged)
                Rb.MovePosition(transform.position + objective);
            else {
                Rb.MovePosition(transform.position + (PushDirection * Time.fixedDeltaTime));
                Damaged = false;
            }
        }

        protected override void OnCollisionStay2D(Collision2D col) {
            if (!col.collider.CompareTag("Player")) return;
			
            Damage damage = new Damage { Amount = Stats.Damage - PlayerManager.Stats.OrcDefense - PlayerManager.Stats.PhysicalDefense, 
                PushOrigin = transform.position, 
                PushForce = PushForce };

            col.collider.SendMessage("TakeDamage", damage);
        }
    }
}
