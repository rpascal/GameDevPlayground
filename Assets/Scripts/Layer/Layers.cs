using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Layer {
    public static class Layers {

        public const string Player = "Player";

        public static int PlayerLayerId = LayerMask.NameToLayer(Player);
    }
}