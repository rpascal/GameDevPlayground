using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Projectile : MonoBehaviour {
        IMoveable _moveBehavior;

        [SerializeField] float _travelSpeed = 10;
        public float TravelSpeed => _travelSpeed;

        Rigidbody2D _rb = null;
        public float maxLifetime = 10.0f;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            // establish our initial projectile movement behavior
            _moveBehavior = new LinearMoveBehavior(_rb, _travelSpeed);
            Destroy(this.gameObject, this.maxLifetime);
        }

        private void FixedUpdate() {
            _moveBehavior.Move();
        }

        public void SetMoveBehavior(IMoveable newMoveBehavior) {
            _moveBehavior = newMoveBehavior;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Destroy(this.gameObject);
        }

    }
}