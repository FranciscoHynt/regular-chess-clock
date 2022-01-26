using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerController : MonoBehaviour
    {
        private TimeSpan clockTime;
        private TextMeshProUGUI timerText;

        private const string DATE_FORMAT = @"mm\:ss";

        public TimerController()
        {
            ClockEvents.StartClockEvent.AddListener(StartTimer);
        }

        private void Awake()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void StartTimer(StartClockEventData data)
        {
            clockTime = TimeSpan.FromMinutes(data.ClockTime);

            StartCoroutine(RunTimerRoutine());
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