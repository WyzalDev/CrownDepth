using System;
using CrownDepth.Stats;
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
             Stats.Stats.ChangeStats(Greed, StatType.Greed);
             Stats.Stats.ChangeStats(Gluttony, StatType.Gluttony);
             Stats.Stats.ChangeStats(Pride, StatType.Pride);
             Stats.Stats.ChangeStats(Envy, StatType.Envy);
             Stats.Stats.ChangeStats(Fury, StatType.Fury);
             
        }
    }
}