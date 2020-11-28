using BlueOrb.Scripts.AI.AtomActions.Physics;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using PM = HutongGames.PlayMaker;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;

namespace BlueOrb.Scripts.AI.Playmaker.Physics
{
    [ActionCategory("RQ.Pathfinding")]
    [PM.Tooltip("Calculate a new Flee path - used by FollowPath.")]
    public class CalculateFleePath : FsmStateAction
    {
        [RequiredField]
        [PM.Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmVector3 FleeFromTarget;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmArray StoreVectorArray;

        public FsmEvent PathError;

        public CalculateFleePathAtom _atom;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            if (FleeFromTarget.IsNone)
            {
                throw new System.Exception("(FSM) CalculatePath needs a FleeFromTarget to locate.");
            }

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = go.GetComponent<IEntity>();
            _atom.FleeFromTarget = FleeFromTarget.Value;
            _atom.Start(entity);

            if (!StoreVectorArray.IsNone)
            {
                //_points = new List<Vector3>();
                //for (int i = 0; i < _waypoints.transform.childCount; i++)
                //{
                //    _points.Add(_waypoints.transform.GetChild(i).position);
                //}



                //Vector4[] vector4s = new Vector4[_atom.Path.Length];
                //for (int i = 0; i < _atom.Path.Length; i++)
                //{
                //    var tempVector = _atom.Path[i];
                //    vector4s[i] = new Vector4(tempVector.x, tempVector.y, tempVector.z);
                //}
                //StoreVectorArray.vector4Values = vector4s;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _atom.OnUpdate();
            if (_atom.IsFinished)
            {
                if (_atom.IsPathError)
                {
                    Fsm.Event(PathError);
                    Finish();
                    return;
                }

                Vector4[] vector4s = new Vector4[_atom.Path.Count];
                for (int i = 0; i < _atom.Path.Count; i++)
                {
                    var tempVector = _atom.Path[i];
                    vector4s[i] = new Vector4(tempVector.x, tempVector.y, tempVector.z);
                }
                StoreVectorArray.vector4Values = vector4s;
                Finish();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
