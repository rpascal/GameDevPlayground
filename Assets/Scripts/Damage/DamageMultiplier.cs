using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {

    public class DamageMultiplier {

        public readonly EntityDamageType type;
        public readonly float value;

        public DamageMultiplier(EntityDamageType type, float value) {
            this.type = type;
            this.value = value;
        }


    }
}