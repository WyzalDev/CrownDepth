using UnityEngine;

namespace CrownDepth.Stat
{
    public static class Stats
    {
        public static float Greed;
        public static float Gluttony;
        public static float Pride;
        public static float Envy;
        public static float Fury;
        
        private const float MAX_STAT_POINT = 100;
        
        public const float STAT_THRESHOLD = 33;

        public static void ChangeStats(float greed, float gluttony, float pride, float envy, float fury)
        {
            var MaxStatIncreaseInvokes = 0;

            if (Mathf.Approximately(Greed, MAX_STAT_POINT) && greed > 0) MaxStatIncreaseInvokes++;
            Greed = Mathf.Clamp(Greed + greed, 0f, MAX_STAT_POINT);


            if (Mathf.Approximately(Gluttony, MAX_STAT_POINT) && gluttony > 0) MaxStatIncreaseInvokes++;
            Gluttony = Mathf.Clamp(Gluttony + gluttony, 0f, MAX_STAT_POINT);


            if (Mathf.Approximately(Pride, MAX_STAT_POINT) && pride > 0) MaxStatIncreaseInvokes++;
            Pride = Mathf.Clamp(Pride + pride, 0f, MAX_STAT_POINT);


            if (Mathf.Approximately(Envy, MAX_STAT_POINT) && envy > 0) MaxStatIncreaseInvokes++;
            Envy = Mathf.Clamp(Envy + envy, 0f, MAX_STAT_POINT);

            if (Mathf.Approximately(Fury, MAX_STAT_POINT) && fury > 0) MaxStatIncreaseInvokes++;
            Fury = Mathf.Clamp(Fury + fury, 0f, MAX_STAT_POINT);

            if (MaxStatIncreaseInvokes > 0) EventManager.InvokeMaxStatIncrease();
        }
    }
}