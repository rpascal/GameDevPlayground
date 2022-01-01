using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {
    public interface IDamageable {

        int maxHealth { get; }
        int health { get; }

        DamageType type { get; }

        public void Damage(int amount);


    }
}