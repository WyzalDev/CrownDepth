using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrownDepth.Dialogue;
using CrownDepth.Handlers;
using CrownDepth.Infrastructure;
using CrownDepth.Limb;
using CrownDepth.Paralax;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth.Incidents
{
    public class IncidentController : MonoBehaviour
    {
        [SerializeField] private List<Incident> _incidents;
        [SerializeField] private int currentIncidentIndex = 0;

        private void Start()
        {
            EventManager.OnResetGameScene += ResetCoroutine;
            //AudioContext.PlayGlobalMusic("crocodile");
            if (_incidents.Count() == 0) return;
            StartCoroutine(ExecuteIncidents());
        }

        private IEnumerator ExecuteIncidents()
        {
            //Wait one frame until Start exits
            yield return null;
            
            //Play cycle
            while (_incidents.Count() > currentIncidentIndex)
            {
                yield return _incidents[currentIncidentIndex].Execute();
                currentIncidentIndex++;
            }

            //End of the game
            EndGame.EndGameType = EndGameType.LooseByOldness;
            EventManager.InvokeGameEnd();
        }

        private void ResetCoroutine()
        {
            StopCoroutine(ExecuteIncidents());
            currentIncidentIndex = 0;
        }

        private void OnDestroy()
        {
            EventManager.OnResetGameScene -= ResetCoroutine;
        }
    }
}