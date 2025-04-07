using System;
using System.Collections;
using CrownDepth.Stat;
using DG.Tweening;
using UnityEngine;
using WyzalUtilities.Audio;

namespace CrownDepth.Paralax
{
    public class ParalaxNextStageChecker : MonoBehaviour
    {
        [SerializeField] private ParalaxViewController paralaxController;
        
        private const string SECOND_STAGE = "UnderGround";
        private const string THIRD_STAGE = "Caves";
        private const string FOURTH_STAGE = "Abyss";
        private const string FIFTH_STAGE = "Hell";

        private int stageAudio = 0;

        private float Greed = 0;
        private float Gluttony = 0;
        private float Pride = 0;
        private float Envy = 0;
        private float Fury = 0;


        public IEnumerator CheckNextStage()
        {
            var invocations = 0;
            var paralaxStageParam = MathF.Truncate(Greed / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (MathF.Truncate(Stats.Greed / Stats.STAT_THRESHOLD) > paralaxStageParam) invocations++;

                Greed = Stats.Greed;
            }

            paralaxStageParam = MathF.Truncate(Gluttony / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Gluttony, Gluttony))
            {
                if (MathF.Truncate(Stats.Gluttony / Stats.STAT_THRESHOLD) > paralaxStageParam) invocations++;

                Gluttony = Stats.Gluttony;
            }

            paralaxStageParam = MathF.Truncate(Pride / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Pride, Pride))
            {
                if (MathF.Truncate(Stats.Pride / Stats.STAT_THRESHOLD) > paralaxStageParam) invocations++;

                Pride = Stats.Pride;
            }

            paralaxStageParam = MathF.Truncate(Envy / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Envy, Envy))
            {
                if (MathF.Truncate(Stats.Envy / Stats.STAT_THRESHOLD) > paralaxStageParam) invocations++;

                Envy = Stats.Envy;
            }

            paralaxStageParam = MathF.Truncate(Fury / Stats.STAT_THRESHOLD);
            if (CheckParameter(Stats.Fury, Fury))
            {
                if (MathF.Truncate(Stats.Fury / Stats.STAT_THRESHOLD) > paralaxStageParam) invocations++;

                Fury = Stats.Fury;
            }

            if (invocations == 0) yield break;

            yield return AudioContext.TurnOffGlobalMusic();
            yield return ApplyNextStage();
            yield return ChangeNextMusic();

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

        private IEnumerator ChangeNextMusic()
        {
            var fadeSettings = new FadeSettings()
            {
                durationIn = 0.4f,
                durationOut = 0.1f,
                easeIn = Ease.InOutQuart,
                easeOut = Ease.InOutQuart,
            };
            switch (stageAudio)
            {
                case 0:
                    yield return AudioContext.PlayGlobalMusic(SECOND_STAGE, fadeSettings).WaitForCompletion();
                    break;
                case 1:
                    yield return AudioContext.PlayGlobalMusic(THIRD_STAGE, fadeSettings).WaitForCompletion();
                    break;
                case 2:
                    yield return AudioContext.PlayGlobalMusic(FOURTH_STAGE, fadeSettings).WaitForCompletion();
                    break;
                default:
                    yield return AudioContext.PlayGlobalMusic(FIFTH_STAGE, fadeSettings).WaitForCompletion();
                    break;
            }

            stageAudio++;
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