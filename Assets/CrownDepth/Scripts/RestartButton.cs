using CrownDepth.SceneManagement;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            Stats.ResetStats();
            SceneLoader.LoadScene(SceneName.Game);
        }
    }
}