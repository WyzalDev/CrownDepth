using System.Collections;
using System.Collections.Generic;
using CrownDepth.Stat;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrownDepth.Handlers
{
    public class OutrageHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private List<string> outrageDescriptions;
        [SerializeField] private RectTransform outrageUI;
        [SerializeField] private float hideOrDisplayDuration;
        [SerializeField] private float onScreenTime;
        [SerializeField] private Ease hideOrDisplayEase;
        [SerializeField] private Vector2 hidePosAnchorMin;
        [SerializeField] private Vector2 hidePosAnchorMax;
        [SerializeField] private Vector2 displayPosAnchorMin;
        [SerializeField] private Vector2 displayPosAnchorMax;
        
        [Header("Slider")]
        [SerializeField] private Slider slider;
        [SerializeField] private List<float> sliderValues;
        [SerializeField] private float sliderAnimationDuration = 1f;
        [SerializeField] private Ease sliderEase;

        public Coroutine DisplayOutrageCoroutine { get; private set; }

        private const int MAX = 4;
        private int currentOutrage = 0;
        private WaitForSeconds cashed;

        private void Start()
        {
            DontDestroyOnLoad(this);
            cashed = new WaitForSeconds(onScreenTime);
            EventManager.OnOutrageStageIncreased += IncreaseOutrageStage;
        }

        private void IncreaseOutrageStage()
        {
            if (currentOutrage == MAX)
            {
                return;
            }
            if (currentOutrage + 1 == MAX)
            {
                EndGame.EndGameType = EndGameType.Loose;
                EventManager.InvokeGameEnd();
            }
            
            text.text = outrageDescriptions[currentOutrage];
            DisplayOutrageCoroutine = StartCoroutine(DisplayOutrageWindow());
        }
        
        private IEnumerator DisplayOutrageWindow()
        {
            outrageUI.gameObject.SetActive(true);
            
            var sequenceDisplay = DOTween.Sequence();
            yield return sequenceDisplay
                .Append(outrageUI.DOAnchorMin(displayPosAnchorMin, hideOrDisplayDuration)).SetEase(hideOrDisplayEase)
                .Join(outrageUI.DOAnchorMax(displayPosAnchorMax, hideOrDisplayDuration)).SetEase(hideOrDisplayEase)
                .WaitForCompletion();
            
            var nextSliderValue = sliderValues[currentOutrage];
            slider.DOValue(nextSliderValue, sliderAnimationDuration).SetEase(sliderEase);
            yield return cashed;
            
            var sequenceHide = DOTween.Sequence();
            yield return sequenceHide
                .Append(outrageUI.DOAnchorMin(hidePosAnchorMin, hideOrDisplayDuration)).SetEase(hideOrDisplayEase)
                .Join(outrageUI.DOAnchorMax(hidePosAnchorMax, hideOrDisplayDuration)).SetEase(hideOrDisplayEase)
                .WaitForCompletion();
            outrageUI.gameObject.SetActive(false);
            currentOutrage++;
        }

        public void ResetOutrage()
        {
            Outrage.Reset();
            currentOutrage = 0;
            slider.value = 0.23f;
            outrageUI.anchorMin = hidePosAnchorMin;
            outrageUI.anchorMax = hidePosAnchorMax;
        }
        
        private void OnDestroy()
        {
            EventManager.OnOutrageStageIncreased -= IncreaseOutrageStage;
        }
    }
}