using UnityEngine;

namespace CrownDepth.Incidents
{
    public class GameAction : ScriptableObject
    {
        [SerializeField] public ActionType actionType;
    }

    public enum ActionType
    {
        Dialogue,
        ChoiceAndDialogue,
    }
}