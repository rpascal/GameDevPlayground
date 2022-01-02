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
            switch (doingDamageType) {
                case EntityDamageType.Normal: return normalToOther(recievingDamageType);
                case EntityDamageType.Red: return redToOther(recievingDamageType);
                case EntityDamageType.Blue: return blueToOther(recievingDamageType);
                case EntityDamageType.Purple: return purpleToOther(recievingDamageType);
                default:
                    throw createRelationshipException(EntityDamageType.Purple, recievingDamageType);
            }
        }

        private static DamageEffect normalToOther(EntityDamageType recievingDamageType) {
            switch (recievingDamageType) {
                case EntityDamageType.Normal:
                    return DamageEffect.Normal;
                case EntityDamageType.Red:
                case EntityDamageType.Blue:
                    return DamageEffect.NotEffective;
                case EntityDamageType.Purple:
                    return DamageEffect.NoEffect;
                default:
                    throw createRelationshipException(EntityDamageType.Normal, recievingDamageType);
            }
        }

        private static DamageEffect redToOther(EntityDamageType recievingDamageType) {
            switch (recievingDamageType) {
                case EntityDamageType.Normal:
                    return DamageEffect.Normal;
                case EntityDamageType.Red:
                    return DamageEffect.Max;
                case EntityDamageType.Blue:
                    return DamageEffect.NoEffect;
                case EntityDamageType.Purple:
                    return DamageEffect.SuperEffective;
                default:
                    throw createRelationshipException(EntityDamageType.Red, recievingDamageType);
            }
        }

        private static DamageEffect blueToOther(EntityDamageType recievingDamageType) {
            switch (recievingDamageType) {
                case EntityDamageType.Normal:
                    return DamageEffect.Normal;
                case EntityDamageType.Red:
                    return DamageEffect.NoEffect;
                case EntityDamageType.Blue:
                    return DamageEffect.Max;
                case EntityDamageType.Purple:
                    return DamageEffect.SuperEffective;
                default:
                    throw createRelationshipException(EntityDamageType.Blue, recievingDamageType);
            }
        }

        private static DamageEffect purpleToOther(EntityDamageType recievingDamageType) {
            switch (recievingDamageType) {
                case EntityDamageType.Normal:
                    return DamageEffect.Normal;
                case EntityDamageType.Red:
                    return DamageEffect.SuperEffective;
                case EntityDamageType.Blue:
                    return DamageEffect.SuperEffective;
                case EntityDamageType.Purple:
                    return DamageEffect.Max;
                default:
                    throw createRelationshipException(EntityDamageType.Purple, recievingDamageType);
            }
        }

        private static System.ArgumentException createRelationshipException(EntityDamageType doingDamageType, EntityDamageType recievingDamageType) {
            return new System.ArgumentException($"Unkown relationship between {doingDamageType} and {recievingDamageType}");
        }
    }



}