using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrownDepth.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI npcNameText;

        [SerializeField] private TextMeshProUGUI npcDialogueText;

        [SerializeField] private float typeSpeed;

        private Queue<string> _paragraphs = new Queue<string>();

        private bool _conversationEnded;

        private bool _isTyping;

        private string _p;

        private Coroutine _typeDialogCoroutine;

        private const string HTML_ALPHA = "<color=#00000000>";

        private const float MAX_TYPE_TIME = 0.1f;

        private WaitForSeconds _typeDialogWait = new WaitForSeconds(MAX_TYPE_TIME);

        private void Start()
        {
            _typeDialogWait = new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        public void ResetDialogueUI()
        {
            npcNameText.text = "";
            npcDialogueText.text = "";
            gameObject.SetActive(false);
        } 

        public void DisplayNextParagraph(DialogueText dialogue)
        {
            // if nothing in the queue
            if (_paragraphs.Count == 0)
                if (!_conversationEnded)
                {
                    StartConversation(dialogue);
                }
                else if (_conversationEnded && !_isTyping)
                {
                    EndConversation(dialogue);
                    return;
                }

            //if there is something in queue
            if (!_isTyping)
            {
                _p = _paragraphs.Dequeue();
                _typeDialogCoroutine = StartCoroutine(TypeDialogueText(_p));
            }
            else
            {
                FinishParagraphEarly();
            }

            //update conversation text
            //npcDialogueText.text = _p;

            if (_paragraphs.Count == 0) _conversationEnded = true;
        }

        private void StartConversation(DialogueText dialogue)
        {
            //activate dialogue box
            if (!gameObject.activeSelf) gameObject.SetActive(true);

            //update speaker name
            npcNameText.text = dialogue.speakerName;

            //add dialogue to the queue
            foreach (var text in dialogue.paragraphs)
            {
                _paragraphs.Enqueue(text);
            }
        }

        private void EndConversation(DialogueText dialogue)
        {
            //clear the queue
            _paragraphs.Clear();

            //return bool to false
            _conversationEnded = false;
            
            if(gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        private IEnumerator TypeDialogueText(string text)
        {
            _isTyping = true;

            var maxVisibleChars = 0;

            npcDialogueText.text = text;
            npcDialogueText.maxVisibleCharacters = maxVisibleChars;        

            foreach (var c in text)
            {

                maxVisibleChars++;
                npcDialogueText.maxVisibleCharacters = maxVisibleChars;

                yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
            }

            _isTyping = false;
        }

        private void FinishParagraphEarly()
        {
            //stop the coroutine
            StopCoroutine(_typeDialogCoroutine);

            //finish displaying text
            npcDialogueText.text = _p;

            //update _isTyping bool
            _isTyping = false;
        }
    }
}