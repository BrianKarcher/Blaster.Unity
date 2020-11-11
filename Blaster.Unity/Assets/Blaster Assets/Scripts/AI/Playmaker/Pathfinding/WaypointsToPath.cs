using BlueOrb.Scripts.AI.AtomActions.Physics;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using PM = HutongGames.PlayMaker;
using UnityEngine;
using System.Collections.Generic;

namespace BlueOrb.Scripts.AI.Playmaker.Physics
{
    [ActionCategory("RQ.Pathfinding")]
    [PM.Tooltip("Convert waypoints to a path - used by FollowPath.")]
    public class WaypointsToPath : FsmStateAction
    {
        //[RequiredField]
        //[CheckForComponent(typeof(CharacterController))]
        //[PM.Tooltip("The main GameObject.")]
        //public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmGameObject Waypoints;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmArray StoreVectorArray;

        //public WaypointsToPathAtom _atom;

        public override void OnEnter()
        {
            //var go = Fsm.GetOwnerDefaultTarget(gameObject);
            //if (go == null)
            //{
            //    return;
            //}

            //var entity = go.GetComponent<IEntity>();
            //_atom.Start(entity);
            if (Waypoints.IsNone)
            {
                throw new System.Exception("(FSM) WaypointsToPath needs a Game Object to pull waypoints from.");
            }

            if (!StoreVectorArray.IsNone)
            {
                //_points = new List<Vector3>();
                //for (int i = 0; i < _waypoints.transform.childCount; i++)
                //{
                //    _points.Add(_waypoints.transform.GetChild(i).position);
                //}

                Vector4[] vector4s = new Vector4[Waypoints.Value.transform.childCount];
                for (int i = 0; i < Waypoints.Value.transform.childCount; i++)
                {
                    var tempVector = Waypoints.Value.transform.GetChild(i).position;
                    vector4s[i] = new Vector4(tempVector.x, tempVector.y, tempVector.z);
                }
                StoreVectorArray.vector4Values = vector4s;

                //Vector4[] vector4s = new Vector4[_atom.Path.Length];
                //for (int i = 0; i < _atom.Path.Length; i++)
                //{
                //    var tempVector = _atom.Path[i];
                //    vector4s[i] = new Vector4(tempVector.x, tempVector.y, tempVector.z);
                //}
                //StoreVectorArray.vector4Values = vector4s;
            }

            Finish();
        }

        //public override void OnExit()
        //{
        //    base.OnExit();
            //_atom.End();
        //}
    }
}
