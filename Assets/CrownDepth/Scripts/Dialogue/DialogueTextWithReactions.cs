using UnityEngine;

namespace CrownDepth.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueTextWithReactions", menuName = "WyzalUtilities/Dialogue/DialogueTextWithReactions")]
    public class DialogueTextWithReactions : DialogueText
    {
        [TextArea(5, 10)] public string[] reactionOnFirstCard;
        [TextArea(5, 10)] public string[] reactionOnSecondCard;
        
        
    }
}