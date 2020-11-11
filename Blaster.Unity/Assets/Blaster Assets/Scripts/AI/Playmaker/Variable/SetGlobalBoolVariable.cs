using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Base.Manager;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.PlayMaker.Actions
{
    [ActionCategory("RQ.Variable")]
    [HutongGames.PlayMaker.Tooltip("Sets a global boolean variable.")]
    public class SetGlobalBoolVariable : BasePlayMakerAction
    {		
        [RequiredField]
		[UIHint(UIHint.Variable)]
		[HutongGames.PlayMaker.Tooltip("The speed, or in technical terms: velocity magnitude")]
		public FsmBool Value;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The name of the global variable to retrieve.")]
        public FsmString GlobalVariableName;

		[HutongGames.PlayMaker.Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
            Value = null;
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
		    if (GlobalVariableName.IsNone) return;

			GameStateController.Instance.GlobalVariables.BoolVariablesDict.Set(GlobalVariableName.Value, 
                Value.Value);
		}
	}
}