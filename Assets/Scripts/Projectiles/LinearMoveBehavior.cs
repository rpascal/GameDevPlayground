using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Projectiles {
    public class LinearMoveBehavior : IMoveable {
        readonly float _travelSpeed;
        readonly Rigidbody2D _rb;
        readonly Transform _objectTransform;

        public LinearMoveBehavior(Rigidbody2D rb, float travelSpeed) {
            _rb = rb;
            _travelSpeed = travelSpeed;
            _objectTransform = _rb.transform;
        }

        public void Move() {
            //_rb.AddForce(_objectTransform.up * _travelSpeed);
            _rb.velocity = _objectTransform.up * _travelSpeed;
        }
    }
}