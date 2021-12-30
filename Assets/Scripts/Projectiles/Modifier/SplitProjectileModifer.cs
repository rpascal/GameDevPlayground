﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles.Modifier {
    public class SplitProjectileModifer : BaseProjectileModifier {
        public override void modifyProjectile(BaseProjectile projectile) {
            Transform tranform = projectile.transform;
            spawnAndRegisterProjectile(tranform.position, tranform.rotation * Quaternion.Euler(0, 0, 15));
            spawnAndRegisterProjectile(tranform.position, tranform.rotation * Quaternion.Euler(0, 0, -15));
            Destroy(projectile.gameObject);
        }
    }
}