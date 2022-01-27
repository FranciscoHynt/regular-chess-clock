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

        private int extraSeconds;
        private bool isClockRunning;
        private TimeSpan clockTime;
        private TextMeshProUGUI timerText;

        private const string DATE_FORMAT_MS = @"ss\.f";
        private const string DATE_FORMAT_MINUTES = @"mm\:ss";

        public TimerController()
        {
            ClockEvents.ConfigureClockEvent.AddListener(SetTimer);
            ClockEvents.ChangePlayerEvent.AddListener(HandleClockTime);
            ClockEvents.PauseClockEvent.AddListener(ClockPause);
        }

        private void Awake()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void SetTimer(ConfigureClockEventData data)
        {
            extraSeconds = data.ExtraSeconds;
            clockTime = TimeSpan.FromMinutes(data.ClockTime);
            
            UpdateTimerText();
        }

        private void HandleClockTime(PlayerPieces playerOnClock)
        {
            if (pieceColor == playerOnClock)
            {
                StartCoroutine(RunTimerRoutine());
            }
            else if (isClockRunning)
            {
                AddExtraTime();
                StopAllCoroutines();
            }
        }

        private void ClockPause()
        {
            isClockRunning = false;
            StopAllCoroutines();
        }

        private IEnumerator RunTimerRoutine()
        {
            isClockRunning = true;
            WaitForSeconds wait = new WaitForSeconds(0.1f);

            while (clockTime.TotalSeconds > 0)
            {
                UpdateTimerText();
                clockTime = GetClockTime();
                yield return wait;
            }

            isClockRunning = false;
        }

        private TimeSpan GetClockTime()
        {
            return clockTime.Subtract(TimeSpan.FromSeconds(0.1f));
        }

        private void AddExtraTime()
        {
            if (extraSeconds > 0)
            {
                clockTime = clockTime.Add(TimeSpan.FromSeconds(extraSeconds));
                UpdateTimerText();
            }
        }

        private void UpdateTimerText()
        {
            string clockFormat = clockTime.TotalMinutes >= 1
                ? DATE_FORMAT_MINUTES
                : DATE_FORMAT_MS;

            timerText.text = clockTime.ToString(clockFormat);
        }
    }
}