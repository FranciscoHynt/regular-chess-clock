using System;
using System.Collections;
using Assets;
using Enumerators;
using Events;
using Settings;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private Image timerBackground;
        [SerializeField] private TimerSettings timerSettings;
        [SerializeField] private PlayerPiece pieceColor;

        private int extraSeconds;
        private int clockMaxSeconds;
        private bool isClockRunning;
        private TimeSpan clockTime;
        private TextMeshProUGUI timerText;

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
            ClockTimeState timeState = clockTime.TotalMinutes >= 1
                ? ClockTimeState.Minutes
                : ClockTimeState.Seconds;

            if (clockTime.TotalHours >= 1)
                timeState = ClockTimeState.Hours;

            ClockTimerData clockTimerData = timerSettings.GetClockTimerData(timeState);
            timerText.text = clockTime.ToString(clockTimerData.dateFormat);
            timerText.fontSize = clockTimerData.fontSize;
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
            UpdateTimerText();
            UpdateTimerColor();

            ClockEvents.ClockTimeEndedEvent.Invoke(pieceColor == PlayerPiece.Black ? PlayerPiece.White : PlayerPiece.Black);
            InGameSoundManager.PlaySound(SingleSound.ClockEndTime, MainAssets.I.soundSettings.clockEndTime);
        }
    }
}