using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles {
    public class SlowStrongProjectile : BaseProjectile {
        protected override float TravelSpeed => 1f;
    }
}