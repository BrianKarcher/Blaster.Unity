using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Components;
using Boo.Lang;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("RQ")]
    [HutongGames.PlayMaker.Tooltip("Random Wait With Action Chance")]
    public class RandomWaitWithEventChance : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("Minimum amount of time to wait.")]
        public FsmFloat min;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("Maximum amount of time to wait.")]
        public FsmFloat max;

        [HutongGames.PlayMaker.Tooltip("Ignore time scale.")]
        public bool realTime;

        public class EventWithWeight
        {
            public float Weight;
            public FsmEvent Event;
        }

        public EventWithWeight[] Events;

        private float startTime;
        private float timer;
        private float time;

        //public EnableComponentAtom _atom;

        public override void Reset()
        {
            gameObject = null;
            min = 0f;
            max = 1f;
            realTime = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            //_atom.Start(entity);
            time = Random.Range(min.Value, max.Value);

            if (time <= 0)
            {
                SendEvent();
                return;
            }

            startTime = FsmTime.RealtimeSinceStartup;
            timer = 0f;
        }

        public override void OnUpdate()
        {
            // update time

            if (realTime)
            {
                timer = FsmTime.RealtimeSinceStartup - startTime;
            }
            else
            {
                timer += Time.deltaTime;
            }

            if (timer >= time)
            {
                SendEvent();
                return;
            }
        }

        private void SendEvent()
        {
            float totalWeights = 0f;

            for (int i = 0; i < Events.Length; i++)
            {
                totalWeights += Events[i].Weight;
            }

            var value = Random.Range(0, totalWeights);

            float weightCounter = 0f;
            for (int i = 0; i < Events.Length; i++)
            {
                weightCounter += Events[i].Weight;
                if (value < weightCounter)
                {
                    Fsm.Event(Events[i].Event);
                    Finish();
                    return;
                }
            }
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            //_atom.End();
        }
    }
}
