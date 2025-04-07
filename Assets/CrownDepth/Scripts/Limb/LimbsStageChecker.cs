using System;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth.Limb
{
    public class LimbsStageChecker : MonoBehaviour
    {
        private float Greed = 0;
        private float Gluttony = 0;
        private float Pride = 0;
        private float Envy = 0;
        private float Fury = 0;

        public LimbType CheckLimbsStage()
        {
            LimbType resultLimbs = LimbType.None;

            var paralaxStageParam = MathF.Truncate(Greed / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (MathF.Truncate(Stats.Greed / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    Greed = Stats.Greed;
                    resultLimbs = LimbType.RightArm;
                }
            }

            paralaxStageParam = MathF.Truncate(Gluttony / Stats.STAT_THRESHOLD);
            if (MathF.Truncate(Stats.Gluttony / Stats.STAT_THRESHOLD) > paralaxStageParam)
            {
                Gluttony = Stats.Gluttony;
                resultLimbs |= LimbType.Body;
            }
            
            paralaxStageParam = MathF.Truncate(Pride / Stats.STAT_THRESHOLD);
            if (MathF.Truncate(Stats.Pride / Stats.STAT_THRESHOLD) > paralaxStageParam)
            {
                Pride = Stats.Pride;
                resultLimbs |= LimbType.Horns;
            }
            
            paralaxStageParam = MathF.Truncate(Envy / Stats.STAT_THRESHOLD);
            if (MathF.Truncate(Stats.Envy / Stats.STAT_THRESHOLD) > paralaxStageParam)
            {
                Envy = Stats.Envy;
                resultLimbs |= LimbType.LeftArm;
            }
            
            paralaxStageParam = MathF.Truncate(Fury / Stats.STAT_THRESHOLD);
            if (MathF.Truncate(Stats.Fury / Stats.STAT_THRESHOLD) > paralaxStageParam)
            {
                Fury = Stats.Fury;
                resultLimbs |= LimbType.Face;
            }
            
            SetStats();
            return resultLimbs;
        }

        public void ResetStats()
        {
            Greed = 0;
            Gluttony = 0;
            Pride = 0;
            Envy = 0;
            Fury = 0;
        }

        private void SetStats()
        {
            Greed = Stats.Greed;
            Gluttony = Stats.Gluttony;
            Pride = Stats.Pride;
            Envy = Stats.Envy;
            Fury = Stats.Fury;
        }

        private bool CheckParameter(float currentParam, float paralaxParam)
        {
            return currentParam > paralaxParam;
        }
    }

    [Flags]
    public enum LimbType
    {
        None = 0,
        RightArm = 1,
        LeftArm = 2,
        Body = 4,
        Horns = 8,
        Face = 16
    }
}