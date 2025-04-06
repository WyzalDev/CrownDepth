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

            var paralaxStageParam = Greed / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (Stats.Greed / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    Greed = Stats.Greed;
                    resultLimbs = LimbType.RightArm;
                }
            }

            paralaxStageParam = Gluttony / Stats.STAT_THRESHOLD;
            if (Stats.Gluttony / Stats.STAT_THRESHOLD > paralaxStageParam)
            {
                Gluttony = Stats.Gluttony;
                resultLimbs |= LimbType.Body;
            }
            
            paralaxStageParam = Pride / Stats.STAT_THRESHOLD;
            if (Stats.Pride / Stats.STAT_THRESHOLD > paralaxStageParam)
            {
                Pride = Stats.Pride;
                resultLimbs |= LimbType.Horns;
            }
            
            paralaxStageParam = Envy / Stats.STAT_THRESHOLD;
            if (Stats.Envy / Stats.STAT_THRESHOLD > paralaxStageParam)
            {
                Envy = Stats.Envy;
                resultLimbs |= LimbType.LeftArm;
            }
            
            paralaxStageParam = Fury / Stats.STAT_THRESHOLD;
            if (Stats.Fury / Stats.STAT_THRESHOLD > paralaxStageParam)
            {
                Fury = Stats.Fury;
                resultLimbs |= LimbType.Face;
            }
            
            SetStats();
            return resultLimbs;
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