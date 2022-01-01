using Assets.Scripts.Damage;
using Assets.Scripts.Layer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectiles {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseProjectile : MonoBehaviour {
        private IMoveable _moveBehavior;

        private float _travelSpeed = 10;
        protected virtual float TravelSpeed => _travelSpeed;

        [SerializeField] private float maxLifetime = 5.0f;

        private Rigidbody2D _rb = null;

        private Dictionary<DamageType, float> _defaultDamageTypeMultipliers = new Dictionary<DamageType, float>();

        protected virtual Dictionary<DamageType, float> damageTypeMultipliers {
            get { return _defaultDamageTypeMultipliers; }
        }

        [SerializeField] float _baseAttack = 1;
        protected virtual float baseAttack => _baseAttack;

        protected virtual void Awake() {
            tag = Tags.ProjectileTag;
            _rb = GetComponent<Rigidbody2D>();
            // establish our initial projectile movement behavior
            _moveBehavior = new LinearMoveBehavior(_rb, TravelSpeed);
            Destroy(this.gameObject, this.maxLifetime);
        }

        private void FixedUpdate() {
            _moveBehavior.Move();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.TryGetComponent(out IDamageable damageable)) {
                float multipler = 1.0f;
                damageTypeMultipliers.TryGetValue(damageable.type, out multipler);
                damageable.Damage((int)(baseAttack * multipler));
                Destroy(this.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Destroy(this.gameObject);
        }

        protected void Reset() {
            tag = Tags.ProjectileTag;
            gameObject.layer = Layers.BulletLayerId;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }
}