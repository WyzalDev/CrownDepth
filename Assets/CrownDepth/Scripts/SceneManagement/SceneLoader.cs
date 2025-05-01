using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrownDepth.SceneManagement
{
    public static class SceneLoader
    {
        
        public static IEnumerator LoadScene(SceneName sceneName) {
            var scene = SceneManager.LoadSceneAsync(sceneName.ToString());
            if (scene is not null)
            {
                scene.allowSceneActivation = false;
            }
            else
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded");
                yield break;
            }
            
            do
            {
                yield return null;
            } while (scene.progress < 0.9f);
            scene.allowSceneActivation = true;
        }
    }
    
    public enum SceneName
    {
        Game,
        Results
    }
}