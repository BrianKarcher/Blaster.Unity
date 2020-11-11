using HutongGames.PlayMaker;
using BlueOrb.Common.Components;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker
{
    public class BasePlayMakerAction : FsmStateAction
    {
        public IEntity GetRepo(GameObject owner)
        {
            //var rqSM = owner.GetComponent<IComponentBase>();
            //return rqSM.GetComponentRepository();
            var entity = owner.GetComponent<IEntity>();
            return entity;
        }
    }
}
