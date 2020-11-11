using BlueOrb.Scripts.Data;
using Boo.Lang;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
using System;
using UnityEngine;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("RQ/Components/PostProcessing Component")]
    public class PostProcessingComponent : ComponentBase
    {
        [SerializeField]
        private PostProcessingEffect[] _postProcessingEffects;
        private Action<Telegram> _setWeightDel;
        private long _setWeightIndex;

        protected override void Awake()
        {
            base.Awake();
            _setWeightDel = (telegram) =>
            {
                var data = telegram.ExtraInfo.ToString();
                var splitData = data.Split(',');
                SetWeight(splitData[0], float.Parse(splitData[1]));
            };
            for (int i = 0; i < _postProcessingEffects.Length; i++)
            {
                _postProcessingEffects[i].Init();
            }

        }

        private void Update()
        {
            for (int i = 0; i < _postProcessingEffects.Length; i++)
            {
                _postProcessingEffects[i].Update();
            }
        }

        public override void StartListening()
        {
            base.StartListening();
            _setWeightIndex = MessageDispatcher.Instance.StartListening("SetPostProcessingWeight", _componentRepository.GetId(), _setWeightDel);
        }

        public override void StopListening()
        {
            base.StopListening();
            MessageDispatcher.Instance.StopListening("SetPostProcessingWeight", _componentRepository.GetId(), _setWeightIndex);
        }

        public void SetWeight(string name, float weight)
        {
            for (int i = 0; i < _postProcessingEffects.Length; i++)
            {
                if (_postProcessingEffects[i].Name == name)
                {
                    _postProcessingEffects[i].SetTargetWeight(weight);
                    return;
                }
            }
        }

        public float GetWeight(string name)
        {
            for (int i = 0; i < _postProcessingEffects.Length; i++)
            {
                if (_postProcessingEffects[i].Name == name)
                {
                    return _postProcessingEffects[i].GetCurrentWeight();
                }
            }
            throw new System.Exception($"PostProcessing Effect {name} not found.");
        }

    }
}
