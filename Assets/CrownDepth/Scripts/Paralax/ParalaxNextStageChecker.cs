﻿using System.Collections;
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
            var paralaxStageParam = Greed / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (Stats.Greed / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Greed = Stats.Greed;
            }

            paralaxStageParam = Gluttony / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Gluttony, Gluttony))
            {
                if (Stats.Gluttony / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Gluttony = Stats.Gluttony;
            }

            paralaxStageParam = Pride / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Pride, Pride))
            {
                if (Stats.Pride / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Pride = Stats.Pride;
            }

            paralaxStageParam = Envy / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Envy, Envy))
            {
                if (Stats.Envy / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Envy = Stats.Envy;
            }

            paralaxStageParam = Fury / Stats.STAT_THRESHOLD;
            if (CheckParameter(Stats.Fury, Fury))
            {
                if (Stats.Fury / Stats.STAT_THRESHOLD > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }

                Fury = Stats.Fury;
            }
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