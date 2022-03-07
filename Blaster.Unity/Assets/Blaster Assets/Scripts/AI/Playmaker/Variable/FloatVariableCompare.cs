using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Variable
{
    [ActionCategory("RQ.Variable")]
    [HutongGames.PlayMaker.Tooltip("Gets a global variable.")]
    public class FloatVariableCompare : BasePlayMakerAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		public enum FloatVariables
        {
			Stamina = 0
        }

		public FloatVariables Variable;

		//[RequiredField]
		//[UIHint(UIHint.Variable)]
		//[Tooltip("The speed, or in technical terms: velocity magnitude")]
		//public FsmFloat storeResult;

		//[RequiredField]
		//[UIHint(UIHint.Variable)]
		//[Tooltip("The name of the global variable to retrieve.")]
		//public FsmString GlobalVariableName;

		public FsmEvent GreaterThan;
		public FsmEvent LessThan;

		[RequiredField]
		[HutongGames.PlayMaker.Tooltip("The first float variable.")]
		public FsmFloat float1;

		[HutongGames.PlayMaker.Tooltip("Repeat every frame.")]
        public bool everyFrame;

		private EntityStatsComponent _entityStatsComponent;
		
		public override void Reset()
		{
			GreaterThan = null;
			LessThan = null;
			everyFrame = false;
			gameObject = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			var entity = go.GetComponent<IEntity>();
			if (_entityStatsComponent == null)
				_entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();

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
			var value = GetValue();

			if (value > float1.Value)
			{
				Fsm.Event(GreaterThan);
				return;
			}

			if (value < float1.Value)
			{
				Fsm.Event(LessThan);
				return;
			}
		}

		public float GetValue()
		{
			return 0f;
		}
	}
}