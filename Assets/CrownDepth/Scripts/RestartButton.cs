using System.Collections;
using CrownDepth.SceneManagement;
using CrownDepth.Stat;
using UnityEngine;
using WyzalUtilities.Audio;

namespace CrownDepth
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private RectTransform mainPanel;
        
        public void Restart()
        {
            mainPanel.gameObject.SetActive(false);
            StartCoroutine(RestartCoroutine());
        }

        private IEnumerator RestartCoroutine()
        {
            yield return AudioContext.TurnOffGlobalSounds(1.25f);
            Stats.ResetStats();
            SceneLoader.LoadScene(SceneName.Game);
        }
    }
}