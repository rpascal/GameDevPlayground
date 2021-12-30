using Assets.Scripts.Layer;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles {

    [RequireComponent(typeof(SpriteRenderer))]
    public class BlueProjectile : BaseProjectile {
        protected override float TravelSpeed => 10f;

        override protected void Awake() {
            base.Awake();
            tag = Tags.BlueProjectileTag;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }

    }
}