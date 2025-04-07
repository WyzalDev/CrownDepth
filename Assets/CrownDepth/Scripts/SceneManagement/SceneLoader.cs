using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrownDepth.SceneManagement
{
    public static class SceneLoader
    {
        
        private static Task cachedTask = Task.Delay(100); 
        
        public static async void LoadScene(SceneName sceneName) {
            var scene = SceneManager.LoadSceneAsync(sceneName.ToString());
            if (scene is not null)
            {
                scene.allowSceneActivation = false;
            }
            else
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded");
                return;
            }
            do
            {
                await cachedTask;
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