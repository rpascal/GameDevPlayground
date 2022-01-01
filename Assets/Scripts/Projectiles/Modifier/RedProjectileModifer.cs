using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles.Modifier {
    public class RedProjectileModifer : BaseProjectileModifier {

        [SerializeField] BaseProjectile purpleProjectile;

        public override void modifyProjectile(BaseProjectile projectile) {
            Transform tranform = projectile.transform;

            if (projectile.type == Damage.EntityDamageType.Blue) {
                spawnAndRegisterProjectile(tranform.position, tranform.rotation, purpleProjectile);
            } else {
                spawnAndRegisterProjectile(tranform.position, tranform.rotation);
            }

            Destroy(projectile.gameObject);
        }
    }
}