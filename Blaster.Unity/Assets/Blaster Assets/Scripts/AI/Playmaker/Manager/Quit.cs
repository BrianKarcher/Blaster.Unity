using HutongGames.PlayMaker;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("Quit the game")]
    public class Quit : BasePlayMakerAction
    {
        public override void OnEnter()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            Finish();
        }
    }
}