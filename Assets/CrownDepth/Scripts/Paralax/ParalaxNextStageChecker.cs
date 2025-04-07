using System;
using System.Collections;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth.Paralax
{
    public class ParalaxNextStageChecker : MonoBehaviour
    {
        [SerializeField] private ParalaxViewController paralaxController;

        private float Greed = 0;
        private float Gluttony = 0;
        private float Pride = 0;
        private float Envy = 0;
        private float Fury = 0;


        public IEnumerator CheckNextStage()
        {
            var paralaxStageParam = MathF.Truncate(Greed / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (MathF.Truncate(Stats.Greed / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Greed = Stats.Greed;
            }

            paralaxStageParam = MathF.Truncate(Gluttony / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Gluttony, Gluttony))
            {
                if (MathF.Truncate(Stats.Gluttony / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Gluttony = Stats.Gluttony;
            }

            paralaxStageParam = MathF.Truncate(Pride / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Pride, Pride))
            {
                if (MathF.Truncate(Stats.Pride / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Pride = Stats.Pride;
            }

            paralaxStageParam = MathF.Truncate(Envy / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Envy, Envy))
            {
                if (MathF.Truncate(Stats.Envy / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Envy = Stats.Envy;
            }

            paralaxStageParam = MathF.Truncate(Fury / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Fury, Fury))
            {
                if (MathF.Truncate(Stats.Fury / Stats.STAT_THRESHOLD) > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Fury = Stats.Fury;
            }
        }

        public void ResetParalax()
        {
            paralaxController.ResetParalax();
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

        private IEnumerator ApplyNextStage()
        {
            SetStats();
            yield return paralaxController.NextStage();
        }
    }
}