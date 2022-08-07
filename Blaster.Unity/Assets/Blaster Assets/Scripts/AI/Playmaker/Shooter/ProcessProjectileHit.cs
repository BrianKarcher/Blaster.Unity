using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using UnityEngine;
using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    public class ProcessProjectileHit : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmGameObject EntityHit;
        [UIHint(UIHint.Tag)]
        public FsmString[] IncrementTag;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            for (int i = 0; i < IncrementTag.Length; i++)
            {
                string tag = IncrementTag[i].Value;
                if (EntityHit.Value.CompareTag(tag))
                {
                    GameStateController.Instance.LevelStateController.PointsMultiplier().IncrementConsecutiveHits();
                    Finish();
                    return;
                }
            }
            GameStateController.Instance.LevelStateController.PointsMultiplier().ResetConsecutiveHits();
            Finish();
        }
    }
}