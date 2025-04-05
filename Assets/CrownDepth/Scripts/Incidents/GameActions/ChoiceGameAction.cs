using UnityEngine;

namespace CrownDepth.Incidents.GameActions
{
    [CreateAssetMenu(fileName = "ChoiceGameAction", menuName = "WyzalUtilities/CrownDepth/ChoiceGameAction")]
    public class ChoiceGameAction : DialogueGameAction
    {
        public CardDTO FirstCardDto;
        
        public CardDTO SecondCardDto;
    }
}