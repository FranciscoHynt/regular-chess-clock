using System;
using System.Collections;
using Enum;
using Events;
using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private PlayerPieces pieceColor;

        private TimeSpan clockTime;
        private TextMeshProUGUI timerText;

        private const string DATE_FORMAT = @"mm\:ss";

        public TimerController()
        {
            ClockEvents.ConfigureClockEvent.AddListener(SetTimer);
            ClockEvents.ChangePlayerEvent.AddListener(HandleClock);
        }

        private void Awake()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void SetTimer(ConfigureClockEventData data)
        {
            clockTime = TimeSpan.FromMinutes(data.ClockTime);
        }

        private void HandleClock(PlayerPieces playerOnClock)
        {
            if (pieceColor == playerOnClock)
            {
                StartCoroutine(RunTimerRoutine());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator RunTimerRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(1);

            while (true)
            {
                UpdateTimerText();
                clockTime = GetClockTime();
                yield return wait;
            }
        }

        private TimeSpan GetClockTime()
        {
            return clockTime.Subtract(TimeSpan.FromSeconds(1));
        }

        private void UpdateTimerText()
        {
            timerText.text = clockTime.ToString(DATE_FORMAT);
        }
    }
}