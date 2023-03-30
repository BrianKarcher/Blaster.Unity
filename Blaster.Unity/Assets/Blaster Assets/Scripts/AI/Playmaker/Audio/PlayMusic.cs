using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Base.Manager;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Audio")]
    [Tooltip("Play music on the music track")]
    public class PlayMusic : FsmStateAction
    {
        public AudioClip AudioClip;

        public override void OnEnter()
        {
            base.OnEnter();
            GameStateController.Instance.PlayMusic(AudioClip);
            Finish();
        }
    }
}