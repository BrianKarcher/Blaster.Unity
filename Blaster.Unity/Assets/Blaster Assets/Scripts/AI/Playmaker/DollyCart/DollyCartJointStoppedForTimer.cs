using BlueOrb.Controller;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCartJoint")]
    public class DollyCartJointStoppedForTimer : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private DollyCartJointComponent dollyCart;
        private float endTime;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            Debug.Log("DollyCartJointStopped Entered");
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

            this.endTime = Time.time + this.dollyCart.StopTime;
            dollyCart.SetBrake(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            dollyCart.SetBrake(false);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time > this.endTime)
            {
                Finish();
            }
        }
    }
}