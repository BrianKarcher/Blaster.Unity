using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public interface IAtomActionBase
    {
        bool IsFinished { get; }

        void End();
        void OnUpdate();
        void Start(IEntity entity);
        void StartListening(IEntity entity);
        void StopListening(IEntity entity);
    }
}