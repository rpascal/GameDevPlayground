using System.Collections;
using UnityEngine;
using Assets.Scripts.Damage;
using System.Collections.Generic;

namespace Assets.Scripts.Projectiles {
    public class StandardProjectile : BaseProjectile {
        protected override float TravelSpeed => 10f;

        private Dictionary<DamageType, float> _damageTypeMultipliers = new Dictionary<DamageType, float>() {
            {DamageType.Normal, 1.0f }
        };

        protected override Dictionary<DamageType, float> damageTypeMultipliers {
            get { return _damageTypeMultipliers; }
        }

    }
}