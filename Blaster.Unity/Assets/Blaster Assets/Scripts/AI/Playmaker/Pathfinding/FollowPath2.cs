using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using PM = HutongGames.PlayMaker;
using System.Collections.Generic;
using UnityEngine;
using BlueOrb.Base.Extensions;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Pathfinding")]
    [PM.Tooltip("Follow supplied path in 2D.")]
    public class FollowPath2 : BasePlayMakerAction
    {
        [RequiredField]
        [PM.Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        public FollowPath2Atom _atom;
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
            List<Vector2> newPath = new List<Vector2>(vectorArray.vector4Values.Length);
            for (int i = 0; i < vectorArray.vector4Values.Length; i++)
            {
                // .xz turns a Vector3's x and z fields into a Vector2 (x=x, y=z)
                newPath.Add(((Vector3)vectorArray.vector4Values[i]).xz());
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
