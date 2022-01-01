using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {
    public static class DamageAmountCalculator {

        public static float calculateDamage(EntityDamageType entityDoingDamageType, EntityDamageType entityRecievingDamageType, float baseAmount) {
            DamageEffect damageEffect = DamageEffectRelationships.damageEffectBetween(entityDoingDamageType, entityRecievingDamageType);
            float multipler = DamageEffectMultiplers.getDamageEffectMultipler(damageEffect);
            return baseAmount * multipler;
        }

    }
}