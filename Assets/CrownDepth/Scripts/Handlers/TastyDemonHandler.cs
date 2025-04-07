using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WyzalUtilities.Audio;

namespace CrownDepth.Handlers
{
    public class TastyDemonHandler : MonoBehaviour
    {
        [Header("Demon Settings")]
        [SerializeField] private Image _demonImage;
        [SerializeField] private float inOrOutDuration;
        [SerializeField] private float onScreenTime;
        [SerializeField] private Ease ease;
        
        [Header("Demon Anchors")]
        [SerializeField] private Vector2 hidePosAnchorMin;
        [SerializeField] private Vector2 hidePosAnchorMax;
        [SerializeField] private Vector2 displayPosAnchorMin;
        [SerializeField] private Vector2 displayPosAnchorMax;

        private WaitForSeconds cachedWait;
        private const string TOASTY_SOUND = "ToastyDemon";
        
        private void Start()
        {
            cachedWait = new WaitForSeconds(onScreenTime);
            EventManager.OnTastyDemon += RunTastyDemon;
        }

        private void RunTastyDemon()
        {
            StartCoroutine(DisplayTastyDemon());
        }

        private IEnumerator DisplayTastyDemon()
        {
            _demonImage.gameObject.SetActive(true);
            
            var displaySequence = DOTween.Sequence();
            
            yield return displaySequence.Append(_demonImage.rectTransform.DOAnchorMin(displayPosAnchorMin, inOrOutDuration).SetEase(ease))
                .Join(_demonImage.rectTransform.DOAnchorMax(displayPosAnchorMax, inOrOutDuration).SetEase(ease))
                .WaitForCompletion();
            
            AudioContext.PlayGlobalSfx(TOASTY_SOUND);
            yield return cachedWait;
            
            var hideSequence = DOTween.Sequence();
            
            yield return hideSequence.Append(_demonImage.rectTransform.DOAnchorMin(hidePosAnchorMin, inOrOutDuration).SetEase(ease))
                .Join(_demonImage.rectTransform.DOAnchorMax(hidePosAnchorMax, inOrOutDuration).SetEase(ease))
                .WaitForCompletion();
            
            _demonImage.gameObject.SetActive(false);
        }
        
        private void OnDestroy()
        {
            EventManager.OnTastyDemon -= RunTastyDemon;
        }
    }
}