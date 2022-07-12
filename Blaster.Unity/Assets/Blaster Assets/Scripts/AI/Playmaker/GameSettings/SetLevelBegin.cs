using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using UnityEngine;
using BlueOrb.Controller.Manager;
using BlueOrb.Base.Global;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("BlueOrb.GameSettings")]
    [HutongGames.PlayMaker.Tooltip("Set Level Begin variable")]
    public class SetLevelBegin : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmString LevelBeginMessage;

        public override void Reset()
        {
            gameObject = null;
            LevelBeginMessage = "SetLevelBegin";
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            SceneSetup sceneSetup = GameObject.FindObjectOfType<SceneSetup>();
            if (sceneSetup != null)
            {
                GlobalStatic.NextSceneConfig = sceneSetup.SceneConfig;
            }
            MessageDispatcher.Instance.DispatchMsg(LevelBeginMessage.Value, 0f, entity.GetId(), entity.GetId(), null);
            Finish();
        }
    }
}
