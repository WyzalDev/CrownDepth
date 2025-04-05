using CrownDepth.Dialogue;
using UnityEngine;

namespace CrownDepth.Incidents.GameActions
{
    [CreateAssetMenu(fileName = "DialogueGameAction", menuName = "WyzalUtilities/CrownDepth/DialogueGameAction")]
    public class DialogueGameAction : GameAction
    {
        [SerializeField] public DialogueText dialogueText;
        [SerializeField] public NPCType npcType;
        [SerializeField] public Consequences consequence;
        
    }
}