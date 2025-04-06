using System;

namespace CrownDepth
{
    public static class EventManager
    {
        public static Action OnGameEnded;

        public static Action OnGameRestarted;

        public static Action OnIncidentStarted;
        
        public static Action OnIncidentEnded;

        public static Action OnMaxStatIncreased;
        
        public static void InvokeGameEnd() => OnGameEnded?.Invoke();
        public static void InvokeGameRestart() => OnGameRestarted?.Invoke();
        public static void InvokeMaxStatIncrease() => OnMaxStatIncreased?.Invoke();
        public static void InvokeStartIncident() => OnIncidentStarted?.Invoke();
        public static void InvokeEndIncident() => OnIncidentEnded?.Invoke();
    }
}