using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using PM = HutongGames.PlayMaker;
using System.Collections.Generic;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Pathfinding")]
    [PM.Tooltip("Follow supplied path.")]
    public class FollowPath3 : BasePlayMakerAction
    {
        [RequiredField]
        [PM.Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        public FollowPath3Atom _atom;
        [UIHint(UIHint.FsmEvent)]
        public FsmEvent Failed;
        [UIHint(UIHint.FsmEvent)]
        public FsmEvent Complete;
        [UIHint(UIHint.Variable)]
        public FsmArray vectorArray;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetRepo(go);
            List<Vector3> newPath = new List<Vector3>(vectorArray.vector4Values.Length);
            for (int i = 0; i < vectorArray.vector4Values.Length; i++)
            {
                newPath.Add((Vector3)vectorArray.vector4Values[i]);
            }
            _atom.SetPath(newPath);
            //var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
        }

        public override void OnDrawActionGizmos()
        {
            base.OnDrawActionGizmos();

        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_atom.IsFinished)
            {
                Fsm.Event(Complete);
                Finish();
            }
        }
    }
}
