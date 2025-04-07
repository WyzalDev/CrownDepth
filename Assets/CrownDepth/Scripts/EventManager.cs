using System;

namespace CrownDepth
{
    public static class EventManager
    {
        public static Action OnGameEnded;

        public static Action OnGameRestarted;

        public static Action OnIncidentStarted;
        
        public static Action OnIncidentEnded;

        public static Action OnTastyDemon;

        public static Action OnOutrageStageIncreased;

        public static Action OnResetGameScene;
        
        public static void InvokeGameEnd() => OnGameEnded?.Invoke();
        public static void InvokeGameRestart() => OnGameRestarted?.Invoke();
        public static void InvokeTastyDemon() => OnTastyDemon?.Invoke();
        public static void InvokeStartIncident() => OnIncidentStarted?.Invoke();
        public static void InvokeEndIncident() => OnIncidentEnded?.Invoke();
        
        public static void InvokeOutrageStageIncrease() => OnOutrageStageIncreased?.Invoke();
        
        public static void InvokeResetGameScene() => OnResetGameScene?.Invoke();
    }
}