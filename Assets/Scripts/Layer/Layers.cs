using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Layer {
    public static class Layers {

        public const string Player = "Player";

        public static int PlayerLayerId = LayerMask.NameToLayer(Player);

        public const string IgnoreCollisions = "Ignore Collisions";
        public static int IgnoreCollisionsLayerId = LayerMask.NameToLayer(IgnoreCollisions);

        public const string Bullet = "Bullet";
        public static int BulletLayerId = LayerMask.NameToLayer(Bullet);

    }
}