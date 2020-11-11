using BlueOrb.Base.Manager;
using BlueOrb.Base.Skill;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class CurrentShardCompareAtom : AtomActionBase
    {
        public ShardConfig ShardConfig;
        //private bool _isEqual;
        //private ThirdPersonUserControl _thirdPersonUserControl;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            //_isEqual = 
            //if (_thirdPersonUserControl == null)
            //    _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
        }

        public bool IsEqual()
        {
            string shard1Name;
            string shard1UniqueId;
            string shard2Name;
            string shard2UniqueId;

            if (ShardConfig != null)
            {
                shard1Name = ShardConfig.name;
                shard1UniqueId = ShardConfig.UniqueId;
            }
            else
            {
                shard1Name = string.Empty;
                shard1UniqueId = string.Empty;
            }

            if (GameStateController.Instance.CurrentMold != null)
            {
                shard2Name = GameStateController.Instance.CurrentMold.name;
                shard2UniqueId = GameStateController.Instance.CurrentMold.UniqueId;
            }
            else
            {
                shard2Name = string.Empty;
                shard2UniqueId = string.Empty;
            }

            Debug.Log("Comparing " + shard1Name + "(" + shard1UniqueId + ") to " + shard2Name + "(" + shard2UniqueId + ")");
            return GameStateController.Instance.CurrentShard == ShardConfig;
        }
    }
}
