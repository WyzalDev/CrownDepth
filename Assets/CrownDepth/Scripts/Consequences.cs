using System;
using CrownDepth.Stat;
using UnityEngine;

namespace CrownDepth
{
    [Serializable]
    public class Consequences
    {
        [Header("Stats")]
        [SerializeField] public float Greed;
        [SerializeField] public float Gluttony;
        [SerializeField] public float Pride;
        [SerializeField] public float Envy;
        [SerializeField] public float Fury;

        [Header("Outrage")]
        [SerializeField] public float outrage;
        public void ApplyConsequences()
        {
             Stats.ChangeStats(Greed, Gluttony, Pride, Envy, Fury);
             Outrage.ChangeValue(outrage);
        }
    }
}