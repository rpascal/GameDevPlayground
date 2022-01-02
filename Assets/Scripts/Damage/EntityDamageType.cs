using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damage {

    public enum EntityDamageType {
        Red,
        Blue,
        Purple,
        Normal
    }

    public static class EntityDamageTypeScoreMultipliers {

        public static int scoreMulitplerForType(EntityDamageType type) {
            switch (type) {
                case EntityDamageType.Normal: return 1;
                case EntityDamageType.Blue:
                case EntityDamageType.Red:
                    return 2;
                case EntityDamageType.Purple:
                    return 5;
            }
            throw new System.ArgumentException($"Unknown damage type {type}");
        }

    }

}