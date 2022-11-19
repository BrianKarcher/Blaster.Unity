using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using System.Collections.Generic;

namespace BlueOrb.Scripts.AI.Playmaker.Material
{
    [ActionCategory("BlueOrb.Colors")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class LerpMaterialFloat : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmArray Meshes;
        public FsmInt materialIndex;
        [RequiredField]
        //[Tooltip("A named float parameter in the shader.")]
        public FsmString namedFloat;
        public FsmFloat Start;
        public FsmFloat Final;
        public FsmFloat Time;

        private List<Renderer> meshes = new List<Renderer>();
        private float startTime;
        private float endTime;
        //private float currentTime;

        public override void Reset()
        {
            base.Reset();
            this.Start = -1;
            this.Final = 0.5f;
        }

        public override void OnEnter()
        {
            Debug.Log("(AddLayerWeightLerp) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            Renderer meshRenderer;
            foreach (FsmGameObject mesh in Meshes.objectReferences)
            {
                meshRenderer = mesh.Value.GetComponent<Renderer>();
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
            float pos = (UnityEngine.Time.time - this.startTime) / (this.endTime - this.startTime);
            if (UnityEngine.Time.time > this.endTime)
            {
                pos = this.Final.Value;
                Finish();
            }
            for (int i = 0; i < this.meshes.Count; i++)
            {
                Renderer meshRenderer = this.meshes[i];
                if (meshRenderer.material == null)
                {
                    LogError("Missing Material!");
                    return;
                }

                //if (materialIndex.Value == 0)
                //{
                //    meshRenderer.material.SetFloat(namedFloat.Value, pos);
                //}
                //else if (meshRenderer.materials.Length > materialIndex.Value)
                //{

                var materials = meshRenderer.materials;
                for (int j = 0; j < materials.Length; j++)
                {
                    materials[j].SetFloat(namedFloat.Value, pos);
                }
                meshRenderer.materials = materials;
                //}
                //Color currentColor = meshRenderer.material.color;
                //meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(this.startTime, this.endTime, perc));
            }
            
        }
    }
}
