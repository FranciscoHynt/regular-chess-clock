using System;
using Assets;
using Enumerators;
using Events;
using Sounds;
using TMPro;
using UnityEngine;

namespace Main
{
    public class ClockController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField clockTimeInputField;
        [SerializeField] private TMP_InputField extraTimeInputField;

        private ClockState currentClockState;
        private PlayerPiece currentPlayerOnClock;

        private void Awake()
        {
            clockTimeInputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
            extraTimeInputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentClockState == ClockState.Play)
            {
                ChangePlayerOnClock();
            }
        }

        public void PlayPauseClock()
        {
            switch (currentClockState)
            {
                case ClockState.Play:
                    ClockEvents.PauseClockEvent.Invoke();
                    currentClockState = ClockState.Pause;
                    break;
                case ClockState.Pause:
                    ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
                    currentClockState = ClockState.Play;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentClockState), currentClockState,
                        "ClockState not implemented");
            }

            ClockEvents.ChangeClockStateEvent.Invoke(currentClockState);
        }

        public void ResetClock()
        {
            currentClockState = ClockState.Pause;
            currentPlayerOnClock = PlayerPiece.White;

            SetClockData();
            ClockEvents.PauseClockEvent.Invoke();
            ClockEvents.ChangeClockStateEvent.Invoke(currentClockState);
            InGameSoundManager.PlaySound(Sound.ButtonClick, MainAssets.I.soundSettings.buttonClick);
        }

        public void StartClock()
        {
            SetClockData();
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
            ClockEvents.ChangeClockStateEvent.Invoke(currentClockState);
            InGameSoundManager.PlaySound(Sound.ButtonClick, MainAssets.I.soundSettings.buttonClick);
        }

        private void SetClockData()
        {
            int clockTime = int.Parse(clockTimeInputField.text);
            int extraSeconds = int.Parse(extraTimeInputField.text);

            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(clockTime, extraSeconds));
        }

        private void ChangePlayerOnClock()
        {
            currentPlayerOnClock = currentPlayerOnClock == PlayerPiece.White
                ? PlayerPiece.Black
                : PlayerPiece.White;

            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
            InGameSoundManager.PlaySound(Sound.ClockChange, MainAssets.I.soundSettings.clockChange);
        }
    }
}