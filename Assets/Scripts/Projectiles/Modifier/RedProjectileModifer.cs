using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles.Modifier {
    public class RedProjectileModifer : BaseProjectileModifier {

        [SerializeField] PurpleProjectile purpleProjectile;

        public override void modifyProjectile(BaseProjectile projectile) {
            Transform tranform = projectile.transform;

            if (projectile is BlueProjectile) {
                spawnAndRegisterProjectile(tranform.position, tranform.rotation, purpleProjectile);
            } else {
                spawnAndRegisterProjectile(tranform.position, tranform.rotation);
            }

            Destroy(projectile.gameObject);
        }
    }
}