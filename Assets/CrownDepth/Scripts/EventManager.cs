using System;

namespace CrownDepth
{
    public static class EventManager
    {
        public static Action OnGameEnded;

        public static Action OnIncidentStarted;
        
        public static Action OnIncidentEnded;
        
        
        public static void InvokeGameEnd() => OnGameEnded?.Invoke();
        public static void InvokeStartIncident() => OnIncidentStarted?.Invoke();
        public static void InvokeEndIncident() => OnIncidentEnded?.Invoke();
    }
}