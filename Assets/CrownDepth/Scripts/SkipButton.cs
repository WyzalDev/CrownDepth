using CrownDepth.Incidents;
using UnityEngine;

namespace CrownDepth
{
    public class SkipButton : MonoBehaviour
    {
        public void Skip() => Incident.Skip();
    }
}