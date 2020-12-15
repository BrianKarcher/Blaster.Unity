using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.AI.Playmaker.Physics.Data
{
    public class ExplodeData
    {
        public Vector3 ExplodePosition { get; set; }
        public float Force { get; set; }
        public float Damage { get; set; }
        public bool HasPoints { get; set; }
        public GameObject ExplodingEntity { get; set; }
    }
}
