using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Base.Manager;
using BlueOrb.Controller.Buff;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    public class ProcessProjectileHit : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.GameObject)]
        public FsmArray EntityHits;

        public FsmGameObject EntityHit;

        [UIHint(UIHint.Tag)]
        public FsmString[] IncrementTag;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmInt Counter;

        public BuffConfig multiplierShieldBuffConfig;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            if (CheckTags(EntityHit.Value))
            {
                GameStateController.Instance.LevelStateController.PointsMultiplier().IncrementConsecutiveHits();
                Counter.Value++;
            }
            for (int k = 0; k < EntityHits.Length; k++)
            {
                GameObject entityHit = (GameObject)EntityHits.objectReferences[k];
                if (CheckTags(entityHit))
                {
                    GameStateController.Instance.LevelStateController.PointsMultiplier().IncrementConsecutiveHits();
                    Counter.Value++;
                    Debug.Log($"Incrementing Cons Hit counter to {Counter.Value}");
                }
            }

            Finish();
        }

        private bool CheckTags(GameObject entityHit)
        {
            if (entityHit == null)
            {
                return false;
            }
            for (int i = 0; i < IncrementTag.Length; i++)
            {
                string tag = IncrementTag[i].Value;
                if (entityHit.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }
    }
}