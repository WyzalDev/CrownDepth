using System.Collections;
using System.Collections.Generic;
using CrownDepth.Stat;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour
{
    
    [Header("Title text")]
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private string LooseTitleText;
    [SerializeField] private string WinTitleText;
    [SerializeField] private string LooseByOldnessTitleText;
    
    [Header("Stats Values text")]
    [SerializeField] private TMP_Text Greed;
    [SerializeField] private TMP_Text Gluttony;
    [SerializeField] private TMP_Text Pride;
    [SerializeField] private TMP_Text Envy;
    [SerializeField] private TMP_Text Fury;
    
    [Header("Dignity")]
    [SerializeField] private RectTransform dignity;
    [SerializeField] private RectTransform GreedDignity;
    [SerializeField] private RectTransform GluttonyDignity;
    [SerializeField] private RectTransform PrideDignity;
    [SerializeField] private RectTransform EnvyDignity;
    [SerializeField] private RectTransform FuryDignity;
    private RectTransform chosenDignity;

    [Header("Statistics Elements")]
    [SerializeField] private RectTransform MainPanel;
    [SerializeField] private List<RectTransform> statisticsElements;
    [SerializeField] private RectTransform ScoreCounterElement;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private float counterAnimationDuration = 0.2f;
    private float scoreCount = 0;
    [SerializeField] private float waitBetweenStatisticsElements;
    [SerializeField] private float startSizeOfAnimatedElements;
    [SerializeField] private Ease animationEase;
    
    
    [Header("Restart")]
    [SerializeField] private RectTransform RestartRect;
    [SerializeField] private Button RestartGame;

    void Start()
    {
        switch (EndGame.EndGameType)
        {
            case EndGameType.Win:
                TitleText.text = WinTitleText;
                break;
            case EndGameType.LooseByOldness:
                TitleText.text = LooseByOldnessTitleText;
                break;
            default:
                TitleText.text = LooseTitleText;
                break;
        }

        CountAmount();
        SetStats();
        SetDignity();
        StartCoroutine(EndGameStatsListAnimation());
    }
    
    private IEnumerator EndGameStatsListAnimation()
    {
        var cachedWait = new WaitForSeconds(waitBetweenStatisticsElements);
        MainPanel.gameObject.SetActive(true);
        yield return cachedWait;
        
        foreach (var element in statisticsElements)
        {
            element.gameObject.SetActive(true);
            yield return cachedWait;
        }

        yield return CounterAnimation(counterAnimationDuration);
        
        dignity.gameObject.SetActive(true);
        chosenDignity.gameObject.SetActive(true);
        yield return cachedWait;
        
        RestartRect.gameObject.SetActive(true);
        RestartGame.gameObject.SetActive(true);
    }

    private IEnumerator CounterAnimation(float duration)
    {
        ScoreText.text = scoreCount.ToString();
        ScoreCounterElement.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitBetweenStatisticsElements);
        ScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitBetweenStatisticsElements);
    }
    
    private void CountAmount()
    {
        scoreCount += Stats.Greed;
        scoreCount += Stats.Gluttony;
        scoreCount += Stats.Pride;
        scoreCount += Stats.Envy;
        scoreCount += Stats.Fury;
    }
    
    private void SetDignity()
    {
        var maxStat = Stats.Greed;
        chosenDignity = GreedDignity;
        
        if (Stats.Gluttony > maxStat)
        {
            maxStat = Stats.Gluttony;
            chosenDignity = GluttonyDignity;
        }
        
        if (Stats.Pride > maxStat)
        {
            maxStat = Stats.Pride;
            chosenDignity = PrideDignity;
        }
        
        if (Stats.Envy > maxStat)
        {
            maxStat = Stats.Envy;
            chosenDignity = EnvyDignity;
        }

        if (Stats.Fury > maxStat)
        {
            maxStat = Stats.Fury;
            chosenDignity = FuryDignity;
        }
    }
    
    private void SetStats()
    {
        Greed.text = Stats.Greed.ToString();
        Gluttony.text = Stats.Gluttony.ToString();
        Pride.text = Stats.Pride.ToString();
        Envy.text = Stats.Envy.ToString();
        Fury.text = Stats.Fury.ToString();
    }
}