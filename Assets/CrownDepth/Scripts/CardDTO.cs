using System;
using UnityEngine;

namespace CrownDepth
{
    [Serializable]
    public class CardDTO
    {
        [SerializeField] public string cardDescription;
        [SerializeField] public Consequences consequences;
    }
}