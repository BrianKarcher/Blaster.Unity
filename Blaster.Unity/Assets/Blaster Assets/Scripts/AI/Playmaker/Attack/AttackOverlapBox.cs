using BlueOrb.Scripts.AI.AtomActions.Attack;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Attack;
using UnityEngine;
using BlueOrb.Physics.Helpers;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.AttackOverlapBox")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class AttackOverlapBox : BasePlayMakerAction
    {
        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        [HutongGames.PlayMaker.Tooltip("The GameObject with an AudioSource component.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Layer)]
        [HutongGames.PlayMaker.Tooltip("Layers to check.")]
        public FsmInt[] Layer;
        public FsmVector3 Size;
        public FsmVector3 Offset;
        public FsmFloat Damage;
        public FsmBool IsDebug;
        [ObjectType(typeof(AudioClip))]
        [HutongGames.PlayMaker.Tooltip("Optionally play a 'one shot' AudioClip. NOTE: Volume cannot be adjusted while playing a 'one shot' AudioClip.")]
        public FsmObject oneShotClip;

        private AttackComponent attackComponent;
        private bool hitDetect = false;
        private IEntity entity;
        [HasFloatSlider(0, 1)]
        [HutongGames.PlayMaker.Tooltip("Set the volume.")]
        public FsmFloat volume;
        public FsmFloat audioDelay;
        private AudioSource audio;

        public override void Reset()
        {
            gameObject = null;
            Size = new Vector3(1f, 1f, 1f);
            Damage = 1.0f;
            Offset = new Vector3(0f, 0f, 1f);
            IsDebug = false;
            volume = 1f;
        }

        private Collider[] _itemHits = new Collider[50];
        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            this.entity = base.GetEntityBase(go);
            if (this.attackComponent == null)
            {
                this.attackComponent = entity.Components.GetComponent<AttackComponent>();
            }
            if (this.attackComponent == null)
            {
                Debug.LogError($"Entity {entity.name} has no Attack Component");
                return;
            }

            //_itemHits = UnityEngine.Physics.BoxCastAll(attackPos, halfExtent, transform.forward, transform.rotation, _attackData.Distance);
            var offset = entity.transform.TransformPoint(this.Offset.Value);
            var halfExtent = Size.Value / 2f;
            //if (IsDebug.Value)
            //{
            DisplayDebugInfo(entity, halfExtent);
            //}

            //DrawBoxCast.DrawBoxCastBox(attackPos, halfExtent, transform.rotation, transform.forward, _attackData.Distance, Color.red, 1f);
            //Debug.Log($"Attack pos: {attackPos}, extents: {halfExtent}, forward: {transform.forward}, distance: {_attackData.Distance}");
            int mask = ActionHelpers.LayerArrayToLayerMask(Layer, false);

            int count = UnityEngine.Physics.OverlapBoxNonAlloc(offset,
                halfExtent, _itemHits, Quaternion.identity, mask);
            Debug.Log($"Hit count: {count}");
            for (int i = 0; i < count; i++)
            {
                Debug.LogWarning($"Attack hit {_itemHits[i].name}");
            }

            this.hitDetect = this.attackComponent.ProcessAttack(_itemHits, count, this.Damage.Value);
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

            Finish();
        }

        private void DisplayDebugInfo(IEntity entity, Vector3 halfExtent)
        {
            var leftUpBack = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(-1f, 1f, -1f)));
            var rightUpBack = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(1f, 1f, -1f)));
            var rightDownBack = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(1f, -1f, -1f)));
            var leftDownBack = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(-1f, -1f, -1f)));

            var leftUpForward = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(-1f, 1f, 1f)));
            var rightUpForward = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(1f, 1f, 1f)));
            var rightDownForward = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(1f, -1f, 1f)));
            var leftDownForward = entity.transform.TransformPoint(Offset.Value + Vector3.Scale(halfExtent, new Vector3(-1f, -1f, 1f)));

            // Draw box outline of the attack.                
            Debug.DrawLine(leftUpBack, leftUpForward, Color.red, 5f);
            Debug.DrawLine(leftUpForward, rightUpForward, Color.red, 5f);
            Debug.DrawLine(rightUpForward, rightUpBack, Color.red, 5f);
            Debug.DrawLine(rightUpBack, leftUpBack, Color.red, 5f);

            Debug.DrawLine(leftUpBack, rightUpBack, Color.red, 5f);
            Debug.DrawLine(rightUpBack, rightDownBack, Color.red, 5f);
            Debug.DrawLine(rightDownBack, leftDownBack, Color.red, 5f);
            Debug.DrawLine(leftDownBack, leftUpBack, Color.red, 5f);
        }

        public override void OnDrawActionGizmos()
        {
            base.OnDrawActionGizmos();
            if (IsDebug.Value)
            {
                if (hitDetect)
                {
                    Gizmos.color = Color.red;
                    //Gizmos.matrix = transform.localToWorldMatrix;
                    var attackPos = this.entity.transform.TransformPoint(Offset.Value);
                    //Draw a Ray forward from GameObject toward the maximum distance
                    Vector3 worldOffset = this.entity.transform.TransformDirection(this.Offset.Value);
                    //Gizmos.DrawRay(attackPos, worldOffset);
                    //Draw a cube at the maximum distance
                    Gizmos.DrawWireCube(attackPos, Size.Value / 2f);

                    ////Draw a Ray forward from GameObject toward the hit
                    //Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
                    ////Draw a cube that extends to where the hit exists
                    //Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
                }
                //If there hasn't been a hit yet, draw the ray at the maximum distance
                else
                {
                    Gizmos.color = Color.white;
                    //Gizmos.matrix = transform.localToWorldMatrix;
                    var attackPos = this.entity.transform.TransformPoint(Offset.Value);
                    //Draw a Ray forward from GameObject toward the maximum distance
                    //Gizmos.DrawRay(attackPos, transform.forward * _attackData.Distance);
                    //Draw a cube at the maximum distance
                    //Gizmos.DrawWireCube(attackPos + transform.forward * _attackData.Distance, _attackData.Size);
                    Gizmos.DrawWireCube(attackPos, Size.Value / 2f);
                }
            }
        }
    }
}