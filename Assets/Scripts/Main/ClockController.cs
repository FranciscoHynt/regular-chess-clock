using System;
using Enum;
using Events;
using TMPro;
using UnityEngine;

namespace Main
{
    public class ClockController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField clockTimeInputField;
        [SerializeField] private TMP_InputField extraTimeInputField;

        private ClockState currentClockState;
        private PlayerPieces currentPlayerOnClock;

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

        private void ChangePlayerOnClock()
        {
            currentPlayerOnClock = currentPlayerOnClock == PlayerPieces.White
                ? PlayerPieces.Black
                : PlayerPieces.White;

            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
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
        }

        public void ResetClock()
        {
            currentClockState = ClockState.Pause;
            currentPlayerOnClock = PlayerPieces.White;

            ClockEvents.PauseClockEvent.Invoke();
            SetClockData();
        }

        public void StartClock()
        {
            SetClockData();
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
        }

        private void SetClockData()
        {
            int clockTime = int.Parse(clockTimeInputField.text);
            int extraSeconds = int.Parse(extraTimeInputField.text);

            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(clockTime, extraSeconds));
        }
    }
}