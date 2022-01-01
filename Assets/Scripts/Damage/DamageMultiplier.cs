using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {

    public class DamageMultiplier {

        public readonly DamageType type;
        public readonly float value;

        public DamageMultiplier(DamageType type, float value) {
            this.type = type;
            this.value = value;
        }


    }
}