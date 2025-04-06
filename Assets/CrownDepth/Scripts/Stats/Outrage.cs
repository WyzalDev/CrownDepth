using UnityEngine;

namespace CrownDepth.Stat
{
    public static class Outrage
    {
        private static float _value = 0;
        
        private const float STAGE_THRESHOLD = 25f;
        private const float MAX_OUTRAGE_VALUE = 100f;

        public static void ChangeValue(float value)
        {
            if(value <= 0) return;
            
            var currentOutrage = _value / STAGE_THRESHOLD;
            var resultValue = Mathf.Clamp(_value + value, 0, MAX_OUTRAGE_VALUE);
            
            if(currentOutrage < resultValue/STAGE_THRESHOLD || Mathf.Approximately(resultValue, 100f)) EventManager.InvokeOutrageStageIncrease();
            
            _value = resultValue;
        }
    }
}