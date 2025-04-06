using System.Collections;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth.Paralax
{
    public class ParalaxNextStageChecker : MonoBehaviour
    {
        [SerializeField] private ParalaxViewController paralaxController;
        [SerializeField] private float StageThreshold;

        
        private float Greed;
        private float Gluttony;
        private float Pride;
        private float Envy;
        private float Fury;


        public IEnumerator CheckNextStage()
        {
            var paralaxStageParam = Greed % StageThreshold;
            if (CheckParameter(Stats.Greed, Greed))
            {
                if (Stats.Greed/StageThreshold > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }
                Greed = Stats.Greed;
            }
            
            paralaxStageParam = Gluttony % StageThreshold;
            if (CheckParameter(Stats.Gluttony, Gluttony))
            {
                if (Stats.Gluttony % StageThreshold > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }
                Gluttony = Stats.Gluttony;
            }
            
            paralaxStageParam = Pride % StageThreshold;
            if (CheckParameter(Stats.Pride, Pride))
            {
                if (Stats.Pride % StageThreshold > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }
                Pride = Stats.Pride;
            }
            
            paralaxStageParam = Envy % StageThreshold;
            if (CheckParameter(Stats.Envy, Envy))
            {
                if (Stats.Envy % StageThreshold > paralaxStageParam)
                {
                    yield return ApplyNextStage();
                }
                Envy = Stats.Envy;
            }
            
            paralaxStageParam = Fury % StageThreshold;
            if (CheckParameter(Stats.Fury, Fury))
            {
                if (Stats.Fury % StageThreshold > paralaxStageParam)
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