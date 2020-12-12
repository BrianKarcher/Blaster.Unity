using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System.Collections.Generic;
using BlueOrb.Controller.Block;
using Assets.Blaster_Assets.Scripts.AI.Playmaker.Physics.Data;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Did we get exploded?")]
    public class IsExploded : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public IsExplodedAtom _atom;

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
            var entity = go.GetComponent<IEntity>();
            _atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
