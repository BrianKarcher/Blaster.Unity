using BlueOrb.Base.Global;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Components;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Manager;
using BlueOrb.Controller.Scene;
using TMPro;
using UnityEngine;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("BlueOrb/Components/UI/Level Detail Panel")]
    public class LevelDetailPanel : ComponentBase<LevelDetailPanel>
    {
        [SerializeField]
        private GameObject currentScore;

        [SerializeField]
        private GameObject pb;

        [SerializeField]
        private TextMeshProUGUI levelDetail;

        [SerializeField]
        private GameObject newHighScore;

        public override void OnEnable()
        {
            base.OnEnable();
            newHighScore.SetActive(GlobalStatic.NewHighScore);
            SceneConfig currentSceneConfig = GlobalStatic.NextSceneConfig;
            int currentScore = EntityContainer.Instance.LevelStateController.GetCurrentScore();
            if (currentSceneConfig != null && !string.IsNullOrEmpty(currentSceneConfig.UniqueId))
            {
                TextMeshProUGUI pbText = this.pb.GetComponent<TextMeshProUGUI>();
                pbText.text = GameStateController.Instance.GetHighScore(currentSceneConfig.UniqueId).ToString();
            }
            if (currentScore != 0)
            {
                this.currentScore.SetActive(true);
                TextMeshProUGUI currentScoreText = this.currentScore.GetComponent<TextMeshProUGUI>();
                currentScoreText.text = currentScore.ToString();
            }
            else
            {
                this.currentScore.SetActive(false);
            }
            if (GlobalStatic.NewHighScore)
            {
                GlobalStatic.NewHighScore = false;
                newHighScore.SetActive(true);
                Debug.Log($"(LevelDetailPanel) New High Score!");
            }
            else
            {
                newHighScore.SetActive(false);
                Debug.Log($"(LevelDetailPanel) No New High Score :(");
            }
        }
    }
}
