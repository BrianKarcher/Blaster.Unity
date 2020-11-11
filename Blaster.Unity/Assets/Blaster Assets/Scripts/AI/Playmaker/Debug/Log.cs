using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Debug")]
    [HutongGames.PlayMaker.Tooltip("Center the camera behind the player.")]
    public class DebugLog : FsmStateAction
    {
        public string EnterMessage;
        public string ExitMessage;
        public LogWarningLevel LogLevel;
        public enum LogWarningLevel
        {
            Info = 0,
            Warning = 1,
            Error = 2
        }

        public override void OnEnter()
        {
            Debug.Log("(CenterCamera) OnEnter called");
            var entity = Owner.GetComponent<IEntity>();
            if (!String.IsNullOrEmpty(EnterMessage))
                Log(EnterMessage);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (!String.IsNullOrEmpty(ExitMessage))
                Log(ExitMessage);
        }

        public void Log(string message)
        {
            if (LogLevel == LogWarningLevel.Info)
                Debug.Log(message);
            else if (LogLevel == LogWarningLevel.Warning)
                Debug.LogWarning(message);
            else if (LogLevel == LogWarningLevel.Error)
                Debug.LogError(message);
        }
    }
}
