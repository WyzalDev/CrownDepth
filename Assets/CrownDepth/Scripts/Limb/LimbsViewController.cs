using System;
using UnityEngine;
using UnityEngine.UI;

namespace CrownDepth.Limb
{
    public class LimbsViewController : MonoBehaviour
    {
        [SerializeField] private LimbsStageChecker limbsStageChecker;
        
        [Header("limbs image settings")]
        [SerializeField] private LimbStageView horns;
        [SerializeField] private LimbStageView body;
        [SerializeField] private LimbStageView face;
        [SerializeField] private LimbStageView leftArm;
        [SerializeField] private LimbStageView rightArm;

        public void TryGrowLimbs()
        {
            var limbType = limbsStageChecker.CheckLimbsStage();
            switch (limbType)
            {
                case LimbType.RightArm:
                    rightArm.ActivateNextStage();
                    break;
                case LimbType.LeftArm:
                    leftArm.ActivateNextStage();
                    break;
                case LimbType.Body:
                    body.ActivateNextStage();
                    break;
                case LimbType.Horns:
                    horns.ActivateNextStage();
                    break;
                case LimbType.Face:
                    face.ActivateNextStage();
                    break;
            }
        }

        public void ResetLimbs()
        {
            horns.ResetStages();
            body.ResetStages();
            face.ResetStages();
            leftArm.ResetStages();
            rightArm.ResetStages();
            limbsStageChecker.ResetStats();
        }

    }

    [Serializable]
    public class LimbStageView
    {
        [SerializeField] private Image stageOne;
        [SerializeField] private Image stageTwo;
        [SerializeField] private Image stageThree;
        
        [NonSerialized]
        public int CurrentStage = 0;

        private const int MAX_LIMB_STAGES = 3;
        
        public void ResetStages()
        {
            CurrentStage = 0;
            stageOne.gameObject.SetActive(false);
            stageTwo.gameObject.SetActive(false);
            stageThree.gameObject.SetActive(false);
        }

        public void ActivateNextStage()
        {
            if(CurrentStage >= MAX_LIMB_STAGES) return;
            CurrentStage++;
            switch (CurrentStage)
            {
                case 1:
                    stageOne.gameObject.SetActive(true);
                    stageTwo.gameObject.SetActive(false);
                    stageThree.gameObject.SetActive(false);
                    break;
                case 2:
                    stageOne.gameObject.SetActive(false);
                    stageTwo.gameObject.SetActive(true);
                    stageThree.gameObject.SetActive(false);
                    break;
                case 3:
                    stageOne.gameObject.SetActive(false);
                    stageTwo.gameObject.SetActive(false);
                    stageThree.gameObject.SetActive(true);
                    break;
            }
        }
    }
}