using System;
using System.Collections;
using CrownDepth;
using CrownDepth.Stat;
using DG.Tweening;
using UnityEngine;

public class ParalaxViewController : MonoBehaviour
{
    [SerializeField] private RectTransform _paralax;
    [SerializeField] private float moveParalaxDuration = 1f;
    [Header("Stages")]
    //[SerializeField] private AnchorsData _stageZero;
    [SerializeField] private AnchorsData _stageOne;
    [SerializeField] private AnchorsData _stageTwo;
    [SerializeField] private AnchorsData _stageThree;
    [SerializeField] private AnchorsData _stageFour;
    [SerializeField] private AnchorsData _stageFive;
    private const int MAX_STAGES = 5;
    
    private int currentStage = 1;

    public IEnumerator NextStage()
    {
        if (currentStage == MAX_STAGES)
        {
            EndGame.EndGameType = EndGameType.Win;
            EventManager.InvokeGameEnd();
            yield break;
        }
        currentStage++;
        yield return MoveParalax();
    }

    private IEnumerator MoveParalax()
    {
        float max = 0;
        float min = 0;
        //find min and max y anchor positions
        switch (currentStage)
        {
            case 1:
                max = _stageOne._anchorYMax;
                min = _stageOne._anchorYMin;
                break;
            case 2:
                max = _stageTwo._anchorYMax;
                min = _stageTwo._anchorYMin;
                break;
            case 3:
                max = _stageThree._anchorYMax;
                min = _stageThree._anchorYMin;
                break;
            case 4:
                max = _stageFour._anchorYMax;
                min = _stageFour._anchorYMin;
                break;
            default:
                max = _stageFive._anchorYMax;
                min = _stageFive._anchorYMin;
                break;
        }
        Vector2 maxAnchor = new Vector2(_paralax.anchorMax.x, max);
        Vector2 minAnchor = new Vector2(_paralax.anchorMin.x, min);

        //Create Sequence to move paralax
        var sequence = DOTween.Sequence();
        sequence.Append(_paralax.DOAnchorMax(maxAnchor, moveParalaxDuration)).SetEase(Ease.InOutQuart)
            .Join(_paralax.DOAnchorMin(minAnchor, moveParalaxDuration)).SetEase(Ease.InOutQuart);
        
        //Wait until paralax moved to current position
        yield return sequence.WaitForCompletion();
    }

    public void ResetParalax()
    {
        currentStage = 1;
        _paralax.anchorMin = new Vector2(_paralax.anchorMin.x, _stageOne._anchorYMin);
        _paralax.anchorMax = new Vector2(_paralax.anchorMax.x, _stageOne._anchorYMax);
    }
    
}

[Serializable]
public class AnchorsData
{
    [SerializeField] public float _anchorYMin;
    [SerializeField] public float _anchorYMax;
} 
