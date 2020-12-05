using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Scripts.UI;
using TMPro;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Custom")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class TargetDestroyed : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Label;
        public FsmVector3 LabelOffset;

        //public AddLayerWeightLerpAtom _atom;

        public override void OnEnter()
        {
            Debug.Log("(TargetDestroyed) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = go.GetComponent<IEntity>();

            var pos = UnityEngine.Camera.main.WorldToScreenPoint(entity.GetPosition() + LabelOffset.Value);

            // TODO This is bad and SLOW!
            var uiController = GameObject.FindObjectOfType<UIController>();

            var canvas = uiController.GetCanvas();

            //var extraInfo = ((Vector3 pos, string text, Color color))data.ExtraInfo;
            var label = GameObject.Instantiate(Label.Value, pos, Quaternion.identity, canvas.transform);
            // Calculate *screen* position (note, not a canvas/recttransform position)
            var textMesh = label.GetComponent<TextMeshProUGUI>();
            textMesh.text = "Hello!";

            //Vector2 canvasPos;

            //_atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            Debug.Log("(TargetDestroyed) OnExit called");
            base.OnExit();
            //_atom.End();
        }
    }
}
