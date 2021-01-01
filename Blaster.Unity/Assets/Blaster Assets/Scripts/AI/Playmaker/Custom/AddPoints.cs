using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Scripts.UI;
using TMPro;
using Assets.Blaster_Assets.Scripts.Components;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Custom")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class AddPoints : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Label;
        public FsmVector3 LabelOffset;
        public FsmInt Points;
        public Color Color = Color.white;

        //public AddLayerWeightLerpAtom _atom;

        public override void OnEnter()
        {
            Debug.Log("(AddPoints) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = go.GetComponent<IEntity>();


            var worldPos = entity.GetPosition() + LabelOffset.Value;

            var pos = UnityEngine.Camera.main.WorldToScreenPoint(worldPos);

            // TODO This is bad and SLOW!
            var uiController = GameObject.FindObjectOfType<UIController>();
            if (uiController == null)
            {
                Debug.LogError($"Cannot locate UIController.");
                Finish();
                return;
            }

            var canvas = uiController.GetCanvas();

            //var extraInfo = ((Vector3 pos, string text, Color color))data.ExtraInfo;
            var label = GameObject.Instantiate(Label.Value, pos, Quaternion.identity, canvas.transform);
            // Calculate *screen* position (note, not a canvas/recttransform position)
            var pinComponent = label.GetComponent<PinUIToWorldSpaceComponent>();            
            pinComponent.SetWorldPosition(worldPos);

            var textMeshPro = label.GetComponent<TextMeshProUGUI>();
            string prefix = string.Empty;
            //Color color;
            //if (Points.Value >= 0)
            //{
            //    prefix = "+";
            //    //color = 
            //}

            textMeshPro.color = Color;
            textMeshPro.SetText(prefix + Points.Value);

            //_atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            Debug.Log("(AddPoints) OnExit called");
            base.OnExit();
            //_atom.End();
        }
    }
}
