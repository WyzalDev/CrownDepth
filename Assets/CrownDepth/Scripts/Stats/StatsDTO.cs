namespace CrownDepth.Stats
{
    public static class Stats
    {
        public static float Greed;
        public static float Gluttony;
        public static float Pride;
        public static float Envy;
        public static float Fury;

        public static void ChangeStats(float value, StatType statType)
        {
            StatsEventManager.InvokeOnStatsChange();
            switch (statType)
            {
                case StatType.Greed:
                    Greed = value;
                    break;
                case StatType.Gluttony:
                    Gluttony = value;
                    break;
                case StatType.Pride:
                    Pride = value;
                    break;
                case StatType.Envy:
                    Envy = value;
                    break;
                case StatType.Fury:
                    Fury = value;
                    break;
            }
        }
    }

    public enum StatType
    {
        Greed,
        Gluttony,
        Pride,
        Envy,
        Fury
    }
}