
using Assets.Scripts.Layer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectiles.Modifier {

    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class BaseProjectileModifier : MonoBehaviour {

        protected Dictionary<int, float> spawnedProjectiles = new Dictionary<int, float>();

        [SerializeField] BaseProjectile _projectilePrefab;
        public BaseProjectile projectilePrefab => _projectilePrefab;

        private float maxTimeBeforeCanModifyAgain = 0.5f;

        private void Awake() {
            GetComponent<BoxCollider2D>().isTrigger = true;
            tag = Tags.ProjectileModifierTag;
            //GetComponent<BoxCollider2D>().isTrigger = true;
        }

        /**
         * Doing this so newly spawned do not interact with the modifier that was just passed through.
         * Maybe could use an id?
         */
        protected BaseProjectile spawnAndRegisterProjectile(Vector3 position, Quaternion rotation) {
            return spawnAndRegisterProjectile(position, rotation, projectilePrefab);
        }
        protected BaseProjectile spawnAndRegisterProjectile(Vector3 position, Quaternion rotation, BaseProjectile prefab) {
            BaseProjectile newProjectile = Instantiate(prefab, position, rotation);
            spawnedProjectiles.Add(newProjectile.GetInstanceID(), Time.time);
            return newProjectile;
        }

        public abstract void modifyProjectile(BaseProjectile projectile);

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.TryGetComponent<BaseProjectile>(out BaseProjectile projectile)) {
                if (spawnedProjectiles.TryGetValue(projectile.GetInstanceID(), out float spawnTime)) {
                    float timeDifference = Time.time - spawnTime;
                    if (timeDifference >= maxTimeBeforeCanModifyAgain) {
                        spawnedProjectiles.Remove(projectile.GetInstanceID());
                    }
                } else {
                    modifyProjectile(projectile);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider) {
            if (collider.TryGetComponent<BaseProjectile>(out BaseProjectile projectile)) {
                spawnedProjectiles.Remove(projectile.GetInstanceID());
            }
        }

    }
}