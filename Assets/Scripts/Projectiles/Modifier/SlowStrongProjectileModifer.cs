using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles.Modifier {
    public class SlowStrongProjectileModifer : BaseProjectileModifier {
        public override void modifyProjectile(BaseProjectile projectile) {
            Transform tranform = projectile.transform;
            spawnAndRegisterProjectile(tranform.position, tranform.rotation);
            Destroy(projectile.gameObject);
        }
    }
}