using System.Collections;
using CrownDepth.Dialogue;
using CrownDepth.Infrastructure;
using CrownDepth.Limb;
using CrownDepth.Paralax;
using CrownDepth.SceneManagement;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CrownDepth.Handlers
{
    public class EndGameHandler : MonoBehaviour
    {
        [SerializeField] private Image _globalFade;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private Ease _fadeEase;
        private void Start()
        {
            EventManager.OnGameEnded += HandleEndGame;
        }

        private void HandleEndGame()
        {
            StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            _globalFade.gameObject.SetActive(true);
            
            //ToDo fade screen and audio
            
            EventManager.InvokeResetGameScene();
            
            //Wait display outrage coroutine if exists
            if (ServiceLocatorMono.Instance.TryGetService<OutrageHandler>(out OutrageHandler outrageHandler))
            {
                if (outrageHandler.DisplayOutrageCoroutine != null)
                {
                    yield return outrageHandler.DisplayOutrageCoroutine;
                }
            }
            FullReset();

            //ChangeScene
            SceneLoader.LoadScene(SceneName.Results);
        }
        
        private void FullReset()
        {
            if (!ServiceLocatorMono.Instance.TryGetService<DialogueController>(out var dialogueController)) return;
            if (!ServiceLocatorMono.Instance.TryGetService<ChoiceUIController>(out var choiceController)) return;
            if(!ServiceLocatorMono.Instance.TryGetService<ParalaxNextStageChecker>(out var paralaxNextStageChecker)) return;
            if(!ServiceLocatorMono.Instance.TryGetService<OutrageHandler>(out var outrageHandler)) return;
            if(!ServiceLocatorMono.Instance.TryGetService<LimbsViewController>(out var limbsViewController)) return;
            
            paralaxNextStageChecker.ResetParalax();
            dialogueController.ResetDialogueUI();
            choiceController.ResetChoiceUI();
            outrageHandler.ResetOutrage();
            limbsViewController.ResetLimbs();
        }

        private void OnDestroy()
        {
            EventManager.OnGameEnded -= HandleEndGame;
        }
    }
}