using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrownDepth.Dialogue;
using CrownDepth.Infrastructure;
using UnityEngine;
using WyzalUtilities.Audio;

namespace CrownDepth.Incidents
{
    public class IncidentController : MonoBehaviour
    {
        [SerializeField] private List<Incident> _incidents;
        [SerializeField] private int currentIncidentIndex = 0;

        private void Start()
        {
            //AudioContext.PlayGlobalMusic("crocodile");
            if (_incidents.Count() == 0) return;
            ResetUI();
            StartCoroutine(ExecuteIncidents());
        }

        private IEnumerator ExecuteIncidents()
        {
            //Play cycle
            while (_incidents.Count() > currentIncidentIndex)
            {
                yield return _incidents[currentIncidentIndex].Execute();
                currentIncidentIndex++;
            }

            //End of the game
            EventManager.InvokeGameEnd();
        }

        private void ResetUI()
        {
            if (!ServiceLocatorMono.Instance.TryGetService<DialogueController>(out var dialogueController)) return;
            if (!ServiceLocatorMono.Instance.TryGetService<ChoiceUIController>(out var choiceController)) return;
            dialogueController.ResetDialogueUI();
            choiceController.ResetChoiceUI();
        }
    }
}