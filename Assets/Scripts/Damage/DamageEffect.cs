using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {
    public enum DamageEffect {
        NoEffect, // 0
        NotEffective, // half
        Normal, // 1
        SuperEffective, // double
        Max // ∞ 
    }

    public static class DamageEffectMultiplers {

        public static float getDamageEffectMultipler(DamageEffect damageEffect) {
            switch (damageEffect) {
                case DamageEffect.NoEffect: return 0f;
                case DamageEffect.NotEffective: return 0.5f;
                case DamageEffect.Normal: return 1f;
                case DamageEffect.SuperEffective: return 2f;
                case DamageEffect.Max: return float.MaxValue;
                default:
                    throw new System.ArgumentException($"{damageEffect} unknown DamageEffect");
            }
        }

    }

    public static class DamageEffectRelationships {

        public static DamageEffect damageEffectBetween(EntityDamageType doingDamageType, EntityDamageType recievingDamageType) {

            if (doingDamageType == EntityDamageType.Normal || recievingDamageType == EntityDamageType.Normal)
                return DamageEffect.Normal;


            if (doingDamageType == recievingDamageType)
                return DamageEffect.Max;

            if (doingDamageType == EntityDamageType.Red) {
                if (recievingDamageType == EntityDamageType.Blue) return DamageEffect.NoEffect;
                else if (recievingDamageType == EntityDamageType.Purple) return DamageEffect.NotEffective;
            }

            if (doingDamageType == EntityDamageType.Blue) {
                if (recievingDamageType == EntityDamageType.Red) return DamageEffect.NoEffect;
                else if (recievingDamageType == EntityDamageType.Purple) return DamageEffect.NotEffective;
            }

            if (doingDamageType == EntityDamageType.Purple)
                return DamageEffect.SuperEffective;


            throw new System.ArgumentException($"Unkown relationship between {doingDamageType} and {recievingDamageType}");
        }

    }

}