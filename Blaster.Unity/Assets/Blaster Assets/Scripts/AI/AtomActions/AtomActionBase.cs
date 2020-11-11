using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class AtomActionBase : IAtomActionBase
    {
        protected bool _isFinished = false;
        public bool IsFinished { get { return _isFinished; } }

        protected IEntity _entity;

        public virtual void Start(IEntity entity)
        {
            _entity = entity;
            _isFinished = false;
            StartListening(_entity);
        }

        public virtual void End()
        {
            StopListening(_entity);
            _isFinished = false;
        }

        protected void Finish()
        {
            _isFinished = true;
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void StartListening(IEntity entity)
        {

        }

        public virtual void StopListening(IEntity entity)
        {

        }
    }
}
