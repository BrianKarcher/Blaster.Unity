﻿using BlueOrb.Common.Components;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("BlueOrb/Components/Canvas Controller")]
    public class CanvasController : ComponentBase<CanvasController>
    {
        [SerializeField]
        private RewiredEventSystem rewiredEventSystem;
        [SerializeField]
        private Selectable firstSelectedGameObject;
        private RewiredStandaloneInputModule inputModule;

        private Rewired.Player _player;

        protected override void Awake()
        {
            base.Awake();
            _player = Rewired.ReInput.players.GetPlayer(0);
            if (inputModule == null)
            {
                inputModule = GameObject.FindObjectOfType<RewiredStandaloneInputModule>();
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SetFirstSelected();
        }

        public void Update()
        {
            if (rewiredEventSystem?.currentSelectedGameObject == null)
            {
                var currentHAxis = _player.GetAxis(inputModule.horizontalAxis);
                var currentVAxis = _player.GetAxis(inputModule.verticalAxis);
                if (Mathf.Abs(currentHAxis) > 0.3f || Mathf.Abs(currentVAxis) > 0.3f)
                {
                    rewiredEventSystem?.SetSelectedGameObject(firstSelectedGameObject.gameObject);
                }
            }
        }

        public void Start()
        {
            SetFirstSelected();
        }

        private void SetFirstSelected()
        {
            if (firstSelectedGameObject != null)
            {
                rewiredEventSystem?.SetSelectedGameObject(firstSelectedGameObject.gameObject);
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (rewiredEventSystem?.currentSelectedGameObject != null)
            {
                this.firstSelectedGameObject = rewiredEventSystem.currentSelectedGameObject.GetComponent<Selectable>();
            }
        }
    }
}