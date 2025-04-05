using System.Collections;
using CrownDepth.Incidents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public IEnumerator DisplayCards(string cardOneText, string cardTwoText)
        {
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
            Incident.Choose(ChoosenCard.One);
        }

        public void ChoiceCardTwo()
        {
            Incident.Choose(ChoosenCard.Two);
        }
    }
}