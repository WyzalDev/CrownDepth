using System.Collections;
using CrownDepth.Incidents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WyzalUtilities.Audio;

namespace CrownDepth
{
    public class ChoiceUIController : MonoBehaviour
    {
        [Header("Card One")]
        [SerializeField] private Button choiceButtonOne;
        [SerializeField] private TMP_Text choiceButtonOneText;

        [Header("Card Two")]        
        [SerializeField] private Button choiceButtonTwo;
        [SerializeField] private TMP_Text choiceButtonTwoText;

        private const string CHOOSE_CARD_SOUND = "CardChoosed";
        private const string DISPLAY_CARD_SOUND = "DisplayCard";

        public IEnumerator DisplayCards(string cardOneText, string cardTwoText)
        {
            AudioContext.PlayGlobalSfx(DISPLAY_CARD_SOUND);
            choiceButtonOneText.text = cardOneText;
            choiceButtonTwoText.text = cardTwoText;
            gameObject.SetActive(true);
            yield break;
        }

        public IEnumerator RemoveCards(ChoosenCard choosenCard)
        {
            gameObject.SetActive(false);
            yield break;
        }
        
        public void ResetChoiceUI()
        {
            choiceButtonOneText.text = "";
            choiceButtonTwoText.text = "";
            gameObject.SetActive(false);
        }

        public void ChoiceCardOne()
        {
            AudioContext.PlayGlobalSfx(CHOOSE_CARD_SOUND);
            Incident.Choose(ChoosenCard.One);
        }

        public void ChoiceCardTwo()
        {
            AudioContext.PlayGlobalSfx(CHOOSE_CARD_SOUND);
            Incident.Choose(ChoosenCard.Two);
        }
    }
}