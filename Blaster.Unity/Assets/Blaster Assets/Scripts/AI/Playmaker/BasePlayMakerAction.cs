using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker
{
    public class BasePlayMakerAction : FsmStateAction
    {
        public IEntity GetEntityBase(GameObject owner)
        {
            //var rqSM = owner.GetComponent<IComponentBase>();
            //return rqSM.GetComponentRepository();
            IEntity entity = null;
            Transform currentTransform = owner.transform;
            while (entity == null && currentTransform != null)
            {
                entity = currentTransform.GetComponent<IEntity>();
                if (entity == null)
                {
                    currentTransform = currentTransform.parent;
                }
            }
            return entity;
        }
    }
}
