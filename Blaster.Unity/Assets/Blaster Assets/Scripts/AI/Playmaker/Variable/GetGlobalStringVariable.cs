﻿using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Base.Manager;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.PlayMaker.Actions
{
    [ActionCategory("RQ.Variable")]
    [HutongGames.PlayMaker.Tooltip("Gets a global variable.")]
    public class GetGlobalStringVariable : BasePlayMakerAction
    {		
        [RequiredField]
		[UIHint(UIHint.Variable)]
		[HutongGames.PlayMaker.Tooltip("The speed, or in technical terms: velocity magnitude")]
		public FsmString storeResult;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The name of the global variable to retrieve.")]
        public FsmString GlobalVariableName;

		[HutongGames.PlayMaker.Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
			storeResult = null;
            GlobalVariableName = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			Tick();

		    if (!everyFrame)
		    {
		        Finish();
		    }		
		}
		
		public override void OnUpdate()
		{
            Tick();
		}
		
		private void Tick()
		{
		    if (storeResult.IsNone) return;

			storeResult.Value = GameStateController.Instance.GlobalVariables.StringVariablesDict.Get(GlobalVariableName.Value);
		}
	}
}