using System;
using Enum;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class ClockController : MonoBehaviour
    {
        [SerializeField] private InputField clockTimeInputField;
        [SerializeField] private InputField extraTimeInputField;

        private PlayerPieces currentPlayerOnClock;
        private ClockState currentClockState;

        private void Awake()
        {
            // clockTimeInputField.characterValidation = InputField.CharacterValidation.Integer;
            // extraTimeInputField.characterValidation = InputField.CharacterValidation.Integer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangePlayerOnClock();
            }
        }

        private void Start()
        {
            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(1, 3));
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
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

        public void StartClock()
        {
            int clockTime = int.Parse(clockTimeInputField.text);
            int extraSeconds = int.Parse(extraTimeInputField.text);

            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(clockTime, extraSeconds));
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
        }
    }
}