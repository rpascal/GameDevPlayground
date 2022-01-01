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

        [SerializeField] private float _travelSpeed = 10;
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
            Destroy(this.gameObject, this.maxLifetime);
        }

        private void FixedUpdate() {
            _rb.velocity = _rb.transform.up * _travelSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.TryGetComponent(out IDamageable damageable)) {
                float multipler = 1.0f;
                if (damageTypeMultipliers.ContainsKey(damageable.type)) {
                    multipler = damageTypeMultipliers[damageable.type];
                }

                damageable.Damage((int)(baseAttack * multipler));
                Destroy(this.gameObject);
            }
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