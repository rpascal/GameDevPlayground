using Assets.Scripts.Projectiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    public abstract class WeaponBase : MonoBehaviour {

        public abstract void Shoot();

        [SerializeField] Projectile _projectile = null;
        protected Projectile Projectile => _projectile;

        [SerializeField] Transform _projectileSpawnLocation = null;
        protected Transform ProjectileSpawnLocation => _projectileSpawnLocation;

    }
}