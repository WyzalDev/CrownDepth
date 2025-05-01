using System.Collections;
using CrownDepth.SceneManagement;
using CrownDepth.Stat;
using UnityEngine;
using UnityEngine.UI;
using WyzalUtilities.Audio;

namespace CrownDepth
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private RectTransform mainPanel;
        [SerializeField] private Button restartButton;
        
        public void Restart()
        {
            
            StartCoroutine(RestartCoroutine());
        }

        private IEnumerator RestartCoroutine()
        {
            restartButton.interactable = false;
            yield return AudioContext.TurnOffGlobalSounds(1f);
            mainPanel.gameObject.SetActive(false);
            Stats.ResetStats();
            yield return SceneLoader.LoadScene(SceneName.Game);
        }
    }
}