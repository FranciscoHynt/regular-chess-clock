﻿using Enumerators;
using UnityEngine.Events;

namespace Events
{
    public static class ClockEvents
    {
        public static readonly PauseClockEvent PauseClockEvent = new PauseClockEvent();
        public static readonly ConfigureClockEvent ConfigureClockEvent = new ConfigureClockEvent();
        public static readonly ChangePlayerEvent ChangePlayerEvent = new ChangePlayerEvent();
        public static readonly ChangeClockStateEvent ChangeClockStateEvent = new ChangeClockStateEvent();
    }

    public class PauseClockEvent : UnityEvent{}
    public class ConfigureClockEvent : UnityEvent<ConfigureClockEventData>{}
    public class ChangePlayerEvent : UnityEvent<PlayerPiece>{}
    public class ChangeClockStateEvent : UnityEvent<ClockState>{}

    public readonly struct ConfigureClockEventData
    {
        public int ClockTime { get; }
        public int ExtraSeconds { get; }

        public ConfigureClockEventData(int clockTime, int extraSeconds)
        {
            this.ClockTime = clockTime;
            this.ExtraSeconds = extraSeconds;
        }
    }
}