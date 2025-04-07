using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrownDepth.Stat;
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
            EventManager.OnResetGameScene += ResetCoroutine;
            if (_incidents.Count() == 0) return;
            StartCoroutine(ExecuteIncidents());
        }
        
        private IEnumerator ExecuteIncidents()
        {
            AudioContext.ClearMusic();
            yield return AudioContext.TurnOnSource(0.2f);
            
            //play first stage music
            AudioContext.PlayGlobalMusic("Hills");
            
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