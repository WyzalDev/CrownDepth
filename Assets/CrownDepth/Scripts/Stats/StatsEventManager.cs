using System;

public static class StatsEventManager
{
    public static Action OnStatsChange;
    
    public static void InvokeOnStatsChange() => OnStatsChange?.Invoke();
}
