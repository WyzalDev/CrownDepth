﻿using System;
using System.Collections;
using System.Collections.Generic;
using CrownDepth.Dialogue;
using CrownDepth.Incidents.GameActions;
using CrownDepth.Infrastructure;
using CrownDepth.Limb;
using CrownDepth.Paralax;
using CrownDepth.Stat;
using UnityEngine;
using WyzalUtilities.Audio;

namespace CrownDepth.Incidents
{
    [Serializable]
    public class Incident
    {
        [SerializeField] private string name;
        [SerializeField] private GameAction action;
        [SerializeField] private NPCType npcType;
        private float waitBetweenIncidents = 0.5f;

        private static bool isSkipped = false;

        private static bool canBeSkipped = false;

        private static bool isChoosen = false;

        private static bool canBeChoosen = false;

        private static ChoosenCard choosenCard = ChoosenCard.One;

        private const string CLICK_SOUND = "Click";

        public IEnumerator Execute()
        {
            EventManager.InvokeStartIncident();

            yield return PlayStepsSound();

            yield return action.actionType switch
            {
                ActionType.Dialogue => ExecuteDialogue(action),
                ActionType.ChoiceAndDialogue => ExecuteChoiceAndDialogue(action)
            };

            yield return PlayStepsSound();
            
            if (ServiceLocatorMono.Instance.TryGetService<ParalaxNextStageChecker>(out var paralaxNextStageChecker))
            {
                yield return paralaxNextStageChecker.CheckNextStage();
            }
            
            yield return new WaitForSeconds(waitBetweenIncidents);
            EventManager.InvokeEndIncident();
        }

        #region DIALOGUE AND CHOICE WITH DIALOGUE EXECUTIONS

        private IEnumerator ExecuteDialogue(GameAction action)
        {
            var dialogueAction = action as DialogueGameAction;
            if (dialogueAction == null) yield break;
            if (!ServiceLocatorMono.Instance.TryGetService<DialogueController>(out var dialogueController)) yield break;
            ChangeAvatar(npcType, dialogueController);

            var dialogueText = dialogueAction.dialogueText;

            dialogueController.DisplayNextParagraph(dialogueText);
            while(dialogueController.isActive()) {
                yield return WaitUntilSkipped();
                dialogueController.DisplayNextParagraph(dialogueText);
            }

            dialogueAction.consequence.ApplyConsequences();
            TryGrowLimbs();
        }

        private IEnumerator ExecuteChoiceAndDialogue(GameAction action)
        {
            var choiceAndDialogue = action as ChoiceGameAction;

            if (choiceAndDialogue == null) yield break;
            if (!ServiceLocatorMono.Instance.TryGetService<DialogueController>(out var dialogueController)) yield break;
            if (!ServiceLocatorMono.Instance.TryGetService<ChoiceUIController>(out var choiceController)) yield break;
            ChangeAvatar(npcType, dialogueController);

            var dialogueText = choiceAndDialogue.dialogueText;

            
            //handle dialog first
            dialogueController.DisplayNextParagraph(dialogueText);
            while (!dialogueController.isLastParagraph())
            {
                yield return WaitUntilSkipped();
                dialogueController.DisplayNextParagraph(dialogueText);
            }
            //Handle choices
            yield return choiceController.DisplayCards(choiceAndDialogue.FirstCardDto.cardDescription,
                choiceAndDialogue.SecondCardDto.cardDescription);
            yield return WaitUntilChoices();
            ApplyChoosenCardConsequences(choiceAndDialogue);
            TryGrowLimbs();
            yield return choiceController.RemoveCards(choosenCard);

            if (dialogueText is DialogueTextWithReactions dialogueTextWithReactions)
            {
                while (dialogueController.isActive())
                {
                    dialogueController.DisplayNextParagraph(dialogueText);
                }
                var reactionText = DisplayReactionParagraphs(dialogueTextWithReactions);
                dialogueController.DisplayNextParagraph(reactionText);
                while(dialogueController.isActive()) {
                    yield return WaitUntilSkipped();
                    dialogueController.DisplayNextParagraph(reactionText);
                }
            }
            else
            {
                //Handle next replic
                dialogueController.DisplayNextParagraph(dialogueText);
                yield return WaitUntilSkipped();
                //skip dialogue
                dialogueController.DisplayNextParagraph(dialogueText);
            }

            //apply dialogue consequences
            choiceAndDialogue.consequence.ApplyConsequences();
            TryGrowLimbs();
        }

        private void ChangeAvatar(NPCType npcType, DialogueController dialogueController)
        {
            dialogueController.ChangeAvatar(npcType);

        }

        private void TryGrowLimbs()
        {
            if (ServiceLocatorMono.Instance.TryGetService<LimbsViewController>(out var limbsViewController))
            {
                limbsViewController.TryGrowLimbs();
            }
        }

        private DialogueText DisplayReactionParagraphs(DialogueTextWithReactions dialogueTextWithReactions)
        {
            switch (choosenCard)
            {
                case ChoosenCard.One:
                    return DialogueText.CreateInstance(dialogueTextWithReactions.speakerName, dialogueTextWithReactions.reactionOnFirstCard);
                default:
                    return DialogueText.CreateInstance(dialogueTextWithReactions.speakerName, dialogueTextWithReactions.reactionOnSecondCard);
            }
        }

        private void ApplyChoosenCardConsequences(ChoiceGameAction choiceAndDialogue)
        {
            switch (choosenCard)
            {
                case ChoosenCard.One:
                    choiceAndDialogue.FirstCardDto.consequences.ApplyConsequences();
                    break;
                case ChoosenCard.Two:
                    choiceAndDialogue.SecondCardDto.consequences.ApplyConsequences();
                    break;
            }
        }

        #endregion

        #region SKIP/CHOICE FUNCTIONALITY

        private IEnumerator WaitUntilSkipped()
        {
            canBeSkipped = true;
            while (!isSkipped) yield return null;
            isSkipped = false;
            canBeSkipped = false;
        }

        private IEnumerator WaitUntilChoices()
        {
            canBeChoosen = true;
            while (!isChoosen) yield return null;
            isChoosen = false;
            canBeChoosen = false;
        }

        public static void Choose(ChoosenCard card)
        {
            if (!canBeChoosen) return;

            choosenCard = card;
            isChoosen = true;
        }

        public static void Skip()
        {
            AudioContext.PlayGlobalSfx(CLICK_SOUND);
            if (canBeSkipped)
                isSkipped = true;
        }

        #endregion


        private IEnumerator PlayStepsSound()
        {
            const string soundName = "steps";
            AudioContext.PlayGlobalSfx(soundName);
            yield return new WaitForSeconds(AudioContext.GetSoundDuration(soundName, SoundType.Sfx));
        }
    }

    public enum ChoosenCard
    {
        One,
        Two
    }

    public enum NPCType
    {
        BlueMage,
        RedMage,
        GreenMage,
        PurpleMage,
        YellowMage,
        WhiteMage,
        WhiteNoble,
        RedNoble,
        PeasantWhiteM,
        PeasantGrayM,
        PeasantWhiteW,
        PeasantGrayW,
        Demon,
        Executioner,
        Monk,
        BurnedMonk,
        ChildBrownW,
        ChildBlondM,
        ChildBlondW,
        ChildBrownM,
        Void
    }
}