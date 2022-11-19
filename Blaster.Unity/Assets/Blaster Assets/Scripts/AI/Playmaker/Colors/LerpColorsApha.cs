using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using System.Collections.Generic;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("BlueOrb.Colors")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class LerpColorsApha : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmArray Meshes;
        public FsmFloat StartingAlpha;
        public FsmFloat FinalAlpha;
        public FsmFloat Time;

        private List<MeshRenderer> meshes = new List<MeshRenderer>();
        private float startTime;
        private float endTime;
        //private float currentTime;

        public override void OnEnter()
        {
            Debug.Log("(AddLayerWeightLerp) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            MeshRenderer meshRenderer;
            foreach (FsmGameObject mesh in Meshes.objectReferences)
            {
                meshRenderer = mesh.Value.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshes.Add(meshRenderer);
                }
                meshes.AddRange(mesh.Value.GetComponentsInChildren<MeshRenderer>());
            }
            this.startTime = UnityEngine.Time.time;
            this.endTime = this.startTime + this.Time.Value;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            float perc = (UnityEngine.Time.time - this.startTime) / (this.endTime - this.startTime);
            if (UnityEngine.Time.time > this.endTime)
            {
                perc = 1;
                Finish();
            }
            for (int i = 0; i < this.meshes.Count; i++)
            {
                MeshRenderer meshRenderer = this.meshes[i];
                Color currentColor = meshRenderer.material.color;
                meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(this.startTime, this.endTime, perc));
            }
            
        }
    }
}
