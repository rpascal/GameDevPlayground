using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    public class TripleShotWeapon : WeaponBase {

        [SerializeField] float fireRate = 0.25f;

        private float nextFire = 0f;

        public override void Shoot() {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                Instantiate(Projectile, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
                Instantiate(Projectile, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation * Quaternion.Euler(0,0,15));
                Instantiate(Projectile, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation * Quaternion.Euler(0, 0, -15));
            }
        }
    }
}