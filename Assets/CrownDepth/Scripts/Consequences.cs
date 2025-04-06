using System;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth
{
    [Serializable]
    public class Consequences
    {
        [SerializeField] public float Greed;
        [SerializeField] public float Gluttony;
        [SerializeField] public float Pride;
        [SerializeField] public float Envy;
        [SerializeField] public float Fury;

        public void ApplyConsequences()
        {
             Stats.ChangeStats(Greed, Gluttony, Pride, Envy, Fury);
        }
    }
}