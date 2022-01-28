using System;
using System.Collections;
using Enumerators;
using Events;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private PlayerPiece pieceColor;
        [SerializeField] private TimerSettings timerSettings;

        private int extraSeconds;
        private int clockMaxSeconds;
        private bool isClockRunning;
        private Image timerBackground;
        private TimeSpan clockTime;
        private TextMeshProUGUI timerText;

        private const string DATE_FORMAT_MS = @"ss\.f";
        private const string DATE_FORMAT_HOURS = @"hh\:mm\:ss";
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
            timerBackground = GetComponentInChildren<Image>();
        }

        private void SetTimer(ConfigureClockEventData data)
        {
            extraSeconds = data.ExtraSeconds;
            clockTime = TimeSpan.FromMinutes(data.ClockTime);
            clockMaxSeconds = (int)clockTime.TotalSeconds;

            UpdateTimerColor();
            UpdateTimerText();
        }

        private void HandleClockTime(PlayerPiece playerOnClock)
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

        private TimeSpan GetClockTime()
        {
            return clockTime.Subtract(TimeSpan.FromSeconds(0.1f));
        }

        private void AddExtraTime()
        {
            if (extraSeconds <= 0) return;
            
            clockTime = clockTime.Add(TimeSpan.FromSeconds(extraSeconds));
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            string clockFormat = clockTime.TotalMinutes >= 1
                ? DATE_FORMAT_MINUTES
                : DATE_FORMAT_MS;

            if (clockTime.TotalHours >= 1)
                clockFormat = DATE_FORMAT_HOURS;

            timerText.text = clockTime.ToString(clockFormat);
        }

        private void UpdateTimerColor()
        {
            float time = (float)clockTime.TotalSeconds / clockMaxSeconds;
            Color currentColor = timerSettings.colors.Evaluate(time);
            timerBackground.color = currentColor;
        }
        
        private IEnumerator RunTimerRoutine()
        {
            isClockRunning = true;
            WaitForSeconds wait = new WaitForSeconds(0.1f);

            while (clockTime.TotalSeconds > 0)
            {
                UpdateTimerText();
                UpdateTimerColor();
                clockTime = GetClockTime();
                yield return wait;
            }

            isClockRunning = false;
        }
    }
}