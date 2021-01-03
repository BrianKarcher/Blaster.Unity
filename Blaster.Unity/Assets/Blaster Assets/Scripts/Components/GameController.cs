using BlueOrb.Base.Global;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlueOrb.Controller.Manager
{
    [AddComponentMenu("RQ/Manager/GameController")]
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private string _firstScene;

        private void Awake()
        {
            // TODO: Move this into a "NewGame" state
            //GameStateController.Instance.BeginNewGame = true;
        }
    }
}
