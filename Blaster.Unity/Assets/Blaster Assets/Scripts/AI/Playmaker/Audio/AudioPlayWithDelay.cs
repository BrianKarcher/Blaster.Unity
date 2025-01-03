﻿using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Audio
{
    [ActionCategory("BlueOrb.AudioPlayWithDelay")]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [ActionTarget(typeof(AudioClip), "oneShotClip")]
    [HutongGames.PlayMaker.Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
    public class AudioPlayWithDelay : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        [HutongGames.PlayMaker.Tooltip("The GameObject with an AudioSource component.")]
        public FsmOwnerDefault gameObject;

        [HasFloatSlider(0, 1)]
        [HutongGames.PlayMaker.Tooltip("Set the volume.")]
        public FsmFloat volume;

        public FsmFloat audioDelay;

        [ObjectType(typeof(AudioClip))]
        [HutongGames.PlayMaker.Tooltip("Optionally play a 'one shot' AudioClip. NOTE: Volume cannot be adjusted while playing a 'one shot' AudioClip.")]
        public FsmObject oneShotClip;

        [HutongGames.PlayMaker.Tooltip("Wait until the end of the clip to send the Finish Event. Set to false to send the finish event immediately.")]
        public FsmBool WaitForEndOfClip;

        public bool StopOnExit = true;

        [HutongGames.PlayMaker.Tooltip("Event to send when the action finishes.")]
        public FsmEvent finishedEvent;

        private AudioSource audio;

        public override void Reset()
        {
            gameObject = null;
            volume = 1f;
            oneShotClip = null;
            finishedEvent = null;
            WaitForEndOfClip = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                // cache the AudioSource component
                var audioClip = oneShotClip.Value as AudioClip;
                if (audioClip != null)
                {
                    audio = go.GetComponent<AudioSource>();
                    if (audio != null)
                    {
                        if (!volume.IsNone)
                        {
                            audio.volume = volume.Value;
                            //audio.PlayOneShot(audioClip, volume.Value);
                        }
                        else
                        {
                            audio.volume = 1;
                            //audio.PlayOneShot(audioClip);
                        }
                        float delay = this.audioDelay.IsNone ? 0f : this.audioDelay.Value;
                        audio.clip = audioClip;
                        audio.PlayDelayed(delay);
                    }
                }
            }

            // Finish if failed to play sound	
            if (!WaitForEndOfClip.Value)
            {
                Finish();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if (StopOnExit)
            {
                audio.Stop();
            }
        }

        public override void OnUpdate()
        {
            if (audio == null)
            {
                Finish();
            }
            else
            {
                if (!audio.isPlaying)
                {
                    Fsm.Event(finishedEvent);
                    Finish();
                }
                else if (!volume.IsNone && volume.Value != audio.volume)
                {
                    audio.volume = volume.Value;
                }
            }
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            if (oneShotClip.Value != null && !oneShotClip.IsNone)
            {
                return ActionHelpers.AutoName(this, oneShotClip);
            }

            return null;
        }
#endif
    }
}