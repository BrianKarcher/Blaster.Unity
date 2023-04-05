using UnityEngine;

namespace BlueOrb.Controller.Manager
{
    [AddComponentMenu("BlueOrb/Manager/GameController")]
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
