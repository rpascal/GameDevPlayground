using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    public class StandardWeapon : WeaponBase {

        [SerializeField] float fireRate = 0.5f;

        private float nextFire = 0f;

        public override void Shoot() {
            if (Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                Instantiate(Projectile, ProjectileSpawnLocation.position, ProjectileSpawnLocation.rotation);
            }
        }
    }
}