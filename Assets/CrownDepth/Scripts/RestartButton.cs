using CrownDepth.SceneManagement;
using CrownDepth.Stat;
using UnityEngine;
using WyzalUtilities.Audio;

namespace CrownDepth
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            Stats.ResetStats();
            AudioContext.TurnOffGlobalSounds(0.1f);
            SceneLoader.LoadScene(SceneName.Game);
        }
    }
}