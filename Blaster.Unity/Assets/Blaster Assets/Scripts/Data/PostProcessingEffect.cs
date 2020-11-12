using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BlueOrb.Scripts.Data
{
    [Serializable]
    public class PostProcessingEffect
    {
        public string Name;
        public GameObject Effect;        
        public float InitialWeight;
        public float Duration;
        private Volume Volume;
        private float CurrentWeight;
        private float TargetWeight;
        private bool _isActive;

        public void Init()
        {
            Volume = Effect.GetComponent<Volume>();
            CurrentWeight = InitialWeight;
            SetWeight(InitialWeight);
            _isActive = false;
        }

        public void SetTargetWeight(float target)
        {
            //Volume.gameObject.SetActive(true);
            TargetWeight = target;
            _isActive = true;
        }

        public void Update()
        {
            if (!_isActive)
                return;
            CurrentWeight = Mathf.MoveTowards(CurrentWeight, TargetWeight, (1 / Duration) * Time.deltaTime);
            if (Mathf.Approximately(CurrentWeight, TargetWeight))
            {
                CurrentWeight = TargetWeight;
                //_isActive = false;
                Volume.gameObject.SetActive(false);
            }
            SetWeight(CurrentWeight);
        }

        private void SetWeight(float weight)
        {
            if (Volume == null)
                return;

            Volume.weight = weight;
            if (weight == 0)
                Volume.gameObject.SetActive(false);
            else
                Volume.gameObject.SetActive(true);
        }

        public float GetCurrentWeight()
        {
            return CurrentWeight;
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}
