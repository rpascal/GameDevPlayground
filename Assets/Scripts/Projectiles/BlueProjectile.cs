using Assets.Scripts.Damage;
using Assets.Scripts.Layer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectiles {

    public class BlueProjectile : BaseProjectile {
        protected override float TravelSpeed => 10f;

        private Dictionary<DamageType, float> _damageTypeMultipliers = new Dictionary<DamageType, float>() {
            {DamageType.Normal, 2.0f }
        };

        protected override Dictionary<DamageType, float> damageTypeMultipliers {
            get { return _damageTypeMultipliers; }
        }

        override protected void Awake() {
            base.Awake();
            tag = Tags.BlueProjectileTag;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }

    }
}