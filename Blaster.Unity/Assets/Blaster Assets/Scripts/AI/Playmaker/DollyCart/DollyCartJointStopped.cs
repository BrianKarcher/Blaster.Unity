using BlueOrb.Controller;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCartJoint")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class DollyCartJointStopped : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private DollyCartJointComponent dollyCart;
        private Collider[] colliders = new Collider[20];
        public FsmEvent Go;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            dollyCart ??= entity.GetComponent<DollyCartJointComponent>();
            if (dollyCart == null)
            {
                Debug.LogError($"Could not locate Dolly Cart Component");
                return;
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (dollyCart == null)
            {
                return;
            }
            if (!CheckEnemyCollision() && dollyCart.HasCart)
            {
                Fsm.Event(Go);
            }
        }

        private bool CheckEnemyCollision()
        {
            int count = UnityEngine.Physics.OverlapBoxNonAlloc(dollyCart.transform.TransformPoint(dollyCart.EnemyCheckOffset),
                dollyCart.EnemyCheckHalfExtents, colliders, Quaternion.identity);
            for (int i = 0; i < count; i++)
            {
                if (this.colliders[i].CompareTag(dollyCart.EnemyTag))
                {
                    return true;
                }
            }
            return false;
        }
    }
}