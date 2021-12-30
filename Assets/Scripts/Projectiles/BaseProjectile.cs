using Assets.Scripts.Layer;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class BaseProjectile : MonoBehaviour {
        private IMoveable _moveBehavior;

        private float _travelSpeed = 10;
        protected virtual float TravelSpeed => _travelSpeed;

        [SerializeField] private float maxLifetime = 10.0f;

        private Rigidbody2D _rb = null;


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


        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Destroy(this.gameObject);
        }

    }
}