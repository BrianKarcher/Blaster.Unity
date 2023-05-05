using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Audio
{
    [ActionCategory("BlueOrb.AudioPlay")]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [ActionTarget(typeof(AudioClip), "clip")]
    [HutongGames.PlayMaker.Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
    public class AudioPlay : FsmStateAction
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
        public FsmObject clip;

        public FsmBool Loop;

        public FsmBool StopOnExit;

        [HutongGames.PlayMaker.Tooltip("Wait until the end of the clip to send the Finish Event. Set to false to send the finish event immediately.")]
        public FsmBool WaitForEndOfClip;

        [HutongGames.PlayMaker.Tooltip("Event to send when the action finishes.")]
        public FsmEvent finishedEvent;

        private AudioSource audio;

        public override void Reset()
        {
            gameObject = null;
            volume = 1f;
            clip = null;
            finishedEvent = null;
            WaitForEndOfClip = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                // cache the AudioSource component
                var audioClip = clip.Value as AudioClip;
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
                        audio.loop = this.Loop.IsNone ? false : this.Loop.Value;
                        audio.Play();
                    }
                }
            }

            // Finish if failed to play sound	

            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (this.StopOnExit.Value)
            {
                audio.Stop();
            }
        }

        public override void OnUpdate()
        {
            // Looping sounds never finish
            if (!this.Loop.IsNone && this.Loop.Value)
            {
                return;
            }
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
            if (clip.Value != null && !clip.IsNone)
            {
                return ActionHelpers.AutoName(this, clip);
            }

            return null;
        }
#endif
    }
}