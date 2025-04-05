using UnityEngine;

namespace CrownDepth.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueText", menuName = "WyzalUtilities/Dialogue/DialogueText")]
    public class DialogueText : ScriptableObject
    {
        public string speakerName;

        [TextArea(5, 10)] public string[] paragraphs;

        private void Init(string speakerName, string[] paragraphs)
        {
            this.speakerName = speakerName;
            this.paragraphs = paragraphs;
        }
        
        public static DialogueText CreateInstance(string speakerName, string[] paragraphs)
        {
            var data = ScriptableObject.CreateInstance<DialogueText>();
            data.Init(speakerName, paragraphs);
            return data;
        }
        
        public bool IsExists(int currentParagraphIndex)
        {
            return currentParagraphIndex < paragraphs.Length;
        }
        
        public bool IsPreLastParagraph(int currentParagraphIndex)
        {
            return currentParagraphIndex == paragraphs.Length - 2;
        }
    }
}