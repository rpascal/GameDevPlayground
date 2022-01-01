using System.Collections;
using UnityEngine;
using Assets.Scripts.Damage;
using System.Collections.Generic;

namespace Assets.Scripts.Projectiles {
    public class StandardProjectile : BaseProjectile {

        private Dictionary<DamageType, float> _damageTypeMultipliers = new Dictionary<DamageType, float>() {
            {DamageType.Normal, 1.0f }
        };

        protected override Dictionary<DamageType, float> damageTypeMultipliers {
            get { return _damageTypeMultipliers; }
        }

    }
}