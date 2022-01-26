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
            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(3, 0));
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
        }

        private void ChangePlayerOnClock()
        {
            currentPlayerOnClock = currentPlayerOnClock == PlayerPieces.White
                ? PlayerPieces.Black
                : PlayerPieces.White;

            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
        }

        public void StartClock()
        {
            int clockTime = int.Parse(clockTimeInputField.text);
            int extraTime = int.Parse(extraTimeInputField.text);

            ClockEvents.ConfigureClockEvent.Invoke(new ConfigureClockEventData(clockTime, extraTime));
            ClockEvents.ChangePlayerEvent.Invoke(currentPlayerOnClock);
        }
    }
}