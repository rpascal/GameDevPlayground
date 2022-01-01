using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {
    public interface IDamageable {

        float maxHealth { get; }
        float health { get; }

        EntityDamageType type { get; }

        public void Damage(float amount);


    }
}