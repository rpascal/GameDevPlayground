using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Layer {
    public static class Tags {
        public const string ProjectileModifierTag = "Projectile Modifier";
        public const string ProjectileTag = "Projectile";
        public const string AsteriodTag = "Asteriod";
        public const string BoundaryTag = "Boundary";

        public static bool hasTag(Component component, string tag) {
            return component.CompareTag(tag);
        }

        public static bool hasTag(Component component, params string[] tags) {
            foreach(string tag in tags) {
                if (component.CompareTag(tag)) {
                    return true;
                }
            }
            return false;
        }

        public static bool hasTag(Collision2D collision, string tag) {
            return collision.gameObject.CompareTag(tag);
        }

        public static bool hasTag(Collision2D collision, params string[] tags) {
            foreach (string tag in tags) {
                if (collision.gameObject.CompareTag(tag)) {
                    return true;
                }
            }
            return false;
        }

    }
}