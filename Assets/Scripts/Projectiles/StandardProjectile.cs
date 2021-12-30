using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles {
    public class StandardProjectile : BaseProjectile {
        protected override float TravelSpeed => 10f;
    }
}